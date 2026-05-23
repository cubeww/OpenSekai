using System;
using Beebyte.Obfuscator;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsCheckBox : MonoBehaviour
	{
		[SerializeField]
		private GameObject checkObj;

		[SerializeField]
		private bool check;

		[SerializeField]
		private CustomToggle toggle;

		public CustomToggle Toggle
		{
			get
			{
				return EnsureToggle();
			}
		}

		public Action<bool> OnCheckValueChanged { get; set; }

		public Action OnPointerClickAction
		{
			get
			{
				CustomToggle customToggle = EnsureToggle();
				return customToggle != null ? customToggle.onPointerClickAction : null;
			}
			set
			{
				CustomToggle customToggle = EnsureToggle();
				if (customToggle != null)
				{
					customToggle.onPointerClickAction = value;
				}
			}
		}

		public bool Check
		{
			get
			{
				return check;
			}
			set
			{
				SetVisual(value);
				CustomToggle customToggle = EnsureToggle();
				if (customToggle != null && customToggle.isOn != check)
				{
					customToggle.isOn = check;
				}
			}
		}

		public bool CheckDefaultValue
		{
			get
			{
				return check;
			}
			set
			{
				SetVisual(value);
				CustomToggle customToggle = EnsureToggle();
				if (customToggle != null)
				{
					customToggle.SetIsOnWithoutNotify(check);
				}
			}
		}

		[Skip]
		public void OnValueChanged(bool isOn)
		{
			CustomToggle customToggle = EnsureToggle();
			Check = customToggle != null ? customToggle.isOn : isOn;
			OnCheckValueChanged?.Invoke(check);
		}

		public void SetEnableToggle(bool enabled)
		{
			CustomToggle customToggle = EnsureToggle();
			if (customToggle != null)
			{
				customToggle.enabled = enabled;
			}
		}

		public void SetCheckValueByToggleEnabled()
		{
			CustomToggle customToggle = EnsureToggle();
			if (customToggle != null)
			{
				check = customToggle.enabled;
			}
		}

		public UIPartsCheckBox()
		{
		}

		private void SetVisual(bool value)
		{
			if (checkObj != null)
			{
				checkObj.SetActive(value);
			}
			check = value;
		}

		private CustomToggle EnsureToggle()
		{
			if (toggle == null)
			{
				toggle = GetComponent<CustomToggle>();
			}
			return toggle;
		}
	}
}
