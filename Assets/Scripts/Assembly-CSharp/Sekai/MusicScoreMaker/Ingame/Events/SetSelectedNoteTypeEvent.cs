using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SetSelectedNoteTypeEvent : MusicScoreMakerDispatcherEventBase
	{
		public NoteType NoteType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SetSelectedNoteTypeEvent()
		{
		}
	}
}
