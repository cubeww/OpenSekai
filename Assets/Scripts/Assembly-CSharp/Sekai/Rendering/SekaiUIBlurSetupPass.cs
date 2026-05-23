using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiUIBlurSetupPass : ScriptableRenderPass
	{
		private SekaiUIBuffer m_UIBuffer;
		private RenderTextureDescriptor m_ColorDescriptor;
		private RenderTextureDescriptor m_DepthDescriptor;

		public void Setup(SekaiUIBuffer uiBuffer, RenderTextureDescriptor colorDescriptor, RenderTextureDescriptor depthDescriptor)
		{
			m_UIBuffer = uiBuffer;
			m_ColorDescriptor = colorDescriptor;
			m_DepthDescriptor = depthDescriptor;
			m_UIBuffer?.TryGetRTHandle(m_ColorDescriptor, m_DepthDescriptor);
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		public void Dispose()
		{
			m_UIBuffer?.Dispose();
			m_UIBuffer = null;
		}
	}
}
