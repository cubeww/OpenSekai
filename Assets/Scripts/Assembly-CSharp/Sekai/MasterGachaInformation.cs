using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGachaInformation
	{
		[Key("gachaId")]
		public int gachaId;

		[Key("summary")]
		public string summary;

		[Key("description")]
		public string description;

		public MasterGachaInformation()
		{
		}
	}
}
