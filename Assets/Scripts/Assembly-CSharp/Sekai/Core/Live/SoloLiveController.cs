using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Presenters;
using UnityEngine;

namespace Sekai.Core.Live
{
	public class SoloLiveController : BaseLiveController
	{
		private bool isTestPlayFinishedCalled;
		private bool liveViewMusicStartInvoked;
		private Coroutine finishCoroutine;
		private Coroutine resumeCoroutine;
		private LiveViewBase[] liveViews;
		private LiveLogic liveLogic;

		protected override void OnAwake()
		{
			base.OnAwake();
			BootData = UserDataManager.Instance.FreeLiveBootData;
			if (BootData == null)
			{
				Debug.LogWarning("FreeLiveBootData is null. Test play cannot start.");
				return;
			}

			currentMusicTimeMs = System.Math.Max(0L, BootData.MusicData?.StartMusicTimeMs ?? 0L);
			Setup();
			LoadSound();
			liveViews = LiveViewFactory.Create(BootData, transform);
			LiveViewExt.Setup(liveViews, this);
			RetryProcess();
			liveOutUIController?.Initialize(BootData.LivePlayMode, BaseCamera);
			LiveViewExt.OnLoad(liveViews);
			StartCoroutine(LiveStart(6f));
		}

		protected override void OnBeforeMusicStartWait(float waitSeconds)
		{
			if (BootData?.ReleaseTransitionBeforeMusicStart != true || waitSeconds <= 0f || liveViewMusicStartInvoked)
			{
				return;
			}

			liveViewMusicStartInvoked = true;
			LiveViewExt.MusicStart(liveViews, currentAudioLatencyMusicTimeMs);
		}

		protected override void OnMusicStart()
		{
			LiveTransitioner.SafeFinish(null, null);
			base.OnMusicStart();
			if (!liveViewMusicStartInvoked)
			{
				liveViewMusicStartInvoked = true;
				LiveViewExt.MusicStart(liveViews, currentAudioLatencyMusicTimeMs);
			}
		}

		protected override void OnRhythmGameStart()
		{
			LiveViewExt.RhythmGameStart(liveViews);
			base.OnRhythmGameStart();
			liveLogic?.RefreshInput();
		}

		protected override void OnUpdate()
		{
			base.OnUpdate();
			if (state != LiveControllerState.Playing)
			{
				return;
			}

			UpdateMusicTime();
			float fillerSec = BootData?.MusicData?.Music?.fillerSec ?? 0f;
			liveLogic?.OnUpdate(currentAudioLatencyMusicTimeMs - fillerSec, currentGameTime);
			if (liveLogic?.result == 2 && liveLogic.IsNotesAllFinished)
			{
				OnFinished();
			}
			if (BootData?.IsAuto == true)
			{
				liveLogic?.OnAutoInput();
			}
			else
			{
				liveLogic?.OnInput();
			}
			LiveViewExt.OnUpdate(liveViews, currentAudioLatencyMusicTimeMs);
		}

		protected override void OnPause()
		{
			if ((state == LiveControllerState.Playing || state == LiveControllerState.ResumeCountDown) && result == 0)
			{
				state = LiveControllerState.Pause;
				SoundManager.Instance.Pause();
				LiveViewExt.Pause(liveViews);
				ShowPauseDialog();
			}
		}

		public void ShowPauseDialog()
		{
			if (BootData?.MusicData?.IsTestPlay == true)
			{
				ScreenLayerMusicScoreMaker.BootArg bootArg = MusicScoreMakerEntryPoint.BootData?.bootData;
				if (bootArg != null && bootArg.IsFromFullComboCheck)
				{
					liveOutUIController?.ShowMusicScoreMakerFullComboCheckPauseDialog(OnResume, OnReturnToMusicScoreMaker, OnTestPlayRetryConfirm);
				}
				else
				{
					liveOutUIController?.ShowMusicScoreMakerTestPlayPauseDialog(OnResume, OnReturnToMusicScoreMaker, OnTestPlayRetryConfirm);
				}
				return;
			}

			liveOutUIController?.ShowPauseDialog(OnResume, OnRetireConfirm, OnRetryConfirm);
		}

		public void PauseLive()
		{
			if ((state != LiveControllerState.Playing && state != LiveControllerState.ResumeCountDown) || result != 0)
			{
				return;
			}

			state = LiveControllerState.Pause;
			SoundManager.Instance.Pause();
			LiveViewExt.Pause(liveViews);
		}

		protected override void OnResume()
		{
			if (state != LiveControllerState.Pause)
			{
				return;
			}

			state = LiveControllerState.ResumeCountDown;
			liveOutUIController?.Destroy();
			LiveViewExt.Countdown(liveViews);
			if (resumeCoroutine != null)
			{
				StopCoroutine(resumeCoroutine);
			}
			resumeCoroutine = StartCoroutine(ResumeCoroutine());
		}

		public void ResumeNoCountDown()
		{
			if (state != LiveControllerState.Pause)
			{
				return;
			}

			state = LiveControllerState.ResumeCountDown;
			liveOutUIController?.Destroy();
			SoundManager.Instance.ResumeIngame(currentMusicTimeMs);
			state = LiveControllerState.Playing;
			LiveViewExt.Resume(liveViews, currentAudioLatencyMusicTimeMs);
			liveLogic?.RefreshInput();
		}

		public void ShowConsecutiveAutoLiveRetireDialog()
		{
			CallPreExit();
		}

		protected override void OnRetry()
		{
			liveOutUIController?.Destroy();
			SoundManager.Instance.StopIngame();
			isTestPlayFinishedCalled = false;
			liveViewMusicStartInvoked = false;
			result = 0;
			state = LiveControllerState.None;
			currentMusicTimeMs = System.Math.Max(0L, BootData?.MusicData?.StartMusicTimeMs ?? 0L);
			RetryProcess();
			LiveViewExt.Retry(liveViews);
			StartCoroutine(LiveStart(6f));
		}

		private void RetryProcess()
		{
			if (liveLogic != null)
			{
				liveLogic.OnFinished -= OnFinished;
				liveLogic.OnFailure -= OnFailure;
			}

			liveLogic = new LiveLogic(liveBundleBuildData);
			liveLogic.Setup(BootData, liveViews);
			liveLogic.SetSkillLogic(new SkillLogic());
			liveLogic.SetScoreLogic(new ScoreLogic(liveBundleBuildData));
			liveLogic.OnFinished += OnFinished;
			liveLogic.OnFailure += OnFailure;
		}

		protected override void OnRetire()
		{
			liveOutUIController?.Destroy();
			result = 2;
			PreExit(0f, 4f);
		}

		private void OnRetireByMySelf()
		{
			liveOutUIController?.Destroy();
			result = 1;
			PreExit(0f, 0f);
		}

		private void OnConfirmCancel()
		{
			liveOutUIController?.ShowPauseDialog(OnResume, OnRetireConfirm, OnRetryConfirm);
		}

		private void OnTestPlayConfirmCancel()
		{
			liveOutUIController?.ShowMusicScoreMakerTestPlayPauseDialog(OnResume, OnReturnToMusicScoreMaker, OnTestPlayRetryConfirm);
		}

		private void OnRetireConfirm()
		{
			liveOutUIController?.ShowConfirmRetireDialog(OnRetireByMySelf, OnConfirmCancel);
		}

		private void OnRetryConfirm()
		{
			liveOutUIController?.ShowConfirmRetryDialog(Retry, OnConfirmCancel);
		}

		private void OnTestPlayRetryConfirm()
		{
			liveOutUIController?.ShowConfirmRetryDialog(Retry, OnTestPlayConfirmCancel);
		}

		private System.Collections.IEnumerator ResumeCoroutine()
		{
			liveLogic?.RefreshInput();
			yield return new WaitForSeconds(3f);
			SoundManager.Instance.ResumeIngame(currentMusicTimeMs);
			state = LiveControllerState.Playing;
			LiveViewExt.Resume(liveViews, currentAudioLatencyMusicTimeMs);
			SoundManager.Instance.SetAudioSyncedUnityTimer(cueId);
			resumeCoroutine = null;
		}

		private void OnFailure()
		{
			currentMusicTimeMs = SoundManager.Instance.GetAudioSyncedUnityTimer();
		}

		protected override void OnFinished()
		{
			if (BootData?.MusicData?.IsTestPlay == true)
			{
				OnTestPlayFinished();
			}
			else
			{
				base.OnFinished();
			}
		}

		private void OnTestPlayFinished()
		{
			if (isTestPlayFinishedCalled || state == LiveControllerState.Finish)
			{
				return;
			}

			isTestPlayFinishedCalled = true;
			UpdateFullComboDataHashIfNeeded();
			state = LiveControllerState.None;
			ShowTestPlayFinishDialog();
		}

		private void ShowTestPlayFinishDialog()
		{
			ScreenLayerMusicScoreMaker.BootArg bootArg = MusicScoreMakerEntryPoint.BootData?.bootData;
			if (bootArg != null && bootArg.IsFromFullComboCheck)
			{
				if (IsFullCombo())
				{
					liveOutUIController?.ShowMusicScoreMakerFullComboSuccessDialog(OnReturnToMusicScoreMaker, Retry, OnProceedToPublish);
				}
				else
				{
					liveOutUIController?.ShowMusicScoreMakerFullComboFailedDialog(OnReturnToMusicScoreMaker, Retry);
				}
				return;
			}

			liveOutUIController?.ShowMusicScoreMakerTestPlayFinishDialog(Retry, CallPreExit);
		}

		private void OnProceedToPublish()
		{
			ScreenLayerMusicScoreMaker.BootArg bootArg = MusicScoreMakerEntryPoint.BootData?.bootData;
			if (bootArg != null)
			{
				bootArg.ShouldProceedToPublish = true;
			}
			OnReturnToMusicScoreMaker();
		}

		private void OnReturnToMusicScoreMaker()
		{
			liveOutUIController?.Destroy();
			state = LiveControllerState.Finish;
			result = 0;
			SetFinish(0f, 0f, BootData?.MusicData?.IsTestPlay == true ? 0.1f : 2f);
		}

		protected override void PreExit(float delay, float waitTime)
		{
			if (state == LiveControllerState.Finish)
			{
				return;
			}

			if (result == 0)
			{
				result = 3;
			}

			state = LiveControllerState.Finish;
			SetFinish(delay, waitTime, BootData?.MusicData?.IsTestPlay == true ? 0.1f : 2f);
		}

		protected override void OnExit()
		{
			LiveViewExt.Finish3D(liveViews);
			LiveViewExt.OnUnload(liveViews);
			MenuScreenType? returnScreenType = (BootData as FreeLiveBootData)?.ReturnScreenType;
			base.OnExit();
			if (returnScreenType.HasValue)
			{
				if (returnScreenType.Value == MenuScreenType.MusicScoreMakerTop)
				{
					MusicScoreMakerEntryPoint.BootData = null;
				}
				SceneManager.Instance.RequestScene(SceneManager.Scene.MusicScoreMaker);
			}
			else if (BootData?.MusicData?.IsTestPlay == true)
			{
				SceneManager.Instance.RequestScene(SceneManager.Scene.MusicScoreMaker);
			}
		}

		private void SetFinish(float delay, float waitTime, float finishWaitSeconds)
		{
			if (finishCoroutine != null)
			{
				StopCoroutine(finishCoroutine);
			}
			if (liveLogic != null)
			{
				liveLogic.OnFinished -= OnFinished;
				liveLogic.OnFailure -= OnFailure;
			}
			finishCoroutine = StartCoroutine(SetFinishCoroutine(delay, waitTime, finishWaitSeconds));
		}

		private System.Collections.IEnumerator SetFinishCoroutine(float delay, float waitTime, float finishWaitSeconds)
		{
			if (delay > 0f)
			{
				yield return new WaitForSeconds(delay);
			}
			yield return ResultAnim(waitTime, finishWaitSeconds);
		}

		private System.Collections.IEnumerator ResultAnim(float waitTime, float finishWaitSeconds)
		{
			LiveViewExt.Result(liveViews, (int)GetLiveResultAnimationType());
			if (waitTime > 0f)
			{
				yield return new WaitForSeconds(waitTime);
			}
			LiveViewExt.Finish(liveViews, finishWaitSeconds);
			if (finishWaitSeconds > 0f)
			{
				yield return new WaitForSeconds(finishWaitSeconds);
			}
			yield return null;
			Exit();
		}

		private LiveResultAnimationType GetLiveResultAnimationType()
		{
			if (BootData?.MusicData?.IsTestPlay == true || result < 2)
			{
				return LiveResultAnimationType.None;
			}

			if (result == 2)
			{
				return LiveResultAnimationType.Failure;
			}

			if (result != 3)
			{
				Debug.LogErrorFormat("Unsupported live result: {0}", result);
				return LiveResultAnimationType.None;
			}

			LiveScore score = liveLogic?.Score ?? default;
			if (score.totalComboCount == score.perfectCount)
			{
				return LiveResultAnimationType.AllPerfect;
			}
			if (score.totalComboCount == score.maxCombo)
			{
				return LiveResultAnimationType.FullCombo;
			}
			return score.life > 0 ? LiveResultAnimationType.Clear : LiveResultAnimationType.LifeZero;
		}

		private bool IsFullCombo()
		{
			LiveScore score = liveLogic?.Score ?? default;
			return score.totalComboCount > 0 && score.maxCombo >= score.totalComboCount;
		}

		private void UpdateFullComboDataHashIfNeeded()
		{
			ScreenLayerMusicScoreMaker.BootArg bootArg = MusicScoreMakerEntryPoint.BootData?.bootData;
			if (bootArg == null || !bootArg.IsFromFullComboCheck || !IsFullCombo())
			{
				return;
			}

			bootArg.FullComboDataHash = bootArg.MusicScoreDataHashAtTestPlay;
		}
	}
}
