using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterCostume3D : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("costume3dGroupId")]
		public int costume3dGroupId;

		[Key("costume3dType")]
		public string costume3dType;

		[Key("name")]
		public string name;

		[Key("partType")]
		public string partType;

		[Key("colorId")]
		public int colorId;

		[Key("colorName")]
		public string colorName;

		[Key("characterId")]
		public int characterId;

		[Key("costume3dRarity")]
		public string costume3dRarity;

		[Key("howToObtain")]
		public string howToObtain;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("designer")]
		public string designer;

		[Key("publishedAt")]
		public long publishedAt;

		[Key("archivePublishedAt")]
		public long archivePublishedAt;

		[Key("archiveDisplayType")]
		public string archiveDisplayType;

		[IgnoreMember]
		public CostumePartType CostumePartType
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
		{
			throw null;
		}

		void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
		{
			throw null;
		}

		public MasterCostume3D()
		{
		}
	}
}
