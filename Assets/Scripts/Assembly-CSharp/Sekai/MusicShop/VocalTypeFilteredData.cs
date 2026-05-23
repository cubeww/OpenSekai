using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai.MusicShop
{
	[MessagePackObject(false)]
	public class VocalTypeFilteredData
	{
		[Key("VirtualSingerGameCharacterIds")]
		public HashSet<int> VirtualSingerGameCharacterIds;

		[Key("VirtualSingerOutSide")]
		public bool VirtualSingerOutSide;

		[Key("SekaiCharacterIds")]
		public List<int> SekaiCharacterIds;

		[IgnoreMember]
		public bool IsAllVirtualSingerCharacter
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsAllVirtualSingerGameCharacter
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsAllSekaiCharacter
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		private int[] VirtualSingerGameCharacterAllIds
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
		private int[] SekaiCharacterAllIds
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

		public VocalTypeFilteredData()
		{
			throw null;
		}

		public VocalTypeFilteredData(VocalTypeFilteredData filteredData)
		{
			throw null;
		}

		public void Setup()
		{
			throw null;
		}
	}
}
