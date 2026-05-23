using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterCardExchangeResource
	{
		[Key("cardRarityType")]
		public string cardRarityType;

		[Key("seq")]
		public int seq;

		[Key("resourceBoxId")]
		public int resourceBoxId;

		[IgnoreMember]
		public CardRarityType CardRarityType
		{
			get
			{
				throw null;
			}
		}

		public MasterCardExchangeResource()
		{
		}
	}
}
