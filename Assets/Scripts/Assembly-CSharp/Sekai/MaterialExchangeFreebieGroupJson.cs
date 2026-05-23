using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MaterialExchangeFreebieGroupJson
	{
		[Key("id")]
		public int id;

		[Key("materialExchangeFreebieType")]
		public string materialExchangeFreebieType;

		[IgnoreMember]
		public ExchangeFreebieType MaterialExchangeFreebieType
		{
			get
			{
				throw null;
			}
		}

		public MaterialExchangeFreebieGroupJson()
		{
		}
	}
}
