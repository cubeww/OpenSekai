using System;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class RankingMusicScoreListCellData : MusicScoreListCellData
	{
		private static readonly float[] RankingFontSizes = { 35f, 30f, 24f, 18f };

		public readonly int Ranking;

		public bool IsTop3
		{
			get
			{
				return Ranking >= 1 && Ranking <= 3;
			}
		}

		public RankingMusicScoreListCellData([NotNull] MusicScoreData musicScoreData, int ranking)
			: base(musicScoreData)
		{
			Ranking = ranking;
		}

		public Color GetRankColor()
		{
			switch (Ranking)
			{
				case 1:
					return Sekai.MusicScoreMaker.OutGame.Search.Defines.RankingRank1Color;
				case 2:
					return Sekai.MusicScoreMaker.OutGame.Search.Defines.RankingRank2Color;
				case 3:
					return Sekai.MusicScoreMaker.OutGame.Search.Defines.RankingRank3Color;
				default:
					return Color.white;
			}
		}

		public float GetHighlightRankingFontSize()
		{
			return IsTop3 ? 48f : GetNeutralRankingFontSize();
		}

		public float GetNeutralRankingFontSize()
		{
			int digits = Math.Max(1, Ranking.ToString().Length);
			int index = Math.Min(digits - 1, RankingFontSizes.Length - 1);
			return RankingFontSizes[index];
		}
	}
}
