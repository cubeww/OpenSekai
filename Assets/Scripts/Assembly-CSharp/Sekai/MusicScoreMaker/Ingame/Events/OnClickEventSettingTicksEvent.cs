using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class OnClickEventSettingTicksEvent : MusicScoreMakerDispatcherEventBase
	{
		public long Ticks
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public MusicScoreEventType EventType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public OnClickEventSettingTicksEvent()
		{
		}
	}
}
