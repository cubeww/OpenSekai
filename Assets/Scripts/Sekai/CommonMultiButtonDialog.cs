using System;
using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
    public class CommonMultiButtonDialog : DialogBase
    {
        [SerializeField] protected List<CommonMultiButtonDialogObjects> dialogButtonObjectList;
        [SerializeField] protected CustomTextMesh messageBodyTextMesh;

        protected ICustomText messageBodyTextInterFace;

        public virtual void Initialize(Dictionary<string, Action> actionDic, DialogSize dialogSize, bool allowCloseExternal = true)
        {
            base.Initialize(dialogSize, allowCloseExternal);
            SetupButtons(actionDic, null);
            Open();
        }

        public void Initialize(
            string messageBodyKey,
            Dictionary<string, string> labelKeyDic,
            Dictionary<string, Action> actionDic,
            DialogSize dialogSize,
            bool allowCloseExternal = true)
        {
            base.Initialize(dialogSize, allowCloseExternal);
            SetWordingText(messageBodyKey);
            SetupButtons(actionDic, labelKeyDic);
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

        protected override void OnCloseExternal()
        {
            base.OnCloseExternal();
        }

        private void SetupButtons(Dictionary<string, Action> actionDic, Dictionary<string, string> labelKeyDic)
        {
            if (dialogButtonObjectList == null)
            {
                return;
            }

            for (int i = 0; i < dialogButtonObjectList.Count; i++)
            {
                CommonMultiButtonDialogObjects buttonObject = dialogButtonObjectList[i];
                if (buttonObject == null || buttonObject.DialogButton == null)
                {
                    continue;
                }

                string key = buttonObject.Key;
                Action action = null;
                bool hasAction = actionDic != null && actionDic.TryGetValue(key, out action);
                buttonObject.DialogButton.gameObject.SetActive(hasAction);
                if (!hasAction)
                {
                    continue;
                }

                if (buttonObject.DialogButtonLabelMesh != null)
                {
                    string labelKey = labelKeyDic != null && labelKeyDic.TryGetValue(key, out string mappedKey) ? mappedKey : key;
                    buttonObject.DialogButtonLabelMesh.SetWordingText(labelKey);
                }

                buttonObject.DialogButton.onClick.RemoveAllListeners();
                buttonObject.DialogButton.onClick.AddListener(() =>
                {
                    Close();
                    action?.Invoke();
                });
            }
        }

        private void EnsureMessageBodyText()
        {
            if (messageBodyTextInterFace == null && messageBodyTextMesh != null)
            {
                messageBodyTextInterFace = messageBodyTextMesh;
            }
        }
    }
}
