using MessagePack;
using Sekai.Constants;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MaterialExchangeDisplayResourceGroup
	{
		[Key("id")]
		public int id;

		[Key("groupId")]
		public int groupId;

		[Key("seq")]
		public int seq;

		[Key("resourceType")]
		public string resourceType;

		[Key("resourceId")]
		public int resourceId;

		[Key("assetbundleName")]
		public string assetbundleName;

		[IgnoreMember]
		public ResourceType ResourceType
		{
			get
			{
				throw null;
			}
		}

		public MaterialExchangeDisplayResourceGroup()
		{
		}
	}
}
