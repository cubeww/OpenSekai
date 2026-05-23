using Newtonsoft.Json;

namespace Sekai.Live
{
	public class FeverEndEvent : EventBase
	{
		public FeverEndEvent([JsonProperty("MusicScoreInfo")] MusicScoreInfo info)
			: base(info)
		{
		}
	}
}
