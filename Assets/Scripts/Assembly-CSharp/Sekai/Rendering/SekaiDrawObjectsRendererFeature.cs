using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiDrawObjectsRendererFeature : ScriptableRendererFeature
	{
		[Serializable]
		protected enum SekaiDrawObjectsRenderQueueType
		{
			opaque = 0,
			transparent = 1
		}

		[Serializable]
		protected class SekaiDrawObjectsSettings
		{
			public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;
			public SekaiDrawObjectsRenderQueueType RenderQueueType = SekaiDrawObjectsRenderQueueType.opaque;
			public bool UseCustomRenderQueueRange;
			public int RenderQueueLowerBound;
			public int RenderQueueUpperBound;
			public SekaiShaderTagType[] ShaderTagTypes = Array.Empty<SekaiShaderTagType>();
		}

		[SerializeField]
		private SekaiDrawObjectsSettings m_Settings = new SekaiDrawObjectsSettings();

		private SekaiDrawObjectsPass[] m_Passes;

		protected SekaiDrawObjectsSettings Settings => m_Settings;

		protected SekaiDrawObjectsPass[] Passes => m_Passes;

		public override void Create()
		{
			CreatePasses(m_Settings, out m_Passes);
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			EnqueuePasses(m_Passes, renderer);
		}

		public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
			SetupRenderPass(m_Settings, m_Passes, renderingData.cameraData.camera);
		}

		protected void CreatePasses(in SekaiDrawObjectsSettings settings, out SekaiDrawObjectsPass[] passes)
		{
			if (settings == null || settings.ShaderTagTypes == null || settings.ShaderTagTypes.Length == 0)
			{
				passes = null;
				return;
			}

			var renderQueueRange = settings.UseCustomRenderQueueRange
				? new RenderQueueRange(settings.RenderQueueLowerBound, settings.RenderQueueUpperBound)
				: settings.RenderQueueType == SekaiDrawObjectsRenderQueueType.transparent
					? RenderQueueRange.transparent
					: RenderQueueRange.opaque;
			var isOpaque = settings.RenderQueueType == SekaiDrawObjectsRenderQueueType.opaque;
			passes = new SekaiDrawObjectsPass[settings.ShaderTagTypes.Length];

			for (var i = 0; i < settings.ShaderTagTypes.Length; i++)
			{
				passes[i] = new SekaiDrawObjectsPass(name, settings.ShaderTagTypes[i], isOpaque, settings.Event, renderQueueRange);
			}
		}

		protected void EnqueuePasses(in SekaiDrawObjectsPass[] passes, ScriptableRenderer renderer)
		{
			if (passes == null || renderer == null)
			{
				return;
			}

			foreach (var pass in passes)
			{
				if (pass != null)
				{
					renderer.EnqueuePass(pass);
				}
			}
		}

		private void SetupRenderPass(in SekaiDrawObjectsSettings settings, in SekaiDrawObjectsPass[] passes, Camera camera)
		{
			if (settings == null || passes == null)
			{
				return;
			}

			var layerMask = camera != null ? camera.cullingMask : ~0;
			foreach (var pass in passes)
			{
				pass?.Setup(layerMask, StencilState.defaultValue, 0);
			}
		}
	}
}
