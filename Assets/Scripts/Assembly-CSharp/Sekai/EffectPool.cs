using UnityEngine;

namespace Sekai
{
	public class EffectPool : MonoBehaviour
	{
		private ParticleSystemController[] pool;

		private int spawnIndex;

		public void Setup(GameObject prefab, int poolCount)
		{
			pool = new ParticleSystemController[Mathf.Max(poolCount, 0)];
			for (int i = 0; i < pool.Length; i++)
			{
				if (prefab == null)
				{
					continue;
				}

				GameObject instance = Instantiate(prefab, transform);
				pool[i] = instance.AddComponent<ParticleSystemController>();
			}

			if (pool.Length > 0 && pool[0] != null)
			{
				pool[0].Play();
				pool[0].Stop();
			}
		}

		public ParticleSystemController Spawn()
		{
			if (pool == null || pool.Length == 0)
			{
				return null;
			}

			ParticleSystemController controller = pool[spawnIndex];
			spawnIndex = (spawnIndex + 1) % pool.Length;
			return controller;
		}

		public EffectPool()
		{
		}
	}
}
