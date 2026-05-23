using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ZoomTimelineEvent : MusicScoreMakerDispatcherEventBase
	{
		public float Scale
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ZoomTimelineEvent()
		{
		}
	}
}
