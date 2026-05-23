using System.Collections.Generic;

namespace Sekai.Live
{
	public interface INote
	{
		MusicScoreInfo MusicScoreInfo { get; }

		float OffsetJudgeTime { get; }

		int DefaultLeftLane { get; }

		int DefaultRightLane { get; }

		int LaneStart { get; set; }

		int LaneEnd { get; set; }

		float LaneStartF { get; set; }

		float LaneEndF { get; set; }

		float JudgeLaneStart { get; set; }

		float JudgeLaneEnd { get; set; }

		float Progress { get; }

		NoteState State { get; }

		(NoteResult result, NoteResultDescription description) JudgeInfo { get; }

		NoteResult Result { get; }

		NoteCategory Category { get; }

		NoteType Type { get; }

		NoteDirection Direction { get; }

		NoteLineType LineType { get; }

		INote ParentNote { get; }

		INote ChildNote { get; }

		INote PairNote { get; }

		List<INote> ViewNoteList { get; set; }

		bool IsSkip { get; }

		int Id { get; }
	}
}
