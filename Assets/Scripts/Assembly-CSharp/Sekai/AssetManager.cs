using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using CP;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sekai
{
	public class AssetManager : SingletonMonoBehaviour<AssetManager>
	{
		public enum BundleElementLoadStatus
		{
			Ready = 0,
			Downloading = 1,
			Downloded = 2,
			AssetLoading = 3,
			AssetLoaded = 4,
			ResourceLoading = 5,
			ResourceLoaded = 6,
			Abort = 7,
			Success = 8,
			Error = 9
		}

		public enum QueuePriority
		{
			Last = 0,
			First = 1
		}

		public enum BundleElementLoadMode
		{
			AllLoad = 0,
			OnlyDownload = 1
		}

		public class BundleElementLoadDevice
		{
			public BundleElement CurrentLoadElement { get; set; }
			public List<BundleElement> LoadQueue { get; set; }
			public IEnumerator LoadCoroutune { get; set; }

			public BundleElementLoadDevice()
			{
				LoadQueue = new List<BundleElement>();
			}
		}

		public class BundleElement
		{
			public string BundleName { get; set; }
			public string FileName { get; set; }
			public UnityEngine.Object LoadedResource { get; set; }
			public bool DoCache { get; set; }
			public bool ShowDLIndicator { get; set; }
			public BundleElementLoadStatus LoadStatus { get; set; }
			public BundleElementLoadMode Mode { get; set; }
			public Action<BundleElement> OnLoadFinished { get; set; }

			public BundleElement(BundleElementLoadMode mode, string bundleName, string fileName, bool doCache, bool showDLIndicator, Action<BundleElement> onLoadFinished)
			{
				Mode = mode;
				BundleName = bundleName;
				FileName = fileName;
				DoCache = doCache;
				ShowDLIndicator = showDLIndicator;
				OnLoadFinished = onLoadFinished;
				LoadStatus = BundleElementLoadStatus.Ready;
			}

			public T GetLoadedResource<T>() where T : UnityEngine.Object
			{
				return LoadedResource as T;
			}

			public void UnloadBundle(bool unloadLoadedAssetBundle)
			{
				if (!string.IsNullOrEmpty(BundleName) && AssetManager.ExistsInstance)
				{
					AssetManager.Instance.DisposeImmediate(BundleName, unloadLoadedAssetBundle);
				}
				LoadedResource = null;
				LoadStatus = BundleElementLoadStatus.Ready;
			}
		}

		private const int PARALLEL_LOAD_BUFFER_SIZE = 4;

		private static Dictionary<string, GameObject> cachedGameObjectMap;

		private Dictionary<string, AssetLoader> loadAssets;
		private List<BundleElement> downloadQueue;
		private BundleElementLoadDevice[] loadDecices;
		private int loadDeviceMax;
		private List<string> downloadBundleNames;
		private Dictionary<string, Dictionary<string, BundleElement>> cacheMap;
		private Dictionary<string, int> referenceCountTable;
		private Dictionary<string, List<bool>> callDisposseHistoryTable;
		private HashSet<string> _loadedAtlasAssetBundleNames;
		private HashSet<string> _protectedAssetBundleNames;

		public int BundleElementAllLoadingCount => BundleElementLoadingCount + BundleElementDownloadingCount;

		public int BundleElementLoadingCount => 0;

		public int BundleElementDownloadingCount => 0;

		public int BundleElementParallelLoadSize
		{
			get => loadDeviceMax;
			set => loadDeviceMax = Mathf.Max(1, value);
		}

		public bool IsDownloading => false;

		public bool IsResourceLoading => false;

		public bool IsActive => true;

		public AssetLoader this[string assetName] => LoadAssets(assetName);

		public AssetLoader LoadAssets(string assetName)
		{
			EnsureInitialized();
			loadAssets.TryGetValue(assetName, out AssetLoader loader);
			return loader;
		}

		protected override void OnInitialize()
		{
			EnsureInitialized();
		}

		public AssetBundleLoader LoadAssetBundleAsync(string bundleName, Action<AssetBundleLoader> onFinish, uint readBufferSize = 65536u)
		{
			AssetBundleLoader loader = LoadAssetBundle(bundleName, true);
			onFinish?.Invoke(loader);
			return loader;
		}

		public AssetBundleLoader LoadAssetBundle(string bundleName, bool loadDependencies = true)
		{
			EnsureInitialized();
			if (string.IsNullOrEmpty(bundleName))
			{
				return null;
			}

			SafeIncrementReferenceCountOf(bundleName);
			if (loadAssets.TryGetValue(bundleName, out AssetLoader cachedLoader))
			{
				return cachedLoader as AssetBundleLoader;
			}

			if (loadDependencies && AssetBundleManager.Instance.ExistsDependencies(bundleName))
			{
				CheckCircularReferenceAssetBundleDependencies(bundleName);
				string[] dependencies = AssetBundleManager.Instance.GetDependencies(bundleName);
				if (dependencies != null)
				{
					foreach (string dependency in dependencies)
					{
						LoadAssetBundle(dependency, true);
					}
				}
			}

			AssetBundleLoader loader = new AssetBundleLoader();
			loader.LoadAssetBundle(bundleName);
			if (loader.AssetBundleLoadState == AssetLoader.LoadState.Done)
			{
				loadAssets[bundleName] = loader;
				return loader;
			}

			SafeDecrementReferenceCountOf(bundleName);
			return loader;
		}

		public T LoadAssetBundle<T>(string bundleName, string fileName) where T : UnityEngine.Object
		{
			AssetBundleLoader loader = LoadAssetBundle(bundleName, true);
			return loader != null ? loader.LoadResource<T>(fileName) : null;
		}

		[Obsolete]
		public IEnumerator LoadAssetBundleAsync<T>(string bundleName, string fileName, Action<T> onFinish) where T : UnityEngine.Object
		{
			onFinish?.Invoke(LoadAssetBundle<T>(bundleName, fileName));
			yield break;
		}

		public T LoadResource<T>(string resourceName) where T : UnityEngine.Object
		{
			return Resources.Load<T>(resourceName);
		}

		public ResourceLoader LoadResourceAsync<T>(string resourceName, Action<T> onFinish) where T : UnityEngine.Object
		{
			ResourceLoader loader = new ResourceLoader();
			loader.LoadResource<T>(resourceName);
			onFinish?.Invoke(loader.GetAsyncLoadedResource<T>());
			return loader;
		}

		public bool Exists(string assetName)
		{
			EnsureInitialized();
			return loadAssets.ContainsKey(assetName);
		}

		public int GetAssetBundleReferenceCount(string assetBundle)
		{
			EnsureInitialized();
			return referenceCountTable.TryGetValue(assetBundle, out int count) ? count : 0;
		}

		public void DisposeWaitOfFrame(string assetName, bool unloadLoadedAssets = false)
		{
			Dispose(assetName, unloadLoadedAssets);
		}

		public void DisposeWaitOfFrame(AssetBundleLoader loader, bool unloadLoadedAssets = false)
		{
			Dispose(loader, unloadLoadedAssets);
		}

		public void Dispose(string assetName, bool unloadLoadedAssets = true)
		{
			DisposeCore(assetName, unloadLoadedAssets, false, false);
		}

		public void Dispose(AssetLoader al, bool unloadLoadedAssets = true)
		{
			if (al != null)
			{
				Dispose(al.AssetName, unloadLoadedAssets);
			}
		}

		public void AddProtectedAssetBundle(string assetName)
		{
			EnsureInitialized();
			if (!string.IsNullOrEmpty(assetName))
			{
				_protectedAssetBundleNames.Add(assetName);
			}
		}

		public void RemoveProtectedAssetBundle(string assetName)
		{
			EnsureInitialized();
			if (!string.IsNullOrEmpty(assetName))
			{
				_protectedAssetBundleNames.Remove(assetName);
			}
		}

		public bool IsProtectedAssetBundle(string assetName)
		{
			EnsureInitialized();
			return !string.IsNullOrEmpty(assetName) && _protectedAssetBundleNames.Contains(assetName);
		}

		public void DisposeImmediate(string assetName, bool unloadLoadedAssets = false)
		{
			DisposeCore(assetName, unloadLoadedAssets, true, false);
		}

		public void DisposeAll()
		{
			ForceDisposeAll();
		}

		public void ForceDispose(string assetName, bool unloadLoadedAssets = false)
		{
			DisposeCore(assetName, unloadLoadedAssets, true, true);
		}

		public void ForceDisposeImmediate(string assetName, bool unloadLoadedAssets = false)
		{
			ForceDispose(assetName, unloadLoadedAssets);
		}

		public void ForceDisposeMysekaiAll(string[] ignoreAsserBundleNames = null)
		{
			ForceDisposeAll(ignoreAsserBundleNames);
		}

		public void ForceDisposeAll(string[] ignoreAsserBundleNames = null)
		{
			EnsureInitialized();
			HashSet<string> ignore = ignoreAsserBundleNames != null ? new HashSet<string>(ignoreAsserBundleNames) : new HashSet<string>();
			List<string> keys = new List<string>(loadAssets.Keys);
			foreach (string key in keys)
			{
				if (!ignore.Contains(key))
				{
					DisposeCore(key, false, true, true);
				}
			}
		}

		public void OnDestroyUnload(string assetName)
		{
			ForceDispose(assetName, false);
		}

		public new void OnDestroy()
		{
			ForceDisposeAll();
		}

		public void OnApplicationQuit()
		{
			ForceDisposeAll();
		}

		public BundleElement LoadAssetBundleAndDownload(string bundleName, string fileName, bool doCache, bool showDLIndicator, Action<BundleElement> onFinished, QueuePriority queuePriority = QueuePriority.Last)
		{
			EnsureInitialized();
			BundleElement cached = GetBundleElementCache(bundleName, fileName);
			if (cached != null && cached.LoadedResource != null)
			{
				onFinished?.Invoke(cached);
				return cached;
			}

			BundleElement element = new BundleElement(BundleElementLoadMode.AllLoad, bundleName, fileName, doCache, showDLIndicator, onFinished);
			element.LoadStatus = BundleElementLoadStatus.AssetLoading;
			AssetBundleLoader loader = LoadAssetBundle(bundleName, true);
			if (loader != null && loader.AssetBundleLoadState == AssetLoader.LoadState.Done)
			{
				element.LoadedResource = loader.LoadResource<UnityEngine.Object>(fileName);
				element.LoadStatus = element.LoadedResource != null ? BundleElementLoadStatus.Success : BundleElementLoadStatus.Error;
				if (doCache && element.LoadedResource != null)
				{
					RegisterBundleElementCache(element);
				}
			}
			else
			{
				element.LoadStatus = BundleElementLoadStatus.Error;
			}
			onFinished?.Invoke(element);
			return element;
		}

		public UniTask<BundleElement> DownloadAssetBundleAsync(string bundleName, bool showDLIndicator, CancellationToken ct = default(CancellationToken))
		{
			ct.ThrowIfCancellationRequested();
			BundleElement element = new BundleElement(BundleElementLoadMode.OnlyDownload, bundleName, null, false, showDLIndicator, null);
			element.LoadStatus = AssetBundleManager.Instance.CheckNeedsDownload(bundleName)
				? BundleElementLoadStatus.Error
				: BundleElementLoadStatus.Success;
			return UniTask.FromResult(element);
		}

		public void DownloadAssetBundle(string bundleName, bool showDLIndicator, Action<BundleElement> onFinished)
		{
			BundleElement element = new BundleElement(BundleElementLoadMode.OnlyDownload, bundleName, null, false, showDLIndicator, onFinished);
			element.LoadStatus = AssetBundleManager.Instance.CheckNeedsDownload(bundleName)
				? BundleElementLoadStatus.Error
				: BundleElementLoadStatus.Success;
			onFinished?.Invoke(element);
		}

		public void AbortDownload(string bundleName)
		{
		}

		public void AbortDownloadAll()
		{
		}

		public void AbortLoadAll()
		{
		}

		public void AbortAll()
		{
		}

		public IEnumerator WaitForAllLoadBundleElementFinished()
		{
			yield break;
		}

		public void AddCacheGameObject(string key, GameObject modelObject, bool duplicateModel)
		{
			if (string.IsNullOrEmpty(key) || modelObject == null)
			{
				return;
			}

			EnsureStaticCache();
			cachedGameObjectMap[key] = duplicateModel ? Instantiate(modelObject) : modelObject;
		}

		public GameObject GetCacheGameObject(string key)
		{
			EnsureStaticCache();
			cachedGameObjectMap.TryGetValue(key, out GameObject value);
			return value;
		}

		public void DeleteCacheGameObject(string key)
		{
			EnsureStaticCache();
			cachedGameObjectMap.Remove(key);
		}

		public void DeleteCacheGameObjectAll()
		{
			EnsureStaticCache();
			cachedGameObjectMap.Clear();
		}

		private void SafeIncrementReferenceCountOf(string assetName)
		{
			EnsureInitialized();
			referenceCountTable.TryGetValue(assetName, out int count);
			referenceCountTable[assetName] = count + 1;
		}

		private bool SafeDecrementReferenceCountOf(string assetName)
		{
			EnsureInitialized();
			if (!referenceCountTable.TryGetValue(assetName, out int count))
			{
				return true;
			}

			count--;
			if (count <= 0)
			{
				referenceCountTable.Remove(assetName);
				return true;
			}

			referenceCountTable[assetName] = count;
			return false;
		}

		private void SafeClearReferenceCountOf(string assetName)
		{
			EnsureInitialized();
			referenceCountTable.Remove(assetName);
		}

		private void DisposeCore(string assetName, bool unloadLoadedAssets, bool immediate, bool force)
		{
			EnsureInitialized();
			if (string.IsNullOrEmpty(assetName))
			{
				return;
			}

			if (!force && IsProtectedAssetBundle(assetName))
			{
				return;
			}

			bool shouldUnload = force || SafeDecrementReferenceCountOf(assetName);
			if (!shouldUnload)
			{
				return;
			}

			if (loadAssets.TryGetValue(assetName, out AssetLoader loader))
			{
				if (immediate)
				{
					loader.UnloadImmediate(unloadLoadedAssets);
				}
				else
				{
					loader.Unload(unloadLoadedAssets);
				}
				loadAssets.Remove(assetName);
			}

			SafeClearReferenceCountOf(assetName);
			ClearBundleElementCache(assetName);
		}

		private void CheckCircularReferenceAssetBundleDependencies(string bundleName, string[] parents = null)
		{
			string[] dependencies = AssetBundleManager.Instance.GetDependencies(bundleName);
			if (dependencies == null || dependencies.Length == 0)
			{
				return;
			}

			List<string> parentList = parents != null ? new List<string>(parents) : new List<string>();
			if (parentList.Contains(bundleName))
			{
				throw new InvalidOperationException("Circular AssetBundle dependency: " + bundleName);
			}

			parentList.Add(bundleName);
			foreach (string dependency in dependencies)
			{
				CheckCircularReferenceAssetBundleDependencies(dependency, parentList.ToArray());
			}
		}

		private void RegisterBundleElementCache(BundleElement elem)
		{
			if (elem == null || string.IsNullOrEmpty(elem.BundleName) || string.IsNullOrEmpty(elem.FileName))
			{
				return;
			}

			EnsureInitialized();
			if (!cacheMap.TryGetValue(elem.BundleName, out Dictionary<string, BundleElement> fileMap))
			{
				fileMap = new Dictionary<string, BundleElement>();
				cacheMap[elem.BundleName] = fileMap;
			}
			fileMap[elem.FileName] = elem;
		}

		public void RegisterCacheLoadedResource(string bundleName, string fileName, UnityEngine.Object loadedResource)
		{
			BundleElement element = new BundleElement(BundleElementLoadMode.AllLoad, bundleName, fileName, true, false, null)
			{
				LoadedResource = loadedResource,
				LoadStatus = loadedResource != null ? BundleElementLoadStatus.Success : BundleElementLoadStatus.Error
			};
			RegisterBundleElementCache(element);
		}

		public BundleElement GetBundleElementCache(string bundleName, string fileName)
		{
			EnsureInitialized();
			if (cacheMap.TryGetValue(bundleName, out Dictionary<string, BundleElement> fileMap)
				&& fileMap.TryGetValue(fileName, out BundleElement element))
			{
				return element;
			}
			return null;
		}

		public void ClearBundleElementCache(string bundleName)
		{
			EnsureInitialized();
			cacheMap.Remove(bundleName);
		}

		public void ClearBundleElementCache(string bundleName, string fileName)
		{
			EnsureInitialized();
			if (cacheMap.TryGetValue(bundleName, out Dictionary<string, BundleElement> fileMap))
			{
				fileMap.Remove(fileName);
			}
		}

		public void ClearBundleElementCacheAll()
		{
			EnsureInitialized();
			cacheMap.Clear();
		}

		private void EnsureInitialized()
		{
			if (loadAssets != null)
			{
				return;
			}

			loadAssets = new Dictionary<string, AssetLoader>();
			downloadQueue = new List<BundleElement>();
			loadDeviceMax = PARALLEL_LOAD_BUFFER_SIZE;
			loadDecices = new BundleElementLoadDevice[loadDeviceMax];
			for (int i = 0; i < loadDecices.Length; i++)
			{
				loadDecices[i] = new BundleElementLoadDevice();
			}
			downloadBundleNames = new List<string>();
			cacheMap = new Dictionary<string, Dictionary<string, BundleElement>>();
			referenceCountTable = new Dictionary<string, int>();
			callDisposseHistoryTable = new Dictionary<string, List<bool>>();
			_loadedAtlasAssetBundleNames = new HashSet<string>();
			_protectedAssetBundleNames = new HashSet<string>();
			AssetBundleManager.Instance.Initialize();
		}

		private static void EnsureStaticCache()
		{
			if (cachedGameObjectMap == null)
			{
				cachedGameObjectMap = new Dictionary<string, GameObject>();
			}
		}

		public AssetManager()
		{
		}
	}
}
