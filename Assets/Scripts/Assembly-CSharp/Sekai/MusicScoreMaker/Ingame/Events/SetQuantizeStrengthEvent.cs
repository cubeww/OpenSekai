using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SetQuantizeStrengthEvent : MusicScoreMakerDispatcherEventBase
	{
		public float Strength
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SetQuantizeStrengthEvent()
		{
		}
	}
}
