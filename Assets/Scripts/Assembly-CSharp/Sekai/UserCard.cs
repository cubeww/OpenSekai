using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserCard
	{
		public const string SPECIAL_TRAINING_DONE = "done";

		public const string SPECIAL_TRAINING_DO_NOTHING = "not_doing";

		public const string DEFAULT_IMAGE_ORIGINAL = "original";

		public const string DEFAULT_IMAGE_SPECIAL_TRAINING = "special_training";

		public const string DEFAULT_IMAGE_NORMAL = "normal";

		[Key("userId")]
		public long userId;

		[Key("cardId")]
		public int cardId;

		[Key("level")]
		public int level;

		[Key("exp")]
		public int exp;

		[Key("totalExp")]
		public int totalExp;

		[Key("skillLevel")]
		public int skillLevel;

		[Key("skillExp")]
		public int skillExp;

		[Key("totalSkillExp")]
		public int totalSkillExp;

		[Key("masterRank")]
		public int masterRank;

		[Key("specialTrainingStatus")]
		public string specialTrainingStatus;

		[Key("defaultImage")]
		public string defaultImage;

		[Key("duplicateCount")]
		public int duplicateCount;

		[Key("createdAt")]
		public long createdAt;

		[Key("episodes")]
		public UserCardEpisode[] episodes;

		[IgnoreMember]
		public bool DoneSpecialTraining
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public TrainingStatus TrainingStatus
		{
			get
			{
				throw null;
			}
		}

		public UserCard(UserCard source)
		{
			throw null;
		}

		public UserCard(AnotherUserCard source)
		{
			throw null;
		}

		public UserCard()
		{
		}
	}
}
