using System.Collections.Generic;
using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.Live
{
	public class NotesViewManager
	{
		private const int DefaultNoteDictCapacity = 128;

		private Dictionary<INote, BaseNoteView> spawnNoteDict;

		private Transform noteRoot;

		private Dictionary<NoteCategory, Dictionary<NoteType, NotePool>> notePoolDict;

		private NoteLineView noteLineView;

		private NoteLineView guideLineView;

		private PairNoteLineView pairNoteLineView;

		private bool isEnablePairNotesLine = true;

		private Dictionary<(NoteCategory, NoteType), GameObject> notePrefabDict;

		private Texture longNoteLineTexture;

		private Texture guideLineTexture;

		private Texture simultaneousLineTexture;

		private Material longNoteLineMaterial;

		private Material pairNoteLineMaterial;

		public void Setup(Transform liveRoot)
		{
			spawnNoteDict = new Dictionary<INote, BaseNoteView>(DefaultNoteDictCapacity);

			GameObject noteRootObject = new GameObject("NoteRoot");
			noteRoot = noteRootObject.transform;
			if (liveRoot != null)
			{
				noteRoot.SetParent(liveRoot, false);
			}
		}

		public void SetAssets(NotePrefabSet[] notePrefabs, Texture longNoteLineTexture, Texture guideLineTexture, Texture simultaneousLineTexture, Material longNoteLineMaterial, Material pairNoteLineMaterial)
		{
			notePrefabDict = new Dictionary<(NoteCategory, NoteType), GameObject>();
			if (notePrefabs != null)
			{
				for (int i = 0; i < notePrefabs.Length; i++)
				{
					NotePrefabSet set = notePrefabs[i];
					if (set == null)
					{
						continue;
					}

					if (set.defaultPrefab != null)
					{
						notePrefabDict[(set.category, NoteType.Default)] = set.defaultPrefab;
					}
					if (set.criticalPrefab != null)
					{
						notePrefabDict[(set.category, NoteType.Critical)] = set.criticalPrefab;
					}
				}
			}

			this.longNoteLineTexture = longNoteLineTexture;
			this.guideLineTexture = guideLineTexture;
			this.simultaneousLineTexture = simultaneousLineTexture;
			this.longNoteLineMaterial = longNoteLineMaterial;
			this.pairNoteLineMaterial = pairNoteLineMaterial;
		}

		public void CreateNotePool(BaseLiveController baseController, Dictionary<(NoteCategory, NoteType), int> noteCountDictionary)
		{
			if (baseController == null || noteRoot == null || notePoolDict != null)
			{
				return;
			}

			if (baseController.Settings != null)
			{
				isEnablePairNotesLine = baseController.Settings.UseSimultaneousPushingLine;
			}

			notePoolDict = new Dictionary<NoteCategory, Dictionary<NoteType, NotePool>>(7);

			AddPoolSet(noteCountDictionary, NoteCategory.Normal, "NormalNotePool", "NormalCriticalNotePool");
			AddPoolSet(noteCountDictionary, NoteCategory.Long, "LongNotePool", "LongCriticalPool");
			AddPoolSet(noteCountDictionary, NoteCategory.Connection, "ConnectionNotePool", "ConnectionCriticalPool");
			AddPoolSet(noteCountDictionary, NoteCategory.Flick, "FlickNotePool", "FlickCriticalPool");
			AddPoolSet(noteCountDictionary, NoteCategory.Friction, "FrictionNotePool", "FrictionCriticalNotePool");
			AddPoolSet(noteCountDictionary, NoteCategory.FrictionLong, "FrictionLongNotePool", "FrictionLongCriticalNotePool");
			AddPoolSet(noteCountDictionary, NoteCategory.FrictionFlick, "FrictionFlickNotePool", "FrictionFlickCriticalNotePool");

			noteLineView = CreateLineView<NoteLineView>("NoteLineView");
			noteLineView?.Setup(baseController.BootData != null ? baseController.BootData.BundleBuildData : null, longNoteLineTexture, longNoteLineMaterial);

			guideLineView = CreateLineView<NoteLineView>("GuideLineView");
			guideLineView?.Setup(baseController.BootData != null ? baseController.BootData.BundleBuildData : null, guideLineTexture, longNoteLineMaterial);

			if (isEnablePairNotesLine)
			{
				pairNoteLineView = CreateLineView<PairNoteLineView>("PairNoteLineView");
				pairNoteLineView?.Setup(simultaneousLineTexture, pairNoteLineMaterial);
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
			if (isEnablePairNotesLine)
			{
				pairNoteLineView?.Clear();
			}
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

			NoteCategory category = LiveUtility.NormalizeViewCategory(note.Category);
			if (notePoolDict != null &&
				notePoolDict.TryGetValue(category, out Dictionary<NoteType, NotePool> pools) &&
				pools.TryGetValue(note.Type, out NotePool pool))
			{
				BaseNoteView noteView = pool.Spawn(note);
				if (noteView != null)
				{
					spawnNoteDict.Add(note, noteView);
				}
			}

			if (isEnablePairNotesLine)
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

		private void AddPoolSet(Dictionary<(NoteCategory, NoteType), int> noteCountDictionary, NoteCategory category, string defaultPoolName, string criticalPoolName)
		{
			Dictionary<NoteType, NotePool> pools = new Dictionary<NoteType, NotePool>(2)
			{
				{ NoteType.Default, CreateNotePool(GetPoolCount(noteCountDictionary, category, NoteType.Default), defaultPoolName, GetPrefab(category, NoteType.Default)) },
				{ NoteType.Critical, CreateNotePool(GetPoolCount(noteCountDictionary, category, NoteType.Critical), criticalPoolName, GetPrefab(category, NoteType.Critical)) }
			};
			notePoolDict[category] = pools;
		}

		private NotePool CreateNotePool(int poolCount, string poolName, GameObject notePrefab)
		{
			GameObject poolObject = new GameObject(poolName);
			NotePool notePool = poolObject.AddComponent<NotePool>();
			if (noteRoot != null)
			{
				notePool.transform.SetParent(noteRoot, false);
			}

			if (notePrefab != null)
			{
				notePool.Setup(Mathf.Max(poolCount, 1), notePrefab);
			}
			return notePool;
		}

		private GameObject GetPrefab(NoteCategory category, NoteType type)
		{
			if (notePrefabDict == null)
			{
				return null;
			}
			if (notePrefabDict.TryGetValue((category, type), out GameObject prefab))
			{
				return prefab;
			}
			if (type == NoteType.Critical && notePrefabDict.TryGetValue((category, NoteType.Default), out prefab))
			{
				return prefab;
			}
			return null;
		}

		private static int GetPoolCount(Dictionary<(NoteCategory, NoteType), int> noteCountDictionary, NoteCategory category, NoteType type)
		{
			if (noteCountDictionary != null && noteCountDictionary.TryGetValue((category, type), out int count))
			{
				return count;
			}
			return 0;
		}

		private T CreateLineView<T>(string objectName) where T : Component
		{
			GameObject lineObject = new GameObject(objectName);
			T lineView = lineObject.AddComponent<T>();
			if (noteRoot != null)
			{
				lineView.transform.SetParent(noteRoot, false);
			}
			return lineView;
		}
	}
}
