using System;
using System.Collections;
using UnityEngine;

namespace Sekai
{
	public class AssetLoader
	{
		public enum LoadState
		{
			Ready = 0,
			Loading = 1,
			Done = 2,
			Error = 3
		}

		protected IEnumerator loadCoroutine;
		protected Action<AssetLoader> onFinish;
		protected LoadState resourceLoadState;

		public virtual string AssetName => string.Empty;

		public virtual LoadState ResourceLoadState => resourceLoadState;

		public AssetLoader()
		{
			resourceLoadState = LoadState.Ready;
		}

		public virtual IEnumerator WaitForLoadedResource()
		{
			while (ResourceLoadState == LoadState.Loading)
			{
				yield return null;
			}
		}

		public virtual T LoadResource<T>(string name) where T : UnityEngine.Object
		{
			return null;
		}

		public virtual int LoadResourceAsync<T>(string name, MonoBehaviour mb, Action<T> onFinish) where T : UnityEngine.Object
		{
			T resource = LoadResource<T>(name);
			onFinish?.Invoke(resource);
			return 0;
		}

		public virtual T GetAsyncLoadedResource<T>() where T : UnityEngine.Object
		{
			return null;
		}

		public virtual void Unload(bool unloadLoadedAssets = false)
		{
		}

		public virtual void Unload()
		{
			Unload(false);
		}

		public virtual void UnloadImmediate(bool unloadLoadedAssets = false)
		{
			Unload(unloadLoadedAssets);
		}

		public virtual void OnDestroyUnload()
		{
			Unload(false);
		}
	}
}
