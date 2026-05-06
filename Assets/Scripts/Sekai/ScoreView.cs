using Sekai.Core.Live;
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

        private int lastScoreRank;
        private ScoreGaugeCalculator scoreGaugeCalculator;
        private Vector2 scoreGaugeSizeCache = new Vector2(5.648148f, 0.2222222f);
        private Vector3 addScoreBasePosition;
        private float addScoreAnimationTime;
        private bool addScoreAnimating;
        private bool inverseScale;

        public void Setup(LiveMusicData musicData)
        {
            if (scoreGauge != null)
            {
                scoreGaugeSizeCache = scoreGauge.size;
            }

            SetupGaugeRankPosition(gaugeRankS, ScoreGaugeCalculator.RankRateS);
            SetupGaugeRankPosition(gaugeRankA, ScoreGaugeCalculator.RankRateA);
            SetupGaugeRankPosition(gaugeRankB, ScoreGaugeCalculator.RankRateB);
            SetupGaugeRankPosition(gaugeRankC, ScoreGaugeCalculator.RankRateC);

            scoreGaugeCalculator = ScoreGaugeCalculator.Create(musicData != null ? musicData.Score : null);
            if (addScoreRoot != null)
            {
                inverseScale = addScoreRoot.localScale.y < 0f;
                addScoreBasePosition = addScoreRoot.localPosition;
            }

            Clear();
        }

        public void ChangeScoreCalculator(ScoreGaugeCalculator newCalculator)
        {
            scoreGaugeCalculator = newCalculator;
        }

        public void SetupScore(LiveScore score)
        {
            UpdateScore(ref score, 0);
        }

        public void Clear()
        {
            lastScoreRank = -1;
            addScoreAnimating = false;

            if (scoreGauge != null)
            {
                scoreGauge.size = new Vector2(0f, scoreGaugeSizeCache.y);
            }
            if (rankSpriteRenderer != null)
            {
                rankSpriteRenderer.sprite = null;
            }

            number?.Setup("#ffffff", "#CCCCEE");
            outlineNumber?.Setup("#444466", "#444466");
            addScoreNumber?.Setup("#ffffff", "#ffffff00");
            addScoreOutlineNumber?.Setup("#444466", "#ffffff00");

            if (addScoreRoot != null)
            {
                addScoreRoot.localScale = Vector3.zero;
                addScoreRoot.localPosition = addScoreBasePosition;
            }
        }

        public float CalculateGaugeProgress(LiveScore score)
        {
            if (scoreGaugeCalculator == null)
            {
                return 0f;
            }

            return scoreGaugeCalculator.CalculateGaugeProgress(score.totalScore);
        }

        public void UpdateScore(ref LiveScore score, int addScore)
        {
            float progress = CalculateGaugeProgress(score);
            if (float.IsNaN(progress))
            {
                progress = 0f;
            }

            if (scoreGauge != null)
            {
                scoreGauge.size = new Vector2(progress * scoreGaugeSizeCache.x, scoreGaugeSizeCache.y);
            }

            number?.UpdateNumber(score.totalScore);
            outlineNumber?.UpdateNumber(score.totalScore);

            int rank = (int)score.rank;
            if (rankTextSpriteRenderer != null)
            {
                rankTextSpriteRenderer.color = ColorUtility.GetScoreRankColor(rank);
            }

            if (lastScoreRank != rank)
            {
                if (rankSpriteRenderer != null)
                {
                    rankSpriteRenderer.sprite = rankSprites != null && rank >= 0 && rank < rankSprites.Length
                        ? rankSprites[rank]
                        : null;
                }
                lastScoreRank = rank;
            }

            if (addScore >= 1)
            {
                PlayAddScore(addScore);
            }
        }

        private void Update()
        {
            if (!addScoreAnimating || addScoreRoot == null)
            {
                return;
            }

            addScoreAnimationTime += Time.deltaTime;
            float moveRate = Mathf.Clamp01(addScoreAnimationTime / 0.2f);
            float ease = 1f - (1f - moveRate) * (1f - moveRate);
            addScoreRoot.localPosition = Vector3.Lerp(
                new Vector3(-0.5f, addScoreBasePosition.y, addScoreBasePosition.z),
                addScoreBasePosition,
                ease);

            float alpha = Mathf.Clamp01(addScoreAnimationTime / 0.2f);
            addScoreNumber?.UpdateAlpha(alpha);
            addScoreOutlineNumber?.UpdateAlpha(alpha);

            if (addScoreAnimationTime >= 0.5f)
            {
                addScoreRoot.localScale = Vector3.zero;
                addScoreAnimating = false;
            }
        }

        private void SetupGaugeRankPosition(Transform t, float value)
        {
            if (t == null || scoreGauge == null)
            {
                return;
            }

            float x = scoreGauge.transform.localPosition.x;
            float maxX = x + scoreGaugeSizeCache.x;
            Vector3 position = t.localPosition;
            position.x = x + (maxX - x) * Mathf.Clamp01(value);
            t.localPosition = position;
        }

        private void PlayAddScore(int addScore)
        {
            if (addScoreRoot == null)
            {
                return;
            }

            addScoreRoot.localScale = Vector3.one;
            if (inverseScale)
            {
                Vector3 scale = addScoreRoot.localScale;
                scale.y = -1f;
                addScoreRoot.localScale = scale;
            }
            addScoreRoot.localPosition = new Vector3(-0.5f, addScoreBasePosition.y, addScoreBasePosition.z);
            addScoreAnimationTime = 0f;
            addScoreAnimating = true;
            addScoreNumber?.UpdateNumber(addScore);
            addScoreOutlineNumber?.UpdateNumber(addScore);
            addScoreNumber?.UpdateAlpha(0f);
            addScoreOutlineNumber?.UpdateAlpha(0f);
        }
    }
}
