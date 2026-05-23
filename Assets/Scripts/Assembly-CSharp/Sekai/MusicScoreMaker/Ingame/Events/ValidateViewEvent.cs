using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ValidateViewEvent : MusicScoreMakerDispatcherEventBase
	{
		public int InvalidPlacementCount
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ValidateViewEvent()
		{
		}
	}
}
