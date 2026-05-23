using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterGachaBonusItemReceivableRewardGroup
	{
		[Key("id")]
		public int id;

		[Key("groupId")]
		public int groupId;

		[Key("gachaBonusBorderPoint")]
		public int gachaBonusBorderPoint;

		[Key("gachaBonusRewardType")]
		public string gachaBonusRewardType;

		[Key("resourceBoxId")]
		public int resourceBoxId;

		[Key("cardSupplyGroupId")]
		public int cardSupplyGroupId;

		[Key("gachaBonusRewardItemGroupId")]
		public int gachaBonusRewardItemGroupId;

		[Key("description")]
		public string description;

		[Key("assetbundleName")]
		public string assetbundleName;

		[IgnoreMember]
		public GachaBonusRewardType GachaBonusRewardType
		{
			get
			{
				throw null;
			}
		}

		public MasterGachaBonusItemReceivableRewardGroup()
		{
		}
	}
}
