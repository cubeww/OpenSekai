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
        [SerializeField] private UIPartsMusicDifficulty difficultyJacket;
        [SerializeField] private CustomTextMesh difficultyJacketText;
        [SerializeField] private CustomTextMesh titleLabel;
        [SerializeField] private CustomTextMesh creatorLabel;
        [SerializeField] private CustomTextMesh singerLabel;
        [SerializeField] private UIPartsMusicCollaborationRibbon collaborationRibbon;
        public float JacketFadeOutDelaySec = 5.5f;

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

            if (bootData?.MusicData?.Music != null && titleLabel != null)
            {
                titleLabel.text = $"music_{bootData.MusicData.Music.id:0000}";
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
            if (viewGroup != null) viewGroup.alpha = 1f;
            if (infoGroup != null)
            {
                for (int i = 0; i < infoGroup.Length; i++)
                {
                    if (infoGroup[i] != null) infoGroup[i].alpha = 1f;
                }
            }
        }

        public void FadeViewGroup(float alpha)
        {
            if (viewGroup != null) viewGroup.alpha = alpha;
        }

        public void PlayBackgroundAnimation(float delayTimeSec, float duration)
        {
            if (mask == null) return;
            Vector3 position = mask.localPosition;
            position.y = 810f;
            mask.localPosition = position;
        }
    }
}
