using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SetSelectedMusicScoreBarIndexEvent : MusicScoreMakerDispatcherEventBase
	{
		public float SelectedBarIndex
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SetSelectedMusicScoreBarIndexEvent()
		{
		}
	}
}
