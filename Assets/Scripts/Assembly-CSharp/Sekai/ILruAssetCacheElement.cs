using System;

namespace Sekai
{
	public interface ILruAssetCacheElement<out TAsset>
	{
		LruAssetCacheLoadingState LoadingState { get; }

		TAsset LoadedAsset { get; }

		event Action<LruAssetCacheLoadingState, TAsset> LoadingStateChanged;

		void DecrementReference();
	}
}
