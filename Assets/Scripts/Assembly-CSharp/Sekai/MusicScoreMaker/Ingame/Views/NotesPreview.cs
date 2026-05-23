using System.Collections.Generic;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class NotesPreview : MonoBehaviour
	{
		private CanvasGroup _raycastControlCanvasGroup;

		private bool _isRaycastEnabledCache;

		[SerializeField]
		private NotePreview _notePreviewPrefab;

		[SerializeField]
		private LongNoteLinesPreview _longNoteLinesPreview;

		private RectTransform _rectTransform;

		private NotePreviewPool _notePreviewPool;

		private RectTransform _cachedNoteInstanceRect;

		private long _beforeStartTicks;

		private long _beforeEndTicks;

		private HashSet<int> _inRangeNoteIdsCache;

		private HashSet<int> _inRangeIdsOnly;

		private List<int> _noteIdsToReleaseCache;

		private List<MusicScoreNoteBase> _tempConnectedNotesList;

		private bool _isInteractiveCache;

		public LongNoteLinesPreview LongNoteLinesPreview => _longNoteLinesPreview;

		public RectTransform RectTransform => _rectTransform;

		public Dictionary<int, NotePreview> NoteDict => _notePreviewPool?.NoteDict;

		public void UpdateView(MusicScoreMakerData MusicScoreMakerData, long startTicks, long endTicks)
		{
			UpdateView(MusicScoreMakerData, startTicks, endTicks, null);
		}

		public void UpdateView(MusicScoreMakerData MusicScoreMakerData, long startTicks, long endTicks, float? customMusicScoreScale)
		{
			if (MusicScoreMakerData == null || _notePreviewPool == null)
			{
				return;
			}

			_notePreviewPool.RefreshParentRectCache();
			UpdateNotes(MusicScoreMakerData, startTicks, endTicks, customMusicScoreScale);
			UpdateSelectedNoteTarget(MusicScoreMakerData, startTicks, endTicks, customMusicScoreScale);
			_longNoteLinesPreview?.UpdateView(MusicScoreMakerData.NoteList, startTicks, endTicks, MusicScoreMakerData);
			_beforeStartTicks = startTicks;
			_beforeEndTicks = endTicks;
		}

		private static int LowerBoundIndexByTicks(IList<MusicScoreNoteBase> noteList, long startTicks)
		{
			if (noteList == null)
			{
				return 0;
			}

			int low = 0;
			int high = noteList.Count;
			while (low < high)
			{
				int mid = low + (high - low) / 2;
				if (noteList[mid].ticks < startTicks)
				{
					low = mid + 1;
				}
				else
				{
					high = mid;
				}
			}
			return low;
		}

		private static int UpperBoundIndexByTicks(IList<MusicScoreNoteBase> noteList, long endTicks)
		{
			if (noteList == null)
			{
				return 0;
			}

			int low = 0;
			int high = noteList.Count;
			while (low < high)
			{
				int mid = low + (high - low) / 2;
				if (noteList[mid].ticks <= endTicks)
				{
					low = mid + 1;
				}
				else
				{
					high = mid;
				}
			}
			return low;
		}

		private void UpdateNotes(MusicScoreMakerData MusicScoreMakerData, long startTicks, long endTicks, float? customMusicScoreScale = null)
		{
			List<MusicScoreNoteBase> noteList = MusicScoreMakerData.NoteList;
			if (noteList == null)
			{
				_notePreviewPool.ReleaseAll();
				return;
			}

			_inRangeNoteIdsCache.Clear();
			int startIndex = LowerBoundIndexByTicks(noteList, startTicks);
			int endIndex = UpperBoundIndexByTicks(noteList, endTicks);
			for (int i = startIndex; i < endIndex; i++)
			{
				MusicScoreNoteBase note = noteList[i];
				if (note == null)
				{
					continue;
				}

				_inRangeNoteIdsCache.Add(note.id);
				_notePreviewPool.UpdateOrSpawn(note, startTicks, endTicks, MusicScoreMakerData, customMusicScoreScale);
			}

			_noteIdsToReleaseCache.Clear();
			foreach (int noteId in _notePreviewPool.NoteDict.Keys)
			{
				if (!_inRangeNoteIdsCache.Contains(noteId) && !MusicScoreMakerData.SelectedNoteTargetIdSet.Contains(noteId))
				{
					_noteIdsToReleaseCache.Add(noteId);
				}
			}
			foreach (int noteId in _noteIdsToReleaseCache)
			{
				_notePreviewPool.Release(noteId);
			}
		}

		private void UpdateSelectedNoteTarget(MusicScoreMakerData MusicScoreMakerData, long startTicks, long endTicks, float? customMusicScoreScale = null)
		{
			if (MusicScoreMakerData.SelectedNoteIdList == null)
			{
				return;
			}

			foreach (int noteId in MusicScoreMakerData.SelectedNoteIdList)
			{
				ProcessSelectedNoteTarget(noteId, MusicScoreMakerData, startTicks, endTicks, customMusicScoreScale);
			}
		}

		private void ProcessSelectedNoteTarget(int noteId, MusicScoreMakerData MusicScoreMakerData, long startTicks, long endTicks, float? customMusicScoreScale = null)
		{
			if (!MusicScoreMakerData.GetNoteIdCacheOrRebuild().TryGetValue(noteId, out MusicScoreNoteBase note))
			{
				return;
			}

			_notePreviewPool.UpdateOrSpawn(note, startTicks, endTicks, MusicScoreMakerData, customMusicScoreScale);
		}

		public void Refresh()
		{
			_notePreviewPool?.ReleaseAll();
			_longNoteLinesPreview?.HideAllLines();
			_cachedNoteInstanceRect = null;
			_beforeStartTicks = 0L;
			_beforeEndTicks = 0L;
		}

		public void RefreshNoteDrawingOrder()
		{
			_notePreviewPool?.RefreshSiblingIndices();
			_longNoteLinesPreview?.EnsureDrawOrderBehindNotes();
		}

		public void MarkSiblingIndicesDirty()
		{
			_notePreviewPool?.MarkSiblingIndicesDirty();
		}

		public void SetInteractive(bool isInteractive)
		{
			_isInteractiveCache = isInteractive;
			if (_notePreviewPool == null)
			{
				return;
			}

			_notePreviewPool.SetInteractive(isInteractive);
		}

		public void SetHideNoInGameSprite(bool hide)
		{
			_notePreviewPool?.SetHideNoInGameSprite(hide);
		}

		public void SetNoteScaleMultiplier(float multiplier)
		{
			_notePreviewPool?.SetNoteScaleMultiplier(multiplier);
		}

		public void Setup(int notePoolCount = 50, int linePoolCount = 10)
		{
			_rectTransform ??= GetComponent<RectTransform>();
			EnsureRaycastControlCanvasGroup();
			_notePreviewPool = new NotePreviewPool(_notePreviewPrefab, transform, notePoolCount);
			_notePreviewPool.SetInteractive(_isInteractiveCache);
			_longNoteLinesPreview?.Setup(linePoolCount);
			_longNoteLinesPreview?.EnsureDrawOrderBehindNotes();
			SetNotesRaycastEnabled(_isRaycastEnabledCache);
		}

		public void SetNotesRaycastEnabled(bool isEnabled)
		{
			_isRaycastEnabledCache = isEnabled;
			EnsureRaycastControlCanvasGroup();
			if (_raycastControlCanvasGroup != null)
			{
				_raycastControlCanvasGroup.blocksRaycasts = isEnabled;
				_raycastControlCanvasGroup.interactable = isEnabled;
			}
		}

		private void EnsureRaycastControlCanvasGroup()
		{
			if (_raycastControlCanvasGroup != null)
			{
				return;
			}

			_raycastControlCanvasGroup = GetComponent<CanvasGroup>();
			if (_raycastControlCanvasGroup == null)
			{
				_raycastControlCanvasGroup = gameObject.AddComponent<CanvasGroup>();
			}
		}

		public void Dispose()
		{
			_notePreviewPool?.Dispose();
			_notePreviewPool = null;
			_longNoteLinesPreview?.HideAllLines();
			_cachedNoteInstanceRect = null;
			_inRangeNoteIdsCache.Clear();
			_inRangeIdsOnly.Clear();
			_noteIdsToReleaseCache.Clear();
			_tempConnectedNotesList.Clear();
		}

		public RectTransform GetNoteInstanceRectTransform()
		{
			if (_cachedNoteInstanceRect != null)
			{
				return _cachedNoteInstanceRect;
			}

			if (_notePreviewPool?.NoteDict != null)
			{
				foreach (NotePreview notePreview in _notePreviewPool.NoteDict.Values)
				{
					if (notePreview != null && notePreview.RectTransform != null)
					{
						_cachedNoteInstanceRect = notePreview.RectTransform;
						return _cachedNoteInstanceRect;
					}
				}
			}

			_cachedNoteInstanceRect = _notePreviewPrefab != null ? _notePreviewPrefab.GetComponent<RectTransform>() : null;
			return _cachedNoteInstanceRect;
		}

		public NotesPreview()
		{
			_isRaycastEnabledCache = true;
			_isInteractiveCache = true;
			_inRangeNoteIdsCache = new HashSet<int>();
			_inRangeIdsOnly = new HashSet<int>();
			_noteIdsToReleaseCache = new List<int>();
			_tempConnectedNotesList = new List<MusicScoreNoteBase>();
		}
	}
}
