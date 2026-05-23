using UnityEngine;

namespace Sekai
{
	public abstract class LruAssetCacheGeneral<TAsset> : LruAssetCache<(string assetBundleName, string fileName), TAsset> where TAsset : Object
	{
		public LruAssetCacheGeneral(int maxCachedAssetCount, string logName)
			: base(maxCachedAssetCount, logName)
		{
			throw null;
		}
	}
}
