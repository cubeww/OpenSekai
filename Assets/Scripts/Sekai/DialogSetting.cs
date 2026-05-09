using Sekai.UI;
using UnityEngine;

namespace Sekai
{
    public sealed class DialogSetting : MonoBehaviour
    {
        [SerializeField] private DialogSizeFitter sizeFitter;
        [SerializeField] private UIPartsDialogTabGroup tabGroup;
        [SerializeField] private UIPartsDialogButtonGroup buttonGroup;
        [SerializeField] private DialogSize dialogSize;
        [SerializeField] private int tabCount;
        [SerializeField] private int buttonCount;

        public DialogSizeFitter SizeFitter => sizeFitter;
        public CustomIndexToggleGroup ToggleGroup => tabGroup != null ? tabGroup.ToggleGroup : null;

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
            sizeFitter?.Setup(dialogSize);
        }

        public void SetTab(int count)
        {
            tabCount = count;
            tabGroup?.Setup(count);
        }

        public void SetButton(int count)
        {
            buttonCount = count;
            buttonGroup?.Setup(count);
        }

        public void SetButtonSize(UIPartsCommonButton.SizeEnum size)
        {
            buttonGroup?.SetButtonSize(size);
        }
    }
}
