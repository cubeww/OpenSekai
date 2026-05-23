using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class UpdateMusicTimeTextEvent : MusicScoreMakerDispatcherEventBase
	{
		public float CurrentTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public UpdateMusicTimeTextEvent(float time)
		{
			CurrentTime = time;
		}
	}
}
