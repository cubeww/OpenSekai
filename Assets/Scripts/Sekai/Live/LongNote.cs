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
