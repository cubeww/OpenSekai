using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMusicDifficulty : IMessagePackSerializationCallbackReceiver
	{
		[IgnoreMember]
		public const int INVALID_ID = -1;

		[Key("id")]
		public int id;

		[Key("musicId")]
		public int musicId;

		[Key("musicDifficulty")]
		public string musicDifficulty;

		[Key("playLevel")]
		public int playLevel;

		[Key("totalNoteCount")]
		public int totalNoteCount;

		[IgnoreMember]
		public MusicDifficulty MusicDifficulty
		{
			get
			{
				throw null;
			}
		}

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public MasterMusicDifficulty()
		{
		}
	}
}
