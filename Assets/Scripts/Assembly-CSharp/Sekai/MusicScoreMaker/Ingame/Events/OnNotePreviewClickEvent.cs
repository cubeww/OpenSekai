using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class OnNotePreviewClickEvent : MusicScoreMakerDispatcherEventBase
	{
		public int NoteId
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public PointerEventData PointerEventData
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public OnNotePreviewClickEvent()
		{
		}
	}
}
