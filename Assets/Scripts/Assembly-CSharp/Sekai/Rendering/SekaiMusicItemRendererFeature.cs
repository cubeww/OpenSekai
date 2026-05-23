using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiMusicItemRendererFeature : SekaiDrawObjectsRendererFeature
	{
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (SekaiMusicItemSettings.ExistTransparentMusicItem())
			{
				EnqueuePasses(Passes, renderer);
			}
		}
	}
}
