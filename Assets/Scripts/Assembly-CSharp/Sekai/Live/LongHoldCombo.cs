namespace Sekai.Live
{
	public class LongHoldCombo : NoteBase
	{
		private readonly float laneOffset;

		public LongHoldCombo()
		{
			Category = NoteCategory.Combo;
		}

		public LongHoldCombo(MusicScoreInfo info, NoteType type, float speedRatio, float laneOffset)
			: base(info, 0, 0, 0, NoteCategory.Combo, speedRatio, laneOffset, type)
		{
			this.laneOffset = laneOffset;
		}

		public LongHoldCombo(MusicScoreInfo info, NoteType type, int id, float speedRatio, float laneOffset)
			: base(info, id, 0, 0, NoteCategory.Combo, speedRatio, laneOffset, type)
		{
			this.laneOffset = laneOffset;
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			if (State == NoteState.Done)
			{
				return;
			}

			if (State == NoteState.Last)
			{
				JudgeInfo = (NoteResult.Miss, NoteResultDescription.None);
				State = NoteState.Done;
				return;
			}

			if (MusicScoreInfo.time <= currentFrameInfo.time)
			{
				State = NoteState.Last;
			}
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			if (State != NoteState.Last || parentNote == null)
			{
				return false;
			}

			return parentNote.LaneStartF - laneOffset <= lane && parentNote.LaneEndF + laneOffset >= lane;
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Last)
			{
				JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
				State = NoteState.Done;
			}

			return true;
		}

		public override void Terminate()
		{
			if (State == NoteState.Done)
			{
				return;
			}

			JudgeInfo = parentNote != null && !parentNote.WasHoldingWhenTerminated
				? (NoteResult.Miss, NoteResultDescription.None)
				: (NoteResult.JustPerfect, NoteResultDescription.None);
			State = NoteState.Done;
		}

		protected override void OnSpawnNote()
		{
		}

		protected override void OnUnSpawnNote()
		{
			onUpdateScore?.Invoke(this);
			onUpdateCombo?.Invoke(this);
			onDamage?.Invoke(this);
			onJudgment?.Invoke(this);
		}

		public override bool AutoJudgment(MusicScoreInfo currentFrameInfo)
		{
			if (State == NoteState.Done || MusicScoreInfo.time > currentFrameInfo.time)
			{
				return false;
			}

			JudgeInfo = (NoteResult.Auto, NoteResultDescription.None);
			State = NoteState.Done;
			return true;
		}
	}
}
