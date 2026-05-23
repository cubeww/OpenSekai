using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Utilities;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SelectCategoryAndAddNoteEvent : MusicScoreMakerDispatcherEventBase
	{
		public int Lane
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

		public MusicScoreMakerUtility.ToolType NoteCategory
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SelectCategoryAndAddNoteEvent()
		{
		}
	}
}
