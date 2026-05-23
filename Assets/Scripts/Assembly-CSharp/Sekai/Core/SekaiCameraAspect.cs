namespace Sekai.Core
{
	using UnityEngine;

	public static class SekaiCameraAspect
	{
		private static int screenHeight;

		private static int screenWidth;

		private static float currentAspect;

		public static readonly float TargetAspect;

		public static float CurrentAspect
		{
			get
			{
				if (screenHeight != Screen.height || screenWidth != Screen.width)
				{
					screenHeight = Screen.height;
					screenWidth = Screen.width;
					currentAspect = screenHeight > 0 ? (float)screenWidth / screenHeight : TargetAspect;
				}

				return currentAspect;
			}
		}

		public static bool IsVertical
		{
			get
			{
				return CurrentAspect < TargetAspect;
			}
		}

		public static bool IsHorizontal
		{
			get
			{
				return TargetAspect < CurrentAspect;
			}
		}

		public static float CalculateVerticalFov(float currentFov)
		{
			return IsVertical ? CalculateFov(currentFov, TargetAspect, CurrentAspect) : currentFov;
		}

		public static float CalculateInvertVerticalFov(float currentFov)
		{
			return IsVertical ? CalculateFov(currentFov, CurrentAspect, TargetAspect) : currentFov;
		}

		static SekaiCameraAspect()
		{
			TargetAspect = 1.7778f;
			screenHeight = -1;
			screenWidth = -1;
			currentAspect = TargetAspect;
		}

		private static float CalculateFov(float fov, float fromAspect, float toAspect)
		{
			if (toAspect <= 0f)
			{
				return fov;
			}

			var rad = fov * Mathf.Deg2Rad;
			return Mathf.Atan(Mathf.Tan(rad * 0.5f) * fromAspect / toAspect) * 2f * Mathf.Rad2Deg;
		}
	}
}
