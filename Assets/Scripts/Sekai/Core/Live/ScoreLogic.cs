using System;
using System.Linq;
using Sekai.Live;
using UnityEngine;

namespace Sekai.Core.Live
{
    public class ScoreLogic
    {
        private const int DefaultTargetScorePerNote = 1000;

        private LiveBootDataBase bootData;
        protected LiveViewBase[] liveViews;
        protected LiveScore score;
        protected LiveBundleBuildData liveBundleBuildData;
        private MasterPlayLevelScore scoreInfo;
        private float totalScoreF;
        private float baseNoteScore;
        private float? continueTime;

        public LiveScore Score
        {
            get { return score; }
        }

        public float BaseNoteScore
        {
            get { return baseNoteScore; }
            private set { baseNoteScore = value; }
        }

        public bool IsAllPerfectCombo
        {
            get { return score.totalComboCount == score.perfectCount; }
        }

        public bool IsPerfectCombo
        {
            get { return score.totalComboCount == score.maxCombo; }
        }

        public ScoreLogic(LiveBundleBuildData data)
        {
            liveBundleBuildData = data;
        }

        public virtual void Setup(LiveBootDataBase bootData, NoteBase[] noteBases, EventBase[] eventBases, LiveViewBase[] liveViews)
        {
            this.bootData = bootData;
            this.liveViews = liveViews;
            scoreInfo = EnsureScoreInfo(bootData, noteBases);

            int totalNoteFactor = Mathf.Max(1, CountScoreNotes(noteBases));
            score = new LiveScore
            {
                life = liveBundleBuildData != null ? liveBundleBuildData.Life : LiveConfig.Life,
                rank = ScoreRank.D,
                totalComboCount = totalNoteFactor,
                baseTotalScore = GetBaseTotalScore(scoreInfo, totalNoteFactor)
            };
            BaseNoteScore = score.baseTotalScore / totalNoteFactor;
            totalScoreF = 0f;
            continueTime = null;

            liveViews.ChangeScoreCalculator(ScoreGaugeCalculator.Create(scoreInfo));
            liveViews.SetupScore(score);
            liveViews.UpdateLife(score.life);
        }

        public virtual void Continue(float time)
        {
            continueTime = time;
            score.life = liveBundleBuildData != null ? liveBundleBuildData.Life : LiveConfig.Life;
            liveViews.UpdateLife(score.life);
        }

        public virtual void ExcuteEvent(EventBase eventInfo)
        {
        }

        public virtual (NoteResult, NoteResultDescription) UpdateResult((NoteResult, NoteResultDescription) result)
        {
            return result;
        }

        public virtual void CalculateScore(INote note, bool dropOut = false)
        {
            float addScore = CalculateAddScore(note, dropOut);
            totalScoreF += addScore;
            score.totalScore = Mathf.FloorToInt(totalScoreF);
            int addScoreInt = Mathf.FloorToInt(addScore);
            UpdateScoreRank();
            liveViews.UpdateScore(ref score, addScoreInt);
        }

        protected virtual float CalculateAddScore(INote note, bool dropOut = false)
        {
            if (note == null || note.Result <= NoteResult.Pass)
            {
                return 0f;
            }

            float resultFactor = GetResultScoreFactor(note.Result);
            if (resultFactor <= 0f)
            {
                return 0f;
            }

            float noteFactor = GetNoteScoreFactor(note);
            float comboFactor = GetComboScoreFactor(score.combo);
            float dropOutFactor = dropOut ? 0.1f : 1f;
            return Mathf.Max(BaseNoteScore * resultFactor * noteFactor * comboFactor * dropOutFactor, 1f);
        }

        protected virtual void UpdateScoreRank()
        {
            score.rank = ScoreGaugeCalculator.GetScoreRank(scoreInfo, score.totalScore);
        }

        public virtual void UpdateNoteResult(NoteBase note)
        {
            switch (note.Result)
            {
                case NoteResult.Miss:
                    score.missCount++;
                    break;
                case NoteResult.Bad:
                    score.badCount++;
                    break;
                case NoteResult.Auto:
                    score.autoCount++;
                    break;
                case NoteResult.Good:
                    score.goodCount++;
                    break;
                case NoteResult.Great:
                    score.greatCount++;
                    break;
                case NoteResult.JustPerfect:
                    score.justPerfectCount++;
                    score.perfectCount++;
                    break;
                case NoteResult.Perfect:
                    score.perfectCount++;
                    break;
            }

            if (note.Result >= NoteResult.Bad && note.Result <= NoteResult.Great)
            {
                switch (note.Description)
                {
                    case NoteResultDescription.FlickMiss:
                        score.flickCount++;
                        break;
                    case NoteResultDescription.Late:
                        score.lateCount++;
                        break;
                    case NoteResultDescription.Fast:
                        score.fastCount++;
                        break;
                }
            }

            liveViews.JudgmentNote(note);
        }

        public virtual void UpdateCombo(NoteBase note)
        {
            if (note.Result == NoteResult.None || note.Result == NoteResult.Pass)
            {
                return;
            }

            score.combo = note.Result < NoteResult.Great ? 0 : score.combo + 1;
            score.maxCombo = Math.Max(score.maxCombo, score.combo);
            liveViews.UpdateCombo(score);
        }

        public virtual void LifeRecovery(int recoveryValue)
        {
            if (bootData != null && bootData.IsAuto)
            {
                return;
            }
            if (score.life >= 1)
            {
                int maxLife = liveBundleBuildData != null ? liveBundleBuildData.MaxLife : LiveConfig.Life;
                score.life = Mathf.Min(score.life + recoveryValue, maxLife);
            }
            liveViews.UpdateLife(score.life);
        }

        public virtual void Damage(INote noteInfo)
        {
            int damage = GetDamage(noteInfo);
            if (!continueTime.HasValue)
            {
                if (damage < 1)
                {
                    return;
                }
            }
            else if (damage < 1 || noteInfo.MusicScoreInfo.time + noteInfo.OffsetJudgeTime < continueTime.Value + LiveConfig.ContinueNoDamageTime)
            {
                return;
            }

            score.life = Mathf.Max(0, score.life - damage);
            liveViews.UpdateLife(score.life);
        }

        private static int CountScoreNotes(NoteBase[] noteBases)
        {
            if (noteBases == null || noteBases.Length == 0)
            {
                return 1;
            }

            return noteBases
                .Where(note => note != null)
                .SelectMany(note => note.NoteList ?? Enumerable.Empty<NoteBase>())
                .Count(note => note != null && note.HasJudgment && note.Category != NoteCategory.Hidden && note.Category != NoteCategory.Skip);
        }

        private static MasterPlayLevelScore EnsureScoreInfo(LiveBootDataBase bootData, NoteBase[] noteBases)
        {
            LiveMusicData musicData = bootData != null ? bootData.MusicData : null;
            if (musicData != null &&
                musicData.Score != null &&
                musicData.Score.s > 0)
            {
                return musicData.Score;
            }

            int noteCount = CountScoreNotes(noteBases);
            int targetScore = Mathf.Max(1, noteCount * DefaultTargetScorePerNote);
            int playLevel = musicData != null && musicData.Difficulty != null ? musicData.Difficulty.playLevel : 0;
            MasterPlayLevelScore score = new MasterPlayLevelScore
            {
                liveType = "free",
                playLevel = playLevel,
                s = Mathf.FloorToInt(targetScore * ScoreGaugeCalculator.RankRateS),
                a = Mathf.FloorToInt(targetScore * ScoreGaugeCalculator.RankRateA),
                b = Mathf.FloorToInt(targetScore * ScoreGaugeCalculator.RankRateB),
                c = Mathf.FloorToInt(targetScore * ScoreGaugeCalculator.RankRateC)
            };
            if (musicData != null)
            {
                musicData.Score = score;
            }
            return score;
        }

        private static float GetBaseTotalScore(MasterPlayLevelScore scoreInfo, int totalNoteFactor)
        {
            if (scoreInfo != null && scoreInfo.s > 0)
            {
                return Mathf.Max(1, scoreInfo.s / ScoreGaugeCalculator.RankRateS);
            }

            return Mathf.Max(1, totalNoteFactor * DefaultTargetScorePerNote);
        }

        private static float GetResultScoreFactor(NoteResult result)
        {
            switch (result)
            {
                case NoteResult.JustPerfect:
                case NoteResult.Perfect:
                    return 1f;
                case NoteResult.Auto:
                    return 1f;
                case NoteResult.Great:
                    return 0.8f;
                case NoteResult.Good:
                    return 0.5f;
                case NoteResult.Bad:
                    return 0.1f;
                default:
                    return 0f;
            }
        }

        private static float GetNoteScoreFactor(INote note)
        {
            if (note == null)
            {
                return 1f;
            }

            float factor = 1f;
            if (note.Type == NoteType.Critical)
            {
                factor *= 2f;
            }
            if (note.Category == NoteCategory.Combo)
            {
                factor *= 0.1f;
            }
            return factor;
        }

        private static float GetComboScoreFactor(int combo)
        {
            if (combo >= 1000)
            {
                return 1.1f;
            }
            if (combo >= 500)
            {
                return 1.08f;
            }
            if (combo >= 100)
            {
                return 1.05f;
            }
            if (combo >= 50)
            {
                return 1.02f;
            }
            return 1f;
        }

        private static int GetDamage(INote note)
        {
            if (note == null)
            {
                return 0;
            }

            switch (note.Result)
            {
                case NoteResult.Miss:
                    return 80;
                case NoteResult.Bad:
                    return 50;
                default:
                    return 0;
            }
        }
    }
}
