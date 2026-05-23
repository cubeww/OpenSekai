namespace Sekai
{
	public class UserAccountManager
	{
		public class UserAccountData
		{
			public string Credential = string.Empty;
		}

		private static readonly UserAccountManager instance = new UserAccountManager();

		public static UserAccountManager Instance => instance;

		public bool IsLogin { get; set; }
		public string DeviceId { get; set; } = string.Empty;
		public UserAccountData Data { get; set; } = new UserAccountData();
	}
}
