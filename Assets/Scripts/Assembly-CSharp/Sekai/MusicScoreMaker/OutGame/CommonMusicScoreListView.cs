using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public class CommonMusicScoreListView : MusicScoreListView<CommonMusicScoreListCell, MusicScoreListCellData, MusicScoreListCellNeutralContents, MusicScoreListCellHighlightContents>
	{
		[SerializeField]
		private CustomTextMesh _musicScoreNotFoundText;

		public void Refresh(MusicScoreListCellData[] cellDataArray, int selectedIndex = 0)
		{
			throw null;
		}

		public CommonMusicScoreListView()
		{
			throw null;
		}
	}
}
