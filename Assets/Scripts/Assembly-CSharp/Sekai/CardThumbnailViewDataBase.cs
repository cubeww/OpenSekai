using System.Runtime.CompilerServices;
using Sekai.MultiLive;
using Sekai.Multiplay;

namespace Sekai
{
	public class CardThumbnailViewDataBase : CardViewDataBase
	{
		public enum DispParamType : byte
		{
			Level = 0,
			TotalPower = 1,
			SkillLevel = 2,
			EventBonus = 3,
			EventSupportBonus = 4
		}

		public DispParamType ParamType
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

		public bool IsNotShowLeaderLabel
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

		public CardThumbnailViewDataBase(CardViewDataBase data)
		{
			throw null;
		}

		public CardThumbnailViewDataBase(UserResource resource)
		{
			throw null;
		}

		public CardThumbnailViewDataBase(UserResource resource, bool isIlustNormal)
		{
			throw null;
		}

		public CardThumbnailViewDataBase(UserCard userCard)
		{
			throw null;
		}

		public CardThumbnailViewDataBase(UserCard userCard, bool isIlustNormal)
		{
			throw null;
		}

		public CardThumbnailViewDataBase(MultiLivePartyMember multiLivePartyMember)
		{
			throw null;
		}

		public CardThumbnailViewDataBase(PlayerInfo playerInfo)
		{
			throw null;
		}
	}
}
