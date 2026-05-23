using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiDrawObjectsPass : ScriptableRenderPass
	{
		private static readonly int s_DrawObjectPassDataPropID = Shader.PropertyToID("_DrawObjectPassData");
		private static readonly int s_ScaleBiasRtPropID = Shader.PropertyToID("_ScaleBiasRt");

		private readonly List<ShaderTagId> m_ShaderTagIdList = new List<ShaderTagId>();
		private readonly ProfilingSampler m_ProfilingSampler;
		private FilteringSettings m_FilteringSettings;
		private RenderStateBlock m_RenderStateBlock;
		private bool m_IsOpaque;

		public SekaiDrawObjectsPass(string profilerTag, ShaderTagId[] shaderTagIds, bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange)
		{
			profilingSampler = new ProfilingSampler(nameof(SekaiDrawObjectsPass));
			m_ProfilingSampler = new ProfilingSampler(profilerTag);
			renderPassEvent = evt;
			m_IsOpaque = opaque;
			m_FilteringSettings = new FilteringSettings(renderQueueRange, ~0);
			m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);

			foreach (var shaderTagId in shaderTagIds)
			{
				m_ShaderTagIdList.Add(shaderTagId);
			}
		}

		public SekaiDrawObjectsPass(string profilerTag, SekaiShaderTagType shaderTagType, bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange)
			: this(profilerTag, new[] { new ShaderTagId(SekaiShaderTag.GetTag(shaderTagType)) }, opaque, evt, renderQueueRange)
		{
		}

		internal void Setup(LayerMask layerMask, StencilState stencilState, int stencilReference)
		{
			m_FilteringSettings.layerMask = layerMask;
			m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			if (stencilState.enabled)
			{
				m_RenderStateBlock.stencilReference = stencilReference;
				m_RenderStateBlock.mask = RenderStateMask.Stencil;
				m_RenderStateBlock.stencilState = stencilState;
			}
		}

		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			var cmd = CommandBufferPool.Get();
			using (new ProfilingScope(cmd, m_ProfilingSampler))
			{
				cmd.SetGlobalVector(s_DrawObjectPassDataPropID, new Vector4(0f, 0f, 0f, m_IsOpaque ? 1f : 0f));

				var yFlip = renderingData.cameraData.IsCameraProjectionMatrixFlipped();
				var flipSign = yFlip ? -1f : 1f;
				var scaleBias = flipSign < 0f
					? new Vector4(flipSign, 1f, -1f, 1f)
					: new Vector4(flipSign, 0f, 1f, 1f);
				cmd.SetGlobalVector(s_ScaleBiasRtPropID, scaleBias);

				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();

				var sortingCriteria = m_IsOpaque
					? renderingData.cameraData.defaultOpaqueSortFlags
					: SortingCriteria.CommonTransparent;
				var drawingSettings = CreateDrawingSettings(m_ShaderTagIdList, ref renderingData, sortingCriteria);
				var filteringSettings = m_FilteringSettings;

#if UNITY_EDITOR
				if (renderingData.cameraData.isPreviewCamera)
				{
					filteringSettings.layerMask = -1;
				}
#endif

				context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings, ref m_RenderStateBlock);
			}

			CommandBufferPool.Release(cmd);
		}
	}
}
