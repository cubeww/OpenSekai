using UnityEngine;

namespace Sekai.UI
{
	public class TouchLongTapEffect : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem[] particles;

		[SerializeField]
		private float longTapSec;

		private float longTapSecCnt;

		public void StartEffect()
		{
			if (particles == null)
			{
				return;
			}

			foreach (ParticleSystem particle in particles)
			{
				if (particle == null)
				{
					continue;
				}

				var main = particle.main;
				main.loop = true;
				var emission = particle.emission;
				emission.enabled = true;
				if (!particle.isPlaying)
				{
					particle.Play();
				}
			}
		}

		public void EndEffect()
		{
			if (particles == null)
			{
				return;
			}

			foreach (ParticleSystem particle in particles)
			{
				if (particle == null)
				{
					continue;
				}

				var main = particle.main;
				main.loop = false;
				var emission = particle.emission;
				emission.enabled = false;
			}
		}

		public void StopEffect()
		{
			longTapSecCnt = 0f;
			if (particles == null)
			{
				return;
			}

			foreach (ParticleSystem particle in particles)
			{
				if (particle == null)
				{
					continue;
				}

				particle.Stop();
			}
		}

		public bool CountLongTap()
		{
			longTapSecCnt = Mathf.Min(longTapSecCnt + Time.deltaTime, longTapSec);
			return longTapSecCnt >= longTapSec;
		}

		public TouchLongTapEffect()
		{
		}
	}
}
