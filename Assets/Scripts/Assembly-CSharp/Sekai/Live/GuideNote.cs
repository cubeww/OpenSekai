namespace Sekai.Live
{
	public class GuideNote : LongNote
	{
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

		public GuideNote()
		{
			Category = NoteCategory.Guide;
		}

		public GuideNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, LiveBundleBuildData bundleBuildData, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, type, speedRatio, LiveUtility.GetLaneOffset(category, bundleBuildData), lineType)
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
			}

			Progress = CalcProgress(currentFrameInfo, offsetTime);
			if (NoteList != null)
			{
				for (var i = 1; i < NoteList.Count; i++)
				{
					var note = NoteList[i];
					var childOffsetTime = CalcTimeOffset?.Invoke(note) ?? offsetTime;
					if (currentFrameInfo.time > note.MusicScoreInfo.time - childOffsetTime)
					{
						note.Excute(currentFrameInfo, childOffsetTime);
					}
				}
			}

			var currentNote = (INote)this;
			var nextNote = (INote)childNote;
			LiveUtility.CalcExcuteNoteLane(this, ref currentNote, ref nextNote, ref currentFrameInfo, MusicScoreInfo, childNote);

			if (State == NoteState.Playing && LiveConfig.JudgeTime < currentFrameInfo.time - MusicScoreInfo.time)
			{
				State = NoteState.Last;
			}
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			return false;
		}

		public override bool IsJudgmentChild(ref LiveTouch touch, float lane)
		{
			return false;
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			return false;
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

		public override bool AutoJudgment(MusicScoreInfo currentFrameInfo)
		{
			return false;
		}
	}
}
