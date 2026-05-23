using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class UpdateMusicPlayTimePreviewEvent : MusicScoreMakerDispatcherEventBase
	{
		public long Ticks
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public UpdateMusicPlayTimePreviewEvent()
		{
		}
	}
}
