using System;
using DG.Tweening;
using UiEffect;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public sealed class ScreenBackground : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private GradientColor _difficultyGradient;

		[SerializeField]
		private CanvasGroup _previewBackgroundCanvasGroup;

		[SerializeField]
		private RectTransform _previewBackgroundGradient;

		[SerializeField]
		private RectTransform _previewBackgroundBase;

		private Tweener _previewBackgroundFadeTween;

		private Tweener _difficultyColorFadeTween;

		public void Initialize()
		{
			throw null;
		}

		private void ApplyPreviewBackgroundSize()
		{
			throw null;
		}

		private void ApplyDifficulty(MusicDifficulty difficulty)
		{
			throw null;
		}

		private void FadeDifficultyColors(Color targetBottomColor, Color targetTopColor)
		{
			throw null;
		}

		private void ApplyVisiblePreview(bool active)
		{
			throw null;
		}

		private void OnDifficultyBackgroundChange(DifficultyBackgroundChangeEvent eventData)
		{
			throw null;
		}

		private void OnPreviewBackgroundVisibilityChange(PreviewBackgroundVisibilityChangeEvent eventData)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public ScreenBackground()
		{
			throw null;
		}
	}
}
