using System.Collections.Generic;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class ClipboardCachePreview : MonoBehaviour
	{
		private const int PREVIEW_BAR_COUNT = 1;

		[SerializeField]
		private NotesPreview _notesPreview;

		[SerializeField]
		private BarLinePreview _barLinePreview;

		[SerializeField]
		private RectTransform _rectTransform;

		private ClipboardCacheData _currentCacheData;

		private MusicScoreMakerData _tempMusicScoreMakerData;

		private long _displayStartTicks;

		private long _displayEndTicks;

		private bool _isInitialized;

		private List<MusicScoreEventData> _emptyEventList;

		private readonly List<MusicScoreNoteBase> _tempNoteListCache;

		private void Awake()
		{
			Setup();
		}

		public void Setup()
		{
			if (_isInitialized)
			{
				return;
			}
			if (_notesPreview != null)
			{
				_notesPreview.Setup(50, 10);
			}
			if (_barLinePreview != null)
			{
				_barLinePreview.Setup();
			}
			DisableRaycastTargets();
			_isInitialized = true;
		}

		private void DisableRaycastTargets()
		{
			Graphic[] graphics = GetComponentsInChildren<Graphic>(true);
			for (int i = 0; i < graphics.Length; i++)
			{
				if (graphics[i] != null)
				{
					graphics[i].raycastTarget = false;
				}
			}
		}

		public void UpdatePreview(ClipboardCacheData cacheData, bool isFlipHorizontal = false)
		{
			if (!_isInitialized)
			{
				Setup();
			}
			_currentCacheData = cacheData;
			if (cacheData?.CopiedNoteList == null || cacheData.CopiedNoteList.Count == 0)
			{
				ClearPreview();
				return;
			}
			CreateTempMusicScoreMakerData(cacheData, isFlipHorizontal);
			CalculateDisplayRange(cacheData.CopiedNoteList, cacheData.CopiedEventDataList);
			if (_notesPreview != null)
			{
				_notesPreview.UpdateView(_tempMusicScoreMakerData, _displayStartTicks, _displayEndTicks);
			}
			if (_barLinePreview != null && _tempMusicScoreMakerData != null)
			{
				_barLinePreview.UpdateView(CalculateScale(), _displayStartTicks, _displayEndTicks, _tempMusicScoreMakerData.MusicScoreEventDataList, false, false);
			}
		}

		private void CreateTempMusicScoreMakerData(ClipboardCacheData cacheData, bool isFlipHorizontal)
		{
			_tempMusicScoreMakerData ??= new MusicScoreMakerData();
			_tempMusicScoreMakerData.ClearNotes();
			_tempNoteListCache.Clear();
			if (cacheData?.CopiedNoteList == null)
			{
				return;
			}
			foreach (MusicScoreNoteBase source in cacheData.CopiedNoteList)
			{
				if (source == null)
				{
					continue;
				}
				MusicScoreNoteBase note = source.Clone();
				if (isFlipHorizontal)
				{
					FlipNoteLanePosition(note);
					FlipNoteDirection(note);
				}
				_tempNoteListCache.Add(note);
			}
			_tempMusicScoreMakerData.AddNoteRange(_tempNoteListCache);
			_tempMusicScoreMakerData.ClearEventList();
			if (cacheData.CopiedEventDataList != null)
			{
				_tempMusicScoreMakerData.AddEventRange(cacheData.CopiedEventDataList);
			}
			_tempMusicScoreMakerData.ClearSelectedNotes();
			_tempMusicScoreMakerData.ClearSelectedTemporaryNotes();
			_tempMusicScoreMakerData.ClearSelectedEvents();
			_tempMusicScoreMakerData.ClearSelectedTemporaryEvents();
			_tempMusicScoreMakerData.SelectedTargetOperation = null;
		}

		private static void FlipNoteLanePosition(MusicScoreNoteBase note)
		{
			if (note == null)
			{
				return;
			}
			int laneStart = note.laneStart;
			note.laneStart = MusicScoreMakerModel.LaneCountMinus1 - note.laneEnd;
			note.laneEnd = MusicScoreMakerModel.LaneCountMinus1 - laneStart;
		}

		private void FlipNoteDirection(MusicScoreNoteBase note)
		{
			if (note == null)
			{
				return;
			}
			if (note.direction == NoteDirection.Left)
			{
				note.direction = NoteDirection.Right;
			}
			else if (note.direction == NoteDirection.Right)
			{
				note.direction = NoteDirection.Left;
			}
		}

		private void CalculateDisplayRange(List<MusicScoreNoteBase> notes, List<MusicScoreEventData> events)
		{
			if (notes != null && notes.Count > 0)
			{
				long minTicks = long.MaxValue;
				for (int i = 0; i < notes.Count; i++)
				{
					if (notes[i] != null && notes[i].ticks < minTicks)
					{
						minTicks = notes[i].ticks;
					}
				}
				_displayStartTicks = 1920L * (minTicks / 1920L);
				_displayEndTicks = _displayStartTicks + 1920L;
				return;
			}
			_displayStartTicks = 0L;
			_displayEndTicks = 1920L;
		}

		private float CalculateScale()
		{
			return _displayEndTicks > _displayStartTicks ? (_displayEndTicks - _displayStartTicks) / 1920f : 1f;
		}

		private void ClearPreview()
		{
			if (_notesPreview != null && _tempMusicScoreMakerData != null)
			{
				_tempMusicScoreMakerData.ClearNoteList();
				_tempMusicScoreMakerData.ClearEventList();
				_notesPreview.UpdateView(_tempMusicScoreMakerData, 0L, 0L);
			}
			if (_barLinePreview != null)
			{
				_barLinePreview.UpdateView(1f, 0L, 0L, _emptyEventList, false, false);
			}
		}

		private void OnDestroy()
		{
			if (_notesPreview != null)
			{
				_notesPreview.Dispose();
			}
			_tempMusicScoreMakerData = null;
			_currentCacheData = null;
		}

		public ClipboardCachePreview()
		{
			_emptyEventList = new List<MusicScoreEventData>();
			_tempNoteListCache = new List<MusicScoreNoteBase>(64);
		}
	}
}
