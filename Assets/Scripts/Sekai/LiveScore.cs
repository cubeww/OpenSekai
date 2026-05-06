namespace Sekai.Core.Live
{
    public struct LiveScore
    {
        public int life;
        public int combo;
        public int maxCombo;
        public int fastCount;
        public int lateCount;
        public int flickCount;
        public int totalComboCount;
        public ScoreRank rank;
        public int totalScore;
        public float baseTotalScore;
        public int justPerfectCount;
        public int perfectCount;
        public int greatCount;
        public int goodCount;
        public int autoCount;
        public int badCount;
        public int missCount;
        public int feverCount;
        public int totalFeverCount;
        public bool joinFever;
        public bool ignoreFeverPerfomance;

        public bool IsAllPerfect
        {
            get { return badCount + missCount + autoCount + goodCount + greatCount < 1; }
        }
    }
}
