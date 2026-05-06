namespace Sekai
{
    public class LiveMusicData
    {
        public MasterMusic Music { get; set; }

        public MasterMusicDifficulty Difficulty { get; set; }

        public MasterMusicVocal Vocal { get; set; }

        public MasterMusicCollaboration Collaboration { get; set; }

        public MasterPlayLevelScore Score { get; set; }

        public bool IsCollaboration
        {
            get { return Collaboration != null; }
        }
    }
}
