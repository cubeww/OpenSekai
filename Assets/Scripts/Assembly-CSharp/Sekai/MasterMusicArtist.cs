using System.Collections.Generic;
using System.Globalization;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterMusicArtist
	{
		[Key("id")]
		public int id;

		[Key("name")]
		public string name;

		[Key("pronunciation")]
		public string pronunciation;

		public bool ContainsWord(string word, CompareOptions options)
		{
			throw null;
		}

		public IEnumerable<string> GetFilteredElementsByWord(string word, CompareOptions options)
		{
			throw null;
		}

		public MasterMusicArtist()
		{
		}
	}
}
