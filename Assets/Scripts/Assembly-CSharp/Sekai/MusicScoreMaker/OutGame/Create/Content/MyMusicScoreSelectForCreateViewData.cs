using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Common.PublishedMusicScoreList;

namespace Sekai.MusicScoreMaker.OutGame.Create.Content
{
	public sealed class MyMusicScoreSelectForCreateViewData : ContentViewDataBase
	{
		public readonly PublishedMusicScoreListViewData PublishedListViewData;

		public MyMusicScoreSelectForCreateViewData()
		{
			PublishedListViewData = new PublishedMusicScoreListViewData();
		}
	}
}
