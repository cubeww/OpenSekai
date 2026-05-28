using System;
using System.Collections;
using CP;
using CriWare;
using Sekai.Live;
using UnityEngine;
using UnityEngine.Scripting;

namespace Sekai.Core.Live
{
	public class BaseLiveController : BaseController
	{
		protected enum LiveControllerState
		{
			None = 0,
			Playing = 1,
			Pause = 2,
			ResumeCountDown = 3,
			Finish = 6
		}

		protected LiveControllerState state;
		protected int result;
		protected long currentMusicTimeMs;
		protected long musicLength = 120000L;
		protected uint cueId;
		protected double currentGameTime;
		protected float timingAdjust;
		protected float accumulateTimeApartFromPause;
		protected LiveBundleBuildData liveBundleBuildData;

		public LiveBootDataBase BootData { get; protected set; }

		public LiveSettingData Settings { get; protected set; }

		public RenderTexture BackgroundTexture { get; protected set; }

		public Camera BaseCamera { get; set; }

		public LiveOutUIController liveOutUIController { get; protected set; }

		public bool JustPlayingState => state == LiveControllerState.Playing;

		public bool IsPlayableState => state == LiveControllerState.Playing || state == LiveControllerState.ResumeCountDown;

		protected float currentAudioLatencyMusicTimeMs => Mathf.Max(timingAdjust + currentMusicTimeMs / 1000f, 0f);

		protected override void OnAwake()
		{
			Application.backgroundLoadingPriority = ThreadPriority.High;
			liveBundleBuildData = Resources.Load<LiveBundleBuildData>(LiveConfig.ConfigBundleNamePath);
			Settings = LiveSettingData.LoadFromStorage();
			LiveConfig.LoadMasterConfig();
			LiveConfig.LoadOptionData();
			LiveConfig.CacheSpeedTime = 0f;
			liveOutUIController = new LiveOutUIController();
		}

		protected virtual void Setup()
		{
			FramerateUtility.SetFrameRate(-1);
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			LiveBootDataBase bootData = BootData;
			if (bootData != null)
			{
				Sekai.Live.MVQualityType qualityType = ConvertQualityType(bootData.MVQualityType);
				ScreenConfig.ScreenSize renderTextureSize = LiveConfig.GetRenderTextureSize(qualityType, bootData.LivePlayMode);
				BackgroundTexture = new RenderTexture(renderTextureSize.width, renderTextureSize.height, 24, RenderTextureFormat.ARGB32);
				ScreenConfig.DownResolution(qualityType);
			}

			ApplicationLocalSettings localSettings = ApplicationLocalSettings.LoadFromStorage();
			ApplicationLocalSettings.VolumeSettings liveVolume = localSettings.LiveVolume ?? localSettings.SetupLiveVolume();
			SoundManager.Instance.SetupVolume(1f, liveVolume.Bgm, liveVolume.Se, liveVolume.Voice);

			timingAdjust = (Settings?.TimingAdjustData ?? 0f) * 0.016667f;
		}

		protected virtual void LoadSound()
		{
			SoundManager.Instance.LoadSoundBundle("live/sound/live_se/default", true);
			LoadTapSeSoundBundle();

			string cueName = ResolveMusicCueName();
			if (!string.IsNullOrEmpty(cueName))
			{
				SoundManager.Instance.LoadSoundBundle(GetLiveMusicBundleName(cueName), true);
				UpdateMusicLength(cueName);
			}
		}

		private void LoadTapSeSoundBundle()
		{
			int noteSeIndex = Settings?.NoteSeIndex ?? 0;
			LiveConfig.SetNoteSeName(noteSeIndex);
			SoundManager.Instance.LoadSoundBundle("live/tap_se/" + LiveConfig.NoteSeName, true);
			SoundManager.Instance.ResumeIngameSe();
		}

		protected virtual IEnumerator LiveStart(float waitTime)
		{
			Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
			yield return null;

			GC.Collect(0, GCCollectionMode.Forced, true, true);
			GC.WaitForPendingFinalizers();
#if !UNITY_EDITOR
			GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
#endif

			bool keepTransitionUntilMusicStart = ShouldKeepTransitionUntilMusicStart();
			if (!keepTransitionUntilMusicStart)
			{
				LiveTransitioner.SafeForceFinish(null);
			}

			yield return StartCoroutine(MusicReady());

			float musicStartWait = ShouldWaitBeforeMusicStart() ? waitTime : 0.35f;
			OnBeforeMusicStartWait(musicStartWait);
			if (musicStartWait > 0f)
			{
				yield return new WaitForSeconds(musicStartWait);
			}

			OnMusicStart();
			OnRhythmGameStart();
		}

		protected virtual bool ShouldKeepTransitionUntilMusicStart()
		{
			return BootData?.MusicData?.PlayStartEffectEnabled == true && BootData?.ReleaseTransitionBeforeMusicStart != true;
		}

		protected virtual bool ShouldWaitBeforeMusicStart()
		{
			return BootData?.MusicData?.PlayStartEffectEnabled == true;
		}

		protected virtual void OnBeforeMusicStartWait(float waitSeconds)
		{
		}

		protected virtual IEnumerator MusicReady()
		{
			string cueName = ResolveMusicCueName();
			if (!string.IsNullOrEmpty(cueName))
			{
				cueId = SoundManager.Instance.PrepareIngameBGM(cueName, currentMusicTimeMs / 1000f);
			}

			yield return null;
		}

		protected virtual void OnMusicStart()
		{
			SoundManager.Instance.ResumePreparedPlaybackIngame();
			accumulateTimeApartFromPause = 0f;
		}

		protected virtual void OnRhythmGameStart()
		{
			result = 0;
			state = LiveControllerState.Playing;
		}

		protected override void OnUpdate()
		{
			currentGameTime = Time.realtimeSinceStartupAsDouble;
			base.OnUpdate();

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				OnBackKey();
			}

			if (state != LiveControllerState.Pause)
			{
				accumulateTimeApartFromPause += Time.unscaledDeltaTime;
			}
		}

		protected virtual void UpdateMusicTime()
		{
			long audioTime = SoundManager.Instance.GetAudioSyncedUnityTimer();
			if (audioTime <= 0L)
			{
				if (currentMusicTimeMs >= 1L)
				{
					audioTime = musicLength;
				}
				else
				{
					return;
				}
			}

			currentMusicTimeMs = audioTime;
		}

		public void Retry()
		{
			OnRetry();
		}

		public void Retire()
		{
			OnRetire();
		}

		public void Pause()
		{
			OnPause();
		}

		public void Resume()
		{
			OnResume();
		}

		public void CallPreExit()
		{
			if (BootData?.MusicData?.IsTestPlay == true)
			{
				PreExit(0f, 0f);
				return;
			}

			if (ShouldReturnCustomMusicScoreManagerAfterResult())
			{
				PreExit(1f, 4f);
				return;
			}

			long restMs = Math.Max(0L, musicLength - currentMusicTimeMs);
			PreExit(1f, Mathf.Max(restMs / 1000f - 1f, 4f));
		}

		private bool ShouldReturnCustomMusicScoreManagerAfterResult()
		{
			if (BootData?.IsCustomMusicScore != true)
			{
				return false;
			}

			return BootData is FreeLiveBootData freeLiveBootData
				&& freeLiveBootData.ReturnScreenType == MenuScreenType.MusicScoreMakerTop;
		}

		protected virtual void OnBackKey()
		{
			if (state == LiveControllerState.Pause)
			{
				OnResume();
			}
			else if (state == LiveControllerState.Playing)
			{
				OnPause();
			}
		}

		protected virtual void OnPause()
		{
		}

		protected virtual void OnResume()
		{
		}

		protected virtual void OnRetry()
		{
		}

		protected virtual void OnRetire()
		{
		}

		protected virtual void OnFinished()
		{
			CallPreExit();
		}

		protected virtual void PreExit(float delay, float waitTime)
		{
		}

		protected override void OnExit()
		{
			SoundManager.Instance.StopAll();
			GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
			ScreenConfig.ResetResolution();

			if (BackgroundTexture != null)
			{
				BackgroundTexture.Release();
				BackgroundTexture = null;
			}
		}

		protected string ResolveMusicCueName()
		{
			if (BootData is FreeLiveBootData freeLiveBootData && !string.IsNullOrEmpty(freeLiveBootData.CustomMusicScoreId))
			{
				return "custom_" + freeLiveBootData.CustomMusicScoreId;
			}

			if (!string.IsNullOrEmpty(BootData?.MusicData?.Vocal?.assetbundleName))
			{
				return BootData.MusicData.Vocal.assetbundleName;
			}

			return BootData?.MusicData?.Music?.assetbundleName;
		}

		private void UpdateMusicLength(string cueName)
		{
			CriAtomEx.CueInfo[] cueInfos = SoundManager.Instance.GetCueInfos(cueName);
			foreach (CriAtomEx.CueInfo cueInfo in cueInfos)
			{
				if ((string.IsNullOrEmpty(cueInfo.name) || cueInfo.name == cueName) && cueInfo.length > 0)
				{
					musicLength = cueInfo.length < 6000 ? 120000L : cueInfo.length;
					return;
				}
			}
		}

		private static string GetLiveMusicBundleName(string cueName)
		{
			string normalized = cueName.Replace('\\', '/');
			if (normalized.StartsWith("music/long/", StringComparison.OrdinalIgnoreCase))
			{
				return normalized;
			}

			return normalized.Contains("/") ? normalized : "music/long/" + normalized;
		}

		private static Sekai.Live.MVQualityType ConvertQualityType(Sekai.MVQualityType qualityType)
		{
			return qualityType == Sekai.MVQualityType.High ? Sekai.Live.MVQualityType.High : Sekai.Live.MVQualityType.Low;
		}
	}
}
