using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SetZoomTimelineScaleEvent : MusicScoreMakerDispatcherEventBase
	{
		public float Scale
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SetZoomTimelineScaleEvent()
		{
		}
	}
}
