using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SetFocusTicksEvent : MusicScoreMakerDispatcherEventBase
	{
		public long Ticks
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SetFocusTicksEvent()
		{
		}
	}
}
