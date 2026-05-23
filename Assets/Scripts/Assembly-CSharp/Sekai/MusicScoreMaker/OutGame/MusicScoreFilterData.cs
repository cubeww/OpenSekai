namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreFilterData
	{
		public readonly Defines.ClearStatusFilterType ClearStatus;

		public readonly int MinPlayLevel;

		public readonly int MaxPlayLevel;

		public readonly Defines.FullComboRateFilterType FullComboRateFilterType;

		public readonly int MinFullComboRate;

		public readonly int MaxFullComboRate;

		public readonly Defines.ReviewCountFilterType ReviewCountFilter;

		public readonly int ReviewCountThreshold;

		public readonly bool OnlyDerivativeAllowed;

		private static (int min, int max)? _cachedDefaultRange;

		public MusicScoreFilterData(Defines.ClearStatusFilterType clearStatus, int minPlayLevel, int maxPlayLevel, Defines.FullComboRateFilterType fullComboRateFilterType, int minFullComboRate, int maxFullComboRate, Defines.ReviewCountFilterType reviewCountFilter, int reviewCountThreshold, bool onlyDerivativeAllowed)
		{
			ClearStatus = clearStatus;
			MinPlayLevel = minPlayLevel;
			MaxPlayLevel = maxPlayLevel;
			FullComboRateFilterType = fullComboRateFilterType;
			MinFullComboRate = minFullComboRate;
			MaxFullComboRate = maxFullComboRate;
			ReviewCountFilter = reviewCountFilter;
			ReviewCountThreshold = reviewCountThreshold;
			OnlyDerivativeAllowed = onlyDerivativeAllowed;
		}

		public bool IsDefault()
		{
			if (!_cachedDefaultRange.HasValue)
			{
				_cachedDefaultRange = new MusicScorePlayLevelRangeProvider().GetPlayLevelRange(MusicDifficulty.none);
			}

			(int min, int max) defaultRange = _cachedDefaultRange.Value;
			return ClearStatus == Defines.ClearStatusFilterType.All
				&& MinPlayLevel == defaultRange.min
				&& MaxPlayLevel == defaultRange.max
				&& FullComboRateFilterType == Defines.FullComboRateFilterType.All
				&& ReviewCountFilter == Defines.ReviewCountFilterType.All
				&& !OnlyDerivativeAllowed;
		}
	}
}
