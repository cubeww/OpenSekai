using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterShopItem
	{
		public const string TYPE_NORMAL = "area_item";

		public const string TYPE_MUSIC = "music";

		public const string TYPE_MUSIC_VOCAL = "music_vocal";

		[Key("id")]
		public int id;

		[Key("shopId")]
		public int shopId;

		[Key("seq")]
		public int seq;

		[Key("releaseConditionId")]
		public int releaseConditionId;

		[Key("resourceBoxId")]
		public int resourceBoxId;

		[Key("startAt")]
		public long startAt;

		[Key("endAt")]
		public long? endAt;

		[Key("costs")]
		public MasterShopItemCost[] costs;

		public MasterShopItem()
		{
			throw null;
		}
	}
}
