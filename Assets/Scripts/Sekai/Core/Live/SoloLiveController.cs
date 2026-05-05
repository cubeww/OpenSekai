using System.Collections;
using Sekai;
using UnityEngine;

namespace Sekai.Core.Live
{
    public class SoloLiveController : BaseLiveController
    {
        private const float BaseGameStartWaitTime = 6f;

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
        [SerializeField] private float previewFillerSec;
        [SerializeField] private string previewTitle = "Tell Your World";
        [SerializeField] private string previewLyricist = "kz";
        [SerializeField] private string previewComposer = "kz";
        [SerializeField] private string previewArranger = "kz";
        [SerializeField] private string previewVocalCaption = "Miku";
        [SerializeField] private string previewVocalType = "sekai";
        [SerializeField] private string previewVocalAssetBundleName;
        [SerializeField] private bool isAuto;
        [SerializeField] private bool canSkipDisplayMusicInfo;
        [SerializeField] private bool isCollaboration;
        [SerializeField] private int collaborationId;
        [SerializeField] private string collaborationLabel;
        [SerializeField] private bool startOnAwake = true;
        [SerializeField] private bool musicStartOnAwake = true;
        [SerializeField] private float liveStartWaitTime = BaseGameStartWaitTime;

        private Coroutine liveStartCoroutine;

        protected virtual void Awake()
        {
            if (startOnAwake)
            {
                Setup();
            }
        }

        public virtual void Setup()
        {
            EnsureBootData();
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
            bootData.LiveSettingData = new LiveSettingData();

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
            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].MusicStart(0f);
                }
            }
        }

        protected virtual void OnRhythmGameStart()
        {
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

        protected virtual void OnDestroy()
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

            if (BackgroundTexture != null)
            {
                BackgroundTexture.Release();
                BackgroundTexture = null;
            }
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
            OnMusicStart();

            if (waitTime > 0f)
            {
                yield return new WaitForSeconds(waitTime);
            }

            OnRhythmGameStart();
            liveStartCoroutine = null;
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
    }
}
