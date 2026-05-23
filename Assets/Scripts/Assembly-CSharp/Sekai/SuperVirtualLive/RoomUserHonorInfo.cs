using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai.SuperVirtualLive
{
	[MessagePackObject(false)]
	public class RoomUserHonorInfo
	{
		[Key(0)]
		public int Type
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		[Key(1)]
		public int Id
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		[Key(2)]
		public int Level
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		[Key(3)]
		public int BondsWordId
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		[Key(4)]
		public int BondsViewType
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		[Key(5)]
		public int HonorMissionProgress
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public RoomUserHonorInfo()
		{
		}

		public RoomUserHonorInfo(int id, int level, int honorMissionProgress)
		{
			throw null;
		}

		public RoomUserHonorInfo(int id, int level, int honorMissionProgress, int bondsWordId, HonorViewType viewType)
		{
			throw null;
		}

		public RoomUserHonorInfo(UserProfileHonor profileHonor, UserHonorMission userHonorMission)
		{
			throw null;
		}
	}
}
