using UnityEngine;
using Sekai.Core;

namespace Sekai
{
	[ExecuteInEditMode]
	public class SpriteAnchor : MonoBehaviour
	{
		public enum VerticalAnchor
		{
			Top = 0,
			Middle = 1,
			Bottom = 2
		}

		public enum HoraizontalAnchor
		{
			Left = 0,
			Center = 1,
			Right = 2
		}

		public const float SAFEAREA_WIDTH = 60f;

		[SerializeField]
		private VerticalAnchor verticalAnchor;

		[SerializeField]
		private HoraizontalAnchor horaizontalAnchor;

		private Camera cachedCamera;

		private void Start()
		{
			cachedCamera = CameraUtility.GetFrontCamera();
		}

		private void Update()
		{
			if (cachedCamera == null)
			{
				Start();
			}
			if (cachedCamera == null)
			{
				return;
			}

			float orthographicSize = cachedCamera.orthographicSize;
			Rect safeArea = Screen.safeArea;
			float safeWidth = safeArea.width;
			float safeHeight = Mathf.Max(1f, safeArea.height);
			if (safeArea.width < Screen.width || safeArea.height < Screen.height)
			{
				safeWidth -= SAFEAREA_WIDTH * 2f;
			}

			Vector3 localPosition = transform.localPosition;
			float y = 0f;
			if (verticalAnchor == VerticalAnchor.Top)
			{
				y = orthographicSize;
			}
			else if (verticalAnchor == VerticalAnchor.Bottom)
			{
				y = -orthographicSize;
			}

			float x = 0f;
			float aspect = safeWidth / safeHeight;
			if (horaizontalAnchor == HoraizontalAnchor.Left)
			{
				x = -orthographicSize * aspect;
			}
			else if (horaizontalAnchor == HoraizontalAnchor.Right)
			{
				x = orthographicSize * aspect;
			}
			transform.localPosition = new Vector3(x, y, localPosition.z);
		}

		public SpriteAnchor()
		{
		}
	}
}
