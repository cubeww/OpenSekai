using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class AddNoteListEvent : MusicScoreMakerDispatcherEventBase
	{
		public int NoteId
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public long Ticks
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public int Lane
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteLineType NoteLineType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteCategory NoteCategory
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

		public AddNoteListEvent()
		{
		}
	}
}
