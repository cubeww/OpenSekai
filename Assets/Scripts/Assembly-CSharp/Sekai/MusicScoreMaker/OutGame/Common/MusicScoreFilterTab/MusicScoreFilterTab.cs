using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common.MusicScoreFilterTab
{
	public sealed class MusicScoreFilterTab : GenericTabItem<MusicScoreFilterTabData>
	{
		[SerializeField]
		private DefaultTabView _defaultTabView;

		[SerializeField]
		private MusicDifficultyTabView _musicDifficultyView;

		[SerializeField]
		private MultiButtonTapEffect _tapEffect;

		public override void ApplySelected()
		{
			throw null;
		}

		protected override void RefreshInternal()
		{
			throw null;
		}

		public MusicScoreFilterTab()
		{
			throw null;
		}
	}
}
