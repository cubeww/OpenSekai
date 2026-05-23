using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMusicCollaboration
	{
		[Key("id")]
		public int id;

		[Key("label")]
		public string label;

		public MasterMusicCollaboration()
		{
		}
	}
}
