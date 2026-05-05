using Sekai;
using Sekai.UI;
using System.Collections;
using UnityEngine;

namespace Sekai.Live
{
    public class MusicInfoView : MonoBehaviour
    {
        [SerializeField] private SafeArea.SafeAreaAdjuster safeAreaAdjuster;
        [SerializeField] private CanvasGroup viewGroup;
        [SerializeField] private CanvasGroup[] infoGroup;
        [SerializeField] private Transform mask;
        [SerializeField] private CustomRawImage jacket;
        [SerializeField] private Texture2D jacketTexture;
        [SerializeField] private UIPartsMusicDifficulty difficultyJacket;
        [SerializeField] private CustomTextMesh difficultyJacketText;
        [SerializeField] private CustomTextMesh titleLabel;
        [SerializeField] private CustomTextMesh creatorLabel;
        [SerializeField] private CustomTextMesh singerLabel;
        [SerializeField] private UIPartsMusicCollaborationRibbon collaborationRibbon;
        public float JacketFadeOutDelaySec = 5.5f;

        private Coroutine playCoroutine;

        public float AnimationDurationSec
        {
            get { return 6f; }
        }

        public void Setup(LiveBootDataBase bootData)
        {
            if (viewGroup != null) viewGroup.alpha = 0f;
            if (infoGroup != null)
            {
                for (int i = 0; i < infoGroup.Length; i++)
                {
                    if (infoGroup[i] != null) infoGroup[i].alpha = 0f;
                }
            }

            LiveMusicData musicData = bootData != null ? bootData.MusicData : null;
            MasterMusic music = musicData != null ? musicData.Music : null;
            MasterMusicDifficulty difficulty = musicData != null ? musicData.Difficulty : null;

            if (jacket != null && jacketTexture != null)
            {
                jacket.texture = jacketTexture;
            }

            if (difficultyJacket != null)
            {
                difficultyJacket.Setup(difficulty != null ? difficulty.MusicDifficulty : MusicDifficulty.Easy);
            }

            SetupDifficulty(difficulty != null ? difficulty.musicDifficulty : "easy");

            if (titleLabel != null)
            {
                titleLabel.text = music != null ? music.Title : string.Empty;
            }

            if (creatorLabel != null)
            {
                string lyricist = music != null ? music.Lyricist : string.Empty;
                string composer = music != null ? music.Composer : string.Empty;
                string arranger = music != null ? music.Arranger : string.Empty;
                creatorLabel.text = FormatCreatorText(lyricist, composer, arranger);
            }

            if (singerLabel != null)
            {
                singerLabel.text = FormatSingerText(musicData != null ? musicData.Vocal : null);
            }

            if (collaborationRibbon != null)
            {
                bool isCollaboration = musicData != null && musicData.IsCollaboration;
                collaborationRibbon.gameObject.SetActive(isCollaboration);
                if (isCollaboration)
                {
                    collaborationRibbon.SetText(musicData.Collaboration.label);
                }
            }
        }

        public void Play(float durationScale = 1f, bool disableOnlyJacketOnFinish = false, float bgFadeoutDuration = 0f)
        {
            if (safeAreaAdjuster != null)
            {
                safeAreaAdjuster.Setup();
                safeAreaAdjuster.Apply();
            }

            gameObject.SetActive(true);
            if (playCoroutine != null)
            {
                StopAllCoroutines();
                playCoroutine = null;
            }

            playCoroutine = StartCoroutine(PlayCoroutine(durationScale, disableOnlyJacketOnFinish, bgFadeoutDuration));
        }

        public void FadeViewGroup(float alpha)
        {
            if (viewGroup != null) viewGroup.alpha = alpha;
        }

        public void PlayBackgroundAnimation(float delayTimeSec, float duration)
        {
            if (mask == null) return;
            StartCoroutine(PlayBackgroundAnimationCoroutine(delayTimeSec, duration));
        }

        private void SetupDifficulty(string difficulty)
        {
            if (difficultyJacket != null)
            {
                difficultyJacket.Show();
            }
            if (difficultyJacketText != null)
            {
                difficultyJacketText.gameObject.SetActive(true);
                difficultyJacketText.SetText(string.IsNullOrEmpty(difficulty) ? string.Empty : difficulty.ToUpper(), false);
            }
        }

        private IEnumerator PlayCoroutine(float durationScale, bool disableOnlyJacketOnFinish, float bgFadeoutDuration)
        {
            yield return new WaitForSecondsRealtime(0.01f);

            if (difficultyJacket != null && jacket != null)
            {
                difficultyJacket.transform.position = jacket.transform.position;
            }

            if (viewGroup != null)
            {
                viewGroup.alpha = 0f;
            }
            SetInfoGroupsAlpha(0f);

            if (viewGroup != null)
            {
                StartCoroutine(FadeCanvasGroup(viewGroup, 1f, durationScale * 0.5f));
            }

            StartCoroutine(DelayedAction(0.2f, () =>
            {
                if (infoGroup == null)
                {
                    return;
                }

                for (int i = 0; i < infoGroup.Length; i++)
                {
                    if (infoGroup[i] != null)
                    {
                        StartCoroutine(FadeCanvasGroup(infoGroup[i], 1f, durationScale * 0.5f));
                    }
                }
            }));

            StartCoroutine(DelayedAction(0.8f, () =>
            {
                if (difficultyJacket == null)
                {
                    return;
                }

                Transform target = difficultyJacket.transform;
                Vector3 position = new Vector3(target.localPosition.x - 44f, target.localPosition.y - 44f, 0f);
                StartCoroutine(MoveLocal(target, position, 2f));
            }));

            PlayBackgroundAnimation(durationScale * 1.5f, durationScale * 2.5f);
            PlayBackgroundAnimation(durationScale * 3.5f, durationScale * 2.5f);

            float fadeOutDelay = JacketFadeOutDelaySec * durationScale + bgFadeoutDuration;
            yield return new WaitForSecondsRealtime(Mathf.Max(0f, fadeOutDelay));

            if (disableOnlyJacketOnFinish)
            {
                if (infoGroup != null)
                {
                    for (int i = 0; i < infoGroup.Length; i++)
                    {
                        if (infoGroup[i] != null)
                        {
                            StartCoroutine(FadeCanvasGroup(infoGroup[i], 0f, durationScale * 0.5f));
                        }
                    }
                }
            }
            else if (viewGroup != null)
            {
                yield return FadeCanvasGroup(viewGroup, 0f, durationScale * 0.5f);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }

            playCoroutine = null;
        }

        private IEnumerator DelayedAction(float delay, System.Action action)
        {
            yield return new WaitForSecondsRealtime(Mathf.Max(0f, delay));
            action?.Invoke();
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

        private IEnumerator MoveLocal(Transform target, Vector3 targetPosition, float duration)
        {
            if (target == null)
            {
                yield break;
            }

            Vector3 startPosition = target.localPosition;
            if (duration <= 0f)
            {
                target.localPosition = targetPosition;
                yield break;
            }

            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                t = 0.5f - Mathf.Cos(t * Mathf.PI) * 0.5f;
                target.localPosition = Vector3.LerpUnclamped(startPosition, targetPosition, t);
                yield return null;
            }

            target.localPosition = targetPosition;
        }

        private IEnumerator PlayBackgroundAnimationCoroutine(float delayTimeSec, float duration)
        {
            yield return new WaitForSecondsRealtime(Mathf.Max(0f, delayTimeSec));

            if (mask == null)
            {
                yield break;
            }

            Vector3 start = mask.localPosition;
            start.y = -810f;
            mask.localPosition = start;

            Vector3 end = start;
            end.y = 810f;
            yield return MoveLocal(mask, end, duration);
        }

        private void SetInfoGroupsAlpha(float alpha)
        {
            if (infoGroup == null)
            {
                return;
            }

            for (int i = 0; i < infoGroup.Length; i++)
            {
                if (infoGroup[i] != null)
                {
                    infoGroup[i].alpha = alpha;
                }
            }
        }

        private static string FormatCreatorText(string lyricist, string composer, string arranger)
        {
            string creator = string.Empty;
            if (!string.IsNullOrEmpty(lyricist))
            {
                creator = lyricist;
            }
            if (!string.IsNullOrEmpty(composer) && composer != lyricist)
            {
                creator = string.IsNullOrEmpty(creator) ? composer : creator + " / " + composer;
            }
            if (!string.IsNullOrEmpty(arranger) && arranger != lyricist && arranger != composer)
            {
                creator = string.IsNullOrEmpty(creator) ? arranger : creator + " / " + arranger;
            }
            return creator;
        }

        private static string FormatSingerText(MasterMusicVocal vocal)
        {
            if (vocal == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(vocal.caption))
            {
                return vocal.caption;
            }

            return string.IsNullOrEmpty(vocal.musicVocalType) ? string.Empty : vocal.musicVocalType;
        }
    }
}
