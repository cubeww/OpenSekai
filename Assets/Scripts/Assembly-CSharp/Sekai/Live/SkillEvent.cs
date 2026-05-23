using Newtonsoft.Json;

namespace Sekai.Live
{
	public class SkillEvent : EventBase
	{
		public SkillEvent([JsonProperty("MusicScoreInfo")] MusicScoreInfo info)
			: base(info)
		{
		}
	}
}
