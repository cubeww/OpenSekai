using System;
using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Publish
{
	public sealed class MusicScorePublishConfirmView : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private UITextureLoader _musicJacketLoader;

		[SerializeField]
		private CustomTextMesh _musicNameText;

		[SerializeField]
		private CustomTextMesh _musicArtistNameText;

		[SerializeField]
		private GameObject _baseMusicScoreMaker;

		[SerializeField]
		private CustomInputFieldTextMesh _titleInputField;

		[SerializeField]
		private CustomInputFieldTextMesh _descriptionInputField;

		[SerializeField]
		private CustomIndexToggleGroup _difficultyToggleGroup;

		[SerializeField]
		private CustomDropdown _playLevelDropdown;

		[SerializeField]
		private MusicScorePublishTagView _tagView;

		[SerializeField]
		private CustomIndexToggleGroup _derivativeAllowedToggleGroup;

		public void Setup(Action<string> onEndEditTitleSuccess, Action onEndEditTitleError, Action<string> onEndEditDescriptionSuccess, Action onEndEditDescriptionError, Action<MusicDifficulty> onDifficultySelected, Action<int> onPlayLevelSelected, Action onTagSelectStartButtonClicked, Action<int> onTagRemoved, Action<int> onDerivativeAllowedChanged)
		{
			throw null;
		}

		private void SetupDifficultyToggle(Action<MusicDifficulty> onDifficultySelected)
		{
			throw null;
		}

		private void SetupDerivativeAllowedToggle(Action<int> onDerivativeAllowedChanged)
		{
			throw null;
		}

		public void RefreshMusicScoreInfo(int musicId, bool hasBaseMusicScore)
		{
			throw null;
		}

		public void RefreshTitle(string title)
		{
			throw null;
		}

		public void RefreshDescription(string description)
		{
			throw null;
		}

		public void RefreshDifficulty(MusicDifficulty selectedDifficulty, MusicDifficulty recommendedDifficulty, MusicDifficulty[] validDifficulties)
		{
			throw null;
		}

		public void RefreshPlayLevel(List<string> keys)
		{
			throw null;
		}

		public void RefreshTags(List<int> selectedTagIds)
		{
			throw null;
		}

		public void RefreshDerivativeAllowed(int index)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScorePublishConfirmView()
		{
			throw null;
		}
	}
}
