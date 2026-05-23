using MessagePack;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	[MessagePackObject(false)]
	public class MusicScoreEventData
	{
		[Key(0)]
		public int id;

		[Key(1)]
		public MusicScoreEventType eventType;

		[Key(2)]
		public long ticks;

		[Key(3)]
		public object changeValue;

		public void SetData(EventOperation eventData)
		{
			ticks = eventData.Ticks;
		}

		public EventOperation GetData()
		{
			return new EventOperation(id, ticks);
		}

		public MusicScoreEventData Clone()
		{
			return new MusicScoreEventData
			{
				eventType = eventType,
				ticks = ticks,
				changeValue = changeValue
			};
		}

		public MusicScoreEventData()
		{
		}
	}
}
