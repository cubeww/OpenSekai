using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class UserCustomMusicScoreInfo
	{
		[Key("baseMusicScoreId")]
		public string baseMusicScoreId;

		[Key("musicId")]
		public int musicId;

		[Key("title")]
		public string title;

		[Key("userCustomMusicScorePath")]
		public string userCustomMusicScorePath;

		public UserCustomMusicScoreInfo()
		{
		}
	}
}
