namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public sealed class SaveDraftRequestData
	{
		public string Title { get; }

		public string Memo { get; }

		public SaveDraftRequestData(string title, string memo)
		{
			Title = title;
			Memo = memo;
		}
	}
}
