using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Sekai.Live
{
	public class NoteBase : INote
	{
		protected Action<NoteBase> onUpdateScore;

		protected Action<NoteBase> onUpdateCombo;

		protected Action<NoteBase> onDamage;

		protected Action<NoteBase> onSpawn;

		protected Action<NoteBase> onJudgment;

		protected Action<NoteBase> onUnspawn;

		protected Func<(NoteResult result, NoteResultDescription description), (NoteResult result, NoteResultDescription description)> onUpdateResult;

		[JsonIgnore]
		public Func<NoteBase, float> CalcTimeOffset;

		protected NoteState state;

		protected LongNote parentNote;

		protected NoteBase childNote;

		public virtual Action<NoteBase> OnUpdateScore
		{
			set
			{
				onUpdateScore = value;
			}
		}

		public virtual Action<NoteBase> OnUpdateCombo
		{
			set
			{
				onUpdateCombo = value;
			}
		}

		public virtual Action<NoteBase> OnDamage
		{
			set
			{
				onDamage = value;
			}
		}

		public virtual Action<NoteBase> OnJudgment
		{
			set
			{
				onJudgment = value;
			}
		}

		public virtual Func<(NoteResult result, NoteResultDescription description), (NoteResult result, NoteResultDescription description)> OnUpdateResult
		{
			set
			{
				onUpdateResult = value;
			}
		}

		public virtual Action<NoteBase> OnSpawn
		{
			set
			{
				onSpawn = value;
			}
		}

		public virtual Action<NoteBase> OnUnspawn
		{
			set
			{
				onUnspawn = value;
			}
		}

		public int DefaultLeftLane
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public int DefaultRightLane
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public virtual int LaneStart
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public virtual int LaneEnd
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public virtual float LaneStartF
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public virtual float LaneEndF
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public virtual float JudgeLaneStart
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public virtual float JudgeLaneEnd
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public virtual bool FinishFirstAction
		{
			get
			{
				return true;
			}
		}

		public MusicScoreInfo MusicScoreInfo
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public float OffsetJudgeTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public float Progress
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			protected set;
		}

		[JsonIgnore]
		public virtual NoteState State
		{
			get
			{
				return state;
			}
			protected set
			{
				state = value;
				if (value == NoteState.Playing)
				{
					OnSpawnNote();
				}
				else if (value == NoteState.Done)
				{
					OnUnSpawnNote();
				}
			}
		}

		[JsonIgnore]
		public (NoteResult result, NoteResultDescription description) JudgeInfo
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			protected set;
		}

		[JsonIgnore]
		public NoteResult Result
		{
			get
			{
				return JudgeInfo.result;
			}
		}

		[JsonIgnore]
		public NoteResultDescription Description
		{
			get
			{
				return JudgeInfo.description;
			}
		}

		public virtual NoteCategory Category
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteType Type
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteDirection Direction
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteLineType LineType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public INote ParentNote
		{
			get
			{
				return parentNote;
			}
		}

		[JsonIgnore]
		public INote ChildNote
		{
			get
			{
				return childNote;
			}
		}

		[JsonIgnore]
		public INote PairNote
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		[JsonIgnore]
		public virtual bool HasJudgment
		{
			get
			{
				return true;
			}
		}

		[JsonIgnore]
		public virtual bool IsFever
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public virtual bool IsSkip
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public virtual int Id
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public float speedRatio
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public float offsetTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public List<NoteBase> NoteList
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[JsonIgnore]
		public List<INote> ViewNoteList
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteBase()
		{
		}

		public NoteBase(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, float speedRatio, float laneOffset, NoteType type = NoteType.Default)
		{
			MusicScoreInfo = info;
			Id = id;
			DefaultLeftLane = laneStart;
			DefaultRightLane = laneEnd;
			LaneStart = laneStart;
			LaneEnd = laneEnd;
			LaneStartF = laneStart;
			LaneEndF = laneEnd;
			JudgeLaneStart = laneStart - laneOffset;
			JudgeLaneEnd = laneEnd + laneOffset;
			State = NoteState.Wait;
			Category = category;
			Type = type;
			this.speedRatio = speedRatio;
			NoteList = new List<NoteBase> { this };
			ViewNoteList = new List<INote> { this };
		}

		public void SetStateUnnotice(NoteState state)
		{
			this.state = state;
		}

		public virtual void SetParentNote(LongNote note)
		{
			parentNote = note;
			if (note.Type == NoteType.Critical)
			{
				Type = note.Type;
			}
		}

		public void SetChildNote(NoteBase note)
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
			if (OffsetJudgeTime > LiveConfig.noteTypeJudgeData.JudgeTimeAfter)
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
			float centerLane = (LaneStartF + LaneEndF) * 0.5f;
			float distance = centerLane - inputLane;
			return distance * distance;
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
			return OffsetJudgeTime >= -LiveConfig.noteTypeJudgeData.JudgeTimeBefore
				&& OffsetJudgeTime <= LiveConfig.noteTypeJudgeData.JudgeTimeAfter;
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

		public virtual void ResetNote()
		{
			State = NoteState.Wait;
			OffsetJudgeTime = 0f;
			Progress = 0f;
			JudgeInfo = (NoteResult.None, NoteResultDescription.None);
		}
	}
}
