using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class OnNotePreviewPointerUpEvent : MusicScoreMakerDispatcherEventBase
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

		public bool IsLongPress
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool IsDragging
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public OnNotePreviewPointerUpEvent()
		{
		}
	}
}
