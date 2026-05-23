using System;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreFilterDialog : Common2ButtonDialog, IDisposable
	{
		[SerializeField]
		private MusicScoreFilterView _view;

		private readonly MusicScoreFilterModel _model;

		private Action<MusicScoreFilterData> _onDecide;

		public static void Show(MusicScoreFilterData musicScoreFilterData, MusicDifficulty difficulty, Action<MusicScoreFilterData> onDecide, Action onCancel = null)
		{
			throw null;
		}

		protected override void OnClickOK()
		{
			throw null;
		}

		public override void Close()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		private void Setup(MusicScoreFilterData musicScoreFilterData, MusicDifficulty difficulty, Action<MusicScoreFilterData> onDecide)
		{
			throw null;
		}

		private void SetupClearStatus()
		{
			throw null;
		}

		private void SetupPlayLevel()
		{
			throw null;
		}

		private void SetupFullComboRate()
		{
			throw null;
		}

		private void SetupReviewCount()
		{
			throw null;
		}

		private void OnClearStatusChanged(int index)
		{
			throw null;
		}

		private void ApplyMinPlayLevelByIndex(int index)
		{
			throw null;
		}

		private void ApplyMaxPlayLevelByIndex(int index)
		{
			throw null;
		}

		private void SyncPlayLevelDropdowns()
		{
			throw null;
		}

		private void OnFullComboRateTypeChanged(int index)
		{
			throw null;
		}

		private void OnMinFullComboRateInputEnd(string text)
		{
			throw null;
		}

		private void OnMaxFullComboRateInputEnd(string text)
		{
			throw null;
		}

		private void SetupDerivative()
		{
			throw null;
		}

		private void OnDerivativeChanged(int index)
		{
			throw null;
		}

		private void OnReviewCountTypeChanged(int index)
		{
			throw null;
		}

		private void OnReviewCountInputEnd(string text)
		{
			throw null;
		}

		public MusicScoreFilterDialog()
		{
			throw null;
		}
	}
}
