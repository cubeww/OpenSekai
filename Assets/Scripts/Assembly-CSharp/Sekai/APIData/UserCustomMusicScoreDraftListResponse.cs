using System.Collections.Generic;
using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class UserCustomMusicScoreDraftListResponse
	{
		[Key("userCustomMusicScoreDrafts")]
		public List<UserCustomMusicScoreDraft> userCustomMusicScoreDrafts;

		public UserCustomMusicScoreDraftListResponse()
		{
		}
	}
}
