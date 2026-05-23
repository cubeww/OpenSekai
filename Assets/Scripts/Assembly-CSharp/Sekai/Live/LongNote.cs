using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sekai.Live
{
	public class LongNote : NoteBase
	{
		protected int lastInputFrame;

		private int checkJudgementMiddleNoteIndex;

		public float LaneOffset
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool WasHoldingWhenTerminated
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public float JudgedLaneEnd
		{
			get
			{
				return LaneEndF + LaneOffset;
			}
		}

		public float JudgedLaneStart
		{
			get
			{
				return LaneStartF - LaneOffset;
			}
		}

		public override int LaneStart
		{
			get
			{
				return (int)Math.Round(LaneStartF, MidpointRounding.ToEven);
			}
			set
			{
				base.LaneStart = value;
				LaneStartF = value;
			}
		}

		public override int LaneEnd
		{
			get
			{
				return (int)Math.Round(LaneEndF, MidpointRounding.ToEven);
			}
			set
			{
				base.LaneEnd = value;
				LaneEndF = value;
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
				if ((state == NoteState.Playing && value == NoteState.InputBegan)
					|| (state == NoteState.Last && value == NoteState.InputBegan))
				{
					onJudgment?.Invoke(this);
					onUpdateCombo?.Invoke(this);
					onUpdateScore?.Invoke(this);
					onDamage?.Invoke(this);
					state = value;
					return;
				}

				if (value == NoteState.Done)
				{
					WasHoldingWhenTerminated = state == NoteState.InputBegan || state == NoteState.Input;
					state = value;
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

		public override Action<NoteBase> OnUpdateScore
		{
			set
			{
				onUpdateScore = value;
				ApplyToChildNotes(note => note.OnUpdateScore = value);
			}
		}

		public override Action<NoteBase> OnUpdateCombo
		{
			set
			{
				onUpdateCombo = value;
				ApplyToChildNotes(note => note.OnUpdateCombo = value);
			}
		}

		public override Action<NoteBase> OnDamage
		{
			set
			{
				onDamage = value;
				ApplyToChildNotes(note => note.OnDamage = value);
			}
		}

		public override Action<NoteBase> OnSpawn
		{
			set
			{
				onSpawn = value;
				ApplyToChildNotes(note => note.OnSpawn = value);
			}
		}

		public override Action<NoteBase> OnJudgment
		{
			set
			{
				onJudgment = value;
				ApplyToChildNotes(note => note.OnJudgment = value);
			}
		}

		public override Action<NoteBase> OnUnspawn
		{
			set
			{
				onUnspawn = value;
				ApplyToChildNotes(note => note.OnUnspawn = value);
			}
		}

		public override Func<(NoteResult result, NoteResultDescription description), (NoteResult result, NoteResultDescription description)> OnUpdateResult
		{
			set
			{
				onUpdateResult = value;
				ApplyToChildNotes(note => note.OnUpdateResult = value);
			}
		}

		public LongNote()
		{
			NoteList = new List<NoteBase> { this };
			ViewNoteList = new List<INote> { this };
			Category = NoteCategory.Long;
			Type = NoteType.Default;
			checkJudgementMiddleNoteIndex = 1;
		}

		public LongNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, float speedRatio, float laneOffset, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, speedRatio, laneOffset, type)
		{
			LineType = lineType;
			checkJudgementMiddleNoteIndex = 1;
			LaneOffset = laneOffset;
		}

		public void AddConnectionNote(NoteBase connectionNote)
		{
			if (connectionNote == null)
			{
				return;
			}

			NoteList ??= new List<NoteBase> { this };
			ViewNoteList ??= new List<INote> { this };
			connectionNote.speedRatio = speedRatio;
			NoteList.Add(connectionNote);
			ViewNoteList.Add(connectionNote);
			connectionNote.SetParentNote(this);
		}

		public void AddHoldCombo(LongHoldCombo longHoldCombo)
		{
			AddConnectionNote(longHoldCombo);
		}

		public void AddNoteListAndSetupChildNote(NoteBase note)
		{
			if (note == null)
			{
				return;
			}

			note.speedRatio = speedRatio;
			childNote = note;
			NoteList ??= new List<NoteBase> { this };
			ViewNoteList ??= new List<INote> { this };
			NoteList.Add(note);
			ViewNoteList.Add(note);
			checkJudgementMiddleNoteIndex = 1;
			SortNoteList();
		}

		public override void Excute(MusicScoreInfo currentFrameInfo, float offsetTime)
		{
			this.offsetTime = offsetTime;
			if (State == NoteState.Done)
			{
				return;
			}

			if (State == NoteState.Wait)
			{
				State = NoteState.Playing;
			}

			if (State == NoteState.Last)
			{
				JudgeInfo = (NoteResult.Miss, NoteResultDescription.None);
				State = NoteState.Release;
				onDamage?.Invoke(this);
				onUpdateCombo?.Invoke(this);
				onJudgment?.Invoke(this);
			}

			Progress = CalcProgress(currentFrameInfo, offsetTime);

			if (NoteList != null)
			{
				for (var i = 1; i < NoteList.Count; i++)
				{
					var note = NoteList[i];
					var childOffsetTime = CalcTimeOffset?.Invoke(note) ?? offsetTime;
					if (currentFrameInfo.time <= note.MusicScoreInfo.time - childOffsetTime)
					{
						break;
					}

					note.Excute(currentFrameInfo, childOffsetTime);
				}
			}

			INote currentNote = this;
			INote nextNote = childNote;
			if (ViewNoteList != null)
			{
				for (var i = 1; i < ViewNoteList.Count - 1; i++)
				{
					var viewNote = ViewNoteList[i];
					if (viewNote == null || viewNote.IsSkip)
					{
						continue;
					}

					if (viewNote.State != NoteState.Done)
					{
						nextNote = viewNote;
						break;
					}

					currentNote = viewNote;
				}
			}

			LiveUtility.CalcExcuteNoteLane(this, ref currentNote, ref nextNote, ref currentFrameInfo, MusicScoreInfo, childNote);

			if (State == NoteState.Playing && currentFrameInfo.time - MusicScoreInfo.time > LiveConfig.JudgeTime)
			{
				State = NoteState.Last;
			}

			if ((State == NoteState.InputBegan || State == NoteState.Input)
				&& lastInputFrame + LiveConfig.noteTypeJudgeData.MissMesh < Time.frameCount)
			{
				State = NoteState.Release;
			}
		}

		public virtual void ForceTerminate()
		{
			if (State != NoteState.Done)
			{
				JudgeInfo = childNote != null
					&& (childNote.Result > NoteResult.Pass || childNote.State == NoteState.Input)
						? (NoteResult.JustPerfect, NoteResultDescription.None)
						: (NoteResult.Miss, NoteResultDescription.None);
				State = NoteState.Done;
			}
		}

		protected override void OnUnSpawnNote()
		{
			ConnectionNoteTerminate();
			onUnspawn?.Invoke(this);
		}

		public override bool Judgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Done)
			{
				return false;
			}

			if ((State == NoteState.Playing || State == NoteState.Last) && JudgeInfo.result == NoteResult.None)
			{
				var judgeInfo = LiveConfig.CalculateLongStartNoteResult(this, touch.musicTime);
				JudgeInfo = onUpdateResult != null ? onUpdateResult(judgeInfo) : judgeInfo;
				State = NoteState.InputBegan;
			}
			else
			{
				State = State == NoteState.Release ? NoteState.InputBegan : NoteState.Input;
			}

			lastInputFrame = Time.frameCount;
			OffsetJudgeTime = 0f;
			ConnectionNoteJudgment(ref touch, lane);

			if (childNote != null
				&& LiveConfig.IsLongEndJudgeTime(childNote.OffsetJudgeTime)
				&& childNote.IsJudgment(ref touch, lane)
				&& childNote.Judgment(ref touch, lane))
			{
				State = NoteState.Done;
			}

			return true;
		}

		protected void ConnectionNoteJudgment(ref LiveTouch touch, float lane)
		{
			if (NoteList == null)
			{
				return;
			}

			for (var i = checkJudgementMiddleNoteIndex; i < NoteList.Count - 1; i++)
			{
				var note = NoteList[i];
				if (note.State == NoteState.Done)
				{
					checkJudgementMiddleNoteIndex = i + 1;
					continue;
				}

				if (note.IsJudgment(ref touch, lane))
				{
					note.Judgment(ref touch, lane);
					checkJudgementMiddleNoteIndex = i + 1;
				}
			}
		}

		protected void ConnectionNoteTerminate()
		{
			if (NoteList == null)
			{
				return;
			}

			for (var i = 1; i < NoteList.Count - 1; i++)
			{
				var note = NoteList[i];
				if (note != null && note.State != NoteState.Done)
				{
					note.Terminate();
				}
			}
		}

		public override float LaneDistance(ref MusicScoreInfo currentFrameInfo, float inputLane)
		{
			var centerLane = (LaneStartF + LaneEndF) * 0.5f;
			var distance = centerLane - inputLane;
			return distance * distance;
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Done)
			{
				return false;
			}

			var left = JudgeInfo.result == NoteResult.None ? JudgeLaneStart : JudgedLaneStart;
			var right = JudgeInfo.result == NoteResult.None ? JudgeLaneEnd : JudgedLaneEnd;
			if (left > lane || right < lane)
			{
				return false;
			}

			return LiveUtility.IsJudgmentTiming(LiveUtility.GetINoteToJudgeFrameType(this), OffsetJudgeTime)
				&& ((State != NoteState.Playing && State != NoteState.Last) || touch.phase == UnityEngine.InputSystem.TouchPhase.Began);
		}

		public virtual bool IsJudgmentChild(ref LiveTouch touch, float lane)
		{
			return State != NoteState.Done && JudgedLaneStart <= lane && JudgedLaneEnd >= lane;
		}

		public override bool AutoJudgment(MusicScoreInfo currentFrameInfo)
		{
			if (State == NoteState.Done || MusicScoreInfo.time > currentFrameInfo.time)
			{
				return false;
			}

			lastInputFrame = Time.frameCount;
			JudgeInfo = (NoteResult.Auto, NoteResultDescription.None);
			State = State == NoteState.Playing || State == NoteState.Last || State == NoteState.Release ? NoteState.InputBegan : NoteState.Input;
			OffsetJudgeTime = 0f;
			ConnectionNoteAutoJudgment(currentFrameInfo);

			if (childNote != null && childNote.AutoJudgment(currentFrameInfo))
			{
				State = NoteState.Done;
			}

			return true;
		}

		private void ConnectionNoteAutoJudgment(MusicScoreInfo currentFrameInfo)
		{
			if (State != NoteState.InputBegan && State != NoteState.Input)
			{
				return;
			}

			if (NoteList == null)
			{
				return;
			}

			for (var i = checkJudgementMiddleNoteIndex; i < NoteList.Count - 1; i++)
			{
				if (NoteList[i].AutoJudgment(currentFrameInfo))
				{
					checkJudgementMiddleNoteIndex = i + 1;
				}
			}
		}

		public override NoteResult CalcNoteResult(float musicTime)
		{
			return LiveConfig.CalculateLongStartNoteResult(this, musicTime).Item1;
		}

		public override void ResetNote()
		{
			base.ResetNote();
			lastInputFrame = 0;
			checkJudgementMiddleNoteIndex = 1;
			WasHoldingWhenTerminated = false;
		}

		public void SortNoteList()
		{
			if (NoteList != null)
			{
				NoteList.Sort((a, b) => a.MusicScoreInfo.time.CompareTo(b.MusicScoreInfo.time));
			}

			if (ViewNoteList != null)
			{
				ViewNoteList = ViewNoteList.OrderBy(note => note.MusicScoreInfo.time).ToList();
			}
		}

		private void ApplyToChildNotes(Action<NoteBase> apply)
		{
			if (NoteList == null || apply == null)
			{
				return;
			}

			foreach (var note in NoteList)
			{
				if (!ReferenceEquals(note, this))
				{
					apply(note);
				}
			}
		}
	}
}
