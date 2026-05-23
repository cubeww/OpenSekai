using UnityEngine;

namespace Sekai
{
	public class AssetBundleResource
	{
		public AssetBundle Bundle;

		public AssetBundleElement Element;

		public string DataPath;

		public AssetBundleStream Stream;

		public void Unload(bool unloadLoadedAsset)
		{
			if (Bundle != null)
			{
				Bundle.Unload(unloadLoadedAsset);
				Bundle = null;
			}

			CloseStream();
		}

		public void CloseStream()
		{
			if (Stream != null)
			{
				Stream.Close();
				Stream = null;
			}
		}

		public AssetBundleResource()
		{
		}
	}
}
