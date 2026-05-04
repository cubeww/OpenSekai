using CP;
using UnityEngine;

namespace Sekai.Core
{
    public static class SekaiCameraAspect
    {
        private static int screenHeight;
        private static int screenWidth;
        private static float currentAspect;

        public static readonly float TargetAspect = 1.7778f;

        public static float CurrentAspect
        {
            get
            {
                if (screenHeight != Screen.height || screenWidth != Screen.width)
                {
                    screenHeight = Screen.height;
                    screenWidth = Screen.width;
                    currentAspect = screenHeight == 0 ? TargetAspect : (float)screenWidth / screenHeight;
                }

                return currentAspect;
            }
        }

        public static bool IsVertical => CurrentAspect < TargetAspect;

        public static bool IsHorizontal => TargetAspect < CurrentAspect;

        public static float CalculateVerticalFov(float currentFov)
        {
            if (IsVertical)
            {
                return CameraAspectUtility.CalculateFOV(currentFov, TargetAspect, CurrentAspect);
            }

            return currentFov;
        }

        public static float CalculateInvertVerticalFov(float currentFov)
        {
            if (IsVertical)
            {
                return CameraAspectUtility.CalculateFOV(currentFov, CurrentAspect, TargetAspect);
            }

            return currentFov;
        }
    }
}
