using System;
using System.Collections.Generic;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class LongNoteLinesPreview : MonoBehaviour
	{
		[Serializable]
		private class SpriteType
		{
			public Sprite sprite;

			public LongNoteType type;

			public SpriteType()
			{
			}
		}

		private enum LongNoteType
		{
			@long = 0,
			longCritical = 1,
			guide = 2,
			guideCritical = 3
		}

		[SerializeField]
		private LongNoteLinePreview _longNoteLinePreviewPrefab;

		[SerializeField]
		private SpriteType[] _spriteTypes;

		private Dictionary<(NoteType, NoteCategory), Sprite> _spriteCache;

		private Dictionary<int, LongNoteLinePreview> _activeLines;

		private List<LongNoteLinePreview> _linePool;

		private const int DefaultLinePoolCount = 10;

		private int _initialLinePoolCount;

		private RectTransform _cachedParentRectTransform;

		private List<MusicScoreNoteBase> _tempConnectedNotes;

		private HashSet<int> _activeLineIds;

		private HashSet<int> _prevActiveLineIds;

		private List<int> _lineIdsToHide;

		public void Setup(int linePoolCount = 10)
		{
			_initialLinePoolCount = Mathf.Max(linePoolCount, 0);
			_spriteCache = new Dictionary<(NoteType, NoteCategory), Sprite>();
			_activeLines = new Dictionary<int, LongNoteLinePreview>();
			_linePool = new List<LongNoteLinePreview>(_initialLinePoolCount);
			_tempConnectedNotes = new List<MusicScoreNoteBase>();
			_activeLineIds = new HashSet<int>();
			_prevActiveLineIds = new HashSet<int>();
			_lineIdsToHide = new List<int>();
			_cachedParentRectTransform = transform.parent as RectTransform;
			InitializeSpriteCache();
			InitializeLinePool();
		}

		private void OnEnable()
		{
			EnsureDrawOrderBehindNotes();
		}

		private void InitializeLinePool()
		{
			if (_linePool == null || _activeLines == null)
			{
				return;
			}
			if (_linePool.Count > 0 || _activeLines.Count > 0 || _initialLinePoolCount < 1)
			{
				return;
			}

			for (int i = 0; i < _initialLinePoolCount; i++)
			{
				LongNoteLinePreview line = CreateNewLineInstance();
				if (line != null)
				{
					_linePool.Add(line);
				}
			}
		}

		public void EnsureDrawOrderBehindNotes()
		{
			transform.SetAsFirstSibling();
		}

		private void InitializeSpriteCache()
		{
			_spriteCache ??= new Dictionary<(NoteType, NoteCategory), Sprite>();
			_spriteCache.Clear();
			if (_spriteTypes == null)
			{
				return;
			}
			AddSpriteToCache(NoteType.Default, NoteCategory.Long, LongNoteType.@long);
			AddSpriteToCache(NoteType.Default, NoteCategory.FrictionLong, LongNoteType.@long);
			AddSpriteToCache(NoteType.Default, NoteCategory.FrictionHideLong, LongNoteType.@long);
			AddSpriteToCache(NoteType.Default, NoteCategory.Guide, LongNoteType.guide);
			AddSpriteToCache(NoteType.Default, NoteCategory.GuideEnd, LongNoteType.guide);
			AddSpriteToCache(NoteType.Default, NoteCategory.GuideHidden, LongNoteType.guide);
			AddSpriteToCache(NoteType.Critical, NoteCategory.Long, LongNoteType.longCritical);
			AddSpriteToCache(NoteType.Critical, NoteCategory.FrictionLong, LongNoteType.longCritical);
			AddSpriteToCache(NoteType.Critical, NoteCategory.FrictionHideLong, LongNoteType.longCritical);
			AddSpriteToCache(NoteType.Critical, NoteCategory.Guide, LongNoteType.guideCritical);
			AddSpriteToCache(NoteType.Critical, NoteCategory.GuideEnd, LongNoteType.guideCritical);
			AddSpriteToCache(NoteType.Critical, NoteCategory.GuideHidden, LongNoteType.guideCritical);
		}

		private void AddSpriteToCache(NoteType noteType, NoteCategory noteCategory, LongNoteType longNoteType)
		{
			if (_spriteTypes == null)
			{
				return;
			}
			foreach (SpriteType spriteType in _spriteTypes)
			{
				if (spriteType != null && spriteType.type == longNoteType)
				{
					_spriteCache[(noteType, noteCategory)] = spriteType.sprite;
					return;
				}
			}
		}

		public void HideAllLines()
		{
			if (_activeLines == null)
			{
				return;
			}
			foreach (LongNoteLinePreview line in _activeLines.Values)
			{
				if (line != null)
				{
					line.SetActive(false);
					line.ResetForPool();
					if (_linePool != null && !_linePool.Contains(line))
					{
						_linePool.Add(line);
					}
				}
			}
			_activeLines.Clear();
			_activeLineIds?.Clear();
			_prevActiveLineIds?.Clear();
			_lineIdsToHide?.Clear();
		}

		private static int UpperBoundIndexByTicks(List<MusicScoreNoteBase> noteList, long endTicks)
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

		public void UpdateView(List<MusicScoreNoteBase> musicScoreNotes, long startTicks, long endTicks, MusicScoreMakerData MusicScoreMakerData)
		{
			if (_activeLineIds == null || _prevActiveLineIds == null || _activeLines == null || _linePool == null)
			{
				Setup(_initialLinePoolCount);
			}

			_activeLineIds.Clear();
			if (musicScoreNotes == null || musicScoreNotes.Count == 0 || _cachedParentRectTransform == null || MusicScoreMakerData == null)
			{
				HidePreviouslyActiveLines();
				_prevActiveLineIds.Clear();
				return;
			}

			Vector2 parentSize = _cachedParentRectTransform.rect.size;
			Dictionary<int, MusicScoreNoteBase> noteIdCache = MusicScoreMakerData.GetNoteIdCacheOrRebuild();
			int endIndex = UpperBoundIndexByTicks(musicScoreNotes, endTicks);
			for (int i = 0; i < endIndex; i++)
			{
				MusicScoreNoteBase note = musicScoreNotes[i];
				if (note == null || note.isSkip || note.previousConnectionId != -1 || note.nextConnectionId == -1)
				{
					continue;
				}

				note.FindConnectedNotes(noteIdCache, _tempConnectedNotes, true);
				int indexMax = _tempConnectedNotes.Count - 1;
				if (indexMax < 1)
				{
					continue;
				}

				Sprite sprite = GetSpriteForMusicScoreNote(note.type, note.category);
				for (int index = 0; index < indexMax; index++)
				{
					AddOrUpdateLineForMusicScoreNote(_tempConnectedNotes[index], _tempConnectedNotes[index + 1], startTicks, endTicks, sprite, MusicScoreMakerData, parentSize, index, indexMax);
				}
			}

			HidePreviouslyActiveLines();
			(_prevActiveLineIds, _activeLineIds) = (_activeLineIds, _prevActiveLineIds);
		}

		private void HidePreviouslyActiveLines()
		{
			if (_prevActiveLineIds == null || _activeLineIds == null || _lineIdsToHide == null || _activeLines == null)
			{
				return;
			}

			_lineIdsToHide.Clear();
			foreach (int id in _prevActiveLineIds)
			{
				if (!_activeLineIds.Contains(id))
				{
					_lineIdsToHide.Add(id);
				}
			}

			foreach (int id in _lineIdsToHide)
			{
				if (!_activeLines.TryGetValue(id, out LongNoteLinePreview line))
				{
					continue;
				}
				if (line != null)
				{
					line.SetActive(false);
					line.ResetForPool();
					_linePool?.Add(line);
				}
				_activeLines.Remove(id);
			}
		}

		private Sprite GetSpriteForMusicScoreNote(NoteType noteType, NoteCategory noteCategory)
		{
			if (_spriteCache != null && _spriteCache.TryGetValue((noteType, noteCategory), out Sprite sprite))
			{
				return sprite;
			}
			if (_spriteCache != null && _spriteCache.TryGetValue((NoteType.Default, noteCategory), out sprite))
			{
				return sprite;
			}
			return null;
		}

		private void AddOrUpdateLineForMusicScoreNote(MusicScoreNoteBase note, MusicScoreNoteBase nextNote, long startTicks, long endTicks, Sprite sprite, MusicScoreMakerData MusicScoreMakerData, Vector2 parentSize, int index, int indexMax)
		{
			if (note == null || nextNote == null)
			{
				return;
			}

			bool isInRange = note.ticks < endTicks && nextNote.ticks > startTicks;
			HashSet<int> selectedNoteIds = MusicScoreMakerData?.SelectedNoteTargetIdSet;
			bool isSelected = selectedNoteIds != null && (selectedNoteIds.Contains(note.id) || selectedNoteIds.Contains(nextNote.id));
			if (!isSelected && !isInRange)
			{
				return;
			}

			(Vector2 startLeft, Vector2 startRight) = CalcPreviewEdgePositionsForMusicScoreNote(note, startTicks, endTicks, parentSize, MusicScoreMakerData);
			(Vector2 endLeft, Vector2 endRight) = CalcPreviewEdgePositionsForMusicScoreNote(nextNote, startTicks, endTicks, parentSize, MusicScoreMakerData);
			LongNoteLinePreview line = GetOrCreateLine(note.id);
			if (line == null)
			{
				return;
			}

			line.UpdateView(new LongNoteLinePreview.ViewData
			{
				ParentId = nextNote.id,
				StartLeft = startLeft,
				StartRight = startRight,
				EndLeft = endLeft,
				EndRight = endRight,
				LineType = note.noteLineType,
				Sprite = sprite,
				Index = index,
				IndexMax = indexMax
			}, isSelected);
			_activeLineIds.Add(note.id);
		}

		private (Vector2, Vector2) CalcPreviewEdgePositionsForMusicScoreNote(MusicScoreNoteBase note, long startTicks, long endTicks, Vector2 parentSizeDelta, MusicScoreMakerData MusicScoreMakerData)
		{
			if (note == null)
			{
				return (Vector2.zero, Vector2.zero);
			}
			long ticks = note.ticks;
			int laneStart = note.laneStart;
			int laneEnd = note.laneEnd;
			MusicScoreMakerUtility.CalcNoteOperation(MusicScoreMakerData, ref ticks, ref laneStart, ref laneEnd, note);
			float laneWidth = parentSizeDelta.x / MusicScoreMakerModel.LaneCount;
			float y = parentSizeDelta.y * MusicScoreMakerUtility.CalcNormalizedPositionFromTicks(startTicks, endTicks, ticks) - parentSizeDelta.y * 0.5f;
			Vector2 left = new Vector2(laneWidth * laneStart - parentSizeDelta.x * 0.5f, y);
			Vector2 right = new Vector2(laneWidth * (laneEnd + 1) - parentSizeDelta.x * 0.5f, y);
			return (left, right);
		}

		private LongNoteLinePreview GetOrCreateLine(int id)
		{
			if (_activeLines.TryGetValue(id, out LongNoteLinePreview line))
			{
				return line;
			}
			if (_linePool.Count > 0)
			{
				int lastIndex = _linePool.Count - 1;
				line = _linePool[lastIndex];
				_linePool.RemoveAt(lastIndex);
			}
			else
			{
				line = CreateNewLineInstance();
			}
			if (line == null)
			{
				return null;
			}
			_activeLines[id] = line;
			return line;
		}

		private LongNoteLinePreview CreateNewLineInstance()
		{
			if (_longNoteLinePreviewPrefab == null)
			{
				return null;
			}
			LongNoteLinePreview line = Instantiate(_longNoteLinePreviewPrefab, transform);
			line.Setup();
			line.gameObject.SetActive(false);
			return line;
		}

		public int FindRayHitNoteLine(PointerEventData pointerEventData)
		{
			if (_activeLines == null)
			{
				return int.MinValue;
			}
			foreach (LongNoteLinePreview line in _activeLines.Values)
			{
				if (line != null && line.IsPointerOver(pointerEventData))
				{
					return line.NoteId;
				}
			}
			return int.MinValue;
		}

		public LongNoteLinesPreview()
		{
			_initialLinePoolCount = DefaultLinePoolCount;
		}
	}
}
