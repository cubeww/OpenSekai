using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class PlanarReflectionFeature : ScriptableRendererFeature
	{
		[SerializeField]
		private Shader _drawStencilShader;

		[SerializeField]
		private PlanarReflectionInfo _planarReflectionInfo;

		public override void Create()
		{
			// TODO: Port PlanarReflectionPass from rendering_tmp/Sekai.Rendering.
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}
	}
}
