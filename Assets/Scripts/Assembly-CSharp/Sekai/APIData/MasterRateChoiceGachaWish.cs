using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterRateChoiceGachaWish
	{
		[Key("id")]
		public int id;

		[Key("groupId")]
		public int groupId;

		[Key("seq")]
		public int seq;

		[Key("lotteryType")]
		public string lotteryType;

		[Key("selectCount")]
		public int selectCount;

		public MasterRateChoiceGachaWish()
		{
		}
	}
}
