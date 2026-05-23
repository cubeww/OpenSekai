namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreTagSelectCellData
	{
		public readonly int Id;

		public readonly string Name;

		public bool IsSelected;

		public bool IsEnabled;

		public MusicScoreTagSelectCellData(int id, string name, bool isSelected = false, bool isEnabled = true)
		{
			Id = id;
			Name = name;
			IsSelected = isSelected;
			IsEnabled = isEnabled;
		}
	}
}
