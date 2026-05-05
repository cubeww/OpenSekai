namespace Sekai
{
    public class FreeLiveBootData : LiveBootDataBase
    {
        public int DeckId { get; set; }

        public int[] DeckFormationIndexes { get; set; }

        public FreeLiveBootData(
            int musicId,
            string difficulty,
            int vocalId,
            int deckId,
            LivePlayMode playMode,
            MusicCategory category = MusicCategory.original)
        {
            DeckId = deckId;
            LivePlayMode = playMode;
            MusicCategory = category;
            MusicData = CreateMusicData(musicId, difficulty, vocalId);
        }

        public FreeLiveBootData(
            int musicId,
            string difficulty,
            int vocalId,
            int deckId,
            int[] formationIndexes)
            : this(musicId, difficulty, vocalId, deckId, LivePlayMode.Free, MusicCategory.image)
        {
            DeckFormationIndexes = formationIndexes;
        }

        private static LiveMusicData CreateMusicData(int musicId, string difficulty, int vocalId)
        {
            return new LiveMusicData
            {
                Music = new MasterMusic
                {
                    id = musicId
                },
                Difficulty = new MasterMusicDifficulty
                {
                    musicId = musicId,
                    musicDifficulty = difficulty
                },
                Vocal = new MasterMusicVocal
                {
                    id = vocalId,
                    musicId = musicId
                }
            };
        }
    }
}
