using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Sekai.Rendering
{
	public class SekaiCharacterOutlinePass : ScriptableRenderPass
	{
		private static class PropertyId
		{
			public static readonly int OutlineFactorId = Shader.PropertyToID("_SekaiOutlineFactor");
			public static readonly int OutlineWidthId = Shader.PropertyToID("_SekaiOutlineWidth");
		}

		[Serializable]
		public class OutlineSettings
		{
			public float outlineWidthMin = 0.04f;
			public float outlineWidthMax = 0.95f;
			public float outlineDistanceNear = 0.45f;
			public float outlineDistanceFar = 20f;
			public AnimationCurve fovCurve = AnimationCurve.Linear(0f, 1f, 100f, 1f);
		}

		private OutlineSettings m_Settings;

		public void Setup(OutlineSettings settings)
		{
			m_Settings = settings;
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UpdateOutline(renderingData.cameraData.camera);
		}

		private void UpdateOutline(Camera camera)
		{
			if (camera == null || m_Settings == null)
			{
				return;
			}

			var fov = camera.fieldOfView;
			var fovScale = m_Settings.fovCurve != null ? m_Settings.fovCurve.Evaluate(fov) : fov;
			if (Mathf.Approximately(fovScale, 0f))
			{
				fovScale = 1f;
			}

			Shader.SetGlobalVector(
				PropertyId.OutlineWidthId,
				new Vector4(m_Settings.outlineWidthMin * 0.01f, m_Settings.outlineWidthMax * 0.01f, 0f, 0f));
			Shader.SetGlobalVector(
				PropertyId.OutlineFactorId,
				new Vector4(
					m_Settings.outlineDistanceNear,
					1f / (m_Settings.outlineDistanceFar - m_Settings.outlineDistanceNear),
					fov / fovScale,
					0f));
		}
	}
}
