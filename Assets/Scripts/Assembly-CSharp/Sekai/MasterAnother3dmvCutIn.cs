using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterAnother3dmvCutIn
	{
		[Key("id")]
		public int id;

		[Key("cardId")]
		public int cardId;

		[Key("musicId")]
		public int musicId;

		[Key("cutInNo")]
		public int cutInNo;

		public MasterAnother3dmvCutIn()
		{
		}
	}
}
