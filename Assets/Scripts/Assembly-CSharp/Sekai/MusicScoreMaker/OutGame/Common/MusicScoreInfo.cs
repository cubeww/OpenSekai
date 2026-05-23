using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sekai.Sound;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public sealed class MusicScoreInfo : MonoBehaviour, IDisposable
	{
		public enum ViewType
		{
			List = 1,
			Detail = 2
		}

		private enum PreviewLoopState
		{
			None = 0,
			FadingOut = 1,
			PreparingLiveMusic = 2
		}

		public const string BOOKMARK_ANIM_ACTIVATING = "Activating";

		public const string BOOKMARK_ANIM_CANCEL = "Cancel";

		public const string BOOKMARK_ANIM_ON = "On";

		public const string BOOKMARK_ANIM_OFF = "Off";

		private const float FadeOutDurationSeconds = 0.5f;
		private const float FadeInDurationSeconds = 0.5f;

		[SerializeField]
		private MusicScoreInfoView _view;

		private readonly MusicScoreInfoModel _model;

		private Func<CancellationToken, UniTask> _onPreEnterDetailAsync;

		private Func<CancellationToken, UniTask> _onPostExitDetailAsync;

		private Action<bool> _prevBackOverride;

		private Action<string, bool> _onBookMarkButtonClicked;

		private Action<string> _onMusicScoreReviewed;

		private Action<string> _onMusicScoreDeleted;

		private IngameBGMPlayer _ingameBGMPlayer;

		private PreviewLoopState _loopState;

		private CancellationTokenSource _prepareSyncPlaybackCts;

		private CancellationTokenSource _detailModeCts;

		private CancellationTokenSource _showBaseMusicScoreCts;

		[CanBeNull]
		public MusicScoreData AppliedMusicScoreData
		{
			get
			{
				return _model.AppliedMusicScoreData;
			}
		}

		public bool IsDetailInfoMode
		{
			get
			{
				return _model.ViewData.IsDetailInfoMode;
			}
		}

		public MusicDifficulty AppliedDifficulty
		{
			get
			{
				return AppliedMusicScoreData != null ? AppliedMusicScoreData.Difficulty : default;
			}
		}

		public void Setup(MusicScoreData musicScoreData, Defines.ContentType contentType, Func<CancellationToken, UniTask> onPreEnterDetailAsync, Func<CancellationToken, UniTask> onPostExitDetailAsync, Action<string, bool> onBookMarkButtonClicked = null, Action onToCreatorButtonClicked = null, bool isValidBookmark = true, Action onDecideButtonClicked = null, Action onDeleteButtonClicked = null, Action<string> onMusicScoreDeleted = null, Action<string> onMusicScoreReviewed = null)
		{
			_onPreEnterDetailAsync = onPreEnterDetailAsync;
			_onPostExitDetailAsync = onPostExitDetailAsync;
			_onBookMarkButtonClicked = onBookMarkButtonClicked;
			_onMusicScoreReviewed = onMusicScoreReviewed;
			_onMusicScoreDeleted = onMusicScoreDeleted;

			_model.SetIsValidBookmark(isValidBookmark);
			_model.ApplyContentType(contentType);
			_model.ApplyMusicScoreData(musicScoreData);
			_model.ApplyForList();

			TryInvokeView(() => _view.Setup(
				_model.ViewData,
				() => ApplyDetailInfoModeAsync(CreateDetailModeToken()).Forget(),
				() => _model.EnableReadyPreviewPlaying(),
				() => OnBookMarkButtonClicked().Forget(),
				onToCreatorButtonClicked,
				OnShowBaseMusicScore,
				onDecideButtonClicked,
				OnLockButtonClicked,
				onDeleteButtonClicked,
				ShowLiveMusicDownloadConfirmationDialog,
				CopyAppliedMusicScoreId,
				() => OnMusicScoreReviewButtonClicked().Forget()));

			PublishMusicScoreChangeEvents();
			PrepareSyncPlaybackAsync(musicScoreData).Forget();
		}

		public void SetupForDetail(MusicScoreData musicScoreData, Defines.ContentType contentType, Action onToCreatorButtonClicked = null, Action onDecideButtonClicked = null, Action onDeleteButtonClicked = null, Action<string> onMusicScoreDeleted = null)
		{
			_onMusicScoreDeleted = onMusicScoreDeleted;
			_model.ApplyContentType(contentType);
			_model.ApplyMusicScoreData(musicScoreData);
			_model.ApplyForDetail();

			TryInvokeView(() => _view.Setup(
				_model.ViewData,
				null,
				() => _model.EnableReadyPreviewPlaying(),
				() => OnBookMarkButtonClicked().Forget(),
				onToCreatorButtonClicked,
				OnShowBaseMusicScore,
				onDecideButtonClicked,
				OnLockButtonClicked,
				onDeleteButtonClicked,
				ShowLiveMusicDownloadConfirmationDialog,
				CopyAppliedMusicScoreId,
				() => OnMusicScoreReviewButtonClicked().Forget()));
			TryInvokeView(() => _view.ApplyDetailInfoMode(CreateDetailModeToken()));

			SetupForDetail();
		}

		private void SetupForDetail()
		{
			PublishMusicScoreChangeEvents();
			PrepareSyncPlaybackAsync(AppliedMusicScoreData).Forget();
			SetOnBackUIScreenForDetailInfo();
		}

		public void CancelPrepareSyncPlayback()
		{
			CancelAndDispose(ref _prepareSyncPlaybackCts);
		}

		public void OnPause()
		{
			_model.DisableReadyPreviewPlaying();
			TryInvokeView(() => _view.OnPause());
		}

		public void OnResume()
		{
			_model.EnableReadyPreviewPlaying();
			TryInvokeView(() => _view.OnResume());
		}

		private void RestoreBackUIOverride()
		{
			// TODO(original): restore ScreenManager.OnBackUIScreenOverride wiring after the navigation layer is restored.
			_prevBackOverride = null;
		}

		public void OnResumeForDetail()
		{
			OnResume();
			if (IsDetailInfoMode)
			{
				SetOnBackUIScreenForDetailInfo();
			}
		}

		private void PublishMusicScoreChangeEvents()
		{
			// TODO(original): publish PreviewBackgroundVisibilityChangeEvent/DifficultyBackgroundChangeEvent once event constructors are restored.
		}

		private void ClearMusicScoreChangeEvents()
		{
			// TODO(original): publish PreviewBackgroundVisibilityChangeEvent(false) and DifficultyBackgroundChangeEvent(default).
		}

		public void ApplyMusicScoreData([CanBeNull] MusicScoreData musicScoreData, bool prepareSyncPlayback = true)
		{
			_model.ApplyMusicScoreData(musicScoreData);
			TryInvokeView(() => _view.Refresh());
			PublishMusicScoreChangeEvents();

			if (prepareSyncPlayback)
			{
				PrepareSyncPlaybackAsync(musicScoreData).Forget();
			}
		}

		private async UniTask PrepareSyncPlaybackAsync([CanBeNull] MusicScoreData musicScoreData)
		{
			CancelAndDispose(ref _prepareSyncPlaybackCts);
			_prepareSyncPlaybackCts = new CancellationTokenSource();
			CancellationToken cancellationToken = _prepareSyncPlaybackCts.Token;

			_model.SetPreviewPlaying(false);
			_loopState = PreviewLoopState.None;

			try
			{
				if (musicScoreData == null)
				{
					DisposeIngameBGMPlayer();
					await TryInvokeViewAsync(() => _view.RefreshPreviewAsync(cancellationToken));
					return;
				}

				if (!_model.IsLiveMusicDownloaded)
				{
					DisposeIngameBGMPlayer();
				}

				await _model.CreateMusicScoreDataAsync(cancellationToken);
				_model.ResetScorePlayback();
				await TryInvokeViewAsync(() => _view.RefreshPreviewAsync(cancellationToken));

				bool liveMusicPrepared = _model.IsLiveMusicDownloaded && await PrepareLiveMusicAsync(cancellationToken);
				_model.SetPreviewPlaying(true);
				if (liveMusicPrepared)
				{
					UpdatePreviewWithMusicSync();
				}
			}
			catch (OperationCanceledException)
			{
			}
		}

		private UniTask<bool> PrepareLiveMusicAsync(CancellationToken cancellationToken, float fadeInDurationSec = 0f)
		{
			// TODO(original): restore IngameBGMPlayer/AssetBundle based live music playback.
			return UniTask.FromResult(false);
		}

		private void ShowLiveMusicDownloadConfirmationDialog()
		{
			// TODO(original): restore live music download confirmation dialog.
		}

		private UniTask OnConfirmedLiveMusicDownload(string assetBundleName)
		{
			// TODO(original): restore live music asset download flow.
			return UniTask.CompletedTask;
		}

		public IngameBGMPlayer TakeIngameBGMPlayer()
		{
			IngameBGMPlayer player = _ingameBGMPlayer;
			_ingameBGMPlayer = null;
			return player;
		}

		private void DisposeIngameBGMPlayer(bool resumeOutGameBgm = true)
		{
			_ingameBGMPlayer = null;
		}

		private static bool ShouldResumeOutGameBgm()
		{
			// TODO(original): restore ScreenManager stack inspection for BGM resume.
			return false;
		}

		private static bool TryGetScreenLayerBgmType(out ScreenLayerBgmType bgmType)
		{
			// TODO(original): restore current ScreenLayer BGM lookup.
			bgmType = default;
			return false;
		}

		public UniTask PlayMusicScorePreviewInAnimation(CancellationToken ct)
		{
			return TryInvokeViewAsync(() => _view.PlayMusicScorePreviewInAnimation(ct));
		}

		public UniTask FadeInAsync(float duration = 0.2f, CancellationToken ct = default)
		{
			return TryInvokeViewAsync(() => _view.FadeInAsync(duration, ct));
		}

		public UniTask FadeOutAsync(float duration = 0.2f, CancellationToken ct = default)
		{
			return TryInvokeViewAsync(() => _view.FadeOutAsync(duration, ct));
		}

		public void InitializeInAnimation()
		{
			TryInvokeView(() => _view.InitializeInAnimation());
		}

		private async UniTask ApplyDetailInfoModeAsync(CancellationToken ct)
		{
			CancelAndDispose(ref _detailModeCts);
			_detailModeCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
			CancellationToken linkedCt = _detailModeCts.Token;

			SetOnBackUIScreenForDetailInfo();
			if (_onPreEnterDetailAsync != null)
			{
				bool canceled = await _onPreEnterDetailAsync(linkedCt).SuppressCancellationThrow();
				if (canceled)
				{
					RestoreBackUIOverride();
					return;
				}
			}

			_model.ApplyForDetail();
			TryInvokeView(() => _view.ApplyDetailInfoMode(linkedCt));
		}

		private void SetOnBackUIScreenForDetailInfo()
		{
			// TODO(original): restore ScreenManager.OnBackUIScreenOverride; ClearDetailInfoModeAsync remains available for callers.
		}

		private async UniTask ClearDetailInfoModeAsync(CancellationToken ct, bool playPreview = true)
		{
			CancelAndDispose(ref _showBaseMusicScoreCts);
			CancelAndDispose(ref _detailModeCts);
			RestoreBackUIOverride();

			bool skipPreviewAnimation = playPreview && !_model.IsViewingBaseMusicScore && _model.IsReadyPreviewPlaying;
			if (!skipPreviewAnimation)
			{
				_model.ClearBaseMusicScoreData();
			}

			_model.ApplyForList();
			TryInvokeView(() => _view.ClearDetailInfoMode(skipPreviewAnimation));

			if (playPreview)
			{
				PublishMusicScoreChangeEvents();
			}

			if (!skipPreviewAnimation)
			{
				_model.ResetScorePlayback();
				if (playPreview)
				{
					PrepareSyncPlaybackAsync(AppliedMusicScoreData).Forget();
				}
			}

			if (_onPostExitDetailAsync != null)
			{
				await _onPostExitDetailAsync(ct);
			}
		}

		private void OnLockButtonClicked()
		{
			int? releaseConditionId = _model.GetReleaseConditionId();
			if (releaseConditionId.HasValue)
			{
				TryInvokeView(() => _view.ShowReleaseConditionsBalloon(releaseConditionId.Value));
			}
		}

		private async UniTask OnMusicScoreReviewButtonClicked()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				return;
			}

			string reviewedId = applied.Id;
			await _model.ExecutePostCustomMusicScoreReview();
			_onMusicScoreReviewed?.Invoke(reviewedId);
			TryInvokeView(() => _view.RefreshMusicScoreReviewButton());
			TryInvokeView(() => _view.ShowReviewedBalloon());
		}

		private async UniTask OnBookMarkButtonClicked()
		{
			MusicScoreData applied = AppliedMusicScoreData;
			if (applied == null)
			{
				return;
			}

			string musicScoreId = applied.Id;
			bool wasBookmarked = applied.IsBookmarked;
			if (wasBookmarked)
			{
				await _model.ExecuteDeleteCustomMusicScoreBookmark();
			}
			else
			{
				await _model.ExecutePostCustomMusicScoreBookmark();
			}

			_onBookMarkButtonClicked?.Invoke(musicScoreId, !wasBookmarked);
			TryInvokeView(() => _view.Refresh());
			TryInvokeView(() => _view.PlayBookmarkAnimation(!wasBookmarked));
		}

		private void Update()
		{
			UpdatePreviewPlayback();
		}

		private void UpdatePreviewPlayback()
		{
			if (AdvancePreviewTime())
			{
				OnPreviewLooped();
			}

			UpdateFade();
			TryInvokeView(() => _view.SyncWithMusicPlayback());
		}

		private bool AdvancePreviewTime()
		{
			return _model.IsPlayingPreview && _model.AddPreviewCurrentMusicTime(Time.deltaTime);
		}

		private void OnPreviewLooped()
		{
			if (_loopState != PreviewLoopState.None)
			{
				return;
			}

			_loopState = PreviewLoopState.FadingOut;
			TryInvokeView(() => _view.FadeInPreviewOnLoop(FadeInDurationSeconds, CreatePreparePlaybackToken()));
			_loopState = PreviewLoopState.None;
		}

		private void UpdateFade()
		{
			// TODO(original): restore preview fade state machine around live music loop transitions.
		}

		private bool ShouldSyncWithLiveMusic()
		{
			return _ingameBGMPlayer != null && _model.IsLiveMusicDownloaded;
		}

		private bool UpdatePreviewWithMusicSync()
		{
			if (!ShouldSyncWithLiveMusic())
			{
				return false;
			}

			return _model.SyncPreviewTimeWithMusic(_model.CurrentLiveMusicSec);
		}

		private void OnShowBaseMusicScore()
		{
			CancelAndDispose(ref _showBaseMusicScoreCts);
			_showBaseMusicScoreCts = new CancellationTokenSource();
			ShowBaseMusicScoreAsync(_showBaseMusicScoreCts.Token).Forget();
		}

		private async UniTask ShowBaseMusicScoreAsync(CancellationToken ct)
		{
			await _model.FetchAndApplyBaseMusicScoreDataAsync(ct);
			if (ct.IsCancellationRequested)
			{
				return;
			}

			TryInvokeView(() => _view.Refresh());
			TryInvokeView(() => _view.RefreshChildMusicScoreInfo());
			PrepareSyncPlaybackAsync(AppliedMusicScoreData).Forget();
		}

		public async UniTask HandleMusicScoreDeletedAsync(string deletedId, string wordingKey)
		{
			if (AppliedMusicScoreData == null || AppliedMusicScoreData.Id != deletedId)
			{
				return;
			}

			_onMusicScoreDeleted?.Invoke(deletedId);
			await CloseMusicScoreInfo();
		}

		public async UniTask CloseMusicScoreInfo()
		{
			await FadeOutAsync(FadeOutDurationSeconds);
			ClearMusicScoreChangeEvents();
			CancelPrepareSyncPlayback();
			CancelAndDispose(ref _detailModeCts);
			CancelAndDispose(ref _showBaseMusicScoreCts);
			DisposeIngameBGMPlayer();
		}

		public void Dispose()
		{
			ClearMusicScoreChangeEvents();
			CancelPrepareSyncPlayback();
			CancelAndDispose(ref _detailModeCts);
			CancelAndDispose(ref _showBaseMusicScoreCts);
			DisposeIngameBGMPlayer();
			_model.Dispose();
			TryInvokeView(() => _view.Dispose());
		}

		private void OnDestroy()
		{
			Dispose();
		}

		public MusicScoreInfo()
		{
			_model = new MusicScoreInfoModel();
		}

		private CancellationToken CreateDetailModeToken()
		{
			CancelAndDispose(ref _detailModeCts);
			_detailModeCts = new CancellationTokenSource();
			return _detailModeCts.Token;
		}

		private CancellationToken CreatePreparePlaybackToken()
		{
			if (_prepareSyncPlaybackCts == null)
			{
				_prepareSyncPlaybackCts = new CancellationTokenSource();
			}

			return _prepareSyncPlaybackCts.Token;
		}

		private void CopyAppliedMusicScoreId()
		{
			string musicScoreId = AppliedMusicScoreData?.Id;
			if (string.IsNullOrEmpty(musicScoreId))
			{
				return;
			}

			try
			{
				MusicScoreMakerUtility.CopyCustomMusicScoreIdToClipboard(musicScoreId);
			}
			catch
			{
				// TODO(original): restore MusicScoreMakerUtility.CopyCustomMusicScoreIdToClipboard.
			}
		}

		private static void TryInvokeView(Action action)
		{
			if (action == null)
			{
				return;
			}

			try
			{
				action();
			}
			catch
			{
				// TODO(original): remove this guard when MusicScoreInfoView rendering methods are restored.
			}
		}

		private static async UniTask TryInvokeViewAsync(Func<UniTask> action)
		{
			if (action == null)
			{
				return;
			}

			try
			{
				await action();
			}
			catch (OperationCanceledException)
			{
			}
			catch
			{
				// TODO(original): remove this guard when MusicScoreInfoView animation methods are restored.
			}
		}

		private static void CancelAndDispose(ref CancellationTokenSource cts)
		{
			if (cts == null)
			{
				return;
			}

			try
			{
				cts.Cancel();
			}
			catch
			{
			}

			cts.Dispose();
			cts = null;
		}
	}
}
