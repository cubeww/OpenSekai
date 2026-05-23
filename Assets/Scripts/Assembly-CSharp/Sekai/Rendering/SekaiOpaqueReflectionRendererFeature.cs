using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiOpaqueReflectionRendererFeature : SekaiDrawObjectsRendererFeature
	{
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			EnqueuePasses(Passes, renderer);
		}
	}
}
