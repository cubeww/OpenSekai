using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
	public class LiveOutUIController
	{
		private const string LiveOutUIPrefabPath = "Live/UI/LiveOutUI";
		private const string LiveOutUIPrefabFallbackPath = "live/ui/LiveOutUI";
		private const string LiveOutUIRootName = "LiveOutUI";
		private const string ButtonKeyCancel = "Cancel";
		private const string ButtonKeyRetry = "Retry";
		private const string ButtonKeyResume = "Resume";

		private GameObject uiPrefab;
		private GameObject uiRootObject;
		private ScreenManager screenManager;
		private Camera baseCamera;
		private DialogBase activeDialog;

		public void Initialize(LivePlayMode livePlayMode, Camera baseCamera)
		{
			uiPrefab = Resources.Load<GameObject>(LiveOutUIPrefabPath)
				?? Resources.Load<GameObject>(LiveOutUIPrefabFallbackPath);
			uiRootObject = null;
			screenManager = null;
			activeDialog = null;
			this.baseCamera = baseCamera;
		}

		public void Destroy()
		{
			if (activeDialog != null)
			{
				activeDialog.Close();
				activeDialog = null;
			}
			RemoveUICameraFromBaseStack();
			if (uiRootObject != null)
			{
				UnityEngine.Object.Destroy(uiRootObject);
				uiRootObject = null;
			}
			screenManager = null;
		}

		public void ShowMusicScoreMakerTestPlayFinishDialog(Action onRetry, Action onExit)
		{
			InstantiateIfNeeded();
			SetActiveDialog(screenManager?.Show2ButtonDialog<Common2ButtonDialog>(
				DialogType.Common2ButtonDialog,
				"MSG_MUSIC_SCORE_MAKER_TEST_PLAY_FINISH",
				"WORD_MUSIC_SCORE_MAKER_FINISH",
				"WORD_MUSIC_SCORE_MAKER_RETRY",
				onExit,
				onRetry,
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				false));
		}

		public void ShowMusicScoreMakerFullComboSuccessDialog(Action onReturn, Action onRetry, Action onProceedToPublish)
		{
			InstantiateIfNeeded();
			SetActiveDialog(DialogUtility.ShowCommonSubWindowDialog("フルコンボに成功しました", onReturn));
		}

		public void ShowMusicScoreMakerFullComboFailedDialog(Action onReturn, Action onRetry)
		{
			InstantiateIfNeeded();
			SetActiveDialog(DialogUtility.ShowCommonSubWindowDialog("フルコンボに失敗しました", onReturn));
		}

		public void ShowPauseDialog(Action onClickResume, Action onClickCancel, Action onClickRetry)
		{
			InstantiateIfNeeded();
			SetActiveDialog(screenManager?.ShowMultiButtonDialog<LivePauseDialog>(
				DialogType.LivePauseDialog,
				CreatePauseActionDictionary(onClickResume, onClickCancel, onClickRetry),
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				false));
		}

		public void ShowMusicScoreMakerTestPlayPauseDialog(Action onClickResume, Action onClickReturnToEditor, Action onClickRetry)
		{
			InstantiateIfNeeded();
			SetActiveDialog(screenManager?.ShowMultiButtonDialog<MusicScoreMaker.Ingame.UI.MusicScoreMakerTestPlayPauseDialog>(
				DialogType.MusicScoreMakerTestPlayPauseDialog,
				CreatePauseActionDictionary(onClickResume, onClickReturnToEditor, onClickRetry),
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				false));
		}

		public void ShowMusicScoreMakerFullComboCheckPauseDialog(Action onClickResume, Action onClickReturnToEditor, Action onClickRetry)
		{
			InstantiateIfNeeded();
			var labelDic = new Dictionary<string, string>
			{
				{ ButtonKeyRetry, "WORD_RETRY" }
			};
			SetActiveDialog(screenManager?.ShowMultiButtonDialog<MusicScoreMaker.Ingame.UI.MusicScoreMakerTestPlayPauseDialog>(
				DialogType.MusicScoreMakerTestPlayPauseDialog,
				null,
				labelDic,
				CreatePauseActionDictionary(onClickResume, onClickReturnToEditor, onClickRetry),
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				false));
		}

		public void ShowConfirmRetryDialog(Action onClickOK, Action onClickCancel)
		{
			InstantiateIfNeeded();
			SetActiveDialog(screenManager?.Show2ButtonDialog<Common2ButtonDialog>(
				DialogType.Common2ButtonDialog,
				"WORD_RETRY",
				"MSG_PAUSE_LIVE_RETRY",
				"WORD_RETRY",
				"WORD_CANCEL",
				onClickOK,
				onClickCancel,
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				true));
		}

		public void ShowConfirmRetireDialog(Action onClickOK, Action onClickCancel)
		{
			InstantiateIfNeeded();
			SetActiveDialog(screenManager?.Show2ButtonDialog<Common2ButtonDialog>(
				DialogType.Common2ButtonDialog,
				"WORD_ABORT",
				"MSG_PAUSE_LIVE_ABORT",
				"WORD_ABORT",
				"WORD_CANCEL",
				onClickOK,
				onClickCancel,
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				true));
		}

		private bool ExistsInstance => uiRootObject != null;

		private void RemoveUICameraFromBaseStack()
		{
			if (screenManager == null)
			{
				return;
			}

			Camera uiCamera = screenManager.GetUICamera();
			if (uiCamera != null)
			{
				screenManager.CameraStack?.RemoveFromStack(uiCamera);
			}
		}

		private void InstantiateIfNeeded()
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
				uiPrefab = Resources.Load<GameObject>(LiveOutUIPrefabPath)
					?? Resources.Load<GameObject>(LiveOutUIPrefabFallbackPath);
			}
			if (uiPrefab == null)
			{
				Debug.LogErrorFormat("LiveOutUI prefab not found: {0}", LiveOutUIPrefabPath);
				return;
			}

			uiRootObject = UnityEngine.Object.Instantiate(uiPrefab);
			uiRootObject.name = LiveOutUIRootName;
			screenManager = uiRootObject.GetComponent<ScreenManager>();
			if (screenManager == null)
			{
				Debug.LogError("ScreenManager component not found on LiveOutUI.");
				return;
			}

			ScreenManager.SetupInstance(screenManager);
			screenManager.SetBaseCamera(baseCamera);
		}

		private static Dictionary<string, Action> CreatePauseActionDictionary(Action onClickResume, Action onClickCancel, Action onClickRetry)
		{
			return new Dictionary<string, Action>
			{
				{ ButtonKeyCancel, onClickCancel },
				{ ButtonKeyRetry, onClickRetry },
				{ ButtonKeyResume, onClickResume }
			};
		}

		private void SetActiveDialog(DialogBase dialog)
		{
			activeDialog = dialog;
			if (activeDialog != null)
			{
				DialogBase registeredDialog = activeDialog;
				activeDialog.OnClose += () => ClearActiveDialogIfNeeded(registeredDialog);
			}
		}

		private void ClearActiveDialogIfNeeded(DialogBase registeredDialog)
		{
			if (activeDialog == registeredDialog)
			{
				activeDialog = null;
			}
		}
	}
}
