using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class ShowInvalidPlacementMessageEvent : MusicScoreMakerDispatcherEventBase
	{
		public InvalidPlacementInfo Info
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ShowInvalidPlacementMessageEvent()
		{
		}
	}
}
