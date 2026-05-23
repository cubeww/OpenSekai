using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class UpdateMusicScoreBarMaxEvent : MusicScoreMakerDispatcherEventBase
	{
		public int BarMax
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public UpdateMusicScoreBarMaxEvent()
		{
		}
	}
}
