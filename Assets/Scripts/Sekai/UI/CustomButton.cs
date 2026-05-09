using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.UI
{
    public class CustomButton : Button
    {
        public enum ShapeType
        {
            Manual = 0,
            Rect_64_White = 1,
            Rect_104_Green = 2,
            Rect_104_White = 3,
            Square_112_Green = 4,
            Square_112_White = 5,
            Circle_72_White = 6,
            Circle_128_White = 7
        }

        public enum DisableActionType
        {
            None = 0,
            Grayout = 1
        }

        private const float DefaultPressScale = 0.95f;

        [SerializeField] protected SeType se;
        [SerializeField] protected string otherSeName;
        [SerializeField] protected InputManager.IntervalUseType interval;
        [SerializeField] private bool absolutelyPress;
        [SerializeField] private bool enableLongPress;
        [SerializeField] private ButtonViewInteractionBase buttonViewInteraction;
        [SerializeField] private ShapeType shape;
        [SerializeField] private CustomImage shapeButtonImage;
        [SerializeField] private CustomImage shapeRectDotImage;
        [SerializeField] private CustomText shapeRectText;
        [SerializeField] protected DisableActionType disableActionType;
        [SerializeField] private Graphic coverImage;
        [SerializeField] private List<Graphic> optionalCoverImages;
        [SerializeField] protected float pressScale = DefaultPressScale;

        public Action OnLongPressEvent;
        public Action<Transform> OnLongPressTransformEvent;
        public Action OnHoldPressEvent;
        public Action OnPointerPress;

        private Vector3 baseScale;

        public SeType SE
        {
            get { return se; }
            set { se = value; }
        }

        public ShapeType Shape
        {
            get { return shape; }
            set { shape = value; }
        }

        public CustomImage ShapeButtonImage
        {
            get { return shapeButtonImage; }
            set { shapeButtonImage = value; }
        }

        public CustomImage ShapeRectDotImage
        {
            get { return shapeRectDotImage; }
            set { shapeRectDotImage = value; }
        }

        public CustomText ShapeRectText
        {
            get { return shapeRectText; }
            set { shapeRectText = value; }
        }

        public Graphic CoverImage
        {
            get { return coverImage; }
            set { coverImage = value; }
        }

        public Vector3 BaseScale { get; set; }

        public bool EnableLongPress
        {
            get { return enableLongPress; }
            set { enableLongPress = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            baseScale = transform.localScale;
            BaseScale = baseScale;
            HideCover();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ResetScale();
            HideCover();
        }

        protected override void OnDisable()
        {
            ResetScale();
            base.OnDisable();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (!IsActive() || !IsInteractable())
            {
                return;
            }

            OnPointerPress?.Invoke();
            buttonViewInteraction?.OnPressed();
            ScalePointerDown();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            buttonViewInteraction?.OnReleased();
            ResetScale();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            buttonViewInteraction?.OnReleased();
            ResetScale();
        }

        public void ShowCover()
        {
            SetCoverActive(true);
        }

        public void HideCover()
        {
            SetCoverActive(false);
        }

        public void ResetScale()
        {
            Vector3 targetScale = BaseScale == Vector3.zero ? baseScale : BaseScale;
            if (targetScale != Vector3.zero)
            {
                transform.localScale = targetScale;
            }
        }

        public void ScalePointerDown()
        {
            Vector3 targetScale = BaseScale == Vector3.zero ? baseScale : BaseScale;
            transform.localScale = targetScale * (pressScale > 0f ? pressScale : DefaultPressScale);
        }

        public void SetPressScaleRatio(float ratio)
        {
            pressScale = ratio;
        }

        public void SetInterval(InputManager.IntervalUseType intervalType)
        {
            interval = intervalType;
        }

        public void ChangeOtherSE(string seName)
        {
            otherSeName = seName;
        }

        public void SetInteraction(ButtonViewInteractionBase interaction)
        {
            buttonViewInteraction = interaction;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        private void SetCoverActive(bool active)
        {
            if (coverImage != null)
            {
                coverImage.gameObject.SetActive(active);
            }

            if (optionalCoverImages == null)
            {
                return;
            }

            for (int i = 0; i < optionalCoverImages.Count; i++)
            {
                if (optionalCoverImages[i] != null)
                {
                    optionalCoverImages[i].gameObject.SetActive(active);
                }
            }
        }
    }
}
