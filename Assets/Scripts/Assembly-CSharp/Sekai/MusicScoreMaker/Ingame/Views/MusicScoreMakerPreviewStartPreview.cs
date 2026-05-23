using System;
using System.Collections.Generic;
using Sekai.MusicScoreMaker.Ingame.Input;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMakerPreviewStartPreview : MonoBehaviour
	{
		[SerializeField]
		private ToolInputHandler _toolInputHandler;

		[SerializeField]
		private NotesPreview _notesPreview;

		[SerializeField]
		private BarLinePreview _barLinePreview;

		[SerializeField]
		private RectTransform _rectTransform;

		[SerializeField]
		private RectTransform _focusTickLine;

		[SerializeField]
		private CustomSlider _scrollSlider;

		[SerializeField]
		[Tooltip("プレビューのスケール係数。transformのスケールとGetCurrentMusicScoreScale()に乗算されます。")]
		private float _previewScaleMultiplier;

		[Tooltip("ドラッグ感度。値が大きいほど速くスクロールします。")]
		[SerializeField]
		private float _dragSensitivity;

		[SerializeField]
		[Tooltip("表示する小節数")]
		private int _displayBars;

		[SerializeField]
		[Tooltip("ノーツのスケール乗数。localScaleに乗算し、sizeDeltaを逆数で補正します。")]
		private float _noteScaleMultiplier;

		private MusicScoreMakerData _tempMusicScoreMakerData;

		private long _displayStartTicks;

		private long _displayEndTicks;

		private bool _isInitialized;

		private List<MusicScoreEventData> _emptyEventList;

		private int _currentBar;

		private MusicScoreMakerData _musicScoreMakerData;

		private Action<int> _onBarChanged;

		private int _maxBar;

		private bool _isAutoMode;

		private int _feverStartBar;

		private bool _isSliderDragging;

		private long _currentFocusTicks;

		private long _cachedDisplayTicksRange;

		private float _cachedFocusRate;

		private long _cachedMaxTicks;

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
				_notesPreview.SetInteractive(false);
				_notesPreview.SetNoteScaleMultiplier(_noteScaleMultiplier);
			}
			if (_barLinePreview != null)
			{
				_barLinePreview.Setup();
			}
			if (_toolInputHandler != null)
			{
				_toolInputHandler.RemoveAllAndAddListener(onDragEvent: OnDrag);
			}
			SetupScrollSlider();
			UpdateCache();
			_isInitialized = true;
		}

		private void UpdateCache()
		{
			_cachedDisplayTicksRange = 1920L * Mathf.Max(1, _displayBars);
			_cachedFocusRate = MusicScoreMakerSettingsManager.ShowFocusTicksRate;
			_cachedMaxTicks = 1920L * (Mathf.Max(1, _maxBar) - 1);
		}

		private void UpdateCachedMaxTicks()
		{
			if (_musicScoreMakerData?.MusicScoreEventDataList != null && _musicScoreMakerData.MusicScoreEventDataList.Count > 0)
			{
				_cachedMaxTicks = MusicScoreMakerUtility.CalculateTicksFromBarAndProgress(_maxBar - 1, 0f, _musicScoreMakerData.MusicScoreEventDataList);
			}
			else
			{
				_cachedMaxTicks = 1920L * (Mathf.Max(1, _maxBar) - 1);
			}
		}

		private void SetupScrollSlider()
		{
			if (_scrollSlider == null)
			{
				return;
			}
			_scrollSlider.wholeNumbers = true;
			_scrollSlider.minValue = 1f;
			_scrollSlider.maxValue = Mathf.Max(1, _maxBar);
			_scrollSlider.SetValueWithoutNotify(1f);
			_scrollSlider.RemoveAllAndAddListener(OnScrollSliderValueChanged);
		}

		public void Initialize(Action<int> onBarChanged)
		{
			_onBarChanged = onBarChanged;
		}

		public void SetMaxBar(int maxBar)
		{
			_maxBar = Mathf.Max(1, maxBar);
			if (_isInitialized)
			{
				UpdateCache();
			}
			if (_scrollSlider != null)
			{
				_scrollSlider.maxValue = _maxBar;
			}
		}

		public void SetAutoMode(bool isAutoMode)
		{
			_isAutoMode = isAutoMode;
		}

		public void SetFeverStartBar(int feverStartBar)
		{
			_feverStartBar = Mathf.Max(1, feverStartBar);
		}

		public void UpdatePreviewFromBar(MusicScoreMakerData musicScoreMakerData, int bar)
		{
			_musicScoreMakerData = musicScoreMakerData;
			UpdateCachedMaxTicks();
			long barStartTicks = GetBarStartTicks(bar);
			long focusOffset = (long)(_cachedFocusRate * _cachedDisplayTicksRange);
			long focusTicks = barStartTicks < 0 ? 0L : System.Math.Min(barStartTicks, _cachedMaxTicks);
			long startTicks = focusTicks - focusOffset;
			long actualFocusTicks = startTicks < 0 && focusTicks == 0 ? 0 : focusTicks;
			int actualBar = 1;
			if (musicScoreMakerData?.MusicScoreEventDataList != null && musicScoreMakerData.MusicScoreEventDataList.Count > 0)
			{
				MusicScoreMakerUtility.CalculateBarAndProgressFromTicks(actualFocusTicks, out int zeroBasedBar, out _, musicScoreMakerData);
				actualBar = Mathf.Max(1, zeroBasedBar + 1);
			}
			else
			{
				actualBar = Mathf.Max(1, (int)(actualFocusTicks / 1920L) + 1);
			}
			UpdatePreview(musicScoreMakerData, startTicks, startTicks + _cachedDisplayTicksRange, actualBar, actualFocusTicks);
		}

		private float GetEffectivePreviewScale()
		{
			float localScaleY = _notesPreview != null ? _notesPreview.transform.localScale.y : 1f;
			if (Mathf.Approximately(localScaleY, 0f))
			{
				localScaleY = 1f;
			}
			return MusicScoreMakerUtility.GetCurrentMusicScoreScale() * _previewScaleMultiplier / localScaleY;
		}

		private long GetBarStartTicks(int bar)
		{
			if (_musicScoreMakerData?.MusicScoreEventDataList != null && _musicScoreMakerData.MusicScoreEventDataList.Count > 0)
			{
				return MusicScoreMakerUtility.CalculateTicksFromBarAndProgress(bar - 1, 0f, _musicScoreMakerData.MusicScoreEventDataList);
			}
			return 1920L * (bar - 1);
		}

		private long GetFocusPositionTicks(long startTicks, long endTicks)
		{
			return startTicks + (long)(_cachedFocusRate * (endTicks - startTicks));
		}

		private long CalculateActualFocusTicks(long targetFocusTicks, long startTicks, long endTicks)
		{
			if (targetFocusTicks == 0L && startTicks < 0L)
			{
				return 0L;
			}
			return GetFocusPositionTicks(startTicks, endTicks);
		}

		private void UpdatePreviewFromFocusTicks(long targetFocusTicks, bool shouldInvokeBarChanged)
		{
			if (_musicScoreMakerData == null)
			{
				return;
			}
			long focusTicks = targetFocusTicks < 0 ? 0 : System.Math.Min(targetFocusTicks, _cachedMaxTicks);
			long focusOffset = (long)(_cachedFocusRate * _cachedDisplayTicksRange);
			long startTicks = focusTicks - focusOffset;
			long endTicks = startTicks + _cachedDisplayTicksRange;
			long actualFocusTicks = startTicks < 0 && focusTicks == 0 ? 0 : focusTicks;
			int previousBar = _currentBar;
			int bar;
			if (_musicScoreMakerData.MusicScoreEventDataList != null && _musicScoreMakerData.MusicScoreEventDataList.Count > 0)
			{
				MusicScoreMakerUtility.CalculateBarAndProgressFromTicks(actualFocusTicks, out int zeroBasedBar, out _, _musicScoreMakerData);
				bar = Mathf.Max(1, zeroBasedBar + 1);
			}
			else
			{
				bar = Mathf.Max(1, (int)(actualFocusTicks / 1920L) + 1);
			}
			UpdatePreview(_musicScoreMakerData, startTicks, endTicks, bar, actualFocusTicks);
			if (bar != previousBar && shouldInvokeBarChanged)
			{
				_onBarChanged?.Invoke(bar);
			}
		}

		private void UpdatePreview(MusicScoreMakerData musicScoreMakerData, long startTicks, long endTicks, int bar)
		{
			UpdatePreview(musicScoreMakerData, startTicks, endTicks, bar, GetFocusPositionTicks(startTicks, endTicks));
		}

		private void UpdatePreview(MusicScoreMakerData musicScoreMakerData, long startTicks, long endTicks, int bar, long focusTicks)
		{
			if (!_isInitialized)
			{
				Setup();
			}
			if (musicScoreMakerData == null)
			{
				ClearPreview();
				return;
			}

			_musicScoreMakerData = musicScoreMakerData;
			_displayStartTicks = startTicks;
			_displayEndTicks = endTicks;
			_currentBar = bar;
			_currentFocusTicks = focusTicks;
			UpdateCachedMaxTicks();
			CreateTempMusicScoreMakerData(musicScoreMakerData);
			float previewScale = GetEffectivePreviewScale();
			_notesPreview?.UpdateView(_tempMusicScoreMakerData, _displayStartTicks, _displayEndTicks, previewScale);
			_notesPreview?.SetInteractive(false);
			if (_barLinePreview != null && _tempMusicScoreMakerData != null)
			{
				float scale = CalculateScale();
				_barLinePreview.UpdateView(scale, _displayStartTicks, _displayEndTicks, _tempMusicScoreMakerData.MusicScoreEventDataList, false, true);
			}
			UpdateFocusTickLine();
			UpdateScrollSliderValue();
		}

		private void UpdateScrollSliderValue()
		{
			if (_scrollSlider != null && !_isSliderDragging)
			{
				_scrollSlider.SetValueWithoutNotify(_currentBar);
			}
		}

		private void UpdateFocusTickLine()
		{
			if (_focusTickLine == null || _rectTransform == null)
			{
				return;
			}
			long targetTicks = _isAutoMode ? GetBarStartTicks(_feverStartBar) : _currentFocusTicks;
			bool visible = targetTicks >= _displayStartTicks && targetTicks <= _displayEndTicks;
			_focusTickLine.gameObject.SetActive(visible);
			if (!visible)
			{
				return;
			}
			long range = _displayEndTicks - _displayStartTicks;
			if (range < 1L)
			{
				return;
			}
			float height = _rectTransform.rect.height;
			Vector2 position = _focusTickLine.anchoredPosition;
			position.y = height * ((targetTicks - _displayStartTicks) / (float)range);
			_focusTickLine.anchoredPosition = position;
		}

		private void CreateTempMusicScoreMakerData(MusicScoreMakerData sourceData)
		{
			_tempMusicScoreMakerData ??= new MusicScoreMakerData();
			_tempMusicScoreMakerData.ClearNoteList();
			if (sourceData?.NoteList != null)
			{
				_tempMusicScoreMakerData.AddNoteRange(sourceData.NoteList);
			}
			_tempMusicScoreMakerData.ClearEventList();
			if (sourceData?.MusicScoreEventDataList != null)
			{
				_tempMusicScoreMakerData.AddEventRange(sourceData.MusicScoreEventDataList);
			}
		}

		private float CalculateScale()
		{
			return _displayEndTicks > _displayStartTicks ? (_displayEndTicks - _displayStartTicks) / 1920f : 1f;
		}

		public void ClearPreview()
		{
			if (_tempMusicScoreMakerData != null)
			{
				_tempMusicScoreMakerData.ClearNoteList();
				_tempMusicScoreMakerData.ClearEventList();
				_notesPreview?.UpdateView(_tempMusicScoreMakerData, 0L, 0L);
			}
			_barLinePreview?.UpdateView(1f, 0L, 0L, _emptyEventList, false, false);
		}

		private void OnDestroy()
		{
			_toolInputHandler?.RemoveAllListeners();
			_scrollSlider?.RemoveAllListeners();
			_notesPreview?.Dispose();
			_tempMusicScoreMakerData = null;
			_musicScoreMakerData = null;
			_onBarChanged = null;
		}

		private void OnDrag(PointerEventData eventData)
		{
			if (_musicScoreMakerData == null || eventData == null)
			{
				return;
			}
			float height = _rectTransform != null ? _rectTransform.rect.height : 100f;
			if (height <= 0f)
			{
				return;
			}
			long range = _displayEndTicks - _displayStartTicks;
			if (range < 1L)
			{
				return;
			}
			long deltaTicks = (long)(eventData.delta.y * (range / height) * _dragSensitivity);
			UpdatePreviewFromFocusTicks(_currentFocusTicks - deltaTicks, true);
		}

		private void OnScrollSliderValueChanged(float value)
		{
			_isSliderDragging = true;
			int previousBar = _currentBar;
			int bar = Mathf.Max(1, (int)value);
			UpdatePreviewFromFocusTicks(GetBarStartTicks(bar), false);
			_isSliderDragging = false;
			if (_currentBar != previousBar)
			{
				_onBarChanged?.Invoke(_currentBar);
			}
		}

		public MusicScoreMakerPreviewStartPreview()
		{
			_previewScaleMultiplier = 1f;
			_dragSensitivity = 10f;
			_displayBars = 8;
			_noteScaleMultiplier = 1f;
			_emptyEventList = new List<MusicScoreEventData>();
			_currentBar = 1;
			_maxBar = 999;
			_feverStartBar = 1;
		}
	}
}
