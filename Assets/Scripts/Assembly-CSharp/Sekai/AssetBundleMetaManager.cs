using System.Collections.Generic;

namespace Sekai
{
	public class AssetBundleMetaManager
	{
		private static readonly AssetBundleMetaManager instance = new AssetBundleMetaManager();
		private readonly Dictionary<string, long> lastAccessMap = new Dictionary<string, long>();

		public static AssetBundleMetaManager Instance => instance;

		public void Initialize()
		{
		}

		public void SetAssetBundleMeta()
		{
		}

		public void UpdateClientMetaLastCheckDate(string assetBundleName)
		{
			if (!string.IsNullOrEmpty(assetBundleName))
			{
				lastAccessMap[assetBundleName] = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			}
		}

		public void RemoveClientMetaLastCheckDate(string assetBundleName)
		{
			if (!string.IsNullOrEmpty(assetBundleName))
			{
				lastAccessMap.Remove(assetBundleName);
			}
		}

		public void StartUpdateAllClientMeta(List<string> info, System.Action onFinish)
		{
			onFinish?.Invoke();
		}

		public void SaveClientAssetBundleMetaMapCore()
		{
		}

		public void ClearCache()
		{
			lastAccessMap.Clear();
		}
	}
}
