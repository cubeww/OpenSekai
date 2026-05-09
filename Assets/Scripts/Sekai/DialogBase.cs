using System;
using Sekai.UI;
using TMPro;
using UnityEngine;

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
            None = 6
        }

        public enum CloseBehavior
        {
            Destroy = 0,
            Sleep = 1
        }

        [SerializeField] protected DialogHeader header;
        [SerializeField] protected CustomButton closeButton;
        [SerializeField] protected GameObject windowObject;
        [SerializeField] private bool needBackgroundCover;
        [SerializeField] protected OpenSE openSE;
        [SerializeField] protected CloseBehavior closeBehavior;

        protected CanvasGroup canvasGroup;
        protected DialogState currentState = DialogState.Instantiated;

        private bool allowCloseExternal = true;

        public virtual bool EnableBlur => true;
        public DialogHeader Header => header;
        public GameObject WindowRoot => windowObject != null ? windowObject : gameObject;
        public DialogState CurrentState => currentState;
        public bool IsChain { get; set; }

        public event Action OnClose;
        public event Action OnOpen;
        public event Action OnOpenPreprocess;

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            if (closeButton != null)
            {
                closeButton.onClick.RemoveListener(OnCloseExternal);
                closeButton.onClick.AddListener(OnCloseExternal);
            }
        }

        public virtual void OpenFromChain()
        {
            IsChain = true;
            Open();
        }

        public void Wakeup()
        {
            gameObject.SetActive(true);
            Open();
        }

        public virtual void Open()
        {
            OnOpenPreprocess?.Invoke();
            gameObject.SetActive(true);
            WindowRoot.SetActive(true);
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }

            currentState = DialogState.Show;
            OnOpen?.Invoke();
        }

        public virtual void Close()
        {
            if (currentState == DialogState.Closed)
            {
                return;
            }

            currentState = DialogState.Closed;
            OnClose?.Invoke();

            if (closeBehavior == CloseBehavior.Sleep)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Initialize(DialogSize dialogSize, bool allowCloseExternal = true)
        {
            currentState = DialogState.Initialized;
            this.allowCloseExternal = allowCloseExternal;

            DialogSetting setting = GetComponentInChildren<DialogSetting>(true);
            if (setting != null)
            {
                setting.SetSize(dialogSize);
            }
        }

        public void Initialize(string titleKey, DialogSize dialogSize, bool allowCloseExternal = true)
        {
            Initialize(dialogSize, allowCloseExternal);
            header?.SetWordingKey(titleKey);
        }

        public void ChangeWindowSize(float width, float height, float messageLabelY, float buttonPositionY)
        {
            RectTransform rectTransform = WindowRoot.transform as RectTransform;
            if (rectTransform != null)
            {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void ExecuteBackKeyProcess()
        {
            OnHardwareBackKeyProcess();
        }

        public virtual void SetMessageAlign(TextAlignmentOptions anchor)
        {
        }

        protected virtual void OnCloseExternal()
        {
            if (allowCloseExternal)
            {
                Close();
            }
        }

        protected virtual void OnHardwareBackKeyProcess()
        {
            OnCloseExternal();
        }
    }
}
