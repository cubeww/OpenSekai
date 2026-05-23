namespace Sekai.UI
{
	public class CardListFilterSortSettingData
	{
		public CardList.FilterUnit FilterUnit { get; set; }

		public CardList.FilterAttribute FilterAttribute { get; set; }

		public CardList.FilterEpisode FilterEpisode { get; set; }

		public CardList.FilterAnotherCutIn FilterAnotherCutIn { get; set; }

		public CardRarityFilterType[] UnCheckedFilterRarityTypes { get; set; }

		public SkillFilterType[] UnCheckedFilterSkillTypes { get; set; }

		public CardList.FilterCostumeType[] FilterCostumeTypes { get; set; }

		public bool IsUseFilterCostume { get; set; }

		public CardList.SortKey SortKey { get; set; }

		public int[] UnCheckedCharacterIds { get; set; }

		public CardListFilterSortSettingData()
		{
		}
	}
}
