using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterShopItemCost
	{
		[Key("shopItemId")]
		public int shopItemId;

		[Key("seq")]
		public int seq;

		[Key("cost")]
		public UserResource cost;

		public MasterShopItemCost()
		{
			throw null;
		}
	}
}
