using System.Collections.Generic;
using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class CustomMusicScorePublishedSearchListResponse
	{
		[Key("userCustomMusicScorePublishedList")]
		public List<UserCustomMusicScorePublishedResponse> userCustomMusicScorePublishedList;

		[Key("customMusicScoreOfficialCreatorPublishedList")]
		public List<CustomMusicScoreOfficialCreatorPublishedResponse> customMusicScoreOfficialCreatorPublishedList;

		public CustomMusicScorePublishedSearchListResponse()
		{
			throw null;
		}
	}
}
