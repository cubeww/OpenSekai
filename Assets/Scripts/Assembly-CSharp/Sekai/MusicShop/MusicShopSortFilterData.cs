using MessagePack;

namespace Sekai.MusicShop
{
	[MessagePackObject(false)]
	public class MusicShopSortFilterData
	{
		public enum FilterType
		{
			MusicShop = 0,
			AnotherVocalShop = 1,
			MusicScoreMakerMusicSelectForCreate = 2
		}

		[Key("FilteredData")]
		public FilteredData FilteredData;

		[Key("SortData")]
		public SortData SortData;

		[IgnoreMember]
		private FilterType _type;

		public void Setup(FilterType type)
		{
			throw null;
		}

		public void Load()
		{
			throw null;
		}

		public void Save()
		{
			throw null;
		}

		public MusicShopSortFilterData()
		{
			throw null;
		}
	}
}
