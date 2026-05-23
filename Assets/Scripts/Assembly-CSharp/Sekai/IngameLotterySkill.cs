using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class IngameLotterySkill
	{
		[Key("seq")]
		public int seq;

		[Key("cardId")]
		public int cardId;

		[Key("relationCardId")]
		public int? relationCardId;

		[Key("ingameCutinCharacterId")]
		public int ingameCutinCharacterId;

		public IngameLotterySkill()
		{
		}
	}
}
