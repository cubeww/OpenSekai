using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserGachaCeilItemExchangeRequest
	{
		[Key("gachaExchangeId")]
		public int gachaExchangeId;

		[Key("exchangeCount")]
		public int exchangeCount;

		[Key("gachaCeilExchangeSubstituteCostId")]
		public int gachaCeilExchangeSubstituteCostId;

		[Key("substituteCostCount")]
		public int substituteCostCount;

		public UserGachaCeilItemExchangeRequest()
		{
		}
	}
}
