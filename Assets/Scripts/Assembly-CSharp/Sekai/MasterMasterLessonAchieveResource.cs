using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMasterLessonAchieveResource
	{
		[Key("releaseConditionId")]
		public int releaseConditionId;

		[Key("cardId")]
		public int cardId;

		[Key("masterRank")]
		public int masterRank;

		[Key("resources")]
		public UserResource[] resources;

		public MasterMasterLessonAchieveResource()
		{
		}
	}
}
