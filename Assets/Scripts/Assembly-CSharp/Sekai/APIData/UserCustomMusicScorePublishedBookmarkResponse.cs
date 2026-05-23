using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class UserCustomMusicScorePublishedBookmarkResponse
	{
		[Key("userCustomMusicScorePublished")]
		public UserCustomMusicScorePublishedResponse userCustomMusicScorePublished;

		[Key("bookmarkedAt")]
		public long bookmarkedAt;

		public UserCustomMusicScorePublishedBookmarkResponse()
		{
			throw null;
		}
	}
}
