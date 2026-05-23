namespace Sekai
{
	public class HeaderUtility
	{
		public static void SetDisplayHeader(ScreenLayerData data)
		{
			if (data == null || data.DisplayHeader == HeaderDisplay.Unrelated)
			{
				return;
			}

			var header = GetHeader();
			if (header == null)
			{
				return;
			}

			switch (data.DisplayHeader)
			{
				case HeaderDisplay.Show:
					header.ShowScreen(false);
					header.SetActivePlayerInfoHeader(data.DisplayCategory);
					break;
				case HeaderDisplay.ShowWithAnimation:
					header.ShowScreen(true);
					header.SetActivePlayerInfoHeader(data.DisplayCategory);
					break;
				case HeaderDisplay.Hide:
					header.HideScreen(false);
					break;
				case HeaderDisplay.HideWithAnimation:
					header.HideScreen(true);
					break;
				case HeaderDisplay.ShowImmediate:
					header.ShowScreenImmediate();
					header.SetActivePlayerInfoHeader(data.DisplayCategory);
					break;
			}

			switch (data.DisplayPlayerInfo)
			{
				case HeaderDisplay.Show:
					header.ShowCurrentHeaderType(data.DisplayCategory, false);
					break;
				case HeaderDisplay.ShowWithAnimation:
					header.ShowCurrentHeaderType(data.DisplayCategory, true);
					break;
				case HeaderDisplay.Hide:
					header.HidePlayerInfo(false);
					break;
				case HeaderDisplay.HideWithAnimation:
					header.HidePlayerInfo(true);
					break;
				case HeaderDisplay.SwapWithAnimation:
					header.HeaderSwap(data.DisplayCategory);
					break;
			}

			switch (data.DisplayBackUIScreen)
			{
				case HeaderDisplay.Show:
					header.ShowBackUIScreen(false);
					break;
				case HeaderDisplay.ShowWithAnimation:
					header.ShowBackUIScreen(true);
					break;
				case HeaderDisplay.Hide:
					header.HideBackUIScreen(false);
					break;
				case HeaderDisplay.HideWithAnimation:
					header.HideBackUIScreen(true);
					break;
			}

			header.EnableBackUIScreen = data.EnableBackUIScreen;

			switch (data.DisplayScreenName)
			{
				case HeaderDisplay.Show:
					header.ShowScreenName(false);
					break;
				case HeaderDisplay.ShowWithAnimation:
					header.ShowScreenName(true);
					break;
				case HeaderDisplay.Hide:
					header.HideScreenName(false);
					break;
				case HeaderDisplay.HideWithAnimation:
					header.HideScreenName(true);
					break;
			}

			if (!string.IsNullOrEmpty(data.ScreenName))
			{
				header.SetScreenName(data.ScreenName);
			}

			if (!string.IsNullOrEmpty(data.ScreenSubName))
			{
				header.SetScreenSubName(data.ScreenSubName);
			}

			// IDA shows the original clearing profile-only header buttons here.
			// The current header implementation is still simplified, so these are no-ops for now.
			header.DeactiveFriendButton();
			header.DetectiveCommunityReportButton();
			header.DeactiveBlockListButton();
			header.DeactiveIdCopyButton();
		}

		public static void ShowBackUIScreen(bool isAnim = true)
		{
			GetHeader()?.ShowBackUIScreen(isAnim);
		}

		public static void HideBackUIScreen(bool isAnim = true)
		{
			GetHeader()?.HideBackUIScreen(isAnim);
		}

		public static void SetScreenSubNameText(string screenName)
		{
			GetHeader()?.SetScreenSubNameText(screenName);
		}

		public static void ShowMenuButton(bool isAnim = true)
		{
			GetHeader()?.ShowMenuButton(isAnim);
		}

		public static void HideMenuButton(bool isAnim = true)
		{
			GetHeader()?.HideMenuButton(isAnim);
		}

		public static void HidePlayerInfo(bool isAnim = true)
		{
			GetHeader()?.HidePlayerInfo(isAnim);
		}

		public static void ShowHeader(bool isAnim = true)
		{
			GetHeader()?.ShowScreen(isAnim);
		}

		public static void HideHeader(bool isAnim = true)
		{
			GetHeader()?.HideScreen(isAnim);
		}

		public static void SetEnableHeader(bool enable)
		{
			var header = GetHeader();
			if (header != null)
			{
				header.gameObject.SetActive(enable);
			}
		}

		private static ScreenLayerHeader GetHeader()
		{
			return ScreenManager.Instance?.GetLayerComponent<ScreenLayerHeader>(MenuScreenType.Header);
		}
	}
}
