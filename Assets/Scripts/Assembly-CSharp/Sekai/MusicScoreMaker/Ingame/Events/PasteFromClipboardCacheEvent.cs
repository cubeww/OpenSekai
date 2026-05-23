using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class PasteFromClipboardCacheEvent : MusicScoreMakerDispatcherEventBase
	{
		public string CacheId
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool IsFlipHorizontal
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public PasteFromClipboardCacheEvent()
		{
		}
	}
}
