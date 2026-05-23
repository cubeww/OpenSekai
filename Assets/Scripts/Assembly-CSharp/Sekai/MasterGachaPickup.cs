using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGachaPickup
	{
		[IgnoreMember]
		public const string GACHA_PICKUP_TYPE_NORMAL = "normal";

		[Key("id")]
		public int id;

		[Key("gachaId")]
		public int gachaId;

		[Key("cardId")]
		public int cardId;

		[Key("gachaPickupType")]
		public string gachaPickupType;

		public MasterGachaPickup()
		{
		}
	}
}
