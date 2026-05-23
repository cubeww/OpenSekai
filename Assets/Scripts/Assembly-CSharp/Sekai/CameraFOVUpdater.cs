using Sekai.Core;
using UnityEngine;

namespace Sekai
{
	public class CameraFOVUpdater : MonoBehaviour
	{
		[SerializeField]
		private Camera camera;

		private void Awake()
		{
			if (camera == null)
			{
				camera = GetComponent<Camera>();
			}

			UpdateFOVSize();
		}

		private void UpdateFOVSize()
		{
			if (camera == null)
			{
				return;
			}

			camera.fieldOfView = SekaiCameraAspect.CalculateVerticalFov(camera.fieldOfView);
		}

		public CameraFOVUpdater()
		{
		}
	}
}
