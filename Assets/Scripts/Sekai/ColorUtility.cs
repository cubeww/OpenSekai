using System;
using UnityEngine;

namespace Sekai
{
    public static class ColorUtility
    {
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
    }
}
