using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiAfterPostProcessRendererFeature : ScriptableRendererFeature
	{
		[SerializeField]
		private LayerMask _targetLayer;

		[SerializeField]
		private Shader _fadeOutBlendShader;

		public override void Create()
		{
			// TODO: Port SekaiAfterPostProcessRenderPass from rendering_tmp/Sekai.Rendering.
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}

		public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
		}
	}
}
