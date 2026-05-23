using System.Collections.Generic;
using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class UserCustomMusicScorePublishedResponse
	{
		[IgnoreMember]
		public bool isOfficialScore;

		[Key("userCustomMusicScoreInfoJson")]
		public UserCustomMusicScoreInfo userCustomMusicScoreInfoJson;

		[Key("userCustomMusicScoreId")]
		public string userCustomMusicScoreId;

		[Key("userId")]
		public long userId;

		[Key("userName")]
		public string userName;

		[Key("musicId")]
		public int musicId;

		[Key("customMusicScoreTags")]
		public List<int> customMusicScoreTags;

		[Key("musicDifficultyType")]
		public string musicDifficultyType;

		[Key("playLevel")]
		public int playLevel;

		[Key("description")]
		public string description;

		[Key("isDerivativeAllowed")]
		public bool isDerivativeAllowed;

		[Key("previewStartTimeSec")]
		public float previewStartTimeSec;

		[Key("publishedAt")]
		public long publishedAt;

		[Key("reviewCount")]
		public int reviewCount;

		[Key("playCount")]
		public int playCount;

		[Key("fullComboRate")]
		public float fullComboRate;

		[Key("customMusicScoreSearchSortValue")]
		public float customMusicScoreSearchSortValue;

		[Key("playResult")]
		public string playResult;

		[Key("isReviewed")]
		public bool isReviewed;

		[Key("isReviewAllowed")]
		public bool isReviewAllowed;

		[IgnoreMember]
		public string ResolvedUserName
		{
			get
			{
				throw null;
			}
		}

		public MusicClearStatus GetClearStatus()
		{
			throw null;
		}

		public UserCustomMusicScorePublishedResponse()
		{
		}
	}
}
