using UnityEngine;

namespace Sekai.Core
{
	public class CameraUtility
	{
		private const string FRONT_CAMERA_TAG = "FrontCamera";

		private static readonly string PVPath;

		private static readonly string RenderCanvasPath;

		private static readonly string MainCameraVLPath;

		private static readonly string MainCameraMVPath;

		private static readonly string SubCameraPath;

		public static GameObject CreateRenderCanvas(Transform parent, bool worldPoitionStays = false)
		{
			return LoadObject(RenderCanvasPath, parent, worldPoitionStays);
		}

		public static GameObject CreateMVMainCamera(Transform parent, bool worldPoitionStays = false)
		{
			return CreateMainCamera(MainCameraMVPath, parent, worldPoitionStays);
		}

		public static GameObject CreateVLMainCamera(Transform parent, bool worldPoitionStays = false)
		{
			return CreateMainCamera(MainCameraVLPath, parent, worldPoitionStays);
		}

		private static GameObject CreateMainCamera(string path, Transform parent, bool worldPoitionStays = false)
		{
			return LoadObject(path, parent, worldPoitionStays);
		}

		public static GameObject CreateSubCamera(Transform parent, bool worldPoitionStays = false)
		{
			return LoadObject(SubCameraPath, parent, worldPoitionStays);
		}

		private static GameObject LoadObject(string path, Transform parent, bool worldPoitionStays = false)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}

			GameObject prefab = Resources.Load<GameObject>(path);
			return prefab != null ? Object.Instantiate(prefab, parent, worldPoitionStays) : null;
		}

		public static Camera GetFrontCamera()
		{
			GameObject frontCamera = GameObject.FindGameObjectWithTag(FRONT_CAMERA_TAG);
			return frontCamera != null ? frontCamera.GetComponent<Camera>() : null;
		}

		public CameraUtility()
		{
		}

		static CameraUtility()
		{
			PVPath = "live/camera/pv";
			RenderCanvasPath = "live/camera/RenderCanvas";
			MainCameraVLPath = "live/camera/MainCameraVL";
			MainCameraMVPath = "live/camera/MainCameraMV";
			SubCameraPath = "live/camera/SubCamera";
		}
	}
}
