using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterHonorLevel
	{
		[Key("honorId")]
		public int honorId;

		[Key("level")]
		public int level;

		[Key("bonus")]
		public int bonus;

		[Key("description")]
		public string description;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("honorRarity")]
		public string honorRarity;

		[IgnoreMember]
		public HonorRarity HonorRarity
		{
			get
			{
				throw null;
			}
		}

		public MasterHonorLevel()
		{
			throw null;
		}
	}
}
