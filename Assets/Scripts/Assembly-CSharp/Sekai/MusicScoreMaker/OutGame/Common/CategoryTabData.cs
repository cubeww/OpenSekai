using Sekai.UI;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public class CategoryTabData : GenericTabData
	{
		public readonly string Title;

		public CategoryTabData(int index, string title)
			: base(index, true)
		{
			Title = title;
		}
	}
}
