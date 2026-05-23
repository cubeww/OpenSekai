using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.Common;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Views;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Presenters
{
	public class ScreenLayerMusicScoreMaker : ScreenLayer
	{
		public class BootArg : BootArgBase
		{
			public int musicId;
			public string difficulty;
			public int vocalId;
			public string baseMusicScoreId;
			public int baseMusicDifficultyId;
			public MusicScoreMakerData MusicScoreMakerData;
			public long FocusTicks { get; set; }
			public int QuantizeDivision { get; set; }
			public MenuScreenType FromScreenType { get; set; }
			public string LastSavedDataHash { get; set; }
			public bool IsReturnFromTestPlay { get; set; }
			public int LastSavedDraftSlotNo { get; set; }
			public UserCustomMusicScoreDraft LastSavedDraft { get; set; }
			public string FullComboDataHash { get; set; }
			public string MusicScoreDataHashAtTestPlay { get; set; }
			public bool IsFromFullComboCheck { get; set; }
			public bool IsAllNotesIncludedInTestPlay { get; set; }
			public bool ShouldProceedToPublish { get; set; }
			public float CurrentMusicScoreScale { get; set; } = 1f;
			public CustomMusicScorePackage CustomMusicScorePackage { get; set; }
			public Action<Action, Action> FinishTransitionCallback { get; set; }
		}

		[SerializeField]
		private MusicScoreMakerView _MusicScoreMakerView;

		private MusicScoreMakerPresenter _presenter;

		private CancellationTokenSource _setupCts;

		private bool _isSetupComplete;

		private bool _hasBootError;

		private BootArg bootArg;

		public bool IsSetupComplete => _isSetupComplete;

		protected override void OnBoot(BootArgBase bootArg)
		{
			this.bootArg = bootArg as BootArg;
			_hasBootError = false;
			OnBootAsync().Forget();
		}

		public override object GetStackObject()
		{
			return bootArg;
		}

		public UniTask OnBeforeTestPlayAsync()
		{
			return UniTask.CompletedTask;
		}

		private async UniTask OnBootAsync()
		{
			try
			{
				await Setup();
			}
			catch (Exception ex)
			{
				_hasBootError = true;
				Debug.LogException(ex);
			}
			finally
			{
				ScreenBootDone();
			}
		}

		protected override void OnInitComponent()
		{
			if (_hasBootError)
			{
				return;
			}
			if (ScreenInsertDirection == InsertDirection.Back)
			{
				_MusicScoreMakerView?.ReactivatePreviewAfterTransition();
				if (MusicScoreMakerEventDispatcher.ExistsInstance && MusicScoreMakerEventDispatcher.Instance.PublishFirst<IsMusicPlayingEvent, bool>(new IsMusicPlayingEvent()))
				{
					MusicScoreMakerEventDispatcher.Instance.Publish(new PauseMusicEvent());
				}
				return;
			}
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new RefreshMusicScoreEvent());
			}
			base.OnInitComponent();
		}

		protected override void OnExitScene()
		{
			base.OnExitScene();
			Dispose();
		}

		private async UniTask Setup()
		{
			_isSetupComplete = false;
			_setupCts?.Cancel();
			_setupCts?.Dispose();
			_setupCts = new CancellationTokenSource();
			CancellationToken token = _setupCts.Token;

			string baseMusicScoreId = bootArg?.baseMusicScoreId;
			int baseMusicDifficultyId = bootArg?.baseMusicDifficultyId ?? -1;
			MusicScoreMakerModel model = new MusicScoreMakerModel(baseMusicScoreId, baseMusicDifficultyId);
			_presenter = await MusicScoreMakerPresenter.Create(model, _MusicScoreMakerView, token, bootArg?.FinishTransitionCallback);

			if (bootArg != null)
			{
				if (bootArg.QuantizeDivision >= 1)
				{
					_presenter.RestoreQuantizeDivision(bootArg.QuantizeDivision);
				}
				if (bootArg.LastSavedDraftSlotNo >= 1)
				{
					model.LastSavedDraftSlotNo = bootArg.LastSavedDraftSlotNo;
					model.LastSavedDraft = bootArg.LastSavedDraft;
				}
			}

			await CreateMusicScore(token);

			bool shouldShowPreviewStartDialog = false;
			MenuScreenType fromScreenType = MenuScreenType.Home;
			if (bootArg != null)
			{
				shouldShowPreviewStartDialog = bootArg.IsReturnFromTestPlay && bootArg.ShouldProceedToPublish;
				if (shouldShowPreviewStartDialog)
				{
					bootArg.ShouldProceedToPublish = false;
				}
				fromScreenType = bootArg.FromScreenType;
			}

			await _presenter.Setup(fromScreenType, token);
			if (shouldShowPreviewStartDialog)
			{
				_presenter.ShowPreviewStartDialog();
			}

			_setupCts?.Dispose();
			_setupCts = null;
			_isSetupComplete = true;
		}

		private async UniTask CreateMusicScore(CancellationToken token)
		{
			if (_presenter == null)
			{
				return;
			}
			if (bootArg == null)
			{
				Debug.LogError("MusicScoreMaker BootArg is null. Falling back to default score.");
				await _presenter.CreateMusicScore(1, "expert", 1, token);
				return;
			}

			if (bootArg.CustomMusicScorePackage != null)
			{
				await _presenter.CreateCustomMusicScore(bootArg.CustomMusicScorePackage, bootArg.MusicScoreMakerData, bootArg.FocusTicks, token);
			}
			else if (bootArg.MusicScoreMakerData != null)
			{
				await _presenter.CreateMusicScore(bootArg.musicId, bootArg.difficulty, bootArg.vocalId, bootArg.MusicScoreMakerData, bootArg.FocusTicks, token);
			}
			else
			{
				await _presenter.CreateMusicScore(bootArg.musicId, bootArg.difficulty, bootArg.vocalId, token);
			}

			if (bootArg.IsReturnFromTestPlay)
			{
				_presenter.Model.SetSavedDataHash(bootArg.LastSavedDataHash);
				_presenter.Model.SetFullComboDataHash(bootArg.FullComboDataHash, bootArg.IsFromFullComboCheck || bootArg.IsAllNotesIncludedInTestPlay);
				_presenter.Model.CurrentMusicScoreScale = bootArg.CurrentMusicScoreScale;
			}
		}

		public void Dispose()
		{
			_setupCts?.Cancel();
			_setupCts?.Dispose();
			_setupCts = null;
			_MusicScoreMakerView?.Dispose();
			_presenter?.Dispose();
			_presenter = null;
		}
	}
}
