using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiCharacterOutlineFeature : ScriptableRendererFeature
	{
		[SerializeField]
		private SekaiCharacterOutlinePass.OutlineSettings settings = new SekaiCharacterOutlinePass.OutlineSettings();

		private SekaiCharacterOutlinePass pass;

		public override void Create()
		{
			pass = new SekaiCharacterOutlinePass();
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (pass != null)
			{
				renderer.EnqueuePass(pass);
			}
		}

		public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
			if (pass != null)
			{
				pass.Setup(settings);
				pass.renderPassEvent = RenderPassEvent.BeforeRendering;
			}
		}
	}
}
