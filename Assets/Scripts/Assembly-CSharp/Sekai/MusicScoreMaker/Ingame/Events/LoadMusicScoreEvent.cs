using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class LoadMusicScoreEvent : MusicScoreMakerDispatcherEventBase
	{
		public string FileName
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool ClearNotesAndSpeedEvents
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool DiscardSpeedChanges
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public LoadMusicScoreEvent()
		{
		}
	}
}
