namespace Sekai.UI
{
	public interface IMusicShopCellData
	{
		MasterMusicAllModel MusicAll { get; set; }

		UserShopItem UserData { get; set; }

		bool IsNew { get; set; }

		int ShopId { get; set; }

		MasterShopItem MasterShopItem { get; set; }

		MasterShopItemModel ShopItem { get; }

		MusicDifficulty SelectedMusicDifficulty { get; set; }

		MasterMusicDifficultyModel MasterMusicDifficulty { get; }

		long PublishedAt { get; }

		void Setup(int shopId, UserShopItem userData);
	}
}
