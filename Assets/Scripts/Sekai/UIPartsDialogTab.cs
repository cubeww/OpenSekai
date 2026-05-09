using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
    public class UIPartsDialogTab : MonoBehaviour
    {
        [SerializeField] private CustomImage backgroundImage;
        [SerializeField] private CustomTextMesh text;
        [SerializeField] private GameObject badge;
        [SerializeField] private GameObject lineObj;

        private bool isOn;
        private CustomToggle toggle;
        private Action onValueChanged;

        public bool IsOn
        {
            get { return toggle != null ? toggle.isOn : isOn; }
            set
            {
                isOn = value;
                if (toggle != null)
                {
                    toggle.isOn = value;
                }

                Change(value);
            }
        }

        public bool IsShowBadge
        {
            get { return badge != null && badge.activeSelf; }
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
            if (toggle != null)
            {
                toggle.onValueChanged.AddListener(Change);
            }
            Change(IsOn);
        }

        protected void OnEnable()
        {
            Change(IsOn);
        }

        protected void OnDisable()
        {
        }

        public void Setup(Action onValueChanged)
        {
            this.onValueChanged = onValueChanged;
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

        private void Change(bool value)
        {
            isOn = value;
            if (lineObj != null)
            {
                lineObj.SetActive(value);
            }

            if (backgroundImage != null)
            {
                backgroundImage.Alpha = value ? 1f : 0.5f;
            }

            onValueChanged?.Invoke();
        }
    }
}
