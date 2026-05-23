using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.MultiLive;
using Sekai.Multiplay;

namespace Sekai
{
	public class CardViewDataBase
	{
		public enum CardUnitType
		{
			OriginalCharacter = 0,
			SekaiVirtualSinger = 1,
			OriginalVirtualSinger = 2
		}

		private float _eventBonus;

		private float _eventBonusLimit;

		private float _eventBonusLimitLeader;

		private float _eventBonusLeader;

		private float _eventHonorBonus;

		public int CardId
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

		public UserCard UserCard
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

		public UserCharacter UserCharacter
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

		public MasterCard MasterCard
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

		public MasterSkill MasterSkill
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

		public MasterSkill SpecialTrainingMasterSkill
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

		public MasterGameCharacter MasterCharacter
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

		public MasterCardRarity MasterCardRarity
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

		public IEnumerable<MasterAnother3dmvCutIn> MasterAnother3dmvCutIns
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public bool IsCreatedCanvas
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public bool IsTraining
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

		public byte Attribute
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

		public int MasterLv
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

		public int Lv
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

		public int SkillLv
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

		public int TotalPower
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

		public int TotalPowerBuff
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

		public int TotalPowerIncludeBuff
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

		public int AreaItemPower
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			protected set
			{
				throw null;
			}
		}

		public int CharacterRankBonusPower
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int GateBonusPower
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public int FixtureBonusPower
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public bool IsIllustNormal
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

		public bool IsCheck
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

		public bool AllowGrowthOnDetail
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

		public MasterCard[] DeckInfo
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

		public float EventBonus
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public decimal EventSupportBonus
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public bool ExistsMasterCard
		{
			get
			{
				throw null;
			}
		}

		public bool IsShowDoubleSkill
		{
			get
			{
				throw null;
			}
		}

		public MasterSkill CurrentMasterSkill
		{
			get
			{
				throw null;
			}
		}

		public bool IsOnlyAfterTraining
		{
			get
			{
				throw null;
			}
		}

		public bool CanSwitchIllust
		{
			get
			{
				throw null;
			}
		}

		public CardTrainingType CardTrainingType
		{
			get
			{
				throw null;
			}
		}

		public bool ShouldShowAfterTrainingView
		{
			get
			{
				throw null;
			}
		}

		public string FileSuffixName
		{
			get
			{
				throw null;
			}
		}

		public CardViewDataBase()
		{
		}

		public CardViewDataBase(CardViewDataBase src)
		{
			throw null;
		}

		public CardViewDataBase(UserCard userCard, MasterCard[] deckInfo = null)
		{
			throw null;
		}

		public void UpdateUserCard(UserCard userCard)
		{
			throw null;
		}

		public CardViewDataBase(UserCard userCard, bool isIllustNormal, MasterCard[] deckInfo = null)
		{
			throw null;
		}

		public CardViewDataBase(UserResource resource)
		{
			throw null;
		}

		public CardViewDataBase(UserResource resource, bool isIllustNormal)
		{
			throw null;
		}

		public CardViewDataBase(MultiLivePartyMember multiLivePartyMember)
		{
			throw null;
		}

		public CardViewDataBase(int cardId)
		{
			throw null;
		}

		public CardViewDataBase(PlayerInfo playerInfo)
		{
			throw null;
		}

		public bool ExistsCard()
		{
			throw null;
		}

		private void SetupCardIdData()
		{
			throw null;
		}

		protected virtual void SetupCardIdDataExtensions()
		{
			throw null;
		}

		public void SetupUserCardData(bool updatedUserCard = true)
		{
			throw null;
		}

		public CardUnitType GetCardUnitType()
		{
			throw null;
		}

		public UnitType GetUnitType()
		{
			throw null;
		}

		public UnitType GetUnit()
		{
			throw null;
		}

		protected virtual void SetupUserCardDataExtensions(bool updatedUserCard = true)
		{
			throw null;
		}

		private void SetupMultiLivePartyMemberData(MultiLivePartyMember multiLivePartyMember)
		{
			throw null;
		}

		private void SetupMultiLivePartyMemberData(PlayerInfo playerInfo)
		{
			throw null;
		}

		protected virtual void SetupMultiLivePartyMemberDataExtensions(MultiLivePartyMember multiLivePartyMember)
		{
			throw null;
		}

		protected virtual void SetupMultiLivePartyMemberDataExtensions(PlayerInfo playerInfo)
		{
			throw null;
		}

		public void SetEventSupportBonus(int gameCharacterId)
		{
			throw null;
		}

		public void SetEventBonus(bool isLeaderCard, bool eventCardBonusLimit)
		{
			throw null;
		}
	}
}
