using UnityEngine.Rendering.Universal;

namespace OverdrawForURP
{
	public class MysekaiOverdrawRendererFeature : ScriptableRendererFeature
	{
		public override void Create()
		{
			// TODO: Port MysekaiOverdrawPass from rendering_tmp/OverdrawForURP if overdraw debugging is needed.
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}
	}
}
