namespace Sekai.MusicScoreMaker.Ingame.Utilities
{
	public static class MusicScoreMakerRuleSlideTutorialUtility
	{
		public static bool TryShowTutorialSlideIfFirstTime(string topicKey)
		{
			string ruleSlideType = GetRuleSlideType(topicKey);
			if (string.IsNullOrEmpty(ruleSlideType))
			{
				return false;
			}
			System.Type tutorialUtilityType = System.Type.GetType("Sekai.TutorialUtility, Assembly-CSharp");
			object result = tutorialUtilityType?.GetMethod("ShowTutorialSlideByType", new[] { typeof(string), typeof(System.Action), typeof(bool) })
				?.Invoke(null, new object[] { ruleSlideType, null, false });
			return result is bool shown && shown;
		}

		public static string GetRuleSlideType(string topicKey)
		{
			return topicKey switch
			{
				"enter" => "MUSIC_SCORE_MAKER_INGAME_ENTER",
				"long" => "MUSIC_SCORE_MAKER_INGAME_LONG",
				"copy" => "MUSIC_SCORE_MAKER_INGAME_COPY",
				"bpm" => "MUSIC_SCORE_MAKER_INGAME_BPM",
				"error" => "MUSIC_SCORE_MAKER_INGAME_ERROR",
				_ => null
			};
		}
	}
}
