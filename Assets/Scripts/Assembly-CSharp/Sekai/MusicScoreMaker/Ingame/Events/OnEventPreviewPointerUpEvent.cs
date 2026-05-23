using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class OnEventPreviewPointerUpEvent : MusicScoreMakerDispatcherEventBase
	{
		public int Id
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

		public OnEventPreviewPointerUpEvent()
		{
		}
	}
}
