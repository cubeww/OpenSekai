using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserGachaCeilItem
	{
		[Key("userId")]
		public long userId;

		[Key("gachaCeilItemId")]
		public int gachaCeilItemId;

		[Key("quantity")]
		public int quantity;

		public UserGachaCeilItem()
		{
		}
	}
}
