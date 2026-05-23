using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class IngameComboCutin
	{
		[Key("cardId1")]
		public int cardId1;

		[Key("cardId2")]
		public int cardId2;

		[Key("ingameCutinCharacterId")]
		public int ingameCutinCharacterId;

		public IngameComboCutin()
		{
		}
	}
}
