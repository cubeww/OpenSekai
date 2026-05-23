using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MultiIngameCutin
	{
		[Key("userId1")]
		public long userId1;

		[Key("cardId1")]
		public int cardId1;

		[Key("userId2")]
		public long userId2;

		[Key("cardId2")]
		public int cardId2;

		[Key("ingameCutinCharacterId")]
		public int ingameCutinCharacterId;

		[Key("seq")]
		public int seq;

		public MultiIngameCutin()
		{
		}
	}
}
