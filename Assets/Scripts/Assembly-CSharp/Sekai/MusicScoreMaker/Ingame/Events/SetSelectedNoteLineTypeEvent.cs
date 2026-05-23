using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SetSelectedNoteLineTypeEvent : MusicScoreMakerDispatcherEventBase
	{
		public NoteLineType NoteLineType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SetSelectedNoteLineTypeEvent()
		{
		}
	}
}
