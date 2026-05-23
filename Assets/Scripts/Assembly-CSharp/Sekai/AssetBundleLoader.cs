using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sekai
{
	public class AssetBundleLoader : AssetLoader
	{
		public class LoadSnippet
		{
			public AssetBundleRequest AssetBundleRequest { get; set; }
			public LoadState LoadStatus { get; set; }
			public int Id { get; set; }
		}

		private AssetBundleResource assetBundle;
		private LoadState assetbundleLoadState;
		private string bundleName;
		private LoadSnippet mainLoadSnippet;
		protected Action<AssetBundleLoader> onFinishBundle;
		protected List<LoadSnippet> assetLoadSnippets;
		protected int nextLoadSnippetIndex;
		private Dictionary<string, UnityEngine.Object> resourceCacheMap;
		private Dictionary<string, UnityEngine.Object[]> resourceArrayCacheMap;

		public AssetBundleElement AssetBundleElement => assetBundle?.Element;

		public AssetBundleResource AssetBundleResource => assetBundle;

		public LoadState AssetBundleLoadState => assetbundleLoadState;

		public string BundleName => bundleName;

		public override string AssetName => bundleName;

		public AssetBundle AB => assetBundle?.Bundle;

		public float ResourceProgress => mainLoadSnippet?.AssetBundleRequest != null ? mainLoadSnippet.AssetBundleRequest.progress : 1f;

		public override LoadState ResourceLoadState => mainLoadSnippet?.LoadStatus ?? resourceLoadState;

		public AssetBundleLoader()
		{
			assetbundleLoadState = LoadState.Ready;
			resourceLoadState = LoadState.Ready;
			mainLoadSnippet = new LoadSnippet { Id = 0, LoadStatus = LoadState.Ready };
			assetLoadSnippets = new List<LoadSnippet>();
			resourceCacheMap = new Dictionary<string, UnityEngine.Object>();
			resourceArrayCacheMap = new Dictionary<string, UnityEngine.Object[]>();
		}

		public override IEnumerator WaitForLoadedResource()
		{
			while (ResourceLoadState == LoadState.Loading)
			{
				yield return null;
			}
		}

		public IEnumerator WaitForLoadedResource(int id)
		{
			LoadSnippet snippet = assetLoadSnippets.Find(x => x.Id == id);
			while (snippet != null && snippet.LoadStatus == LoadState.Loading)
			{
				yield return null;
			}
		}

		public void LoadAssetBundleAsync(string bundleName, MonoBehaviour mb, Action<AssetBundleLoader> onFinish, uint readBufferSize = 65536u)
		{
			onFinishBundle = onFinish;
			LoadAssetBundle(bundleName);
			onFinishBundle?.Invoke(this);
		}

		public void LoadAssetBundle(string bundleName)
		{
			this.bundleName = bundleName;
			assetbundleLoadState = LoadState.Loading;
			assetBundle = AssetBundleManager.Instance.LoadAssetBundle(bundleName);
			assetbundleLoadState = assetBundle?.Bundle != null ? LoadState.Done : LoadState.Error;
		}

		public string GetFullAssetName(string abbreviatedAssetName)
		{
			if (AB == null || string.IsNullOrEmpty(abbreviatedAssetName))
			{
				return abbreviatedAssetName;
			}

			if (AB.Contains(abbreviatedAssetName))
			{
				return abbreviatedAssetName;
			}

			string lowerName = abbreviatedAssetName.ToLowerInvariant();
			foreach (string assetName in AB.GetAllAssetNames())
			{
				string fileName = Path.GetFileNameWithoutExtension(assetName);
				if (string.Equals(assetName, lowerName, StringComparison.OrdinalIgnoreCase)
					|| string.Equals(fileName, abbreviatedAssetName, StringComparison.OrdinalIgnoreCase))
				{
					return assetName;
				}
			}

			return abbreviatedAssetName;
		}

		public T LoadAssetBundle<T>(string bundleName, string fileName) where T : UnityEngine.Object
		{
			LoadAssetBundle(bundleName);
			return LoadResource<T>(fileName);
		}

		public IEnumerator WaitForLoadedAssetBundle()
		{
			while (assetbundleLoadState == LoadState.Loading)
			{
				yield return null;
			}
		}

		public IEnumerator WaitForLoadedAssetBundle(float timeoutSec)
		{
			float timeout = Time.realtimeSinceStartup + timeoutSec;
			while (assetbundleLoadState == LoadState.Loading && Time.realtimeSinceStartup < timeout)
			{
				yield return null;
			}
		}

		public override T LoadResource<T>(string name)
		{
			mainLoadSnippet.LoadStatus = LoadState.Loading;
			resourceLoadState = LoadState.Loading;
			if (assetbundleLoadState != LoadState.Done || AB == null)
			{
				mainLoadSnippet.LoadStatus = LoadState.Error;
				resourceLoadState = LoadState.Error;
				return null;
			}

			UnityEngine.Object cached = GetResourceCache(name);
			if (cached != null)
			{
				mainLoadSnippet.LoadStatus = LoadState.Done;
				resourceLoadState = LoadState.Done;
				return cached as T;
			}

			string fullName = GetFullAssetName(name);
			T asset = AB.LoadAsset<T>(fullName);
			if (asset == null && !string.Equals(fullName, name, StringComparison.Ordinal))
			{
				asset = AB.LoadAsset<T>(name);
			}

			if (asset != null)
			{
				AddResourceCache(name, asset);
				mainLoadSnippet.LoadStatus = LoadState.Done;
				resourceLoadState = LoadState.Done;
			}
			else
			{
				mainLoadSnippet.LoadStatus = LoadState.Error;
				resourceLoadState = LoadState.Error;
			}

			return asset;
		}

		public T FindAssetByPattern<T>(AssetBundleLoader loader, Regex pattern) where T : UnityEngine.Object
		{
			if (loader?.AB == null || pattern == null)
			{
				return null;
			}

			foreach (string assetName in loader.AB.GetAllAssetNames())
			{
				if (pattern.IsMatch(assetName))
				{
					return loader.AB.LoadAsset<T>(assetName);
				}
			}

			return null;
		}

		public Sprite LoadResourceMultipleSprite(string name, string spriteName)
		{
			Sprite[] sprites = LoadResourceMultipleSprites(name);
			return GetSprite(ref sprites, spriteName);
		}

		public Sprite LoadResourceMultipleSprite(string name, int spriteIndex)
		{
			Sprite[] sprites = LoadResourceMultipleSprites(name);
			return GetSprite(ref sprites, spriteIndex);
		}

		private Sprite GetSprite(ref Sprite[] sprites, string spriteName)
		{
			if (sprites == null)
			{
				return null;
			}

			foreach (Sprite sprite in sprites)
			{
				if (sprite != null && sprite.name == spriteName)
				{
					return sprite;
				}
			}
			return null;
		}

		private Sprite GetSprite(ref Sprite[] sprites, int spriteIndex)
		{
			return sprites != null && spriteIndex >= 0 && spriteIndex < sprites.Length ? sprites[spriteIndex] : null;
		}

		public Sprite[] LoadResourceMultipleSprites(string name)
		{
			UnityEngine.Object[] cached = GetResourceArrayCache(name);
			if (cached != null)
			{
				return Array.ConvertAll(cached, x => x as Sprite);
			}

			if (AB == null)
			{
				return null;
			}

			string fullName = GetFullAssetName(name);
			Sprite[] sprites = AB.LoadAssetWithSubAssets<Sprite>(fullName);
			if (sprites != null)
			{
				AddResourceArrayCache(name, sprites);
			}
			return sprites;
		}

		public T[] LoadResources<T>() where T : UnityEngine.Object
		{
			if (AB == null)
			{
				return null;
			}

			string key = typeof(T).FullName;
			UnityEngine.Object[] cached = GetResourceArrayCache(key);
			if (cached != null)
			{
				return Array.ConvertAll(cached, x => x as T);
			}

			T[] assets = AB.LoadAllAssets<T>();
			if (assets != null)
			{
				AddResourceArrayCache(key, assets);
			}
			return assets;
		}

		private LoadSnippet GetLoadSnippet()
		{
			LoadSnippet snippet = new LoadSnippet
			{
				Id = ++nextLoadSnippetIndex,
				LoadStatus = LoadState.Ready
			};
			assetLoadSnippets.Add(snippet);
			return snippet;
		}

		public override int LoadResourceAsync<T>(string name, MonoBehaviour mb, Action<T> onFinish)
		{
			LoadSnippet snippet = GetLoadSnippet();
			snippet.LoadStatus = LoadState.Loading;
			T asset = LoadResource<T>(name);
			snippet.LoadStatus = asset != null ? LoadState.Done : LoadState.Error;
			onFinish?.Invoke(asset);
			return snippet.Id;
		}

		public int LoadResourceAsync<T>(string name, Action<T> onFinish) where T : UnityEngine.Object
		{
			return LoadResourceAsync(name, null, onFinish);
		}

		public UniTask<T> LoadResourceAsync<T>(string name, CancellationToken ct = default(CancellationToken)) where T : UnityEngine.Object
		{
			ct.ThrowIfCancellationRequested();
			return UniTask.FromResult(LoadResource<T>(name));
		}

		public IEnumerator LoadResourceAsyncCore<T>(LoadSnippet snippet, string name, Action<T> onFinish) where T : UnityEngine.Object
		{
			T asset = LoadResource<T>(name);
			if (snippet != null)
			{
				snippet.LoadStatus = asset != null ? LoadState.Done : LoadState.Error;
			}
			onFinish?.Invoke(asset);
			yield break;
		}

		public override T GetAsyncLoadedResource<T>()
		{
			foreach (UnityEngine.Object value in resourceCacheMap.Values)
			{
				if (value is T typed)
				{
					return typed;
				}
			}
			return null;
		}

		public T GetAsyncLoadedResource<T>(string fileName) where T : UnityEngine.Object
		{
			return GetResourceCache(fileName) as T;
		}

		public int LoadResourceMultipleSpriteAsync(string name, string spriteName, MonoBehaviour mb, Action<Sprite> onFinish)
		{
			Sprite sprite = LoadResourceMultipleSprite(name, spriteName);
			onFinish?.Invoke(sprite);
			return 0;
		}

		public IEnumerator LoadResourceMultipleSpriteAsyncCore(LoadSnippet snippet, string name, string spriteName, Action<Sprite> onFinish)
		{
			Sprite sprite = LoadResourceMultipleSprite(name, spriteName);
			if (snippet != null)
			{
				snippet.LoadStatus = sprite != null ? LoadState.Done : LoadState.Error;
			}
			onFinish?.Invoke(sprite);
			yield break;
		}

		public T[] GetAsyncLoadedaArrayResource<T>() where T : UnityEngine.Object
		{
			return GetAsyncLoadedArrayResource<T>(typeof(T).FullName);
		}

		public T[] GetAsyncLoadedArrayResource<T>(string fileName) where T : UnityEngine.Object
		{
			UnityEngine.Object[] cached = GetResourceArrayCache(fileName);
			return cached == null ? null : Array.ConvertAll(cached, x => x as T);
		}

		public Sprite GetAsyncLoadedMultipleSprite(string fileName, string spriteName)
		{
			Sprite[] sprites = GetAsyncLoadedArrayResource<Sprite>(fileName);
			return GetSprite(ref sprites, spriteName);
		}

		private UnityEngine.Object GetResourceCache(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}

			resourceCacheMap.TryGetValue(name, out UnityEngine.Object cache);
			return cache;
		}

		private void AddResourceCache(string name, UnityEngine.Object cache)
		{
			if (!string.IsNullOrEmpty(name) && cache != null)
			{
				resourceCacheMap[name] = cache;
			}
		}

		private UnityEngine.Object[] GetResourceArrayCache(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}

			resourceArrayCacheMap.TryGetValue(name, out UnityEngine.Object[] cache);
			return cache;
		}

		private void AddResourceArrayCache(string name, UnityEngine.Object[] caches)
		{
			if (!string.IsNullOrEmpty(name) && caches != null)
			{
				resourceArrayCacheMap[name] = caches;
			}
		}

		private void OnLoadAssetBundleFinished(AssetBundleResource ab)
		{
			assetBundle = ab;
			assetbundleLoadState = ab?.Bundle != null ? LoadState.Done : LoadState.Error;
		}

		public override void Unload(bool unloadLoadedAssets)
		{
			UnloadAssetBundleCore(unloadLoadedAssets);
		}

		public override void UnloadImmediate(bool unloadLoadedAssets)
		{
			UnloadAssetBundleCore(unloadLoadedAssets);
		}

		public override void Unload()
		{
			Unload(false);
		}

		public override void OnDestroyUnload()
		{
			Unload(false);
		}

		private IEnumerator UnloadAssetbundle(bool unloadLoadedAssets)
		{
			UnloadAssetBundleCore(unloadLoadedAssets);
			yield break;
		}

		private void UnloadAssetBundleCore(bool unloadLoadedAssets)
		{
			resourceCacheMap.Clear();
			resourceArrayCacheMap.Clear();
			assetBundle?.Unload(unloadLoadedAssets);
			assetBundle = null;
			assetbundleLoadState = LoadState.Ready;
			resourceLoadState = LoadState.Ready;
			mainLoadSnippet.LoadStatus = LoadState.Ready;
		}

		public bool ExistsAsset(string assetName)
		{
			return AB != null && AB.Contains(GetFullAssetName(assetName));
		}
	}
}
