using UnityEngine;

namespace Sekai.UI
{
	public class TouchFollowEffect : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem[] particles;

		[SerializeField]
		private Transform root;

		public Transform Root
		{
			get
			{
				return root;
			}
		}

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

		public TouchFollowEffect()
		{
		}
	}
}
