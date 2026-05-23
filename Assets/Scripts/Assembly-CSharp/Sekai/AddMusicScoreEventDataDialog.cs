using System;
using System.Collections.Generic;
using System.Globalization;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.UI;
using TMPro;
using UnityEngine;

namespace Sekai
{
	public class AddMusicScoreEventDataDialog : Common2ButtonDialog
	{
		private struct BpmValueParseResult
		{
			public int DotIndex;

			public bool HasDecimal;

			public int IntegerLength;

			public int DecimalLength;
		}

		private const int MAX_BPM_INTEGER_DIGITS = 3;

		private const int MAX_BPM_DECIMAL_DIGITS = 3;

		private const int MIN_BPM_VALUE = 1;

		private const string MIN_BPM_VALUE_STRING = "1";

		private const int MAX_BPM_VALUE = 999;

		private const string MAX_BPM_VALUE_STRING = "999";

		private const string DECIMAL_POINT = ".";

		private const int MAX_BPM_CHARACTER_LIMIT = 7;

		private const string DEFAULT_BPM_VALUE = "120";

		private const string DEFAULT_HIGH_SPEED_VALUE = "1.0";

		private const string DEFAULT_TIME_SIGNATURE_VALUE = "4";

		private const int MIN_TIME_SIGNATURE_NUMERATOR = 1;

		private const int MAX_TIME_SIGNATURE_NUMERATOR = 99;

		private const int TIME_SIGNATURE_NUMERATOR_CHARACTER_LIMIT = 2;

		[SerializeField]
		private UIPartsNumericInputField _inputField;

		[SerializeField]
		private CustomTextMesh _slashText;

		[SerializeField]
		private CustomDropdown _dropdown;

		[SerializeField]
		private CustomButton _deleteButton;

		private Action _onClickDelete;

		private MusicScoreEventType _currentEventType;

		public string InputFieldText
		{
			get
			{
				return _inputField != null ? _inputField.text : string.Empty;
			}
		}

		public string DropdownText
		{
			get
			{
				if (_dropdown == null || _dropdown.options == null || _dropdown.options.Count == 0)
				{
					return string.Empty;
				}
				int index = Mathf.Clamp(_dropdown.value, 0, _dropdown.options.Count - 1);
				return _dropdown.options[index].text;
			}
		}

		public void ShowDeleteButton(Action onClickDelete)
		{
			_onClickDelete = onClickDelete;
			if (_deleteButton == null)
			{
				return;
			}
			_deleteButton.gameObject.SetActive(true);
			_deleteButton.onClick.RemoveAllListeners();
			_deleteButton.onClick.AddListener(OnClickDelete);
		}

		public void HideDeleteButton()
		{
			if (_deleteButton != null)
			{
				_deleteButton.gameObject.SetActive(false);
				_deleteButton.onClick.RemoveAllListeners();
			}
			_onClickDelete = null;
		}

		private void OnClickDelete()
		{
			Action onClickDelete = _onClickDelete;
			_onClickDelete = null;
			onClickDelete?.Invoke();
			Close();
		}

		private static string FormatBpmForInput(decimal bpm)
		{
			if (bpm <= 0m)
			{
				return MIN_BPM_VALUE_STRING;
			}
			if (bpm >= MAX_BPM_VALUE)
			{
				return MAX_BPM_VALUE_STRING;
			}
			decimal rounded = decimal.Round(bpm, MAX_BPM_DECIMAL_DIGITS, MidpointRounding.AwayFromZero);
			return rounded.ToString("0.###", CultureInfo.InvariantCulture);
		}

		private static int TimeSignatureDenominatorToDropdownIndex(int denominator)
		{
			return denominator switch
			{
				2 => 0,
				4 => 1,
				8 => 2,
				16 => 3,
				32 => 4,
				64 => 5,
				_ => 1
			};
		}

		public void Setup(MusicScoreEventType musicScoreEventType, decimal? initialBpm = null, float? initialHighSpeed = null, (int numerator, int denominator)? initialTimeSignature = null)
		{
			_currentEventType = musicScoreEventType;
			if (_inputField == null)
			{
				return;
			}

			_inputField.onValueChanged.RemoveAllListeners();
			_inputField.onEndEdit.RemoveAllListeners();
			_inputField.onSubmit.RemoveAllListeners();

			switch (musicScoreEventType)
			{
			case MusicScoreEventType.BPM:
				SetMessageWording("MSG_CHANGE_BPM_AT_TIMING");
				SetupInputField(FormatBpmForInput(initialBpm ?? decimal.Parse(DEFAULT_BPM_VALUE, CultureInfo.InvariantCulture)), TMP_InputField.ContentType.DecimalNumber, MAX_BPM_CHARACTER_LIMIT);
				_inputField.onValueChanged.AddListener(ValidateBpmInput);
				_inputField.onEndEdit.AddListener(OnBpmInputEndEdit);
				SetActive(_slashText, false);
				SetActive(_dropdown, false);
				break;
			case MusicScoreEventType.HighSpeed:
				SetMessageWording("MSG_CHANGE_HIGH_SPEED_AT_TIMING", "指定したタイミングでハイスピードを変更します。");
				SetupInputField(FormatHighSpeedForInput(initialHighSpeed ?? float.Parse(DEFAULT_HIGH_SPEED_VALUE, CultureInfo.InvariantCulture)), TMP_InputField.ContentType.DecimalNumber, 0);
				SetActive(_slashText, false);
				SetActive(_dropdown, false);
				break;
			case MusicScoreEventType.TimeSignature:
				SetMessageWording("MSG_CHANGE_TIME_SIGNATURE_AT_TIMING");
				(int numerator, int denominator) signature = initialTimeSignature ?? (int.Parse(DEFAULT_TIME_SIGNATURE_VALUE, CultureInfo.InvariantCulture), 4);
				signature.numerator = Mathf.Clamp(signature.numerator, MIN_TIME_SIGNATURE_NUMERATOR, MAX_TIME_SIGNATURE_NUMERATOR);
				SetupInputField(signature.numerator.ToString(CultureInfo.InvariantCulture), TMP_InputField.ContentType.IntegerNumber, TIME_SIGNATURE_NUMERATOR_CHARACTER_LIMIT);
				_inputField.onValueChanged.AddListener(ValidateTimeSignatureNumeratorInput);
				_inputField.onEndEdit.AddListener(OnTimeSignatureNumeratorInputEndEdit);
				SetActive(_slashText, true);
				SetupTimeSignatureDropdown(signature.denominator);
				SetActive(_dropdown, true);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(musicScoreEventType), musicScoreEventType, null);
			}

			_inputField.gameObject.SetActive(true);
		}

		private static string FormatHighSpeedForInput(float value)
		{
			return value.ToString("0.0", CultureInfo.InvariantCulture);
		}

		private void OnBpmInputEndEdit(string value)
		{
			if (!decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal bpm)
				&& !decimal.TryParse(value, out bpm))
			{
				bpm = MIN_BPM_VALUE;
			}
			bpm = Math.Min(Math.Max(bpm, MIN_BPM_VALUE), MAX_BPM_VALUE);
			_inputField?.SetTextWithoutNotify(FormatBpmForInput(bpm));
		}

		private void ValidateTimeSignatureNumeratorInput(string value)
		{
			if (!int.TryParse(value, out int parsed))
			{
				return;
			}
			if (parsed < MIN_TIME_SIGNATURE_NUMERATOR)
			{
				_inputField?.SetTextWithoutNotify(MIN_TIME_SIGNATURE_NUMERATOR.ToString(CultureInfo.InvariantCulture));
			}
			else if (parsed > MAX_TIME_SIGNATURE_NUMERATOR)
			{
				_inputField?.SetTextWithoutNotify(MAX_TIME_SIGNATURE_NUMERATOR.ToString(CultureInfo.InvariantCulture));
			}
		}

		private void OnTimeSignatureNumeratorInputEndEdit(string value)
		{
			if (!int.TryParse(value, out int parsed))
			{
				parsed = MIN_TIME_SIGNATURE_NUMERATOR;
			}
			parsed = Mathf.Clamp(parsed, MIN_TIME_SIGNATURE_NUMERATOR, MAX_TIME_SIGNATURE_NUMERATOR);
			_inputField?.SetTextWithoutNotify(parsed.ToString(CultureInfo.InvariantCulture));
		}

		private void ValidateBpmInput(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			BpmValueParseResult parseResult = ParseBpmValue(value);
			bool needsUpdate;
			bool use999;
			ValidateBpmIntegerPart(value, parseResult.IntegerLength, out needsUpdate, out use999);
			bool truncateDecimal = ShouldTruncateDecimalPart(parseResult.DecimalLength);
			if (needsUpdate || truncateDecimal)
			{
				_inputField?.SetTextWithoutNotify(BuildValidatedBpmValue(value, parseResult, use999, truncateDecimal));
			}
		}

		private BpmValueParseResult ParseBpmValue(string value)
		{
			int dotIndex = value.IndexOf(DECIMAL_POINT, StringComparison.Ordinal);
			bool hasDecimal = dotIndex >= 0;
			int integerLength = hasDecimal ? dotIndex : value.Length;
			int decimalLength = hasDecimal ? value.Length - dotIndex - 1 : 0;
			return new BpmValueParseResult
			{
				DotIndex = dotIndex,
				HasDecimal = hasDecimal,
				IntegerLength = integerLength,
				DecimalLength = decimalLength
			};
		}

		private void ValidateBpmIntegerPart(string value, int integerLength, out bool needsUpdate, out bool use999)
		{
			needsUpdate = false;
			use999 = false;
			if (integerLength >= MAX_BPM_INTEGER_DIGITS + 1)
			{
				needsUpdate = true;
				use999 = ParseIntegerValue(value, MAX_BPM_INTEGER_DIGITS) > MAX_BPM_VALUE;
				return;
			}
			if (integerLength >= 1 && ParseIntegerValue(value, integerLength) > MAX_BPM_VALUE)
			{
				needsUpdate = true;
				use999 = true;
			}
		}

		private int ParseIntegerValue(string value, int length)
		{
			if (string.IsNullOrEmpty(value) || length < 1)
			{
				return 0;
			}
			int result = 0;
			int count = Math.Min(length, value.Length);
			for (int i = 0; i < count; i++)
			{
				char c = value[i];
				if (c >= '0' && c <= '9')
				{
					result = result * 10 + c - '0';
				}
			}
			return result;
		}

		private bool ShouldTruncateDecimalPart(int decimalLength)
		{
			return decimalLength > MAX_BPM_DECIMAL_DIGITS;
		}

		private string BuildValidatedBpmValue(string value, BpmValueParseResult parseResult, bool use999, bool truncateDecimal)
		{
			string integerPart;
			if (use999)
			{
				integerPart = MAX_BPM_VALUE_STRING;
			}
			else if (parseResult.IntegerLength >= MAX_BPM_INTEGER_DIGITS + 1)
			{
				integerPart = value.Substring(0, MAX_BPM_INTEGER_DIGITS);
			}
			else if (parseResult.HasDecimal)
			{
				integerPart = value.Substring(0, parseResult.DotIndex);
			}
			else
			{
				integerPart = value;
			}

			if (!parseResult.HasDecimal || parseResult.DecimalLength < 1)
			{
				return integerPart;
			}
			int decimalLength = truncateDecimal ? MAX_BPM_DECIMAL_DIGITS : parseResult.DecimalLength;
			return integerPart + DECIMAL_POINT + value.Substring(parseResult.DotIndex + 1, decimalLength);
		}

		private void SetupInputField(string text, TMP_InputField.ContentType contentType, int characterLimit)
		{
			_inputField.SetTextWithoutNotify(text);
			_inputField.contentType = contentType;
			_inputField.lineType = TMP_InputField.LineType.SingleLine;
			_inputField.inputType = TMP_InputField.InputType.Standard;
			_inputField.keyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
			_inputField.characterLimit = characterLimit;
		}

		private void SetupTimeSignatureDropdown(int denominator)
		{
			if (_dropdown == null)
			{
				return;
			}
			_dropdown.Setup(new List<string>
			{
				"2",
				"4",
				"8",
				"16",
				"32",
				"64"
			}, TimeSignatureDenominatorToDropdownIndex(denominator), null);
		}

		private void SetMessageWording(string key, string fallback = "")
		{
			if (!string.IsNullOrEmpty(WordingManager.Get(key)))
			{
				SetWordingText(key);
				return;
			}
			SetMessageBodyText(fallback);
		}

		private static void SetActive(Component component, bool active)
		{
			if (component != null)
			{
				component.gameObject.SetActive(active);
			}
		}

		public AddMusicScoreEventDataDialog()
		{
		}
	}
}
