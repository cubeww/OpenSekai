using System;
using CP;
using DG.Tweening;
using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai
{
	public class DialogBase : MonoBehaviour, IBlurTarget
	{
		public enum OpenSE
		{
			Default = 0,
			Reward = 1,
			CostumeCreate = 2,
			RankUp = 3,
			CoicesOpen = 4,
			SubWindowOpen = 5,
			MysekaiRecycling = 6,
			MysekaiGetBlueprint = 7,
			None = 8
		}

		public enum CloseBehavior
		{
			Destroy = 0,
			Sleep = 1
		}

		protected const float DIALOG_OPEN_DURATION = 0.125f;
		protected const string BACKGROUND_COVER_PREFAB_PATH = "UI/UIParts/UIPartsDialogFillCover";

		[SerializeField]
		protected DialogHeader header;

		[SerializeField]
		protected CustomButton closeButton;

		[SerializeField]
		protected GameObject windowObject;

		[SerializeField]
		private bool needBackgroundCover;

		[SerializeField]
		protected OpenSE openSE;

		[SerializeField]
		protected CloseBehavior closeBehavior;

		protected UIPartsFillCover fillCover;
		protected CanvasGroup canvasGroup;
		protected DialogState currentState;
		protected string[] seArray;

		private bool _allowCloseExternal = true;
		private bool _externalCloseAttached;
		private Tween _animationTween;

		public virtual bool EnableBlur => true;
		public DialogHeader Header => header;
		public GameObject WindowRoot => windowObject;
		public DialogState CurrentState => currentState;
		public bool IsChain { get; set; }

		// OpenSekai uses this property as the ScreenManager close hook. The original
		// class has close/open events plus virtual callbacks; this keeps that bridge.
		public Action OnClosed { get; set; }
		public event Action OnClose;
		public event Action OnOpen;
		public event Action OnOpenPreprocess;

		protected virtual void Awake()
		{
			AwakeProcess();
		}

		protected virtual void Update()
		{
			ExecuteBackKeyProcess();
		}

		protected virtual void AwakeProcess()
		{
			canvasGroup = GetComponent<CanvasGroup>();
			if (canvasGroup == null)
			{
				canvasGroup = gameObject.AddComponent<CanvasGroup>();
			}
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
			SetupBackgroundGrayoutCover();
			InitializeAnimation();
			currentState = DialogState.Instantiated;
		}

		public virtual void Initialize(DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true)
		{
			SetupDialogSize(dialogSize);
			SetActiveBackground(false);
			currentState = DialogState.Initialized;
			SetCloseExternal(allowCloseExternal);
		}

		public virtual void Open()
		{
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}

			OpenPreprocess();
			OpenAnimation();
			currentState = DialogState.PlayOpenAnimation;
			PlayOpenSE();
		}

		public virtual void Close()
		{
			if (currentState == DialogState.PlayCloseAnimation || currentState == DialogState.Closed)
			{
				return;
			}

			currentState = DialogState.PlayCloseAnimation;
			CloseAnimation();
		}

		public virtual void CloseKeepObject()
		{
			if (currentState == DialogState.PlayCloseAnimation || currentState == DialogState.Closed)
			{
				return;
			}

			currentState = DialogState.PlayCloseAnimation;
			CloseAnimationNotMyselfDestroy();
		}

		public virtual void Wakeup()
		{
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}

			Open();
		}

		public virtual void OpenFromChain()
		{
			IsChain = true;
			Open();
		}

		protected virtual void InitializeAnimation()
		{
			if (windowObject != null)
			{
				windowObject.transform.localScale = Vector3.zero;
			}
		}

		protected virtual void OpenAnimation()
		{
			KillAnimationTween();
			if (windowObject == null)
			{
				OnFinishOpenAnimation();
			}
			else
			{
				_animationTween = windowObject.transform
					.DOScale(1f, DIALOG_OPEN_DURATION)
					.SetEase(Ease.Linear)
					.OnComplete(OnFinishOpenAnimation);
			}

			if (fillCover != null && fillCover.Fader != null)
			{
				fillCover.Fader.Play(ColorUtility.COLOR_BASE_DBL_ALPHA_30, 0f, 0.025f, null);
			}
		}

		protected virtual void CloseAnimation()
		{
			KillAnimationTween();
			if (windowObject == null)
			{
				Destroy();
			}
			else
			{
				_animationTween = windowObject.transform
					.DOScale(0f, DIALOG_OPEN_DURATION)
					.SetEase(Ease.Linear)
					.OnComplete(Destroy);
			}

			if (fillCover != null && fillCover.Fader != null)
			{
				fillCover.Fader.Play(ColorUtility.BLACK_ALPHA_0, 0f, 0.025f, null);
			}
		}

		protected virtual void CloseAnimationNotMyselfDestroy()
		{
			KillAnimationTween();
			if (windowObject == null)
			{
				FinishCloseWithoutDestroy();
			}
			else
			{
				_animationTween = windowObject.transform
					.DOScale(0f, DIALOG_OPEN_DURATION)
					.SetEase(Ease.Linear)
					.OnComplete(FinishCloseWithoutDestroy);
			}

			if (fillCover != null && fillCover.Fader != null)
			{
				fillCover.Fader.Play(ColorUtility.BLACK_ALPHA_0, 0f, 0.025f, null);
			}
		}

		protected virtual void OnFinishOpenAnimation()
		{
			currentState = DialogState.Show;
			OnFinishOpenAnimationCallback();
			OnOpen?.Invoke();
			if (ScreenManager.Instance != null && transform.parent != null && transform.parent.name == DisplayLayerType.Layer_Dialog.ToString())
			{
				ScreenManager.Instance.EnableTapScreenForce();
			}

			if (_allowCloseExternal)
			{
				SystemUtility.DelayCall(this, AttachCloseExternalListener, 0.1f);
			}
		}

		protected virtual void OnFinishOpenAnimationCallback()
		{
		}

		protected virtual void OnFinishCloseAnimationCallback()
		{
		}

		protected virtual void OnCloseExternal()
		{
			Close();
		}

		protected virtual void OnHardwareBackKeyProcess()
		{
			Close();
		}

		protected virtual void OnDisable()
		{
			KillAnimationTween();
			DetachCloseExternalListener();
		}

		protected void OpenPreprocess()
		{
			SyncLayerParentObject();
			ToFront();
			SetActiveBackground(true);
			SetupTab();
			OnOpenPreprocess?.Invoke();
		}

		protected void SetupBackgroundGrayoutCover()
		{
			if (!needBackgroundCover || fillCover != null)
			{
				return;
			}

			var prefab = Resources.Load<GameObject>(BACKGROUND_COVER_PREFAB_PATH)
				?? Resources.Load<GameObject>(BACKGROUND_COVER_PREFAB_PATH.ToLowerInvariant());
			if (prefab == null)
			{
				return;
			}

			var coverObject = UnityEngine.Object.Instantiate(prefab, transform, false);
			coverObject.name = "UIPartsDialogFillCover";
			coverObject.transform.SetAsFirstSibling();
			fillCover = coverObject.GetComponent<UIPartsFillCover>();
			if (fillCover != null)
			{
				fillCover.FillColor = ColorUtility.BLACK_ALPHA_0;
				fillCover.EnableClick = false;
			}
			coverObject.SetActive(false);
		}

		protected void SetActiveBackground(bool isActive)
		{
			if (fillCover != null)
			{
				fillCover.gameObject.SetActive(isActive);
			}
		}

		protected void SetupTab()
		{
			var dialogSetting = GetComponent<DialogSetting>();
			dialogSetting?.SetSize();
		}

		protected void SetupDialogSize(DialogSize dialogSize)
		{
			if (dialogSize == DialogSize.Manual)
			{
				GetComponent<DialogSetting>()?.Execute();
				return;
			}

			GetComponent<DialogSetting>()?.SetSize(dialogSize);
		}

		protected void ToFront()
		{
			transform.SetAsLastSibling();
		}

		protected void SyncLayerParentObject()
		{
			if (transform.parent == null)
			{
				return;
			}

			SetLayerRecursive(transform, transform.parent.gameObject.layer);
		}

		protected void AttachCloseExternalListener()
		{
			if (!_allowCloseExternal || fillCover == null || fillCover.CloseButton == null || _externalCloseAttached)
			{
				return;
			}

			fillCover.CloseButton.onClick.AddListener(OnCloseExternal);
			fillCover.EnableClick = true;
			_externalCloseAttached = true;
		}

		protected void DetachCloseExternalListener()
		{
			if (!_externalCloseAttached || fillCover == null || fillCover.CloseButton == null)
			{
				return;
			}

			fillCover.CloseButton.onClick.RemoveListener(OnCloseExternal);
			_externalCloseAttached = false;
		}

		protected void DisableCloseExternalOnly()
		{
			SetCloseExternal(false);
		}

		protected void DestroyCover()
		{
			if (fillCover == null)
			{
				return;
			}

			UnityEngine.Object.Destroy(fillCover.gameObject);
			fillCover = null;
		}

		protected void ChangeWindowSize(Vector2 size)
		{
			if (windowObject == null)
			{
				return;
			}

			var rectTransform = windowObject.GetComponent<RectTransform>();
			if (rectTransform != null)
			{
				rectTransform.sizeDelta = size;
			}
		}

		protected void Destroy()
		{
			if (closeBehavior == CloseBehavior.Sleep)
			{
				SleepMyself();
			}
			else
			{
				DestroyMyself();
			}
		}

		private void DestroyMyself()
		{
			DisableAllColliders();
			OnFinishCloseAnimationCallback();
			OnClose?.Invoke();
			OnClosed?.Invoke();
			currentState = DialogState.Closed;
			UnityEngine.Object.Destroy(gameObject);
		}

		private void SleepMyself()
		{
			DisableAllColliders();
			OnFinishCloseAnimationCallback();
			OnClose?.Invoke();
			OnClosed?.Invoke();
			currentState = DialogState.Closed;
			gameObject.SetActive(false);
		}

		private void FinishCloseWithoutDestroy()
		{
			DisableAllColliders();
			OnFinishCloseAnimationCallback();
			OnClose?.Invoke();
			OnClosed?.Invoke();
			currentState = DialogState.Closed;
		}

		private void DisableAllColliders()
		{
			if (canvasGroup != null)
			{
				canvasGroup.interactable = false;
				canvasGroup.blocksRaycasts = false;
			}

			foreach (var selectable in GetComponentsInChildren<Selectable>(true))
			{
				selectable.interactable = false;
			}
			foreach (var interaction in GetComponentsInChildren<ButtonViewInteractionBase>(true))
			{
				interaction.KillFadeTween();
			}
			if (fillCover != null)
			{
				fillCover.EnableClick = false;
			}
		}

		private void ExecuteBackKeyProcess()
		{
			if (currentState != DialogState.Show || !Input.GetKeyDown(KeyCode.Escape))
			{
				return;
			}

			OnHardwareBackKeyProcess();
		}

		private void SetCloseExternal(bool allowCloseExternal)
		{
			_allowCloseExternal = allowCloseExternal;
			if (closeButton != null)
			{
				closeButton.SetActive(allowCloseExternal);
				if (allowCloseExternal)
				{
					closeButton.onClick.RemoveAllListeners();
					closeButton.onClick.AddListener(IfNeedCloseExternal);
				}
			}

			if (fillCover != null)
			{
				fillCover.EnableClick = false;
				if (fillCover.CloseButton != null)
				{
					fillCover.CloseButton.onClick.RemoveAllListeners();
				}
			}

			if (!allowCloseExternal)
			{
				DetachCloseExternalListener();
			}
		}

		private void IfNeedCloseExternal()
		{
			if (currentState == DialogState.Show)
			{
				OnCloseExternal();
			}
		}

		private void PlayOpenSE()
		{
			if (openSE == OpenSE.None)
			{
				return;
			}

			seArray ??= new[]
			{
				"SE_UI_DIALOG_OPEN",
				"SE_UI_DIALOG_OPEN",
				"SE_UI_DIALOG_OPEN",
				"SE_UI_DIALOG_OPEN",
				"SE_UI_DIALOG_OPEN",
				"SE_SUBWINDOW_OPEN",
				"SE_UI_DIALOG_OPEN",
				"SE_UI_DIALOG_OPEN"
			};
			var index = (int)openSE;
			if (index < 0 || index >= seArray.Length)
			{
				index = 0;
			}

			SoundManager.Instance.PlaySEOneShot(seArray[index], 0);
		}

		private void KillAnimationTween()
		{
			if (_animationTween == null)
			{
				return;
			}

			_animationTween.Kill(false);
			_animationTween = null;
		}

		private static void SetLayerRecursive(Transform target, int layer)
		{
			if (target == null)
			{
				return;
			}

			target.gameObject.layer = layer;
			for (var i = 0; i < target.childCount; i++)
			{
				SetLayerRecursive(target.GetChild(i), layer);
			}
		}
	}
}
