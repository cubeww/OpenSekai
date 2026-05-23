using System;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterEvent : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("eventType")]
		public string eventType;

		[Key("name")]
		public string name;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("bgmAssetbundleName")]
		public string bgmAssetbundleName;

		[Key("eventOnlyComponentDisplayStartAt")]
		public long eventOnlyComponentDisplayStartAt;

		[Key("eventOnlyComponentDisplayEndAt")]
		public long eventOnlyComponentDisplayEndAt;

		[Key("eventPointAssetbundleName")]
		public string eventPointAssetbundleName;

		[Key("startAt")]
		public long startAt;

		[Key("aggregateAt")]
		public long aggregateAt;

		[Key("rankingAnnounceAt")]
		public long rankingAnnounceAt;

		[Key("distributionStartAt")]
		public long distributionStartAt;

		[Key("closedAt")]
		public long closedAt;

		[Key("distributionEndAt")]
		public long distributionEndAt;

		[Key("standbyScreenDisplayStartAt")]
		public long standbyScreenDisplayStartAt;

		[Key("eventRankingRewardRanges")]
		public EventRankingRewardRange[] eventRankingRewardRanges;

		[Key("virtualLiveId")]
		public int? virtualLiveId;

		[Key("unit")]
		public string unit;

		[Key("eventBreakTimeId")]
		public int? eventBreakTimeId;

		[IgnoreMember]
		public GameEventType EventType;

		[IgnoreMember]
		public DateTime DisplayableStartTime;

		[IgnoreMember]
		public DateTime StartTime;

		[IgnoreMember]
		public DateTime AggregateTime;

		[IgnoreMember]
		public DateTime RankingAnnounceAt;

		[IgnoreMember]
		public DateTime EndTime;

		[IgnoreMember]
		public DateTime StandbyScreenDisplayStartAt;

		[IgnoreMember]
		public UnitType BonusUnitType
		{
			get
			{
				throw null;
			}
		}

		void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
		{
			throw null;
		}

		void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
		{
			throw null;
		}

		public bool IsPlayableEvent()
		{
			throw null;
		}

		public bool IsWorldBloomEvent()
		{
			throw null;
		}

		public bool HasVirtualLive()
		{
			throw null;
		}

		public MasterEvent()
		{
		}
	}
}
