using UnityEngine;

namespace Sekai
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer scoreGauge;
        [SerializeField] private SpriteRenderer rankSpriteRenderer;
        [SerializeField] private Sprite[] rankSprites;
        [SerializeField] private SpriteRenderer rankTextSpriteRenderer;
        [SerializeField] private Transform gaugeRankS;
        [SerializeField] private Transform gaugeRankA;
        [SerializeField] private Transform gaugeRankB;
        [SerializeField] private Transform gaugeRankC;
        [SerializeField] private NumberView number;
        [SerializeField] private NumberView outlineNumber;
        [SerializeField] private Transform addScoreRoot;
        [SerializeField] private NumberView addScoreNumber;
        [SerializeField] private NumberView addScoreOutlineNumber;

        public void Setup(LiveMusicData musicData) { }
        public void SetupScore(LiveScore score) { }
    }
}
