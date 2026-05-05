using System.Collections.Generic;
using Sekai.Core.Live;
using Sekai.Live;
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

			public EffectPair(GameObject genPrefab, GameObject auraPrefab, Transform parent, BaseLiveController liveController)
			{
				Gen = CreateEffect(genPrefab, parent, liveController);
				Aura = CreateEffect(auraPrefab, parent, liveController);
				Stop();
			}

			public void Play(INote note)
			{
				IsUsed = true;
				SetTransform(note);
				if (Aura != null && !Aura.IsPlaying)
				{
					Aura.Play();
				}
				if (Gen != null && !Gen.IsPlaying)
				{
					Gen.Play();
				}
			}

			public void SetTransform(INote note)
			{
				Vector3 position = GetNoteCenterPosition(note);
				if (Aura != null)
				{
					Aura.transform.localPosition = position;
					float width = Mathf.Max(1f, note.LaneEndF + 1f - note.LaneStartF);
					Aura.transform.localScale = new Vector3(width, 1f, 1f);
				}
				if (Gen != null)
				{
					Gen.transform.localPosition = position;
				}
			}

			public void Stop()
			{
				IsUsed = false;
				Aura?.Stop();
				Gen?.Stop();
			}

			private static ParticleSystemController CreateEffect(GameObject prefab, Transform parent, BaseLiveController liveController)
			{
				if (prefab == null)
				{
					return null;
				}

				GameObject instance = Object.Instantiate(prefab, parent, false);
				instance.name = prefab.name;
				ParticleSystemController controller = instance.AddComponent<ParticleSystemController>();
				controller.RegisterToLiveController(liveController);
				return controller;
			}
		}

		[SerializeField] private GameObject holdAuraPrefab;

		[SerializeField] private GameObject holdGenPrefab;

		[SerializeField] private GameObject criticalHoldAuraPrefab;

		[SerializeField] private GameObject criticalHoldGenPrefab;

		[SerializeField] private int poolCount = 16;

		private EffectPair[] tapKeeps;

		private EffectPair[] criticalTapKeeps;

		private Dictionary<INote, EffectPair> longNotes;

		public void Setup(GameObject holdAuraPrefab = null, GameObject holdGenPrefab = null, GameObject criticalHoldAuraPrefab = null, GameObject criticalHoldGenPrefab = null, int poolCount = 16, BaseLiveController liveController = null)
		{
			if (holdAuraPrefab != null)
			{
				this.holdAuraPrefab = holdAuraPrefab;
			}
			if (holdGenPrefab != null)
			{
				this.holdGenPrefab = holdGenPrefab;
			}
			if (criticalHoldAuraPrefab != null)
			{
				this.criticalHoldAuraPrefab = criticalHoldAuraPrefab;
			}
			if (criticalHoldGenPrefab != null)
			{
				this.criticalHoldGenPrefab = criticalHoldGenPrefab;
			}
			this.poolCount = Mathf.Max(1, poolCount);

			longNotes = new Dictionary<INote, EffectPair>();
			tapKeeps = CreatePairs("LongTapKeep", this.holdGenPrefab, this.holdAuraPrefab, this.poolCount, liveController);
			criticalTapKeeps = CreatePairs("CriticalLongTapKeep", this.criticalHoldGenPrefab, this.criticalHoldAuraPrefab, this.poolCount, liveController);
		}

		public void Add(INote note)
		{
			if (note == null || longNotes == null || longNotes.ContainsKey(note))
			{
				return;
			}

			EffectPair pair = GetFreePair(note.Type);
			if (pair == null)
			{
				return;
			}

			pair.IsUsed = true;
			longNotes[note] = pair;
		}

		public void Remove(INote note)
		{
			if (note == null || longNotes == null)
			{
				return;
			}

			if (longNotes.TryGetValue(note, out EffectPair pair))
			{
				pair.Stop();
				longNotes.Remove(note);
			}
		}

		public void Clear()
		{
			if (longNotes != null)
			{
				foreach (EffectPair pair in longNotes.Values)
				{
					pair?.Stop();
				}
				longNotes.Clear();
			}

			StopPairs(tapKeeps);
			StopPairs(criticalTapKeeps);
		}

		public void Excute(float time)
		{
			if (longNotes == null || longNotes.Count == 0)
			{
				return;
			}

			List<INote> removeList = null;
			foreach (KeyValuePair<INote, EffectPair> pair in longNotes)
			{
				INote note = pair.Key;
				EffectPair effect = pair.Value;
				if (note == null || effect == null || note.State == NoteState.Done || note.State == NoteState.Release || note.State == NoteState.Last)
				{
					if (removeList == null)
					{
						removeList = new List<INote>();
					}
					removeList.Add(note);
					continue;
				}

				if (note.State == NoteState.InputBegan || note.State == NoteState.Input)
				{
					effect.Play(note);
				}
				else
				{
					effect.SetTransform(note);
				}
			}

			for (int i = 0; removeList != null && i < removeList.Count; i++)
			{
				Remove(removeList[i]);
			}
		}

		private EffectPair[] CreatePairs(string rootName, GameObject genPrefab, GameObject auraPrefab, int count, BaseLiveController liveController)
		{
			Transform root = CreateChild(rootName);
			EffectPair[] pairs = new EffectPair[count];
			for (int i = 0; i < pairs.Length; i++)
			{
				pairs[i] = new EffectPair(genPrefab, auraPrefab, root, liveController);
			}
			return pairs;
		}

		private EffectPair GetFreePair(NoteType type)
		{
			EffectPair[] pairs = type == NoteType.Critical ? criticalTapKeeps : tapKeeps;
			for (int i = 0; pairs != null && i < pairs.Length; i++)
			{
				if (pairs[i] != null && !pairs[i].IsUsed)
				{
					return pairs[i];
				}
			}
			return null;
		}

		private Transform CreateChild(string objectName)
		{
			Transform child = transform.Find(objectName);
			if (child != null)
			{
				return child;
			}

			GameObject gameObject = new GameObject(objectName);
			child = gameObject.transform;
			child.SetParent(transform, false);
			return child;
		}

		private static Vector3 GetNoteCenterPosition(INote note)
		{
			float lane = (note.LaneStartF + note.LaneEndF) * 0.5f;
			return new Vector3(LiveUtility.CalcLaneTransformX(lane), 0f, 0f);
		}

		private static void StopPairs(EffectPair[] pairs)
		{
			for (int i = 0; pairs != null && i < pairs.Length; i++)
			{
				pairs[i]?.Stop();
			}
		}
	}
}
