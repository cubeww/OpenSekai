using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreFilterEvaluator
	{
		public IEnumerable<MusicScoreData> Filter([NotNull] IEnumerable<MusicScoreData> source, [NotNull] MusicScoreFilterData filter)
		{
			return source.Where(x => IsMatch(x, filter));
		}

		private bool IsMatch([NotNull] MusicScoreData source, [NotNull] MusicScoreFilterData filter)
		{
			if (!IsMatchClearStatus(filter.ClearStatus, source.ClearStatus))
			{
				return false;
			}

			if (source.PlayLevel < filter.MinPlayLevel || source.PlayLevel > filter.MaxPlayLevel)
			{
				return false;
			}

			if (filter.FullComboRateFilterType == Defines.FullComboRateFilterType.Range
				&& (source.FullComboRate < filter.MinFullComboRate || source.FullComboRate > filter.MaxFullComboRate))
			{
				return false;
			}

			if (filter.ReviewCountFilter == Defines.ReviewCountFilterType.OverThreshold
				&& source.ReviewCount < filter.ReviewCountThreshold)
			{
				return false;
			}

			return !filter.OnlyDerivativeAllowed || source.IsDerivativeAllowed;
		}

		private bool IsMatchClearStatus(Defines.ClearStatusFilterType type, MusicClearStatus status)
		{
			switch (type)
			{
				case Defines.ClearStatusFilterType.NotClear:
					return status == MusicClearStatus.NotClear;
				case Defines.ClearStatusFilterType.NotFullCombo:
					return status < MusicClearStatus.FullCombo;
				case Defines.ClearStatusFilterType.NotAllPerfect:
					return status < MusicClearStatus.AllPerfect;
				default:
					return true;
			}
		}

		public MusicScoreFilterEvaluator()
		{
		}
	}
}
