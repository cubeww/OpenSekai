using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	[Serializable]
	[MessagePackObject(false)]
	public class ClipboardCacheData
	{
		[Key(0)]
		public string Id
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(1)]
		public List<MusicScoreNoteBase> CopiedNoteList
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(2)]
		public List<MusicScoreEventData> CopiedEventDataList
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(3)]
		public string CreatedAt
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ClipboardCacheData()
		{
			Id = Guid.NewGuid().ToString();
			CopiedNoteList = new List<MusicScoreNoteBase>();
			CopiedEventDataList = new List<MusicScoreEventData>();
			CreatedAt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
		}

		public ClipboardCacheData(List<MusicScoreNoteBase> copiedNoteList, List<MusicScoreEventData> copiedEventDataList)
		{
			if (copiedNoteList == null)
			{
				throw new ArgumentNullException(nameof(copiedNoteList));
			}
			if (copiedEventDataList == null)
			{
				throw new ArgumentNullException(nameof(copiedEventDataList));
			}

			Id = Guid.NewGuid().ToString();
			CopiedNoteList = copiedNoteList.Select(note => note.Clone()).ToList();
			CopiedEventDataList = copiedEventDataList.Select(eventData => eventData.Clone()).ToList();
			CreatedAt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
		}
	}
}
