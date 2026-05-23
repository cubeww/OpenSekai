using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	[Serializable]
	[MessagePackObject(false)]
	public class ClipboardCacheListData
	{
		[Key(0)]
		public List<ClipboardCacheData> Caches
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ClipboardCacheListData()
		{
			Caches = new List<ClipboardCacheData>();
		}
	}
}
