using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class PublishedFilteredData
	{
		public enum FilteredType
		{
			All = 0,
			AD = 1
		}

		[Key("Type")]
		public FilteredType Type;

		[IgnoreMember]
		public int[] PublishedAdList
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsAD
		{
			get
			{
				throw null;
			}
		}

		[Key("PublishedAd")]
		public int PublishedAd
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		[IgnoreMember]
		public List<string> PublishedAdStringList
		{
			get
			{
				throw null;
			}
		}

		public int GetPublishedAdIndex()
		{
			throw null;
		}

		public void SetData(int[] publishedAdList)
		{
			throw null;
		}

		public void SetPublishedAd(int publishedAd)
		{
			throw null;
		}

		public PublishedFilteredData()
		{
			throw null;
		}
	}
}
