namespace Sekai.Live
{
	public class FrictionHideLongNote : FrictionLongNote
	{
		public override bool HasJudgment
		{
			get { return false; }
		}

		public override NoteState State
		{
			get { return state; }
			protected set
			{
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

		public FrictionHideLongNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, LiveBundleBuildData bundleBuildData, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, type, bundleBuildData, speedRatio, lineType)
		{
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			this.offsetTime = offsetTime;
			if (State == NoteState.Wait)
			{
				State = NoteState.Playing;
			}
			if (State == NoteState.Last)
			{
				JudgeInfo = (NoteResult.Pass, NoteResultDescription.None);
				State = NoteState.Release;
			}

			OffsetJudgeTime = currentFrameInfo.time - MusicScoreInfo.time;
			Progress = CalcProgress(currentFrameInfo, offsetTime);
			ExecuteSubNotes(currentFrameInfo);
			UpdateExecutingLane(currentFrameInfo);

			if (State == NoteState.Playing && LiveConfig.JudgeTime < currentFrameInfo.time - MusicScoreInfo.time)
			{
				State = NoteState.Last;
				return;
			}
			if (Result == NoteResult.None && Description == NoteResultDescription.None && state <= NoteState.Last && isTouched)
			{
				if (Progress < 1f)
				{
					return;
				}
				JudgeInfo = (NoteResult.Pass, NoteResultDescription.None);
				State = NoteState.InputBegan;
			}
			if ((State == NoteState.InputBegan || State == NoteState.Input) && lastInputFrame + LiveConfig.LiveMasterData.MissMesh < UnityEngine.Time.frameCount)
			{
				State = NoteState.Release;
			}
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			return base.Judgment(ref touch, lane);
		}

		protected override void OnSpawnNote()
		{
			onSpawn?.Invoke(this);
		}

		protected override void OnUnSpawnNote()
		{
			ConnectionNoteTerminate();
			onUnspawn?.Invoke(this);
		}
	}
}
