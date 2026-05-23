using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class AnotherUserCard
	{
		[Key("cardId")]
		public int cardId;

		[Key("level")]
		public int level;

		[Key("masterRank")]
		public int masterRank;

		[Key("specialTrainingStatus")]
		public string specialTrainingStatus;

		[Key("defaultImage")]
		public string defaultImage;

		public AnotherUserCard()
		{
		}
	}
}
