using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class CustomMusicScoreOfficialCreatorPublishedResponse
	{
		[Key("customMusicScoreId")]
		public string customMusicScoreId;

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

		public CustomMusicScoreOfficialCreatorPublishedResponse()
		{
			throw null;
		}
	}
}
