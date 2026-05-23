using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai
{
	[RequireComponent(typeof(HorizontalLayoutGroup))]
	public sealed class UIPartsDialogButtonGroup : MonoBehaviour
	{
		[SerializeField]
		private HorizontalLayoutGroup layoutGroup;

		[SerializeField]
		private UIPartsCommonButton buttonPrefab;

		public void Setup(int buttonCount)
		{
			// Dialog prefabs already contain their concrete footer buttons; this keeps
			// those children usable and only instantiates from the prefab when needed.
			if (layoutGroup == null)
			{
				layoutGroup = GetComponent<HorizontalLayoutGroup>();
			}
			if (buttonPrefab == null)
			{
				return;
			}

			buttonCount = Mathf.Max(0, buttonCount);
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				Transform child = transform.GetChild(i);
				if (child != null && child.GetComponent<UIPartsCommonButton>() != null)
				{
					child.gameObject.SetActive(i < buttonCount);
				}
			}

			for (int i = transform.childCount; i < buttonCount; i++)
			{
				Instantiate(buttonPrefab, transform, false);
			}
		}

		public void SetButtonSize(UIPartsCommonButton.SizeEnum size)
		{
			foreach (UIPartsCommonButton button in GetComponentsInChildren<UIPartsCommonButton>(true))
			{
				if (button != null)
				{
					button.SetSize(size);
				}
			}
		}

		public UIPartsDialogButtonGroup()
		{
		}
	}
}
