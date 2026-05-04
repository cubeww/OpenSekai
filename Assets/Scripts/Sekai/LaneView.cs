using UnityEngine;

namespace Sekai
{
    public class LaneView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer defaultLaneBase;
        [SerializeField] private SpriteRenderer defaultLaneLine;
        [SerializeField] private SpriteRenderer defaultJudgeLine;

        public void Setup(LiveSettingData liveSetting)
        {
            float alpha = liveSetting != null ? liveSetting.LaneTransparent : 1f;
            Color color = Color.white;
            color.a = alpha;
            if (defaultLaneBase != null)
            {
                defaultLaneBase.color = color;
                defaultLaneBase.enabled = alpha > 0f;
            }
            if (defaultLaneLine != null) defaultLaneLine.enabled = true;
            if (defaultJudgeLine != null) defaultJudgeLine.enabled = true;
        }
    }
}
