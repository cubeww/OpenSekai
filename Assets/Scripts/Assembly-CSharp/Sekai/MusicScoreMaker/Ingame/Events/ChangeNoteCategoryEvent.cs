using System.Runtime.CompilerServices;
using Sekai.Live;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ChangeNoteCategoryEvent : MusicScoreMakerDispatcherEventBase
	{
		public int NoteId
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

		public NoteDirection NoteDirection
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ChangeNoteCategoryEvent()
		{
		}
	}
}
