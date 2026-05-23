using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class EventRankingReward : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("eventRankingRewardRangeId")]
		public int eventRankingRewardRangeId;

		[Key("resourceBoxId")]
		public int resourceBoxId;

		[Key("rewardConditionType")]
		public string rewardConditionType;

		[Key("conditionValue")]
		public int conditionValue;

		[IgnoreMember]
		public EventRankingRewardConditionType RewardConditionType;

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public EventRankingReward()
		{
		}
	}
}
