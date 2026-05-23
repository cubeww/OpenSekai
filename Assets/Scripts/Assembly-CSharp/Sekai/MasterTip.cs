using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterTip
	{
		public enum ViewType
		{
			TYPE_COMIC = 0,
			TYPE_TEXT_TIPS = 1
		}

		[Key("id")]
		public int id;

		[Key("title")]
		public string title;

		[Key("description")]
		public string description;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("fromUserRank")]
		public int fromUserRank;

		[Key("toUserRank")]
		public int toUserRank;

		public ViewType GetViewType()
		{
			return string.IsNullOrEmpty(description) ? ViewType.TYPE_COMIC : ViewType.TYPE_TEXT_TIPS;
		}

		public MasterTip()
		{
		}
	}
}
