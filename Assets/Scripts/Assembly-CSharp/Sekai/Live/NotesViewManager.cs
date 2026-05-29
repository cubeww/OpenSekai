using System.Collections.Generic;
using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.Live
{
	public class NotesViewManager
	{
		private static readonly int DefaultNoteDictCapacity;

		private Dictionary<INote, BaseNoteView> spawnNoteDict;

		private Transform noteRoot;

		private Dictionary<NoteCategory, Dictionary<NoteType, NotePool>> notePoolDict;

		private NoteLineView noteLineView;

		private NoteLineView guideLineView;

		private PairNoteLineView pairNoteLineView;

		private bool isEnablePairNotesLine;

		public void Setup(Transform liveRoot)
		{
			spawnNoteDict = new Dictionary<INote, BaseNoteView>(DefaultNoteDictCapacity);
			GameObject root = new GameObject("NoteRoot");
			noteRoot = root.transform;
			noteRoot.SetParent(liveRoot, false);
		}

		public void CreateNotePool(BaseLiveController baseController, Dictionary<(NoteCategory, NoteType), int> noteCountDictionary)
		{
			if (baseController == null || notePoolDict != null)
			{
				return;
			}

			float noteShowRate = 1f;
			if (baseController.Settings != null && !baseController.Settings.NoteShowRate.Equals(1f))
			{
				noteShowRate = LiveUtility.CalcNoteShowRate(baseController.Settings.NoteShowRate);
			}

			isEnablePairNotesLine = baseController.Settings?.UseSimultaneousPushingLine ?? true;
			notePoolDict = new Dictionary<NoteCategory, Dictionary<NoteType, NotePool>>(7)
			{
				[NoteCategory.Normal] = CreateTypedPools(noteCountDictionary, NoteCategory.Normal, "NormalNotePool", "NormalNote", "NormalCriticalNotePool", "NormalCrtNote", noteShowRate),
				[NoteCategory.Long] = CreateTypedPools(noteCountDictionary, NoteCategory.Long, "LongNotePool", "LongNote", "LongCriticalPool", "LongCrtNote", noteShowRate),
				[NoteCategory.Connection] = CreateTypedPools(noteCountDictionary, NoteCategory.Connection, "ConnectionNotePool", "ConnectionNote", "ConnectionCriticalPool", "ConnectionCrtNote", noteShowRate),
				[NoteCategory.Flick] = CreateTypedPools(noteCountDictionary, NoteCategory.Flick, "FlickNotePool", "FlickNote", "FlickCriticalPool", "FlickCrtNote", noteShowRate),
				[NoteCategory.Friction] = CreateTypedPools(noteCountDictionary, NoteCategory.Friction, "FrictionNotePool", "FrictionNote", "FrictionCriticalNotePool", "FrictionCrtNote", noteShowRate),
				[NoteCategory.FrictionLong] = CreateTypedPools(noteCountDictionary, NoteCategory.FrictionLong, "FrictionLongNotePool", "FrictionLongNote", "FrictionLongCriticalNotePool", "FrictionLongCrtNote", noteShowRate),
				[NoteCategory.FrictionFlick] = CreateTypedPools(noteCountDictionary, NoteCategory.FrictionFlick, "FrictionFlickNotePool", "FrictionFlickNote", "FrictionFlickCriticalNotePool", "FrictionFlickCrtNote", noteShowRate)
			};

			bool isCustomMusicScore = baseController.BootData?.IsCustomMusicScore ?? false;
			LiveBundleBuildData bundleBuildData = baseController.BootData?.BundleBuildData;
			noteLineView = CreateLineView<NoteLineView>("NoteLineView");
			noteLineView.Setup(bundleBuildData, "longNoteLine.png", LiveConfig.LongNoteAlpha, isCustomMusicScore);
			guideLineView = CreateLineView<NoteLineView>("GuideLineView");
			guideLineView.Setup(bundleBuildData, "traceLine_eff.png", LiveConfig.GuideAlpha, isCustomMusicScore);
			if (isEnablePairNotesLine)
			{
				pairNoteLineView = CreateLineView<PairNoteLineView>("PairNoteLineView");
				pairNoteLineView.Setup();
			}

			SetupNoteMask(noteShowRate);
		}

		private void SetupNoteMask(float NoteShowRate)
		{
			if (notePoolDict == null)
			{
				return;
			}

			foreach (Dictionary<NoteType, NotePool> typedPools in notePoolDict.Values)
			{
				if (typedPools == null)
				{
					continue;
				}

				foreach (NotePool pool in typedPools.Values)
				{
					pool?.SetupNoteMask(NoteShowRate);
				}
			}
		}

		public void OnUpdate(float time)
		{
			if (spawnNoteDict != null)
			{
				foreach (BaseNoteView noteView in spawnNoteDict.Values)
				{
					noteView?.Move();
				}
			}
			noteLineView?.Excute(time);
			guideLineView?.Excute(time);
			if (isEnablePairNotesLine)
			{
				pairNoteLineView?.Execute();
			}
		}

		public void Clear()
		{
			if (spawnNoteDict != null)
			{
				foreach (BaseNoteView noteView in spawnNoteDict.Values)
				{
					noteView?.Unspawn();
				}
				spawnNoteDict.Clear();
			}
			noteLineView?.Clear();
			guideLineView?.Clear();
			pairNoteLineView?.Clear();
		}

		public void SpawnNote(INote note)
		{
			if (note == null || spawnNoteDict == null || spawnNoteDict.ContainsKey(note))
			{
				return;
			}

			if (note is GuideNote guideNote)
			{
				guideLineView?.Add(guideNote);
				return;
			}

			if (note is LongNote longNote)
			{
				noteLineView?.Add(longNote);
			}

			NotePool pool = FindPool(note.Category, note.Type);
			BaseNoteView noteView = pool?.Spawn(note);
			if (noteView != null)
			{
				spawnNoteDict[note] = noteView;
			}

			if (isEnablePairNotesLine && note.PairNote != null)
			{
				pairNoteLineView?.Add(note);
			}
		}

		public void UnspawnNote(INote note)
		{
			if (note == null)
			{
				return;
			}

			if (note is GuideNote guideNote)
			{
				guideLineView?.Remove(guideNote);
				return;
			}

			if (note is LongNote longNote)
			{
				noteLineView?.Remove(longNote);
			}

			if (spawnNoteDict != null && spawnNoteDict.TryGetValue(note, out BaseNoteView noteView))
			{
				noteView?.Unspawn();
				spawnNoteDict.Remove(note);
			}
		}

		private NotePool CreateNotePool(int poolCount, string poolName, string noteResourceName, float noteShowRate)
		{
			GameObject poolObject = new GameObject(poolName);
			poolObject.transform.SetParent(noteRoot, false);
			NotePool pool = poolObject.AddComponent<NotePool>();
			GameObject notePrefab = AssetBundleUtility.LoadAsset<GameObject>(LiveConfig.NoteBundleName, noteResourceName, false);
			pool.Setup(Mathf.Max(poolCount, 1), notePrefab, noteShowRate);
			return pool;
		}

		public NotesViewManager()
		{
		}

		static NotesViewManager()
		{
			DefaultNoteDictCapacity = 100;
		}

		private Dictionary<NoteType, NotePool> CreateTypedPools(
			Dictionary<(NoteCategory, NoteType), int> counts,
			NoteCategory category,
			string defaultPoolName,
			string defaultResourceName,
			string criticalPoolName,
			string criticalResourceName,
			float noteShowRate)
		{
			return new Dictionary<NoteType, NotePool>(2)
			{
				[NoteType.Default] = CreateNotePool(GetPoolCount(counts, category, NoteType.Default), defaultPoolName, defaultResourceName, noteShowRate),
				[NoteType.Critical] = CreateNotePool(GetPoolCount(counts, category, NoteType.Critical), criticalPoolName, criticalResourceName, noteShowRate)
			};
		}

		private static int GetPoolCount(Dictionary<(NoteCategory, NoteType), int> counts, NoteCategory category, NoteType type)
		{
			if (counts != null && counts.TryGetValue((category, type), out int count))
			{
				return Mathf.Max(count + 1, 1);
			}

			return 4;
		}

		private NotePool FindPool(NoteCategory category, NoteType type)
		{
			if (notePoolDict == null)
			{
				return null;
			}

			if (!notePoolDict.TryGetValue(category, out Dictionary<NoteType, NotePool> typedPools))
			{
				return null;
			}

			if (typedPools.TryGetValue(type, out NotePool pool))
			{
				return pool;
			}

			return null;
		}

		private T CreateLineView<T>(string viewName) where T : Component
		{
			GameObject viewObject = new GameObject(viewName, typeof(T));
			viewObject.transform.SetParent(noteRoot, false);
			return viewObject.GetComponent<T>();
		}
	}
}
