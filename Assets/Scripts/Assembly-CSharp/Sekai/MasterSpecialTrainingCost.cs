using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterSpecialTrainingCost
	{
		[Key("cardId")]
		public int cardId;

		[Key("seq")]
		public int seq;

		[Key("cost")]
		public UserResource cost;

		public MasterSpecialTrainingCost()
		{
		}
	}
}
