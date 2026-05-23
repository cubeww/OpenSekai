using Newtonsoft.Json;

namespace Sekai.Live
{
	public class FeverStartEvent : EventBase
	{
		public FeverStartEvent([JsonProperty("MusicScoreInfo")] MusicScoreInfo info)
			: base(info)
		{
		}
	}
}
