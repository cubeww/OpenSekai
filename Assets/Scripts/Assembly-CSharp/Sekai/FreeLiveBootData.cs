namespace Sekai
{
	public class FreeLiveBootData : LiveBootDataBase
	{
		public int[] DeckFormationIndexes { get; set; }

		public string CustomMusicScoreId { get; set; }

		public string CustomMusicScorePath { get; set; }

		public bool IsOfficialMusicScore { get; set; }

		public string CustomMusicScoreTitle { get; set; }

		public string CustomMusicScoreAuthorName { get; set; }

		public MenuScreenType? ReturnScreenType { get; set; }

		public FreeLiveBootData(int musicId, string difficulty, int vocalId, int deckId, int[] formationIndexes, LiveMusicData.CollaborationModeState collaboModeState, bool isOriginalMember = false, LiveScoreMode liveScoreMode = LiveScoreMode.Normal, LiveTournamentMode liveTournamentMode = LiveTournamentMode.None)
			: base(musicId, difficulty, vocalId, deckId, LiveType.solo, collaboModeState)
		{
			DeckFormationIndexes = formationIndexes ?? System.Array.Empty<int>();
			LiveScoreMode = liveScoreMode;
			LiveTournamentMode = liveTournamentMode;
			LiveModelData = new LiveModelData(isOriginalMember);
		}

		public FreeLiveBootData(int musicId, string difficulty, int vocalId)
			: base(musicId, difficulty, vocalId, LiveType.solo, LiveMusicData.CollaborationModeState.Off)
		{
			DeckFormationIndexes = System.Array.Empty<int>();
		}

		public FreeLiveBootData(int musicId, string difficulty, int vocalId, int deckId, LivePlayMode playMode, LiveMusicData.CollaborationModeState collaboModeState, MusicCategory category = MusicCategory.original)
			: base(musicId, difficulty, vocalId, deckId, LiveType.solo, collaboModeState)
		{
			DeckFormationIndexes = System.Array.Empty<int>();
			LivePlayMode = playMode;
			MusicCategory = category;
		}
	}
}
