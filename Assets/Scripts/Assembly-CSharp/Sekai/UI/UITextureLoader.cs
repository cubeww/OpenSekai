using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
	public class UITextureLoader : MonoBehaviour
	{
		[SerializeField]
		protected Graphic targetGraphic;

		[SerializeField]
		protected GameObject loadingObject;

		[SerializeField]
		protected GameObject notFoundObject;

		[SerializeField]
		protected bool doCache;

		[SerializeField]
		protected bool showIndicator;

		[SerializeField]
		protected bool onDisableUnload;

		private string bundleName;

		private string fileName;

		private AssetManager.BundleElementLoadStatus loadStatus;

		private bool reserved;

		private string reserveBundleName;

		private string reserveFileName;

		private LruAssetCacheGeneral<UnityEngine.Object> reserveLruCache;

		private LruAssetCacheHandle<UnityEngine.Object> lruCacheHandle;

		private Action<AssetManager.BundleElement> _onSuccess;

		private Action<AssetManager.BundleElement> _onError;

		private Func<AssetManager.BundleElement, bool> _onPreUpdate;

		public bool DoCache
		{
			get
			{
				return doCache;
			}
			set
			{
				doCache = value;
			}
		}

		public bool ShowIndicator
		{
			get
			{
				return showIndicator;
			}
			set
			{
				showIndicator = value;
			}
		}

		public string BundleName
		{
			get
			{
				return bundleName;
			}
		}

		public string FileName
		{
			get
			{
				return fileName;
			}
		}

		public Graphic TargetGraphic
		{
			get
			{
				return targetGraphic;
			}
		}

		public RawImage RawImage
		{
			get
			{
				return targetGraphic as RawImage;
			}
		}

		public AssetManager.BundleElementLoadStatus LoadStatus
		{
			get
			{
				return loadStatus;
			}
		}

		public Action<AssetManager.BundleElement> OnSuccess
		{
			get
			{
				return _onSuccess;
			}
			set
			{
				_onSuccess = value;
			}
		}

		public Action<AssetManager.BundleElement> OnError
		{
			get
			{
				return _onError;
			}
			set
			{
				_onError = value;
			}
		}

		public Func<AssetManager.BundleElement, bool> OnPreUpdate
		{
			get
			{
				return _onPreUpdate;
			}
			set
			{
				_onPreUpdate = value;
			}
		}

		public LruAssetCacheLoadingState LruCacheState
		{
			get
			{
				return loadStatus == AssetManager.BundleElementLoadStatus.Success ? LruAssetCacheLoadingState.Loaded : LruAssetCacheLoadingState.Unloaded;
			}
		}

		public void SetLoadOption(bool doCache, bool showIndicator)
		{
			this.doCache = doCache;
			this.showIndicator = showIndicator;
		}

		private void OnDisable()
		{
			if (onDisableUnload)
			{
				Unload();
			}
		}

		public void Load(string assetBundleName, string fileName, Action<AssetManager.BundleElement> onSuccess = null, Action<AssetManager.BundleElement> onError = null, LruAssetCacheGeneral<UnityEngine.Object> lruCache = null)
		{
			OnSuccess = onSuccess;
			OnError = onError;
			Load(assetBundleName, fileName, lruCache);
		}

		public UniTask<(bool, AssetManager.BundleElement)> LoadAsync(string assetBundleName, string targetFile, LruAssetCacheGeneral<UnityEngine.Object> lruCache = null, CancellationToken ctx = default)
		{
			Load(assetBundleName, targetFile, lruCache);
			return UniTask.FromResult((loadStatus == AssetManager.BundleElementLoadStatus.Success, (AssetManager.BundleElement)null));
		}

		private void Load(string assetBundleName, string fileName, LruAssetCacheGeneral<UnityEngine.Object> lruCache = null)
		{
			bundleName = assetBundleName;
			this.fileName = fileName;
			reserved = false;
			reserveBundleName = null;
			reserveFileName = null;
			reserveLruCache = null;
			SetActiveLoadingObject(showIndicator);
			SetActiveNotFoundObject(false);

			bool success = TryLoadFromResources(fileName);
			loadStatus = success ? AssetManager.BundleElementLoadStatus.Success : AssetManager.BundleElementLoadStatus.Error;
			SetActiveLoadingObject(false);
			SetActiveNotFoundObject(!success);

			if (success)
			{
				OnSuccess?.Invoke(null);
			}
			else
			{
				// TODO(original): restore AssetManager/LruAssetCache based async loading.
				OnError?.Invoke(null);
			}
		}

		private UniTaskVoid LoadWithLruCache(string assetBundleName, string fileName, LruAssetCacheGeneral<UnityEngine.Object> lruCache)
		{
			Load(assetBundleName, fileName, lruCache);
			return default;
		}

		public void LoadSync(string assetBundleName, string fileName, Action onSuccess = null, Action onError = null)
		{
			Load(assetBundleName, fileName, (LruAssetCacheGeneral<UnityEngine.Object>)null);
			if (loadStatus == AssetManager.BundleElementLoadStatus.Success)
			{
				onSuccess?.Invoke();
			}
			else
			{
				onError?.Invoke();
			}
		}

		public void Unload()
		{
			ClearTexture();
			bundleName = null;
			fileName = null;
			loadStatus = AssetManager.BundleElementLoadStatus.Ready;
			lruCacheHandle = default;
		}

		public void ClearTexture()
		{
			if (targetGraphic is RawImage rawImage)
			{
				rawImage.texture = null;
			}
			else if (targetGraphic is Image image)
			{
				image.sprite = null;
			}
		}

		private bool TryLoadFromResources(string targetFile)
		{
			if (targetGraphic == null || string.IsNullOrEmpty(targetFile))
			{
				return false;
			}

			string resourcePath = targetFile;
			int extensionIndex = resourcePath.LastIndexOf('.');
			if (extensionIndex > 0)
			{
				resourcePath = resourcePath.Substring(0, extensionIndex);
			}

			if (targetGraphic is RawImage rawImage)
			{
				Texture texture = Resources.Load<Texture>(resourcePath);
				rawImage.texture = texture;
				targetGraphic.enabled = texture != null;
				return texture != null;
			}

			if (targetGraphic is Image image)
			{
				Sprite sprite = Resources.Load<Sprite>(resourcePath);
				image.sprite = sprite;
				targetGraphic.enabled = sprite != null;
				return sprite != null;
			}

			return false;
		}

		private void OnLoadFinished(AssetManager.BundleElement element)
		{
			loadStatus = element != null ? AssetManager.BundleElementLoadStatus.Success : AssetManager.BundleElementLoadStatus.Error;
			if (loadStatus == AssetManager.BundleElementLoadStatus.Success)
			{
				OnSuccess?.Invoke(element);
			}
			else
			{
				OnError?.Invoke(element);
			}
		}

		public void ResetLoadingObject()
		{
			SetActiveLoadingObject(false);
			SetActiveNotFoundObject(loadStatus == AssetManager.BundleElementLoadStatus.Error);
		}

		public void SetActiveLoadingObject(bool isActive)
		{
			if (loadingObject != null)
			{
				loadingObject.SetActive(isActive);
			}
		}

		public void SetActiveNotFoundObject(bool isActive)
		{
			if (notFoundObject != null)
			{
				notFoundObject.SetActive(isActive);
			}
		}

		public void SetupSize(Vector2 size)
		{
			if (targetGraphic != null)
			{
				targetGraphic.rectTransform.sizeDelta = size;
			}
		}

		public void HideTargetGraphic()
		{
			if (targetGraphic != null)
			{
				targetGraphic.enabled = false;
			}
		}

		public UITextureLoader()
		{
		}
	}
}
