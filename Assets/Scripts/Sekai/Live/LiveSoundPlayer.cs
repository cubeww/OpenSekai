using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai.Live
{
	public class LiveSoundPlayer : MonoBehaviour
	{
		[Serializable]
		public class CueClip
		{
			public string cueName;
			public AudioClip clip;
			public AudioClip[] clips;
			[Range(0f, 2f)] public float volume = 1f;

			private int nextClipIndex;

			public AudioClip GetClip()
			{
				if (clips != null && clips.Length > 0)
				{
					for (int i = 0; i < clips.Length; i++)
					{
						int index = (nextClipIndex + i) % clips.Length;
						if (clips[index] == null)
						{
							continue;
						}

						nextClipIndex = (index + 1) % clips.Length;
						return clips[index];
					}
				}

				return clip;
			}

			public bool HasClip()
			{
				if (clip != null)
				{
					return true;
				}

				for (int i = 0; clips != null && i < clips.Length; i++)
				{
					if (clips[i] != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		[SerializeField] private AudioSource oneShotSource;
		[SerializeField] private CueClip[] cueClips;
		[SerializeField] private float masterVolume = 1f;
		[SerializeField] private bool logMissingCue;

		private readonly Dictionary<string, CueClip> cueClipDict = new Dictionary<string, CueClip>();
		private readonly Dictionary<uint, AudioSource> playingSources = new Dictionary<uint, AudioSource>();
		private uint nextPlaybackId = 1;

		private void Reset()
		{
			if (cueClips != null && cueClips.Length > 0)
			{
				return;
			}

			cueClips = new[]
			{
				CreateCueClip(LiveSoundDefine.SE_LIVE_TAP),
				CreateCueClip(LiveSoundDefine.SE_LIVE_GOOD),
				CreateCueClip(LiveSoundDefine.SE_LIVE_GREAT),
				CreateCueClip(LiveSoundDefine.SE_LIVE_PERFECT),
				CreateCueClip(LiveSoundDefine.SE_LIVE_CRITICAL),
				CreateCueClip(LiveSoundDefine.SE_LIVE_LONG),
				CreateCueClip(LiveSoundDefine.SE_LIVE_LONG_CRITICAL),
				CreateCueClip(LiveSoundDefine.SE_LIVE_CONNECT),
				CreateCueClip(LiveSoundDefine.SE_LIVE_CONNECT_CRITICAL),
				CreateCueClip(LiveSoundDefine.SE_LIVE_FLICK),
				CreateCueClip(LiveSoundDefine.SE_LIVE_FLICK_CRITICAL),
				CreateCueClip(LiveSoundDefine.SE_LIVE_TRACE),
				CreateCueClip(LiveSoundDefine.SE_LIVE_TRACE_CRITICAL)
			};
		}

		private void Awake()
		{
			Setup();
		}

		public void Setup()
		{
			EnsureOneShotSource();
			cueClipDict.Clear();
			if (cueClips == null)
			{
				return;
			}

			for (int i = 0; i < cueClips.Length; i++)
			{
				CueClip cueClip = cueClips[i];
				if (cueClip == null || string.IsNullOrEmpty(cueClip.cueName) || !cueClip.HasClip())
				{
					continue;
				}

				cueClipDict[cueClip.cueName] = cueClip;
			}
		}

		public void PlayIngameSEOneShot(string cueName, float seVolume = 1f)
		{
			if (!TryGetCueClip(cueName, out CueClip cueClip))
			{
				return;
			}

			EnsureOneShotSource();
			AudioClip clip = cueClip.GetClip();
			if (clip != null)
			{
				oneShotSource.PlayOneShot(clip, GetVolume(cueClip, seVolume));
			}
		}

		public uint PlayIngameSE(string cueName, float seVolume = 1f)
		{
			if (!TryGetCueClip(cueName, out CueClip cueClip))
			{
				return 0;
			}

			AudioSource source = CreateLoopSource(cueName, cueClip, seVolume);
			uint playbackId = nextPlaybackId++;
			if (nextPlaybackId == 0)
			{
				nextPlaybackId = 1;
			}

			playingSources[playbackId] = source;
			source.Play();
			return playbackId;
		}

		public void StopSE(uint playbackId)
		{
			if (playbackId == 0 || !playingSources.TryGetValue(playbackId, out AudioSource source))
			{
				return;
			}

			playingSources.Remove(playbackId);
			if (source == null)
			{
				return;
			}

			source.Stop();
			Destroy(source.gameObject);
		}

		public void StopAllSE()
		{
			foreach (KeyValuePair<uint, AudioSource> pair in playingSources)
			{
				if (pair.Value != null)
				{
					pair.Value.Stop();
					Destroy(pair.Value.gameObject);
				}
			}

			playingSources.Clear();
		}

		private bool TryGetCueClip(string cueName, out CueClip cueClip)
		{
			if (cueClipDict.Count == 0 && cueClips != null && cueClips.Length > 0)
			{
				Setup();
			}

			if (!string.IsNullOrEmpty(cueName) && cueClipDict.TryGetValue(cueName, out cueClip) && cueClip.HasClip())
			{
				return true;
			}

			if (logMissingCue && !string.IsNullOrEmpty(cueName))
			{
				Debug.LogWarning("LiveSoundPlayer cue is not assigned: " + cueName, this);
			}
			cueClip = null;
			return false;
		}

		private static CueClip CreateCueClip(string cueName)
		{
			return new CueClip
			{
				cueName = cueName,
				volume = 1f
			};
		}

		private AudioSource CreateLoopSource(string cueName, CueClip cueClip, float seVolume)
		{
			GameObject sourceObject = new GameObject("LiveSE_" + cueName);
			sourceObject.transform.SetParent(transform, false);

			AudioSource source = sourceObject.AddComponent<AudioSource>();
			source.clip = cueClip.GetClip();
			source.loop = true;
			source.playOnAwake = false;
			source.spatialBlend = 0f;
			source.volume = GetVolume(cueClip, seVolume);
			return source;
		}

		private float GetVolume(CueClip cueClip, float seVolume)
		{
			return Mathf.Clamp01(masterVolume * seVolume * (cueClip != null ? cueClip.volume : 1f));
		}

		private void EnsureOneShotSource()
		{
			if (oneShotSource != null)
			{
				return;
			}

			oneShotSource = GetComponent<AudioSource>();
			if (oneShotSource == null)
			{
				oneShotSource = gameObject.AddComponent<AudioSource>();
			}
			oneShotSource.playOnAwake = false;
			oneShotSource.loop = false;
			oneShotSource.spatialBlend = 0f;
		}

		private void OnDestroy()
		{
			StopAllSE();
		}
	}
}
