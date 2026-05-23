using Sekai.MusicScoreMaker.OutGame.Common.Content;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public class MusicScoreCreatorTopBootData : IContentBootData
	{
		public readonly long CreatorId;

		public MusicScoreCreatorTopBootData(long creatorUserId)
		{
			CreatorId = creatorUserId;
		}
	}
}
