using System;
using MessagePack;
using Sekai.MultiLive;

namespace Sekai.Multiplay
{
	[Serializable]
	[MessagePackObject(false)]
	public struct MultiLivePartyMember
	{
		[Key("Index")]
		public int Index;

		[Key("ActorNum")]
		public int ActorNum;

		[Key("UserId")]
		public string UserId;

		[Key("UserName")]
		public string UserName;

		[Key("CardId")]
		public int CardId;

		[Key("CardLv")]
		public int CardLv;

		[Key("CardSkillLv")]
		public int CardSkillLv;

		[Key("CardMasterRank")]
		public int CardMasterRank;

		[Key("CostumeUnitType")]
		public UnitType CostumeUnitType;

		[Key("HairLiveCostumeId")]
		public int HairLiveCostumeId;

		[Key("BodyLiveCostumeId")]
		public int BodyLiveCostumeId;

		[Key("AccessoryLiveCostumeId")]
		public int AccessoryLiveCostumeId;

		[Key("TotalPowerIncludeBuff")]
		public int TotalPowerIncludeBuff;

		[Key("IsTraining")]
		public bool IsTraining;

		[Key("DefaultImage")]
		public string DefaultImage;

		[Key("SubCardIds")]
		public int[] SubCardIds;

		[Key("SubCardSkillLv")]
		public int[] SubCardSkillLv;

		[Key("SubCardImages")]
		public string[] SubCardImages;

		[Key("MainHonor")]
		public RoomUserHonorInfo MainHonor;

		[Key("SubHonors")]
		public RoomUserHonorInfo[] SubHonors;

		[Key("Difficulty")]
		public string Difficulty;

		[Key("ConnectStatus")]
		public MultiLiveMultiplayConst.PartyMemberConnectStatus ConnectStatus;

		[Key("FriendRequestStatus")]
		public FriendRequestStatus FriendRequestStatus;

		[Key("MemberCharacterRank")]
		public MemberCharacterRank[] MemberCharacterRank;

		[Key("PlayerFrameId")]
		public int PlayerFrameId;

		[Key("CustomScoreId")]
		public string CustomScoreId;

		public void Reset()
		{
			throw null;
		}

		public void Setup(PlayerInfo player)
		{
			throw null;
		}
	}
}
