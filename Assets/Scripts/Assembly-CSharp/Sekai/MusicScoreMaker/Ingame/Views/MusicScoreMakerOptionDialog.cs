using System.Collections.Generic;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMakerOptionDialog : Common2ButtonDialog
	{
		[SerializeField]
		private Transform _uiParent;

		[SerializeField]
		private CustomSlider _sliderPrefab;

		[SerializeField]
		private CustomToggle _checkBoxPrefab;

		[SerializeField]
		private CustomButton _buttonPrefab;

		[SerializeField]
		private CustomTextMesh _textPrefab;

		[SerializeField]
		private CustomToggle _togglePrefab;

		[SerializeField]
		private Transform _toggleParentPrefab;

		private List<GameObject> _generatedUIElements;

		private List<CustomSlider> _sliders;

		private List<CustomToggle> _checkBoxes;

		private int _selectedLayoutPatternIndexPortrait;

		private int _selectedLayoutPatternIndexLandscape;

		private Dictionary<string, List<CustomToggle>> _toggleGroups;

		private Dictionary<string, Transform> _toggleParents;

		private MusicScoreMakerOptionUIBuilder _uiBuilder;

		private bool _isUIComponentsCreated;

		private IUIElementInfo[] _settingItems;

		public void Setup()
		{
			_generatedUIElements ??= new List<GameObject>();
			_sliders ??= new List<CustomSlider>();
			_checkBoxes ??= new List<CustomToggle>();
			_toggleGroups ??= new Dictionary<string, List<CustomToggle>>();
			_toggleParents ??= new Dictionary<string, Transform>();
			_selectedLayoutPatternIndexPortrait = MusicScoreMakerSettingsManager.SelectedLayoutPatternIndexPortrait;
			_selectedLayoutPatternIndexLandscape = MusicScoreMakerSettingsManager.SelectedLayoutPatternIndexLandscape;
			_settingItems = CreateSettingItems();
			CreateUIComponents();
		}

		private void OnClearClipboardButtonClicked()
		{
			ClipboardCacheManager.Instance.ClearAll();
		}

		private void OnDestroy()
		{
			if (_uiBuilder != null)
			{
				_uiBuilder.CleanupClockMenuPreview();
				_uiBuilder.ClearElements();
			}
			if (_generatedUIElements != null)
			{
				foreach (GameObject element in _generatedUIElements)
				{
					if (element != null)
					{
						ClearEventListeners(element);
					}
				}
			}
		}

		private void ClearEventListeners(GameObject gameObject)
		{
			if (gameObject == null)
			{
				return;
			}
			foreach (CustomSlider slider in gameObject.GetComponentsInChildren<CustomSlider>(true))
			{
				slider.RemoveAllListeners();
			}
			foreach (Toggle toggle in gameObject.GetComponentsInChildren<Toggle>(true))
			{
				toggle.onValueChanged.RemoveAllListeners();
			}
			foreach (Button button in gameObject.GetComponentsInChildren<Button>(true))
			{
				button.onClick.RemoveAllListeners();
			}
		}

		private void CreateUIComponents()
		{
			if (_uiParent == null)
			{
				return;
			}
			if (_isUIComponentsCreated)
			{
				RebuildUI();
				return;
			}
			_uiBuilder = new MusicScoreMakerOptionUIBuilder(_sliderPrefab, _checkBoxPrefab, _buttonPrefab, _textPrefab, _togglePrefab, _toggleParentPrefab, _generatedUIElements, _sliders, _checkBoxes, _toggleGroups, _toggleParents, _selectedLayoutPatternIndexPortrait, _selectedLayoutPatternIndexLandscape);
			_uiBuilder.SetParentDialog(this);
			for (int i = 0; i < _settingItems.Length; i++)
			{
				_uiBuilder.CreateUIElement(_settingItems[i], i, _uiParent);
			}
			_isUIComponentsCreated = true;
		}

		private void ClearChildObjects(Transform parent)
		{
			if (parent == null)
			{
				return;
			}
			for (int i = parent.childCount - 1; i >= 0; i--)
			{
				Destroy(parent.GetChild(i).gameObject);
			}
		}

		protected override void OnClickOK()
		{
			ApplyData();
			base.OnClickOK();
		}

		private void ApplyData()
		{
			if (_uiBuilder != null)
			{
				MusicScoreMakerSettingsManager.SelectedLayoutPatternIndexPortrait = _uiBuilder.GetSelectedLayoutPatternIndexPortrait();
				MusicScoreMakerSettingsManager.SelectedLayoutPatternIndexLandscape = _uiBuilder.GetSelectedLayoutPatternIndexLandscape();
			}
			MusicScoreMakerSettingsManager.SaveSettingData();
		}

		public void RebuildUI()
		{
			_uiBuilder?.ClearElements();
			ClearChildObjects(_uiParent);
			_uiBuilder = new MusicScoreMakerOptionUIBuilder(_sliderPrefab, _checkBoxPrefab, _buttonPrefab, _textPrefab, _togglePrefab, _toggleParentPrefab, _generatedUIElements, _sliders, _checkBoxes, _toggleGroups, _toggleParents, _selectedLayoutPatternIndexPortrait, _selectedLayoutPatternIndexLandscape);
			_uiBuilder.SetParentDialog(this);
			if (_settingItems == null)
			{
				_settingItems = CreateSettingItems();
			}
			for (int i = 0; i < _settingItems.Length; i++)
			{
				_uiBuilder.CreateUIElement(_settingItems[i], i, _uiParent);
			}
		}

		public MusicScoreMakerOptionDialog()
		{
			_generatedUIElements = new List<GameObject>();
			_sliders = new List<CustomSlider>();
			_checkBoxes = new List<CustomToggle>();
			_toggleGroups = new Dictionary<string, List<CustomToggle>>();
			_toggleParents = new Dictionary<string, Transform>();
		}

		private IUIElementInfo[] CreateSettingItems()
		{
			List<IUIElementInfo> items = new List<IUIElementInfo>
			{
				UIElementInfoFactory.CreateText(MusicScoreMakerOptionConstants.SECTION_DISPLAY_SETTINGS),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_ZOOM_TIMELINE_STEP, MusicScoreMakerSettingData.MIN_ZOOM_TIMELINE_STEP, MusicScoreMakerSettingData.MAX_ZOOM_TIMELINE_STEP, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_3, () => MusicScoreMakerSettingsManager.ZoomTimelineStep, value => MusicScoreMakerSettingsManager.ZoomTimelineStep = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_ZOOM_TIMELINE_MAX, MusicScoreMakerSettingData.MIN_ZOOM_TIMELINE_SCALE, MusicScoreMakerSettingData.MAX_ZOOM_TIMELINE_SCALE, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_2, () => MusicScoreMakerSettingsManager.ZoomTimelineScaleMax, value => MusicScoreMakerSettingsManager.ZoomTimelineScaleMax = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_ZOOM_TIMELINE_MIN, MusicScoreMakerSettingData.MIN_ZOOM_TIMELINE_SCALE, MusicScoreMakerSettingData.MAX_ZOOM_TIMELINE_SCALE, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_3, () => MusicScoreMakerSettingsManager.ZoomTimelineScaleMin, value => MusicScoreMakerSettingsManager.ZoomTimelineScaleMin = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_SCORE_DISPLAY_SCALE_H, MusicScoreMakerSettingData.MIN_SCORE_DISPLAY_SCALE, MusicScoreMakerSettingData.MAX_SCORE_DISPLAY_SCALE, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_2, () => MusicScoreMakerSettingsManager.ScoreDisplayScaleHorizontal, value => MusicScoreMakerSettingsManager.ScoreDisplayScaleHorizontal = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_SCORE_DISPLAY_SCALE_V, MusicScoreMakerSettingData.MIN_SCORE_DISPLAY_SCALE, MusicScoreMakerSettingData.MAX_SCORE_DISPLAY_SCALE, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_2, () => MusicScoreMakerSettingsManager.ScoreDisplayScaleVertical, value => MusicScoreMakerSettingsManager.ScoreDisplayScaleVertical = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_TOOL_WINDOW_CHILD_SCALE, MusicScoreMakerSettingData.DEFAULT_TOOL_WINDOW_CHILD_SCALE_MIN, MusicScoreMakerSettingData.DEFAULT_TOOL_WINDOW_CHILD_SCALE_MAX, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_2, () => MusicScoreMakerSettingsManager.ToolWindowChildScale, value => MusicScoreMakerSettingsManager.ToolWindowChildScale = value),
				UIElementInfoFactory.CreateToggleParent(MusicScoreMakerOptionConstants.SETTING_LAYOUT_PATTERN, MusicScoreMakerOptionConstants.TOGGLE_GROUP_LAYOUT_PATTERN)
			};

			for (int i = 0; i < MusicScoreMakerSettingsManager.LayoutPatternCount; i++)
			{
				int patternIndex = i;
				items.Add(UIElementInfoFactory.CreateToggle(string.Format(MusicScoreMakerOptionConstants.PATTERN_NAME_FORMAT, patternIndex + 1), MusicScoreMakerOptionConstants.TOGGLE_GROUP_LAYOUT_PATTERN, patternIndex));
			}

			items.AddRange(new IUIElementInfo[]
			{
				UIElementInfoFactory.CreateText(MusicScoreMakerOptionConstants.SECTION_OPERATION_SETTINGS),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_UNDO_STACK_LIMIT, MusicScoreMakerSettingData.MIN_UNDO_STACK_LIMIT, MusicScoreMakerSettingData.MAX_UNDO_STACK_LIMIT, SliderValueType.Integer, MusicScoreMakerOptionConstants.FORMAT_INTEGER, () => MusicScoreMakerSettingsManager.UndoStackLimit, value => MusicScoreMakerSettingsManager.UndoStackLimit = Mathf.RoundToInt(value)),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_SHOW_FOCUS_TICKS_RATE, MusicScoreMakerSettingData.MIN_SHOW_FOCUS_TICKS_RATE, MusicScoreMakerSettingData.MAX_SHOW_FOCUS_TICKS_RATE, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_2, () => MusicScoreMakerSettingsManager.ShowFocusTicksRate, value => MusicScoreMakerSettingsManager.ShowFocusTicksRate = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_TICKS_PER_SCROLL_STEP, MusicScoreMakerSettingData.MIN_TICKS_PER_SCROLL_STEP, MusicScoreMakerSettingData.MAX_TICKS_PER_SCROLL_STEP, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_3, () => MusicScoreMakerSettingsManager.TicksPerScrollStep, value => MusicScoreMakerSettingsManager.TicksPerScrollStep = value),
				UIElementInfoFactory.CreateCheckBox(MusicScoreMakerOptionConstants.SETTING_ENABLE_SWIPE_SCROLL, () => MusicScoreMakerSettingsManager.EnableSwipeScroll, value => MusicScoreMakerSettingsManager.EnableSwipeScroll = value),
				UIElementInfoFactory.CreateCheckBox(MusicScoreMakerOptionConstants.SETTING_ENABLE_INVALID_PLACEMENT_CHECK, () => MusicScoreMakerSettingsManager.EnableInvalidPlacementCheck, value => MusicScoreMakerSettingsManager.EnableInvalidPlacementCheck = value),
				UIElementInfoFactory.CreateCheckBox(MusicScoreMakerOptionConstants.SETTING_AREA_SELECT_PARTIAL_OVERLAP, () => MusicScoreMakerSettingsManager.AreaSelectPartialOverlap, value => MusicScoreMakerSettingsManager.AreaSelectPartialOverlap = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_NOTE_EDGE_WIDTH, MusicScoreMakerSettingData.MIN_NOTE_EDGE_WIDTH, MusicScoreMakerSettingData.MAX_NOTE_EDGE_WIDTH, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_2, () => MusicScoreMakerSettingsManager.NoteEdgeWidth, value => MusicScoreMakerSettingsManager.NoteEdgeWidth = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_NOTE_Y_SCALE_START_THRESHOLD, MusicScoreMakerSettingData.MIN_NOTE_Y_SCALE_START_THRESHOLD, MusicScoreMakerSettingData.MAX_NOTE_Y_SCALE_START_THRESHOLD, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_2, () => MusicScoreMakerSettingsManager.NoteYScaleStartThreshold, value => MusicScoreMakerSettingsManager.NoteYScaleStartThreshold = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_NOTE_Y_SCALE_END_THRESHOLD, MusicScoreMakerSettingData.MIN_NOTE_Y_SCALE_END_THRESHOLD, MusicScoreMakerSettingData.MAX_NOTE_Y_SCALE_END_THRESHOLD, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_2, () => MusicScoreMakerSettingsManager.NoteYScaleEndThreshold, value => MusicScoreMakerSettingsManager.NoteYScaleEndThreshold = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_NOTE_Y_SCALE_MIN, MusicScoreMakerSettingData.MIN_NOTE_Y_SCALE_MIN, MusicScoreMakerSettingData.MAX_NOTE_Y_SCALE_MIN, SliderValueType.Float, MusicScoreMakerOptionConstants.FORMAT_FLOAT_2, () => MusicScoreMakerSettingsManager.NoteYScaleMin, value => MusicScoreMakerSettingsManager.NoteYScaleMin = value),
				UIElementInfoFactory.CreateCheckBox(MusicScoreMakerOptionConstants.SETTING_DRAW_SMALLER_TICK_TO_BACK, () => MusicScoreMakerSettingsManager.DrawSmallerTickToBack, value => MusicScoreMakerSettingsManager.DrawSmallerTickToBack = value),
				UIElementInfoFactory.CreateText(MusicScoreMakerOptionConstants.SECTION_AUTO_SAVE_SETTINGS),
				UIElementInfoFactory.CreateCheckBox(MusicScoreMakerOptionConstants.SETTING_AUTO_SAVE_ENABLED, () => MusicScoreMakerSettingsManager.AutoSaveEnabled, value => MusicScoreMakerSettingsManager.AutoSaveEnabled = value),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_AUTO_SAVE_INTERVAL, MusicScoreMakerSettingData.MIN_AUTO_SAVE_INTERVAL, MusicScoreMakerSettingData.MAX_AUTO_SAVE_INTERVAL, SliderValueType.Integer, MusicScoreMakerOptionConstants.FORMAT_INTEGER, () => MusicScoreMakerSettingsManager.AutoSaveInterval, value => MusicScoreMakerSettingsManager.AutoSaveInterval = Mathf.RoundToInt(value)),
				UIElementInfoFactory.CreateText(MusicScoreMakerOptionConstants.SECTION_DISPLAY_LINE_SETTINGS),
				UIElementInfoFactory.CreateCheckBox(MusicScoreMakerOptionConstants.SETTING_SHOW_BAR_LINES, () => MusicScoreMakerSettingsManager.ShowBarLines, value => MusicScoreMakerSettingsManager.ShowBarLines = value),
				UIElementInfoFactory.CreateCheckBox(MusicScoreMakerOptionConstants.SETTING_SHOW_BEAT_LINES, () => MusicScoreMakerSettingsManager.ShowBeatLines, value => MusicScoreMakerSettingsManager.ShowBeatLines = value),
				UIElementInfoFactory.CreateCheckBox(MusicScoreMakerOptionConstants.SETTING_SHOW_QUANTIZE_LINES, () => MusicScoreMakerSettingsManager.ShowQuantizeLines, value => MusicScoreMakerSettingsManager.ShowQuantizeLines = value),
				UIElementInfoFactory.CreateText(MusicScoreMakerOptionConstants.SECTION_AUDIO_SETTINGS),
				UIElementInfoFactory.CreateCheckBox(MusicScoreMakerOptionConstants.SETTING_PLAY_MUSIC_SE_ENABLED, () => MusicScoreMakerSettingsManager.PlayMusicSEEnabled, value => MusicScoreMakerSettingsManager.PlayMusicSEEnabled = value),
				UIElementInfoFactory.CreateText(MusicScoreMakerOptionConstants.SECTION_SYSTEM),
				UIElementInfoFactory.CreateSlider(MusicScoreMakerOptionConstants.SETTING_MAX_CLIPBOARD_CACHE_COUNT, MusicScoreMakerSettingData.MIN_MAX_CLIPBOARD_CACHE_COUNT, MusicScoreMakerSettingData.MAX_MAX_CLIPBOARD_CACHE_COUNT, SliderValueType.Integer, MusicScoreMakerOptionConstants.FORMAT_INTEGER, () => MusicScoreMakerSettingsManager.MaxClipboardCacheCount, value => MusicScoreMakerSettingsManager.MaxClipboardCacheCount = Mathf.RoundToInt(value)),
				UIElementInfoFactory.CreateButton(MusicScoreMakerOptionConstants.SETTING_RESET_SETTING_DATA, MusicScoreMakerSettingsManager.ResetSettingData),
				UIElementInfoFactory.CreateButton("クリップボードキャッシュ削除", OnClearClipboardButtonClicked)
			});

			return items.ToArray();
		}
	}
}
