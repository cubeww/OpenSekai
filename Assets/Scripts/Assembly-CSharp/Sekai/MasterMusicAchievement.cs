using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMusicAchievement : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("musicAchievementType")]
		public string musicAchievementType;

		[Key("musicAchievementTypeValue")]
		public string musicAchievementTypeValue;

		[Key("musicDifficultyType")]
		public string musicDifficultyType;

		[Key("resourceBoxId")]
		public int resourceBoxId;

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

		public MasterMusicAchievement()
		{
		}
	}
}
