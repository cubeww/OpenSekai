using DG.Tweening;
using CP;
using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.Live
{
    public class LiveResultView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private LiveSoundPlayer liveSoundPlayer;
        [SerializeField] private GameObject failedPrefab;
        [SerializeField] private GameObject finishPrefab;
        [SerializeField] private GameObject clearPrefab;
        [SerializeField] private GameObject fullComboPrefab;
        [SerializeField] private GameObject allPerfectPrefab;

        private Tween fadeTween;
        private Tween instantiateTween;
        private GameObject instance;

        public void Execute(LiveResultAnimationType liveResultAnimationType)
        {
            switch (liveResultAnimationType)
            {
                case LiveResultAnimationType.None:
                    break;
                case LiveResultAnimationType.Failure:
                    Fade(failedPrefab);
                    break;
                case LiveResultAnimationType.LifeZero:
                    Fade(finishPrefab);
                    break;
                case LiveResultAnimationType.Clear:
                    liveSoundPlayer?.PlayIngameSEOneShot(LiveSoundDefine.SE_LIVE_CLEAR);
                    Fade(clearPrefab);
                    break;
                case LiveResultAnimationType.FullCombo:
                    liveSoundPlayer?.PlayIngameSEOneShot(LiveSoundDefine.SE_LIVE_FULL_COMBO);
                    Fade(fullComboPrefab);
                    break;
                case LiveResultAnimationType.AllPerfect:
                    liveSoundPlayer?.PlayIngameSEOneShot(LiveSoundDefine.SE_LIVE_ALL_PERFECT);
                    Fade(allPerfectPrefab);
                    break;
                default:
                    Debug.LogErrorFormat(this, "Unknown liveResultAnimationType : {0}", liveResultAnimationType);
                    break;
            }
        }

        public void Unload()
        {
            KillTweens();
            if (instance != null)
            {
                Destroy(instance);
                instance = null;
            }
        }

        private void Fade(GameObject prefab)
        {
            KillTweens();

            if (background != null)
            {
                background.gameObject.SetActive(true);
                fadeTween = background.DOColor(new Color(0f, 0f, 0f, 0.5f), 0.2f)
                    .SetEase((Ease)21)
                    .SetTarget(this);
            }

            instantiateTween = DOVirtual.DelayedCall(0.2f, () =>
            {
                instantiateTween = null;
                if (prefab == null)
                {
                    return;
                }

                if (instance != null)
                {
                    Destroy(instance);
                }

                instance = Instantiate(prefab, transform, false);
                AnimationSEPlayer[] sePlayers = instance.GetComponentsInChildren<AnimationSEPlayer>(true);
                for (int i = 0; i < sePlayers.Length; i++)
                {
                    sePlayers[i].Setup(liveSoundPlayer);
                }

                Renderer[] renderers = instance.GetComponentsInChildren<Renderer>(true);
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].sortingOrder += 220;
                }
            }, true).SetTarget(this);
        }

        private void KillTweens()
        {
            if (fadeTween != null)
            {
                fadeTween.Kill();
                fadeTween = null;
            }

            if (instantiateTween != null)
            {
                instantiateTween.Kill();
                instantiateTween = null;
            }
        }
    }
}
