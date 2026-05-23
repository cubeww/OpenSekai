using System;
using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.Sound;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Create
{
	public class ScreenLayerMusicScoreMakerDifficultySelectForCreateView : MonoBehaviour
	{
		[SerializeField]
		private UITextureLoader _musicJacketLoader;

		[SerializeField]
		private CustomTextMesh _musicName;

		[SerializeField]
		private CustomTextMesh _musicArtistName;

		[SerializeField]
		private DifficultySelectButton[] _difficultySelectButtons;

		[SerializeField]
		private ScreenBackground _screenBackground;

		private ScreenLayerMusicScoreMakerDifficultySelectForCreateViewData _viewData;

		private Action<MusicDifficulty> _onSelectDifficulty;

		private MusicShortBGMPlayer _musicShortBGMPlayer;

		private Action<MusicShortBGMPlayer> _onBGMDispose;

		public void Setup(ScreenLayerMusicScoreMakerDifficultySelectForCreateViewData viewData, Action<MusicDifficulty> onSelectDifficulty)
		{
			throw null;
		}

		public void SetBGMDisposeCallback(Action<MusicShortBGMPlayer> callback)
		{
			throw null;
		}

		public void LoadAndPlayBGM()
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		private void LoadJacket()
		{
			throw null;
		}

		private void SetupDifficultyButtons()
		{
			throw null;
		}

		private void Unload()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public ScreenLayerMusicScoreMakerDifficultySelectForCreateView()
		{
			throw null;
		}
	}
}
