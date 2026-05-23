using System;
using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMakerOptionUIBuilder
	{
		private readonly CustomSlider _sliderPrefab;

		private readonly CustomToggle _checkBoxPrefab;

		private readonly CustomButton _buttonPrefab;

		private readonly CustomTextMesh _textPrefab;

		private readonly CustomToggle _togglePrefab;

		private readonly Transform _toggleParentPrefab;

		private readonly List<GameObject> _generatedUIElements;

		private readonly List<CustomSlider> _sliders;

		private readonly List<CustomToggle> _checkBoxes;

		private readonly Dictionary<string, List<CustomToggle>> _toggleGroups;

		private readonly Dictionary<string, Transform> _toggleParents;

		private int _selectedLayoutPatternIndexPortrait;

		private int _selectedLayoutPatternIndexLandscape;

		private int _sectionCreationCount;

		private MusicScoreMakerOptionDialog _parentDialog;

		public MusicScoreMakerOptionUIBuilder(CustomSlider sliderPrefab, CustomToggle checkBoxPrefab, CustomButton buttonPrefab, CustomTextMesh textPrefab, CustomToggle togglePrefab, Transform toggleParentPrefab, List<GameObject> generatedUIElements, List<CustomSlider> sliders, List<CustomToggle> checkBoxes, Dictionary<string, List<CustomToggle>> toggleGroups, Dictionary<string, Transform> toggleParents, int selectedLayoutPatternIndexPortrait, int selectedLayoutPatternIndexLandscape)
		{
			_sliderPrefab = sliderPrefab;
			_checkBoxPrefab = checkBoxPrefab;
			_buttonPrefab = buttonPrefab;
			_textPrefab = textPrefab;
			_togglePrefab = togglePrefab;
			_toggleParentPrefab = toggleParentPrefab;
			_generatedUIElements = generatedUIElements;
			_sliders = sliders;
			_checkBoxes = checkBoxes;
			_toggleGroups = toggleGroups;
			_toggleParents = toggleParents;
			_selectedLayoutPatternIndexPortrait = selectedLayoutPatternIndexPortrait;
			_selectedLayoutPatternIndexLandscape = selectedLayoutPatternIndexLandscape;
		}

		public void CreateUIElement(IUIElementInfo item, int index, Transform parent)
		{
			if (item == null || parent == null)
			{
				return;
			}
			switch (item.ElementType)
			{
			case UIElementType.Slider:
				CreateSliderElement((SliderElementInfo)item, index, parent);
				break;
			case UIElementType.CheckBox:
				CreateCheckBoxElement((CheckBoxElementInfo)item, parent);
				break;
			case UIElementType.Button:
				CreateButtonElement((ButtonElementInfo)item, parent);
				break;
			case UIElementType.Text:
				CreateTextElement((TextElementInfo)item, parent);
				break;
			case UIElementType.ToggleParent:
				CreateToggleParentElement((ToggleParentElementInfo)item, parent);
				break;
			case UIElementType.Toggle:
				CreateToggleElement((ToggleElementInfo)item);
				break;
			}
		}

		private void CreateSliderElement(SliderElementInfo item, int index, Transform parent)
		{
			if (_sliderPrefab == null)
			{
				return;
			}
			CustomSlider slider = UnityEngine.Object.Instantiate(_sliderPrefab, parent);
			slider.gameObject.SetActive(true);
			slider.minValue = item.MinValue;
			slider.maxValue = item.MaxValue;
			slider.wholeNumbers = item.ValueType == SliderValueType.Integer;
			slider.SetValueWithoutNotify(item.GetFloatValue?.Invoke() ?? item.MinValue);
			CustomTextMesh[] labels = slider.GetComponentsInChildren<CustomTextMesh>(true);
			if (labels.Length > 0)
			{
				labels[0].SetText(item.DisplayName);
			}
			CustomTextMesh valueLabel = labels.Length > 1 ? labels[1] : null;
			UpdateSliderValueDisplay(slider, item, valueLabel);
			slider.RemoveAllAndAddListener(value =>
			{
				float appliedValue = item.ValueType == SliderValueType.Integer ? Mathf.Round(value) : value;
				item.SetFloatValue?.Invoke(appliedValue);
				if (!Mathf.Approximately(appliedValue, value))
				{
					slider.SetValueWithoutNotify(appliedValue);
				}
				UpdateSliderValueDisplay(slider, item, valueLabel);
			});
			_sliders?.Add(slider);
			_generatedUIElements?.Add(slider.gameObject);
		}

		private void CreateCheckBoxElement(CheckBoxElementInfo item, Transform parent)
		{
			if (_checkBoxPrefab == null)
			{
				return;
			}
			CustomToggle toggle = UnityEngine.Object.Instantiate(_checkBoxPrefab, parent);
			toggle.gameObject.SetActive(true);
			SetCaption(toggle.gameObject, item.DisplayName);
			toggle.SetIsOnWithoutNotify(item.GetBoolValue?.Invoke() ?? false);
			toggle.onValueChanged.RemoveAllListeners();
			toggle.onValueChanged.AddListener(value => item.SetBoolValue?.Invoke(value));
			_checkBoxes?.Add(toggle);
			_generatedUIElements?.Add(toggle.gameObject);
		}

		private void CreateButtonElement(ButtonElementInfo item, Transform parent)
		{
			if (_buttonPrefab == null)
			{
				return;
			}
			CustomButton button = UnityEngine.Object.Instantiate(_buttonPrefab, parent);
			button.gameObject.SetActive(true);
			SetCaption(button.gameObject, item.DisplayName);
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => item.ButtonAction?.Invoke());
			_generatedUIElements?.Add(button.gameObject);
		}

		private void CreateTextElement(TextElementInfo item, Transform parent)
		{
			if (_textPrefab == null)
			{
				return;
			}
			CustomTextMesh text = UnityEngine.Object.Instantiate(_textPrefab, parent);
			text.gameObject.SetActive(true);
			text.SetText(item.DisplayName);
			_generatedUIElements?.Add(text.gameObject);
		}

		private void CreateToggleParentElement(ToggleParentElementInfo item, Transform parent)
		{
			if (_toggleParentPrefab == null)
			{
				return;
			}
			Transform toggleParent = UnityEngine.Object.Instantiate(_toggleParentPrefab, parent);
			toggleParent.gameObject.SetActive(true);
			SetCaption(toggleParent.gameObject, item.DisplayName);
			_toggleParents[item.ToggleGroupId] = toggleParent;
			if (!_toggleGroups.ContainsKey(item.ToggleGroupId))
			{
				_toggleGroups[item.ToggleGroupId] = new List<CustomToggle>();
			}
			_generatedUIElements?.Add(toggleParent.gameObject);
			_sectionCreationCount++;
		}

		private void CreateToggleElement(ToggleElementInfo item)
		{
			if (_togglePrefab == null || !_toggleParents.TryGetValue(item.ToggleGroupId, out Transform parent))
			{
				return;
			}
			CustomToggle toggle = UnityEngine.Object.Instantiate(_togglePrefab, parent);
			toggle.gameObject.SetActive(true);
			SetCaption(toggle.gameObject, item.DisplayName);
			List<CustomToggle> group = _toggleGroups[item.ToggleGroupId];
			group.Add(toggle);
			bool isSelected = item.LayoutPatternIndex == _selectedLayoutPatternIndexPortrait;
			toggle.SetIsOnWithoutNotify(isSelected);
			toggle.onValueChanged.RemoveAllListeners();
			toggle.onValueChanged.AddListener(isOn =>
			{
				if (!isOn)
				{
					return;
				}
				foreach (CustomToggle groupedToggle in group)
				{
					if (groupedToggle != null && groupedToggle != toggle)
					{
						groupedToggle.SetIsOnWithoutNotify(false);
					}
				}
				_selectedLayoutPatternIndexPortrait = item.LayoutPatternIndex;
				_selectedLayoutPatternIndexLandscape = item.LayoutPatternIndex;
				OnSectionOrderChanged();
			});
			_generatedUIElements?.Add(toggle.gameObject);
		}

		private void UpdateSliderValueDisplay(CustomSlider slider, SliderElementInfo item, CustomTextMesh valueLabel)
		{
			if (slider == null || valueLabel == null)
			{
				return;
			}
			string format = string.IsNullOrEmpty(item.Format) ? (item.ValueType == SliderValueType.Integer ? "N0" : "F2") : item.Format;
			valueLabel.SetText(slider.value.ToString(format));
		}

		public int GetSelectedLayoutPatternIndexPortrait()
		{
			return _selectedLayoutPatternIndexPortrait;
		}

		public int GetSelectedLayoutPatternIndexLandscape()
		{
			return _selectedLayoutPatternIndexLandscape;
		}

		public void CleanupClockMenuPreview()
		{
			// TODO(original): restore clock-menu preview cleanup once the original preview widgets are copied.
		}

		public void SetParentDialog(MusicScoreMakerOptionDialog parentDialog)
		{
			_parentDialog = parentDialog;
		}

		private void OnSectionOrderChanged()
		{
			// The original rebuilds some clock-menu preview blocks here. Rebuilding the whole option UI
			// while a toggle callback is running is noisy in Unity, so keep the selected value only for now.
		}

		public void ClearElements()
		{
			if (_generatedUIElements != null)
			{
				foreach (GameObject element in _generatedUIElements)
				{
					if (element != null)
					{
						UnityEngine.Object.Destroy(element);
					}
				}
				_generatedUIElements.Clear();
			}
			_sliders?.Clear();
			_checkBoxes?.Clear();
			_toggleGroups?.Clear();
			_toggleParents?.Clear();
			_sectionCreationCount = 0;
		}

		private static void SetCaption(GameObject target, string caption)
		{
			CustomTextMesh text = target != null ? target.GetComponentInChildren<CustomTextMesh>(true) : null;
			if (text != null)
			{
				text.SetText(caption);
			}
		}
	}
}
