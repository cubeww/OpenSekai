using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Utilities;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class OnToolIconClickEvent : MusicScoreMakerDispatcherEventBase
	{
		public MusicScoreMakerUtility.ToolType ToolType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public OnToolIconClickEvent()
		{
		}
	}
}
