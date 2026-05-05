namespace Sekai
{
    public class LiveBootDataBase
    {
        public LiveMusicData MusicData { get; set; }

        public LiveSettingData LiveSettingData { get; set; } = new LiveSettingData();

        public MusicCategory MusicCategory { get; set; } = MusicCategory.image;

        public LivePlayMode LivePlayMode { get; set; } = LivePlayMode.Free;

        public bool IsAuto { get; set; }

        public bool canSkipDisplayMusicInfo { get; set; }
    }
}
