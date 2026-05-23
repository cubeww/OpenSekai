using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserGachaCeilExchange
	{
		[Key("userId")]
		public long userId;

		[Key("gachaCeilExchangeId")]
		public int gachaCeilExchangeId;

		[Key("exchangeStatus")]
		public string exchangeStatus;

		[Key("exchangeRemaining")]
		public int exchangeRemaining;

		[IgnoreMember]
		public ExchangeStatus ExchangeStatus
		{
			get
			{
				throw null;
			}
		}

		public UserGachaCeilExchange()
		{
		}
	}
}
