using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SetQuantizeDivisionEvent : MusicScoreMakerDispatcherEventBase
	{
		public int Division
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SetQuantizeDivisionEvent()
		{
		}
	}
}
