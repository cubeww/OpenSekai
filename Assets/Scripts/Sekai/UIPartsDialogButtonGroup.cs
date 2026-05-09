using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai
{
    public sealed class UIPartsDialogButtonGroup : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup layoutGroup;
        [SerializeField] private UIPartsCommonButton buttonPrefab;

        public void Setup(int buttonCount)
        {
            UIPartsCommonButton[] buttons = GetComponentsInChildren<UIPartsCommonButton>(true);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(i < buttonCount);
            }
        }

        public void SetButtonSize(UIPartsCommonButton.SizeEnum size)
        {
            UIPartsCommonButton[] buttons = GetComponentsInChildren<UIPartsCommonButton>(true);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetSize(size);
            }
        }
    }
}
