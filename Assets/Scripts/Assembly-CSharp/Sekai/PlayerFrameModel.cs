using Sekai.ApiData;

namespace Sekai
{
	public class PlayerFrameModel
	{
		public enum PlayerFrameCellType
		{
			horizontal = 0,
			vertical = 1,
			landscape = 2
		}

		private UserPlayerFrame _userPlayerFrame;

		private MasterPlayerFrame _masterPlayerFrame;

		private MasterPlayerFrameGroup _masterPlayerFrameGroup;

		public int PlayerFrameId
		{
			get
			{
				throw null;
			}
		}

		public bool IsSetting
		{
			get
			{
				throw null;
			}
		}

		public bool ExistPlayerFrame
		{
			get
			{
				throw null;
			}
		}

		public string FrameName
		{
			get
			{
				throw null;
			}
		}

		public string FrameDescription
		{
			get
			{
				throw null;
			}
		}

		public PlayerFrameModel(UserPlayerFrame userPlayerFrame)
		{
			throw null;
		}

		public PlayerFrameModel(int playerFrameId, PlayerFrameAttachStatus status)
		{
			throw null;
		}

		private void InitializeMasterData()
		{
			throw null;
		}

		public string GetAssetBundleName(PlayerFrameCellType type)
		{
			throw null;
		}

		public string GetThumbnailAssetBundleName()
		{
			throw null;
		}

		public string GetThumbnailAssetName()
		{
			throw null;
		}
	}
}
