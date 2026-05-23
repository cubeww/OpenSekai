using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ToggleEditRestrictedEvent : MusicScoreMakerDispatcherEventBase
	{
		public bool IsRestricted
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ToggleEditRestrictedEvent()
		{
		}
	}
}
