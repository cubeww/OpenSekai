using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.UI
{
    public class CustomToggle : Toggle
    {
        public enum DisableActionType
        {
            None = 0,
            Grayout = 1
        }

        [SerializeField] protected SeType se;
        [SerializeField] private InputManager.IntervalUseType interval;
        [SerializeField] private DisableActionType disableActionType;
        [SerializeField] private Graphic coverImage;
        [SerializeField] private List<Graphic> optionalCoverImages;

        public Action onPointerClickAction;

        protected override void Awake()
        {
            base.Awake();
            UpdateCover();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateCover();
        }

        protected override void OnDisable()
        {
            HideCover();
            base.OnDisable();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            onPointerClickAction?.Invoke();
            UpdateCover();
        }

        public void HideCover()
        {
            SetCoverActive(false);
        }

        public void SetOptionalCoverImage(int index)
        {
            if (optionalCoverImages == null)
            {
                return;
            }

            for (int i = 0; i < optionalCoverImages.Count; i++)
            {
                if (optionalCoverImages[i] != null)
                {
                    optionalCoverImages[i].gameObject.SetActive(i == index);
                }
            }
        }

        private void UpdateCover()
        {
            SetCoverActive(disableActionType == DisableActionType.Grayout && !interactable);
        }

        private void SetCoverActive(bool active)
        {
            if (coverImage != null)
            {
                coverImage.gameObject.SetActive(active);
            }
        }
    }
}
