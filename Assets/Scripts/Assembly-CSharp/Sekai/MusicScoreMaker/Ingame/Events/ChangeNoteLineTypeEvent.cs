using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ChangeNoteLineTypeEvent : MusicScoreMakerDispatcherEventBase
	{
		public int NoteId
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteLineType noteLineType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ChangeNoteLineTypeEvent()
		{
		}
	}
}
