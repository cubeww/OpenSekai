using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGachaCardRarityRate
	{
		[Key("id")]
		public int id;

		[Key("groupId")]
		public int groupId;

		[Key("cardRarityType")]
		public string cardRarityType;

		[Key("rate")]
		public float rate;

		[Key("lotteryType")]
		public string lotteryType;

		[IgnoreMember]
		public CardRarityType CardRarityType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public LotteryType LotteryType
		{
			get
			{
				throw null;
			}
		}

		public MasterGachaCardRarityRate()
		{
		}
	}
}
