using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class OnExpandInputDragEvent : MusicScoreMakerDispatcherEventBase
	{
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

		public Vector2 PressPosition
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public OnExpandInputDragEvent()
		{
		}
	}
}
