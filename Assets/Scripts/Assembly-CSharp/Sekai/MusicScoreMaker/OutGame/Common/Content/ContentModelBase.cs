namespace Sekai.MusicScoreMaker.OutGame.Common.Content
{
	public abstract class ContentModelBase<TViewData> : IContentModel where TViewData : ContentViewDataBase, new()
	{
		public readonly TViewData ViewData;

		protected ContentModelBase()
		{
			ViewData = new TViewData();
		}
	}
}
