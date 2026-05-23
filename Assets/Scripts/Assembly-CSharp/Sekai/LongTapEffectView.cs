using Sekai.Live;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
	public class LongTapEffectView : MonoBehaviour
	{
		public class EffectPair
		{
			public ParticleSystemController Aura;

			public ParticleSystemController Gen;

			public uint PlaybackId;

			public bool IsUsed;

			public EffectPair(string bundleName, string genPath, string auraPath, Transform parent)
			{
				Gen = LoadTapEffect(bundleName, genPath, parent);
				Aura = LoadTapEffect(bundleName, auraPath, parent);
			}

			private ParticleSystemController LoadTapEffect(string bundleName, string name, Transform parent)
			{
				GameObject prefab = AssetBundleUtility.LoadAsset<GameObject>(bundleName, name);
				if (prefab == null)
				{
					return null;
				}

				GameObject instance = Object.Instantiate(prefab, parent);
				return instance.AddComponent<ParticleSystemController>();
			}
		}

		private EffectPair[] tapKeeps;

		private EffectPair[] criticalTapKeeps;

		private Dictionary<INote, EffectPair> longNotes;

		public void Setup()
		{
			tapKeeps = new EffectPair[10];
			criticalTapKeeps = new EffectPair[10];
			for (int i = 0; i < tapKeeps.Length; i++)
			{
				tapKeeps[i] = new EffectPair(LiveConfig.EffectBundleName, LivePrefabDefine.FX_LONG_HOLD_GEN, LivePrefabDefine.FX_LONG_HOLD_AURA, transform);
				criticalTapKeeps[i] = new EffectPair(LiveConfig.EffectBundleName, LivePrefabDefine.FX_CRITICAL_LONG_HOLD_GEN, LivePrefabDefine.FX_CRITICAL_LONG_HOLD_AURA, transform);
			}

			Warmup(tapKeeps);
			Warmup(criticalTapKeeps);
		}

		public void Add(INote note)
		{
			if (note == null || note.IsSkip || longNotes.ContainsKey(note))
			{
				return;
			}

			EffectPair[] pairs = note.Type == NoteType.Critical ? criticalTapKeeps : tapKeeps;
			if (pairs == null)
			{
				return;
			}

			for (int i = 0; i < pairs.Length; i++)
			{
				EffectPair pair = pairs[i];
				if (pair == null || pair.IsUsed)
				{
					continue;
				}

				longNotes.Add(note, pair);
				pair.IsUsed = true;
				return;
			}
		}

		public void Remove(INote note)
		{
			if (note == null || !longNotes.TryGetValue(note, out EffectPair pair))
			{
				return;
			}

			longNotes.Remove(note);
			pair.Gen?.Stop();
			pair.Aura?.Stop();
			pair.IsUsed = false;
			SoundManager.Instance.StopSE(pair.PlaybackId);
			pair.PlaybackId = 0;
		}

		public void Clear()
		{
			foreach (EffectPair pair in longNotes.Values)
			{
				if (pair == null)
				{
					continue;
				}

				pair.Gen?.Stop();
				pair.Aura?.Stop();
				pair.IsUsed = false;
				SoundManager.Instance.StopSE(pair.PlaybackId);
				pair.PlaybackId = 0;
			}

			longNotes.Clear();
		}

		public void Excute(float time)
		{
			foreach (var entry in longNotes)
			{
				INote note = entry.Key;
				EffectPair pair = entry.Value;
				if (note == null || pair == null)
				{
					continue;
				}

				if (note.State == NoteState.InputBegan || note.State == NoteState.Input)
				{
					float x = LiveUtility.CalcLaneTransformX((note.LaneStartF + note.LaneEndF) * 0.5f);
					float width = note.LaneEndF + 1f - note.LaneStartF;
					if (pair.Gen != null)
					{
						pair.Gen.transform.localPosition = new Vector3(x, 0f, 0f);
					}

					if (pair.Aura != null)
					{
						pair.Aura.transform.localPosition = new Vector3(x, 0f, 0f);
						pair.Aura.transform.localScale = new Vector3(width, 1f, 1f);
					}

					if (pair.Gen != null && pair.Aura != null && (!pair.Gen.IsPlaying || !pair.Aura.IsPlaying))
					{
						pair.Gen.Play();
						pair.Aura.Play();
						string cueName = note.Type == NoteType.Critical ? LiveSoundDefine.SE_LIVE_LONG_CRITICAL : LiveSoundDefine.SE_LIVE_LONG;
						pair.PlaybackId = SoundManager.Instance.PlayIngameSE(cueName);
					}
				}

				if (note.State == NoteState.Release)
				{
					pair.Gen?.Stop();
					pair.Aura?.Stop();
					SoundManager.Instance.StopSE(pair.PlaybackId);
					pair.PlaybackId = 0;
				}
			}
		}

		public LongTapEffectView()
		{
			longNotes = new Dictionary<INote, EffectPair>();
		}

		private static void Warmup(EffectPair[] pairs)
		{
			if (pairs == null || pairs.Length == 0 || pairs[0] == null)
			{
				return;
			}

			pairs[0].Aura?.Play();
			pairs[0].Gen?.Play();
			pairs[0].Aura?.Stop();
			pairs[0].Gen?.Stop();
		}
	}
}
