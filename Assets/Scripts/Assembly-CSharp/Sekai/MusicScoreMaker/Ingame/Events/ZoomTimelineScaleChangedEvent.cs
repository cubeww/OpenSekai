using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ZoomTimelineScaleChangedEvent : MusicScoreMakerDispatcherEventBase
	{
		public float Scale
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ZoomTimelineScaleChangedEvent()
		{
		}
	}
}
