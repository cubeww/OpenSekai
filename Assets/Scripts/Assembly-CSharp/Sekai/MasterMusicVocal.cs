using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMusicVocal : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("musicId")]
		public int musicId;

		[Key("musicVocalType")]
		public string musicVocalType;

		[Key("seq")]
		public int seq;

		[Key("releaseConditionId")]
		public int releaseConditionId;

		[Key("caption")]
		public string caption;

		[Key("characters")]
		public MasterMusicVocalCharacter[] characters;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("specialSeasonId")]
		public int specialSeasonId;

		[Key("archivePublishedAt")]
		public long archivePublishedAt;

		[Key("archiveDisplayType")]
		public string archiveDisplayType;

		[IgnoreMember]
		public ArchiveDisplayType ArchiveDisplayType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public MusicVocalType MusicVocalType
		{
			get
			{
				throw null;
			}
		}

		public bool ExistsCharacterByUnitType(UnitType unitType)
		{
			throw null;
		}

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public MasterMusicVocal()
		{
		}
	}
}
