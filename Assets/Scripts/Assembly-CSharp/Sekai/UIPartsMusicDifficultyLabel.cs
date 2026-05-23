using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsMusicDifficultyLabel : MonoBehaviour
	{
		[SerializeField]
		private UIPartsMusicDifficulty _musicDifficulty;

		[SerializeField]
		private CustomTextMesh _difficultyText;

		[SerializeField]
		private CustomImage _customMusicScoreIcon;

		public void Setup(MusicDifficulty difficulty, bool showIcon = false)
		{
			if (_musicDifficulty != null)
			{
				_musicDifficulty.Setup(difficulty);
			}
			if (_difficultyText != null)
			{
				_difficultyText.text = difficulty.ToString().ToUpperInvariant();
			}
			if (_customMusicScoreIcon != null)
			{
				_customMusicScoreIcon.gameObject.SetActive(showIcon);
			}
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public UIPartsMusicDifficultyLabel()
		{
		}
	}
}
