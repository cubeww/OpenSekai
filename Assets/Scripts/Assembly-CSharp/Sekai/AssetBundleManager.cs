using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CP;
using UnityEngine;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Sekai
{
	public class AssetBundleManager
	{
		public enum DownloadResultCode
		{
			Success = 0,
			Error = 1
		}

		public enum DownloadErrorAction
		{
			None = 0
		}

		public enum CompareType
		{
			None = 0,
			Hash = 1
		}

		public enum ParallelDownloadControlStatus
		{
			None = 0
		}

		public class DownloadInfo
		{
			public float Progress;
			public DownloadResultCode ResultCode;
		}

		public class DeleteOldAssetBundleInfo
		{
			public string BundleName { get; }
			public string FilePath { get; }

			public DeleteOldAssetBundleInfo()
			{
			}

			public DeleteOldAssetBundleInfo(string bundleName, string filePath)
			{
				BundleName = bundleName;
				FilePath = filePath;
			}
		}

		public class ParallelDownloadContainer
		{
			public enum DownloadStatus
			{
				Ready = 0,
				Downloading = 1,
				Finished = 2
			}
		}

		private const string AssetBundleInfoFileName = "AssetBundleInfo.bytes";
		private const string CacheDirectoryName = "data";

		private static readonly AssetBundleManager instance = new AssetBundleManager();

		private AssetBundleInfo clientAssetBundleInfo;
		private AssetBundleInfo serverAssetBundleInfo;
		private Dictionary<string, List<string>> categoryResourceMap;
		private Dictionary<string, AssetBundleElement> serverAssetBundleCacheFileNameMap;
		private bool initialized;

		public static AssetBundleManager Instance => instance;

		public bool IsValid => clientAssetBundleInfo?.bundles != null && clientAssetBundleInfo.bundles.Count > 0;

		public bool IsDownloading => false;

		public long TotalDownloadFileSize { get; private set; }

		public bool IsCleanUpPause { get; set; }

		public bool Initialize()
		{
			InitLocalPaths();
			LoadClientAssetBundleInfo();
			serverAssetBundleInfo = clientAssetBundleInfo;
			ParseServerAssetBundleInfo();
			initialized = IsValid;
			return initialized;
		}

		public void InitializeForLocalCache()
		{
			Initialize();
		}

		public void ClearServerBundleInfo()
		{
			serverAssetBundleInfo = null;
			categoryResourceMap = null;
			serverAssetBundleCacheFileNameMap = null;
		}

		public void ClearCache()
		{
			ClearServerBundleInfo();
			clientAssetBundleInfo = null;
			initialized = false;
		}

		public bool HasBundle(string bundleName)
		{
			EnsureInitialized();
			return clientAssetBundleInfo?.bundles != null && clientAssetBundleInfo.bundles.ContainsKey(bundleName);
		}

		public AssetBundleElement GetAssetBundleElement(string bundleName)
		{
			EnsureInitialized();
			if (clientAssetBundleInfo?.bundles == null || string.IsNullOrEmpty(bundleName))
			{
				return null;
			}

			clientAssetBundleInfo.bundles.TryGetValue(bundleName, out AssetBundleElement element);
			return element;
		}

		public string GetStorageAssetBundleName(string bundleName)
		{
			return GetStreamingAssetsAssetBundleName(bundleName);
		}

		public string GetStreamingAssetsAssetBundleName(string bundleName)
		{
			AssetBundleElement element = GetAssetBundleElement(bundleName);
			if (element == null)
			{
				return null;
			}

			return CombineStreamingAssetsPath(CacheDirectoryName, element.cacheDirectoryName, element.cacheFileName);
		}

		public AssetBundleResource LoadAssetBundle(string bundleName, uint readBufferSize = 65536u)
		{
			EnsureInitialized();
			AssetBundleElement element = GetAssetBundleElement(bundleName);
			if (element == null)
			{
				LogUtility.LogError("AssetBundleInfo does not contain bundle: {0}", bundleName);
				return null;
			}

			string path = GetStreamingAssetsAssetBundleName(bundleName);
			if (!ExistsPackagedAssetBundle(element, path))
			{
				LogUtility.LogError("AssetBundle file not found. bundle:{0} streamingPath:{1}", bundleName, path);
				return null;
			}

			AssetBundle bundle = null;
			try
			{
				if (IsDirectFilePath(path))
				{
					bundle = AssetBundle.LoadFromFile(path);
				}
				else
				{
					bundle = LoadAssetBundleFromStreamingAssetsUri(path);
				}

				if (bundle == null)
				{
					LogUtility.LogError("Failed to load AssetBundle. bundle:{0} path:{1}", bundleName, path);
					return null;
				}

				AssetBundleMetaManager.Instance.UpdateClientMetaLastCheckDate(bundleName);
				return new AssetBundleResource
				{
					Bundle = bundle,
					Element = element,
					DataPath = path,
					Stream = null
				};
			}
			catch (Exception ex)
			{
				LogUtility.LogException(ex);
				return null;
			}
		}

		public bool ExistsBundleFile(string bundleName)
		{
			AssetBundleElement element = GetAssetBundleElement(bundleName);
			return element != null && ExistsPackagedAssetBundle(element, GetStreamingAssetsAssetBundleName(bundleName));
		}

		public bool ExistsClientBundleFile(string bundleName)
		{
			return ExistsBundleFile(bundleName);
		}

		public long GetClientBundleFileSize(string bundleName)
		{
			AssetBundleElement element = GetAssetBundleElement(bundleName);
			if (element == null)
			{
				return 0L;
			}

			string path = GetStreamingAssetsAssetBundleName(bundleName);
			if (IsDirectFilePath(path) && File.Exists(path))
			{
				return new FileInfo(path).Length;
			}

			return element.fileSize;
		}

		public long GetClientBundleFileTotalSize()
		{
			EnsureInitialized();
			long total = 0L;
			if (clientAssetBundleInfo?.bundles == null)
			{
				return total;
			}

			foreach (string bundleName in clientAssetBundleInfo.bundles.Keys)
			{
				total += GetClientBundleFileSize(bundleName);
			}

			return total;
		}

		public List<string> GetClientBundleNameList()
		{
			EnsureInitialized();
			return clientAssetBundleInfo?.bundles == null
				? new List<string>()
				: new List<string>(clientAssetBundleInfo.bundles.Keys);
		}

		public bool CheckNeedsDownload(string bundleName)
		{
			AssetBundleElement element = GetAssetBundleElement(bundleName);
			if (element == null)
			{
				return true;
			}

			string path = GetStreamingAssetsAssetBundleName(bundleName);
			if (!ExistsPackagedAssetBundle(element, path))
			{
				return true;
			}

			if (IsDirectFilePath(path) && element.fileSize > 0L && new FileInfo(path).Length != element.fileSize)
			{
				return true;
			}

			return false;
		}

		public bool ExistsDependencies(string bundleName)
		{
			AssetBundleElement element = GetAssetBundleElement(bundleName);
			return element?.dependencies != null && element.dependencies.Length > 0;
		}

		public string[] GetDependencies(string bundleName)
		{
			return GetAssetBundleElement(bundleName)?.dependencies;
		}

		public void OverwriteDataVersion(string dataVersion)
		{
			if (clientAssetBundleInfo != null)
			{
				clientAssetBundleInfo.version = dataVersion;
			}
		}

		public List<string> GetAssetBundleContainsNameFilesInCategory(string categoryName, string[] containNames, string[] excludeNames)
		{
			EnsureInitialized();
			List<string> result = new List<string>();
			if (serverAssetBundleInfo?.bundles == null)
			{
				return result;
			}

			foreach (AssetBundleElement element in serverAssetBundleInfo.bundles.Values)
			{
				if (!string.Equals(element.category, categoryName, StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}

				string bundleName = element.bundleName;
				if (!ContainsAll(bundleName, containNames) || ContainsAny(bundleName, excludeNames))
				{
					continue;
				}

				result.Add(bundleName);
			}

			return result;
		}

		public List<string> GetAssetBundleContainsNameFilesInCategory(string categoryName, string[] containNames)
		{
			return GetAssetBundleContainsNameFilesInCategory(categoryName, containNames, null);
		}

		public List<string> GetAssetBundleContainsNameFiles(string[] containNames)
		{
			EnsureInitialized();
			List<string> result = new List<string>();
			if (serverAssetBundleInfo?.bundles == null)
			{
				return result;
			}

			foreach (string bundleName in serverAssetBundleInfo.bundles.Keys)
			{
				if (ContainsAll(bundleName, containNames))
				{
					result.Add(bundleName);
				}
			}

			return result;
		}

		public bool MatchWithServerInfo(AssetBundleElement elem)
		{
			if (elem == null)
			{
				return false;
			}

			AssetBundleElement server = GetAssetBundleElement(elem.bundleName);
			return server != null && string.Equals(server.hash, elem.hash, StringComparison.Ordinal);
		}

		public bool MatchWithServerInfo(string bundleName)
		{
			return GetAssetBundleElement(bundleName) != null;
		}

		public List<string> GetServerAssetBundleMatchList(string regularExpressionName)
		{
			EnsureInitialized();
			List<string> result = new List<string>();
			if (serverAssetBundleInfo?.bundles == null || string.IsNullOrEmpty(regularExpressionName))
			{
				return result;
			}

			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regularExpressionName);
			foreach (string bundleName in serverAssetBundleInfo.bundles.Keys)
			{
				if (regex.IsMatch(bundleName))
				{
					result.Add(bundleName);
				}
			}

			return result;
		}

		public int InitCategoryDownloadInfo(List<string> categoryNames, List<string> bundleNames = null)
		{
			return 0;
		}

		public int InitBundleListDownloadInfo(List<string> bundleNames, Action onAssetBundleError = null)
		{
			return 0;
		}

		public int StartCategoryDownload(List<string> categoryNames, List<string> bundleNames, DownloadEventHandler onDownloadProgress, DownloadEventHandler onFinished, DownloadEventHandler onError, int parallelDLSize = 1)
		{
			DownloadInfo info = new DownloadInfo { Progress = 1f, ResultCode = DownloadResultCode.Success };
			onDownloadProgress?.Invoke(info);
			onFinished?.Invoke(info);
			return 0;
		}

		public void DownloadResourceList(DownloadResourceResultHandler onFinished)
		{
			onFinished?.Invoke(null);
		}

		public delegate void DownloadEventHandler(DownloadInfo info);

		public delegate void DownloadResourceResultHandler(object result);

		private void EnsureInitialized()
		{
			if (!initialized)
			{
				Initialize();
			}
		}

		private void InitLocalPaths()
		{
		}

		private static bool ExistsPackagedAssetBundle(AssetBundleElement element, string streamingPath)
		{
			if (element == null || string.IsNullOrEmpty(streamingPath))
			{
				return false;
			}

			return !IsDirectFilePath(streamingPath) || File.Exists(streamingPath);
		}

		private static AssetBundle LoadAssetBundleFromStreamingAssetsUri(string uri)
		{
			using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri))
			{
				UnityWebRequestAsyncOperation operation = request.SendWebRequest();
				while (!operation.isDone)
				{
					Thread.Sleep(1);
				}

				if (request.result != UnityWebRequest.Result.Success)
				{
					LogUtility.LogError("Failed to load StreamingAssets AssetBundle. uri:{0} error:{1}", uri, request.error);
					return null;
				}

				return DownloadHandlerAssetBundle.GetContent(request);
			}
		}

		private static byte[] ReadStreamingAssetBytes(string uri)
		{
			using (UnityWebRequest request = UnityWebRequest.Get(uri))
			{
				UnityWebRequestAsyncOperation operation = request.SendWebRequest();
				while (!operation.isDone)
				{
					Thread.Sleep(1);
				}

				if (request.result != UnityWebRequest.Result.Success)
				{
					LogUtility.LogError("Failed to read StreamingAssets file. uri:{0} error:{1}", uri, request.error);
					return null;
				}

				return request.downloadHandler?.data;
			}
		}

		private static string CombineStreamingAssetsPath(params string[] parts)
		{
			string path = Application.streamingAssetsPath.TrimEnd('/', '\\');
			foreach (string part in parts)
			{
				if (!string.IsNullOrEmpty(part))
				{
					path += "/" + part.Trim('/', '\\');
				}
			}

			return path;
		}

		private static bool IsDirectFilePath(string path)
		{
			return !string.IsNullOrEmpty(path)
				&& path.IndexOf("://", StringComparison.Ordinal) < 0
				&& !path.StartsWith("jar:", StringComparison.OrdinalIgnoreCase);
		}

		private void LoadClientAssetBundleInfo()
		{
#if UNITY_EDITOR
			clientAssetBundleInfo = CreateEditorAssetBundleInfo();
			if (clientAssetBundleInfo?.bundles == null || clientAssetBundleInfo.bundles.Count == 0)
			{
				Debug.LogWarning("No AssetBundle names are configured in the Editor AssetDatabase.");
				clientAssetBundleInfo = new AssetBundleInfo();
			}
			return;
#else
			byte[] assetBundleInfoBytes = LoadClientAssetBundleInfoBytes();
			if (assetBundleInfoBytes == null || assetBundleInfoBytes.Length == 0)
			{
				LogUtility.LogError("AssetBundleInfo.bytes was not found.");
				clientAssetBundleInfo = new AssetBundleInfo();
				return;
			}

			try
			{
				AssetBundleInfoMessagePackResolver.EnsureRegistered();
				clientAssetBundleInfo = BinarySerializer.Deserialize<AssetBundleInfo>(assetBundleInfoBytes);
				if (clientAssetBundleInfo == null)
				{
					clientAssetBundleInfo = new AssetBundleInfo();
				}
			}
			catch (Exception ex)
			{
				clientAssetBundleInfo = new AssetBundleInfo();
				LogUtility.LogException(ex);
			}
#endif
		}

		private byte[] LoadClientAssetBundleInfoBytes()
		{
			string streamingPath = CombineStreamingAssetsPath(CacheDirectoryName, AssetBundleInfoFileName);
			if (IsDirectFilePath(streamingPath))
			{
				if (File.Exists(streamingPath))
				{
					return File.ReadAllBytes(streamingPath);
				}
			}
			else
			{
				byte[] streamingBytes = ReadStreamingAssetBytes(streamingPath);
				if (streamingBytes != null && streamingBytes.Length > 0)
				{
					return streamingBytes;
				}
			}

			return null;
		}

#if UNITY_EDITOR
		private static AssetBundleInfo CreateEditorAssetBundleInfo()
		{
			AssetBundleInfo info = new AssetBundleInfo
			{
				version = Application.version,
				bundles = new Dictionary<string, AssetBundleElement>()
			};

			string[] bundleNames = AssetDatabase.GetAllAssetBundleNames();
			if (bundleNames == null)
			{
				return info;
			}

			Array.Sort(bundleNames, StringComparer.Ordinal);
			for (int i = 0; i < bundleNames.Length; i++)
			{
				string bundleName = NormalizeBundleName(bundleNames[i]);
				if (string.IsNullOrEmpty(bundleName))
				{
					continue;
				}

				info.bundles[bundleName] = new AssetBundleElement
				{
					bundleName = bundleName,
					cacheFileName = bundleName,
					cacheDirectoryName = string.Empty,
					hash = string.Empty,
					category = InferEditorCategory(bundleName),
					crc = 0u,
					fileSize = 0L,
					dependencies = GetEditorAssetBundleDependencies(bundleName),
					paths = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName),
					isBuiltin = false,
					isRelocate = true
				};
			}

			return info;
		}

		private static string[] GetEditorAssetBundleDependencies(string bundleName)
		{
			string[] dependencies = AssetDatabase.GetAssetBundleDependencies(bundleName, true);
			if (dependencies == null || dependencies.Length == 0)
			{
				return Array.Empty<string>();
			}

			for (int i = 0; i < dependencies.Length; i++)
			{
				dependencies[i] = NormalizeBundleName(dependencies[i]);
			}

			Array.Sort(dependencies, StringComparer.Ordinal);
			return dependencies;
		}

		private static string InferEditorCategory(string bundleName)
		{
			return bundleName.IndexOf("tutorial", StringComparison.OrdinalIgnoreCase) >= 0
				? "Tutorial"
				: "StartApp";
		}

		private static string NormalizeBundleName(string bundleName)
		{
			return (bundleName ?? string.Empty).Replace('\\', '/').Trim('/');
		}
#endif

		private void ParseServerAssetBundleInfo()
		{
			categoryResourceMap = new Dictionary<string, List<string>>();
			serverAssetBundleCacheFileNameMap = new Dictionary<string, AssetBundleElement>();
			if (serverAssetBundleInfo?.bundles == null)
			{
				return;
			}

			foreach (AssetBundleElement element in serverAssetBundleInfo.bundles.Values)
			{
				if (element == null)
				{
					continue;
				}

				if (!string.IsNullOrEmpty(element.cacheFileName) && !serverAssetBundleCacheFileNameMap.ContainsKey(element.cacheFileName))
				{
					serverAssetBundleCacheFileNameMap.Add(element.cacheFileName, element);
				}

				if (string.IsNullOrEmpty(element.category))
				{
					continue;
				}

				if (!categoryResourceMap.TryGetValue(element.category, out List<string> list))
				{
					list = new List<string>();
					categoryResourceMap.Add(element.category, list);
				}
				list.Add(element.bundleName);
			}
		}

		private static bool ContainsAll(string value, string[] contains)
		{
			if (contains == null || contains.Length == 0)
			{
				return true;
			}

			foreach (string contain in contains)
			{
				if (!string.IsNullOrEmpty(contain) && (value == null || value.IndexOf(contain, StringComparison.OrdinalIgnoreCase) < 0))
				{
					return false;
				}
			}

			return true;
		}

		private static bool ContainsAny(string value, string[] contains)
		{
			if (contains == null || contains.Length == 0)
			{
				return false;
			}

			foreach (string contain in contains)
			{
				if (!string.IsNullOrEmpty(contain) && value != null && value.IndexOf(contain, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return true;
				}
			}

			return false;
		}
	}
}
