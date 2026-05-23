using System;
using UnityEngine;

namespace Sekai
{
	public class APIExecutor : MonoBehaviour
	{
		private static APIExecutor instance;

		public static APIExecutor Instance
		{
			get
			{
				if (instance == null)
				{
					var canvasRoot = GameObject.Find("CanvasRoot");
					if (canvasRoot != null)
					{
						instance = canvasRoot.GetComponent<APIExecutor>();
					}
				}

				return instance;
			}
		}

		public void SetupInstance(APIExecutor apiexec)
		{
			instance = apiexec;
		}

		public APICoreParam Execute<A, B>(IAPICaller<A, B> caller, APICore<A, B>.OnAPIEventHandler onCallBackResponse, Action onInterruption, APICoreParam.AfterErrorDetectionType errorDialogType, APICoreParam.AfterInterruptionType afterInterruptionType, bool showLoadingIndicator = true, bool enableExecutingTap = false, Action onRetry = null)
		{
			if (showLoadingIndicator && ScreenManager.ExistsInstance)
			{
				ScreenManager.Instance.ShowNetworkIndicatorScreen(this);
			}

			ExecutingTapEnable(enableExecutingTap);

			if (UserAccountManager.Instance.IsLogin && string.IsNullOrEmpty(APIManager.Instance.SessionToken))
			{
				Recertification();
			}

			var executeBehaviourParam = new APIExecuteBehaviourParam
			{
				OnInterruption = onInterruption,
				ShowIndicator = showLoadingIndicator,
				AfterErrorDetaction = errorDialogType,
				AfterInterruption = afterInterruptionType,
				OnRetry = onRetry
			};

			caller.ClearCallBack();
			caller.AddExecuteBehaviour(executeBehaviourParam);
			caller.AddCallback(OnApiFinished);

			var apiId = caller.Execute(onCallBackResponse);
			return APIManager.Instance.GetExecutingAPI(apiId) ?? new APICoreParam();
		}

		private void Recertification()
		{
			var credential = UserAccountManager.Instance.Data.Credential;
			var deviceId = UserAccountManager.Instance.DeviceId;
			var triggerType = AuthTriggerType.normal.ToString();
			var api = new PutUserAuthAPI(credential, deviceId, triggerType, true);
			var executeBehaviourParam = new APIExecuteBehaviourParam
			{
				OnInterruption = null,
				ShowIndicator = true,
				AfterErrorDetaction = APICoreParam.AfterErrorDetectionType.RetryCancel,
				AfterInterruption = APICoreParam.AfterInterruptionType.Restart
			};

			api.ClearCallBack();
			api.AddExecuteBehaviour(executeBehaviourParam);
			api.AddCallback(OnApiFinished);
			APIManager.Instance.GetExecutingAPI(api.Execute(null));
		}

		public APICoreParam ExecuteFirst<A, B>(IAPICaller<A, B> caller, APICore<A, B>.OnAPIEventHandler onCallBackResponse, Action onInterruption, APICoreParam.AfterErrorDetectionType errorDialogType, APICoreParam.AfterInterruptionType afterInterruptionType, bool showLoadingIndicator = true, bool enableExecutingTap = false)
		{
			if (showLoadingIndicator && ScreenManager.ExistsInstance)
			{
				ScreenManager.Instance.ShowNetworkIndicatorScreen(this);
			}

			ExecutingTapEnable(enableExecutingTap);

			var executeBehaviourParam = new APIExecuteBehaviourParam
			{
				OnInterruption = onInterruption,
				ShowIndicator = showLoadingIndicator,
				AfterErrorDetaction = errorDialogType,
				AfterInterruption = afterInterruptionType
			};

			caller.ClearCallBack();
			caller.AddExecuteBehaviour(executeBehaviourParam);
			caller.AddCallback(OnApiFinished);

			var apiId = caller.ExecuteFirst(onCallBackResponse);
			return APIManager.Instance.GetExecutingAPI(apiId) ?? new APICoreParam();
		}

		public void ExecutingTapEnable(bool isEnable)
		{
			if (ScreenManager.ExistsInstance)
			{
				if (isEnable)
				{
					ScreenManager.Instance.EnableTapScreen(APIManager.Instance);
				}
				else
				{
					ScreenManager.Instance.DisableTapScreen(APIManager.Instance);
				}
			}
		}

		private void OnApiFinished<A, B>(APICore<A, B> apiCore)
		{
			if (apiCore == null)
			{
				return;
			}

			ExecutingTapEnable(false);
		}

		private void CallRetry<A, B>(APICore<A, B> apiCore)
		{
			if (apiCore != null)
			{
				APIManager.Instance.CallRetry(apiCore);
			}
		}

		private void OnFinishRetryAuthentication<A, B>(APICore<A, B> apiCore, bool isSuccess)
		{
			if (isSuccess)
			{
				CallRetry(apiCore);
			}
			else
			{
				ErrorToTitle();
			}
		}

		private void ErrorToTitle()
		{
			SceneManager.Instance.RequestScene(SceneManager.Scene.Title);
		}

		private void OpenStoreURL()
		{
			Application.OpenURL("https://www.google.co.jp/");
		}

		public APIExecutor()
		{
		}
	}
}
