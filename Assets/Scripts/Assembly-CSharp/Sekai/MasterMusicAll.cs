using System.Collections.Generic;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMusicAll
	{
		[Key("id")]
		public int id;

		[Key("music")]
		public MasterMusic music;

		[Key("musicArtist")]
		public MasterMusicArtist musicArtist;

		[Key("musicDifficulties")]
		public MasterMusicDifficulty[] musicDifficulties;

		[Key("musicVocals")]
		public MasterMusicVocal[] musicVocals;

		[Key("musicAchievements")]
		public MasterMusicAchievement[] musicAchievements;

		[Key("musicVideoCharacters")]
		public List<MasterMusicVideoCharacter> musicVideoCharacters;

		[Key("isLimitedTimeMusic")]
		public bool isLimitedTimeMusic;

		[IgnoreMember]
		public string ArtistName
		{
			get
			{
				throw null;
			}
		}

		public bool ExistDifficulty(MusicDifficulty difficulty)
		{
			throw null;
		}

		public MasterMusicDifficulty GetMasterMusicDifficulty(MusicDifficulty difficulty)
		{
			throw null;
		}

		public int GetPlayLevelByDifficulty(MusicDifficulty difficulty)
		{
			throw null;
		}

		public bool ContainsWord(string word)
		{
			throw null;
		}

		public IEnumerable<string> GetFilteredElementsByWord(string word)
		{
			throw null;
		}

		public MasterMusicAll()
		{
		}
	}
}
