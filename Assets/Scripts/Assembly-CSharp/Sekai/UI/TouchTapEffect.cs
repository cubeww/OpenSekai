using UnityEngine;

namespace Sekai.UI
{
	public class TouchTapEffect : MonoBehaviour
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
				particle?.Play();
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
				particle?.Stop();
			}
		}

		public TouchTapEffect()
		{
		}
	}
}
