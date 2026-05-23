using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class OnNotePreviewDragEvent : MusicScoreMakerDispatcherEventBase
	{
		public int NoteId
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SelectedTargetOperation.NoteTapPosition NoteTapPosition
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

		public OnNotePreviewDragEvent()
		{
		}
	}
}
