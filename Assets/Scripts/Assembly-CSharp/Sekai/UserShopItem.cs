using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserShopItem
	{
		public const string STATUS_SALE = "sale";

		public const string STATUS_FORBIDDEN = "forbidden";

		public const string STATUS_SOLD_OUT = "sold_out";

		[Key("shopItemId")]
		public int shopItemId;

		[Key("level")]
		public int level;

		[Key("status")]
		public string status;

		[IgnoreMember]
		public bool IsSale
		{
			get
			{
				throw null;
			}
		}

		public UserShopItem()
		{
			throw null;
		}
	}
}
