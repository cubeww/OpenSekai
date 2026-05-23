using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Sekai.Live
{
	public class FeverBeginEvent : EventBase
	{
		[JsonProperty]
		public int FeverNoteCount
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public FeverBeginEvent([JsonProperty("MusicScoreInfo")] MusicScoreInfo info)
			: base(info)
		{
		}

		public void Setup(int noteCount)
		{
			FeverNoteCount = noteCount;
		}
	}
}
