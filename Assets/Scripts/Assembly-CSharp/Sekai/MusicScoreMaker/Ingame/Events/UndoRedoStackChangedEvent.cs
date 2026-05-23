using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class UndoRedoStackChangedEvent : MusicScoreMakerDispatcherEventBase
	{
		public bool CanUndo
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool CanRedo
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public UndoRedoStackChangedEvent()
		{
		}
	}
}
