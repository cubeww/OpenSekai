using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SetSelectedNoteDirectionEvent : MusicScoreMakerDispatcherEventBase
	{
		public NoteDirection NoteDirection
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SetSelectedNoteDirectionEvent()
		{
		}
	}
}
