using System;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMusic
	{
		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("releaseConditionId")]
		public int releaseConditionId;

		[Key("title")]
		public string title;

		[Key("pronunciation")]
		public string pronunciation;

		[Key("categories")]
		public string[] categories;

		[Key("creatorArtistId")]
		public int creatorArtistId;

		[Key("lyricist")]
		public string lyricist;

		[Key("composer")]
		public string composer;

		[Key("arranger")]
		public string arranger;

		[Key("dancerCount")]
		public int dancerCount;

		[Key("selfDancerPosition")]
		public int selfDancerPosition;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("liveTalkBackgroundAssetbundleName")]
		public string liveTalkBackgroundAssetbundleName;

		[Key("description")]
		public string description;

		[Key("publishedAt")]
		public long publishedAt;

		[Key("releasedAt")]
		public long releasedAt;

		[Key("liveStageId")]
		public int liveStageId;

		[Key("fillerSec")]
		public float fillerSec;

		[Key("musicCollaborationId")]
		public int musicCollaborationId;

		[Key("isNewlyWrittenMusic")]
		public bool isNewlyWrittenMusic;

		[Key("isFullLength")]
		public bool isFullLength;

		[Key("secForMusicScoreMaker")]
		public int secForMusicScoreMaker;

		[Key("isAvailableForMusicScoreMaker")]
		public bool isAvailableForMusicScoreMaker;

		[IgnoreMember]
		public DateTime PublishedTime
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public DateTime ReleasedTime
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public MusicCategory[] Categories
		{
			get
			{
				throw null;
			}
		}

		public MasterMusic()
		{
		}
	}
}
