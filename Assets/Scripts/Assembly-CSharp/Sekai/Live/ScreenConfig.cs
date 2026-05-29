using UnityEngine;

namespace Sekai.Live
{
	public enum MVQualityType
	{
		Low = 0,
		Middle = 1,
		High = 2
	}

	public static class ScreenConfig
	{
		public enum ResolutionQuality
		{
			High = 0,
			Middle = 1,
			Low = 2,
			Default = 3
		}

		public struct ScreenSize
		{
			public int width;
			public int height;

			public ScreenSize(int width, int height)
			{
				this.width = width;
				this.height = height;
			}
		}

		public static readonly int MinHeight = 640;
		public static readonly int MaxHeight = 1080;
		public static readonly int MinHeightForIngame3DMV = 560;
		public static readonly int MaxHeightForIngame3DMV = 1080;
		private static readonly float TargetLowDpi = 350f;
		private static readonly float TargetHighDpi = 500f;
		private static readonly float TargetLowRenderingScaleForIngame3DMV = 0.6f;
		private static readonly float TargetHighRenderingScaleForIngame3DMV = 0.8f;
		private static readonly float VirtualLiveDefaultTargetDpi = 350f;
		private static readonly float StreamingLiveHighDpi = 500f;
		private static readonly float StreamingLiveDefaultDpi = 320f;
		private static readonly float StreamingLiveLowDpi = 250f;
		public static readonly ScreenSize DefaultSize = new ScreenSize(Screen.width, Screen.height);
		public static readonly float Aspect = DefaultSize.height > 0 ? (float)DefaultSize.width / DefaultSize.height : 16f / 9f;
		public static readonly ScreenSize LowDPISize = CalculateDpiSize(TargetLowDpi, MinHeight, MaxHeight, true);
		public static readonly ScreenSize HighDPISize = CalculateDpiSize(TargetHighDpi, MinHeight, MaxHeight, false);
		public static readonly ScreenSize LowDPISizeForIngame3DMV = CalculateScaleSize(TargetLowRenderingScaleForIngame3DMV, MinHeightForIngame3DMV, MaxHeightForIngame3DMV, true);
		public static readonly ScreenSize HighDPISizeForIngame3DMV = CalculateScaleSize(TargetHighRenderingScaleForIngame3DMV, MinHeightForIngame3DMV, MaxHeightForIngame3DMV, false);
		public static readonly ScreenSize VirtualLiveDefaultDPISize = CalculateDpiSize(VirtualLiveDefaultTargetDpi, MinHeight, MaxHeight, true);
		public static readonly ScreenSize StreamingLiveHighDPISize = CalculateDpiSize(StreamingLiveHighDpi, MinHeight, MaxHeight, true);
		public static readonly ScreenSize StreamingLiveDefaultDPISize = CalculateDpiSize(StreamingLiveDefaultDpi, MinHeight, MaxHeight, true);
		public static readonly ScreenSize StreamingLiveLowDPISize = CalculateDpiSize(StreamingLiveLowDpi, MinHeight, MaxHeight, true);

		private static ScreenSize standaloneRestoreSize = DefaultSize;

		public static ResolutionQuality CurrentResolutionQuality { get; private set; } = ResolutionQuality.Default;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeStandaloneResolution()
		{
			CaptureStandaloneRestoreSize();
			if (ShouldRestoreStandaloneDefaultResolution())
			{
				RestoreStandaloneResolution();
			}
		}

		public static void CaptureStandaloneRestoreSize()
		{
			if (!ShouldRestoreStandaloneDefaultResolution())
			{
				return;
			}

			// OpenSekai: desktop players can resize the window before live; remember that size.
			standaloneRestoreSize = new ScreenSize(Screen.width, Screen.height);
		}

		public static void DownResolution(MVQualityType qualityType)
		{
			DownResolution(qualityType == MVQualityType.High ? ResolutionQuality.High : qualityType == MVQualityType.Middle ? ResolutionQuality.Middle : ResolutionQuality.Low);
		}

		public static void DownResolution(ResolutionQuality resolutionQuality)
		{
			CurrentResolutionQuality = resolutionQuality;
			var size = resolutionQuality == ResolutionQuality.Low ? LowDPISize :
				resolutionQuality == ResolutionQuality.Middle ? VirtualLiveDefaultDPISize :
				HighDPISize;
			if (ShouldApplyMobileResolution())
			{
				Screen.SetResolution(size.width, size.height, Screen.fullScreenMode);
			}
			else if (ShouldRestoreStandaloneDefaultResolution())
			{
				RestoreStandaloneResolution();
			}
		}

		public static void DownResolutionStreaming(ResolutionQuality resolutionQuality)
		{
			DownResolution(resolutionQuality);
		}

		public static void ResetResolution()
		{
			CurrentResolutionQuality = ResolutionQuality.Default;
			if (ShouldApplyMobileResolution())
			{
				Screen.SetResolution(DefaultSize.width, DefaultSize.height, Screen.fullScreenMode);
			}
			else if (ShouldRestoreStandaloneDefaultResolution())
			{
				RestoreStandaloneResolution();
			}
		}

		private static void RestoreStandaloneResolution()
		{
			Screen.SetResolution(standaloneRestoreSize.width, standaloneRestoreSize.height, Screen.fullScreenMode);
		}

		private static ScreenSize CalculateDpiSize(float targetDpi, int minHeight, int maxHeight, bool clampMaxHeight)
		{
			float dpi = Screen.dpi;
			float scale = dpi > 0f ? targetDpi / dpi : float.PositiveInfinity;
			return CalculateScaleSize(scale, minHeight, maxHeight, clampMaxHeight);
		}

		private static ScreenSize CalculateScaleSize(float scale, int minHeight, int maxHeight, bool clampMaxHeight)
		{
			float height = Mathf.Max(DefaultSize.height, 1);
			float minScale = Mathf.Min(minHeight / height, 1f);
			float maxScale = clampMaxHeight ? Mathf.Min(maxHeight / height, 1f) : 1f;
			float clampedScale = Mathf.Clamp(scale, minScale, maxScale);
			return new ScreenSize((int)(DefaultSize.width * clampedScale), (int)(DefaultSize.height * clampedScale));
		}

		private static bool ShouldApplyMobileResolution()
		{
#if UNITY_ANDROID || UNITY_IOS
			return true;
#else
			return false;
#endif
		}

		private static bool ShouldRestoreStandaloneDefaultResolution()
		{
#if UNITY_STANDALONE && !UNITY_EDITOR
			return true;
#else
			return false;
#endif
		}
	}
}
