namespace Sekai
{
    public class MasterMusicDifficulty
    {
        public int id;

        public int musicId;

        public string musicDifficulty = "easy";

        public int playLevel;

        public int totalNoteCount;

        public MusicDifficulty MusicDifficulty
        {
            get
            {
                return System.Enum.TryParse(musicDifficulty, true, out MusicDifficulty result)
                    ? result
                    : MusicDifficulty.Easy;
            }
        }
    }
}
