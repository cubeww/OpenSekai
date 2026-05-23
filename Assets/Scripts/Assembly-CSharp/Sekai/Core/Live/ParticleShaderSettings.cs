using System.Collections.Generic;
using UnityEngine;

namespace Sekai.Core.Live
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleShaderSettings : MonoBehaviour
	{
		public enum Mode
		{
			Additive = 0,
			AlphaBlend = 1
		}

		[SerializeField]
		private Mode mode;

		private List<ParticleSystemVertexStream> streams;

		private void Awake()
		{
			UpdateMode();
		}

		private void UpdateMode()
		{
			ParticleSystemRenderer particleRenderer = GetComponent<ParticleSystemRenderer>();
			if (particleRenderer != null)
			{
				streams ??= new List<ParticleSystemVertexStream>();
				streams.Clear();
				particleRenderer.GetActiveVertexStreams(streams);
				if (!streams.Contains(ParticleSystemVertexStream.Custom1X))
				{
					streams.Add(ParticleSystemVertexStream.Custom1X);
				}
				particleRenderer.SetActiveVertexStreams(streams);
			}

			ParticleSystem particle = GetComponent<ParticleSystem>();
			if (particle == null)
			{
				return;
			}
			ParticleSystem.CustomDataModule customData = particle.customData;
			customData.enabled = true;
			customData.SetMode(ParticleSystemCustomData.Custom1, ParticleSystemCustomDataMode.Vector);
			customData.SetVectorComponentCount(ParticleSystemCustomData.Custom1, 1);
			customData.SetVector(ParticleSystemCustomData.Custom1, 0, new ParticleSystem.MinMaxCurve(mode == Mode.Additive ? 1f : 0f));
		}

		public ParticleShaderSettings()
		{
			streams = new List<ParticleSystemVertexStream>();
		}
	}
}
