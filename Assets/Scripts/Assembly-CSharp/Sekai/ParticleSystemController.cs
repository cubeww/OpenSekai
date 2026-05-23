using UnityEngine;

namespace Sekai
{
	public class ParticleSystemController : MonoBehaviour
	{
		private ParticleSystem particleSystem;

		private ParticleSystem[] particleSystems;

		private ParticleSystemRenderer[] renderers;

		private bool isPlayInternal;

		public bool IsPlaying
		{
			get
			{
				int particleCount = 0;
				if (particleSystems != null)
				{
					for (int i = 0; i < particleSystems.Length; i++)
					{
						if (particleSystems[i] != null)
						{
							particleCount += particleSystems[i].particleCount;
						}
					}
				}

				return particleCount > 0 || (particleSystem != null && particleSystem.isPlaying);
			}
		}

		private void Awake()
		{
			particleSystem = GetComponentInChildren<ParticleSystem>(true);
			particleSystems = GetComponentsInChildren<ParticleSystem>(true);
			renderers = GetComponentsInChildren<ParticleSystemRenderer>(true);

			if (particleSystem != null)
			{
				particleSystem.Clear(true);
				particleSystem.Stop(true);
			}

			if (particleSystems == null)
			{
				return;
			}

			for (int i = 0; i < particleSystems.Length; i++)
			{
				if (particleSystems[i] == null)
				{
					continue;
				}

				particleSystems[i].Clear(true);
				particleSystems[i].Stop(true);
			}
		}

		private void Update()
		{
			if (isPlayInternal && !IsPlaying)
			{
				Stop();
			}
		}

		public void Play()
		{
			if (isPlayInternal)
			{
				if (particleSystem != null)
				{
					particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
				}

				if (particleSystems != null)
				{
					for (int i = 0; i < particleSystems.Length; i++)
					{
						particleSystems[i]?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
					}
				}
			}

			if (particleSystems != null)
			{
				for (int i = 0; i < particleSystems.Length; i++)
				{
					particleSystems[i]?.Play(true);
				}
			}

			if (particleSystem != null)
			{
				particleSystem.Play(true);
			}

			isPlayInternal = true;
		}

		public void Stop()
		{
			if (particleSystem != null)
			{
				particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			}

			if (particleSystems != null)
			{
				for (int i = 0; i < particleSystems.Length; i++)
				{
					particleSystems[i]?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
				}
			}

			isPlayInternal = false;
		}

		public ParticleSystemController()
		{
		}
	}
}
