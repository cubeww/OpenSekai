using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ChangeNoteTypeEvent : MusicScoreMakerDispatcherEventBase
	{
		public int NoteId
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

		public ChangeNoteTypeEvent()
		{
		}
	}
}
