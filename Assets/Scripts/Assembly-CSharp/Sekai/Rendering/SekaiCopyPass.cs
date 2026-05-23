using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiCopyPass : ScriptableRenderPass
	{
		private readonly ProfilingSampler m_ProfilingSampler;
		private RTHandle m_Source;
		private RTHandle m_Dest;

		public SekaiCopyPass(string profilerTag)
		{
			profilingSampler = new ProfilingSampler(nameof(SekaiCopyPass));
			m_ProfilingSampler = new ProfilingSampler(profilerTag);
		}

		public void Setup(RTHandle source, RTHandle dest)
		{
			m_Source = source;
			m_Dest = dest;
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (m_Source == null || m_Dest == null)
			{
				return;
			}

			var cmd = CommandBufferPool.Get();
			using (new ProfilingScope(cmd, m_ProfilingSampler))
			{
				Blitter.BlitCameraTexture(cmd, m_Source, m_Dest);
			}

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}
	}
}
