using UnityEngine;

namespace CP
{
    public static class CameraAspectUtility
    {
        public static float CalculateFOV(float verticalFOV, float targetAspect, float currentAspect)
        {
            float horizontalFOV = CalculateVertical2HorizontalFOV(verticalFOV, targetAspect);
            return CalculateHorizontal2VerticalFOV(horizontalFOV, currentAspect);
        }

        public static float CalculateVertical2HorizontalFOV(float verticalFOV, float aspect)
        {
            return Mathf.Atan(Mathf.Tan(verticalFOV * 0.5f * Mathf.Deg2Rad) * aspect) * 2f * Mathf.Rad2Deg;
        }

        public static float CalculateHorizontal2VerticalFOV(float horizontalFOV, float aspect)
        {
            return Mathf.Atan(Mathf.Tan(horizontalFOV * 0.5f * Mathf.Deg2Rad) / aspect) * 2f * Mathf.Rad2Deg;
        }

        public static float CalculateVerticalPositionOffset(float baseFov, float targetFov, float dist)
        {
            return -(Mathf.Tan(targetFov * 0.5f * Mathf.Deg2Rad) - Mathf.Tan(baseFov * 0.5f * Mathf.Deg2Rad)) * dist;
        }
    }
}
