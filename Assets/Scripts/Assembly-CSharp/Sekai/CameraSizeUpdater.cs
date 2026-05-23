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

		[SerializeField]
		private float baseWidth;

		[SerializeField]
		private float baseHeight;

		[SerializeField]
		private float pixelPerUnit;

		[SerializeField]
		private BaseType baseType;

		[SerializeField]
		private Camera camera;

		public float OrthographicSize
		{
			get
			{
				return camera != null ? camera.orthographicSize : 0f;
			}
		}

		public float BaseWidth
		{
			get
			{
				return baseWidth;
			}
		}

		public float BaseHeight
		{
			get
			{
				return baseHeight;
			}
		}

		public float BaseOrthographicSize
		{
			get
			{
				return baseHeight / pixelPerUnit * 0.5f;
			}
		}

		private void Awake()
		{
			UpdateOrthographicSize();
		}

		private void UpdateOrthographicSize()
		{
			if (camera == null)
			{
				camera = GetComponent<Camera>();
			}
			if (camera == null || pixelPerUnit == 0f || baseWidth == 0f || baseHeight == 0f)
			{
				return;
			}

			float size = BaseOrthographicSize;
			float screenAspect = (float)Screen.height / Screen.width;
			float baseAspect = baseHeight / baseWidth;
			if (baseType != BaseType.Height && (baseAspect <= screenAspect || baseType == BaseType.Width))
			{
				size *= screenAspect / baseAspect;
			}
			camera.orthographicSize = size;
		}

		public CameraSizeUpdater()
		{
			baseWidth = 1920f;
			baseHeight = 1080f;
			pixelPerUnit = 108f;
		}
	}
}
