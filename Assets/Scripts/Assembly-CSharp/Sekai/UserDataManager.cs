namespace Sekai
{
	public class UserDataManager
	{
		private static UserDataManager instance;

		public static UserDataManager Instance => instance ??= new UserDataManager();

		public FreeLiveBootData FreeLiveBootData { get; set; }

		public int SelectedDeckId { get; set; }

		private UserDataManager()
		{
			SelectedDeckId = 0;
		}
	}
}
