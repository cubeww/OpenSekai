using UnityEngine;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class MusicScoreListCellNeutralContents : MusicScoreListCellContentsBase
	{
		[SerializeField]
		private MusicScoreInfoCellCommonView _infoCellCommonView;

		[SerializeField]
		private Graphic[] _disableTargets;

		public void UpdateView(MusicScoreInfoCellCommonData cellCommonData)
		{
			throw null;
		}

		public void UpdateBookmarkState(bool isBookmarked)
		{
			throw null;
		}

		public MusicScoreListCellNeutralContents()
		{
			throw null;
		}
	}
}
