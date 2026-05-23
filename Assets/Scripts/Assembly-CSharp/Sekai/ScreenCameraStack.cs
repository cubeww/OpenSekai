using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Sekai
{
	public class ScreenCameraStack
	{
		private const int UiRendererIndex = 2;

		private class StackCameraLayer
		{
			public Camera Camera;
			public UniversalAdditionalCameraData CameraData;
			public int Priority;

			public StackCameraLayer(Camera camera, UniversalAdditionalCameraData cameraData, int priority)
			{
				Camera = camera;
				CameraData = cameraData;
				Priority = priority;
			}
		}

		private Camera baseCamera;
		private UniversalAdditionalCameraData baseCameraData;
		private readonly List<StackCameraLayer> cameraStackList = new List<StackCameraLayer>();

		public Camera BaseCamera => baseCamera;

		public Camera CreateEmptyBaseCamera(Transform parent)
		{
			var cameraObject = new GameObject("BaseCamera");
			if (parent != null)
			{
				cameraObject.transform.SetParent(parent, false);
			}

			var camera = cameraObject.AddComponent<Camera>();
			camera.backgroundColor = Color.black;
			camera.clearFlags = CameraClearFlags.Color;
			camera.cullingMask = 0;
			camera.orthographic = true;
			camera.depth = -100f;
			camera.useOcclusionCulling = false;
			GetOrAddCameraData(camera)?.SetRenderer(UiRendererIndex);
			SetBaseCamera(camera);
			return camera;
		}

		public void AddToStack(Camera camera, int priority)
		{
			if (camera == null)
			{
				return;
			}

			var cameraData = GetOrAddCameraData(camera);
			if (cameraData != null)
			{
				cameraData.renderType = CameraRenderType.Overlay;
			}

			cameraStackList.RemoveAll(x => x.Camera == camera);
			cameraStackList.Add(new StackCameraLayer(camera, cameraData, priority));
			cameraStackList.Sort((a, b) => a.Priority.CompareTo(b.Priority));
			StackToBaseCamera();
		}

		public void RemoveFromStack(Camera camera)
		{
			if (camera == null)
			{
				return;
			}

			cameraStackList.RemoveAll(x => x.Camera == null || x.Camera == camera);
			if (baseCameraData != null)
			{
				baseCameraData.cameraStack.RemoveAll(x => x == null || x == camera);
			}
			if (cameraStackList.Count > 0)
			{
				StackToBaseCamera();
			}
			else
			{
				SetupSekaiCameraData();
			}
		}

		public void SetBaseCamera(Camera camera)
		{
			baseCamera = camera;
			baseCameraData = GetOrAddCameraData(camera);
			if (baseCameraData != null)
			{
				baseCameraData.renderType = CameraRenderType.Base;
			}

			StackToBaseCamera();
		}

		private static UniversalAdditionalCameraData GetOrAddCameraData(Camera camera)
		{
			if (camera == null)
			{
				return null;
			}

			return camera.GetComponent<UniversalAdditionalCameraData>() ?? camera.gameObject.AddComponent<UniversalAdditionalCameraData>();
		}

		private void StackToBaseCamera()
		{
			if (baseCameraData == null)
			{
				return;
			}

			cameraStackList.RemoveAll(x => x.Camera == null);
			if (cameraStackList.Count == 0)
			{
				SetupSekaiCameraData();
				return;
			}

			baseCameraData.cameraStack.Clear();
			foreach (var layer in cameraStackList)
			{
				if (layer.Camera != null && layer.Camera != baseCamera)
				{
					baseCameraData.cameraStack.Add(layer.Camera);
				}
			}

			SetupSekaiCameraData();
		}

		private void SetupSekaiCameraData()
		{
			if (baseCamera == null || baseCameraData == null)
			{
				return;
			}

			var cameraCount = baseCameraData.cameraStack.Count + 1;
			baseCamera.GetSekaiAdditionalCameraData()?.Setup(0, cameraCount);
			for (var i = 0; i < baseCameraData.cameraStack.Count; i++)
			{
				baseCameraData.cameraStack[i]?.GetSekaiAdditionalCameraData()?.Setup(i + 1, cameraCount);
			}
		}
	}
}
