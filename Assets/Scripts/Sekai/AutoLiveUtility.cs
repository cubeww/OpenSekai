namespace Sekai
{
    public static class AutoLiveUtility
    {
        private static ConsecutiveAutoLiveData consecutiveAutoLiveData;

        public static bool IsRunningConsecutiveAutoLive()
        {
            return consecutiveAutoLiveData != null;
        }

        public static bool IsCreatedConsecutiveAutoLiveData()
        {
            return consecutiveAutoLiveData != null;
        }

        public static ConsecutiveAutoLiveData LoadConsecutiveAutoLiveData()
        {
            return consecutiveAutoLiveData;
        }

        public static void SetConsecutiveAutoLiveData(ConsecutiveAutoLiveData data)
        {
            consecutiveAutoLiveData = data;
        }

        public static void ClearConsecutiveAutoLiveData()
        {
            consecutiveAutoLiveData = null;
        }

        public static int GetCurrentConsecutiveAutoLiveRemainCount()
        {
            return consecutiveAutoLiveData != null ? consecutiveAutoLiveData.remainCount : 0;
        }
    }
}
