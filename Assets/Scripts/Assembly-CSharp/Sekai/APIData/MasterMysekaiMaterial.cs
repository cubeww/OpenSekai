using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterMysekaiMaterial : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("mysekaiMaterialType")]
		public string mysekaiMaterialType;

		[Key("name")]
		public string name;

		[Key("pronunciation")]
		public string pronunciation;

		[Key("description")]
		public string description;

		[Key("mysekaiMaterialRarityType")]
		public string mysekaiMaterialRarityType;

		[Key("iconAssetbundleName")]
		public string iconAssetbundleName;

		[Key("modelAssetbundleName")]
		public string modelAssetbundleName;

		[Key("mysekaiPhenomenaGroupId")]
		public int mysekaiPhenomenaGroupId;

		[Key("mysekaiSiteIds")]
		public List<int> mysekaiSiteIds;

		[IgnoreMember]
		public MysekaiMaterialType MysekaiMaterialType
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

		[IgnoreMember]
		public MysekaiMaterialRarityType MysekaiMaterialRarityType
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

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public MasterMysekaiMaterial()
		{
		}
	}
}
