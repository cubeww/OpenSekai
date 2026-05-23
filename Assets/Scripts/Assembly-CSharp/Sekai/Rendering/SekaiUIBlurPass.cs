using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiUIBlurPass : ScriptableRenderPass
	{
		private static readonly int SamplingDistance = Shader.PropertyToID("_SamplingDistance");
		private static readonly int BlurResolutionParams = Shader.PropertyToID("_BlurResolutionParams");

		private readonly ProfilingSampler m_ProfilingSampler;
		private readonly Material m_UIEffectMaterial;
		private RTHandle m_Source;
		private RTHandle m_Destination;

		public SekaiUIBlurPass(string profilerTag)
		{
			profilingSampler = new ProfilingSampler(nameof(SekaiUIBlurPass));
			m_ProfilingSampler = new ProfilingSampler(profilerTag);
			var shader = Shader.Find("Hidden/CP/PostEffect/UIEffect");
			if (shader == null)
			{
				shader = Shader.Find("Sekai/UI/UIGaussianBlur");
			}

			if (shader != null)
			{
				m_UIEffectMaterial = CoreUtils.CreateEngineMaterial(shader);
			}
		}

		public void Setup(RTHandle source, RTHandle dest)
		{
			m_Source = source;
			m_Destination = dest;
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (m_Source == null || m_Destination == null || SekaiUIBuffer.BlurTempHandle == null || m_UIEffectMaterial == null)
			{
				return;
			}

			var cmd = CommandBufferPool.Get();
			using (new ProfilingScope(cmd, m_ProfilingSampler))
			{
				ExecuteBlur(cmd, ref renderingData);
			}

			context.ExecuteCommandBuffer(cmd);
			CommandBufferPool.Release(cmd);
		}

		private void ExecuteBlur(CommandBuffer cmd, ref RenderingData renderingData)
		{
			cmd.SetGlobalFloat(SamplingDistance, SekaiUIEffectSettings.Blur.BlurSamplingDistance);
			var descriptor = renderingData.cameraData.cameraTargetDescriptor;
			cmd.SetGlobalVector(
				BlurResolutionParams,
				new Vector4(1f / descriptor.width, 1f / descriptor.height, 0f, 0f));
			Blitter.BlitCameraTexture(cmd, m_Source, SekaiUIBuffer.BlurTempHandle, m_UIEffectMaterial, 0);
			Blitter.BlitCameraTexture(cmd, SekaiUIBuffer.BlurTempHandle, m_Destination, m_UIEffectMaterial, 1);
		}

		public void Cleanup()
		{
			CoreUtils.Destroy(m_UIEffectMaterial);
		}
	}
}
