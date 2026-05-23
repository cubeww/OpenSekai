using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Create
{
	public class DifficultySelectButton : UIPartsMusicDifficulty
	{
		[SerializeField]
		private GameObject _defaultObject;

		[SerializeField]
		private CustomButton _button;

		[SerializeField]
		private CustomTextMesh _difficultyText;

		[SerializeField]
		private CustomTextMesh _playLevelText;

		[SerializeField]
		private CustomTextMesh _appendPlayLevelText;

		public void Setup(MasterMusicDifficultyModel musicDifficultyModel, Action onClick)
		{
			throw null;
		}

		public void Setup(MusicDifficulty musicDifficulty, int playLevel, Action onClick)
		{
			throw null;
		}

		public DifficultySelectButton()
		{
			throw null;
		}
	}
}
