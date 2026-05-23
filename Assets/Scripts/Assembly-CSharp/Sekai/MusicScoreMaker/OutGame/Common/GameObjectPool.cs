using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public abstract class GameObjectPool<TKey> where TKey : Enum
	{
		private readonly Transform _parent;

		private readonly Dictionary<TKey, GameObject> _pool;

		protected GameObjectPool([NotNull] Transform parent)
		{
			_parent = parent;
			_pool = new Dictionary<TKey, GameObject>();
		}

		public GameObject GetOrCreate(TKey key)
		{
			if (_pool.TryGetValue(key, out GameObject pooled) && pooled != null)
			{
				pooled.SetActive(true);
				return pooled;
			}

			GameObject gameObject = CreateGameObject(key);
			_pool[key] = gameObject;
			return gameObject;
		}

		public void ReleaseAll()
		{
			foreach (GameObject gameObject in _pool.Values)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
		}

		public void Clear()
		{
			foreach (GameObject gameObject in _pool.Values)
			{
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}

			_pool.Clear();
		}

		protected GameObject CreateGameObject(TKey key)
		{
			string prefabPath = GetPrefabPath(key);
			GameObject prefab = Resources.Load<GameObject>(prefabPath);
			GameObject gameObject = prefab != null ? UnityEngine.Object.Instantiate(prefab, _parent) : new GameObject(key.ToString());
			if (_parent != null && gameObject.transform.parent != _parent)
			{
				gameObject.transform.SetParent(_parent, false);
			}

			OnGameObjectCreated(key, gameObject);
			return gameObject;
		}

		protected abstract string GetPrefabPath(TKey key);

		protected virtual void OnGameObjectCreated(TKey key, GameObject gameObject)
		{
		}
	}
}
