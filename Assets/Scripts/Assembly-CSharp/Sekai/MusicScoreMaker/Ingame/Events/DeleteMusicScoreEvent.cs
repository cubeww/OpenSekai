using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class DeleteMusicScoreEvent : MusicScoreMakerDispatcherEventBase
	{
		public string FileName
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public DeleteMusicScoreEvent()
		{
		}
	}
}
