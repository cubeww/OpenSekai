using Sekai.Core.Live;
using UnityEngine;

namespace Sekai
{
	public class ParticleSystemController : MonoBehaviour
	{
		private ParticleSystem particleSystem;

		private ParticleSystem[] particleSystems;

		private ParticleSystemRenderer[] renderers;

		private bool isPlayInternal;

		private int playFrame = -1;

		private BaseLiveController liveController;

		public bool IsPlaying
		{
			get
			{
				if (particleSystems == null)
				{
					CacheComponents();
				}

				int particleCount = 0;
				for (int i = 0; particleSystems != null && i < particleSystems.Length; i++)
				{
					if (particleSystems[i] != null)
					{
						particleCount += particleSystems[i].particleCount;
					}
				}
				return particleCount > 0 || (particleSystem != null && particleSystem.isPlaying);
			}
		}

		private void Awake()
		{
			CacheComponents();
			if (particleSystem != null)
			{
				particleSystem.Clear(true);
				particleSystem.Stop(true);
			}
		}

		private void Start()
		{
			RegisterToLiveController(GetComponentInParent<BaseLiveController>());
			if (!isPlayInternal)
			{
				Stop();
			}
		}

		public void OnUpdate()
		{
			if (isPlayInternal && Time.frameCount > playFrame && !IsPlaying)
			{
				Stop();
			}
		}

		private void OnDestroy()
		{
			if (liveController != null)
			{
				liveController.UnregisterParticleSystemController(this);
				liveController = null;
			}

			particleSystem = null;
			particleSystems = null;
			renderers = null;
		}

		public void RegisterToLiveController(BaseLiveController controller)
		{
			if (controller == null || liveController == controller)
			{
				return;
			}

			if (liveController != null)
			{
				liveController.UnregisterParticleSystemController(this);
			}

			liveController = controller;
			liveController.RegisterParticleSystemController(this);
		}

		public void Play()
		{
			if (particleSystems == null)
			{
				CacheComponents();
			}

			if (isPlayInternal && particleSystem != null)
			{
				particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
			particleSystem?.Play(true);
			playFrame = Time.frameCount;
			isPlayInternal = true;
		}

		public void Stop()
		{
			if (particleSystems == null)
			{
				CacheComponents();
			}

			particleSystem?.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			isPlayInternal = false;
		}

		private void CacheComponents()
		{
			particleSystem = GetComponentInChildren<ParticleSystem>(true);
			particleSystems = GetComponentsInChildren<ParticleSystem>(true);
			renderers = GetComponentsInChildren<ParticleSystemRenderer>(true);
		}
	}
}
