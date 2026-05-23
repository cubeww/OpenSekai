using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserBondsHonor
	{
		[Key("bondsHonorId")]
		public int bondsHonorId;

		[Key("level")]
		public int level;

		[Key("obtainedAt")]
		public long obtainedAt;

		[Key("description")]
		public string description;

		public UserBondsHonor()
		{
		}
	}
}
