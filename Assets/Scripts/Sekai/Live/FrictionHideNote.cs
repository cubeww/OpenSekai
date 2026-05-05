namespace Sekai.Live
{
	public class FrictionHideNote : FrictionNote
	{
		public override bool HasJudgment
		{
			get { return false; }
		}

		public FrictionHideNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, NoteType type, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, type, speedRatio, lineType)
		{
		}

		public override void SetParentNote(LongNote note)
		{
			parentNote = note;
			if (parentNote != null && (parentNote.Type == NoteType.Critical || Type == NoteType.Critical))
			{
				Type = NoteType.Critical;
			}
			Category = NoteCategory.FrictionHideLong;
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
				JudgeInfo = (NoteResult.Pass, NoteResultDescription.None);
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
				JudgeInfo = (NoteResult.Pass, NoteResultDescription.None);
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
				LiveConfig.IsLongEndJudgeTime(MusicScoreInfo.time - touch.musicTime);
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Last)
			{
				OffsetJudgeTime = 0f;
				Progress = 1f;
				JudgeInfo = (NoteResult.Pass, NoteResultDescription.None);
				State = NoteState.Done;
				return true;
			}

			isTouched = true;
			return false;
		}

		protected override void OnSpawnNote()
		{
		}

		protected override void OnUnSpawnNote()
		{
		}
	}
}
