using UnityEngine;

namespace Sekai.UI
{
	public class TouchTapEffect : MonoBehaviour
	{
		[SerializeField] private ParticleSystem[] particles;

		[SerializeField] private Transform root;

		public Transform Root => root != null ? root : transform;

		public void StartEffect()
		{
			SetRootActive(true);
			for (int i = 0; particles != null && i < particles.Length; i++)
			{
				ParticleSystem particle = particles[i];
				if (particle == null)
				{
					continue;
				}
				particle.Clear(true);
				particle.Play(true);
			}
		}

		public void StopEffect()
		{
			for (int i = 0; particles != null && i < particles.Length; i++)
			{
				particles[i]?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			}
			SetRootActive(false);
		}

		private void SetRootActive(bool isActive)
		{
			if (Root != null)
			{
				Root.gameObject.SetActive(isActive);
			}
		}
	}
}
