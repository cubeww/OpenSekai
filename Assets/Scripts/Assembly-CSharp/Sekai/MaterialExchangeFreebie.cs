using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MaterialExchangeFreebie
	{
		[Key("id")]
		public int id;

		[Key("materialExchangeFreebieGroupId")]
		public int materialExchangeFreebieGroupId;

		[Key("materialExchangeFreebieTypeId")]
		public int materialExchangeFreebieTypeId;

		[Key("resourceBoxId")]
		public int resourceBoxId;

		public MaterialExchangeFreebie()
		{
		}
	}
}
