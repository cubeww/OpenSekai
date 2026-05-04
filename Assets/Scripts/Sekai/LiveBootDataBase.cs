namespace Sekai
{
    public class LiveBootDataBase
    {
        public LiveMusicData MusicData { get; set; }

        public MusicCategory MusicCategory { get; set; } = MusicCategory.image;

        public LivePlayMode LivePlayMode { get; set; } = LivePlayMode.Free;
    }
}
