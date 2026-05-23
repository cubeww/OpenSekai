using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiBeforePostProcessRendererFeature : ScriptableRendererFeature
	{
		[Serializable]
		protected class SekaiBeforePostProcessSettings
		{
			public RenderPassEvent Event = RenderPassEvent.AfterRenderingPostProcessing;
			public RenderQueueType RenderQueueType = RenderQueueType.Transparent;
			public bool UseCustomRenderQueueRange;
			public int RenderQueueLowerBound;
			public int RenderQueueUpperBound;
		}

		[SerializeField]
		private SekaiBeforePostProcessSettings m_BeforePostProcessSettings = new SekaiBeforePostProcessSettings();

		public override void Create()
		{
			// TODO: Port SekaiMeshFlareParaPass from rendering_tmp/Sekai.Rendering.
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
		}

		public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
		}
	}
}
