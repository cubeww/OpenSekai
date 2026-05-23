using System;
using System.Collections.Generic;
using Sekai.UI;
using TMPro;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame
{
	public class MusicScoreMakerCustomQuantizeDialog : Common2ButtonDialog
	{
		[SerializeField]
		private CustomInputFieldTextMesh _quantizeDivisionInputField;

		[SerializeField]
		private UIPartsNumericSelector _quantizeDivisionSelector;

		[SerializeField]
		private CustomTextMesh _resultText;

		private int _baseNoteDivision;

		private int _splitCount;

		private readonly List<CustomButton> _presetButtons;

		private int _maxDivision;

		private static readonly float[] VARIATION_VALUES;

		public void Setup(int currentDivision)
		{
			_maxDivision = 1920;
			CalculateBaseNoteAndSplit(currentDivision);
			if (_quantizeDivisionInputField != null)
			{
				_quantizeDivisionInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
				_quantizeDivisionInputField.characterLimit = 3;
				_quantizeDivisionInputField.text = _splitCount.ToString();
				_quantizeDivisionInputField.onValueChanged.AddListener(OnInputValueChanged);
				_quantizeDivisionInputField.onEndEdit.AddListener(OnQuantizeDivisionInputEndEdit);
			}
			if (_quantizeDivisionSelector != null)
			{
				_quantizeDivisionSelector.Setup(_splitCount, 1, _maxDivision / _baseNoteDivision, VARIATION_VALUES, string.Empty, "{0}", OnSelectorValueChanged);
			}
			UpdateResultText();
		}

		private void CalculateBaseNoteAndSplit(int division)
		{
			_baseNoteDivision = 4;
			if (division >= _maxDivision + 1)
			{
				_splitCount = _maxDivision / _baseNoteDivision;
				return;
			}

			_splitCount = division <= 3 ? 1 : division / _baseNoteDivision;
		}

		private void OnInputValueChanged(string value)
		{
			if (!int.TryParse(value, out int result))
			{
				return;
			}
			int maxSplit = _maxDivision / _baseNoteDivision;
			UpdateSplitCount(Math.Max(1, Math.Min(result, maxSplit)));
		}

		private void OnQuantizeDivisionInputEndEdit(string value)
		{
			if (!int.TryParse(value, out int result))
			{
				result = 1;
			}
			int maxSplit = _maxDivision / _baseNoteDivision;
			UpdateSplitCount(Math.Max(1, Math.Min(result, maxSplit)));
		}

		private void OnSelectorValueChanged()
		{
			if (_quantizeDivisionSelector != null)
			{
				UpdateSplitCount((int)_quantizeDivisionSelector.Value);
			}
		}

		private void UpdateSplitCount(int splitCount)
		{
			if (splitCount < 1)
			{
				return;
			}
			_splitCount = Math.Min(splitCount, _maxDivision / _baseNoteDivision);
			SyncInputFields(_splitCount);
			UpdateResultText();
		}

		private void SyncInputFields(int splitCount)
		{
			if (_quantizeDivisionInputField != null)
			{
				_quantizeDivisionInputField.SetTextWithoutNotify(splitCount.ToString());
			}
			if (_quantizeDivisionSelector != null && (int)_quantizeDivisionSelector.Value != splitCount)
			{
				_quantizeDivisionSelector.UpdateValue((float)splitCount);
			}
		}

		public int GetInputDivision()
		{
			int splitCount = _splitCount;
			if (_quantizeDivisionSelector != null)
			{
				splitCount = (int)_quantizeDivisionSelector.Value;
			}
			else if (_quantizeDivisionInputField != null && int.TryParse(_quantizeDivisionInputField.text, out int inputSplitCount))
			{
				splitCount = inputSplitCount;
			}
			return Math.Min(_maxDivision, splitCount * _baseNoteDivision);
		}

		private void UpdateResultText()
		{
			if (_resultText != null)
			{
				_resultText.SetWordingText("WORD_QUANTIZE_RESULT_FORMAT", GetInputDivision());
			}
		}

		private void OnDestroy()
		{
			if (_quantizeDivisionInputField != null)
			{
				_quantizeDivisionInputField.onValueChanged.RemoveListener(OnInputValueChanged);
				_quantizeDivisionInputField.onEndEdit.RemoveListener(OnQuantizeDivisionInputEndEdit);
			}
			foreach (CustomButton button in _presetButtons)
			{
				if (button != null)
				{
					button.onClick.RemoveAllListeners();
				}
			}
			_presetButtons.Clear();
		}

		public MusicScoreMakerCustomQuantizeDialog()
		{
			_baseNoteDivision = 4;
			_splitCount = 4;
			_presetButtons = new List<CustomButton>();
		}

		static MusicScoreMakerCustomQuantizeDialog()
		{
			VARIATION_VALUES = new[] { 10f, 1f };
		}
	}
}
