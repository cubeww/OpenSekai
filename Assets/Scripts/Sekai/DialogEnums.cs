namespace Sekai
{
    public enum DialogSize
    {
        Small = 0,
        Medium = 1,
        Large = 2,
        Manual = 3
    }

    public enum DialogState
    {
        Instantiated = 0,
        Initialized = 1,
        PlayOpenAnimation = 2,
        Show = 3,
        PlayCloseAnimation = 4,
        Closed = 5
    }

    public enum DialogType
    {
        Common1ButtonDialog = 0,
        Common2ButtonDialog = 2,
        LivePauseDialog = 18,
        ConsecutiveAutoLivePauseDialog = 179
    }

    public enum ScreenLayerType
    {
        Back = 0,
        Main = 1,
        Front = 2,
        Dialog = 4
    }
}
