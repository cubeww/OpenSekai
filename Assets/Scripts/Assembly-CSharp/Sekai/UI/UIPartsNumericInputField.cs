using System;
using Beebyte.Obfuscator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
	public class UIPartsNumericInputField : TMP_InputField
	{
		[SerializeField]
		private GameObject _inputFieldRoot;

		[SerializeField]
		private Button _guardButton;

		private int _maxValue;

		private int _minValue;

		private Action<int> _onValueChanged;

		private int _inputValue;

		public void Initialize(int max, int min, Action<int> onValueChanged)
		{
			SetEnable(false);
			_maxValue = max;
			_minValue = min;
			_onValueChanged = onValueChanged;
			onSubmit.RemoveAllListeners();
			onEndEdit.RemoveAllListeners();
			onSubmit.AddListener(OnEndEdit);
			onEndEdit.AddListener(OnEndEdit);

			if (_guardButton != null)
			{
				_guardButton.onClick.RemoveAllListeners();
				_guardButton.onClick.AddListener(() => SetEnable(true));
			}
		}

		public void SetNumber(int value)
		{
			_inputValue = Mathf.Clamp(value, _minValue, _maxValue);
			text = _inputValue.ToString();
		}

		[Skip]
		public void OnEndEdit(string str)
		{
			if (!string.IsNullOrEmpty(str) && int.TryParse(str, out int parsed))
			{
				_inputValue = Mathf.Clamp(parsed, _minValue, _maxValue);
			}

			text = _inputValue.ToString();
			_onValueChanged?.Invoke(_inputValue);
			SetEnable(false);
		}

		public void SetActive(bool active)
		{
			if (_inputFieldRoot != null)
			{
				_inputFieldRoot.SetActive(active);
			}
			else
			{
				gameObject.SetActive(active);
			}
		}

		private void SetEnable(bool enable)
		{
			if (_guardButton != null)
			{
				_guardButton.gameObject.SetActive(!enable);
			}

			interactable = enable;
			if (enable)
			{
				ActivateInputField();
			}
		}

		public UIPartsNumericInputField()
		{
		}
	}
}
