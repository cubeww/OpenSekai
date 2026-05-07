namespace Sekai.Live
{
	public class FrictionNote : ConnectionNote
	{
		protected bool isTouched;

		public FrictionNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, NoteType type, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, type, speedRatio, lineType)
		{
			isTouched = false;
		}

		protected override void OnUnSpawnNote()
		{
			onJudgment?.Invoke(this);
			onUpdateCombo?.Invoke(this);
			onUpdateScore?.Invoke(this);
			onDamage?.Invoke(this);
			onUnspawn?.Invoke(this);
		}

		public override NoteState State
		{
			get { return base.State; }
			protected set
			{
				if (value == NoteState.Done && State != NoteState.Done && parentNote != null)
				{
					parentNote.ForceTerminate();
				}
				base.State = value;
			}
		}

		public override void SetParentNote(LongNote note)
		{
			parentNote = note;
			if (parentNote != null && (parentNote.Type == NoteType.Critical || Type == NoteType.Critical))
			{
				Type = NoteType.Critical;
			}
			Category = NoteCategory.FrictionLong;
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			this.offsetTime = offsetTime;
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

			OffsetJudgeTime = currentFrameInfo.time - MusicScoreInfo.time;
			if (State == NoteState.Wait)
			{
				State = NoteState.Playing;
			}

			float progress = CalcProgress(currentFrameInfo, offsetTime);
			if (progress < 0f && state != NoteState.Playing)
			{
				return;
			}

			Progress = progress;
			if (State == NoteState.Input || isTouched)
			{
				if (progress < 1f)
				{
					return;
				}

				OffsetJudgeTime = 0f;
				Progress = 1f;
				JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
				State = NoteState.Done;
			}
			if (OffsetJudgeTime > LiveConfig.LiveMasterData.JudgeTimeAfter)
			{
				State = NoteState.Last;
			}
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			return State != NoteState.Done &&
				JudgeLaneStart <= lane &&
				JudgeLaneEnd >= lane &&
				LiveUtility.IsJudgmentTiming(LiveUtility.GetINoteToJudgeFrameType(this), OffsetJudgeTime);
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Last)
			{
				OffsetJudgeTime = 0f;
				Progress = 1f;
			}
			else if (Progress < 1f)
			{
				isTouched = true;
				return false;
			}

			JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
			State = NoteState.Done;
			return true;
		}
	}
}
