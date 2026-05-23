using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreFilterModel
	{
		public Defines.ClearStatusFilterType ClearStatus
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int MinPlayLevel
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int MaxPlayLevel
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public List<int> PlayLevelList
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int MinPlayLevelIndex
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int MaxPlayLevelIndex
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public Defines.FullComboRateFilterType FullComboRateFilterType
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int MinFullComboRate
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int MaxFullComboRate
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public Defines.ReviewCountFilterType ReviewCountFilter
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int ReviewCount
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public bool OnlyDerivativeAllowed
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public void Initialize(MusicDifficulty difficulty)
		{
			throw null;
		}

		public void InitializeFromModel(MusicScoreFilterData musicScoreFilterData, MusicDifficulty difficulty)
		{
			throw null;
		}

		private void SetPlayLevelRange(int min, int max)
		{
			throw null;
		}

		public void ApplyMinPlayLevelByIndex(int index)
		{
			throw null;
		}

		public void ApplyMaxPlayLevelByIndex(int index)
		{
			throw null;
		}

		public void SetClearStatusByIndex(int index)
		{
			throw null;
		}

		public void SetFullComboRateTypeByIndex(int index)
		{
			throw null;
		}

		public void SetMinFullComboRateByText(string text)
		{
			throw null;
		}

		public void SetMaxFullComboRateByText(string text)
		{
			throw null;
		}

		public void SetReviewCountTypeByIndex(int index)
		{
			throw null;
		}

		public void SetReviewCountByText(string text)
		{
			throw null;
		}

		public void SetOnlyDerivativeAllowedByIndex(int index)
		{
			throw null;
		}

		public MusicScoreFilterData BuildFilterData()
		{
			throw null;
		}

		private int ClampIndex(int index, int count)
		{
			throw null;
		}

		public MusicScoreFilterModel()
		{
			throw null;
		}
	}
}
