using UnityEngine;

namespace Sekai
{
    public class CameraSizeUpdater : MonoBehaviour
    {
        private enum BaseType
        {
            Both = 0,
            Width = 1,
            Height = 2
        }

        [SerializeField] private float baseWidth = 1920f;
        [SerializeField] private float baseHeight = 1080f;
        [SerializeField] private float pixelPerUnit = 108f;
        [SerializeField] private BaseType baseType = BaseType.Both;
        [SerializeField] private Camera camera;

        private int currentScreenWidth;
        private int currentScreenHeight;

        public float OrthographicSize => camera != null ? camera.orthographicSize : 0f;

        public float BaseWidth => baseWidth;

        public float BaseHeight => baseHeight;

        public float BaseOrthographicSize => pixelPerUnit == 0f ? 0f : baseHeight / pixelPerUnit * 0.5f;

        private void Awake()
        {
            UpdateOrthographicSize();
        }

        private void OnEnable()
        {
            UpdateOrthographicSize();
        }

        private void Update()
        {
            if (currentScreenWidth == Screen.width && currentScreenHeight == Screen.height)
            {
                return;
            }

            UpdateOrthographicSize();
        }

        private void OnValidate()
        {
            if (camera == null)
            {
                camera = GetComponentInChildren<Camera>();
            }
        }

        public void ForceUpdate()
        {
            UpdateOrthographicSize();
        }

        private void UpdateOrthographicSize()
        {
            if (camera == null || pixelPerUnit == 0f || baseWidth == 0f || Screen.width == 0)
            {
                return;
            }

            currentScreenWidth = Screen.width;
            currentScreenHeight = Screen.height;

            float orthographicSize = BaseOrthographicSize;

            if (baseType != BaseType.Height)
            {
                float screenRatio = (float)Screen.height / Screen.width;
                float baseRatio = baseHeight / baseWidth;

                if (baseRatio <= screenRatio || baseType == BaseType.Width)
                {
                    orthographicSize *= screenRatio / baseRatio;
                }
            }

            camera.orthographicSize = orthographicSize;
        }
    }
}
