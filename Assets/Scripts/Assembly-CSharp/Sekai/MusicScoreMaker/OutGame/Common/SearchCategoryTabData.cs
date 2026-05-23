namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public sealed class SearchCategoryTabData : CategoryTabData
	{
		public readonly Defines.CategoryType Category;

		public SearchCategoryTabData(int index, string title, Defines.CategoryType categoryType)
			: base(index, title)
		{
			Category = categoryType;
		}
	}
}
