using UnityEngine;

namespace Sekai.UI
{
	public class TouchLongTapEffect : MonoBehaviour
	{
		[SerializeField] private ParticleSystem[] particles;

		[SerializeField] private float longTapSec = 0.4f;

		private float longTapSecCnt;

		public void StartEffect()
		{
			longTapSecCnt = 0f;
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

		public void EndEffect()
		{
			for (int i = 0; particles != null && i < particles.Length; i++)
			{
				particles[i]?.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
		}

		public void StopEffect()
		{
			longTapSecCnt = 0f;
			for (int i = 0; particles != null && i < particles.Length; i++)
			{
				particles[i]?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			}
		}

		public bool CountLongTap()
		{
			longTapSecCnt += Time.deltaTime;
			if (longTapSecCnt < longTapSec)
			{
				return false;
			}
			longTapSecCnt = 0f;
			return true;
		}
	}
}
