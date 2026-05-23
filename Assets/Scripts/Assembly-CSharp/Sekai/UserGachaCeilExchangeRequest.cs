using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserGachaCeilExchangeRequest
	{
		[Key("gachaCeilExchangeIds")]
		public int[] gachaCeilExchangeIds;

		[Key("gachaCeilExchangeRequest")]
		public UserGachaCeilItemExchangeRequest gachaCeilExchangeRequest;

		public UserGachaCeilExchangeRequest()
		{
		}
	}
}
