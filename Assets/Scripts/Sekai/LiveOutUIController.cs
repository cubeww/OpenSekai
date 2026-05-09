using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
    public class LiveOutUIController : MonoBehaviour
    {
        [SerializeField] private GameObject uiPrefab;
        [SerializeField] private LivePlayMode playMode;
        [SerializeField] private Camera baseCamera;

        private GameObject uiRootObject;
        private ScreenManager screenManager;

        public bool ExistsInstance => uiRootObject != null && screenManager != null;
        public DialogBase ActiveDialog => screenManager != null ? screenManager.ActiveDialog : null;

        public void Initialize(LivePlayMode playMode, Camera baseCamera)
        {
            this.playMode = playMode;
            this.baseCamera = baseCamera;
            uiRootObject = null;
            screenManager = null;
        }

        public void ShowPauseDialog(Action onClickResume, Action onClickCancel, Action onClickRetry)
        {
            EnsureInstance();
            if (screenManager == null)
            {
                return;
            }

            Dictionary<string, Action> actions = new Dictionary<string, Action>
            {
                { "Cancel", onClickCancel },
                { "Retry", onClickRetry },
                { "Resume", onClickResume }
            };

            screenManager.ShowMultiButtonDialog<LivePauseDialog>(
                DialogType.LivePauseDialog,
                actions,
                DialogSize.Small,
                ScreenLayerType.Dialog,
                false);
        }

        public ConsecutiveAutoLivePauseDialog ShowConsecutiveAutoLivePauseDialog(Action onClickResume, Action onClickCancel)
        {
            EnsureInstance();
            if (screenManager == null)
            {
                return null;
            }

            ConsecutiveAutoLivePauseDialog dialog = screenManager.Show2ButtonDialog<ConsecutiveAutoLivePauseDialog>(
                DialogType.ConsecutiveAutoLivePauseDialog,
                onClickResume,
                onClickCancel,
                ScreenLayerType.Dialog,
                true);
            dialog?.Setup();
            return dialog;
        }

        public void ShowConfirmRetryDialog(Action onClickOK, Action onClickCancel)
        {
            ShowCommon2ButtonDialog(
                "WORD_RETRY",
                "MSG_PAUSE_LIVE_RETRY",
                "WORD_RETRY",
                "WORD_CANCEL",
                onClickOK,
                onClickCancel);
        }

        public void ShowConfirmRetireDialog(Action onClickOK, Action onClickCancel)
        {
            ShowCommon2ButtonDialog(
                "WORD_ABORT",
                "MSG_PAUSE_LIVE_ABORT",
                "WORD_ABORT",
                "WORD_CANCEL",
                onClickOK,
                onClickCancel);
        }

        public void ShowConsecutiveAutoLiveConfirmRetireDialog(Action onClickOK, Action onClickCancel)
        {
            ShowCommon2ButtonDialog(
                "WORD_ABORT",
                "MSG_PAUSE_LIVE_ABORT",
                "WORD_HOME_BACK",
                "WORD_RETURN_LIVE",
                onClickOK,
                onClickCancel);
        }

        public void ExecuteBackKeyProcess()
        {
            ActiveDialog?.ExecuteBackKeyProcess();
        }

        public void DestroyInstance()
        {
            if (uiRootObject != null)
            {
                Destroy(uiRootObject);
            }

            uiRootObject = null;
            screenManager = null;
        }

        private void ShowCommon2ButtonDialog(
            string titleKey,
            string messageBodyKey,
            string okButtonLabelKey,
            string cancelButtonLabelKey,
            Action onClickOK,
            Action onClickCancel)
        {
            EnsureInstance();
            if (screenManager == null)
            {
                return;
            }

            screenManager.Show2ButtonDialog<Common2ButtonDialog>(
                DialogType.Common2ButtonDialog,
                titleKey,
                messageBodyKey,
                okButtonLabelKey,
                cancelButtonLabelKey,
                onClickOK,
                onClickCancel,
                DialogSize.Small,
                ScreenLayerType.Dialog,
                true);
        }

        private void EnsureInstance()
        {
            if (ExistsInstance)
            {
                return;
            }

            Instantiate();
        }

        private void Instantiate()
        {
            if (uiPrefab == null)
            {
                Debug.LogError("LiveOutUI prefab is not assigned.", this);
                return;
            }

            uiRootObject = UnityEngine.Object.Instantiate(uiPrefab, null);
            uiRootObject.name = "LiveOutUI";
            screenManager = uiRootObject.GetComponent<ScreenManager>();
            if (screenManager == null)
            {
                Debug.LogError("LiveOutUI prefab does not have ScreenManager.", uiRootObject);
                return;
            }

            ScreenManager.SetupInstance(screenManager);
            screenManager.SetBaseCamera(baseCamera);
        }
    }
}
