using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserMaterial
	{
		[Key("materialId")]
		public int materialId;

		[Key("quantity")]
		public int quantity;

		public UserMaterial()
		{
		}
	}
}
