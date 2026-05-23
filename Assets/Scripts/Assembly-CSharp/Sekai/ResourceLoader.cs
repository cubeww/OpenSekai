using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
	public class ResourceLoader : AssetLoader
	{
		protected List<UnityEngine.Object> resources;
		private string resourceName;
		private ResourceRequest request;

		public override string AssetName => resourceName;

		public UnityEngine.Object Resource => resources != null && resources.Count > 0 ? resources[0] : null;

		public List<UnityEngine.Object> AllResources => resources;

		public ResourceLoader()
		{
			resources = new List<UnityEngine.Object>();
		}

		public override T LoadResource<T>(string name)
		{
			resourceName = name;
			resourceLoadState = LoadState.Loading;
			T resource = Resources.Load<T>(name);
			resources.Clear();
			if (resource != null)
			{
				resources.Add(resource);
				resourceLoadState = LoadState.Done;
			}
			else
			{
				resourceLoadState = LoadState.Error;
			}
			return resource;
		}

		public T[] LoadResourcesAll<T>(string name) where T : UnityEngine.Object
		{
			resourceName = name;
			resourceLoadState = LoadState.Loading;
			T[] loaded = Resources.LoadAll<T>(name);
			resources.Clear();
			if (loaded != null)
			{
				resources.AddRange(loaded);
			}
			resourceLoadState = loaded != null ? LoadState.Done : LoadState.Error;
			return loaded;
		}

		public override int LoadResourceAsync<T>(string name, MonoBehaviour mb, Action<T> onFinish)
		{
			resourceName = name;
			resourceLoadState = LoadState.Loading;
			loadCoroutine = LoadResourceAsyncCore(name, onFinish);
			if (mb != null)
			{
				mb.StartCoroutine(loadCoroutine);
			}
			else
			{
				T resource = LoadResource<T>(name);
				onFinish?.Invoke(resource);
			}
			return 0;
		}

		public IEnumerator LoadResourceAsyncCore<T>(string name, Action<T> onFinish) where T : UnityEngine.Object
		{
			request = Resources.LoadAsync<T>(name);
			yield return request;
			resources.Clear();
			T resource = request.asset as T;
			if (resource != null)
			{
				resources.Add(resource);
				resourceLoadState = LoadState.Done;
			}
			else
			{
				resourceLoadState = LoadState.Error;
			}
			onFinish?.Invoke(resource);
		}

		public override T GetAsyncLoadedResource<T>()
		{
			return Resource as T;
		}

		public override void Unload(bool unloadLoadedAssets)
		{
			resources.Clear();
			resourceLoadState = LoadState.Ready;
			request = null;
		}

		public override void Unload()
		{
			Unload(false);
		}

		public override void OnDestroyUnload()
		{
			Unload(false);
		}
	}
}
