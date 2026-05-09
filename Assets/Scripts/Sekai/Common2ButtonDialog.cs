using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
    public class Common2ButtonDialog : DialogBase
    {
        [SerializeField] protected CustomButton positiveButton;
        [SerializeField] protected CustomTextMesh positiveButtonLabelMesh;
        [SerializeField] protected CustomText positiveButtonLabel;
        [SerializeField] protected CustomButton negativeButton;
        [SerializeField] protected CustomTextMesh negativeButtonLabelMesh;
        [SerializeField] protected CustomText negativeButtonLabel;
        [SerializeField] protected CustomTextMesh messageBodyTextMesh;
        [SerializeField] protected CustomText messageBodyText;

        protected ICustomText messageBodyTextInterFace;
        protected Action onClickOK;
        protected Action onClickCancel;

        public virtual void Initialize(
            string messageBodyKey,
            string okButtonLabelKey,
            string cancelButtonLabelKey,
            Action onClickOK,
            Action onClickCancel,
            DialogSize dialogSize,
            bool allowCloseExternal = true)
        {
            base.Initialize(dialogSize, allowCloseExternal);
            Initialize(messageBodyKey, okButtonLabelKey, cancelButtonLabelKey, onClickOK, onClickCancel);
            Open();
        }

        public virtual void Initialize(
            string titleKey,
            string messageBodyKey,
            string okButtonLabelKey,
            string cancelButtonLabelKey,
            Action onClickOK,
            Action onClickCancel,
            DialogSize dialogSize,
            bool allowCloseExternal = true)
        {
            base.Initialize(titleKey, dialogSize, allowCloseExternal);
            Initialize(messageBodyKey, okButtonLabelKey, cancelButtonLabelKey, onClickOK, onClickCancel);
            Open();
        }

        public void SetWordingText(string key, params object[] args)
        {
            EnsureMessageBodyText();
            messageBodyTextInterFace?.SetWordingText(key, args);
        }

        public void SetMessageBodyText(string messageBody)
        {
            EnsureMessageBodyText();
            messageBodyTextInterFace?.SetText(messageBody);
        }

        public override void Open()
        {
            base.Open();
        }

        protected virtual void OnClickOK()
        {
            Action action = onClickOK;
            Close();
            action?.Invoke();
        }

        protected virtual void OnClickCancel()
        {
            Action action = onClickCancel;
            Close();
            action?.Invoke();
        }

        protected override void OnCloseExternal()
        {
            OnClickCancel();
        }

        protected override void OnHardwareBackKeyProcess()
        {
            OnCloseExternal();
        }

        private void Initialize(
            string messageBodyKey,
            string okButtonLabelKey,
            string cancelButtonLabelKey,
            Action okAction,
            Action cancelAction)
        {
            onClickOK = okAction;
            onClickCancel = cancelAction;

            SetWordingText(messageBodyKey);
            SetButtonLabel(positiveButtonLabelMesh, positiveButtonLabel, okButtonLabelKey);
            SetButtonLabel(negativeButtonLabelMesh, negativeButtonLabel, cancelButtonLabelKey);
            WireButton(positiveButton, OnClickOK);
            WireButton(negativeButton, OnClickCancel);
        }

        private void EnsureMessageBodyText()
        {
            if (messageBodyTextInterFace != null)
            {
                return;
            }

            if (messageBodyTextMesh != null)
            {
                messageBodyTextInterFace = messageBodyTextMesh;
            }
            else if (messageBodyText != null)
            {
                messageBodyTextInterFace = messageBodyText;
            }
        }

        private static void SetButtonLabel(CustomTextMesh labelMesh, CustomText label, string wordingKey)
        {
            if (labelMesh != null)
            {
                labelMesh.SetWordingText(wordingKey);
            }

            if (label != null)
            {
                label.SetWordingText(wordingKey);
            }
        }

        private static void WireButton(CustomButton button, Action action)
        {
            if (button == null)
            {
                return;
            }

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => action?.Invoke());
        }
    }
}
