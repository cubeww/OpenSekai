using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class ScenarioPostProcessRendererFeature : ScriptableRendererFeature
	{
		public Shader shader;

		public override void Create()
		{
			// TODO: Port ScenarioPostProcessRenderPass from rendering_tmp/Sekai.Rendering.
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}

		public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
		}
	}
}
