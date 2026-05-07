using Sekai.Live;

namespace Sekai.Core.Live
{
    public static class LiveViewExt
    {
        public static void SetupScore(this LiveViewBase[] liveViews, LiveScore score)
        {
            ForEach(liveViews, view => view.SetupScore(score));
        }

        public static void ChangeScoreCalculator(this LiveViewBase[] liveViews, ScoreGaugeCalculator scoreGaugeCalculator)
        {
            ForEach(liveViews, view => view.ChangeScoreCalculator(scoreGaugeCalculator));
        }

        public static void JudgmentNote(this LiveViewBase[] liveViews, INote note)
        {
            ForEach(liveViews, view => view.JudgmentNote(note));
        }

        public static void Unpicked(this LiveViewBase[] liveViews, int lane, ref LiveTouch touch)
        {
            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                liveViews[i]?.Unpicked(lane, ref touch);
            }
        }

        public static void UpdateScore(this LiveViewBase[] liveViews, ref LiveScore score, int addScore)
        {
            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                liveViews[i]?.UpdateScore(ref score, addScore);
            }
        }

        public static void UpdateLife(this LiveViewBase[] liveViews, int life)
        {
            ForEach(liveViews, view => view.UpdateLife(life));
        }

        public static void UpdateCombo(this LiveViewBase[] liveViews, LiveScore score)
        {
            ForEach(liveViews, view => view.UpdateCombo(score));
        }

        public static void Result(this LiveViewBase[] liveViews, LiveResultAnimationType animationType)
        {
            ForEach(liveViews, view => view.Result(animationType));
        }

        public static void Finish(this LiveViewBase[] liveViews)
        {
            ForEach(liveViews, view => view.Finish());
        }

        private static void ForEach(LiveViewBase[] liveViews, System.Action<LiveViewBase> action)
        {
            if (liveViews == null || action == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    action(liveViews[i]);
                }
            }
        }
    }
}
