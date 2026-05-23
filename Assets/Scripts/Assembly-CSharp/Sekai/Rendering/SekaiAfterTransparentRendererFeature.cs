using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiAfterTransparentRendererFeature : ScriptableRendererFeature
	{
		public override void Create()
		{
			// TODO: Port distortion/after-transparent passes from rendering_tmp/Sekai.Rendering.
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}

		public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
		}
	}
}
