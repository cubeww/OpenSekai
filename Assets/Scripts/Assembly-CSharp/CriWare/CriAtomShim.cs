using System;
using System.Collections.Generic;
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
		private readonly Dictionary<string, AcbEntry> entriesByCueName = new Dictionary<string, AcbEntry>(StringComparer.Ordinal);
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

			foreach (AcbEntry entry in acbFile.Entries)
			{
				RegisterCueName(entry.Name, entry);
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
			if (string.IsNullOrEmpty(cueName) || !entriesByCueName.TryGetValue(cueName, out AcbEntry entry))
			{
				return false;
			}

			cueInfo = CreateCueInfo(cueName, entry);
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
			foreach (KeyValuePair<string, AcbEntry> pair in entriesByCueName)
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
			if (string.IsNullOrEmpty(cueName) || !entriesByCueName.TryGetValue(cueName, out AcbEntry entry))
			{
				return null;
			}

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
					if (GetOrCreateAudioClip(cueName) != null)
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

		private void RegisterDecodedClip(AcbEntry entry, AudioClip clip)
		{
			if (entry == null || clip == null)
			{
				return;
			}

			clipsByEntry[entry] = clip;
			foreach (KeyValuePair<string, AcbEntry> pair in entriesByCueName)
			{
				if (ReferenceEquals(pair.Value, entry))
				{
					clipsByCueName[pair.Key] = clip;
				}
			}
		}

		private static CriAtomEx.CueInfo CreateCueInfo(string cueName, AcbEntry entry)
		{
			return new CriAtomEx.CueInfo
			{
				id = entry.WaveId,
				name = cueName,
				length = 0,
				numTracks = 1,
				numRelatedWaveforms = 1
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

		private static void RegisterCueName(Dictionary<string, AcbEntry> map, string cueName, AcbEntry entry)
		{
			if (!string.IsNullOrEmpty(cueName) && !map.ContainsKey(cueName))
			{
				map.Add(cueName, entry);
			}
		}

		private void RegisterCueName(string cueNames, AcbEntry entry)
		{
			RegisterCueName(entriesByCueName, cueNames, entry);
			if (string.IsNullOrEmpty(cueNames))
			{
				return;
			}

			string[] splitNames = cueNames.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string splitName in splitNames)
			{
				RegisterCueName(entriesByCueName, splitName.Trim(), entry);
			}
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

			AudioClip clip = acb.GetOrCreateAudioClip(cueName);
			currentPlayback = CriAtomAudioRuntime.Play(clip, volume, startTimeMs / 1000f);
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
			public AudioSource Source;
			public bool Stopped;
		}

		private static readonly Dictionary<uint, PlaybackState> playbacks = new Dictionary<uint, PlaybackState>();
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

			EnsureInitialized();
			uint id = AllocatePlaybackId();
			GameObject playbackObject = new GameObject("CriAtomExPlayback");
			playbackObject.transform.SetParent(root.transform, false);
			playbackObject.AddComponent<CriAtomPlaybackCleaner>().Id = id;
			AudioSource source = playbackObject.AddComponent<AudioSource>();
			source.playOnAwake = false;
			source.spatialBlend = 0f;
			source.clip = clip;
			source.volume = Mathf.Max(0f, volume);
			if (startTimeSeconds > 0f && startTimeSeconds < clip.length)
			{
				source.time = startTimeSeconds;
			}

			playbacks[id] = new PlaybackState
			{
				GameObject = playbackObject,
				Source = source,
				Stopped = false
			};

			EnsureAudioListener();
			source.Play();
			float destroyDelay = Math.Max(0.1f, clip.length - source.time + 0.1f);
			UnityEngine.Object.Destroy(playbackObject, destroyDelay);
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

			if (state.Source == null || state.GameObject == null)
			{
				playbacks.Remove(id);
				return CriAtomExPlayback.Status.PlayEnd;
			}

			return state.Source.isPlaying ? CriAtomExPlayback.Status.Playing : CriAtomExPlayback.Status.PlayEnd;
		}

		public static long GetTimeMs(uint id)
		{
			if (id == 0 || !playbacks.TryGetValue(id, out PlaybackState state) || state.Source == null)
			{
				return 0L;
			}

			return (long)(state.Source.time * 1000f);
		}

		public static void Stop(uint id)
		{
			if (id == 0 || !playbacks.TryGetValue(id, out PlaybackState state))
			{
				return;
			}

			state.Stopped = true;
			if (state.Source != null)
			{
				state.Source.Stop();
			}
			if (state.GameObject != null)
			{
				UnityEngine.Object.Destroy(state.GameObject);
			}
			playbacks.Remove(id);
		}

		public static void Pause(uint id)
		{
			if (id != 0 && playbacks.TryGetValue(id, out PlaybackState state) && state.Source != null)
			{
				state.Source.Pause();
			}
		}

		public static void Resume(uint id)
		{
			if (id != 0 && playbacks.TryGetValue(id, out PlaybackState state) && state.Source != null)
			{
				state.Source.UnPause();
			}
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
	}

	internal sealed class CriAtomPlaybackCleaner : MonoBehaviour
	{
		public uint Id { get; set; }

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
