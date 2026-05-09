namespace Sekai
{
    public class ConsecutiveAutoLiveData
    {
        public enum RunMode
        {
            UntilNotEnough = 0,
            NumberOfPlay = 1
        }

        public int runMode;
        public int remainCount;

        public ConsecutiveAutoLiveData()
        {
        }

        public ConsecutiveAutoLiveData(RunMode mode, int remainCount)
        {
            runMode = (int)mode;
            this.remainCount = remainCount;
        }
    }
}
