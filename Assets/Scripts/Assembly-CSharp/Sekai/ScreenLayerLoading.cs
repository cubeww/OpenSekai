using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
	public class ScreenLayerLoading : ScreenLayer
	{
		public enum DownloadStatus
		{
			Ready = 0,
			Downloading = 1,
			SuccessFinish = 2,
			ErrorFinish = 3
		}

		public enum LoadingViewType
		{
			None = 0,
			Default = 1,
			Black = 2
		}

		private readonly HashSet<UnityEngine.Object> holdObjects = new HashSet<UnityEngine.Object>();

		public DownloadStatus CurrentDownloadStatus { get; private set; } = DownloadStatus.Ready;
		public int CurrentHoldObjectCount => holdObjects.Count;

		public void ShowBlack()
		{
			gameObject.SetActive(true);
		}

		public void HideBlack()
		{
		}

		public void SetupCategoryDownload(List<string> categories, List<string> addBundleNameList, Action<AssetBundleManager.DownloadInfo> onFinished, Action<AssetBundleManager.DownloadInfo> onError)
		{
			// TODO(original): hook AssetBundleManager download progress.
			CurrentDownloadStatus = DownloadStatus.Ready;
		}

		public void SetupFileListDownload(List<string> addBundleNameList, Action<AssetBundleManager.DownloadInfo> onFinished, Action<AssetBundleManager.DownloadInfo> onError)
		{
			CurrentDownloadStatus = DownloadStatus.Ready;
		}

		public void SetupMultiLive()
		{
		}

		public void SetupRankLive()
		{
		}

		public void SetMultiLiveResultText(int current, int max)
		{
		}

		public void SetupLiveBackground()
		{
		}

		public void OnClickScreen()
		{
		}

		public void SetupLoading(LoadingViewType viewType = LoadingViewType.None)
		{
			CurrentDownloadStatus = DownloadStatus.Ready;
		}

		public void HideAllLoadingView()
		{
		}

		public override void OnWillExit()
		{
			ScreenWillExitDone();
		}

		public void UnloadBackgroundView()
		{
		}

		public void StartDownload()
		{
			CurrentDownloadStatus = DownloadStatus.SuccessFinish;
		}

		public void Hold(UnityEngine.Object holdObj)
		{
			if (holdObj != null)
			{
				holdObjects.Add(holdObj);
			}
		}

		public void Release(UnityEngine.Object holdObj)
		{
			if (holdObj != null)
			{
				holdObjects.Remove(holdObj);
			}
		}

		public void ForceRelease()
		{
			holdObjects.Clear();
		}

		public IEnumerator WaitForFinished()
		{
			yield break;
		}

		public void OnClickNews()
		{
		}
	}
}
