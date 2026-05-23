using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ChangeNoteDataEvent : MusicScoreMakerDispatcherEventBase
	{
		public NoteCategory NoteCategory
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteDirection NoteDirection
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteType NoteType
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

		public bool IsSkip
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ChangeNoteDataEvent()
		{
		}
	}
}
