using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Outgame;
using Sekai.MusicShop;

namespace Sekai.MusicScoreMaker.OutGame.Create
{
	public class ScreenLayerMusicScoreMakerMusicSelectForCreateModel
	{
		public readonly bool IsCreateFromOfficialScore;

		private readonly MusicShopSortFilterData _sortFilterData;

		public CategoryTabType[] MusicCategoryTabArray
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

		public CategoryTabType SelectMusicCategoryTab
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

		public MusicCellData[] MusicCellDataArray
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

		public bool IsMusicEmpty
		{
			get
			{
				throw null;
			}
		}

		public SortType SortType
		{
			get
			{
				throw null;
			}
		}

		public SortOrderBy SortOrder
		{
			get
			{
				throw null;
			}
		}

		public FilteredData FilteredData
		{
			get
			{
				throw null;
			}
		}

		private bool IsFilteredByFreeWord
		{
			get
			{
				throw null;
			}
		}

		public ScreenLayerMusicScoreMakerMusicSelectForCreateModel(bool isCreateFromOfficialScore)
		{
			throw null;
		}

		public void Setup()
		{
			throw null;
		}

		public void ApplyMusicCellDataArray()
		{
			throw null;
		}

		public void ApplyMusicCategory(CategoryTabType musicCategory)
		{
			throw null;
		}

		public void ApplyFreeWordForFilter(string freeWordForFilter)
		{
			throw null;
		}

		public void SetSortType(SortType sortType)
		{
			throw null;
		}

		public void SetSortOrder(SortOrderBy sortOrder)
		{
			throw null;
		}

		public void SetFilteredData(FilteredData filteredData)
		{
			throw null;
		}

		public void SaveSortFilterData()
		{
			throw null;
		}

		private CategoryTabType[] CreateMusicCategoryTabArray()
		{
			throw null;
		}

		private IEnumerable<MusicCellData> CreateMusicCellDataList()
		{
			throw null;
		}

		private IEnumerable<MusicCellData> SortMusicCellDataList(IEnumerable<MusicCellData> source)
		{
			throw null;
		}

		private bool IsFilteredByVocalType(MasterMusicAllModel musicAllModel, VocalTypeFilteredData vocalTypeFilteredData)
		{
			throw null;
		}
	}
}
