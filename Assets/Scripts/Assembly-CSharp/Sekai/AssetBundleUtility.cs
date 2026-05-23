using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Sekai
{
	public static class AssetBundleUtility
	{
		private static readonly HashSet<string> downloadingBundleNames = new HashSet<string>();

		public static T LoadAsset<T>(string bundleName, string resourceName, bool disposeAssetBundle = false) where T : UnityEngine.Object
		{
#if UNITY_EDITOR
			return TryLoadAssetFromEditorSource<T>(bundleName, resourceName);
#else
			AssetBundleLoader loader = LoadAssetBundle(bundleName);
			if (loader == null || loader.AssetBundleLoadState != AssetLoader.LoadState.Done)
			{
				return null;
			}

			T asset = loader.LoadResource<T>(resourceName);
			if (disposeAssetBundle)
			{
				UnloadAssetBundle(bundleName);
			}

			return asset;
#endif
		}

		public static UniTask<T> LoadOrDownloadAssetAsync<T>(string bundleName, string assetName, bool disposeAssetBundle = false, CancellationToken ct = default) where T : UnityEngine.Object
		{
			ct.ThrowIfCancellationRequested();
#if UNITY_EDITOR
			return UniTask.FromResult(TryLoadAssetFromEditorSource<T>(bundleName, assetName));
#else
			return LoadOrDownloadAssetAsyncCore<T>(bundleName, assetName, disposeAssetBundle, ct);
#endif
		}

#if !UNITY_EDITOR
		private static async UniTask<T> LoadOrDownloadAssetAsyncCore<T>(string bundleName, string assetName, bool disposeAssetBundle, CancellationToken ct) where T : UnityEngine.Object
		{
			AssetBundleLoader loader = await LoadOrDownloadAsync(bundleName, ct);
			ct.ThrowIfCancellationRequested();
			if (loader == null || loader.AssetBundleLoadState != AssetLoader.LoadState.Done)
			{
				return null;
			}

			T asset = loader.LoadResource<T>(assetName);
			if (disposeAssetBundle)
			{
				UnloadAssetBundle(bundleName);
			}

			return asset;
		}
#endif

		public static UniTask<AssetBundleLoader> LoadOrDownloadAsync(string bundleName, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
#if UNITY_EDITOR
			return UniTask.FromResult<AssetBundleLoader>(null);
#else
			return UniTask.FromResult(LoadAssetBundle(bundleName));
#endif
		}

		public static void UnloadAssetBundle(string bundleName)
		{
			if (!string.IsNullOrEmpty(bundleName) && AssetManager.ExistsInstance)
			{
				AssetManager.Instance.DisposeImmediate(bundleName, false);
			}
		}

		public static Sprite LoadMultipleSprite(string bundleName, string resourceName, string spriteName, bool disposeAssetBundle = false)
		{
#if UNITY_EDITOR
			return TryLoadMultipleSpriteFromEditorSource(bundleName, resourceName, spriteName);
#else
			AssetBundleLoader loader = LoadAssetBundle(bundleName);
			if (loader == null || loader.AssetBundleLoadState != AssetLoader.LoadState.Done)
			{
				return null;
			}

			Sprite sprite = loader.LoadResourceMultipleSprite(resourceName, spriteName);
			if (disposeAssetBundle)
			{
				UnloadAssetBundle(bundleName);
			}

			return sprite;
#endif
		}

		public static T[] LoadAssets<T>(string bundleName, bool disposeAssetBundle = false) where T : UnityEngine.Object
		{
#if UNITY_EDITOR
			return TryLoadAssetsFromEditorSource<T>(bundleName);
#else
			AssetBundleLoader loader = LoadAssetBundle(bundleName);
			if (loader == null || loader.AssetBundleLoadState != AssetLoader.LoadState.Done)
			{
				return null;
			}

			T[] assets = loader.LoadResources<T>();
			if (disposeAssetBundle)
			{
				UnloadAssetBundle(bundleName);
			}

			return assets;
#endif
		}

		public static T[] LoadAsset<T>(string bundleName, string[] resourceNames) where T : UnityEngine.Object
		{
			if (resourceNames == null)
			{
				return null;
			}

			T[] assets = new T[resourceNames.Length];
			for (int i = 0; i < resourceNames.Length; i++)
			{
				assets[i] = LoadAsset<T>(bundleName, resourceNames[i], false);
			}

			return assets;
		}

		public static void DownLoadAssetBundle(List<string> bundleNameList, Action callback)
		{
			callback?.Invoke();
		}

		private static AssetBundleLoader LoadAssetBundle(string bundleName)
		{
			if (string.IsNullOrEmpty(bundleName))
			{
				return null;
			}

			return AssetManager.Instance.LoadAssetBundle(bundleName, true);
		}

#if UNITY_EDITOR
		private static T TryLoadAssetFromEditorSource<T>(string bundleName, string resourceName) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(bundleName) || string.IsNullOrEmpty(resourceName))
			{
				return null;
			}

			foreach (string assetPath in GetEditorCandidateAssetPaths(bundleName, resourceName))
			{
				T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
				if (asset != null)
				{
					return asset;
				}

				UnityEngine.Object[] subAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
				for (int i = 0; i < subAssets.Length; i++)
				{
					if (subAssets[i] is T typedAsset)
					{
						return typedAsset;
					}
				}
			}

			return null;
		}

		private static T[] TryLoadAssetsFromEditorSource<T>(string bundleName) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(bundleName))
			{
				return null;
			}

			List<T> assets = new List<T>();
			foreach (string path in GetEditorAssetPathsForBundle(bundleName))
			{
				T mainAsset = AssetDatabase.LoadAssetAtPath<T>(path);
				AddUniqueEditorAsset(assets, mainAsset);

				UnityEngine.Object[] subAssets = AssetDatabase.LoadAllAssetsAtPath(path);
				for (int i = 0; i < subAssets.Length; i++)
				{
					AddUniqueEditorAsset(assets, subAssets[i] as T);
				}
			}

			return assets.Count > 0 ? assets.ToArray() : null;
		}

		private static Sprite TryLoadMultipleSpriteFromEditorSource(string bundleName, string resourceName, string spriteName)
		{
			if (string.IsNullOrEmpty(spriteName))
			{
				return null;
			}

			foreach (string assetPath in GetEditorCandidateAssetPaths(bundleName, resourceName))
			{
				UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
				for (int i = 0; i < assets.Length; i++)
				{
					if (assets[i] is Sprite sprite && string.Equals(sprite.name, spriteName, StringComparison.OrdinalIgnoreCase))
					{
						return sprite;
					}
				}
			}

			return TryLoadAssetFromEditorSource<Sprite>(bundleName, spriteName);
		}

		private static IEnumerable<string> GetEditorCandidateAssetPaths(string bundleName, string resourceName)
		{
			string normalizedResource = NormalizeEditorPathPart(resourceName);
			string[] candidateNames = GetEditorCandidateNames(normalizedResource);
			HashSet<string> yieldedPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (string assetPath in GetEditorAssetPathsForBundle(bundleName))
			{
				if (IsEditorCandidateAssetPath(assetPath, candidateNames) && yieldedPaths.Add(assetPath))
				{
					yield return assetPath;
				}
			}
		}

		private static IEnumerable<string> GetEditorAssetPathsForBundle(string bundleName)
		{
			AssetBundleElement element = AssetBundleManager.Instance.GetAssetBundleElement(bundleName);
			if (element?.paths == null)
			{
				yield break;
			}

			for (int i = 0; i < element.paths.Length; i++)
			{
				if (!string.IsNullOrEmpty(element.paths[i]))
				{
					yield return element.paths[i];
				}
			}
		}

		private static bool IsEditorCandidateAssetPath(string assetPath, string[] candidateNames)
		{
			string normalizedPath = NormalizeEditorPathPart(assetPath);
			string fileName = Path.GetFileName(normalizedPath);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(normalizedPath);

			for (int i = 0; i < candidateNames.Length; i++)
			{
				string candidateName = NormalizeEditorPathPart(candidateNames[i]);
				if (normalizedPath.EndsWith("/" + candidateName, StringComparison.OrdinalIgnoreCase)
					|| string.Equals(normalizedPath, candidateName, StringComparison.OrdinalIgnoreCase)
					|| string.Equals(fileName, Path.GetFileName(candidateName), StringComparison.OrdinalIgnoreCase)
					|| string.Equals(fileNameWithoutExtension, Path.GetFileNameWithoutExtension(candidateName), StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}

			return false;
		}

		private static string[] GetEditorCandidateNames(string resourceName)
		{
			if (resourceName.Contains("."))
			{
				return resourceName.EndsWith(".bytes", StringComparison.OrdinalIgnoreCase)
					? new[] { resourceName }
					: new[] { resourceName, resourceName + ".bytes" };
			}

			return new[]
			{
				resourceName,
				resourceName + ".prefab",
				resourceName + ".asset",
				resourceName + ".mat",
				resourceName + ".png",
				resourceName + ".jpg",
				resourceName + ".jpeg",
				resourceName + ".txt",
				resourceName + ".json",
				resourceName + ".acb",
				resourceName + ".awb",
				resourceName + ".bytes"
			};
		}

		private static string NormalizeEditorPathPart(string value)
		{
			return value.Replace('\\', '/').Trim('/');
		}

		private static void AddUniqueEditorAsset<T>(List<T> assets, T asset) where T : UnityEngine.Object
		{
			if (asset == null || assets.Contains(asset))
			{
				return;
			}

			assets.Add(asset);
		}
#endif

		private static void AddDownloadBundleName(string bundleName)
		{
			if (!string.IsNullOrEmpty(bundleName))
			{
				downloadingBundleNames.Add(bundleName);
			}
		}

		private static void RemoveDownloadBundleName(string bundleName)
		{
			if (!string.IsNullOrEmpty(bundleName))
			{
				downloadingBundleNames.Remove(bundleName);
			}
		}
	}
}
