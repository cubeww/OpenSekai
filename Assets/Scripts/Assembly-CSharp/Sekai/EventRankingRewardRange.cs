using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class EventRankingRewardRange
	{
		[Key("id")]
		public int id;

		[Key("eventId")]
		public int eventId;

		[Key("fromRank")]
		public int fromRank;

		[Key("toRank")]
		public int toRank;

		[Key("eventRankingRewards")]
		public EventRankingReward[] eventRankingRewards;

		public EventRankingRewardRange()
		{
		}
	}
}
