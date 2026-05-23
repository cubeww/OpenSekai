using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;
using MusicScorePreviewPlayData = Sekai.MusicScoreMaker.OutGame.MusicScorePreviewPlayData;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMakerPreviewStartDialog : Common2ButtonDialog
	{
		private const float PREVIEW_DURATION_SECONDS = 10f;

		private const float PREVIEW_PLACEABLE_TICKS_OFFSET_SEC = 10f;

		private const int MIN_BAR = 1;

		[SerializeField]
		private Toggle _barModeToggle;

		[SerializeField]
		private Toggle _autoModeToggle;

		[SerializeField]
		private UIPartsNumericInputField _barInputField;

		[SerializeField]
		private MusicScoreMakerPreviewStartPreview _preview;

		private MusicScoreMakerData _musicScoreMakerData;

		private Action<MusicScorePreviewPlayData> _onDecideCallback;

		private int _feverStartBar;

		private int _currentBar;

		private int _lastBarModeBar;

		private bool _isBarMode;

		private int _maxBar;

		private int _masterMusicSec;

		private float _fillerSec;

		public void Setup(MusicScoreMakerData musicScoreMakerData, EventBase[] eventArray, Action<MusicScorePreviewPlayData> onDecideCallback, int masterMusicSec = 0, float fillerSec = 0f)
		{
			_musicScoreMakerData = musicScoreMakerData;
			_onDecideCallback = onDecideCallback;
			_masterMusicSec = masterMusicSec;
			_fillerSec = fillerSec;
			_feverStartBar = GetFeverStartBar(eventArray, musicScoreMakerData);
			_currentBar = 1;
			_lastBarModeBar = 1;
			_isBarMode = true;

			if (_barModeToggle != null)
			{
				_barModeToggle.SetIsOnWithoutNotify(true);
				if (_barModeToggle.group != null)
				{
					_barModeToggle.group.allowSwitchOff = false;
				}
				_barModeToggle.onValueChanged.RemoveListener(OnBarModeToggleChanged);
				_barModeToggle.onValueChanged.AddListener(OnBarModeToggleChanged);
			}

			if (_autoModeToggle != null)
			{
				_autoModeToggle.SetIsOnWithoutNotify(false);
				if (_autoModeToggle.group != null)
				{
					_autoModeToggle.group.allowSwitchOff = false;
				}
				_autoModeToggle.onValueChanged.RemoveListener(OnAutoModeToggleChanged);
				_autoModeToggle.onValueChanged.AddListener(OnAutoModeToggleChanged);
			}

			if (_barInputField != null)
			{
				_barInputField.text = _currentBar.ToString();
				_barInputField.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;
				_barInputField.onValueChanged.RemoveListener(OnBarInputChanged);
				_barInputField.onValueChanged.AddListener(OnBarInputChanged);
				_barInputField.onEndEdit.RemoveListener(OnBarInputEndEdit);
				_barInputField.onEndEdit.AddListener(OnBarInputEndEdit);
			}

			_maxBar = CalculateMaxBar(musicScoreMakerData);
			if (_preview != null)
			{
				_preview.SetMaxBar(_maxBar);
				_preview.SetFeverStartBar(_feverStartBar);
				_preview.SetAutoMode(!_isBarMode);
				_preview.Initialize(OnPreviewBarChanged);
			}

			UpdatePreview();
		}

		private int CalculateMaxBar(MusicScoreMakerData musicScoreMakerData)
		{
			if (musicScoreMakerData == null)
			{
				return 999;
			}

			if (_masterMusicSec >= 1 && musicScoreMakerData.MusicScoreEventDataList != null && musicScoreMakerData.MusicScoreEventDataList.Count > 0)
			{
				float previewLatestStartSec = Mathf.Max(_masterMusicSec - PREVIEW_PLACEABLE_TICKS_OFFSET_SEC, 0f);
				long ticks = MusicScoreMakerUtility.GetTicksFromTime(previewLatestStartSec, MusicScoreMakerUtility.ConvertMusicScoreInfo(musicScoreMakerData.MusicScoreEventDataList));
				MusicScoreMakerUtility.CalculateBarAndProgressFromTicks(ticks, out int bar, out float progress, musicScoreMakerData);
				return Mathf.Max(1, progress > 0f ? bar + 1 : bar);
			}

			long maxTicks = musicScoreMakerData.MusicScoreTicksMax > 0 ? musicScoreMakerData.MusicScoreTicksMax : musicScoreMakerData.GetMaxTicks();
			MusicScoreMakerUtility.CalculateBarAndProgressFromTicks(maxTicks, out int maxBar, out float maxProgress, musicScoreMakerData);
			if (maxProgress > 0f)
			{
				maxBar++;
			}
			return Mathf.Max(1, maxBar + 10);
		}

		private void OnPreviewBarChanged(int newBar)
		{
			if (!_isBarMode)
			{
				return;
			}
			_currentBar = Mathf.Clamp(newBar, MIN_BAR, _maxBar);
			_lastBarModeBar = _currentBar;
			if (_barInputField != null)
			{
				_barInputField.SetTextWithoutNotify(_currentBar.ToString());
			}
		}

		private int GetFeverStartBar(EventBase[] eventArray, MusicScoreMakerData musicScoreMakerData)
		{
			float feverStartTimeSec = FindFeverStartTimeSec(eventArray);
			if (musicScoreMakerData == null || feverStartTimeSec < 0f || musicScoreMakerData.MusicScoreEventDataList == null || musicScoreMakerData.MusicScoreEventDataList.Count < 1)
			{
				return 1;
			}

			long ticks = MusicScoreMakerUtility.GetTicksFromTime(feverStartTimeSec, MusicScoreMakerUtility.ConvertMusicScoreInfo(musicScoreMakerData.MusicScoreEventDataList));
			MusicScoreMakerUtility.CalculateBarAndProgressFromTicks(ticks, out int bar, out _, musicScoreMakerData);
			return Mathf.Max(1, bar + 1);
		}

		private static float FindFeverStartTimeSec(EventBase[] eventArray)
		{
			if (eventArray == null)
			{
				return -1f;
			}
			foreach (EventBase eventBase in eventArray)
			{
				if (eventBase is FeverStartEvent)
				{
					return eventBase.MusicScoreInfo.time;
				}
			}
			foreach (EventBase eventBase in eventArray)
			{
				if (eventBase is FeverBeginEvent)
				{
					return eventBase.MusicScoreInfo.time;
				}
			}
			return -1f;
		}

		private void OnBarModeToggleChanged(bool isOn)
		{
			if (!isOn)
			{
				if (_autoModeToggle != null && !_autoModeToggle.isOn && _barModeToggle != null)
				{
					_barModeToggle.SetIsOnWithoutNotify(true);
				}
				return;
			}

			_isBarMode = true;
			if (_autoModeToggle != null)
			{
				_autoModeToggle.SetIsOnWithoutNotify(false);
			}
			_currentBar = Mathf.Clamp(_lastBarModeBar, MIN_BAR, _maxBar);
			if (_barInputField != null)
			{
				_barInputField.interactable = true;
				_barInputField.SetTextWithoutNotify(_currentBar.ToString());
			}
			if (_preview != null)
			{
				_preview.SetAutoMode(false);
			}
			UpdatePreview();
		}

		private void OnAutoModeToggleChanged(bool isOn)
		{
			if (!isOn)
			{
				if (_barModeToggle != null && !_barModeToggle.isOn && _autoModeToggle != null)
				{
					_autoModeToggle.SetIsOnWithoutNotify(true);
				}
				return;
			}

			if (_isBarMode)
			{
				_lastBarModeBar = Mathf.Max(_currentBar, MIN_BAR);
			}
			_isBarMode = false;
			if (_barModeToggle != null)
			{
				_barModeToggle.SetIsOnWithoutNotify(false);
			}
			if (_barInputField != null)
			{
				_barInputField.interactable = false;
			}
			_currentBar = _feverStartBar;
			if (_preview != null)
			{
				_preview.SetAutoMode(true);
			}
			UpdatePreview();
		}

		private void OnBarInputChanged(string value)
		{
			if (int.TryParse(value, out int bar))
			{
				_currentBar = Mathf.Clamp(bar, MIN_BAR, _maxBar);
				_lastBarModeBar = _currentBar;
				UpdatePreview();
			}
		}

		private void OnBarInputEndEdit(string value)
		{
			if (!int.TryParse(value, out int bar))
			{
				bar = MIN_BAR;
			}
			_currentBar = Mathf.Clamp(bar, MIN_BAR, _maxBar);
			_lastBarModeBar = _currentBar;
			if (_barInputField != null)
			{
				_barInputField.SetTextWithoutNotify(_currentBar.ToString());
			}
			UpdatePreview();
		}

		private void UpdatePreview()
		{
			if (_preview != null && _musicScoreMakerData != null)
			{
				_preview.UpdatePreviewFromBar(_musicScoreMakerData, _currentBar);
			}
		}

		protected override void OnClickOK()
		{
			_onDecideCallback?.Invoke(CreatePreviewPlayData());
			base.OnClickOK();
		}

		[CanBeNull]
		private MusicScorePreviewPlayData CreatePreviewPlayData()
		{
			if (_musicScoreMakerData == null)
			{
				return null;
			}

			long startTicks = MusicScoreMakerUtility.CalculateTicksFromBarAndProgress(_currentBar - 1, 0f, _musicScoreMakerData.MusicScoreEventDataList);
			float startTimeSec = _musicScoreMakerData.GetTimeFromTicks(startTicks);
			long endTicks = MusicScoreMakerUtility.GetTicksFromTime(startTimeSec + PREVIEW_DURATION_SECONDS, MusicScoreMakerUtility.ConvertMusicScoreInfo(_musicScoreMakerData.MusicScoreEventDataList));

			MusicScoreMakerData previewData = new MusicScoreMakerData();
			Dictionary<int, MusicScoreNoteBase> noteById = new Dictionary<int, MusicScoreNoteBase>(_musicScoreMakerData.NoteList?.Count ?? 0);
			if (_musicScoreMakerData.NoteList != null)
			{
				foreach (MusicScoreNoteBase note in _musicScoreMakerData.NoteList)
				{
					if (note != null)
					{
						noteById[note.id] = note;
					}
				}

				HashSet<int> previewNoteIds = new HashSet<int>();
				foreach (MusicScoreNoteBase note in _musicScoreMakerData.NoteList)
				{
					if (note == null || note.previousConnectionId != -1)
					{
						continue;
					}

					MusicScoreNoteBase endNote = note.FindEndNote(noteById) ?? note;
					if (note.nextConnectionId == -1)
					{
						if (note.ticks >= startTicks && note.ticks < endTicks)
						{
							previewNoteIds.Add(note.id);
						}
					}
					else if (endNote.ticks >= startTicks && note.ticks < endTicks)
					{
						MusicScoreNoteBase current = note;
						while (current != null)
						{
							previewNoteIds.Add(current.id);
							if (current.nextConnectionId == -1 || !noteById.TryGetValue(current.nextConnectionId, out current))
							{
								break;
							}
						}
					}
				}

				foreach (MusicScoreNoteBase note in _musicScoreMakerData.NoteList)
				{
					if (note != null && previewNoteIds.Contains(note.id))
					{
						previewData.AddNote(note.Clone());
					}
				}
			}

			if (_musicScoreMakerData.MusicScoreEventDataList != null)
			{
				foreach (MusicScoreEventData eventData in _musicScoreMakerData.MusicScoreEventDataList)
				{
					if (eventData != null)
					{
						previewData.AddEvent(new MusicScoreEventData
						{
							id = eventData.id,
							eventType = eventData.eventType,
							ticks = eventData.ticks,
							changeValue = eventData.changeValue
						});
					}
				}
			}
			previewData.EventArray = _musicScoreMakerData.EventArray;
			previewData.MusicScoreTicksMax = endTicks;
			return new MusicScorePreviewPlayData(startTimeSec, previewData);
		}

		private void OnDestroy()
		{
			if (_barModeToggle != null)
			{
				_barModeToggle.onValueChanged.RemoveAllListeners();
			}
			if (_autoModeToggle != null)
			{
				_autoModeToggle.onValueChanged.RemoveAllListeners();
			}
			if (_barInputField != null)
			{
				_barInputField.onValueChanged.RemoveAllListeners();
				_barInputField.onEndEdit.RemoveAllListeners();
			}
			if (_preview != null)
			{
				_preview.ClearPreview();
			}
		}

		public MusicScoreMakerPreviewStartDialog()
		{
			_feverStartBar = 1;
			_currentBar = 1;
			_lastBarModeBar = 1;
			_isBarMode = true;
			_maxBar = 1;
		}
	}
}
