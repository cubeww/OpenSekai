using System.Collections.Generic;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class AssetBundleInfo
	{
		[Key("version")]
		public string version;

		[Key("bundles")]
		public Dictionary<string, AssetBundleElement> bundles;

		public AssetBundleInfo()
		{
			bundles = new Dictionary<string, AssetBundleElement>();
		}
	}
}
