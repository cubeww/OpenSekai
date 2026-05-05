using System;
using System.Collections.Generic;

namespace Sekai.Live
{
	public class NoteBase : INote
	{
		protected LongNote parentNote;

		protected NoteBase childNote;

		protected Action<NoteBase> onUpdateScore;

		protected Action<NoteBase> onUpdateCombo;

		protected Action<NoteBase> onDamage;

		protected Action<NoteBase> onSpawn;

		protected Action<NoteBase> onJudgment;

		protected Action<NoteBase> onUnspawn;

		protected Func<(NoteResult, NoteResultDescription), (NoteResult, NoteResultDescription)> onUpdateResult;

		public Func<NoteBase, float> CalcTimeOffset;

		protected NoteState state;

		public virtual Action<NoteBase> OnUpdateScore
		{
			set { onUpdateScore = value; }
		}

		public virtual Action<NoteBase> OnUpdateCombo
		{
			set { onUpdateCombo = value; }
		}

		public virtual Action<NoteBase> OnDamage
		{
			set { onDamage = value; }
		}

		public virtual Action<NoteBase> OnJudgment
		{
			set { onJudgment = value; }
		}

		public virtual Func<(NoteResult, NoteResultDescription), (NoteResult, NoteResultDescription)> OnUpdateResult
		{
			set { onUpdateResult = value; }
		}

		public virtual Action<NoteBase> OnSpawn
		{
			set { onSpawn = value; }
		}

		public virtual Action<NoteBase> OnUnspawn
		{
			set { onUnspawn = value; }
		}

		public int DefaultLeftLane { get; private set; }

		public int DefaultRightLane { get; private set; }

		public virtual int LaneStart { get; set; }

		public virtual int LaneEnd { get; set; }

		public virtual float LaneStartF { get; set; }

		public virtual float LaneEndF { get; set; }

		public virtual float JudgeLaneStart { get; set; }

		public virtual float JudgeLaneEnd { get; set; }

		public MusicScoreInfo MusicScoreInfo { get; protected set; }

		public float OffsetJudgeTime { get; set; }

		public float Progress { get; protected set; }

		public virtual NoteState State
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

		public (NoteResult, NoteResultDescription) JudgeInfo { get; protected set; }

		public NoteResult Result
		{
			get { return JudgeInfo.Item1; }
		}

		public NoteResultDescription Description
		{
			get { return JudgeInfo.Item2; }
		}

		public virtual NoteCategory Category { get; protected set; }

		public NoteType Type { get; protected set; }

		public NoteDirection Direction { get; protected set; }

		public virtual NoteLineType LineType
		{
			get { return NoteLineType.Linear; }
		}

		public virtual NoteLongType LongType { get; set; }

		public INote ParentNote
		{
			get { return parentNote; }
		}

		public INote ChildNote
		{
			get { return childNote; }
		}

		public INote PairNote { get; private set; }

		public virtual bool HasJudgment
		{
			get { return true; }
		}

		public virtual bool IsFever { get; protected set; }

		public virtual bool IsSkip { get; protected set; }

		public virtual int Id { get; protected set; }

		public float speedRatio { get; set; }

		public float offsetTime { get; protected set; }

		public List<NoteBase> NoteList { get; protected set; }

		public List<INote> ViewNoteList { get; set; }

		public NoteBase(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, float speedRatio, NoteType type = NoteType.Default)
		{
			MusicScoreInfo = info;
			Id = id;
			DefaultLeftLane = laneStart;
			DefaultRightLane = laneEnd;
			LaneStart = laneStart;
			LaneEnd = laneEnd;
			LaneStartF = laneStart;
			LaneEndF = laneEnd;
			float laneOffset = LiveUtility.GetLaneOffset(category, bundleBuildData);
			JudgeLaneStart = laneStart - laneOffset;
			JudgeLaneEnd = laneEnd + laneOffset;
			State = NoteState.Wait;
			Category = category;
			Type = type;
			Direction = NoteDirection.Default;
			this.speedRatio = speedRatio;
			NoteList = new List<NoteBase> { this };
			ViewNoteList = new List<INote> { this };
			JudgeInfo = (NoteResult.None, NoteResultDescription.None);
		}

		public virtual void SetParentNote(LongNote note)
		{
			parentNote = note;
			if (parentNote != null && parentNote.Type == NoteType.Critical)
			{
				Type = NoteType.Critical;
			}
		}

		public virtual void SetChildNote(NoteBase note)
		{
			childNote = note;
		}

		public void SetPairNote(NoteBase note)
		{
			PairNote = note;
		}

		public virtual void SetFever(bool fever)
		{
			IsFever = fever;
		}

		public virtual void SetSkip(bool skip)
		{
			IsSkip = skip;
		}

		public virtual void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			this.offsetTime = offsetTime;
			if (State == NoteState.Done)
			{
				return;
			}
			if (State >= NoteState.Last)
			{
				JudgeInfo = (NoteResult.Miss, NoteResultDescription.None);
				State = NoteState.Done;
				return;
			}

			OffsetJudgeTime = currentFrameInfo.time - MusicScoreInfo.time;
			float progress = CalcProgress(currentFrameInfo, offsetTime);
			if (progress < 0f && state != NoteState.Playing)
			{
				return;
			}

			if (State == NoteState.Wait)
			{
				State = NoteState.Playing;
			}
			Progress = progress;
			if (OffsetJudgeTime > LiveConfig.LiveMasterData.JudgeTimeAfter)
			{
				State = NoteState.Last;
			}
		}

		protected float CalcProgress(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			if (offsetTime.Equals(0f))
			{
				return 1f;
			}
			return (currentFrameInfo.time + offsetTime - MusicScoreInfo.time) / offsetTime;
		}

		public virtual void Terminate()
		{
		}

		public virtual bool Judgment(ref LiveTouch touch, float lane)
		{
			return true;
		}

		protected virtual void OnSpawnNote()
		{
			onSpawn?.Invoke(this);
		}

		protected virtual void OnUnSpawnNote()
		{
			onJudgment?.Invoke(this);
			onUpdateCombo?.Invoke(this);
			onUpdateScore?.Invoke(this);
			onDamage?.Invoke(this);
			onUnspawn?.Invoke(this);
		}

		public virtual float LaneDistance(ref MusicScoreInfo currentFrameInfo, float inputLane)
		{
			float center = (LaneStartF + LaneEndF) * 0.5f;
			float diff = center - inputLane;
			return diff * diff;
		}

		public virtual NoteResult CalcNoteResult(float musicTime)
		{
			return NoteResult.None;
		}

		public virtual bool IsJudgment(ref LiveTouch touch, float lane)
		{
			return true;
		}

		public virtual bool IsJudgmentTime()
		{
			return OffsetJudgeTime >= -LiveConfig.LiveMasterData.JudgeTimeBefore && OffsetJudgeTime <= LiveConfig.LiveMasterData.JudgeTimeAfter;
		}

		public virtual bool AutoJudgment(MusicScoreInfo currentFrameInfo)
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
