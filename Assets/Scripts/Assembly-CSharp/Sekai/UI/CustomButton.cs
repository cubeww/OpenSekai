using System;
using System.Collections.Generic;
using System.Threading;
using Beebyte.Obfuscator;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.UI
{
	[AddComponentMenu("Custom UI/Custom Button")]
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

		private const float PRESS_SCALE_RATIO = 0.95f;
		private const float PRESS_SCALE_DURATION = 0.08f;
		private const float RELEASE_SCALE_DURATION = 0.12f;
		private const float LONG_PRESS_SEC = 0.4f;

		[SerializeField]
		protected SeType se = SeType.Decide;

		[SerializeField]
		protected string otherSeName;

		[SerializeField]
		protected InputManager.IntervalUseType interval = InputManager.IntervalUseType.Use;

		[SerializeField]
		private bool absolutelyPress;

		[SerializeField]
		private bool enableLongPress;

		[SerializeField]
		private ButtonViewInteractionBase buttonViewInteraction;

		[SerializeField]
		private ShapeType shape;

		[SerializeField]
		private CustomImage shapeButtonImage;

		[SerializeField]
		private CustomImage shapeRectDotImage;

		[SerializeField]
		private CustomText shapeRectText;

		[SerializeField]
		protected DisableActionType disableActionType = DisableActionType.Grayout;

		[SerializeField]
		private Graphic coverImage;

		[SerializeField]
		private List<Graphic> optionalCoverImages;

		[SerializeField]
		protected float pressScale = PRESS_SCALE_RATIO;

		protected Tweener pressTween;

		public Action OnLongPressEvent;

		private bool executedLongPress;

		public Action OnHoldPressEvent;

		public Action OnPressInteractionOverride;

		public Action OnReleaseInteractionOverride;

		private InputManager.ControlState controlState;

		private CancellationTokenSource holdRepeatCts;

		private CancellationTokenSource longPressCts;

		[SerializeField]
		private bool enableHoldRepeat;

		[SerializeField]
		private float holdRepeatInterval = 0.2f;

		private bool isHolding;

		private float holdTimer;

		public Action OnHoldRepeatEvent { get; set; }

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

		public bool EnableHoldRepeat
		{
			get { return enableHoldRepeat; }
			set { enableHoldRepeat = value; }
		}

		public float HoldRepeatInterval
		{
			get { return holdRepeatInterval; }
			set { holdRepeatInterval = value; }
		}

		protected override void Awake()
		{
			base.Awake();
			BaseScale = transform.localScale;
			// AssetRipper kept the TapEffect objects, but some restored prefabs were
			// serialized while CustomButton was still a stub and lost this field.
			buttonViewInteraction ??= GetComponentInChildren<ButtonViewInteractionBase>(true);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			HideCover();
			buttonViewInteraction?.OnButtonViewEnabled();
			buttonViewInteraction?.ResetInteraction();
			ResetScale();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			StopHoldRepeat();
			StopLongPressCheck();
			if (disableActionType == DisableActionType.Grayout)
			{
				ShowCover();
			}
			buttonViewInteraction?.OnButtonViewDisabled();
			if (controlState == InputManager.ControlState.Press)
			{
				OnFinishedControlSelectable();
			}
			else
			{
				controlState = InputManager.ControlState.NoControl;
			}
			buttonViewInteraction?.ResetInteraction();
		}

		private void OnFinishedControlSelectable()
		{
			if (controlState == InputManager.ControlState.Press)
			{
				PlayReleaseEffect();
			}
			controlState = InputManager.ControlState.NoControl;
		}

		public void SetHoldRepeatEvent(Action action)
		{
			OnHoldRepeatEvent = action;
		}

		public void StopHoldRepeat()
		{
			isHolding = false;
			holdTimer = 0f;
			if (holdRepeatCts != null)
			{
				holdRepeatCts.Cancel();
				holdRepeatCts.Dispose();
				holdRepeatCts = null;
			}
		}

		[Skip]
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (!executedLongPress || absolutelyPress)
			{
				if (CustomSelectableDefine.CheckPointerClickAction(this, interval, se, otherSeName))
				{
					base.OnPointerClick(eventData);
				}
			}
			executedLongPress = false;
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			if (!IsInteractable())
			{
				return;
			}

			controlState = InputManager.ControlState.Press;
			CustomPointerDown();
			PlayPressEffect();
			StartLongPressCheck();
			StartHoldRepeat();
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			CustomPointerUp();
			StopLongPressCheck();
			StopHoldRepeat();
			if (controlState == InputManager.ControlState.Press)
			{
				PlayReleaseEffect();
			}
			controlState = InputManager.ControlState.ClickCheck;
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			base.OnPointerExit(eventData);
			StopLongPressCheck();
			StopHoldRepeat();
			if (controlState == InputManager.ControlState.Press)
			{
				PlayReleaseEffect();
			}
			controlState = InputManager.ControlState.NoControl;
		}

		public void ShowCover()
		{
			SetCoverActive(true);
		}

		public void HideCover()
		{
			SetCoverActive(false);
		}

		private void SetCoverActive(bool isActive)
		{
			Color color = global::Sekai.ColorUtility.COLOR_DISABLE_ALPHA_50;
			if (coverImage != null)
			{
				coverImage.color = color;
				coverImage.gameObject.SetActive(isActive);
			}
			if (optionalCoverImages == null)
			{
				return;
			}
			for (int i = 0; i < optionalCoverImages.Count; i++)
			{
				Graphic graphic = optionalCoverImages[i];
				if (graphic != null)
				{
					graphic.color = color;
					graphic.gameObject.SetActive(isActive);
				}
			}
		}

		private void PlayPressEffect()
		{
			if (OnPressInteractionOverride != null)
			{
				OnPressInteractionOverride();
				return;
			}
			if (buttonViewInteraction != null)
			{
				buttonViewInteraction.OnPressed();
			}
		}

		private void PlayReleaseEffect()
		{
			if (OnReleaseInteractionOverride != null)
			{
				OnReleaseInteractionOverride();
				return;
			}
			if (buttonViewInteraction != null)
			{
				buttonViewInteraction.OnReleased();
			}
		}

		private void StartLongPressCheck()
		{
			if (!enableLongPress)
			{
				return;
			}

			StopLongPressCheck();
			executedLongPress = false;
			longPressCts = new CancellationTokenSource();
			LongPressUniTask(longPressCts.Token).Forget();
		}

		private async UniTask LongPressUniTask(CancellationToken token)
		{
			try
			{
				await UniTask.Delay(TimeSpan.FromSeconds(LONG_PRESS_SEC), cancellationToken: token);
				if (token.IsCancellationRequested)
				{
					return;
				}
				executedLongPress = true;
				if (OnLongPressEvent != null)
				{
					SoundManager.Instance.PlaySEOneShot("SE_DECIDE1", 0);
					OnLongPressEvent.Invoke();
				}
				if (OnHoldPressEvent != null)
				{
					CustomSelectableDefine.PlaySE(se, otherSeName);
					OnHoldPressEvent.Invoke();
				}
			}
			catch (OperationCanceledException)
			{
			}
		}

		private void StopLongPressCheck()
		{
			if (longPressCts != null)
			{
				longPressCts.Cancel();
				longPressCts.Dispose();
				longPressCts = null;
			}
		}

		private void StartHoldRepeat()
		{
			if (!enableHoldRepeat || OnHoldRepeatEvent == null)
			{
				return;
			}

			StopHoldRepeat();
			isHolding = true;
			holdTimer = 0f;
			holdRepeatCts = new CancellationTokenSource();
			HoldRepeatUniTask(holdRepeatCts.Token).Forget();
		}

		private async UniTask HoldRepeatUniTask(CancellationToken token)
		{
			try
			{
				while (!token.IsCancellationRequested)
				{
					float intervalSec = Mathf.Max(0.01f, holdRepeatInterval);
					await UniTask.Delay(TimeSpan.FromSeconds(intervalSec), cancellationToken: token);
					holdTimer += intervalSec;
					if (isHolding && !token.IsCancellationRequested)
					{
						OnHoldRepeatEvent?.Invoke();
					}
				}
			}
			catch (OperationCanceledException)
			{
			}
		}

		public void ResetScale()
		{
			KillPressTween();
			transform.localScale = BaseScale == Vector3.zero ? Vector3.one : BaseScale;
		}

		private void ResetScaleAnimated()
		{
			KillPressTween();
			Vector3 targetScale = BaseScale == Vector3.zero ? Vector3.one : BaseScale;
			pressTween = transform.DOScale(targetScale, RELEASE_SCALE_DURATION).SetEase(Ease.OutQuart);
		}

		public void ScalePointerDown()
		{
			KillPressTween();
			Vector3 baseScale = BaseScale == Vector3.zero ? Vector3.one : BaseScale;
			pressTween = transform.DOScale(baseScale * pressScale, PRESS_SCALE_DURATION).SetEase(Ease.OutQuart);
		}

		public void SetPressScaleRatio(float ratio)
		{
			pressScale = ratio;
		}

		protected virtual void CustomPointerDown()
		{
		}

		protected virtual void CustomPointerUp()
		{
		}

		protected virtual void KillPressTween()
		{
			if (pressTween != null && pressTween.IsActive())
			{
				pressTween.Kill(false);
			}
			pressTween = null;
			buttonViewInteraction?.KillFadeTween();
		}

		public void SetInterval(InputManager.IntervalUseType intervalType)
		{
			interval = intervalType;
		}

		public void ChangeOtherSE(string seName)
		{
			otherSeName = seName;
			se = string.IsNullOrEmpty(seName) ? SeType.Decide : SeType.Other;
		}

		public void SetInteraction(ButtonViewInteractionBase interaction)
		{
			buttonViewInteraction = interaction;
		}

		public void SetActive(bool isActive)
		{
			gameObject.SetActive(isActive);
		}
	}
}
