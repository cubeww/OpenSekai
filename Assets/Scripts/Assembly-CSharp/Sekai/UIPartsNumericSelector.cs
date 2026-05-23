using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsNumericSelector : MonoBehaviour
	{
		[SerializeField]
		private CustomTextMesh _numText;

		[SerializeField]
		private CustomButton[] _decrementButtons;

		[SerializeField]
		private CustomTextMesh[] _decrementButtonTexts;

		[SerializeField]
		private CustomButton[] _incrementButtons;

		[SerializeField]
		private CustomTextMesh[] _incrementButtonTexts;

		private decimal _value;

		private int _minValue;

		private int _maxValue;

		private string _format;

		private string _stringFormat;

		private Action _onUpdateAction;

		private const decimal Threshold = 0.0001m;

		public float Value
		{
			get
			{
				return (float)_value;
			}
		}

		public void Setup(float value, int minValue, int maxValue, float[] variationValues, string format = "", string stringFormat = "", Action onUpdateAction = null)
		{
			SetupParams(value, minValue, maxValue, format, stringFormat, onUpdateAction);
			if (variationValues == null || variationValues.Length == 0)
			{
				return;
			}
			for (int i = 0; i < variationValues.Length; i++)
			{
				SetupButton(i, variationValues[i]);
			}
		}

		public void Setup(float value, int minValue, int maxValue, float variationValue, string format = "", string stringFormat = "", Action onUpdateAction = null)
		{
			SetupParams(value, minValue, maxValue, format, stringFormat, onUpdateAction);
			SetupButton(0, variationValue);
		}

		private void SetupParams(float value, int minValue, int maxValue, string format = "", string stringFormat = "", Action onUpdateAction = null)
		{
			_minValue = minValue;
			_maxValue = Math.Max(minValue, maxValue);
			_format = format ?? string.Empty;
			_stringFormat = stringFormat ?? string.Empty;
			_onUpdateAction = onUpdateAction;
			UpdateValue((decimal)value);
		}

		private void SetupButton(int i, float variationValue)
		{
			decimal variation = (decimal)variationValue;
			if (_decrementButtons != null && i >= 0 && i < _decrementButtons.Length && _decrementButtons[i] != null)
			{
				_decrementButtons[i].onClick.RemoveAllListeners();
				_decrementButtons[i].onClick.AddListener(() => OnClickDecrement(variation));
			}
			if (_incrementButtons != null && i >= 0 && i < _incrementButtons.Length && _incrementButtons[i] != null)
			{
				_incrementButtons[i].onClick.RemoveAllListeners();
				_incrementButtons[i].onClick.AddListener(() => OnClickIncrement(variation));
			}
			string display = variationValue.ToString(string.IsNullOrEmpty(_format) ? "0.####" : _format);
			if (_decrementButtonTexts != null && i >= 0 && i < _decrementButtonTexts.Length && _decrementButtonTexts[i] != null)
			{
				_decrementButtonTexts[i].text = "-" + display;
			}
			if (_incrementButtonTexts != null && i >= 0 && i < _incrementButtonTexts.Length && _incrementButtonTexts[i] != null)
			{
				_incrementButtonTexts[i].text = "+" + display;
			}
		}

		public void UpdateValue(float value)
		{
			UpdateValue((decimal)value);
		}

		public void UpdateValue(decimal value)
		{
			decimal clamped = Math.Min(_maxValue, Math.Max(_minValue, value));
			if (Math.Abs(_value - clamped) < Threshold)
			{
				_value = clamped;
			}
			else
			{
				_value = clamped;
				_onUpdateAction?.Invoke();
			}
			if (_numText != null)
			{
				string formatted = string.IsNullOrEmpty(_format) ? ((float)_value).ToString("0.####") : ((float)_value).ToString(_format);
				if (string.IsNullOrEmpty(_stringFormat))
				{
					_numText.text = formatted;
				}
				else
				{
					_numText.text = string.Format(_stringFormat, formatted);
				}
			}
			UpdateButtonEnable();
		}

		private void UpdateButtonEnable()
		{
			bool canDecrement = _value > _minValue;
			bool canIncrement = _value < _maxValue;
			if (_decrementButtons != null)
			{
				foreach (CustomButton button in _decrementButtons)
				{
					if (button != null)
					{
						button.interactable = canDecrement;
					}
				}
			}
			if (_incrementButtons != null)
			{
				foreach (CustomButton button in _incrementButtons)
				{
					if (button != null)
					{
						button.interactable = canIncrement;
					}
				}
			}
		}

		private void OnClickIncrement(decimal increaseValue)
		{
			UpdateValue(_value + increaseValue);
		}

		private void OnClickDecrement(decimal decreaseValue)
		{
			UpdateValue(_value - decreaseValue);
		}

		public UIPartsNumericSelector()
		{
		}

		static UIPartsNumericSelector()
		{
		}
	}
}
