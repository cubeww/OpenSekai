using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SetQuantizeTypeEvent : MusicScoreMakerDispatcherEventBase
	{
		public QuantizeSettings.QuantizeType QuantizeType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public SetQuantizeTypeEvent()
		{
		}
	}
}
