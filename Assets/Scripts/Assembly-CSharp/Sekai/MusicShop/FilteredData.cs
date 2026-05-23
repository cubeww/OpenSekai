using System.Collections.Generic;
using MessagePack;
using Sekai.UI;

namespace Sekai.MusicShop
{
	[MessagePackObject(false)]
	public class FilteredData
	{
		[Key("MusicTag")]
		public MusicTag MusicTag;

		[Key("FreeWord")]
		public string FreeWord;

		[Key("MusicVideoFilterKey")]
		public MusicVideoFilterKey MusicVideoFilterKey;

		[Key("PublishedData")]
		public PublishedFilteredData PublishedData;

		[Key("VocalTypeData")]
		public VocalTypeFilteredData VocalTypeData;

		[IgnoreMember]
		public bool FilteredMusic
		{
			get
			{
				throw null;
			}
		}

		public void SetData<T>(IReadOnlyList<T> viewDataList) where T : IMusicShopCellData
		{
			throw null;
		}

		public void Setup()
		{
			throw null;
		}

		public FilteredData()
		{
			throw null;
		}
	}
}
