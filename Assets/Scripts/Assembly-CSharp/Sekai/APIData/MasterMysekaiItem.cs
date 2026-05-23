using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterMysekaiItem : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("mysekaiItemType")]
		public string mysekaiItemType;

		[Key("name")]
		public string name;

		[Key("pronunciation")]
		public string pronunciation;

		[Key("description")]
		public string description;

		[Key("iconAssetbundleName")]
		public string iconAssetbundleName;

		[Key("modelAssetbundleName")]
		public string modelAssetbundleName;

		[IgnoreMember]
		public MysekaiItemType MysekaiItemType
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

		public MasterMysekaiItem()
		{
		}
	}
}
