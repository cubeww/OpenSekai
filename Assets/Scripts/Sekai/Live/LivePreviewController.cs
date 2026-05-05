using Sekai.Core.Live;
using System.Collections;
using UnityEngine;

namespace Sekai.Live
{
    public class LivePreviewController : BaseLiveController
    {
        [SerializeField] private LiveViewBase[] liveViews;
        [SerializeField] private int renderTextureWidth = 1920;
        [SerializeField] private int renderTextureHeight = 1080;
        [SerializeField] private int previewMusicId = 1;
        [SerializeField] private MusicCategory musicCategory = MusicCategory.image;
        [SerializeField] private LivePlayMode livePlayMode = LivePlayMode.Free;
        [SerializeField] private bool startOnAwake = true;
        [SerializeField] private bool musicStartOnAwake = true;
        [SerializeField] private float liveStartWaitTime = 6f;
        [SerializeField] private CanvasGroup openingTransitionCover;
        [SerializeField] private float openingTransitionFadeDuration = 1f;
        [SerializeField] private string previewTitle = "music_0001";
        [SerializeField] private string previewLyricist = "";
        [SerializeField] private string previewComposer = "";
        [SerializeField] private string previewArranger = "";
        [SerializeField] private string previewVocalCaption = "";
        [SerializeField] private MusicDifficulty previewDifficulty = MusicDifficulty.Easy;

        private Coroutine liveStartCoroutine;

        private void Awake()
        {
            if (startOnAwake)
            {
                Setup();
            }
        }

        public void Setup()
        {
            EnsurePreviewBootData();
            EnsureBackgroundTexture();

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

            if (!musicStartOnAwake)
            {
                return;
            }

            if (liveStartCoroutine != null)
            {
                StopCoroutine(liveStartCoroutine);
            }

            liveStartCoroutine = StartCoroutine(LiveStart(liveStartWaitTime));
        }

        private IEnumerator LiveStart(float waitTime)
        {
            if (openingTransitionCover != null)
            {
                openingTransitionCover.gameObject.SetActive(true);
                openingTransitionCover.alpha = 1f;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].MusicStart(0f);
                }
            }

            if (openingTransitionCover != null)
            {
                yield return FadeCanvasGroup(openingTransitionCover, 0f, openingTransitionFadeDuration);
                openingTransitionCover.gameObject.SetActive(false);
            }

            float elapsedTransitionTime = openingTransitionCover != null ? openingTransitionFadeDuration : 0f;
            float remainingWaitTime = Mathf.Max(0f, waitTime - elapsedTransitionTime);
            if (remainingWaitTime > 0f)
            {
                yield return new WaitForSecondsRealtime(remainingWaitTime);
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].RhythmGameStart();
                }
            }

            liveStartCoroutine = null;
        }

        private void EnsurePreviewBootData()
        {
            if (BootData != null)
            {
                return;
            }

            SetupBootDataForPreview(new LiveBootDataBase
            {
                MusicCategory = musicCategory,
                LivePlayMode = livePlayMode,
                MusicData = new LiveMusicData
                {
                    Music = new MasterMusic
                    {
                        id = previewMusicId,
                        title = previewTitle,
                        lyricist = previewLyricist,
                        composer = previewComposer,
                        arranger = previewArranger
                    },
                    Difficulty = new MasterMusicDifficulty
                    {
                        musicId = previewMusicId,
                        musicDifficulty = previewDifficulty.ToString().ToLower()
                    },
                    Vocal = new MasterMusicVocal
                    {
                        musicId = previewMusicId,
                        caption = previewVocalCaption
                    }
                }
            });
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

        private void OnDestroy()
        {
            if (BackgroundTexture != null)
            {
                BackgroundTexture.Release();
                BackgroundTexture = null;
            }
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration)
        {
            if (canvasGroup == null)
            {
                yield break;
            }

            float startAlpha = canvasGroup.alpha;
            if (duration <= 0f)
            {
                canvasGroup.alpha = targetAlpha;
                yield break;
            }

            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, Mathf.Clamp01(elapsed / duration));
                yield return null;
            }

            canvasGroup.alpha = targetAlpha;
        }
    }
}
