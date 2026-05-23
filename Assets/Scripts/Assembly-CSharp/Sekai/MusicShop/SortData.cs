using MessagePack;

namespace Sekai.MusicShop
{
	[MessagePackObject(false)]
	public class SortData
	{
		[Key("SortOrder")]
		public SortOrderBy SortOrder;

		[Key("SortType")]
		public SortType SortType;

		[Key("MusicDifficulty")]
		public MusicDifficulty MusicDifficulty;

		[IgnoreMember]
		public bool IsMusicDifficulty
		{
			get
			{
				throw null;
			}
		}

		public SortData()
		{
			throw null;
		}
	}
}
