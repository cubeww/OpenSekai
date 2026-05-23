using System;
using UnityEngine;

namespace Sekai
{
	public class ScreenLayerHeader : ScreenLayer
	{
		public sealed class BoostData : BootArgBase, IDisposable
		{
			public Action OnSelectedMenu { get; }

			public BoostData(Action onSelectedMenu)
			{
				OnSelectedMenu = onSelectedMenu;
			}

			public void Dispose()
			{
			}
		}

		public sealed class BoostConfigData
		{
		}

		public bool EnableBackUIScreen { get; set; } = true;

		protected override void OnBoot(BootArgBase bootArg)
		{
			// TODO(original): wire header view, player info, currency and menu buttons.
		}

		public override void OnWillExit()
		{
			ScreenWillExitDone();
		}

		public void ShowScreen(bool isAnim = true)
		{
			gameObject.SetActive(true);
		}

		public void ShowScreenImmediate()
		{
			ShowScreen(false);
		}

		public void SetActivePlayerInfoHeader(HeaderCategory category)
		{
		}

		public void HideScreen(bool isAnim = true)
		{
			gameObject.SetActive(false);
		}

		public void UpdatePlayerInfo()
		{
		}

		public void UpdateExp()
		{
		}

		public void SetRankInfo(int rank, int totalExp, bool enabledPlayerRankExpButton = true)
		{
		}

		public void HidePlayerRankExpBalloon()
		{
		}

		public void SetJewelInfo(int jewel)
		{
		}

		public void UpdateLiveBoost()
		{
		}

		public void ShowBackUIScreen(bool isAnim = true)
		{
		}

		public void HideBackUIScreen(bool isAnim = true)
		{
		}

		public void ShowMenuButton(bool isAnim = true)
		{
		}

		public void HideMenuButton(bool isAnim = true)
		{
		}

		public void SetScreenName(string screenName)
		{
		}

		public void SetScreenSubName(string screenSubName)
		{
		}

		public void SetScreenSubName(string screenSubName, params object[] args)
		{
		}

		public void SetScreenSubNameText(string text)
		{
		}

		public void ShowScreenName(bool isAnim = true)
		{
		}

		public void HideScreenName(bool isAnim = true)
		{
		}

		public void InitializeExtension()
		{
		}

		public void ShowCurrentHeaderType(HeaderCategory category, bool isShowAnim = true)
		{
		}

		public void ShowPlayerInfo(bool isAnim = true)
		{
		}

		public void HidePlayerInfo(bool isAnim = true)
		{
		}

		public void HeaderSwap(HeaderCategory category)
		{
		}

		public void VisiblePlayerInfoCover(bool visible)
		{
		}

		public void OnClickBoostRecovery()
		{
		}

		public void OnClickCurrencyUp()
		{
		}

		public void OnClickBackUIScreen()
		{
			if (EnableBackUIScreen)
			{
				ScreenManager.Instance?.BackUIScreen();
			}
		}

		public void OnClickMenuButton()
		{
		}

		public void RefreshUnreadNewsNoti()
		{
		}

		public void ShowAppealBalloon()
		{
		}

		public void SetupOnUpdateUserDataCallBack()
		{
		}

		public void HideAppealBalloon(float duration = 0f)
		{
		}

		public void DeactiveFriendButton()
		{
		}

		public void DeactiveBlockListButton()
		{
		}

		public void DetectiveCommunityReportButton()
		{
		}

		public void DeactiveIdCopyButton()
		{
		}
	}
}
