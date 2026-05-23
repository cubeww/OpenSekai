using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Sekai
{
	public class MasterMusicAllModel
	{
		private MasterMusicModel _masterMusic;

		private IReadOnlyList<MasterMusicDifficultyModel> _musicDifficulties;

		private IReadOnlyList<MasterMusicVocalModel> _musicVocals;

		private IReadOnlyList<MasterMusicVideoCharacterModel> _musicVideoCharacters;

		public int Id
		{
			get
			{
				throw null;
			}
		}

		public MasterMusicModel Music
		{
			get
			{
				throw null;
			}
		}

		public IReadOnlyList<MasterMusicDifficultyModel> MusicDifficulties
		{
			get
			{
				throw null;
			}
		}

		public int MusicDifficultyCount
		{
			get
			{
				throw null;
			}
		}

		public IReadOnlyList<MasterMusicVocalModel> MusicVocals
		{
			get
			{
				throw null;
			}
		}

		public bool ExistsMultipleMusicVocals
		{
			get
			{
				throw null;
			}
		}

		public MasterMusicAchievement[] MusicAchievements
		{
			get
			{
				throw null;
			}
		}

		public IReadOnlyList<MasterMusicVideoCharacterModel> MusicVideoCharacters
		{
			get
			{
				throw null;
			}
		}

		public MasterMusicAll MasterMusicAll
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		private MasterMusicArtist MusicArtist
		{
			get
			{
				throw null;
			}
		}

		public string ArtistName
		{
			get
			{
				throw null;
			}
		}

		public float FillerSec
		{
			get
			{
				throw null;
			}
		}

		public bool IsLimitedTimeMusic
		{
			get
			{
				throw null;
			}
		}

		public MasterCollaborationModeModel CollaborationMode
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public bool ExistsCollaborationMode
		{
			get
			{
				throw null;
			}
		}

		public bool IsForVirtualSinger
		{
			get
			{
				throw null;
			}
		}

		public bool ExistsAppend
		{
			get
			{
				throw null;
			}
		}

		public MasterMusicAllModel(MasterMusicAll masterMusicAll)
		{
			throw null;
		}

		public MusicDifficulty GetDefaultDifficulty()
		{
			throw null;
		}

		public bool ExistDifficulty(MusicDifficulty difficulty)
		{
			throw null;
		}

		public MasterMusicDifficultyModel GetMasterMusicDifficulty(MusicDifficulty difficulty)
		{
			throw null;
		}

		public int GetPlayLevelByDifficulty(MusicDifficulty difficulty)
		{
			throw null;
		}

		public MasterMusicVocalModel GetMusicVocalModel(int vocalId)
		{
			throw null;
		}

		public bool ContainsWord(string word)
		{
			throw null;
		}

		[CanBeNull]
		private MasterCollaborationModeModel GetMasterCollaborationModeModel()
		{
			throw null;
		}
	}
}
