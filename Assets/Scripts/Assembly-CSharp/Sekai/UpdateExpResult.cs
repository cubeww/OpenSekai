using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UpdateExpResult
	{
		[Key("beforeTotalExp")]
		public int beforeTotalExp;

		[Key("afterTotalExp")]
		public int afterTotalExp;

		[Key("beforeExp")]
		public int beforeExp;

		[Key("afterExp")]
		public int afterExp;

		[Key("beforeLevel")]
		public int beforeLevel;

		[Key("afterLevel")]
		public int afterLevel;

		public UpdateExpResult()
		{
		}
	}
}
