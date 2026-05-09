using System;
using UnityEngine;

namespace Sekai.UI
{
    public class UIPartsCommonButton : MonoBehaviour
    {
        private enum ButtonTypeEnum
        {
            Main = 0,
            Sub = 1
        }

        public enum DisplayTypeEnum
        {
            Normal = 0,
            Primary = 1,
            Special = 2
        }

        public enum SizeEnum
        {
            S = 0,
            M = 1,
            L = 2
        }

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CustomImage baseImage;
        [SerializeField] private CustomImage coverImage;
        [SerializeField] private CustomButton customButton;
        [SerializeField] private bool adjustButtonSetting;
        [SerializeField] private bool adjustTextSize;
        [SerializeField] private bool changeSprite;
        [SerializeField] private CustomTextMesh customTextMesh;
        [SerializeField] private ButtonTypeEnum buttonType;
        [SerializeField] private DisplayTypeEnum displayType;
        [SerializeField] private SizeEnum size;
        [SerializeField] private CommonButtonTapEffect tapEffect;

        public CustomImage BaseImage => baseImage;
        public CustomButton CustomButton => customButton;

        private void Awake()
        {
            if (customButton == null)
            {
                customButton = GetComponent<CustomButton>();
            }

            if (rectTransform == null)
            {
                rectTransform = transform as RectTransform;
            }

            if (tapEffect != null && customButton != null)
            {
                customButton.SetInteraction(tapEffect);
            }
        }

        public void SetTextSize()
        {
        }

        public void SetTextWording(string wordingKey)
        {
            SetWordingKey(wordingKey);
        }

        public void UpdateSprite(string iconName)
        {
        }

        public void SetGlowCoverColor()
        {
        }

        public void SetImage(DisplayTypeEnum displayTypeEnum)
        {
            displayType = displayTypeEnum;
        }

        public void SetPrimary()
        {
            displayType = DisplayTypeEnum.Primary;
        }

        public void SetSize(SizeEnum newSize)
        {
            size = newSize;
        }

        public void SetText(string value)
        {
            if (customTextMesh != null)
            {
                customTextMesh.SetText(value);
            }
        }

        public void SetDefaultFontDB()
        {
            customTextMesh?.SetDefaultFontDB();
        }

        public void SetDefaultFontEB()
        {
            customTextMesh?.SetDefaultFontEB();
        }

        public void SetWordingKey(string wordingKey)
        {
            if (customTextMesh != null)
            {
                customTextMesh.SetWordingText(wordingKey);
            }
        }

        public void RemoveAllAndAddListener(Action action)
        {
            if (customButton == null)
            {
                customButton = GetComponent<CustomButton>();
            }

            if (customButton == null)
            {
                return;
            }

            customButton.onClick.RemoveAllListeners();
            customButton.onClick.AddListener(() => action?.Invoke());
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
