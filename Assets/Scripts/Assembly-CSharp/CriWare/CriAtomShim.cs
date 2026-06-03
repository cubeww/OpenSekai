using System;
using System.Collections.Generic;
using Stopwatch = System.Diagnostics.Stopwatch;
using System.IO;
using AcbDecoder;
using UnityEngine;
using UnityEngine.Networking;

namespace CriWare
{
	public sealed class CriFsBinder
	{
	}

	public static class Common
	{
		public static string streamingAssetsPath => Application.streamingAssetsPath;
	}

	public static class CriAtomPlugin
	{
		private static bool initialized;

		public static bool IsLibraryInitialized()
		{
			return initialized;
		}

		public static void InitializeLibrary()
		{
			initialized = true;
			CriAtomAudioRuntime.EnsureInitialized();
		}

		public static void FinalizeLibrary()
		{
			initialized = false;
		}
	}

	public static class CriAtomEx
	{
		public struct CueInfo
		{
			public int id;
			public string name;
			public int length;
			public int numTracks;
			public int numRelatedWaveforms;
		}

		public static bool RegisterAcf(CriFsBinder binder, string path)
		{
			// The compatibility layer decodes ACB/HCA directly, so ACF data is not needed.
			return !string.IsNullOrEmpty(path);
		}
	}

	public sealed class CriAtomExAcb
	{
		private readonly AcbFile acbFile;
		private readonly Dictionary<string, List<CueEntry>> entriesByCueName = new Dictionary<string, List<CueEntry>>(StringComparer.Ordinal);
		private readonly Dictionary<string, AudioClip> clipsByCueName = new Dictionary<string, AudioClip>(StringComparer.Ordinal);
		private readonly Dictionary<AcbEntry, AudioClip> clipsByEntry = new Dictionary<AcbEntry, AudioClip>();
		private readonly Dictionary<string, AudioClip> externalClipsByCueName = new Dictionary<string, AudioClip>(StringComparer.Ordinal);

		private CriAtomExAcb(AcbFile acbFile)
		{
			this.acbFile = acbFile;
			isAvailable = acbFile != null;
			if (acbFile == null)
			{
				return;
			}

			Dictionary<int, AcbEntry> entriesByWaveId = new Dictionary<int, AcbEntry>();
			foreach (AcbEntry entry in acbFile.Entries)
			{
				if (!entriesByWaveId.ContainsKey(entry.WaveId))
				{
					entriesByWaveId.Add(entry.WaveId, entry);
				}
			}

			foreach (KeyValuePair<string, IReadOnlyList<AcbCueWaveform>> pair in acbFile.CueWaveforms)
			{
				foreach (AcbCueWaveform waveform in pair.Value)
				{
					if (entriesByWaveId.TryGetValue(waveform.WaveId, out AcbEntry entry))
					{
						RegisterCueName(pair.Key, entry, waveform.VolumeScale);
					}
				}
			}

			foreach (AcbEntry entry in acbFile.Entries)
			{
				RegisterCueNameFallback(entry.Name, entry);
			}
		}

		private CriAtomExAcb(string cueName, AudioClip clip)
		{
			isAvailable = clip != null && !string.IsNullOrEmpty(cueName);
			if (!isAvailable)
			{
				return;
			}

			externalClipsByCueName[cueName] = clip;
			clipsByCueName[cueName] = clip;
		}

		public bool isAvailable { get; private set; }

		public static CriAtomExAcb LoadAcbFile(CriFsBinder binder, string acbPath, string awbPath)
		{
			byte[] bytes = ReadAllBytes(acbPath);
			return LoadAcbData(bytes, null, null);
		}

		public static CriAtomExAcb LoadAcbData(byte[] acbBytes, object awbBinder, object awb)
		{
			if (acbBytes == null || acbBytes.Length == 0)
			{
				return null;
			}

			try
			{
				return new CriAtomExAcb(AcbFile.Load(acbBytes));
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return null;
			}
		}

		public static CriAtomExAcb CreateFromAudioClip(string cueName, AudioClip clip)
		{
			return new CriAtomExAcb(cueName, clip);
		}

		public bool Exists(string cueName)
		{
			return !string.IsNullOrEmpty(cueName)
				&& (entriesByCueName.ContainsKey(cueName) || externalClipsByCueName.ContainsKey(cueName));
		}

		public bool GetCueInfo(string cueName, out CriAtomEx.CueInfo cueInfo)
		{
			cueInfo = default(CriAtomEx.CueInfo);
			if (!string.IsNullOrEmpty(cueName) && externalClipsByCueName.TryGetValue(cueName, out AudioClip clip))
			{
				cueInfo = CreateCueInfo(cueName, clip);
				return true;
			}
			if (string.IsNullOrEmpty(cueName) || !entriesByCueName.TryGetValue(cueName, out List<CueEntry> entries) || entries.Count == 0)
			{
				return false;
			}

			cueInfo = CreateCueInfo(cueName, entries);
			return true;
		}

		public CriAtomEx.CueInfo[] GetCueInfoList()
		{
			CriAtomEx.CueInfo[] cueInfos = new CriAtomEx.CueInfo[entriesByCueName.Count + externalClipsByCueName.Count];
			HashSet<string> addedNames = new HashSet<string>(StringComparer.Ordinal);
			int index = 0;
			foreach (KeyValuePair<string, AudioClip> pair in externalClipsByCueName)
			{
				cueInfos[index++] = CreateCueInfo(pair.Key, pair.Value);
				addedNames.Add(pair.Key);
			}
			foreach (KeyValuePair<string, List<CueEntry>> pair in entriesByCueName)
			{
				if (addedNames.Contains(pair.Key))
				{
					continue;
				}
				cueInfos[index++] = CreateCueInfo(pair.Key, pair.Value);
			}
			if (index != cueInfos.Length)
			{
				Array.Resize(ref cueInfos, index);
			}
			return cueInfos;
		}

		internal AudioClip GetOrCreateAudioClip(string cueName)
		{
			if (!string.IsNullOrEmpty(cueName) && externalClipsByCueName.TryGetValue(cueName, out AudioClip externalClip))
			{
				return externalClip;
			}
			if (string.IsNullOrEmpty(cueName) || !entriesByCueName.TryGetValue(cueName, out List<CueEntry> entries) || entries.Count == 0)
			{
				return null;
			}

			AcbEntry entry = entries[0].Entry;
			if (clipsByCueName.TryGetValue(cueName, out AudioClip cachedClip) && cachedClip != null)
			{
				return cachedClip;
			}

			if (clipsByEntry.TryGetValue(entry, out AudioClip cachedEntryClip) && cachedEntryClip != null)
			{
				clipsByCueName[cueName] = cachedEntryClip;
				return cachedEntryClip;
			}

			AudioClip clip = DecodeEntryToAudioClip(entry, cueName);
			RegisterDecodedClip(entry, clip);
			return clip;
		}

		internal CueAudioClip[] GetOrCreateAudioClips(string cueName)
		{
			if (!string.IsNullOrEmpty(cueName) && externalClipsByCueName.TryGetValue(cueName, out AudioClip externalClip))
			{
				return new[] { new CueAudioClip(externalClip, 1f) };
			}
			if (string.IsNullOrEmpty(cueName) || !entriesByCueName.TryGetValue(cueName, out List<CueEntry> entries) || entries.Count == 0)
			{
				return Array.Empty<CueAudioClip>();
			}

			var clips = new List<CueAudioClip>(entries.Count);
			foreach (CueEntry cueEntry in entries)
			{
				if (cueEntry?.Entry == null)
				{
					continue;
				}

				AudioClip clip = GetOrCreateAudioClip(cueEntry.Entry, cueName);
				if (clip != null)
				{
					clips.Add(new CueAudioClip(clip, cueEntry.VolumeScale));
				}
			}
			return clips.ToArray();
		}

		internal int PreloadAllAudioClips()
		{
			if (acbFile == null)
			{
				return 0;
			}

			int count = 0;
			foreach (AcbEntry entry in acbFile.Entries)
			{
				try
				{
					if (clipsByEntry.ContainsKey(entry))
					{
						continue;
					}

					string clipName = string.IsNullOrEmpty(entry.Name) ? "ACB Audio" : entry.Name;
					AudioClip clip = DecodeEntryToAudioClip(entry, clipName);
					RegisterDecodedClip(entry, clip);
					count++;
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			return count;
		}

		internal int PreloadAudioClips(IEnumerable<string> cueNames)
		{
			if (cueNames == null)
			{
				return 0;
			}

			int count = 0;
			foreach (string cueName in cueNames)
			{
				try
				{
					if (GetOrCreateAudioClips(cueName).Length > 0)
					{
						count++;
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			return count;
		}

		private AudioClip DecodeEntryToAudioClip(AcbEntry entry, string clipName)
		{
			AudioData audioData = acbFile.Decode(entry);
			return audioData.ToAudioClip(clipName, false);
		}

		private AudioClip GetOrCreateAudioClip(AcbEntry entry, string clipName)
		{
			if (entry == null)
			{
				return null;
			}

			if (clipsByEntry.TryGetValue(entry, out AudioClip cachedEntryClip) && cachedEntryClip != null)
			{
				return cachedEntryClip;
			}

			AudioClip clip = DecodeEntryToAudioClip(entry, clipName);
			RegisterDecodedClip(entry, clip);
			return clip;
		}

		private void RegisterDecodedClip(AcbEntry entry, AudioClip clip)
		{
			if (entry == null || clip == null)
			{
				return;
			}

			clipsByEntry[entry] = clip;
			foreach (KeyValuePair<string, List<CueEntry>> pair in entriesByCueName)
			{
				foreach (CueEntry cueEntry in pair.Value)
				{
					if (ReferenceEquals(cueEntry.Entry, entry) && !clipsByCueName.ContainsKey(pair.Key))
					{
						clipsByCueName[pair.Key] = clip;
					}
				}
			}
		}

		private static CriAtomEx.CueInfo CreateCueInfo(string cueName, List<CueEntry> entries)
		{
			AcbEntry entry = entries != null && entries.Count > 0 ? entries[0].Entry : null;
			return new CriAtomEx.CueInfo
			{
				id = entry != null ? entry.WaveId : 0,
				name = cueName,
				length = 0,
				numTracks = entries != null ? entries.Count : 0,
				numRelatedWaveforms = entries != null ? entries.Count : 0
			};
		}

		private static CriAtomEx.CueInfo CreateCueInfo(string cueName, AudioClip clip)
		{
			return new CriAtomEx.CueInfo
			{
				id = 0,
				name = cueName,
				length = clip != null ? (int)(clip.length * 1000f) : 0,
				numTracks = 1,
				numRelatedWaveforms = clip != null ? 1 : 0
			};
		}

		private static void RegisterCueName(Dictionary<string, List<CueEntry>> map, string cueName, AcbEntry entry, float volumeScale)
		{
			if (string.IsNullOrEmpty(cueName) || entry == null)
			{
				return;
			}

			if (!map.TryGetValue(cueName, out List<CueEntry> entries))
			{
				entries = new List<CueEntry>();
				map.Add(cueName, entries);
			}

			entries.Add(new CueEntry(entry, volumeScale));
		}

		private void RegisterCueName(string cueName, AcbEntry entry, float volumeScale)
		{
			RegisterCueName(entriesByCueName, cueName, entry, volumeScale);
		}

		private void RegisterCueNameFallback(string cueNames, AcbEntry entry)
		{
			if (string.IsNullOrEmpty(cueNames))
			{
				return;
			}
			string[] splitNames = cueNames.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string splitName in splitNames)
			{
				string cueName = splitName.Trim();
				if (!entriesByCueName.ContainsKey(cueName))
				{
					RegisterCueName(entriesByCueName, cueName, entry, 1f);
				}
			}
		}

		internal readonly struct CueAudioClip
		{
			public CueAudioClip(AudioClip clip, float volumeScale)
			{
				Clip = clip;
				VolumeScale = volumeScale;
			}

			public AudioClip Clip { get; }

			public float VolumeScale { get; }
		}

		private sealed class CueEntry
		{
			public CueEntry(AcbEntry entry, float volumeScale)
			{
				Entry = entry;
				VolumeScale = Mathf.Max(0f, volumeScale);
			}

			public AcbEntry Entry { get; }

			public float VolumeScale { get; }
		}

		private static byte[] ReadAllBytes(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}

			if (File.Exists(path))
			{
				return File.ReadAllBytes(path);
			}

			if (!path.Contains("://") && !path.Contains("!/"))
			{
				return null;
			}

			using (UnityWebRequest request = UnityWebRequest.Get(path))
			{
				UnityWebRequestAsyncOperation operation = request.SendWebRequest();
				while (!operation.isDone)
				{
				}

				if (request.result != UnityWebRequest.Result.Success)
				{
					Debug.LogWarningFormat("Failed to load ACB file. path:{0} error:{1}", path, request.error);
					return null;
				}

				return request.downloadHandler.data;
			}
		}
	}

	public sealed class CriAtomExPlayer : IDisposable
	{
		private CriAtomExAcb acb;
		private string cueName;
		private float volume = 1f;
		private long startTimeMs;
		private CriAtomExPlayback currentPlayback;

		public void SetCue(CriAtomExAcb acb, string cueName)
		{
			this.acb = acb;
			this.cueName = cueName;
		}

		public void SetVolume(float volume)
		{
			this.volume = Mathf.Max(0f, volume);
		}

		public void SetStartTime(long startTimeMs)
		{
			this.startTimeMs = Math.Max(0L, startTimeMs);
		}

		public CriAtomExPlayback Start()
		{
			if (acb == null || string.IsNullOrEmpty(cueName))
			{
				currentPlayback = CriAtomExPlayback.Invalid;
				return currentPlayback;
			}

			CriAtomExAcb.CueAudioClip[] clips = acb.GetOrCreateAudioClips(cueName);
			currentPlayback = CriAtomAudioRuntime.Play(clips, volume, startTimeMs / 1000f);
			startTimeMs = 0L;
			return currentPlayback;
		}

		public void Stop()
		{
			currentPlayback.Stop();
		}

		public void Pause()
		{
			currentPlayback.Pause();
		}

		public void Resume()
		{
			currentPlayback.Resume();
		}

		public CriAtomExPlayback.Status GetStatus()
		{
			return currentPlayback.GetStatus();
		}

		public void Dispose()
		{
			Stop();
		}
	}

	public struct CriAtomExPlayback
	{
		public enum Status
		{
			Stop = 0,
			Prep = 1,
			Playing = 2,
			PlayEnd = 3,
			Error = 4
		}

		public static readonly CriAtomExPlayback Invalid = new CriAtomExPlayback(0);

		public readonly uint id;

		internal CriAtomExPlayback(uint id)
		{
			this.id = id;
		}

		public Status GetStatus()
		{
			return CriAtomAudioRuntime.GetStatus(id);
		}

		public long GetTime()
		{
			return CriAtomAudioRuntime.GetTimeMs(id);
		}

		public void GetTimeAndScaleSyncedWithAudio(out long playbackTime, out float timeScale)
		{
			CriAtomAudioRuntime.GetTimeAndScaleSyncedWithAudio(id, out playbackTime, out timeScale);
		}

		public void Stop()
		{
			CriAtomAudioRuntime.Stop(id);
		}

		public void Pause()
		{
			CriAtomAudioRuntime.Pause(id);
		}

		public void Resume()
		{
			CriAtomAudioRuntime.Resume(id);
		}
	}

	internal static class CriAtomAudioRuntime
	{
		private sealed class PlaybackState
		{
			public GameObject GameObject;
			public AudioSource PrimarySource;
			public AudioSource[] Sources;
			public bool Stopped;
			public float BaseTimeSeconds;
			public long BaseStopwatchTicks;
			public float PausedTimeSeconds;
			public bool Paused;
		}

		private static readonly Dictionary<uint, PlaybackState> playbacks = new Dictionary<uint, PlaybackState>();
		private static readonly double StopwatchSecondsPerTick = 1d / Stopwatch.Frequency;
		private static GameObject root;
		private static AudioListener fallbackAudioListener;
		private static uint nextPlaybackId = 1;

		public static void EnsureInitialized()
		{
			if (root == null)
			{
				root = new GameObject("CriAtomAudioRuntime");
				UnityEngine.Object.DontDestroyOnLoad(root);
			}

			EnsureAudioListenerGuard();
			CacheFallbackAudioListener();
			if (HasEnabledExternalAudioListener())
			{
				ReleaseFallbackAudioListener();
				return;
			}

			EnsureFallbackAudioListener();
		}

		public static CriAtomExPlayback Play(AudioClip clip, float volume, float startTimeSeconds)
		{
			if (clip == null)
			{
				return CriAtomExPlayback.Invalid;
			}

			return Play(new[] { new CriAtomExAcb.CueAudioClip(clip, 1f) }, volume, startTimeSeconds);
		}

		public static CriAtomExPlayback Play(CriAtomExAcb.CueAudioClip[] clips, float volume, float startTimeSeconds)
		{
			if (clips == null || clips.Length == 0)
			{
				return CriAtomExPlayback.Invalid;
			}

			EnsureInitialized();
			uint id = AllocatePlaybackId();
			GameObject playbackObject = new GameObject("CriAtomExPlayback");
			playbackObject.transform.SetParent(root.transform, false);
			playbackObject.AddComponent<CriAtomPlaybackCleaner>().Id = id;
			List<AudioSource> sources = new List<AudioSource>(clips.Length);
			float primaryStartTimeSeconds = 0f;
			for (int i = 0; i < clips.Length; i++)
			{
				AudioClip clip = clips[i].Clip;
				if (clip == null)
				{
					continue;
				}

				AudioSource source = playbackObject.AddComponent<AudioSource>();
				source.playOnAwake = false;
				source.spatialBlend = 0f;
				source.clip = clip;
				source.volume = Mathf.Max(0f, volume * clips[i].VolumeScale);

				float sourceStartTimeSeconds = 0f;
				if (startTimeSeconds > 0f && startTimeSeconds < clip.length)
				{
					source.time = startTimeSeconds;
					sourceStartTimeSeconds = startTimeSeconds;
				}
				if (sources.Count == 0)
				{
					primaryStartTimeSeconds = sourceStartTimeSeconds;
				}

				sources.Add(source);
			}

			if (sources.Count == 0)
			{
				UnityEngine.Object.Destroy(playbackObject);
				return CriAtomExPlayback.Invalid;
			}

			PlaybackState state = new PlaybackState
			{
				GameObject = playbackObject,
				PrimarySource = sources[0],
				Sources = sources.ToArray(),
				Stopped = false,
				BaseTimeSeconds = primaryStartTimeSeconds,
				BaseStopwatchTicks = Stopwatch.GetTimestamp(),
				PausedTimeSeconds = primaryStartTimeSeconds,
				Paused = false
			};
			playbacks[id] = state;

			EnsureAudioListener();
			state.BaseStopwatchTicks = Stopwatch.GetTimestamp();
			foreach (AudioSource source in sources)
			{
				source.Play();
			}
			return new CriAtomExPlayback(id);
		}

		public static CriAtomExPlayback.Status GetStatus(uint id)
		{
			if (id == 0 || !playbacks.TryGetValue(id, out PlaybackState state))
			{
				return CriAtomExPlayback.Status.Stop;
			}

			if (state.Stopped)
			{
				playbacks.Remove(id);
				return CriAtomExPlayback.Status.Stop;
			}

			if (state.PrimarySource == null || state.GameObject == null)
			{
				playbacks.Remove(id);
				return CriAtomExPlayback.Status.PlayEnd;
			}

			if (state.Paused)
			{
				return CriAtomExPlayback.Status.Playing;
			}

			if (state.Sources != null)
			{
				for (int i = 0; i < state.Sources.Length; i++)
				{
					AudioSource source = state.Sources[i];
					if (source != null && source.isPlaying)
					{
						return CriAtomExPlayback.Status.Playing;
					}
				}
			}

			return CriAtomExPlayback.Status.PlayEnd;
		}

		public static long GetTimeMs(uint id)
		{
			if (id == 0 || !playbacks.TryGetValue(id, out PlaybackState state) || state.PrimarySource == null)
			{
				return 0L;
			}

			return (long)(GetMonotonicPlaybackTimeSeconds(state) * 1000f);
		}

		public static void GetTimeAndScaleSyncedWithAudio(uint id, out long playbackTime, out float timeScale)
		{
			playbackTime = -1L;
			timeScale = 1f;
			if (id == 0 || !playbacks.TryGetValue(id, out PlaybackState state) || state.PrimarySource == null)
			{
				return;
			}

			playbackTime = (long)(GetMonotonicPlaybackTimeSeconds(state) * 1000f);
			timeScale = state.PrimarySource.pitch;
			if (timeScale <= 0f)
			{
				timeScale = 1f;
			}
		}

		public static void Stop(uint id)
		{
			if (id == 0 || !playbacks.TryGetValue(id, out PlaybackState state))
			{
				return;
			}

			state.Stopped = true;
			if (state.Sources != null)
			{
				for (int i = 0; i < state.Sources.Length; i++)
				{
					AudioSource source = state.Sources[i];
					if (source != null)
					{
						source.Stop();
					}
				}
			}
			if (state.GameObject != null)
			{
				UnityEngine.Object.Destroy(state.GameObject);
			}
			playbacks.Remove(id);
		}

		public static void Pause(uint id)
		{
			if (id != 0 && playbacks.TryGetValue(id, out PlaybackState state) && state.PrimarySource != null)
			{
				if (!state.Paused)
				{
					state.PausedTimeSeconds = GetMonotonicPlaybackTimeSeconds(state);
					state.Paused = true;
				}
				if (state.Sources != null)
				{
					for (int i = 0; i < state.Sources.Length; i++)
					{
						AudioSource source = state.Sources[i];
						if (source != null)
						{
							source.Pause();
						}
					}
				}
			}
		}

		public static void Resume(uint id)
		{
			if (id != 0 && playbacks.TryGetValue(id, out PlaybackState state) && state.PrimarySource != null)
			{
				if (state.Paused)
				{
					state.BaseTimeSeconds = state.PausedTimeSeconds;
					state.BaseStopwatchTicks = Stopwatch.GetTimestamp();
					state.Paused = false;
				}
				if (state.Sources != null)
				{
					for (int i = 0; i < state.Sources.Length; i++)
					{
						AudioSource source = state.Sources[i];
						if (source != null)
						{
							source.UnPause();
						}
					}
				}
			}
		}

		// OpenSekai: AudioSource.time and dspTime can advance in large chunks on some Android devices.
		// Use a process monotonic clock for visual sync while AudioSource still owns actual audio output.
		private static float GetMonotonicPlaybackTimeSeconds(PlaybackState state)
		{
			if (state == null || state.PrimarySource == null)
			{
				return 0f;
			}
			if (state.Paused)
			{
				return state.PausedTimeSeconds;
			}

			float pitch = state.PrimarySource.pitch;
			if (pitch <= 0f)
			{
				pitch = 1f;
			}

			double elapsedSeconds = (Stopwatch.GetTimestamp() - state.BaseStopwatchTicks) * StopwatchSecondsPerTick * pitch;
			float playbackTimeSeconds = state.BaseTimeSeconds + (float)Math.Max(0d, elapsedSeconds);
			AudioClip clip = state.PrimarySource.clip;
			if (clip != null && clip.length > 0f)
			{
				playbackTimeSeconds = Mathf.Min(playbackTimeSeconds, clip.length);
			}

			return Mathf.Max(0f, playbackTimeSeconds);
		}

		private static uint AllocatePlaybackId()
		{
			if (nextPlaybackId == 0)
			{
				nextPlaybackId = 1;
			}
			return nextPlaybackId++;
		}

		private static void EnsureAudioListener()
		{
			EnsureInitialized();
			if (HasEnabledExternalAudioListener())
			{
				ReleaseFallbackAudioListener();
				return;
			}

			EnsureFallbackAudioListener();
		}

		private static void EnsureFallbackAudioListener()
		{
			CacheFallbackAudioListener();

			if (fallbackAudioListener == null && root != null)
			{
				fallbackAudioListener = root.AddComponent<AudioListener>();
			}

			if (fallbackAudioListener != null)
			{
				fallbackAudioListener.enabled = true;
			}
			EnsureAudioListenerGuard();
		}

		private static void CacheFallbackAudioListener()
		{
			if (fallbackAudioListener == null && root != null)
			{
				fallbackAudioListener = root.GetComponent<AudioListener>();
			}
		}

		private static void EnsureAudioListenerGuard()
		{
			if (root != null && root.GetComponent<CriAtomAudioListenerGuard>() == null)
			{
				root.AddComponent<CriAtomAudioListenerGuard>();
			}
		}

		internal static void ReleaseFallbackAudioListenerIfExternalExists()
		{
			if (HasEnabledExternalAudioListener())
			{
				ReleaseFallbackAudioListener();
				return;
			}

			EnsureAudioListener();
		}

		private static bool HasEnabledExternalAudioListener()
		{
			AudioListener[] listeners = UnityEngine.Object.FindObjectsOfType<AudioListener>();
			for (int i = 0; i < listeners.Length; i++)
			{
				AudioListener listener = listeners[i];
				if (listener != null && listener != fallbackAudioListener && listener.enabled && listener.gameObject.activeInHierarchy)
				{
					return true;
				}
			}

			return false;
		}

		private static void ReleaseFallbackAudioListener()
		{
			if (fallbackAudioListener == null && root != null)
			{
				fallbackAudioListener = root.GetComponent<AudioListener>();
			}

			if (fallbackAudioListener != null)
			{
				UnityEngine.Object.Destroy(fallbackAudioListener);
				fallbackAudioListener = null;
			}
		}

		internal static void Forget(uint id)
		{
			if (id != 0)
			{
				playbacks.Remove(id);
			}
		}

		internal static void DestroyIfPlaybackEnded(uint id)
		{
			if (id == 0 || !playbacks.TryGetValue(id, out PlaybackState state))
			{
				return;
			}
			if (state.Stopped || state.Paused || state.GameObject == null)
			{
				return;
			}

			if (state.Sources != null)
			{
				for (int i = 0; i < state.Sources.Length; i++)
				{
					AudioSource source = state.Sources[i];
					if (source != null && source.isPlaying)
					{
						return;
					}
				}
			}

			UnityEngine.Object.Destroy(state.GameObject);
		}
	}

	internal sealed class CriAtomPlaybackCleaner : MonoBehaviour
	{
		public uint Id { get; set; }

		private void Update()
		{
			CriAtomAudioRuntime.DestroyIfPlaybackEnded(Id);
		}

		private void OnDestroy()
		{
			CriAtomAudioRuntime.Forget(Id);
		}
	}

	internal sealed class CriAtomAudioListenerGuard : MonoBehaviour
	{
		private void LateUpdate()
		{
			CriAtomAudioRuntime.ReleaseFallbackAudioListenerIfExternalExists();
		}
	}
}
