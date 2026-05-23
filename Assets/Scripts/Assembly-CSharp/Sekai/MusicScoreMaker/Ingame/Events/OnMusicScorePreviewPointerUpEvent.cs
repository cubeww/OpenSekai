using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class OnMusicScorePreviewPointerUpEvent : MusicScoreMakerDispatcherEventBase
	{
		public PointerEventData EventData
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

		public bool IsLongPress
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public OnMusicScorePreviewPointerUpEvent()
		{
		}
	}
}
