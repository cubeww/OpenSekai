using System;
using System.Linq;
using UnityEngine;

namespace Sekai.Live
{
	public class LongNote : NoteBase
	{
		private readonly NoteLineType lineType;

		protected int lastInputFrame;

		public float LaneOffset { get; set; }

		public float JudgedLaneEnd
		{
			get { return LaneEndF + LaneOffset; }
		}

		public float JudgedLaneStart
		{
			get { return LaneStartF - LaneOffset; }
		}

		public override NoteLineType LineType
		{
			get { return lineType; }
		}

		public override int LaneStart
		{
			get { return Mathf.RoundToInt(LaneStartF); }
			set { LaneStartF = value; }
		}

		public override int LaneEnd
		{
			get { return Mathf.RoundToInt(LaneEndF); }
			set { LaneEndF = value; }
		}

		public override NoteState State
		{
			get { return base.State; }
			protected set
			{
				NoteState current = State;
				if ((current == NoteState.Playing && value == NoteState.InputBegan) ||
					(current == NoteState.Last && value == NoteState.InputBegan))
				{
					onJudgment?.Invoke(this);
					onUpdateCombo?.Invoke(this);
					onUpdateScore?.Invoke(this);
					onDamage?.Invoke(this);
				}

				base.State = value;
			}
		}

		public override Action<NoteBase> OnUpdateScore
		{
			set
			{
				base.OnUpdateScore = value;
				ForEachSubNote(note => note.OnUpdateScore = value);
			}
		}

		public override Action<NoteBase> OnUpdateCombo
		{
			set
			{
				base.OnUpdateCombo = value;
				ForEachSubNote(note => note.OnUpdateCombo = value);
			}
		}

		public override Action<NoteBase> OnDamage
		{
			set
			{
				base.OnDamage = value;
				ForEachSubNote(note => note.OnDamage = value);
			}
		}

		public override Action<NoteBase> OnSpawn
		{
			set
			{
				base.OnSpawn = value;
				ForEachSubNote(note => note.OnSpawn = value);
			}
		}

		public override Action<NoteBase> OnJudgment
		{
			set
			{
				base.OnJudgment = value;
				ForEachSubNote(note => note.OnJudgment = value);
			}
		}

		public override Action<NoteBase> OnUnspawn
		{
			set
			{
				base.OnUnspawn = value;
				ForEachSubNote(note => note.OnUnspawn = value);
			}
		}

		public override Func<(NoteResult, NoteResultDescription), (NoteResult, NoteResultDescription)> OnUpdateResult
		{
			set
			{
				base.OnUpdateResult = value;
				ForEachSubNote(note => note.OnUpdateResult = value);
			}
		}

		public LongNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, LiveBundleBuildData bundleBuildData, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, speedRatio, type)
		{
			this.lineType = lineType;
			LaneOffset = LiveUtility.GetLaneOffset(category, bundleBuildData);
		}

		public void AddConnectionNote(NoteBase connectionNote)
		{
			connectionNote.speedRatio = speedRatio;
			NoteList.Add(connectionNote);
			ViewNoteList.Add(connectionNote);
			connectionNote.SetParentNote(this);
		}

		public void AddHoldCombo(LongHoldCombo longHoldCombo)
		{
			longHoldCombo.speedRatio = speedRatio;
			NoteList.Add(longHoldCombo);
			longHoldCombo.SetParentNote(this);
		}

		public override void SetChildNote(NoteBase note)
		{
			note.speedRatio = speedRatio;
			NoteList = NoteList.OrderBy(x => x.MusicScoreInfo.time).ToList();
			ViewNoteList = ViewNoteList.OrderBy(x => x.MusicScoreInfo.time).ToList();
			base.SetChildNote(note);
			NoteList.Add(note);
			ViewNoteList.Add(note);
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
				JudgeInfo = (NoteResult.Miss, NoteResultDescription.None);
				State = NoteState.Release;
				onDamage?.Invoke(this);
				onUpdateCombo?.Invoke(this);
				onJudgment?.Invoke(this);
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
			if ((State == NoteState.InputBegan || State == NoteState.Input) && lastInputFrame + LiveConfig.LiveMasterData.MissMesh < Time.frameCount)
			{
				State = NoteState.Release;
			}
		}

		public virtual void ForceTerminate()
		{
			if (State == NoteState.Done)
			{
				return;
			}

			JudgeInfo = IsChildSuccessful()
				? (NoteResult.JustPerfect, NoteResultDescription.None)
				: (NoteResult.Miss, NoteResultDescription.None);
			State = NoteState.Done;
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

			if (State == NoteState.Playing || State == NoteState.Last)
			{
				if (Result == NoteResult.None)
				{
					JudgeInfo = (NoteResult.JustPerfect, NoteResultDescription.None);
				}
				State = NoteState.InputBegan;
			}
			else
			{
				State = State == NoteState.Release ? NoteState.InputBegan : NoteState.Input;
			}

			lastInputFrame = Time.frameCount;
			OffsetJudgeTime = 0f;
			ConnectionNoteJudgment(ref touch, lane);
			if (childNote != null &&
				childNote.HasJudgment &&
				LiveConfig.IsLongEndJudgeTime(childNote.OffsetJudgeTime) &&
				childNote.IsJudgment(ref touch, lane) &&
				childNote.Judgment(ref touch, lane))
			{
				State = NoteState.Done;
			}
			return true;
		}

		protected void ConnectionNoteJudgment(ref LiveTouch touch, float lane)
		{
			if (State != NoteState.InputBegan && State != NoteState.Input && State != NoteState.Release)
			{
				return;
			}

			for (int i = 1; i < NoteList.Count - 1; i++)
			{
				NoteList[i].Judgment(ref touch, lane);
			}
		}

		protected void ConnectionNoteTerminate()
		{
			for (int i = 1; i < NoteList.Count - 1; i++)
			{
				NoteList[i].Terminate();
			}
		}

		public override float LaneDistance(ref MusicScoreInfo currentFrameInfo, float inputLane)
		{
			if (childNote != null && LiveConfig.IsLongEndJudgeTime(childNote.MusicScoreInfo.time - currentFrameInfo.time))
			{
				return childNote.LaneDistance(ref currentFrameInfo, inputLane);
			}

			return base.LaneDistance(ref currentFrameInfo, inputLane);
		}

		public override bool IsJudgment(ref LiveTouch touch, float lane)
		{
			if (State == NoteState.Done)
			{
				return false;
			}

			float laneStart = Result != NoteResult.None || Description != NoteResultDescription.None ? JudgedLaneStart : JudgeLaneStart;
			float laneEnd = Result != NoteResult.None || Description != NoteResultDescription.None ? JudgedLaneEnd : JudgeLaneEnd;
			if (laneStart > lane || laneEnd < lane)
			{
				return false;
			}

			return IsJudgmentTime() && (State != NoteState.Playing && State != NoteState.Last || touch.phase == TouchPhase.Began);
		}

		public virtual bool IsJudgmentChild(ref LiveTouch touch, float lane)
		{
			for (int i = 1; i < NoteList.Count - 1; i++)
			{
				if (NoteList[i].IsJudgment(ref touch, lane))
				{
					return true;
				}
			}

			return false;
		}

		public override bool AutoJudgment(MusicScoreInfo currentFrameInfo)
		{
			if (State == NoteState.Done || MusicScoreInfo.time > currentFrameInfo.time)
			{
				return false;
			}

			lastInputFrame = Time.frameCount;
			JudgeInfo = (NoteResult.Auto, NoteResultDescription.None);
			State = State == NoteState.Playing || State == NoteState.Last || State == NoteState.Release
				? NoteState.InputBegan
				: NoteState.Input;
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

			for (int i = 1; i < NoteList.Count - 1; i++)
			{
				NoteList[i].AutoJudgment(currentFrameInfo);
			}
		}

		protected void ExecuteSubNotes(MusicScoreInfo currentFrameInfo)
		{
			for (int i = 1; i < NoteList.Count; i++)
			{
				NoteBase note = NoteList[i];
				float timeOffset = CalcTimeOffset != null ? CalcTimeOffset(note) : offsetTime;
				if (currentFrameInfo.time <= note.MusicScoreInfo.time - timeOffset)
				{
					break;
				}
				note.Excute(currentFrameInfo, timeOffset);
			}
		}

		protected void UpdateExecutingLane(MusicScoreInfo currentFrameInfo)
		{
			if (childNote == null)
			{
				return;
			}

			INote currentNote = this;
			INote nextNote = childNote;
			for (int i = 1; i < ViewNoteList.Count - 1; i++)
			{
				INote note = ViewNoteList[i];
				if (note.IsSkip)
				{
					continue;
				}
				if (note.State == NoteState.Done)
				{
					currentNote = note;
					continue;
				}

				nextNote = note;
				break;
			}

			MusicScoreInfo startInfo = MusicScoreInfo;
			LiveUtility.CalcExcuteNoteLane(this, ref currentNote, ref nextNote, ref currentFrameInfo, startInfo, childNote);
		}

		private bool IsChildSuccessful()
		{
			INote child = ChildNote;
			return child != null && (child.Result > NoteResult.Pass || child.State == NoteState.Input);
		}

		private void ForEachSubNote(Action<NoteBase> action)
		{
			if (NoteList == null)
			{
				return;
			}
			for (int i = 1; i < NoteList.Count; i++)
			{
				action(NoteList[i]);
			}
		}
	}
}
