using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search
{
	public static class Defines
	{
		public enum RankingFilterType
		{
			Daily = 0,
			All = 1
		}

		public enum SpecifiedMusicFilterType
		{
			Recommend = 0,
			Ranking = 1
		}

		public static readonly Color RankingRank1Color;

		public static readonly Color RankingRank2Color;

		public static readonly Color RankingRank3Color;

		static Defines()
		{
			RankingRank1Color = ParseHtmlColor("#d0b65b99");
			RankingRank2Color = ParseHtmlColor("#acd2df99");
			RankingRank3Color = ParseHtmlColor("#e4c7ad99");
		}

		private static Color ParseHtmlColor(string html)
		{
			return UnityEngine.ColorUtility.TryParseHtmlString(html, out Color color) ? color : Color.white;
		}
	}
}
