using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterCostume3DModel : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("costume3dId")]
		public int costume3dId;

		[Key("unit")]
		public string unit;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("headCostume3dAssetbundleType")]
		public string headCostume3dAssetbundleType;

		[Key("colorAssetbundleName")]
		public string colorAssetbundleName;

		[Key("thumbnailAssetbundleName")]
		public string thumbnailAssetbundleName;

		[Key("part")]
		public string part;

		[IgnoreMember]
		public UnitType Unit
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
		public HeadCostume3dAssetbundleType HeadCostume3dAssetbundleType
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

		public MasterCostume3DModel()
		{
		}
	}
}
