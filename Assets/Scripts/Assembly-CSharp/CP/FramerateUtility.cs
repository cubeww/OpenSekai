using UnityEngine;

namespace CP
{
	public static class FramerateUtility
	{
		public const int DefaultFrameRate = 60;

		public static void SetFrameRate(int targetFrameRate = -1)
		{
			Application.targetFrameRate = targetFrameRate > 0 ? targetFrameRate : GetFeasibleFramerate();
		}

		private static int GetFeasibleFramerate()
		{
#if UNITY_2022_2_OR_NEWER
			RefreshRate refreshRateRatio = Screen.currentResolution.refreshRateRatio;
			if (refreshRateRatio.numerator > 0 && refreshRateRatio.denominator > 0)
			{
				double refreshRate = (double)refreshRateRatio.numerator / refreshRateRatio.denominator;
				return Mathf.Max(1, Mathf.RoundToInt((float)refreshRate));
			}
#endif

#pragma warning disable CS0618
			int legacyRefreshRate = Screen.currentResolution.refreshRate;
#pragma warning restore CS0618
			return legacyRefreshRate > 0 ? legacyRefreshRate : DefaultFrameRate;
		}
	}
}
