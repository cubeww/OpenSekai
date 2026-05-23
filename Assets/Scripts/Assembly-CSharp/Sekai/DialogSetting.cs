using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public sealed class DialogSetting : MonoBehaviour
	{
		[SerializeField]
		private DialogSizeFitter sizeFitter;

		[SerializeField]
		private UIPartsDialogTabGroup tabGroup;

		[SerializeField]
		private UIPartsDialogButtonGroup buttonGroup;

		[SerializeField]
		private DialogSize dialogSize;

		[SerializeField]
		private int tabCount;

		[SerializeField]
		private int buttonCount;

		public DialogSizeFitter SizeFitter
		{
			get
			{
				return sizeFitter;
			}
		}

		public CustomIndexToggleGroup ToggleGroup
		{
			get
			{
				return tabGroup != null ? tabGroup.ToggleGroup : null;
			}
		}

		public void Execute()
		{
			SetSize();
			SetTab(tabCount);
			SetButton(buttonCount);
		}

		public void SetSize(DialogSize size)
		{
			dialogSize = size;
			SetSize();
		}

		public void SetSize()
		{
			if (sizeFitter != null)
			{
				sizeFitter.Setup(dialogSize, tabGroup != null && tabCount > 0 ? DialogSizeFitter.ViewType.HasTab : DialogSizeFitter.ViewType.Default);
			}
		}

		public void SetTab(int count)
		{
			tabCount = count;
			if (tabGroup != null)
			{
				tabGroup.Setup(tabCount);
			}
			SetSize();
		}

		public void SetButton(int count)
		{
			buttonCount = count;
			if (buttonGroup != null)
			{
				buttonGroup.Setup(buttonCount);
			}
		}

		public void SetButtonSize(UIPartsCommonButton.SizeEnum size)
		{
			buttonGroup?.SetButtonSize(size);
		}

		public DialogSetting()
		{
		}
	}
}
