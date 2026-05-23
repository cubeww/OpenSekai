using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiUIBuffer
	{
		private static class ShaderPropertyId
		{
			public const string CaptureTexName = "_CaptureTex";
			public const string CaptureDepthTexName = "_CaptureDepthTex";
			public const string UIBlurTexName = "_UIBlurTex";
			public static readonly int UIBlurTexId = Shader.PropertyToID(UIBlurTexName);
			public const string BlurTempName = "_BlurTemp";
		}

		public static RTHandle CaptureColorTexHandle;
		public static RTHandle CaptureDepthTexHandle;
		public static RTHandle BlurTempHandle;
		public static RTHandle UIBlurTexHandle;
		public static bool RequireDisposeUIBlurTexHandle;

		private static RenderTextureDescriptor GetCaptureTexDescriptor(RenderTextureDescriptor cameraColorDesc)
		{
			var desc = cameraColorDesc;
			var scale = SekaiUIEffectSettings.Blur.CaptureResolutionScale;
			desc.width = Mathf.Max(1, Mathf.RoundToInt(desc.width * scale));
			desc.height = Mathf.Max(1, Mathf.RoundToInt(desc.height * scale));
			desc.mipCount = 0;
			desc.autoGenerateMips = false;
			desc.useMipMap = false;
			desc.msaaSamples = 1;
			desc.depthBufferBits = 0;
			return desc;
		}

		private static RenderTextureDescriptor GetCaptureDepthTexDescriptor(RenderTextureDescriptor cameraDepthDesc)
		{
			var desc = cameraDepthDesc;
			var scale = SekaiUIEffectSettings.Blur.CaptureResolutionScale;
			desc.width = Mathf.Max(1, Mathf.RoundToInt(desc.width * scale));
			desc.height = Mathf.Max(1, Mathf.RoundToInt(desc.height * scale));
			return desc;
		}

		private static RenderTextureDescriptor GetUIBlurTexDescriptor(RenderTextureDescriptor cameraColorDesc)
		{
			var desc = GetCaptureTexDescriptor(cameraColorDesc);
			desc.stencilFormat = GraphicsFormat.None;
			return desc;
		}

		private static RenderTextureDescriptor GetBlurTempTexDescriptor(RenderTextureDescriptor cameraColorDesc)
		{
			return GetUIBlurTexDescriptor(cameraColorDesc);
		}

		public void TryGetRTHandle(RenderTextureDescriptor cameraColorDesc, RenderTextureDescriptor cameraDepthDesc)
		{
			AllocRTHandle(ref UIBlurTexHandle, GetUIBlurTexDescriptor(cameraColorDesc), ShaderPropertyId.UIBlurTexName);
			AllocRTHandle(ref CaptureColorTexHandle, GetCaptureTexDescriptor(cameraColorDesc), ShaderPropertyId.CaptureTexName);
			AllocRTHandle(ref BlurTempHandle, GetBlurTempTexDescriptor(cameraColorDesc), ShaderPropertyId.BlurTempName);
			AllocRTHandle(ref CaptureDepthTexHandle, GetCaptureDepthTexDescriptor(cameraDepthDesc), ShaderPropertyId.CaptureDepthTexName);
			Shader.SetGlobalTexture(ShaderPropertyId.UIBlurTexId, UIBlurTexHandle?.rt);
		}

		private void AllocRTHandle(ref RTHandle handle, RenderTextureDescriptor desc, string idName)
		{
			RenderingUtils.ReAllocateIfNeeded(ref handle, desc, FilterMode.Bilinear, TextureWrapMode.Clamp, name: idName);
		}

		public void Dispose()
		{
			CaptureColorTexHandle?.Release();
			CaptureDepthTexHandle?.Release();
			BlurTempHandle?.Release();
			if (RequireDisposeUIBlurTexHandle)
			{
				UIBlurTexHandle?.Release();
				UIBlurTexHandle = null;
			}

			CaptureColorTexHandle = null;
			CaptureDepthTexHandle = null;
			BlurTempHandle = null;
		}
	}
}
