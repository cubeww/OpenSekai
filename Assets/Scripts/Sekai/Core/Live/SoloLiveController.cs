using System.Collections;
using Sekai;
using Sekai.Live;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sekai.Core.Live
{
    public class SoloLiveController : BaseLiveController
    {
        private const float BaseGameStartWaitTime = 6f;
        private const float TimingAdjustFrameTime = 0.016667f;

        [SerializeField] private LiveViewBase[] liveViews;
        [SerializeField] private int renderTextureWidth = 1920;
        [SerializeField] private int renderTextureHeight = 1080;
        [SerializeField] private int previewMusicId = 1;
        [SerializeField] private MusicCategory musicCategory = MusicCategory.image;
        [SerializeField] private LivePlayMode livePlayMode = LivePlayMode.Free;
        [SerializeField] private int previewVocalId = 1;
        [SerializeField] private MusicDifficulty previewDifficulty = MusicDifficulty.Easy;
        [SerializeField] private int previewPlayLevel = 1;
        [SerializeField] private int previewTotalNoteCount;
        [SerializeField] private float previewTimingAdjustData;
        [SerializeField] private float previewFillerSec = 9f;
        [SerializeField] private string previewTitle = "Tell Your World";
        [SerializeField] private string previewLyricist = "kz";
        [SerializeField] private string previewComposer = "kz";
        [SerializeField] private string previewArranger = "kz";
        [SerializeField] private string previewVocalCaption = "Miku";
        [SerializeField] private string previewVocalType = "sekai";
        [SerializeField] private string previewVocalAssetBundleName;
        [SerializeField] private AudioClip previewMusicClip;
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private bool isAuto;
        [SerializeField] private bool canSkipDisplayMusicInfo;
        [SerializeField] private bool isCollaboration;
        [SerializeField] private int collaborationId;
        [SerializeField] private string collaborationLabel;
        [SerializeField] private bool startOnAwake = true;
        [SerializeField] private bool musicStartOnAwake = true;
        [SerializeField] private float liveStartWaitTime = BaseGameStartWaitTime;
        [SerializeField] private TextAsset previewSusScore;
        [SerializeField] private LiveBundleBuildData liveBundleBuildData;

        private Coroutine liveStartCoroutine;
        private Coroutine resumeCoroutine;
        private Coroutine resultCoroutine;
        private LiveLogic liveLogic;
        private LiveResult result;
        private bool isRhythmGameRunning;
        private bool isPaused;
        private float currentMusicTime;
        private float adjustedMusicTime;
        private AudioSourceSyncedUnityTimer musicTimer;

        protected virtual void Awake()
        {
            NativeInput.Enable();

            if (startOnAwake)
            {
                Setup();
            }
        }

        public virtual void Setup()
        {
            EnsureBootData();
            ApplyLiveFrameRateSettings();
            EnsureBackgroundTexture();
            SetupLiveViews();

            if (!musicStartOnAwake)
            {
                return;
            }

            StartLive();
        }

        public void StartLive()
        {
            if (liveStartCoroutine != null)
            {
                StopCoroutine(liveStartCoroutine);
            }

            liveStartCoroutine = StartCoroutine(LiveStart(liveStartWaitTime));
        }

        protected virtual void EnsureBootData()
        {
            if (BootData != null)
            {
                if (BootData.LiveSettingData == null)
                {
                    BootData.LiveSettingData = new LiveSettingData();
                }
                if (BootData.BundleBuildData == null)
                {
                    BootData.BundleBuildData = liveBundleBuildData;
                }

                return;
            }

            SetupBootDataForPreview(CreatePreviewBootData());
        }

        private LiveBootDataBase CreatePreviewBootData()
        {
            string difficulty = GetDifficultyName(previewDifficulty);
            FreeLiveBootData bootData = new FreeLiveBootData(
                previewMusicId,
                difficulty,
                previewVocalId,
                0,
                livePlayMode,
                musicCategory);

            bootData.IsAuto = isAuto;
            bootData.canSkipDisplayMusicInfo = canSkipDisplayMusicInfo;
            bootData.LiveSettingData = new LiveSettingData
            {
                TimingAdjustData = previewTimingAdjustData
            };
            bootData.BundleBuildData = liveBundleBuildData;

            LiveMusicData musicData = bootData.MusicData ?? new LiveMusicData();
            musicData.Music = new MasterMusic
            {
                id = previewMusicId,
                title = previewTitle,
                lyricist = previewLyricist,
                composer = previewComposer,
                arranger = previewArranger,
                fillerSec = previewFillerSec
            };
            musicData.Difficulty = new MasterMusicDifficulty
            {
                id = previewMusicId * 10 + (int)previewDifficulty,
                musicId = previewMusicId,
                musicDifficulty = difficulty,
                playLevel = previewPlayLevel,
                totalNoteCount = previewTotalNoteCount
            };
            if (previewTotalNoteCount > 0)
            {
                musicData.Score = CreatePreviewScore(previewPlayLevel, previewTotalNoteCount);
            }
            musicData.Vocal = new MasterMusicVocal
            {
                id = previewVocalId,
                musicId = previewMusicId,
                musicVocalType = previewVocalType,
                seq = 1,
                caption = previewVocalCaption,
                assetbundleName = previewVocalAssetBundleName
            };
            musicData.Collaboration = isCollaboration
                ? new MasterMusicCollaboration
                {
                    id = collaborationId,
                    label = collaborationLabel
                }
                : null;

            bootData.MusicData = musicData;
            return bootData;
        }

        private static MasterPlayLevelScore CreatePreviewScore(int playLevel, int totalNoteCount)
        {
            int noteCount = Mathf.Max(1, totalNoteCount);
            int targetScore = noteCount * 1000;
            return new MasterPlayLevelScore
            {
                liveType = "free",
                playLevel = playLevel,
                s = Mathf.FloorToInt(targetScore * ScoreGaugeCalculator.RankRateS),
                a = Mathf.FloorToInt(targetScore * ScoreGaugeCalculator.RankRateA),
                b = Mathf.FloorToInt(targetScore * ScoreGaugeCalculator.RankRateB),
                c = Mathf.FloorToInt(targetScore * ScoreGaugeCalculator.RankRateC)
            };
        }

        private static string GetDifficultyName(MusicDifficulty difficulty)
        {
            if (difficulty == MusicDifficulty.None)
            {
                return "easy";
            }

            return difficulty.ToString().ToLowerInvariant();
        }

        protected virtual void OnMusicStart()
        {
            PlayMusic();

            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].MusicStart(GetAdjustedMusicTime(currentMusicTime));
                }
            }
        }

        protected virtual void OnRhythmGameStart()
        {
            SetupLiveLogic();
            liveLogic?.RefreshInput();
            result = LiveResult.None;
            isRhythmGameRunning = true;
            isPaused = false;

            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].RhythmGameStart();
                }
            }
        }

        protected virtual void Update()
        {
            base.OnUpdate();
            CheckBackKey();

            if (!isRhythmGameRunning || isPaused)
            {
                return;
            }

            currentMusicTime = GetCurrentMusicTime();
            adjustedMusicTime = GetAdjustedMusicTime(currentMusicTime);
            float scoreInfoTime = adjustedMusicTime - GetMusicFillerSec();
            liveLogic?.OnUpdate(scoreInfoTime, Time.realtimeSinceStartupAsDouble);
            if (liveLogic != null && liveLogic.Result == LiveResult.Failure && liveLogic.IsNotesAllFinished)
            {
                OnFinished();
            }

            if (BootData != null && BootData.IsAuto)
            {
                liveLogic?.OnAutoInput();
            }
            else
            {
                liveLogic?.OnInput();
            }

            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                liveViews[i]?.OnUpdate(adjustedMusicTime);
            }
        }

        protected override void OnDestroy()
        {
            if (liveViews != null)
            {
                for (int i = 0; i < liveViews.Length; i++)
                {
                    if (liveViews[i] != null)
                    {
                        liveViews[i].OnUnload();
                    }
                }
            }

            isRhythmGameRunning = false;
            if (resumeCoroutine != null)
            {
                StopCoroutine(resumeCoroutine);
                resumeCoroutine = null;
            }
            StopMusic();
            UnsubscribeLiveLogic();
            if (resultCoroutine != null)
            {
                StopCoroutine(resultCoroutine);
                resultCoroutine = null;
            }

            if (BackgroundTexture != null)
            {
                BackgroundTexture.Release();
                BackgroundTexture = null;
            }

            NativeInput.Disable();
            base.OnDestroy();
        }

        public override void Pause()
        {
            if (!isRhythmGameRunning || isPaused || result != LiveResult.None)
            {
                return;
            }

            if (resumeCoroutine != null)
            {
                StopCoroutine(resumeCoroutine);
                resumeCoroutine = null;
            }
            isPaused = true;
            currentMusicTime = GetCurrentMusicTime();
            adjustedMusicTime = GetAdjustedMusicTime(currentMusicTime);
            if (musicAudioSource != null)
            {
                musicAudioSource.Pause();
            }
            NotifyPause(adjustedMusicTime);
        }

        public override void Resume()
        {
            if (!isRhythmGameRunning || !isPaused || result != LiveResult.None)
            {
                return;
            }

            NotifyCountdown();
            if (resumeCoroutine != null)
            {
                StopCoroutine(resumeCoroutine);
            }
            resumeCoroutine = StartCoroutine(ResumeCoroutine());
        }

        public void ResumeNoCountDown()
        {
            if (!isRhythmGameRunning || !isPaused || result != LiveResult.None)
            {
                return;
            }

            if (resumeCoroutine != null)
            {
                StopCoroutine(resumeCoroutine);
                resumeCoroutine = null;
            }
            ResumeLiveNow();
        }

        public override void Continue(float time)
        {
            if (liveLogic == null)
            {
                return;
            }

            result = LiveResult.None;
            isPaused = false;
            isRhythmGameRunning = true;
            liveLogic.Continue(time);
            currentMusicTime = time;
            adjustedMusicTime = GetAdjustedMusicTime(currentMusicTime);
            musicTimer?.Reset(Mathf.FloorToInt(time * 1000f), Time.time);
            NotifyResume(adjustedMusicTime);
        }

        private IEnumerator ResumeCoroutine()
        {
            liveLogic?.RefreshInput();
            yield return new WaitForSeconds(3f);
            ResumeLiveNow();
            resumeCoroutine = null;
        }

        private void ResumeLiveNow()
        {
            if (!isRhythmGameRunning || result != LiveResult.None)
            {
                return;
            }

            isPaused = false;
            if (musicAudioSource != null && musicAudioSource.clip != null)
            {
                musicAudioSource.UnPause();
            }

            musicTimer?.Reset(Mathf.FloorToInt(currentMusicTime * 1000f), Time.time);
            adjustedMusicTime = GetAdjustedMusicTime(currentMusicTime);
            liveLogic?.RefreshInput();
            NotifyResume(adjustedMusicTime);
        }

        private void SetupLiveLogic()
        {
            string scoreText = previewSusScore != null ? previewSusScore.text : null;
            MusicScore musicScore = MusicScoreFactory.Create(scoreText, liveBundleBuildData);
            UnsubscribeLiveLogic();
            liveLogic = new LiveLogic(liveBundleBuildData);
            liveLogic.Setup(BootData, liveViews, musicScore, BaseCamera);
            liveLogic.OnFinished += OnFinished;
            liveLogic.OnFailure += OnFailure;
        }

        private void SetupLiveViews()
        {
            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].Setup(this);
                }
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].OnLoad();
                }
            }
        }

        private IEnumerator LiveStart(float waitTime)
        {
            yield return StartCoroutine(MusicReady());
            OnMusicStart();

            if (waitTime > 0f)
            {
                yield return new WaitForSeconds(waitTime);
            }

            OnRhythmGameStart();
            liveStartCoroutine = null;
        }

        private void OnFailure()
        {
            currentMusicTime = GetCurrentMusicTime();
            adjustedMusicTime = GetAdjustedMusicTime(currentMusicTime);
        }

        protected virtual void OnFinished()
        {
            float musicLength = previewMusicClip != null ? previewMusicClip.length : currentMusicTime;
            float waitTime = Mathf.Max(musicLength - currentMusicTime - 1f, 4f);
            PreExit(1f, waitTime);
        }

        protected virtual void PreExit(float delay = 0f, float waitTime = 4f)
        {
            if (result != LiveResult.None)
            {
                return;
            }

            isRhythmGameRunning = false;
            isPaused = false;
            if (resumeCoroutine != null)
            {
                StopCoroutine(resumeCoroutine);
                resumeCoroutine = null;
            }
            result = liveLogic != null && liveLogic.Result == LiveResult.Failure ? LiveResult.Clear : liveLogic != null ? liveLogic.Result : LiveResult.None;
            UnsubscribeLiveLogic();

            if (resultCoroutine != null)
            {
                StopCoroutine(resultCoroutine);
            }
            resultCoroutine = StartCoroutine(PreExitCoroutine(delay, waitTime));
        }

        private IEnumerator PreExitCoroutine(float delay, float waitTime)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }
            yield return StartCoroutine(ResultAnim(waitTime));
            resultCoroutine = null;
        }

        private IEnumerator ResultAnim(float waitTime = 4f)
        {
            liveViews.Result(GetLiveResultAnimationType());
            yield return new WaitForSeconds(waitTime);
            liveViews.Finish();
            yield return new WaitForSeconds(2f);
            Exit();
        }

        protected virtual void Exit()
        {
        }

        private LiveResultAnimationType GetLiveResultAnimationType()
        {
            if (result < LiveResult.Failure)
            {
                return LiveResultAnimationType.None;
            }
            if (result == LiveResult.Failure)
            {
                return LiveResultAnimationType.Failure;
            }
            if (result == LiveResult.Clear)
            {
                if (liveLogic != null && liveLogic.IsAllPerfectCombo)
                {
                    return LiveResultAnimationType.AllPerfect;
                }
                if (liveLogic != null && liveLogic.IsPerfectCombo)
                {
                    return LiveResultAnimationType.FullCombo;
                }

                LiveScore score = liveLogic != null ? liveLogic.Score : default(LiveScore);
                return score.life != 0 ? LiveResultAnimationType.Clear : LiveResultAnimationType.LifeZero;
            }

            Debug.LogErrorFormat(this, "Unknown LiveResult {0}", result);
            return LiveResultAnimationType.None;
        }

        private void UnsubscribeLiveLogic()
        {
            if (liveLogic == null)
            {
                return;
            }

            liveLogic.OnFinished -= OnFinished;
            liveLogic.OnFailure -= OnFailure;
        }

        private void EnsureBackgroundTexture()
        {
            if (BackgroundTexture != null)
            {
                return;
            }

            BackgroundTexture = new RenderTexture(renderTextureWidth, renderTextureHeight, 24)
            {
                name = "LiveBackgroundTexture"
            };
            BackgroundTexture.Create();
        }

        private IEnumerator MusicReady()
        {
            if (previewMusicClip == null || previewMusicClip.loadState == AudioDataLoadState.Loaded)
            {
                yield break;
            }

            if (previewMusicClip.loadState == AudioDataLoadState.Unloaded)
            {
                previewMusicClip.LoadAudioData();
            }

            while (previewMusicClip.loadState == AudioDataLoadState.Loading)
            {
                yield return null;
            }
        }

        private void PlayMusic()
        {
            currentMusicTime = 0f;
            adjustedMusicTime = GetAdjustedMusicTime(currentMusicTime);

            if (previewMusicClip == null)
            {
                musicTimer = null;
                return;
            }

            if (musicAudioSource == null)
            {
                musicAudioSource = GetComponent<AudioSource>();
                if (musicAudioSource == null)
                {
                    musicAudioSource = gameObject.AddComponent<AudioSource>();
                }
            }

            musicAudioSource.clip = previewMusicClip;
            musicAudioSource.loop = false;
            musicAudioSource.playOnAwake = false;
            musicAudioSource.spatialBlend = 0f;
            musicAudioSource.time = 0f;
            musicAudioSource.Play();
            musicTimer = new AudioSourceSyncedUnityTimer(musicAudioSource);
            musicTimer.Reset(0, Time.time);
        }

        private void StopMusic()
        {
            if (musicAudioSource != null)
            {
                musicAudioSource.Stop();
            }

            musicTimer = null;
        }

        private void NotifyPause(float time)
        {
            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                liveViews[i]?.Pause(time);
            }
        }

        private void NotifyCountdown()
        {
            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                liveViews[i]?.Countdown();
            }
        }

        private void NotifyResume(float time)
        {
            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                liveViews[i]?.Resume(time);
            }
        }

        private float GetCurrentMusicTime()
        {
            if (musicTimer != null)
            {
                musicTimer.Execute(Time.time);
                return Mathf.Max((float)musicTimer.PlaybackTime / 1000f, 0f);
            }

            return currentMusicTime;
        }

        private float GetMusicFillerSec()
        {
            return BootData != null &&
                   BootData.MusicData != null &&
                   BootData.MusicData.Music != null
                ? BootData.MusicData.Music.fillerSec
                : 0f;
        }

        private float GetAdjustedMusicTime(float time)
        {
            LiveSettingData settings = Settings;
            float timingAdjust = settings != null ? settings.TimingAdjustData * TimingAdjustFrameTime : 0f;
            return Mathf.Max(time + timingAdjust, 0f);
        }

        private void CheckBackKey()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null || !keyboard.escapeKey.wasPressedThisFrame)
            {
                return;
            }

            if (isPaused)
            {
                Resume();
            }
            else if (isRhythmGameRunning)
            {
                Pause();
            }
        }

    }
}
