using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMusicVideoCharacter
	{
		[Key("id")]
		public int id;

		[Key("musicId")]
		public int musicId;

		[Key("defaultMusicType")]
		public string defaultMusicType;

		[Key("gameCharacterUnitId")]
		public int gameCharacterUnitId;

		[Key("dancePriority")]
		public int dancePriority;

		[Key("seq")]
		public int seq;

		[IgnoreMember]
		public DefaultMusicType DefaultMusicType
		{
			get
			{
				throw null;
			}
		}

		public MasterMusicVideoCharacter()
		{
		}
	}
}
