using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public class RankingMusicScoreListView : MusicScoreListView<RankingMusicScoreListCell, RankingMusicScoreListCellData, RankingMusicScoreListCellNeutralContents, RankingMusicScoreListCellHighlightContents>
	{
		[SerializeField]
		private CustomTextMesh _musicScoreNotFoundText;

		public void Refresh(RankingMusicScoreListCellData[] cellDataArray, int selectedIndex = 0)
		{
			throw null;
		}

		public RankingMusicScoreListView()
		{
			throw null;
		}
	}
}
