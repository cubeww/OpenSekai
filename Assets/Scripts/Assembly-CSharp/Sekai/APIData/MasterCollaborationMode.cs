using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterCollaborationMode
	{
		[Key("id")]
		public int id;

		[Key("assetbundleName")]
		public string assetbundleName;

		public MasterCollaborationMode()
		{
		}
	}
}
