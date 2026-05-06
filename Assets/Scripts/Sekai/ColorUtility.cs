using System;
using UnityEngine;

namespace Sekai
{
    public static class ColorUtility
    {
        public static Color Create(string code)
        {
            if (UnityEngine.ColorUtility.TryParseHtmlString(code, out Color color))
            {
                return color;
            }

            return Color.white;
        }

        public static Color Create(string code, float alpha)
        {
            Color color = Create(code);
            color.a = alpha;
            return color;
        }

        public static Color Create(int r, int g, int b, int a = 255)
        {
            return new Color(
                Mathf.Clamp01(r / 255f),
                Mathf.Clamp01(g / 255f),
                Mathf.Clamp01(b / 255f),
                Mathf.Clamp01(a / 255f));
        }

        public static Color GetDifficultyColor(MusicDifficulty difficulty)
        {
            switch (difficulty)
            {
                case MusicDifficulty.Easy:
                    return new Color(0.06666667f, 0.8666667f, 0.46666667f, 1f);
                case MusicDifficulty.Normal:
                    return new Color(0.2f, 0.8f, 1f, 1f);
                case MusicDifficulty.Hard:
                    return new Color(1f, 0.8f, 0f, 1f);
                case MusicDifficulty.Expert:
                    return new Color(1f, 0.26666668f, 0.46666667f, 1f);
                case MusicDifficulty.Master:
                    return new Color(0.8f, 0.2f, 1f, 1f);
                case MusicDifficulty.Append:
                    return new Color(1f, 0.49019608f, 0.7882353f, 1f);
                default:
                    return Color.white;
            }
        }

        public static Color GetDifficultyColor(string difficulty)
        {
            if (Enum.TryParse(difficulty, true, out MusicDifficulty parsed))
            {
                return GetDifficultyColor(parsed);
            }

            return Color.white;
        }

        public static Color GetScoreRankColor(ScoreRank rank)
        {
            return GetScoreRankColor((int)rank);
        }

        public static Color GetScoreRankColor(int index)
        {
            switch ((ScoreRank)index)
            {
                case ScoreRank.S:
                    return Create("#FF8ec6");
                case ScoreRank.A:
                    return Create("#e18aff");
                case ScoreRank.B:
                    return Create("#78adff");
                case ScoreRank.C:
                    return Create("#55fdf7");
                case ScoreRank.D:
                    return Create("#73ffcc");
                default:
                    return Color.white;
            }
        }
    }
}
