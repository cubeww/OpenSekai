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

		public static readonly int MinHeight = 720;
		public static readonly int MaxHeight = 1080;
		public static readonly int MinHeightForIngame3DMV = 540;
		public static readonly int MaxHeightForIngame3DMV = 1080;
		public static readonly float Aspect = 16f / 9f;
		public static readonly ScreenSize DefaultSize = new ScreenSize(1920, 1080);
		public static readonly ScreenSize LowDPISize = new ScreenSize(1280, 720);
		public static readonly ScreenSize HighDPISize = DefaultSize;
		public static readonly ScreenSize LowDPISizeForIngame3DMV = new ScreenSize(960, 540);
		public static readonly ScreenSize HighDPISizeForIngame3DMV = DefaultSize;
		public static readonly ScreenSize VirtualLiveDefaultDPISize = DefaultSize;
		public static readonly ScreenSize StreamingLiveHighDPISize = DefaultSize;
		public static readonly ScreenSize StreamingLiveDefaultDPISize = DefaultSize;
		public static readonly ScreenSize StreamingLiveLowDPISize = LowDPISize;

		public static ResolutionQuality CurrentResolutionQuality { get; private set; } = ResolutionQuality.Default;

		public static void DownResolution(MVQualityType qualityType)
		{
			DownResolution(qualityType == MVQualityType.High ? ResolutionQuality.High : qualityType == MVQualityType.Middle ? ResolutionQuality.Middle : ResolutionQuality.Low);
		}

		public static void DownResolution(ResolutionQuality resolutionQuality)
		{
			CurrentResolutionQuality = resolutionQuality;
			var size = resolutionQuality == ResolutionQuality.Low ? LowDPISize : DefaultSize;
			Screen.SetResolution(size.width, size.height, Screen.fullScreenMode);
		}

		public static void DownResolutionStreaming(ResolutionQuality resolutionQuality)
		{
			DownResolution(resolutionQuality);
		}

		public static void ResetResolution()
		{
			CurrentResolutionQuality = ResolutionQuality.Default;
			Screen.SetResolution(DefaultSize.width, DefaultSize.height, Screen.fullScreenMode);
		}
	}
}
