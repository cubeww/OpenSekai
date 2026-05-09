using System.Collections;
using System.Collections.Generic;
using Sekai;
using Sekai.Live;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

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
        [SerializeField] private Texture2D previewJacket;
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
        [SerializeField] private LivePauseDialog livePauseDialogPrefab;
        [SerializeField] private Common2ButtonDialog common2ButtonDialogPrefab;
        [SerializeField] private ConsecutiveAutoLivePauseDialog consecutiveAutoLivePauseDialogPrefab;
        [SerializeField] private Transform dialogRoot;

        private Coroutine liveStartCoroutine;
        private Coroutine resumeCoroutine;
        private Coroutine resultCoroutine;
        private LiveLogic liveLogic;
        private LiveResult result;
        private bool isRhythmGameRunning;
        private bool isPaused;
        private DialogBase activePauseDialog;
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
            musicData.JacketTexture = previewJacket;

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
            ShowPauseDialog();
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
            DestroyActivePauseDialog();
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
            DestroyActivePauseDialog();
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
            if (result != LiveResult.None && result != LiveResult.Retire)
            {
                return;
            }

            LiveResult requestedResult = result;
            isRhythmGameRunning = false;
            isPaused = false;
            if (resumeCoroutine != null)
            {
                StopCoroutine(resumeCoroutine);
                resumeCoroutine = null;
            }
            DestroyActivePauseDialog();
            result = requestedResult != LiveResult.None
                ? requestedResult
                : liveLogic != null && liveLogic.Result == LiveResult.Failure ? LiveResult.Clear : liveLogic != null ? liveLogic.Result : LiveResult.None;
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

        protected virtual void Retry()
        {
            DestroyActivePauseDialog();
            ResumeNoCountDown();

            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                liveViews[i]?.Retry();
            }
        }

        protected virtual void Retire()
        {
            DestroyActivePauseDialog();
            result = LiveResult.Retire;
            PreExit();
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

            if (activePauseDialog != null)
            {
                activePauseDialog.ExecuteBackKeyProcess();
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

        private void ShowPauseDialog()
        {
            DestroyActivePauseDialog();

            if (AutoLiveUtility.IsRunningConsecutiveAutoLive() && consecutiveAutoLivePauseDialogPrefab != null)
            {
                ConsecutiveAutoLivePauseDialog dialog = InstantiateDialog(consecutiveAutoLivePauseDialogPrefab);
                activePauseDialog = dialog;
                dialog.Initialize(
                    string.Empty,
                    "Resume",
                    "Cancel",
                    Resume,
                    ShowConsecutiveAutoLiveRetireDialog,
                    DialogSize.Small,
                    true);
                dialog.Setup();
                return;
            }

            if (livePauseDialogPrefab == null)
            {
                return;
            }

            LivePauseDialog pauseDialog = InstantiateDialog(livePauseDialogPrefab);
            activePauseDialog = pauseDialog;
            Dictionary<string, System.Action> actions = new Dictionary<string, System.Action>
            {
                { "Cancel", OnRetireConfirm },
                { "Retry", OnRetryConfirm },
                { "Resume", Resume }
            };
            pauseDialog.Initialize(actions, DialogSize.Small, false);
        }

        private void OnRetireConfirm()
        {
            ShowConfirmRetireDialog();
        }

        private void OnRetryConfirm()
        {
            ShowConfirmRetryDialog();
        }

        private void OnConfirmCancel()
        {
            ShowPauseDialog();
        }

        private void ShowConfirmRetireDialog()
        {
            if (common2ButtonDialogPrefab == null)
            {
                OnConfirmCancel();
                return;
            }

            Common2ButtonDialog dialog = InstantiateDialog(common2ButtonDialogPrefab);
            activePauseDialog = dialog;
            dialog.Initialize(
                "MSG_PAUSE_LIVE_ABORT",
                "WORD_ABORT",
                "WORD_CANCEL",
                Retire,
                OnConfirmCancel,
                DialogSize.Small,
                true);
        }

        private void ShowConfirmRetryDialog()
        {
            if (common2ButtonDialogPrefab == null)
            {
                OnConfirmCancel();
                return;
            }

            Common2ButtonDialog dialog = InstantiateDialog(common2ButtonDialogPrefab);
            activePauseDialog = dialog;
            dialog.Initialize(
                "MSG_PAUSE_LIVE_RETRY",
                "WORD_RETRY",
                "WORD_CANCEL",
                Retry,
                OnConfirmCancel,
                DialogSize.Small,
                true);
        }

        private void ShowConsecutiveAutoLiveRetireDialog()
        {
            if (common2ButtonDialogPrefab == null)
            {
                OnConfirmCancel();
                return;
            }

            Common2ButtonDialog dialog = InstantiateDialog(common2ButtonDialogPrefab);
            activePauseDialog = dialog;
            dialog.Initialize(
                "MSG_PAUSE_LIVE_ABORT",
                "WORD_ABORT",
                "WORD_CANCEL",
                Retire,
                Resume,
                DialogSize.Small,
                true);
        }

        private T InstantiateDialog<T>(T prefab) where T : DialogBase
        {
            Transform parent = EnsureDialogRoot();
            T dialog = Instantiate(prefab, parent, false);
            RectTransform rectTransform = dialog.transform as RectTransform;
            if (rectTransform != null)
            {
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
                rectTransform.localScale = Vector3.one;
            }

            return dialog;
        }

        private Transform EnsureDialogRoot()
        {
            if (dialogRoot != null)
            {
                return dialogRoot;
            }

            Canvas canvas = FindAnyObjectByType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObject = new GameObject("DialogCanvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                canvas = canvasObject.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920f, 1080f);
                scaler.matchWidthOrHeight = 0.5f;
            }

            if (FindAnyObjectByType<EventSystem>() == null)
            {
                new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            }

            dialogRoot = canvas.transform;
            return dialogRoot;
        }

        private void DestroyActivePauseDialog()
        {
            if (activePauseDialog == null)
            {
                return;
            }

            DialogBase dialog = activePauseDialog;
            activePauseDialog = null;

            if (dialog != null)
            {
                Destroy(dialog.gameObject);
            }
        }

    }
}
