using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.ApiData;

namespace Sekai
{
	public class MusicSelectMusicCustomScoreData
	{
		public readonly int MusicId;

		public Dictionary<MusicDifficulty, UserCustomMusicScorePublishedResponse> SelectedCustomMusicScoreDic
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

		public Dictionary<MusicDifficulty, List<UserCustomMusicScorePublishedResponse>> SelectableCustomMusicScoreListDic
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

		public MusicSelectMusicCustomScoreData(int musicId)
		{
			throw null;
		}

		public bool HasSelectedCustomMusicScore(MusicDifficulty difficulty)
		{
			throw null;
		}

		public bool HasSelectableCustomMusicScore(MusicDifficulty difficulty)
		{
			throw null;
		}
	}
}
