using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMusicVocalCharacter : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("musicId")]
		public int musicId;

		[Key("musicVocalId")]
		public int musicVocalId;

		[Key("characterType")]
		public string characterType;

		[Key("characterId")]
		public int characterId;

		[Key("seq")]
		public int seq;

		[IgnoreMember]
		public CharacterType CharacterType
		{
			get
			{
				throw null;
			}
		}

		public UnitType GetUnitType()
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

		public MasterMusicVocalCharacter()
		{
		}
	}
}
