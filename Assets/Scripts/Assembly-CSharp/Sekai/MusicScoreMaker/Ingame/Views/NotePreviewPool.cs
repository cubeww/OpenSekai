using System.Collections.Generic;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class NotePreviewPool
	{
		private Dictionary<int, NotePreview> _noteDict;

		private List<NotePreview> _poolList;

		private const int DefaultPoolCount = 50;

		private readonly int _basePoolCount;

		private NotePreview _notePrefab;

		private int _nextSpawnIndex;

		private Transform _parent;

		private RectTransform _parentRectTransform;

		private List<NotePreview> _sortedNoteList;

		private List<NotePreviewWithX> _sortList;

		private ParentRectContext _cachedParentRectContext;

		private bool _siblingIndicesDirty;

		private bool _hideNoInGameSprite;

		private bool _isInteractive;

		private float _noteScaleMultiplier;

		public Dictionary<int, NotePreview> NoteDict
		{
			get
			{
				return _noteDict;
			}
		}

		public int ShowNoteCount
		{
			get
			{
				return _noteDict.Count;
			}
		}

		public void RefreshParentRectCache()
		{
			if (_parentRectTransform == null && _parent != null)
			{
				_parentRectTransform = _parent as RectTransform;
			}
			if (_parentRectTransform != null)
			{
				Rect rect = _parentRectTransform.rect;
				_cachedParentRectContext = new ParentRectContext(rect.width, rect.height, _parentRectTransform.anchoredPosition);
			}
		}

		public void SetHideNoInGameSprite(bool hide)
		{
			_hideNoInGameSprite = hide;
			foreach (NotePreview notePreview in _poolList)
			{
				notePreview.SetHideNoInGameSprite(hide);
			}
		}

		public void SetNoteScaleMultiplier(float multiplier)
		{
			_noteScaleMultiplier = multiplier;
			foreach (NotePreview notePreview in _poolList)
			{
				notePreview.SetNoteScaleMultiplier(multiplier);
			}
		}

		public void SetInteractive(bool isInteractive)
		{
			_isInteractive = isInteractive;
			foreach (NotePreview notePreview in _poolList)
			{
				notePreview.SetInteractive(isInteractive);
			}
		}

		public NotePreviewPool(NotePreview notePrefab, Transform parent, int poolCount = 50)
		{
			_notePrefab = notePrefab;
			_parent = parent;
			_parentRectTransform = parent as RectTransform;
			_basePoolCount = Mathf.Max(poolCount, 1);
			_noteDict = new Dictionary<int, NotePreview>(_basePoolCount);
			_poolList = new List<NotePreview>(_basePoolCount);
			_sortedNoteList = new List<NotePreview>(_basePoolCount);
			_sortList = new List<NotePreviewWithX>(_basePoolCount);
			_isInteractive = true;
			_noteScaleMultiplier = 1f;
			RefreshParentRectCache();
			for (int i = 0; i < _basePoolCount; i++)
			{
				_poolList.Add(CreateNotePreview());
			}
		}

		public void Release(int noteId)
		{
			if (!_noteDict.TryGetValue(noteId, out NotePreview notePreview))
			{
				return;
			}

			notePreview.Release();
			_noteDict.Remove(noteId);
			_siblingIndicesDirty = true;
		}

		public void Dispose()
		{
			foreach (NotePreview notePreview in _poolList)
			{
				if (notePreview != null)
				{
					notePreview.Dispose();
					Object.Destroy(notePreview.gameObject);
				}
			}
			_poolList.Clear();
			_noteDict.Clear();
			_sortedNoteList.Clear();
			_sortList.Clear();
		}

		public void UpdateOrSpawn(MusicScoreNoteBase noteBase, long startTicks, long endTicks, MusicScoreMakerData MusicScoreMakerData)
		{
			UpdateOrSpawn(noteBase, startTicks, endTicks, MusicScoreMakerData, null);
		}

		public void UpdateOrSpawn(MusicScoreNoteBase noteBase, long startTicks, long endTicks, MusicScoreMakerData MusicScoreMakerData, float? customMusicScoreScale)
		{
			if (noteBase == null)
			{
				return;
			}

			float currentMusicScoreScale = customMusicScoreScale ?? Sekai.MusicScoreMaker.Ingame.Utilities.MusicScoreMakerUtility.GetCurrentMusicScoreScale();
			if (_noteDict.TryGetValue(noteBase.id, out NotePreview notePreview))
			{
				notePreview.Show(noteBase, startTicks, endTicks, MusicScoreMakerData, _parentRectTransform, currentMusicScoreScale, _cachedParentRectContext);
				return;
			}

			Spawn(noteBase, startTicks, endTicks, MusicScoreMakerData, currentMusicScoreScale);
		}

		private void Spawn(MusicScoreNoteBase noteBase, long startTicks, long endTicks, MusicScoreMakerData MusicScoreMakerData, float currentMusicScoreScale)
		{
			NotePreview notePreview = null;
			for (int i = 0; i < _poolList.Count; i++)
			{
				int index = (_nextSpawnIndex + i) % _poolList.Count;
				if (!_poolList[index].IsUsing)
				{
					notePreview = _poolList[index];
					_nextSpawnIndex = (index + 1) % _poolList.Count;
					break;
				}
			}

			if (notePreview == null)
			{
				notePreview = CreateNotePreview();
				_poolList.Add(notePreview);
				_nextSpawnIndex = _poolList.Count % Mathf.Max(_poolList.Count, 1);
			}

			if (notePreview.IsUsing)
			{
				_noteDict.Remove(notePreview.NoteId);
			}
			notePreview.Show(noteBase, startTicks, endTicks, MusicScoreMakerData, _parentRectTransform, currentMusicScoreScale, _cachedParentRectContext);
			_noteDict[noteBase.id] = notePreview;
			_siblingIndicesDirty = true;
		}

		private NotePreview CreateNotePreview()
		{
			NotePreview notePreview = Object.Instantiate(_notePrefab, _parent);
			notePreview.Setup();
			notePreview.SetHideNoInGameSprite(_hideNoInGameSprite);
			notePreview.SetNoteScaleMultiplier(_noteScaleMultiplier);
			notePreview.SetInteractive(_isInteractive);
			notePreview.Release();
			return notePreview;
		}

		public void RefreshSiblingIndices()
		{
			UpdateSiblingIndices();
			_siblingIndicesDirty = false;
		}

		public void MarkSiblingIndicesDirty()
		{
			_siblingIndicesDirty = true;
		}

		private void UpdateSiblingIndices()
		{
			if (!_siblingIndicesDirty)
			{
				return;
			}

			_sortList.Clear();
			_sortedNoteList.Clear();
			foreach (NotePreview notePreview in _noteDict.Values)
			{
				if (notePreview != null && notePreview.IsUsing)
				{
					_sortList.Add(new NotePreviewWithX(notePreview, notePreview.transform.position.x));
				}
			}
			if (_sortList.Count == 0)
			{
				return;
			}

			_sortList.Sort((a, b) => a.x.CompareTo(b.x));
			for (int i = 0; i < _sortList.Count; i++)
			{
				_sortedNoteList.Add(_sortList[i].note);
			}

			if (MusicScoreMakerSettingsManager.DrawSmallerTickToBack)
			{
				for (int i = 0; i < _sortedNoteList.Count; i++)
				{
					int siblingIndex = i + 1;
					Transform noteTransform = _sortedNoteList[i].transform;
					if (noteTransform.GetSiblingIndex() != siblingIndex)
					{
						noteTransform.SetSiblingIndex(siblingIndex);
					}
				}
			}
			else
			{
				int siblingIndex = 1;
				for (int i = _sortedNoteList.Count - 1; i >= 0; i--, siblingIndex++)
				{
					Transform noteTransform = _sortedNoteList[i].transform;
					if (noteTransform.GetSiblingIndex() != siblingIndex)
					{
						noteTransform.SetSiblingIndex(siblingIndex);
					}
				}
			}
		}

		public void ReleaseAll()
		{
			foreach (NotePreview notePreview in _poolList)
			{
				notePreview.Release();
			}
			_noteDict.Clear();
			_siblingIndicesDirty = true;
		}
	}
}
