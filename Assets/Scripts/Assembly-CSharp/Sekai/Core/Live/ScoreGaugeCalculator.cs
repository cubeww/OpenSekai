using UnityEngine;

namespace Sekai.Core.Live
{
	public class ScoreGaugeCalculator
	{
		public const float RankRateS = 0.9f;
		public const float RankRateA = 0.75f;
		public const float RankRateB = 0.6f;
		public const float RankRateC = 0.45f;

		private readonly System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, float>> gaugeValues;

		public ScoreGaugeCalculator()
		{
			gaugeValues = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, float>>();
		}

		private ScoreGaugeCalculator(System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, float>> gaugeValues)
		{
			this.gaugeValues = gaugeValues;
		}

		public static ScoreGaugeCalculator Create(MasterPlayLevelScore score)
		{
			if (score == null || (score.s <= 0 && score.a <= 0 && score.b <= 0 && score.c <= 0))
			{
				return Create(1000000);
			}
			return Create(score.s, score.a, score.b, score.c);
		}

		public static ScoreGaugeCalculator Create(int totalScore)
		{
			float total = totalScore;
			return Create(
				Mathf.FloorToInt(RankRateS * total),
				Mathf.FloorToInt(RankRateA * total),
				Mathf.FloorToInt(RankRateB * total),
				Mathf.FloorToInt(RankRateC * total));
		}

		public static ScoreGaugeCalculator Create(int s, int a, int b, int c)
		{
			var values = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, float>>
			{
				new System.Collections.Generic.KeyValuePair<int, float>(0, 0f),
				new System.Collections.Generic.KeyValuePair<int, float>(c, RankRateC),
				new System.Collections.Generic.KeyValuePair<int, float>(b, RankRateB),
				new System.Collections.Generic.KeyValuePair<int, float>(a, RankRateA),
				new System.Collections.Generic.KeyValuePair<int, float>(s, RankRateS)
			};
			return new ScoreGaugeCalculator(values);
		}

		public static ScoreRank GetScoreRank(MasterPlayLevelScore masterPlayLevelScore, int score)
		{
			if (masterPlayLevelScore == null)
			{
				return ScoreRank.D;
			}
			if (score >= masterPlayLevelScore.s)
			{
				return ScoreRank.S;
			}
			if (score >= masterPlayLevelScore.a)
			{
				return ScoreRank.A;
			}
			if (score >= masterPlayLevelScore.b)
			{
				return ScoreRank.B;
			}
			if (score >= masterPlayLevelScore.c)
			{
				return ScoreRank.C;
			}
			return ScoreRank.D;
		}

		public float CalculateGaugeProgress(int score)
		{
			if (gaugeValues == null || gaugeValues.Count <= 1)
			{
				return 0f;
			}

			for (int i = 0; i < gaugeValues.Count - 1; i++)
			{
				var current = gaugeValues[i];
				var next = gaugeValues[i + 1];
				bool isLastSegment = i == gaugeValues.Count - 2;
				if (next.Key <= score && !isLastSegment)
				{
					continue;
				}

				float denominator = next.Key - current.Key;
				if (Mathf.Approximately(denominator, 0f))
				{
					return Mathf.Clamp01(next.Value);
				}

				float progress = ((score - current.Key) / denominator) * (next.Value - current.Value) + current.Value;
				return progress < 0f ? 0f : Mathf.Min(progress, 1f);
			}

			return 0f;
		}

		public float[] CalculateMultiGaugeProgress(LiveScore[] scoreArray)
		{
			if (scoreArray == null)
			{
				return System.Array.Empty<float>();
			}
			float[] values = new float[scoreArray.Length];
			int total = 0;
			for (int i = 0; i < scoreArray.Length; i++)
			{
				total += scoreArray[i].totalScore;
			}
			float totalProgress = CalculateGaugeProgress(total);
			for (int i = 0; i < scoreArray.Length; i++)
			{
				float ratio = total > 0 ? (float)scoreArray[i].totalScore / total : 0f;
				values[i] = Mathf.Clamp01(totalProgress * ratio);
			}
			return values;
		}

		public float[] CalculateMultiGaugeProgress(int[] scoreArray)
		{
			if (scoreArray == null)
			{
				return System.Array.Empty<float>();
			}
			float[] values = new float[scoreArray.Length];
			int total = 0;
			for (int i = 0; i < scoreArray.Length; i++)
			{
				total += scoreArray[i];
			}
			float totalProgress = CalculateGaugeProgress(total);
			for (int i = 0; i < scoreArray.Length; i++)
			{
				float ratio = total > 0 ? (float)scoreArray[i] / total : 0f;
				values[i] = Mathf.Clamp01(totalProgress * ratio);
			}
			return values;
		}
	}
}
