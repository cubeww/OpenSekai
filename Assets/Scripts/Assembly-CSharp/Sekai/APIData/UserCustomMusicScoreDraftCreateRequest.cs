using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class UserCustomMusicScoreDraftCreateRequest
	{
		[Key("baseMusicScoreId")]
		public string baseMusicScoreId;

		[Key("musicId")]
		public int musicId;

		[Key("title")]
		public string title;

		[Key("userCustomMusicScoreJsonGzipBase64")]
		public string userCustomMusicScoreJsonGzipBase64;

		[Key("memo")]
		public string memo;

		[Key("baseMusicDifficultyId")]
		public int baseMusicDifficultyId;

		public UserCustomMusicScoreDraftCreateRequest()
		{
			throw null;
		}
	}
}
