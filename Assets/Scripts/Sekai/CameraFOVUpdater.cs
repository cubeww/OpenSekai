using Sekai.Core;
using UnityEngine;

namespace Sekai
{
    public class CameraFOVUpdater : MonoBehaviour
    {
        [SerializeField] private Camera camera;

        private int currentScreenWidth;
        private int currentScreenHeight;
        private float baseFieldOfView;

        private void Awake()
        {
            if (camera != null)
            {
                baseFieldOfView = camera.fieldOfView;
            }

            UpdateFOVSize();
        }

        private void OnEnable()
        {
            if (camera != null && baseFieldOfView <= 0f)
            {
                baseFieldOfView = camera.fieldOfView;
            }

            UpdateFOVSize();
        }

        private void Update()
        {
            if (currentScreenWidth == Screen.width && currentScreenHeight == Screen.height)
            {
                return;
            }

            UpdateFOVSize();
        }

        private void OnValidate()
        {
            if (camera == null)
            {
                camera = GetComponent<Camera>();
            }
        }

        private void UpdateFOVSize()
        {
            if (camera == null)
            {
                return;
            }

            currentScreenWidth = Screen.width;
            currentScreenHeight = Screen.height;

            float sourceFov = baseFieldOfView > 0f ? baseFieldOfView : camera.fieldOfView;
            camera.fieldOfView = SekaiCameraAspect.CalculateVerticalFov(sourceFov);
        }
    }
}
