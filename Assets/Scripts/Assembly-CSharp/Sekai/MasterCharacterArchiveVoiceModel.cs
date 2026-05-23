using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.ApiData;

namespace Sekai
{
	public sealed class MasterCharacterArchiveVoiceModel
	{
		private readonly MasterCharacterArchiveVoice _master;

		public int Id
		{
			get
			{
				throw null;
			}
		}

		public int GroupId
		{
			get
			{
				throw null;
			}
		}

		public int GameCharacterId
		{
			get
			{
				throw null;
			}
		}

		public CharacterArchiveVoiceType VoiceType
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public IReadOnlyList<MasterCharacterArchiveVoiceTagModel> VoiceTags
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public string DisplayPhrase
		{
			get
			{
				throw null;
			}
		}

		public string DisplayPhrase2
		{
			get
			{
				throw null;
			}
		}

		public int ExternalId
		{
			get
			{
				throw null;
			}
		}

		public string AssetName
		{
			get
			{
				throw null;
			}
		}

		public bool IsNextGrade
		{
			get
			{
				throw null;
			}
		}

		public long DisplayStartAt
		{
			get
			{
				throw null;
			}
		}

		public UnitType UnitType
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public bool ExistsUnitType
		{
			get
			{
				throw null;
			}
		}

		public bool ExistsVoiceTags
		{
			get
			{
				throw null;
			}
		}

		public bool ExistsMaster
		{
			get
			{
				throw null;
			}
		}

		public static MasterCharacterArchiveVoiceModel CreateMasterCharacterArchiveVoiceModel(MasterLiveClearVoiceModel liveClearVoice)
		{
			throw null;
		}

		public MasterCharacterArchiveVoiceModel(int archiveVoiceId)
		{
			throw null;
		}

		public MasterCharacterArchiveVoiceModel(MasterCharacterArchiveVoice master)
		{
			throw null;
		}

		public bool ContainsTag(CharacterArchiveVoiceTagType tagType)
		{
			throw null;
		}

		private CharacterArchiveVoiceType ConvertVoiceType()
		{
			throw null;
		}

		private IReadOnlyList<MasterCharacterArchiveVoiceTagModel> CreateVoiceTags()
		{
			throw null;
		}

		private UnitType ConvertUnitType()
		{
			throw null;
		}
	}
}
