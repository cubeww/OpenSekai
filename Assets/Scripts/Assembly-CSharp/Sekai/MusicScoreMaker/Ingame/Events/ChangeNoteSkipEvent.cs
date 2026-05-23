using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ChangeNoteSkipEvent : MusicScoreMakerDispatcherEventBase
	{
		public int NoteId
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool IsSkip
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ChangeNoteSkipEvent()
		{
		}
	}
}
