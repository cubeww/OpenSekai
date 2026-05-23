using Sekai.MusicShop;
using UnityEngine;

namespace Sekai
{
	public class MusicShopFilterDialog : CharacterFilterDialog
	{
		[SerializeField]
		private MusicShopMVFilterContent mvFilterContent;

		[SerializeField]
		private PublishFilterContent publishFilterContent;

		[SerializeField]
		private MusicShopVocalFilterContent vocalFilterContent;

		[SerializeField]
		private GameObject[] vocalFilterRoots;

		private FilteredData filteredData;

		public FilteredData GetFilteredData()
		{
			throw null;
		}

		public void Setup(MusicShopSortFilterData.FilterType filterType, FilteredData filteredData)
		{
			throw null;
		}

		private void SetupMV()
		{
			throw null;
		}

		private void SetupPublished()
		{
			throw null;
		}

		private void SetVocal(MusicShopSortFilterData.FilterType filterType)
		{
			throw null;
		}

		public MusicShopFilterDialog()
		{
			throw null;
		}
	}
}
