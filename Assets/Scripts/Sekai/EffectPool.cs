using Sekai.Core.Live;
using UnityEngine;

namespace Sekai
{
	public class EffectPool : MonoBehaviour
	{
		private ParticleSystemController[] pool;

		private int spawnIndex;

		public void Setup(GameObject prefab, int poolCount, BaseLiveController liveController = null)
		{
			ClearChildren();

			if (prefab == null || poolCount <= 0)
			{
				pool = null;
				return;
			}

			pool = new ParticleSystemController[poolCount];
			for (int i = 0; i < pool.Length; i++)
			{
				GameObject instance = Object.Instantiate(prefab, transform, false);
				instance.name = prefab.name;
				ParticleSystemController controller = instance.AddComponent<ParticleSystemController>();
				controller.RegisterToLiveController(liveController);
				controller.Stop();
				pool[i] = controller;
			}
			spawnIndex = 0;

			// Original warms the first pooled instance once, which also primes particle renderers/material state.
			pool[0].Play();
			pool[0].Stop();
		}

		public ParticleSystemController Spawn()
		{
			if (pool == null || pool.Length == 0)
			{
				return null;
			}

			ParticleSystemController result = pool[spawnIndex];
			spawnIndex = (spawnIndex + 1) % pool.Length;
			return result;
		}

		private void ClearChildren()
		{
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				Transform child = transform.GetChild(i);
				if (Application.isPlaying)
				{
					Object.Destroy(child.gameObject);
				}
				else
				{
					Object.DestroyImmediate(child.gameObject);
				}
			}
		}
	}
}
