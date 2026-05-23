using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterPlayLevelScore
	{
		[Key("liveType")]
		public string liveType;

		[Key("playLevel")]
		public int playLevel;

		[Key("s")]
		public int s;

		[Key("a")]
		public int a;

		[Key("b")]
		public int b;

		[Key("c")]
		public int c;

		public MasterPlayLevelScore()
		{
		}
	}
}
