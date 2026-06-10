using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using CriWare;
using Sekai.Live;
using UnityEngine;
using UnityEngine.Networking;
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
		private const string LiveTapSeBundleNamePrefix = "live/tap_se";
		private const string LiveTapSeCustom01BundleName = "live/tap_se/custom01";
		private const string LiveMusicBundleNameBase = "music/long/{0}";
		private const string ProjectSekaiAcfFileName = "ProjectSekai.acf";
		private const string CriwareGameObjectName = "CRIWARE";
		private const byte CriAcfCompatibleTarget = 6;
		private const uint ExternalWaveVoicePoolIdentifier = 0x4F534B57;
		private const int CriAtomOutputSamplingRate = 48000;
		private const int CriAtomVoicePoolMaxSamplingRate = 96000;
		private static readonly string[] LiveTapCueNames =
		{
			"se_live_long",
			"se_live_long_critical",
			"se_live_perfect",
			"se_live_tap",
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

		private static readonly HashSet<string> LiveTapCueNameSet = new HashSet<string>(LiveTapCueNames, StringComparer.OrdinalIgnoreCase);

		private static readonly SoundManager instance = new SoundManager();
		private readonly HashSet<string> loadedBundles = new HashSet<string>();
		private readonly List<string> loadedBundleSearchOrder = new List<string>();
		private readonly Dictionary<string, CriAtomExAcb> acbByBundleName = new Dictionary<string, CriAtomExAcb>();
		private readonly Dictionary<string, ExternalAudioEntry> externalAudioByKey = new Dictionary<string, ExternalAudioEntry>(StringComparer.OrdinalIgnoreCase);
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
		private CriAtomExPlayer ingameSoundEffectPlayer;
		private CriAtomExPlayer ingameBgmPlayer;
		private CriAtomExWaveVoicePool externalWaveVoicePool;
		private int externalWaveVoicePoolMaxChannels;
		private int externalWaveVoicePoolMaxSamplingRate;
		private string externalIngamePlayerKey;
		private long externalIngamePlayerSourceVersion;
		private int externalIngamePlayerChannels;
		private int externalIngamePlayerSampleRate;
		private GCHandle externalAudioDataHandle;
		private CriAtomExPlayback currentIngamePlayback = CreateInvalidPlayback();
		private uint currentIngamePlaybackRequestId;
		private AudioSyncedUnityTimer audioSyncedUnityTimer;
		private GCHandle acfDataHandle;
		private float masterVolume = 1f;
		private float bgmVolume = 1f;
		private float seVolume = 1f;
		private bool initialized;
		private static bool criRuntimeInitialized;
		private static GameObject criwareGameObject;
		private bool defaultSoundBundlesRequested;
		private bool acfRegisterAttempted;

		public static SoundManager Instance => instance;

		public void Initialize()
		{
			if (initialized)
			{
				return;
			}

			EnsureAtomInitialized();
			EnsureProjectSekaiAcfRegistered();
			GetSoundEffectPlayer();
			GetIngameSoundEffectPlayer();
			EnsureDefaultSoundBundlesLoaded();
			initialized = true;
		}

		public void SetupVolume(float master, float bgm, float se, float voice)
		{
			masterVolume = Mathf.Clamp01(master);
			bgmVolume = Mathf.Clamp01(bgm);
			seVolume = Mathf.Clamp01(se);
			soundEffectPlayer?.SetVolume(masterVolume * seVolume);
			ingameSoundEffectPlayer?.SetVolume(masterVolume * seVolume);
			ingameBgmPlayer?.SetVolume(masterVolume * bgmVolume);
		}

		public bool IsLoadedSoundBundle(string bundleName)
		{
			return !string.IsNullOrEmpty(bundleName)
				&& (loadedBundles.Contains(bundleName) || acbByBundleName.ContainsKey(bundleName) || externalAudioByKey.ContainsKey(bundleName));
		}

		public bool IsExternalAudioRegistered(string key)
		{
			return TryResolveExternalAudio(key, out _);
		}

		public void LoadSoundBundle(string bundleName, bool isResident)
		{
			if (string.IsNullOrEmpty(bundleName) || loadedBundles.Contains(bundleName) || acbByBundleName.ContainsKey(bundleName) || externalAudioByKey.ContainsKey(bundleName))
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

		public bool RegisterExternalAudioData(string key, byte[] wavData, int channels, int sampleRate, long lengthMs, long sourceVersion)
		{
			if (string.IsNullOrEmpty(key) || wavData == null || wavData.Length <= 44 || channels <= 0 || sampleRate <= 0)
			{
				Debug.LogWarningFormat(
					"External audio could not be registered. key:{0} bytes:{1} channels:{2} sampleRate:{3}",
					key,
					wavData?.Length ?? 0,
					channels,
					sampleRate);
				return false;
			}

			ReleaseExternalAudioData();
			ExternalAudioEntry entry = new ExternalAudioEntry
			{
				Key = key,
				WavData = wavData,
				Channels = channels,
				SampleRate = sampleRate,
				LengthMs = Math.Max(0L, lengthMs),
				SourceVersion = sourceVersion
			};
			RegisterExternalAudioEntry(key, entry);

			string liveMusicBundleName = GetLiveMusicBundleNameCandidate(key);
			if (!string.IsNullOrEmpty(liveMusicBundleName))
			{
				RegisterExternalAudioEntry(liveMusicBundleName, entry);
			}
			return IsExternalAudioRegistered(key);
		}

		public bool ExistsCueName(string cueName)
		{
			return TryResolveExternalAudio(cueName, out _) || ResolveCueName(cueName, out _) != null;
		}

		public CriAtomEx.CueInfo[] GetCueInfos(string bundleName)
		{
			if (string.IsNullOrEmpty(bundleName))
			{
				return Array.Empty<CriAtomEx.CueInfo>();
			}

			if (externalAudioByKey.TryGetValue(bundleName, out ExternalAudioEntry externalAudio))
			{
				return new[]
				{
					new CriAtomEx.CueInfo
					{
						length = externalAudio.LengthMs
					}
				};
			}

			LoadSoundBundle(bundleName, true);
			if (!acbByBundleName.TryGetValue(bundleName, out CriAtomExAcb acb) || acb == null)
			{
				return Array.Empty<CriAtomEx.CueInfo>();
			}

			return GetCueInfosWithLength(acb);
		}

		public bool IsAudioSyncedUnityTimerEnabled => audioSyncedUnityTimer != null;

		public uint PrepareIngameBGM(string cueName, float startTime, Action callback = null, bool loop = false)
		{
			uint requestId = AllocateIngamePlaybackRequestId();
			float startTimeSeconds = Mathf.Max(0f, startTime);
			long startTimeMs = (long)(startTimeSeconds * 1000f);

			if (TryResolveExternalAudio(cueName, out ExternalAudioEntry externalAudio))
			{
				try
				{
					bool reusePlayer = CanReuseExternalIngamePlayer(externalAudio);
					StopIngame(reusePlayer);
					EnsureExternalWaveVoicePool(externalAudio.Channels, externalAudio.SampleRate);
					if (!reusePlayer)
					{
						ConfigureExternalIngamePlayer(externalAudio);
					}
					ingameBgmPlayer.SetVolume(masterVolume * bgmVolume);
					ingameBgmPlayer.Loop(loop);
					ingameBgmPlayer.SetStartTime(startTimeMs);
					currentIngamePlayback = ingameBgmPlayer.Prepare();
					audioSyncedUnityTimer = new AudioSyncedUnityTimer(currentIngamePlayback);
					callback?.Invoke();
					return requestId;
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}

			if (IsCustomExternalAudioKey(cueName))
			{
				StopIngame();
				currentIngamePlayback = CreateInvalidPlayback();
				audioSyncedUnityTimer = null;
				LogMissingCue(cueName);
				callback?.Invoke();
				return requestId;
			}

			StopIngame();
			if (TryResolveIngameCue(cueName, out CriAtomExAcb acb, out string resolvedCueName))
			{
				try
				{
					ingameBgmPlayer = new CriAtomExPlayer(true);
					ingameBgmPlayer.SetCue(acb, resolvedCueName);
					ingameBgmPlayer.SetVolume(masterVolume * bgmVolume);
					ingameBgmPlayer.Loop(loop);
					ingameBgmPlayer.SetStartTime(startTimeMs);
					currentIngamePlayback = ingameBgmPlayer.Prepare();
					audioSyncedUnityTimer = new AudioSyncedUnityTimer(currentIngamePlayback);
					callback?.Invoke();
					return requestId;
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}

			currentIngamePlayback = CreateInvalidPlayback();
			audioSyncedUnityTimer = null;
			if (!string.IsNullOrEmpty(cueName))
			{
				LogMissingCue(cueName);
			}
			callback?.Invoke();
			return requestId;
		}

		public CriAtomExPlayback? GetPlayback(uint requestId)
		{
			if (requestId == 0 || requestId != currentIngamePlaybackRequestId || !IsPlaybackValid(currentIngamePlayback))
			{
				return null;
			}
			return currentIngamePlayback;
		}

		public bool IsIngamePlaybackReady(uint requestId)
		{
			CriAtomExPlayback? playback = GetPlayback(requestId);
			if (!playback.HasValue)
			{
				return true;
			}
			CriAtomExPlayback.Status status = playback.Value.GetStatus();
			return status == CriAtomExPlayback.Status.Playing || status == CriAtomExPlayback.Status.Removed;
		}

		public void ResumePreparedPlaybackIngame()
		{
			if (ingameBgmPlayer != null)
			{
				ingameBgmPlayer.Resume(CriAtomEx.ResumeMode.AllPlayback);
			}
			ingameSoundEffectPlayer?.Resume(CriAtomEx.ResumeMode.AllPlayback);
		}

		public void ResumeIngame(long musicTimeMs)
		{
			if (ingameBgmPlayer != null)
			{
				ingameBgmPlayer.SetStartTime(Math.Max(0L, musicTimeMs));
				ingameBgmPlayer.Resume(CriAtomEx.ResumeMode.AllPlayback);
			}
			ingameSoundEffectPlayer?.Resume(CriAtomEx.ResumeMode.AllPlayback);
		}

		public void PauseIngame(long musicTimeMs)
		{
			if (ingameBgmPlayer != null)
			{
				ingameBgmPlayer.SetStartTime(Math.Max(0L, musicTimeMs));
				ingameBgmPlayer.Pause();
			}
			ingameSoundEffectPlayer?.Pause();
		}

		public void SetAudioSyncedUnityTimer(uint requestId)
		{
			if (requestId != 0 && requestId != currentIngamePlaybackRequestId)
			{
				return;
			}
			ResetAudioSyncedUnityTimer();
		}

		public long GetAudioSyncedUnityTimer()
		{
			if (audioSyncedUnityTimer == null)
			{
				return 0L;
			}

			audioSyncedUnityTimer.Execute(Time.time);
			return audioSyncedUnityTimer.PlaybackTime;
		}

		public bool TryGetAudioSyncedUnityTimer(out long playbackTime)
		{
			playbackTime = 0L;
			if (audioSyncedUnityTimer == null)
			{
				return false;
			}

			audioSyncedUnityTimer.Execute(Time.time);
			playbackTime = audioSyncedUnityTimer.PlaybackTime;
			return true;
		}

		private void ResetAudioSyncedUnityTimer()
		{
			audioSyncedUnityTimer = IsPlaybackValid(currentIngamePlayback) ? new AudioSyncedUnityTimer(currentIngamePlayback) : null;
		}

		public void StopIngame()
		{
			StopIngame(false);
		}

		private void StopIngame(bool keepExternalPlayer)
		{
			if (IsPlaybackValid(currentIngamePlayback))
			{
				currentIngamePlayback.Stop();
			}
			if (!keepExternalPlayer)
			{
				ingameBgmPlayer?.Dispose();
				ingameBgmPlayer = null;
				externalIngamePlayerKey = null;
				externalIngamePlayerSourceVersion = 0L;
				externalIngamePlayerChannels = 0;
				externalIngamePlayerSampleRate = 0;
				if (externalAudioDataHandle.IsAllocated)
				{
					externalAudioDataHandle.Free();
				}
			}
			currentIngamePlayback = CreateInvalidPlayback();
			audioSyncedUnityTimer = null;
		}

		public void StopAll()
		{
			StopIngame();
			soundEffectPlayer?.Stop();
			ingameSoundEffectPlayer?.Stop();
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
			return PlayIngameSEInternal(cueName);
		}

		public void PlayIngameSEOneShot(string cueName)
		{
			PlayIngameSEOneShotInternal(cueName, 0);
		}

		public void PlayIngameVoiceOneShot(string cueName)
		{
			PlayIngameSEOneShotInternal(cueName, 0);
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
			ingameSoundEffectPlayer?.Resume(CriAtomEx.ResumeMode.AllPlayback);
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
				player.Loop(false);
				player.SetStartTime((long)(Mathf.Max(0f, startTime) * 1000f));
				CriAtomExPlayback playback = player.Start();
				return playback.id;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return 0;
			}
		}

		private uint PlayIngameSEInternal(string cueName)
		{
			string resolvedCueName = ResolveCueName(cueName, out CriAtomExAcb acb);
			if (string.IsNullOrEmpty(resolvedCueName) || acb == null)
			{
				LogMissingCue(cueName);
				return 0;
			}

			try
			{
				CriAtomExPlayer player = GetIngameSoundEffectPlayer();
				player.SetCue(acb, resolvedCueName);
				player.SetVolume(masterVolume * seVolume);
				player.Loop(false);
				player.SetStartTime(0L);
				CriAtomExPlayback playback = player.Start();
				return playback.id;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return 0;
			}
		}

		private void PlayIngameSEOneShotInternal(string cueName, int priority)
		{
			string resolvedCueName = ResolveCueName(cueName, out CriAtomExAcb acb);
			if (string.IsNullOrEmpty(resolvedCueName) || acb == null)
			{
				LogMissingCue(cueName);
				return;
			}

			try
			{
				CriAtomExPlayer player = GetIngameSoundEffectPlayer();
				player.SetCue(acb, resolvedCueName);
				player.SetVoicePriority(priority);
				player.Start();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		private CriAtomExPlayer GetSoundEffectPlayer()
		{
			EnsureAtomInitialized();
			if (soundEffectPlayer == null)
			{
				soundEffectPlayer = new CriAtomExPlayer();
				soundEffectPlayer.SetSoundRendererType(CriAtomEx.SoundRendererType.Native);
				soundEffectPlayer.SetVolume(masterVolume * seVolume);
			}
			return soundEffectPlayer;
		}

		private CriAtomExPlayer GetIngameSoundEffectPlayer()
		{
			EnsureAtomInitialized();
			if (ingameSoundEffectPlayer == null)
			{
				ingameSoundEffectPlayer = new CriAtomExPlayer();
				ingameSoundEffectPlayer.SetSoundRendererType(CriAtomEx.SoundRendererType.Native);
				ingameSoundEffectPlayer.SetVolume(masterVolume * seVolume);
			}
			return ingameSoundEffectPlayer;
		}

		private string ResolveCueName(string cueName, out CriAtomExAcb acb)
		{
			acb = null;
			if (string.IsNullOrEmpty(cueName))
			{
				return null;
			}

			EnsureDefaultSoundBundlesLoaded();

			if (IsLiveTapCueName(cueName) && TryResolveCurrentLiveTapSeCue(cueName, out acb, out string currentTapSeCueName))
			{
				return currentTapSeCueName;
			}

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

		private static bool IsLiveTapCueName(string cueName)
		{
			return !string.IsNullOrEmpty(cueName) && LiveTapCueNameSet.Contains(cueName);
		}

		private bool TryResolveCurrentLiveTapSeCue(string cueName, out CriAtomExAcb acb, out string resolvedCueName)
		{
			string bundleName = LiveTapSeBundleNamePrefix + "/" + LiveConfig.NoteSeName;
			resolvedCueName = ResolveCueNameFromBundle(bundleName, cueName, out acb);
			return resolvedCueName != null;
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

		private void RegisterExternalAudioEntry(string key, ExternalAudioEntry entry)
		{
			externalAudioByKey[key] = entry;
			loadedBundles.Add(key);
			AddLoadedBundleSearchOrder(key);
		}

		private void ReleaseExternalAudioData()
		{
			StopIngame();
			foreach (string key in new List<string>(externalAudioByKey.Keys))
			{
				loadedBundles.Remove(key);
				loadedBundleSearchOrder.Remove(key);
			}
			externalAudioByKey.Clear();
			if (externalAudioDataHandle.IsAllocated)
			{
				externalAudioDataHandle.Free();
			}
		}

		private bool TryResolveExternalAudio(string key, out ExternalAudioEntry entry)
		{
			if (!string.IsNullOrEmpty(key) && externalAudioByKey.TryGetValue(key, out entry))
			{
				return true;
			}

			string liveMusicBundleName = GetLiveMusicBundleNameCandidate(key);
			if (!string.IsNullOrEmpty(liveMusicBundleName) && externalAudioByKey.TryGetValue(liveMusicBundleName, out entry))
			{
				return true;
			}

			entry = null;
			return false;
		}

		private static bool IsCustomExternalAudioKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return false;
			}

			string normalized = key.Replace('\\', '/');
			const string liveMusicPrefix = "music/long/";
			if (normalized.StartsWith(liveMusicPrefix, StringComparison.OrdinalIgnoreCase))
			{
				normalized = normalized.Substring(liveMusicPrefix.Length);
			}
			return normalized.StartsWith("custom_", StringComparison.OrdinalIgnoreCase);
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
			return acb.GetCueInfoList();
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

		private static CriAtomExPlayback CreateInvalidPlayback()
		{
			return new CriAtomExPlayback(CriAtomExPlayback.invalidId);
		}

		private static bool IsPlaybackValid(CriAtomExPlayback playback)
		{
			return playback.id != 0 && playback.id != CriAtomExPlayback.invalidId;
		}

		private void EnsureExternalWaveVoicePool(int channels, int samplingRate)
		{
			EnsureAtomInitialized();
			int maxChannels = Mathf.Max(2, channels);
			// OpenSekai compatibility: at CRI serverFrequency 120Hz, a 48000Hz pool
			// does not allocate enough decoder frame capacity for ordinary 44100Hz WAV.
			int maxSamplingRate = Mathf.Max(CriAtomVoicePoolMaxSamplingRate, samplingRate);
			if (externalWaveVoicePool != null
				&& externalWaveVoicePoolMaxChannels >= maxChannels
				&& externalWaveVoicePoolMaxSamplingRate >= maxSamplingRate)
			{
				return;
			}

			externalWaveVoicePool?.Dispose();
			externalWaveVoicePool = new CriAtomExWaveVoicePool(1, maxChannels, maxSamplingRate, false, ExternalWaveVoicePoolIdentifier);
			externalWaveVoicePoolMaxChannels = maxChannels;
			externalWaveVoicePoolMaxSamplingRate = maxSamplingRate;
		}

		private bool CanReuseExternalIngamePlayer(ExternalAudioEntry externalAudio)
		{
			return ingameBgmPlayer != null
				&& externalAudio != null
				&& externalAudioDataHandle.IsAllocated
				&& string.Equals(externalIngamePlayerKey, externalAudio.Key, StringComparison.Ordinal)
				&& externalIngamePlayerSourceVersion == externalAudio.SourceVersion
				&& externalIngamePlayerChannels == externalAudio.Channels
				&& externalIngamePlayerSampleRate == externalAudio.SampleRate;
		}

		private void ConfigureExternalIngamePlayer(ExternalAudioEntry externalAudio)
		{
			ingameBgmPlayer?.Dispose();
			if (externalAudioDataHandle.IsAllocated)
			{
				externalAudioDataHandle.Free();
			}
			externalAudioDataHandle = GCHandle.Alloc(externalAudio.WavData, GCHandleType.Pinned);
			ingameBgmPlayer = new CriAtomExPlayer(0, 0, true);
			ingameBgmPlayer.SetVoicePoolIdentifier(ExternalWaveVoicePoolIdentifier);
			ingameBgmPlayer.SetFormat(CriAtomEx.Format.WAVE);
			ingameBgmPlayer.SetNumChannels(externalAudio.Channels);
			ingameBgmPlayer.SetSamplingRate(externalAudio.SampleRate);
			ingameBgmPlayer.SetData(externalAudioDataHandle.AddrOfPinnedObject(), externalAudio.WavData.Length);
			externalIngamePlayerKey = externalAudio.Key;
			externalIngamePlayerSourceVersion = externalAudio.SourceVersion;
			externalIngamePlayerChannels = externalAudio.Channels;
			externalIngamePlayerSampleRate = externalAudio.SampleRate;
		}

		private static void EnsureAtomInitialized()
		{
			if (criRuntimeInitialized && CriAtomPlugin.IsLibraryInitialized())
			{
				return;
			}

			if (CriAtomPlugin.IsLibraryInitialized())
			{
				criRuntimeInitialized = true;
				return;
			}

			criwareGameObject = new GameObject(CriwareGameObjectName);
			criwareGameObject.SetActive(false);

			CriWareInitializer initializer = criwareGameObject.AddComponent<CriWareInitializer>();
			initializer.dontDestroyOnLoad = true;
			initializer.fileSystemConfig.numberOfLoaders = 64;
			initializer.atomConfig.standardVoicePoolConfig.memoryVoices = 32;
			initializer.atomConfig.standardVoicePoolConfig.streamingVoices = 16;
			initializer.atomConfig.maxVirtualVoices = 64;
			initializer.atomConfig.outputSamplingRate = CriAtomOutputSamplingRate;
			initializer.atomConfig.serverFrequency = 120f;
			initializer.manaConfig.numberOfDecoders = 32;
			initializer.manaConfig.numberOfMaxEntries = 8;

			CriAtomPlugin.SetMaxSamplingRateForStandardVoicePool(CriAtomVoicePoolMaxSamplingRate, CriAtomVoicePoolMaxSamplingRate);

			criwareGameObject.SetActive(true);
			criwareGameObject.AddComponent<CriAtom>();
			criwareGameObject.AddComponent<CriWareErrorHandler>();
			criRuntimeInitialized = true;
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
				try
				{
					byte[] acfData = TryLoadStreamingAssetBytes(path);
					if (acfData == null || acfData.Length == 0)
					{
						continue;
					}

					FixProjectSekaiAcfForOfficialCri(acfData);
					if (acfDataHandle.IsAllocated)
					{
						acfDataHandle.Free();
					}
					acfDataHandle = GCHandle.Alloc(acfData, GCHandleType.Pinned);
					if (CriAtomEx.RegisterAcf(acfDataHandle.AddrOfPinnedObject(), acfData.Length))
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

		private static void FixProjectSekaiAcfForOfficialCri(byte[] acfData)
		{
			// OpenSekai compatibility: ProjectSekai.acf is marked with Target=0. The
			// official CRI Unity plugin expects the Unity target value 6 and otherwise
			// skips DSP table setup with W2010071401.
			ReplaceUtfRootByteValue(acfData, "Target", CriAcfCompatibleTarget);

			// OpenSekai compatibility: the original ACF uses a single space as the default
			// DSP bus setting name. The official CRI Unity plugin tries to auto-attach it
			// during ACF registration and rejects the name, so clear only the registration
			// option. Do not rename DspSetting entries: CRI searches those through an
			// internal sorted table, not by a raw string occurrence.
			ReplaceNullTerminatedValue(acfData, "DefaultDspBusSettingName", (byte)' ', 0);
		}

		private static void ReplaceUtfRootByteValue(byte[] data, string fieldName, byte value)
		{
			if (data == null || data.Length < 32 || data[0] != (byte)'@' || data[1] != (byte)'U' || data[2] != (byte)'T' || data[3] != (byte)'F')
			{
				return;
			}

			int tableSize = ReadBigEndianInt32(data, 4);
			int rowsOffset = ReadBigEndianInt16(data, 10);
			int stringOffset = ReadBigEndianInt32(data, 12);
			int dataOffset = ReadBigEndianInt32(data, 16);
			int columnCount = ReadBigEndianInt16(data, 24);
			int rowSize = ReadBigEndianInt16(data, 26);
			int rowCount = ReadBigEndianInt32(data, 28);
			if (tableSize <= 0
				|| rowsOffset < 0
				|| stringOffset < rowsOffset
				|| dataOffset < stringOffset
				|| columnCount <= 0
				|| rowSize < 0
				|| rowCount <= 0
				|| 8 + tableSize > data.Length)
			{
				return;
			}

			int columnsStart = 32;
			int rowsStart = 8 + rowsOffset;
			int stringsStart = 8 + stringOffset;
			int tableEnd = 8 + tableSize;
			int rowValueOffset = 0;
			int offset = columnsStart;
			for (int i = 0; i < columnCount && offset < tableEnd; i++)
			{
				byte storageType = data[offset++];
				bool hasName = (storageType & 0x10) != 0;
				bool hasConstValue = (storageType & 0x20) != 0;
				bool hasRowValue = (storageType & 0x40) != 0;

				int valueSize = GetUtfStorageValueSize(storageType);
				if (valueSize < 0)
				{
					return;
				}

				string currentFieldName = string.Empty;
				if (hasName)
				{
					if (offset + 4 > tableEnd)
					{
						return;
					}
					int nameOffset = ReadBigEndianInt32(data, offset);
					offset += 4;
					currentFieldName = ReadNullTerminatedAscii(data, stringsStart + nameOffset, tableEnd);
				}

				int valueOffset = -1;
				if (hasConstValue)
				{
					valueOffset = offset;
					offset += valueSize;
				}
				else if (hasRowValue)
				{
					valueOffset = rowsStart + rowValueOffset;
					rowValueOffset += valueSize;
				}

				if (string.Equals(currentFieldName, fieldName, StringComparison.Ordinal)
					&& valueSize == 1
					&& valueOffset >= rowsStart
					&& valueOffset < rowsStart + rowSize
					&& valueOffset < tableEnd)
				{
					data[valueOffset] = value;
					return;
				}
			}
		}

		private static int GetUtfStorageValueSize(byte storageType)
		{
			switch (storageType & 0x0F)
			{
				case 0x00:
				case 0x01:
					return 1;
				case 0x02:
				case 0x03:
					return 2;
				case 0x04:
				case 0x05:
				case 0x08:
				case 0x0A:
					return 4;
				case 0x06:
				case 0x07:
				case 0x09:
					return 8;
				default:
					return -1;
			}
		}

		private static int ReadBigEndianInt32(byte[] data, int offset)
		{
			if (data == null || offset < 0 || offset + 4 > data.Length)
			{
				return 0;
			}
			return (data[offset] << 24) | (data[offset + 1] << 16) | (data[offset + 2] << 8) | data[offset + 3];
		}

		private static int ReadBigEndianInt16(byte[] data, int offset)
		{
			if (data == null || offset < 0 || offset + 2 > data.Length)
			{
				return 0;
			}
			return (data[offset] << 8) | data[offset + 1];
		}

		private static string ReadNullTerminatedAscii(byte[] data, int offset, int endOffset)
		{
			if (data == null || offset < 0 || offset >= data.Length)
			{
				return string.Empty;
			}

			int end = Math.Min(Math.Min(endOffset, data.Length), offset);
			while (end < data.Length && end < endOffset && data[end] != 0)
			{
				end++;
			}
			return System.Text.Encoding.ASCII.GetString(data, offset, end - offset);
		}

		private static void ReplaceNullTerminatedValue(byte[] data, string marker, byte oldValue, byte newValue)
		{
			byte[] markerBytes = System.Text.Encoding.ASCII.GetBytes(marker);
			int markerIndex = IndexOf(data, markerBytes, 0);
			if (markerIndex < 0)
			{
				return;
			}

			int valueIndex = markerIndex + markerBytes.Length;
			if (valueIndex < data.Length && data[valueIndex] == 0)
			{
				valueIndex++;
			}
			while (valueIndex + 1 < data.Length)
			{
				if (data[valueIndex] == oldValue && data[valueIndex + 1] == 0)
				{
					data[valueIndex] = newValue;
					return;
				}
				valueIndex++;
			}
		}

		private static int IndexOf(byte[] data, byte[] pattern, int startIndex)
		{
			if (data == null || pattern == null || pattern.Length == 0 || startIndex < 0 || startIndex >= data.Length)
			{
				return -1;
			}

			for (int i = startIndex; i <= data.Length - pattern.Length; i++)
			{
				bool match = true;
				for (int j = 0; j < pattern.Length; j++)
				{
					if (data[i + j] != pattern[j])
					{
						match = false;
						break;
					}
				}
				if (match)
				{
					return i;
				}
			}

			return -1;
		}

		private static byte[] TryLoadStreamingAssetBytes(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}

			if (IsDirectFilePath(path))
			{
				return File.Exists(path) ? File.ReadAllBytes(path) : null;
			}

			using (UnityWebRequest request = UnityWebRequest.Get(path))
			{
				UnityWebRequestAsyncOperation operation = request.SendWebRequest();
				while (!operation.isDone)
				{
					Thread.Sleep(1);
				}

				if (request.result != UnityWebRequest.Result.Success)
				{
					return null;
				}

				return request.downloadHandler?.data;
			}
		}

		private static bool IsDirectFilePath(string path)
		{
			return !string.IsNullOrEmpty(path)
				&& path.IndexOf("://", StringComparison.Ordinal) < 0
				&& !path.StartsWith("jar:", StringComparison.OrdinalIgnoreCase);
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
			if (IsPlaybackValid(currentIngamePlayback))
			{
				currentIngamePlayback.Pause();
			}
			soundEffectPlayer?.Pause();
		}

		public void Resume()
		{
			if (ingameBgmPlayer != null)
			{
				ingameBgmPlayer.Resume(CriAtomEx.ResumeMode.PausedPlayback);
			}
			soundEffectPlayer?.Resume(CriAtomEx.ResumeMode.PausedPlayback);
		}
	}

	internal sealed class ExternalAudioEntry
	{
		public string Key;
		public byte[] WavData;
		public int Channels;
		public int SampleRate;
		public long LengthMs;
		public long SourceVersion;
	}

	internal sealed class AudioSyncedUnityTimer
	{
		private readonly CriAtomExPlayback playback;
		private long referenceAudioTimeMs = -1L;
		private float referenceUnityTime;
		private bool isWaitingForAudioTimeUpdate;

		public long PlaybackTime { get; private set; } = -1L;

		public long DebugAudioSyncedTime { get; private set; } = -1L;

		public AudioSyncedUnityTimer(CriAtomExPlayback playback)
		{
			this.playback = playback;
		}

		public void Execute(float unityTime)
		{
			long audioTimeMs = playback.GetTimeSyncedWithAudio();
			if (audioTimeMs < 0L)
			{
				audioTimeMs = playback.GetTime();
			}
			DebugAudioSyncedTime = audioTimeMs;
			if (audioTimeMs <= 99L)
			{
				PlaybackTime = audioTimeMs;
				return;
			}

			if (referenceAudioTimeMs < 0L)
			{
				referenceAudioTimeMs = audioTimeMs;
				referenceUnityTime = unityTime;
			}

			if (isWaitingForAudioTimeUpdate)
			{
				long previousPlaybackTime = PlaybackTime;
				referenceAudioTimeMs = audioTimeMs;
				referenceUnityTime = unityTime;
				if (audioTimeMs > previousPlaybackTime)
				{
					PlaybackTime = audioTimeMs;
					isWaitingForAudioTimeUpdate = false;
				}
				return;
			}

			long estimatedTimeMs = referenceAudioTimeMs + (long)((unityTime - referenceUnityTime) * 1000f);
			long delta = estimatedTimeMs - audioTimeMs;
			if (delta >= 63L)
			{
				PlaybackTime = audioTimeMs + 62L;
				isWaitingForAudioTimeUpdate = true;
			}
			else if (-delta >= 63L)
			{
				PlaybackTime = audioTimeMs;
				referenceAudioTimeMs = audioTimeMs;
				referenceUnityTime = unityTime;
			}
			else
			{
				PlaybackTime = estimatedTimeMs;
			}
		}
	}
}
