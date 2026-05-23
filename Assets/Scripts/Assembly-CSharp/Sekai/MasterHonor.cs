using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterHonor : IMessagePackSerializationCallbackReceiver
	{
		public enum HonorType
		{
			@default = 0,
			rank_match_grade = 1
		}

		[Key("id")]
		public int id;

		[Key("seq")]
		public int seq;

		[Key("groupId")]
		public int groupId;

		[Key("honorRarity")]
		public string honorRarity;

		[Key("name")]
		public string name;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("description")]
		public string description;

		[Key("levels")]
		public MasterHonorLevel[] levels;

		[Key("honorTypeId")]
		public int honorTypeId;

		[Key("honorMissionType")]
		public string honorMissionType;

		[IgnoreMember]
		public static IReadOnlyList<HonorMissionType> LiveMasterHonorMissionTypes
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public HonorMissionType HonorMissionType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public HonorRarity? Rarity
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool ExistsBonus
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool ExistsLevels
		{
			get
			{
				throw null;
			}
		}

		public int GetBonus(int level)
		{
			throw null;
		}

		public string GetLevelDescription(int level)
		{
			throw null;
		}

		public bool IsLiveMasterHonor()
		{
			throw null;
		}

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public MasterHonor()
		{
			throw null;
		}

		static MasterHonor()
		{
			throw null;
		}
	}
}
