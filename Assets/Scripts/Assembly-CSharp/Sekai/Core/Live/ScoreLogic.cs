using Sekai.Live;

namespace Sekai.Core.Live
{
	public class ScoreLogic
	{
		private readonly LiveBundleBuildData liveBundleBuildData;
		private MasterPlayLevelScore scoreInfo;

		public LiveScore score;

		public int BaseNoteScore { get; set; }

		public bool IsPerfectCombo => score.badCount == 0 && score.missCount == 0 && score.goodCount == 0;

		public bool IsAllPerfectCombo => score.IsAllPerfect;

		public ScoreLogic(LiveBundleBuildData data)
		{
			liveBundleBuildData = data;
			score.life = 1000;
			score.rank = ScoreRank.D;
		}

		public virtual void Setup(LiveBootDataBase bootData, MusicScore musicScore)
		{
			score = default;
			scoreInfo = bootData?.MusicData?.Score;
			int totalCombo = LiveUtility.CalculateTotalComboCount(musicScore);
			if (totalCombo <= 0)
			{
				totalCombo = bootData?.MusicData?.TotalNoteCount ?? 0;
			}
			score.totalComboCount = totalCombo;
			score.life = liveBundleBuildData != null && liveBundleBuildData.Life > 0 ? liveBundleBuildData.Life : 1000;
			score.rank = ScoreRank.D;
			BaseNoteScore = totalCombo > 0 ? UnityEngine.Mathf.Max(1, 1000000 / totalCombo) : 0;
		}

		public virtual void ExcuteEvent(EventBase eventBase)
		{
		}

		public virtual void UpdateCombo(NoteBase note)
		{
			if (note == null || note.Result == NoteResult.None)
			{
				return;
			}

			score.combo = note.Result < NoteResult.Great ? 0 : score.combo + 1;
			if (score.combo > score.maxCombo)
			{
				score.maxCombo = score.combo;
			}
		}

		public virtual void UpdateNoteResult(NoteBase note)
		{
			switch (note?.Result)
			{
				case NoteResult.JustPerfect:
					score.justPerfectCount++;
					score.perfectCount++;
					break;
				case NoteResult.Perfect:
					score.perfectCount++;
					break;
				case NoteResult.Great:
					score.greatCount++;
					break;
				case NoteResult.Good:
					score.goodCount++;
					break;
				case NoteResult.Auto:
					score.autoCount++;
					break;
				case NoteResult.Bad:
					score.badCount++;
					break;
				case NoteResult.Miss:
					score.missCount++;
					break;
			}

			if (note != null && note.Description == NoteResultDescription.Fast)
			{
				score.fastCount++;
			}
			else if (note != null && note.Description == NoteResultDescription.Late)
			{
				score.lateCount++;
			}
		}

		public virtual int CalculateAddScore(NoteBase note, float factor = 1f)
		{
			if (note == null)
			{
				return 0;
			}

			int addScore = note.Result switch
			{
				NoteResult.JustPerfect => BaseNoteScore,
				NoteResult.Perfect => UnityEngine.Mathf.RoundToInt(BaseNoteScore * 0.9f),
				NoteResult.Great => UnityEngine.Mathf.RoundToInt(BaseNoteScore * 0.7f),
				NoteResult.Good => UnityEngine.Mathf.RoundToInt(BaseNoteScore * 0.5f),
				NoteResult.Auto => BaseNoteScore,
				NoteResult.Pass => 0,
				_ => 0
			};
			int actualAddScore = UnityEngine.Mathf.RoundToInt(addScore * factor);
			score.totalScore += actualAddScore;
			UpdateScoreRank();
			return actualAddScore;
		}

		public virtual void Damage(NoteBase note)
		{
			if (note == null)
			{
				return;
			}

			if (LiveConfig.Damages.TryGetValue(note.Result, out int damage))
			{
				score.life = UnityEngine.Mathf.Max(0, score.life - damage);
			}
		}

		public virtual void UpdateScoreRank()
		{
			if (scoreInfo != null && (scoreInfo.s > 0 || scoreInfo.a > 0 || scoreInfo.b > 0 || scoreInfo.c > 0))
			{
				score.rank = ScoreGaugeCalculator.GetScoreRank(scoreInfo, score.totalScore);
				return;
			}

			score.rank = score.totalScore >= 900000 ? ScoreRank.S
				: score.totalScore >= 800000 ? ScoreRank.A
				: score.totalScore >= 700000 ? ScoreRank.B
				: score.totalScore >= 600000 ? ScoreRank.C
				: ScoreRank.D;
		}
	}
}
