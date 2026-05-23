using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiLegacyDrawUIRendererFeature : ScriptableRendererFeature
	{
		[Serializable]
		public class AreaCameraSettings
		{
			public int Event_DrawUI = (int)RenderPassEvent.AfterRenderingTransparents;
			public int Event_CopyScreenToCapture = (int)RenderPassEvent.AfterRenderingPostProcessing;
		}

		[Serializable]
		public class PassSettings
		{
			public int Event_DrawBlurUI = (int)RenderPassEvent.AfterRenderingOpaques;
			public int Event_Blur = (int)RenderPassEvent.AfterRenderingPostProcessing;
			public int Event_DrawUI = (int)RenderPassEvent.AfterRendering;
		}

		[Serializable]
		public class UIBlurSettings
		{
			public LayerMask BlurLayerMask;
			public LayerMask UILayerMask;
			public RenderQueueRange RenderQueueRange = RenderQueueRange.transparent;
		}

		[SerializeField]
		private UIBlurSettings m_BlurSettings = new UIBlurSettings();

		[SerializeField]
		private PassSettings m_PassSettings = new PassSettings();

		[SerializeField]
		private AreaCameraSettings m_AreaCameraSettings = new AreaCameraSettings();

		private SekaiUIBuffer m_UIBuffer;
		private SekaiUIBlurSetupPass m_SetupPass;
		private SekaiCopyPass m_CopyScreenToCapturePass;
		private SekaiDrawUIPass m_DrawBlurUIPass;
		private SekaiUIBlurPass m_BlurPass;
		private SekaiDrawUIPass m_DrawUIPass;

		public override void Create()
		{
			m_UIBuffer?.Dispose();
			m_BlurPass?.Cleanup();
			m_UIBuffer = new SekaiUIBuffer();
			m_SetupPass = new SekaiUIBlurSetupPass();
			m_DrawBlurUIPass = new SekaiDrawUIPass("Render BlurUI [Sekai UICamera] ");
			m_BlurPass = new SekaiUIBlurPass("Execute Blur [Sekai UICamera] ");
			m_DrawUIPass = new SekaiDrawUIPass("Render UI [Sekai DefaultCamera]");
			m_CopyScreenToCapturePass = new SekaiCopyPass("Copy Screen to CaptureTex [Sekai]");
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (SekaiUIEffectSettings.Blur.IsActive && IsFinalCamera(renderingData.cameraData.camera))
			{
				renderer.EnqueuePass(m_SetupPass);
				renderer.EnqueuePass(m_CopyScreenToCapturePass);
				renderer.EnqueuePass(m_DrawBlurUIPass);
				renderer.EnqueuePass(m_BlurPass);
				renderer.EnqueuePass(m_DrawUIPass);
				return;
			}

			renderer.EnqueuePass(m_DrawUIPass);
		}

		public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
			if (renderer == null || m_DrawUIPass == null)
			{
				return;
			}

			if (SekaiUIEffectSettings.Blur.IsActive && IsFinalCamera(renderingData.cameraData.camera))
			{
				SetupUICameraPass(renderer, in renderingData);
			}
			else
			{
				SetupDefaultCameraPass(renderer, in renderingData);
			}
		}

		protected override void Dispose(bool disposing)
		{
			m_BlurPass?.Cleanup();
			m_SetupPass?.Dispose();
			m_UIBuffer?.Dispose();
		}

		private void SetupUICameraPass(ScriptableRenderer renderer, in RenderingData renderingData)
		{
			var colorTarget = renderer.cameraColorTargetHandle;
			var depthTarget = renderer.cameraDepthTargetHandle;
			var colorDescriptor = GetDescriptor(colorTarget, renderingData.cameraData.cameraTargetDescriptor);
			var depthDescriptor = GetDescriptor(depthTarget, renderingData.cameraData.cameraTargetDescriptor);
			var cameraLayerMask = GetCameraLayerMask(renderingData.cameraData.camera);

			m_SetupPass.renderPassEvent = RenderPassEvent.BeforeRendering;
			m_SetupPass.Setup(m_UIBuffer, colorDescriptor, depthDescriptor);

			m_CopyScreenToCapturePass.renderPassEvent = (RenderPassEvent)1;
			m_CopyScreenToCapturePass.Setup(colorTarget, SekaiUIBuffer.CaptureColorTexHandle);

			m_DrawBlurUIPass.renderPassEvent = (RenderPassEvent)m_PassSettings.Event_DrawBlurUI;
			m_DrawBlurUIPass.Setup(
				cameraLayerMask & m_BlurSettings.BlurLayerMask,
				StencilState.defaultValue,
				0,
				SekaiUIBuffer.CaptureColorTexHandle,
				SekaiUIBuffer.CaptureDepthTexHandle,
				false,
				m_BlurSettings.RenderQueueRange);

			m_BlurPass.renderPassEvent = (RenderPassEvent)m_PassSettings.Event_Blur;
			m_BlurPass.Setup(SekaiUIBuffer.CaptureColorTexHandle, SekaiUIBuffer.UIBlurTexHandle);

			m_DrawUIPass.renderPassEvent = (RenderPassEvent)m_PassSettings.Event_DrawUI;
			m_DrawUIPass.Setup(
				cameraLayerMask & m_BlurSettings.UILayerMask,
				StencilState.defaultValue,
				0,
				colorTarget,
				depthTarget,
				true,
				m_BlurSettings.RenderQueueRange);
		}

		private void SetupDefaultCameraPass(ScriptableRenderer renderer, in RenderingData renderingData)
		{
			m_DrawUIPass.renderPassEvent = (RenderPassEvent)m_AreaCameraSettings.Event_DrawUI;
			m_DrawUIPass.Setup(
				GetCameraLayerMask(renderingData.cameraData.camera),
				StencilState.defaultValue,
				0,
				renderer.cameraColorTargetHandle,
				renderer.cameraDepthTargetHandle,
				true,
				m_BlurSettings.RenderQueueRange);
		}

		private static LayerMask GetCameraLayerMask(Camera camera)
		{
			return camera != null ? camera.cullingMask : ~0;
		}

		private static RenderTextureDescriptor GetDescriptor(RTHandle handle, RenderTextureDescriptor fallback)
		{
			return handle != null && handle.rt != null ? handle.rt.descriptor : fallback;
		}

		private static bool IsFinalCamera(Camera camera)
		{
			if (camera == null)
			{
				return true;
			}

			return camera.TryGetComponent<UnityEngine.Rendering.Universal.SekaiAdditionalCameraData>(out var data)
				? data.IsFinalCamera
				: true;
		}
	}
}
