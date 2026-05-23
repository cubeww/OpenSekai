using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SaveMusicScoreEvent : MusicScoreMakerDispatcherEventBase
	{
		public string FileName
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SaveMusicScoreEvent()
		{
		}
	}
}
