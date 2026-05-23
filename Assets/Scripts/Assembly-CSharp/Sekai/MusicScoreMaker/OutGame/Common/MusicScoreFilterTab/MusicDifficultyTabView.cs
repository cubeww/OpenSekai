using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.OutGame.Common.MusicScoreFilterTab
{
	public sealed class MusicDifficultyTabView : TabViewBase
	{
		[SerializeField]
		private Graphic[] _offPartses;

		[SerializeField]
		private UIPartsMusicDifficulty _musicDifficulty;

		[SerializeField]
		private GraphicButtonTapEffect _tapEffect;

		public void Refresh(MusicDifficulty difficulty, bool isSelected)
		{
			throw null;
		}

		public MusicDifficultyTabView()
		{
			throw null;
		}
	}
}
