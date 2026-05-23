using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsCharacterIconAndCheckBox : MonoBehaviour
	{
		[SerializeField]
		private UIPartsCharacterIcon Icon;

		[SerializeField]
		private UIPartsCircleCharacterSDIcon _sdIcon;

		[SerializeField]
		private UIPartsCheckBox CheckBox;

		[SerializeField]
		private int characterId;

		[SerializeField]
		private bool check;

		[SerializeField]
		private CustomImage characterBg;

		[SerializeField]
		private GameObject grayMaskObject;

		public bool Check
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public bool IsCheckBoxEnabled
		{
			get
			{
				throw null;
			}
		}

		public int CharacterId
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public Action OnPointerClickAction
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		private void Awake()
		{
			throw null;
		}

		public void ApplySerializeFieldSetting()
		{
			throw null;
		}

		private void Setup(int charaId, UIPartsCharacterIcon.Size iconSize = UIPartsCharacterIcon.Size.M, bool defaultCheck = false)
		{
			throw null;
		}

		private void OnCheckValue(bool isOn)
		{
			throw null;
		}

		private void SetCheck(bool isOn)
		{
			throw null;
		}

		public void SetGrayFilter(bool interactable)
		{
			throw null;
		}

		public void SetIconGrayMask(bool flag)
		{
			throw null;
		}

		public void SetEnableCheckBox(bool enabled)
		{
			throw null;
		}

		public void SetCheckValueByToggleEnabled()
		{
			throw null;
		}

		public UIPartsCharacterIconAndCheckBox()
		{
			throw null;
		}
	}
}
