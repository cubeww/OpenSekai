namespace Sekai
{
    public class LiveSettingData
    {
        public float LaneTransparent { get; set; } = 1f;

        public bool UseAllPerfectEffect { get; set; } = true;

        public bool UseSimultaneousPushingLine { get; set; } = true;

        public bool UseVibration { get; set; } = false;

        public bool Use120FPS { get; set; } = true;

        public bool? UseVSync { get; set; } = true;

        public float NoteSpeed { get; set; } = 10f;

        public float TimingAdjustData { get; set; }

        public float Brightness { get; set; } = 1f;
    }
}
