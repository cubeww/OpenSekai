using System;
using System.Collections.Generic;
using System.IO;
using CriWare;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Sekai
{
	public class SoundManager
	{
		public enum BundleWorkingStatus
		{
			None = 0,
			Loading = 1,
			Loaded = 2
		}

		public class AcbDataMemoryContainer
		{
		}

		private const string MenuCommonBundleName = "sound/menu/menu_common";
		private const string MenuCommonCueSheetName = "MenuCommon";
		private const string MenuCommonAcbFileName = "MenuCommon.acb";
		private const string MenuCommonBuiltInCueSheetName = "MenuCommon_Built_in";
		private const string MenuCommonBuiltInAcbFileName = "MenuCommon_Built_in.acb";
		private const string SoundBundleBuildDataAssetName = "SoundBundleBuildData";
		private const string LiveDefaultSoundBundleName = "live/sound/live_se/default";
		private const string LiveDefaultCommonAcbFileName = "se_live_common.acb.bytes";
		private const string LiveTapSeCustom01BundleName = "live/tap_se/custom01";
		private const string LiveMusicBundleNameBase = "music/long/{0}";
		private const string ProjectSekaiAcfFileName = "ProjectSekai.acf";
		private static readonly string[] LiveTapPredecodeCueNames =
		{
			"se_live_long",
			"se_live_long_critical",
			"se_live_perfect",
			"se_live_great",
			"se_live_good",
			"se_live_critical",
			"se_live_connect",
			"se_live_connect_critical",
			"se_live_flick",
			"se_live_flick_critical",
			"se_live_trace",
			"se_live_trace_critical"
		};
		private static readonly Dictionary<string, string[]> PredecodeCueNamesByBundleName = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
		{
			{ LiveDefaultSoundBundleName, LiveTapPredecodeCueNames },
			{ LiveTapSeCustom01BundleName, LiveTapPredecodeCueNames }
		};

		private static readonly SoundManager instance = new SoundManager();
		private readonly HashSet<string> loadedBundles = new HashSet<string>();
		private readonly List<string> loadedBundleSearchOrder = new List<string>();
		private readonly Dictionary<string, CriAtomExAcb> acbByBundleName = new Dictionary<string, CriAtomExAcb>();
		private readonly Dictionary<string, string> acbFileNameByBundleName = new Dictionary<string, string>
		{
			{ MenuCommonBundleName, MenuCommonAcbFileName },
			{ MenuCommonCueSheetName, MenuCommonAcbFileName },
			{ MenuCommonBuiltInCueSheetName, MenuCommonBuiltInAcbFileName }
		};
		private readonly Dictionary<string, string> convertSeTable = new Dictionary<string, string>();
		private readonly HashSet<string> warnedMissingBundles = new HashSet<string>();
		private readonly HashSet<string> warnedMissingCues = new HashSet<string>();
		private CriAtomExPlayer soundEffectPlayer;
		private CriAtomExPlayer ingameBgmPlayer;
		private CriAtomExPlayback currentIngamePlayback = CriAtomExPlayback.Invalid;
		private uint currentIngamePlaybackRequestId;
		private bool audioSyncedUnityTimerEnabled;
		private bool ingameFallbackTimerActive;
		private long ingameFallbackStartTimeMs;
		private long ingameLastPlaybackTimeMs;
		private bool ingameSmoothTimerRunning;
		private double ingameSmoothTimerStartClockTime;
		private long ingameSmoothTimerStartTimeMs;
		private float masterVolume = 1f;
		private float bgmVolume = 1f;
		private float seVolume = 1f;
		private bool defaultSoundBundlesRequested;
		private bool acfRegisterAttempted;

		public static SoundManager Instance => instance;

		public void SetupVolume(float master, float bgm, float se, float voice)
		{
			masterVolume = Mathf.Clamp01(master);
			bgmVolume = Mathf.Clamp01(bgm);
			seVolume = Mathf.Clamp01(se);
			soundEffectPlayer?.SetVolume(masterVolume * seVolume);
			ingameBgmPlayer?.SetVolume(masterVolume * bgmVolume);
		}

		public bool IsLoadedSoundBundle(string bundleName)
		{
			return !string.IsNullOrEmpty(bundleName)
				&& (loadedBundles.Contains(bundleName) || acbByBundleName.ContainsKey(bundleName));
		}

		public void LoadSoundBundle(string bundleName, bool isResident)
		{
			if (string.IsNullOrEmpty(bundleName) || loadedBundles.Contains(bundleName) || acbByBundleName.ContainsKey(bundleName))
			{
				return;
			}

			if (TryRegisterLoadedBundleAlias(bundleName))
			{
				return;
			}

			if (!defaultSoundBundlesRequested && !IsDefaultSoundBundle(bundleName))
			{
				EnsureDefaultSoundBundlesLoaded();
				if (acbByBundleName.ContainsKey(bundleName))
				{
					return;
				}
			}

			EnsureAtomInitialized();
			EnsureProjectSekaiAcfRegistered();
			List<string> acbFileNames = GetAcbFileNameCandidates(bundleName);
			if (TryLoadSoundBundleFromBuildData(bundleName, acbFileNames))
			{
				return;
			}

			foreach (string acbFileName in acbFileNames)
			{
				CriAtomExAcb acb = null;
				if (ShouldPreferAssetBundleAcb(bundleName))
				{
					byte[] acbBytes = TryLoadAcbBytesFromAssetBundle(bundleName, acbFileName);
					acb = LoadAcbData(acbBytes);
					if (acb == null)
					{
						acb = TryLoadAcbFromStreamingAssets(acbFileName);
					}
				}
				else
				{
					acb = TryLoadAcbFromStreamingAssets(acbFileName);
					if (acb == null)
					{
						byte[] acbBytes = TryLoadAcbBytesFromAssetBundle(bundleName, acbFileName);
						acb = LoadAcbData(acbBytes);
					}
				}
				if (acb == null || !acb.isAvailable)
				{
					continue;
				}

				acbByBundleName[bundleName] = acb;
				loadedBundles.Add(bundleName);
				AddLoadedBundleSearchOrder(bundleName);
				PredecodeSoundBundle(bundleName, acb);
				return;
			}

			LogMissingBundle(bundleName, string.Join(", ", acbFileNames.ToArray()));
		}

		public void Load(string cueSheetName, string acbFileName)
		{
			if (string.IsNullOrEmpty(cueSheetName) || string.IsNullOrEmpty(acbFileName))
			{
				return;
			}

			acbFileNameByBundleName[cueSheetName] = acbFileName;
			LoadSoundBundle(cueSheetName, true);
		}

		public void RegisterExternalAudioClip(string bundleName, string cueName, AudioClip clip)
		{
			if (clip == null || string.IsNullOrEmpty(cueName))
			{
				return;
			}

			if (string.IsNullOrEmpty(bundleName))
			{
				bundleName = cueName;
			}

			EnsureAtomInitialized();
			CriAtomExAcb acb = CriAtomExAcb.CreateFromAudioClip(cueName, clip);
			if (acb == null || !acb.isAvailable)
			{
				return;
			}

			acbByBundleName[bundleName] = acb;
			loadedBundles.Add(bundleName);
			if (!loadedBundleSearchOrder.Contains(bundleName))
			{
				loadedBundleSearchOrder.Add(bundleName);
			}
			string liveMusicBundleName = GetLiveMusicBundleNameCandidate(bundleName);
			if (!string.IsNullOrEmpty(liveMusicBundleName))
			{
				acbByBundleName[liveMusicBundleName] = acb;
				loadedBundles.Add(liveMusicBundleName);
				if (!loadedBundleSearchOrder.Contains(liveMusicBundleName))
				{
					loadedBundleSearchOrder.Add(liveMusicBundleName);
				}
			}
		}

		public bool ExistsCueName(string cueName)
		{
			return ResolveCueName(cueName, out _) != null;
		}

		public CriAtomEx.CueInfo[] GetCueInfos(string bundleName)
		{
			if (string.IsNullOrEmpty(bundleName))
			{
				return Array.Empty<CriAtomEx.CueInfo>();
			}

			LoadSoundBundle(bundleName, true);
			if (!acbByBundleName.TryGetValue(bundleName, out CriAtomExAcb acb) || acb == null)
			{
				return Array.Empty<CriAtomEx.CueInfo>();
			}

			return GetCueInfosWithLength(acb);
		}

		public bool IsAudioSyncedUnityTimerEnabled => audioSyncedUnityTimerEnabled;

		private static double GetAudioClockTime()
		{
			// AudioSettings.dspTime is quantized by the audio buffer on some Unity
			// backends, which makes note movement step every 20-40ms at high FPS.
			// Use Unity's realtime clock for the visual live timer after anchoring
			// the start position to the requested audio playback time.
			return Time.realtimeSinceStartupAsDouble;
		}

		private void StartIngameSmoothTimer(long startTimeMs)
		{
			ingameSmoothTimerStartTimeMs = Math.Max(0L, startTimeMs);
			ingameSmoothTimerStartClockTime = GetAudioClockTime();
			ingameLastPlaybackTimeMs = ingameSmoothTimerStartTimeMs;
			ingameSmoothTimerRunning = true;
		}

		private long GetIngameSmoothTimerMs()
		{
			if (!ingameSmoothTimerRunning)
			{
				return ingameLastPlaybackTimeMs;
			}

			double elapsedSeconds = Math.Max(0.0, GetAudioClockTime() - ingameSmoothTimerStartClockTime);
			long currentTimeMs = ingameSmoothTimerStartTimeMs + (long)(elapsedSeconds * 1000.0);
			return Math.Max(ingameLastPlaybackTimeMs, currentTimeMs);
		}

		private void PauseIngameSmoothTimer()
		{
			if (!ingameSmoothTimerRunning)
			{
				return;
			}

			ingameLastPlaybackTimeMs = GetIngameSmoothTimerMs();
			ingameSmoothTimerRunning = false;
		}

		public uint PrepareIngameBGM(string cueName, float startTime, Action callback = null, bool loop = false)
		{
			StopIngame();
			uint requestId = AllocateIngamePlaybackRequestId();
			float startTimeSeconds = Mathf.Max(0f, startTime);
			ingameFallbackStartTimeMs = (long)(startTimeSeconds * 1000f);
			ingameLastPlaybackTimeMs = ingameFallbackStartTimeMs;
			audioSyncedUnityTimerEnabled = true;

			if (TryResolveIngameCue(cueName, out CriAtomExAcb acb, out string resolvedCueName))
			{
				try
				{
					ingameBgmPlayer = new CriAtomExPlayer();
					ingameBgmPlayer.SetCue(acb, resolvedCueName);
					ingameBgmPlayer.SetVolume(masterVolume * bgmVolume);
					ingameBgmPlayer.SetStartTime(ingameFallbackStartTimeMs);
					currentIngamePlayback = ingameBgmPlayer.Start();
					ingameFallbackTimerActive = currentIngamePlayback.id == 0;
					StartIngameSmoothTimer(ingameFallbackStartTimeMs);
					callback?.Invoke();
					return requestId;
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}

			ingameFallbackTimerActive = true;
			currentIngamePlayback = CriAtomExPlayback.Invalid;
			StartIngameSmoothTimer(ingameFallbackStartTimeMs);
			if (!string.IsNullOrEmpty(cueName))
			{
				LogMissingCue(cueName);
			}
			callback?.Invoke();
			return requestId;
		}

		public CriAtomExPlayback? GetPlayback(uint requestId)
		{
			if (requestId == 0 || requestId != currentIngamePlaybackRequestId || currentIngamePlayback.id == 0)
			{
				return null;
			}
			return currentIngamePlayback;
		}

		public void ResumePreparedPlaybackIngame()
		{
			currentIngamePlayback.Resume();
			if (!ingameSmoothTimerRunning)
			{
				StartIngameSmoothTimer(ingameLastPlaybackTimeMs);
			}
		}

		public void ResumeIngame(long musicTimeMs)
		{
			ingameLastPlaybackTimeMs = Math.Max(0L, musicTimeMs);
			currentIngamePlayback.Resume();
			if (!ingameSmoothTimerRunning)
			{
				StartIngameSmoothTimer(ingameLastPlaybackTimeMs);
			}
		}

		public void SetAudioSyncedUnityTimer(uint requestId)
		{
			if (requestId != 0 && requestId != currentIngamePlaybackRequestId)
			{
				return;
			}
			if (audioSyncedUnityTimerEnabled && !ingameSmoothTimerRunning)
			{
				StartIngameSmoothTimer(ingameLastPlaybackTimeMs);
			}
		}

		public long GetAudioSyncedUnityTimer()
		{
			if (!audioSyncedUnityTimerEnabled)
			{
				return 0L;
			}

			if (currentIngamePlayback.id != 0)
			{
				CriAtomExPlayback.Status status = currentIngamePlayback.GetStatus();
				if (status == CriAtomExPlayback.Status.Playing)
				{
					ingameLastPlaybackTimeMs = GetIngameSmoothTimerMs();
					return ingameLastPlaybackTimeMs;
				}
				if (status == CriAtomExPlayback.Status.PlayEnd || status == CriAtomExPlayback.Status.Stop || status == CriAtomExPlayback.Status.Error)
				{
					return 0L;
				}
				return ingameLastPlaybackTimeMs;
			}

			if (ingameFallbackTimerActive)
			{
				ingameLastPlaybackTimeMs = GetIngameSmoothTimerMs();
				return ingameLastPlaybackTimeMs;
			}

			return 0L;
		}

		public void StopIngame()
		{
			currentIngamePlayback.Stop();
			ingameBgmPlayer?.Dispose();
			ingameBgmPlayer = null;
			currentIngamePlayback = CriAtomExPlayback.Invalid;
			audioSyncedUnityTimerEnabled = false;
			ingameFallbackTimerActive = false;
			ingameLastPlaybackTimeMs = 0L;
			ingameSmoothTimerRunning = false;
			ingameSmoothTimerStartClockTime = 0.0;
			ingameSmoothTimerStartTimeMs = 0L;
		}

		public void StopAll()
		{
			StopIngame();
			soundEffectPlayer?.Stop();
		}

		public uint PlaySE(string cueName)
		{
			return PlaySE(cueName, 1f, 0f, 1);
		}

		public uint PlaySE(string cueName, float vol)
		{
			return PlaySE(cueName, vol, 0f, 1);
		}

		public uint PlaySE(string cueName, float volume = 1f, float startTime = 0f, int playerIndex = 1)
		{
			return PlaySEInternal(cueName, volume, startTime);
		}

		public void PlaySEOneShot(string cueName, int priority = 0)
		{
			PlaySEInternal(cueName, 1f, 0f);
		}

		public uint PlayIngameSE(string cueName)
		{
			return PlaySEInternal(cueName, 1f, 0f);
		}

		public void PlayIngameSEOneShot(string cueName)
		{
			PlaySEInternal(cueName, 1f, 0f);
		}

		public void PlayIngameVoiceOneShot(string cueName)
		{
			PlaySEInternal(cueName, 1f, 0f);
		}

		public void StopSE(uint playbackId)
		{
			if (playbackId == 0)
			{
				return;
			}
			new CriAtomExPlayback(playbackId).Stop();
		}

		public void ResumeIngameSe()
		{
		}

		private uint PlaySEInternal(string cueName, float volume, float startTime)
		{
			string resolvedCueName = ResolveCueName(cueName, out CriAtomExAcb acb);
			if (string.IsNullOrEmpty(resolvedCueName) || acb == null)
			{
				LogMissingCue(cueName);
				return 0;
			}

			try
			{
				CriAtomExPlayer player = GetSoundEffectPlayer();
				player.SetCue(acb, resolvedCueName);
				player.SetVolume(masterVolume * seVolume * Mathf.Clamp01(volume));
				if (startTime > 0f)
				{
					player.SetStartTime((long)(startTime * 1000f));
				}
				CriAtomExPlayback playback = player.Start();
				return playback.id;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return 0;
			}
		}

		private CriAtomExPlayer GetSoundEffectPlayer()
		{
			EnsureAtomInitialized();
			if (soundEffectPlayer == null)
			{
				soundEffectPlayer = new CriAtomExPlayer();
				soundEffectPlayer.SetVolume(masterVolume * seVolume);
			}
			return soundEffectPlayer;
		}

		private string ResolveCueName(string cueName, out CriAtomExAcb acb)
		{
			acb = null;
			if (string.IsNullOrEmpty(cueName))
			{
				return null;
			}

			EnsureDefaultSoundBundlesLoaded();

			// Original SoundManager.Initialize registers MenuCommon_Built_in before
			// dynamic menu bundles, so common button SE should resolve there first.
			string builtInCueName = ResolveCueNameFromBundle(MenuCommonBuiltInCueSheetName, cueName, out acb);
			if (builtInCueName != null)
			{
				return builtInCueName;
			}

			foreach (string bundleName in loadedBundleSearchOrder)
			{
				if (string.Equals(bundleName, MenuCommonBuiltInCueSheetName, StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}

				string resolvedCueName = ResolveCueNameFromBundle(bundleName, cueName, out acb);
				if (resolvedCueName != null)
				{
					return resolvedCueName;
				}
			}

			if (convertSeTable.TryGetValue(cueName, out string convertedCueName))
			{
				builtInCueName = ResolveCueNameFromBundle(MenuCommonBuiltInCueSheetName, convertedCueName, out acb);
				if (builtInCueName != null)
				{
					return builtInCueName;
				}

				foreach (string bundleName in loadedBundleSearchOrder)
				{
					if (string.Equals(bundleName, MenuCommonBuiltInCueSheetName, StringComparison.OrdinalIgnoreCase))
					{
						continue;
					}

					string resolvedCueName = ResolveCueNameFromBundle(bundleName, convertedCueName, out acb);
					if (resolvedCueName != null)
					{
						return resolvedCueName;
					}
				}
			}

			return null;
		}

		private string ResolveCueNameFromBundle(string bundleName, string cueName, out CriAtomExAcb acb)
		{
			acb = null;
			if (string.IsNullOrEmpty(bundleName) || string.IsNullOrEmpty(cueName))
			{
				return null;
			}
			if (!acbByBundleName.TryGetValue(bundleName, out CriAtomExAcb loadedAcb))
			{
				return null;
			}
			if (loadedAcb != null && loadedAcb.Exists(cueName))
			{
				acb = loadedAcb;
				return cueName;
			}
			return null;
		}

		private bool TryResolveIngameCue(string cueName, out CriAtomExAcb acb, out string resolvedCueName)
		{
			acb = null;
			resolvedCueName = null;
			if (string.IsNullOrEmpty(cueName))
			{
				return false;
			}

			string liveMusicBundleName = GetLiveMusicBundleNameCandidate(cueName);
			if (TryResolveCueFromSpecificBundle(liveMusicBundleName, cueName, out acb, out resolvedCueName))
			{
				return true;
			}

			if (TryResolveCueFromSpecificBundle(cueName, cueName, out acb, out resolvedCueName))
			{
				return true;
			}

			foreach (string bundleName in loadedBundleSearchOrder)
			{
				if (TryResolveCueFromSpecificBundle(bundleName, cueName, out acb, out resolvedCueName))
				{
					return true;
				}
			}

			if (!string.IsNullOrEmpty(liveMusicBundleName))
			{
				LoadSoundBundle(liveMusicBundleName, true);
				if (TryResolveCueFromSpecificBundle(liveMusicBundleName, cueName, out acb, out resolvedCueName))
				{
					return true;
				}
			}

			LoadSoundBundle(cueName, true);
			if (TryResolveCueFromSpecificBundle(cueName, cueName, out acb, out resolvedCueName))
			{
				return true;
			}

			if (acbByBundleName.TryGetValue(cueName, out CriAtomExAcb directAcb) && TryGetFirstCueName(directAcb, out resolvedCueName))
			{
				acb = directAcb;
				return true;
			}

			return false;
		}

		private static string GetLiveMusicBundleNameCandidate(string cueName)
		{
			if (string.IsNullOrEmpty(cueName))
			{
				return string.Empty;
			}

			string normalized = cueName.Replace('\\', '/');
			if (normalized.StartsWith("music/long/", StringComparison.OrdinalIgnoreCase))
			{
				return normalized;
			}

			if (normalized.Contains("/"))
			{
				return string.Empty;
			}

			return string.Format(LiveMusicBundleNameBase, normalized);
		}

		private bool TryRegisterLoadedBundleAlias(string bundleName)
		{
			if (string.IsNullOrEmpty(bundleName))
			{
				return false;
			}

			string normalized = bundleName.Replace('\\', '/');
			if (!normalized.StartsWith("music/long/", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}

			string cueName = normalized.Substring("music/long/".Length);
			if (string.IsNullOrEmpty(cueName) || !acbByBundleName.TryGetValue(cueName, out CriAtomExAcb acb) || acb == null)
			{
				return false;
			}

			acbByBundleName[bundleName] = acb;
			loadedBundles.Add(bundleName);
			AddLoadedBundleSearchOrder(bundleName);
			return true;
		}

		private bool TryResolveCueFromSpecificBundle(string bundleName, string cueName, out CriAtomExAcb acb, out string resolvedCueName)
		{
			foreach (string candidateCueName in GetCueNameCandidates(cueName))
			{
				resolvedCueName = ResolveCueNameFromBundle(bundleName, candidateCueName, out acb);
				if (!string.IsNullOrEmpty(resolvedCueName) && acb != null)
				{
					return true;
				}
			}
			resolvedCueName = null;
			acb = null;
			return false;
		}

		private static IEnumerable<string> GetCueNameCandidates(string cueName)
		{
			if (string.IsNullOrEmpty(cueName))
			{
				yield break;
			}
			yield return cueName;
			string normalized = cueName.Replace('\\', '/');
			int slashIndex = normalized.LastIndexOf('/');
			if (slashIndex >= 0 && slashIndex + 1 < normalized.Length)
			{
				normalized = normalized.Substring(slashIndex + 1);
				yield return normalized;
			}
			if (normalized.EndsWith(".acb", StringComparison.OrdinalIgnoreCase))
			{
				yield return normalized.Substring(0, normalized.Length - ".acb".Length);
			}
		}

		private static bool TryGetFirstCueName(CriAtomExAcb acb, out string cueName)
		{
			cueName = null;
			if (acb == null || !acb.isAvailable)
			{
				return false;
			}
			CriAtomEx.CueInfo[] cueInfos = acb.GetCueInfoList();
			for (int i = 0; i < cueInfos.Length; i++)
			{
				if (!string.IsNullOrEmpty(cueInfos[i].name))
				{
					cueName = cueInfos[i].name;
					return true;
				}
			}
			return false;
		}

		private static CriAtomEx.CueInfo[] GetCueInfosWithLength(CriAtomExAcb acb)
		{
			if (acb == null || !acb.isAvailable)
			{
				return Array.Empty<CriAtomEx.CueInfo>();
			}
			CriAtomEx.CueInfo[] cueInfos = acb.GetCueInfoList();
			for (int i = 0; i < cueInfos.Length; i++)
			{
				if (cueInfos[i].length > 0 || string.IsNullOrEmpty(cueInfos[i].name))
				{
					continue;
				}
				try
				{
					AudioClip clip = acb.GetOrCreateAudioClip(cueInfos[i].name);
					if (clip != null)
					{
						cueInfos[i].length = (int)(clip.length * 1000f);
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			return cueInfos;
		}

		private uint AllocateIngamePlaybackRequestId()
		{
			currentIngamePlaybackRequestId++;
			if (currentIngamePlaybackRequestId == 0)
			{
				currentIngamePlaybackRequestId = 1;
			}
			return currentIngamePlaybackRequestId;
		}

		private static void EnsureAtomInitialized()
		{
			if (!CriAtomPlugin.IsLibraryInitialized())
			{
				CriAtomPlugin.InitializeLibrary();
			}
		}

		private void EnsureProjectSekaiAcfRegistered()
		{
			if (acfRegisterAttempted)
			{
				return;
			}

			acfRegisterAttempted = true;
			foreach (string path in GetStreamingAssetPathCandidates(ProjectSekaiAcfFileName))
			{
				if (!ShouldTryStreamingAssetPath(path))
				{
					continue;
				}

				try
				{
					if (CriAtomEx.RegisterAcf(null, path))
					{
						return;
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}

		private string GetAcbFileName(string bundleName)
		{
			if (acbFileNameByBundleName.TryGetValue(bundleName, out string acbFileName))
			{
				return acbFileName;
			}

			string lastSegment = bundleName.Replace('\\', '/');
			int slashIndex = lastSegment.LastIndexOf('/');
			if (slashIndex >= 0)
			{
				lastSegment = lastSegment.Substring(slashIndex + 1);
			}
			return lastSegment + ".acb";
		}

		private void EnsureDefaultSoundBundlesLoaded()
		{
			if (defaultSoundBundlesRequested)
			{
				return;
			}

			defaultSoundBundlesRequested = true;
			// IDA shows the original app loads MenuCommon_Built_in on SoundManager.Initialize.
			// MenuCommon is kept as a project-local supplement for score-maker cues copied from AssetRipper.
			LoadSoundBundle(MenuCommonBuiltInCueSheetName, true);
			LoadSoundBundle(MenuCommonBundleName, true);
		}

		private static bool IsDefaultSoundBundle(string bundleName)
		{
			return string.Equals(bundleName, MenuCommonBuiltInCueSheetName, StringComparison.OrdinalIgnoreCase)
				|| string.Equals(bundleName, MenuCommonBundleName, StringComparison.OrdinalIgnoreCase)
				|| string.Equals(bundleName, MenuCommonCueSheetName, StringComparison.OrdinalIgnoreCase);
		}

		private static bool ShouldPreferAssetBundleAcb(string bundleName)
		{
			// Project-local menu_common contains score-maker cues and is built through
			// the README AssetBundle flow; the loose StreamingAssets ACB is only a fallback.
			return string.Equals(bundleName, MenuCommonBundleName, StringComparison.OrdinalIgnoreCase);
		}

		private List<string> GetAcbFileNameCandidates(string bundleName)
		{
			List<string> candidates = new List<string>();
			AddAcbFileNameCandidateWithVariants(candidates, GetAcbFileName(bundleName));
			if (string.Equals(bundleName, LiveDefaultSoundBundleName, StringComparison.OrdinalIgnoreCase))
			{
				AddAcbFileNameCandidateWithVariants(candidates, LiveDefaultCommonAcbFileName);
			}
			if (string.Equals(bundleName, MenuCommonBuiltInCueSheetName, StringComparison.OrdinalIgnoreCase))
			{
				AddAcbFileNameCandidateWithVariants(candidates, MenuCommonBuiltInAcbFileName);
			}
			if (string.Equals(bundleName, MenuCommonBundleName, StringComparison.OrdinalIgnoreCase)
				|| string.Equals(bundleName, MenuCommonCueSheetName, StringComparison.OrdinalIgnoreCase))
			{
				AddAcbFileNameCandidateWithVariants(candidates, MenuCommonAcbFileName);
			}
			return candidates;
		}

		private static void AddAcbFileNameCandidateWithVariants(List<string> candidates, string acbFileName)
		{
			if (string.IsNullOrEmpty(acbFileName))
			{
				return;
			}

			if (acbFileName.EndsWith(".bytes", StringComparison.OrdinalIgnoreCase))
			{
				string withoutBytes = acbFileName.Substring(0, acbFileName.Length - ".bytes".Length);
				AddAcbFileNameCandidate(candidates, withoutBytes);
				AddAcbFileNameCandidate(candidates, acbFileName);
				return;
			}

			AddAcbFileNameCandidate(candidates, acbFileName);
			AddAcbFileNameCandidate(candidates, acbFileName + ".bytes");
		}

		private static void AddAcbFileNameCandidate(List<string> candidates, string acbFileName)
		{
			if (string.IsNullOrEmpty(acbFileName) || candidates.Contains(acbFileName))
			{
				return;
			}
			candidates.Add(acbFileName);
		}

		public SoundBundleBuildData GetSoundBundleBuildData(string bundleName)
		{
			if (string.IsNullOrEmpty(bundleName))
			{
				return null;
			}

#if UNITY_EDITOR
			SoundBundleBuildData editorBuildData = TryLoadSoundBundleBuildDataFromEditorAssets(bundleName);
			if (editorBuildData != null)
			{
				return editorBuildData;
			}
#endif

			try
			{
				if (!AssetBundleManager.Instance.HasBundle(bundleName))
				{
					return null;
				}

				return AssetBundleUtility.LoadAsset<SoundBundleBuildData>(bundleName, SoundBundleBuildDataAssetName, false);
			}
			catch
			{
				return null;
			}
		}

		private bool TryLoadSoundBundleFromBuildData(string bundleName, List<string> diagnosticCandidates)
		{
			SoundBundleBuildData buildData = GetSoundBundleBuildData(bundleName);
			SoundBundleData[] acbFiles = buildData?.AcbFiles;
			if (acbFiles == null || acbFiles.Length == 0)
			{
				return false;
			}

			bool loadedAny = false;
			for (int i = 0; i < acbFiles.Length; i++)
			{
				SoundBundleData soundBundleData = acbFiles[i];
				if (soundBundleData == null)
				{
					continue;
				}

				List<string> acbFileCandidates = new List<string>();
				AddAcbFileNameCandidateWithVariants(acbFileCandidates, soundBundleData.AssetBundleFileName);
				for (int j = 0; j < acbFileCandidates.Count; j++)
				{
					AddAcbFileNameCandidate(diagnosticCandidates, acbFileCandidates[j]);
					byte[] acbBytes = TryLoadAcbBytesFromAssetBundle(bundleName, acbFileCandidates[j]);
					CriAtomExAcb acb = LoadAcbData(acbBytes);
					if (acb == null)
					{
						acb = TryLoadAcbFromStreamingAssets(acbFileCandidates[j]);
					}
					if (acb == null || !acb.isAvailable)
					{
						continue;
					}

					string cueSheetName = GetCueSheetName(soundBundleData);
					if (string.IsNullOrEmpty(cueSheetName))
					{
						cueSheetName = bundleName;
					}

					acbByBundleName[cueSheetName] = acb;
					AddLoadedBundleSearchOrder(cueSheetName);
					if (!acbByBundleName.ContainsKey(bundleName))
					{
						acbByBundleName[bundleName] = acb;
					}
					PredecodeSoundBundle(bundleName, acb);
					loadedAny = true;
					break;
				}
			}

			if (!loadedAny)
			{
				return false;
			}

			loadedBundles.Add(bundleName);
			return true;
		}

		private static string GetCueSheetName(SoundBundleData soundBundleData)
		{
			if (!string.IsNullOrEmpty(soundBundleData.CueSheetName))
			{
				return soundBundleData.CueSheetName;
			}

			string fileName = soundBundleData.AssetBundleFileName;
			if (string.IsNullOrEmpty(fileName))
			{
				return string.Empty;
			}

			if (fileName.EndsWith(".bytes", StringComparison.OrdinalIgnoreCase))
			{
				fileName = fileName.Substring(0, fileName.Length - ".bytes".Length);
			}
			if (fileName.EndsWith(".acb", StringComparison.OrdinalIgnoreCase))
			{
				fileName = fileName.Substring(0, fileName.Length - ".acb".Length);
			}
			return Path.GetFileName(fileName);
		}

		private void AddLoadedBundleSearchOrder(string bundleName)
		{
			if (!string.IsNullOrEmpty(bundleName) && !loadedBundleSearchOrder.Contains(bundleName))
			{
				loadedBundleSearchOrder.Add(bundleName);
			}
		}

		private static void PredecodeSoundBundle(string bundleName, CriAtomExAcb acb)
		{
			if (acb == null || !acb.isAvailable)
			{
				return;
			}

			string[] cueNames = GetPredecodeCueNames(bundleName);
			if (cueNames == null || cueNames.Length == 0)
			{
				return;
			}

			try
			{
				acb.PreloadAudioClips(cueNames);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		private static string[] GetPredecodeCueNames(string bundleName)
		{
			if (string.IsNullOrEmpty(bundleName))
			{
				return null;
			}

			string normalized = bundleName.Replace('\\', '/').Trim('/');
			if (PredecodeCueNamesByBundleName.TryGetValue(normalized, out string[] cueNames))
			{
				return cueNames;
			}

			int slashIndex = normalized.LastIndexOf('/');
			while (slashIndex > 0)
			{
				normalized = normalized.Substring(0, slashIndex);
				if (PredecodeCueNamesByBundleName.TryGetValue(normalized, out cueNames))
				{
					return cueNames;
				}
				slashIndex = normalized.LastIndexOf('/');
			}

			return null;
		}

		private static byte[] TryLoadAcbBytesFromAssetBundle(string bundleName, string acbFileName)
		{
			if (string.IsNullOrEmpty(bundleName) || string.IsNullOrEmpty(acbFileName))
			{
				return null;
			}

			try
			{
#if UNITY_EDITOR
				byte[] editorBytes = TryLoadAcbBytesFromEditorAssets(bundleName, acbFileName);
				if (editorBytes != null && editorBytes.Length > 0)
				{
					return editorBytes;
				}
#endif

				if (!AssetBundleManager.Instance.HasBundle(bundleName))
				{
					return null;
				}
				TextAsset acbAsset = AssetBundleUtility.LoadAsset<TextAsset>(bundleName, acbFileName, false);
				return acbAsset != null ? acbAsset.bytes : null;
			}
			catch
			{
				return null;
			}
		}

#if UNITY_EDITOR
		private static SoundBundleBuildData TryLoadSoundBundleBuildDataFromEditorAssets(string bundleName)
		{
			foreach (string basePath in GetEditorAssetBundleResourceBasePaths(bundleName))
			{
				string path = basePath + "/" + SoundBundleBuildDataAssetName + ".asset";
				SoundBundleBuildData buildData = AssetDatabase.LoadAssetAtPath<SoundBundleBuildData>(path);
				if (buildData != null)
				{
					return buildData;
				}
			}
			return null;
		}

		private static byte[] TryLoadAcbBytesFromEditorAssets(string bundleName, string acbFileName)
		{
			foreach (string basePath in GetEditorAssetBundleResourceBasePaths(bundleName))
			{
				foreach (string fileName in GetEditorAcbFileNameCandidates(acbFileName))
				{
					string path = basePath + "/" + fileName;
					TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
					if (textAsset != null && textAsset.bytes != null && textAsset.bytes.Length > 0)
					{
						return textAsset.bytes;
					}

					string fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);
					if (File.Exists(fullPath))
					{
						return File.ReadAllBytes(fullPath);
					}
				}
			}
			return null;
		}

		private static IEnumerable<string> GetEditorAssetBundleResourceBasePaths(string bundleName)
		{
			string normalized = bundleName.Replace('\\', '/').Trim('/');
			yield return "Assets/Sekai/assetbundle/resources/startapp/" + normalized;
			yield return "Assets/Sekai/assetbundle/resources/tutorial/" + normalized;
			yield return "Assets/Sekai/assetbundle/resources/" + normalized;
		}

		private static IEnumerable<string> GetEditorAcbFileNameCandidates(string acbFileName)
		{
			if (string.IsNullOrEmpty(acbFileName))
			{
				yield break;
			}

			yield return acbFileName;
			if (acbFileName.EndsWith(".bytes", StringComparison.OrdinalIgnoreCase))
			{
				yield return acbFileName.Substring(0, acbFileName.Length - ".bytes".Length);
			}
			else
			{
				yield return acbFileName + ".bytes";
			}
		}
#endif

		private static CriAtomExAcb LoadAcbData(byte[] acbBytes)
		{
			if (acbBytes == null || acbBytes.Length == 0)
			{
				return null;
			}

			try
			{
				return CriAtomExAcb.LoadAcbData(acbBytes, null, null);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return null;
			}
		}

		private static CriAtomExAcb TryLoadAcbFromStreamingAssets(string acbFileName)
		{
			if (string.IsNullOrEmpty(acbFileName))
			{
				return null;
			}

			foreach (string candidate in GetStreamingAssetPathCandidates(acbFileName))
			{
				if (!ShouldTryStreamingAssetPath(candidate))
				{
					continue;
				}

				try
				{
					CriAtomExAcb acb = CriAtomExAcb.LoadAcbFile(null, candidate, null);
					if (acb != null && acb.isAvailable)
					{
						return acb;
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}

			return null;
		}

		private static IEnumerable<string> GetStreamingAssetPathCandidates(string fileName)
		{
			string[] candidates =
			{
				Path.Combine(CriWare.Common.streamingAssetsPath, fileName),
				Path.Combine(CriWare.Common.streamingAssetsPath, "data", fileName),
				Path.Combine(CriWare.Common.streamingAssetsPath, "sound/menu/menu_common", fileName),
				Path.Combine(Application.streamingAssetsPath, fileName),
				Path.Combine(Application.streamingAssetsPath, "data", fileName),
				Path.Combine(Application.streamingAssetsPath, "sound/menu/menu_common", fileName)
			};

			foreach (string candidate in candidates)
			{
				if (!string.IsNullOrEmpty(candidate))
				{
					yield return candidate;
				}
			}
		}

		private static bool ShouldTryStreamingAssetPath(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}

			if (Application.platform == RuntimePlatform.Android)
			{
				return true;
			}

			return File.Exists(path);
		}

		private void LogMissingBundle(string bundleName, string acbFileName)
		{
			string key = bundleName + "|" + acbFileName;
			if (warnedMissingBundles.Add(key))
			{
				Debug.LogWarningFormat("Sound bundle could not be loaded. bundle:{0} acb:{1}", bundleName, acbFileName);
			}
		}

		private void LogMissingCue(string cueName)
		{
			if (!string.IsNullOrEmpty(cueName) && warnedMissingCues.Add(cueName))
			{
				Debug.LogWarningFormat("{0} は登録されていません。", cueName);
			}
		}

		public void Pause()
		{
			PauseIngameSmoothTimer();
			currentIngamePlayback.Pause();
		}

		public void Resume()
		{
			currentIngamePlayback.Resume();
			if (!ingameSmoothTimerRunning)
			{
				StartIngameSmoothTimer(ingameLastPlaybackTimeMs);
			}
		}
	}
}
