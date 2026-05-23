using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiPostProcessRendererFeature : ScriptableRendererFeature
	{
		public override void Create()
		{
			// TODO: Port SekaiPostProcessPass from rendering_tmp/Sekai.Rendering.PostPrcessV2.
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}

		public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
		}
	}
}
