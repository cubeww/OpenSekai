using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class AssetBundleElement : IMessagePackSerializationCallbackReceiver
	{
		[Key("bundleName")]
		public string bundleName;

		[Key("cacheFileName")]
		public string cacheFileName;

		[Key("cacheDirectoryName")]
		public string cacheDirectoryName;

		[Key("hash")]
		public string hash;

		[Key("category")]
		public string category;

		[Key("crc")]
		public uint crc;

		[Key("fileSize")]
		public long fileSize;

		[Key("dependencies")]
		public string[] dependencies;

		[Key("paths")]
		public string[] paths;

		[Key("isBuiltin")]
		public bool isBuiltin;

		[Key("isRelocate")]
		public bool isRelocate;

		public AssetBundleElement()
		{
		}

		public AssetBundleElement(AssetBundleElement element)
		{
			Copy(element);
		}

		public void Copy(AssetBundleElement element)
		{
			if (element == null)
			{
				return;
			}

			bundleName = element.bundleName;
			cacheFileName = element.cacheFileName;
			cacheDirectoryName = element.cacheDirectoryName;
			hash = element.hash;
			category = element.category;
			crc = element.crc;
			fileSize = element.fileSize;
			dependencies = element.dependencies;
			paths = element.paths;
			isBuiltin = element.isBuiltin;
			isRelocate = element.isRelocate;
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
		}
	}
}
