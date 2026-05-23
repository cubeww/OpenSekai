using MessagePack;
using Sekai.MultiLive;
using Sekai.Mysekai.Multi;
using Sekai.SuperVirtualLive;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserProfileHonor
	{
		[Key("seq")]
		public int seq;

		[Key("profileHonorType")]
		public string profileHonorType;

		[Key("honorId")]
		public int honorId;

		[Key("bondsHonorViewType")]
		public string bondsHonorViewType;

		[Key("bondsHonorWordId")]
		public int bondsHonorWordId;

		[Key("honorLevel")]
		public int honorLevel;

		[IgnoreMember]
		public HonorType Type
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public HonorViewType BondsViewType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool useUnitVirtualSinger
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsEmpty
		{
			get
			{
				throw null;
			}
		}

		public UserProfileHonor()
		{
		}

		public UserProfileHonor(int seq, int honorId, int honorLevel)
		{
			throw null;
		}

		public UserProfileHonor(int seq)
		{
			throw null;
		}

		public UserProfileHonor(Sekai.SuperVirtualLive.RoomUserHonorInfo info)
		{
			throw null;
		}

		public UserProfileHonor(Sekai.MultiLive.RoomUserHonorInfo info)
		{
			throw null;
		}

		public UserProfileHonor(Sekai.Mysekai.Multi.RoomUserHonorInfo info)
		{
			throw null;
		}

		public UserProfileHonor(UserBondsHonor profileDataUserHonor)
		{
			throw null;
		}

		public UserProfileHonor(int seq, int honorId, int honorLevel, int wordId, bool reverse, bool useUnitVirtualSinger)
		{
			throw null;
		}
	}
}
