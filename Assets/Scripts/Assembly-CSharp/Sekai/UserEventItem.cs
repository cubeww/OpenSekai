using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserEventItem
	{
		[Key("eventItemId")]
		public int eventItemId;

		[Key("quantity")]
		public int quantity;

		public UserEventItem()
		{
		}
	}
}
