using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiDrawUIPass : ScriptableRenderPass
	{
		private static readonly int s_DrawObjectPassDataPropID = Shader.PropertyToID("_DrawObjectPassData");
		private static readonly int s_ScaleBiasRtPropID = Shader.PropertyToID("_ScaleBiasRt");
		private static readonly int s_ScreenParamsPropID = Shader.PropertyToID("_ScreenParams");

		private readonly ProfilingSampler m_ProfilingSampler;
		private ShaderTagId m_ShaderTagId = new ShaderTagId(SekaiShaderTag.GetTag(SekaiShaderTagType.Default));
		private FilteringSettings m_FilteringSettings = new FilteringSettings(RenderQueueRange.transparent, ~0);
		private RenderStateBlock m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
		private RTHandle m_ColorTargetHandle;
		private RTHandle m_DepthTargetHandle;
		private bool m_IsCameraRenderTarget;

		public SekaiDrawUIPass(string profilerTag)
		{
			profilingSampler = new ProfilingSampler(nameof(SekaiDrawUIPass));
			m_ProfilingSampler = new ProfilingSampler(profilerTag);
		}

		public void Setup(
			LayerMask layerMask,
			StencilState stencilState,
			int stencilReference,
			RTHandle colorTargetHandle,
			RTHandle depthTargetHandle,
			bool isCameraRenderTarget,
			RenderQueueRange renderQueueRange)
		{
			m_ShaderTagId = new ShaderTagId(SekaiShaderTag.GetTag(SekaiShaderTagType.Default));
			m_FilteringSettings = new FilteringSettings(renderQueueRange, layerMask);
			m_ColorTargetHandle = colorTargetHandle;
			m_DepthTargetHandle = depthTargetHandle;
			m_IsCameraRenderTarget = isCameraRenderTarget;
			m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			if (stencilState.enabled)
			{
				m_RenderStateBlock.stencilReference = stencilReference;
				m_RenderStateBlock.mask = RenderStateMask.Stencil;
				m_RenderStateBlock.stencilState = stencilState;
			}
		}

		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			base.OnCameraSetup(cmd, ref renderingData);
			ConfigureColorStoreAction(RenderBufferStoreAction.Store);
			ConfigureDepthStoreAction(RenderBufferStoreAction.DontCare);
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			base.Configure(cmd, cameraTextureDescriptor);
			if (m_ColorTargetHandle != null && m_DepthTargetHandle != null)
			{
				ConfigureTarget(m_ColorTargetHandle, m_DepthTargetHandle);
			}

			ConfigureClear(ClearFlag.Depth, Color.black);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (m_ColorTargetHandle == null)
			{
				return;
			}

			var cmd = CommandBufferPool.Get();
			using (new ProfilingScope(cmd, m_ProfilingSampler))
			{
				cmd.SetGlobalVector(s_DrawObjectPassDataPropID, Vector4.zero);
				cmd.SetGlobalVector(s_ScaleBiasRtPropID, GetScaleBiasRt(ref renderingData));
				cmd.SetGlobalVector(s_ScreenParamsPropID, GetScreenParams(ref renderingData));
			}

			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();

			var drawingSettings = CreateDrawingSettings(m_ShaderTagId, ref renderingData, (SortingCriteria)23);
			var filteringSettings = m_FilteringSettings;
			context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref m_RenderStateBlock);
			CommandBufferPool.Release(cmd);
		}

		private Vector4 GetScaleBiasRt(ref RenderingData renderingData)
		{
			var yFlip = renderingData.cameraData.IsCameraProjectionMatrixFlipped();
			var flipSign = yFlip ? -1f : 1f;
			return flipSign < 0f
				? new Vector4(flipSign, 1f, -1f, 1f)
				: new Vector4(flipSign, 0f, 1f, 1f);
		}

		private Vector4 GetScreenParams(ref RenderingData renderingData)
		{
			var descriptor = renderingData.cameraData.cameraTargetDescriptor;
			var width = descriptor.width;
			var height = descriptor.height;
			if (!m_IsCameraRenderTarget)
			{
				var scale = SekaiUIEffectSettings.Blur.CaptureResolutionScale;
				width = Mathf.Max(1, Mathf.RoundToInt(width * scale));
				height = Mathf.Max(1, Mathf.RoundToInt(height * scale));
			}

			return new Vector4(width, height, 1f + 1f / width, 1f + 1f / height);
		}
	}
}
