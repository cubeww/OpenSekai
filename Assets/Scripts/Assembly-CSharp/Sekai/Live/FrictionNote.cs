namespace Sekai.Live
{
	public class FrictionNote : ConnectionNote
	{
		protected bool isTouched;

		public override NoteState State
		{
			get
			{
				return state;
			}
			protected set
			{
				if (value == NoteState.Done && State != NoteState.Done)
				{
					parentNote?.ForceTerminate();
					state = NoteState.Done;
					OnUnSpawnNote();
					return;
				}

				state = value;
				if (value == NoteState.Done)
				{
					OnUnSpawnNote();
				}
				else if (value == NoteState.Playing)
				{
					OnSpawnNote();
				}
			}
		}

		protected override void OnUnSpawnNote()
		{
			onJudgment?.Invoke(this);
			onUpdateCombo?.Invoke(this);
			onUpdateScore?.Invoke(this);
			onDamage?.Invoke(this);
			onUnspawn?.Invoke(this);
		}

		public FrictionNote()
		{
			Category = NoteCategory.Friction;
		}

		public FrictionNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, NoteType type, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, type, speedRatio, LiveUtility.GetLaneOffset(category, bundleBuildData), lineType)
		{
			isTouched = false;
		}

		public override void SetParentNote(LongNote note)
		{
			base.SetParentNote(note);
			Type = (note != null && note.Type == NoteType.Critical) || Type == NoteType.Critical
				? NoteType.Critical
				: NoteType.Default;
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

			var progress = CalcProgress(currentFrameInfo, offsetTime);
			if (progress < 0f && state != NoteState.Playing)
			{
				return;
			}

			Progress = progress;
			if ((State == NoteState.Input || isTouched) && Progress >= 1f)
			{
				OffsetJudgeTime = 0f;
				Progress = 1f;
				JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
				State = NoteState.Done;
				return;
			}

			if (OffsetJudgeTime > LiveConfig.noteTypeJudgeData.JudgeTimeAfter)
			{
				State = NoteState.Last;
			}
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			return State != NoteState.Done
				&& JudgeLaneStart <= lane
				&& JudgeLaneEnd >= lane
				&& LiveUtility.IsJudgmentTiming(LiveUtility.GetINoteToJudgeFrameType(this), OffsetJudgeTime);
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
				State = NoteState.Input;
				return false;
			}

			JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
			State = NoteState.Done;
			return true;
		}

		public override void ResetNote()
		{
			base.ResetNote();
			isTouched = false;
		}
	}
}
