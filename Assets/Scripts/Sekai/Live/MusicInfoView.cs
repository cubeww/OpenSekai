using DG.Tweening;
using Sekai;
using Sekai.UI;
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

        private Tween playTween;

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

            Texture2D displayJacketTexture = musicData != null && musicData.JacketTexture != null
                ? musicData.JacketTexture
                : jacketTexture;
            if (jacket != null && displayJacketTexture != null)
            {
                jacket.texture = displayJacketTexture;
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

            if (difficultyJacket != null && jacket != null)
            {
                difficultyJacket.transform.position = jacket.transform.position;
            }

            KillTweens();
            playTween = DOVirtual.DelayedCall(0.01f, () =>
            {
                playTween = null;
                if (viewGroup != null)
                {
                    viewGroup.DOFade(0f, 0f);
                }

                gameObject.SetActive(true);

                if (viewGroup != null)
                {
                    viewGroup.DOFade(1f, durationScale * 0.5f);
                }

                DOVirtual.DelayedCall(0.2f, () =>
                {
                    if (infoGroup == null)
                    {
                        return;
                    }

                    for (int i = 0; i < infoGroup.Length; i++)
                    {
                        if (infoGroup[i] != null)
                        {
                            infoGroup[i].DOFade(1f, durationScale * 0.5f);
                        }
                    }
                }, true).SetTarget(this);

                DOVirtual.DelayedCall(0.8f, () =>
                {
                    if (difficultyJacket == null)
                    {
                        return;
                    }

                    Transform target = difficultyJacket.transform;
                    Vector3 position = new Vector3(target.localPosition.x - 44f, target.localPosition.y - 44f, 0f);
                    target.DOLocalMove(position, 2f, false).SetEase((Ease)3);
                }, true).SetTarget(this);

                PlayBackgroundAnimation(durationScale * 1.5f, durationScale * 2.5f);
                PlayBackgroundAnimation(durationScale * 3.5f, durationScale * 2.5f);

                DOVirtual.DelayedCall(JacketFadeOutDelaySec * durationScale + bgFadeoutDuration, () =>
                {
                    if (disableOnlyJacketOnFinish)
                    {
                        if (infoGroup == null)
                        {
                            return;
                        }

                        for (int i = 0; i < infoGroup.Length; i++)
                        {
                            if (infoGroup[i] != null)
                            {
                                infoGroup[i].DOFade(0f, durationScale * 0.5f);
                            }
                        }
                    }
                    else if (viewGroup != null)
                    {
                        viewGroup.DOFade(0f, durationScale * 0.5f)
                            .OnComplete(() => gameObject.SetActive(false));
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }, true).SetTarget(this);
            }, true).SetTarget(this);
        }

        public void FadeViewGroup(float alpha)
        {
            if (viewGroup != null) viewGroup.alpha = alpha;
        }

        public void PlayBackgroundAnimation(float delayTimeSec, float duration)
        {
            if (mask == null) return;
            DOVirtual.DelayedCall(delayTimeSec, () =>
            {
                if (mask == null)
                {
                    return;
                }

                mask.DOLocalMoveY(-810f, 0f, false);
                mask.DOLocalMoveY(810f, duration, false);
            }, true).SetTarget(this);
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

        private void KillTweens()
        {
            if (playTween != null)
            {
                playTween.Kill();
                playTween = null;
            }

            DOTween.Kill(this);
            if (viewGroup != null) viewGroup.DOKill();
            if (infoGroup != null)
            {
                for (int i = 0; i < infoGroup.Length; i++)
                {
                    if (infoGroup[i] != null) infoGroup[i].DOKill();
                }
            }
            if (difficultyJacket != null) difficultyJacket.transform.DOKill();
            if (mask != null) mask.DOKill();
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

        private void OnDestroy()
        {
            KillTweens();
        }
    }
}
