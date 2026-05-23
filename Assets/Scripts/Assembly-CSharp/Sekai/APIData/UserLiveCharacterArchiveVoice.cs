using System.Collections.Generic;
using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class UserLiveCharacterArchiveVoice
	{
		[Key("characterArchiveVoiceGroupIds")]
		public List<int> characterArchiveVoiceGroupIds;

		public UserLiveCharacterArchiveVoice()
		{
		}
	}
}
