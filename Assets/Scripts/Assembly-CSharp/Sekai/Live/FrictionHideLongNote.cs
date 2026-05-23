namespace Sekai.Live
{
	public class FrictionHideLongNote : FrictionLongNote
	{
		protected override NoteResult CompletedTouchResult => NoteResult.Pass;

		public override bool HasJudgment
		{
			get
			{
				return false;
			}
		}

		public override NoteState State
		{
			get
			{
				return state;
			}
			protected set
			{
				if (value == NoteState.Done)
				{
					WasHoldingWhenTerminated = state == NoteState.Playing
						|| state == NoteState.Last
						|| state == NoteState.InputBegan
						|| state == NoteState.Input;
					state = NoteState.Done;
					OnUnSpawnNote();
					return;
				}

				state = value;
				if (value == NoteState.Playing)
				{
					OnSpawnNote();
				}
			}
		}

		public FrictionHideLongNote()
		{
			Category = NoteCategory.FrictionHideLong;
		}

		public FrictionHideLongNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, LiveBundleBuildData bundleBuildData, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, type, bundleBuildData, speedRatio, lineType)
		{
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			base.Excute(currentFrameInfo, offsetTime);
			if (State == NoteState.Done)
			{
				return;
			}

			if (JudgeInfo.result == NoteResult.None && State <= NoteState.Last && isTouched)
			{
				if (Progress < 1f)
				{
					return;
				}

				JudgeInfo = (NoteResult.Pass, NoteResultDescription.None);
				State = NoteState.InputBegan;
			}
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			return base.Judgment(ref touch, lane);
		}

		protected override void OnSpawnNote()
		{
		}

		protected override void OnUnSpawnNote()
		{
			ConnectionNoteTerminate();
			onUnspawn?.Invoke(this);
		}
	}
}
