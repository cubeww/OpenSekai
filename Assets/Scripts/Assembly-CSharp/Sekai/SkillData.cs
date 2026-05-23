using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.Core.Live;
using Sekai.Multiplay;

namespace Sekai
{
	public class SkillData
	{
		public int CardId
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

		public int Index
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

		public string UserId
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public string RandomSeedStr
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

		public SkillSpriteType Type
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

		public int Level
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

		public int? ReferenceTargetCardId
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

		public bool IsReferenceTargetIllustNormal
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

		public int? ReferenceTargetCardIdForEncore
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

		public bool IsReferenceTargetIllustNormalForEncore
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

		public BaseSkillEffect[] SkillEffects
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

		public SkillData(IngameLotterySkill skill, LiveDeckData deck, bool isAuto)
		{
			throw null;
		}

		public SkillData(MultiLivePartyMember member, string randomSeed)
		{
			throw null;
		}

		private List<BaseSkillEffect> Create(MasterSkill skill, int cardId, int level, LiveDeckData deck, int characterRank, bool isOthers, bool isAuto, bool isSubCard = false)
		{
			throw null;
		}

		private bool IsMatchUnitCountSkillCondition(MasterSkillEffect skillEffect, int unitCount)
		{
			throw null;
		}

		private int GetExcludeSpecifiedTypeUnitCount(int cardId, LiveDeckMember[] members)
		{
			throw null;
		}
	}
}
