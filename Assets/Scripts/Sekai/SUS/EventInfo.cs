namespace Sekai.SUS
{
	public class EventInfo
	{
		public int Bar { get; private set; }

		public float BarProgress { get; private set; }

		public EventType EventType { get; private set; }

		public EventInfo(int bar, float barProgress, EventType eventType)
		{
			Bar = bar;
			BarProgress = barProgress;
			EventType = eventType;
		}
	}
}
