using System.Runtime.CompilerServices;

namespace Sekai.SUS
{
	public class EventInfo
	{
		public int Bar
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public float BarProgress
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public EventType EventType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public EventInfo(int bar, float barProgress, EventType eventType)
		{
			Bar = bar;
			BarProgress = barProgress;
			EventType = eventType;
		}
	}
}
