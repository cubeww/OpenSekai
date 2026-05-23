using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	[RequireComponent(typeof(CustomToggle))]
	public class UIPartsDialogTab : MonoBehaviour
	{
		[SerializeField]
		private CustomImage backgroundImage;

		[SerializeField]
		private CustomTextMesh text;

		[SerializeField]
		private GameObject badge;

		[SerializeField]
		private GameObject lineObj;

		private bool isOn;

		private CustomToggle toggle;

		private Action onValueChanged;

		public bool IsOn
		{
			get
			{
				return toggle != null ? toggle.isOn : isOn;
			}
			set
			{
				isOn = value;
				if (toggle != null)
				{
					toggle.isOn = value;
				}
				if (value)
				{
					On();
				}
				else
				{
					Off();
				}
			}
		}

		public bool IsShowBadge
		{
			get
			{
				return badge != null && badge.activeSelf;
			}
			set
			{
				if (badge != null)
				{
					badge.SetActive(value);
				}
			}
		}

		protected void Awake()
		{
			toggle = GetComponent<CustomToggle>();
		}

		protected void OnEnable()
		{
			if (toggle == null)
			{
				toggle = GetComponent<CustomToggle>();
			}
			if (toggle != null)
			{
				toggle.onValueChanged.RemoveListener(Change);
				toggle.onValueChanged.AddListener(Change);
				Change(toggle.isOn);
			}
		}

		protected void OnDisable()
		{
			if (toggle != null)
			{
				toggle.onValueChanged.RemoveListener(Change);
			}
		}

		private void On()
		{
			isOn = true;
			if (backgroundImage != null)
			{
				backgroundImage.color = Color.white;
			}
			if (text != null)
			{
				text.color = Color.white;
			}
			ShowLine();
		}

		private void Off()
		{
			isOn = false;
			if (backgroundImage != null)
			{
				backgroundImage.color = new Color(1f, 1f, 1f, 0.65f);
			}
			if (text != null)
			{
				text.color = new Color(1f, 1f, 1f, 0.65f);
			}
			HideLine();
		}

		public void Setup(Action onValueChanged)
		{
			this.onValueChanged = onValueChanged;
			if (toggle == null)
			{
				toggle = GetComponent<CustomToggle>();
			}
			if (toggle != null)
			{
				toggle.onValueChanged.RemoveListener(Change);
				toggle.onValueChanged.AddListener(Change);
			}
		}

		private void Change(bool isOn)
		{
			if (isOn)
			{
				On();
			}
			else
			{
				Off();
			}
			onValueChanged?.Invoke();
		}

		public void ShowLine()
		{
			if (lineObj != null)
			{
				lineObj.SetActive(true);
			}
		}

		public void HideLine()
		{
			if (lineObj != null)
			{
				lineObj.SetActive(false);
			}
		}

		public UIPartsDialogTab()
		{
		}
	}
}
