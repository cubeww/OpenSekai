using Sekai.Core.Live;
using DG.Tweening;
using UnityEngine;

namespace Sekai
{
	public class ScoreView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer scoreGauge;

		[SerializeField]
		private SpriteRenderer rankSpriteRenderer;

		[SerializeField]
		private Sprite[] rankSprites;

		[SerializeField]
		private SpriteRenderer rankTextSpriteRenderer;

		[SerializeField]
		private Transform gaugeRankS;

		[SerializeField]
		private Transform gaugeRankA;

		[SerializeField]
		private Transform gaugeRankB;

		[SerializeField]
		private Transform gaugeRankC;

		[SerializeField]
		private NumberView number;

		[SerializeField]
		private NumberView outlineNumber;

		[SerializeField]
		private Transform addScoreRoot;

		[SerializeField]
		private NumberView addScoreNumber;

		[SerializeField]
		private NumberView addScoreOutlineNumber;

		private int lastScoreRank;

		private Sequence addScoreSequence;

		private ScoreGaugeCalculator scoreGaugeCalculator;

		private Vector2 scoreGaugeSizeCache = new Vector2(5.648148f, 0.222222f);

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
			scoreGaugeCalculator = ScoreGaugeCalculator.Create(musicData?.Score);
			inverseScale = addScoreRoot != null && addScoreRoot.localScale.y < 0f;
			CreateAddScoreAnimation();
			Clear();
		}

		public void ChangeScoreCalculator(ScoreGaugeCalculator newCalculator)
		{
			scoreGaugeCalculator = newCalculator ?? ScoreGaugeCalculator.Create(1);
		}

		public void SetupScore(LiveScore score)
		{
			UpdateScore(ref score, 0);
		}

		public void Clear()
		{
			lastScoreRank = -1;
			if (scoreGauge != null)
			{
				scoreGauge.size = new Vector2(0f, scoreGaugeSizeCache.y);
			}
			if (rankSpriteRenderer != null)
			{
				rankSpriteRenderer.sprite = null;
			}
			if (number != null)
			{
				number.Setup("#ffffff", "#CCCCEE");
			}
			if (outlineNumber != null)
			{
				outlineNumber.Setup("#444466", "#444466");
			}
			if (addScoreNumber != null)
			{
				addScoreNumber.Setup("#ffffff", "#ffffff00");
			}
			if (addScoreOutlineNumber != null)
			{
				addScoreOutlineNumber.Setup("#444466", "#ffffff00");
			}
			if (addScoreRoot != null)
			{
				addScoreRoot.localScale = Vector3.zero;
			}
		}

		public float CalculateGaugeProgress(LiveScore score)
		{
			return scoreGaugeCalculator.CalculateGaugeProgress(score.totalScore);
		}

		public void UpdateScore(ref LiveScore score, int addScore)
		{
			if (scoreGaugeCalculator == null)
			{
				scoreGaugeCalculator = ScoreGaugeCalculator.Create(1);
			}
			if (scoreGauge != null)
			{
				float progress = CalculateGaugeProgress(score);
				if (float.IsNaN(progress))
				{
					progress = 0f;
				}
				Vector2 size = scoreGauge.size;
				size.x = progress * scoreGaugeSizeCache.x;
				size.y = scoreGaugeSizeCache.y;
				scoreGauge.size = size;
			}
			if (number != null)
			{
				number.UpdateNumber(score.totalScore);
			}
			if (outlineNumber != null)
			{
				outlineNumber.UpdateNumber(score.totalScore);
			}

			if (rankSpriteRenderer != null && lastScoreRank != (int)score.rank)
			{
				int rankIndex = (int)score.rank;
				rankSpriteRenderer.sprite = rankSprites != null && rankIndex >= 0 && rankIndex < rankSprites.Length ? rankSprites[rankIndex] : null;
				if (rankTextSpriteRenderer != null)
				{
					rankTextSpriteRenderer.color = ColorUtility.GetScoreRankColor(score.rank);
				}
				lastScoreRank = rankIndex;
			}

			if (addScore > 0 && addScoreRoot != null)
			{
				addScoreRoot.localScale = inverseScale ? new Vector3(1f, -1f, 1f) : Vector3.one;
				if (addScoreSequence == null)
				{
					CreateAddScoreAnimation();
				}
				addScoreSequence?.Restart();
				if (addScoreNumber != null)
				{
					addScoreNumber.UpdateNumber(addScore);
				}
				if (addScoreOutlineNumber != null)
				{
					addScoreOutlineNumber.UpdateNumber(addScore);
				}
			}
		}

		private void SetupGaugeRankPosition(Transform t, float value)
		{
			if (t == null || scoreGauge == null)
			{
				return;
			}
			Vector3 gaugePosition = scoreGauge.transform.localPosition;
			Vector3 position = t.localPosition;
			position.x = gaugePosition.x + Mathf.Clamp01(value) * scoreGaugeSizeCache.x;
			t.localPosition = position;
		}

		private void CreateAddScoreAnimation()
		{
			if (addScoreRoot == null)
			{
				return;
			}
			addScoreSequence?.Kill();
			Vector3 start = addScoreRoot.localPosition;
			Vector3 from = new Vector3(-0.5f, start.y, start.z);
			Vector3 to = new Vector3(0f, start.y, start.z);
			addScoreSequence = DOTween.Sequence();
			addScoreSequence.Pause();
			addScoreSequence.AppendCallback(() =>
			{
				addScoreRoot.localPosition = from;
				UpdateAddNumberAlpha(0f);
			});
			addScoreSequence.Append(addScoreRoot.DOLocalMove(to, 0.2f).SetEase(Ease.OutQuad));
			addScoreSequence.Join(DOTween.To(() => 0f, UpdateAddNumberAlpha, 1f, 0.2f).SetEase(Ease.OutQuad));
			addScoreSequence.AppendInterval(0.5f);
			addScoreSequence.AppendCallback(() => addScoreRoot.localScale = Vector3.zero);
			addScoreSequence.SetAutoKill(false);
		}

		private void UpdateAddNumberAlpha(float alpha)
		{
			if (addScoreNumber != null)
			{
				addScoreNumber.UpdateAlpha(alpha);
			}
			if (addScoreOutlineNumber != null)
			{
				addScoreOutlineNumber.UpdateAlpha(alpha);
			}
		}
	}
}
