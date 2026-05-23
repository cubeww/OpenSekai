using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ShowNoteChangeButtonsEvent : MusicScoreMakerDispatcherEventBase
	{
		public NoteGroupType GroupType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public MusicScoreNoteBase[] SelectedNotes
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ShowNoteChangeButtonsEvent()
		{
		}
	}
}
