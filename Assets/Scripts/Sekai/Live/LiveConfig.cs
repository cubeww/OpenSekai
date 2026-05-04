using UnityEngine;

namespace Sekai.Live
{
	public static class LiveConfig
	{
		public static readonly string ConfigBundleNamePath = "Live/Config/LiveBundleBuildData";

		public static float JudgeTime = 0.1f;

		public static float GetNoteLineParentProgress(float time, INote startNote, INote endNote)
		{
			float endTime = endNote.MusicScoreInfo.time;
			float startTime = startNote.MusicScoreInfo.time;
			float duration = endTime - startTime;
			float t = Mathf.Approximately(duration, 0f) ? 0f : 1f - Mathf.Clamp01((endTime - time) / duration);

			if (startNote.LineType == NoteLineType.EaseOut)
			{
				return t * t;
			}
			if (startNote.LineType == NoteLineType.EaseIn)
			{
				return 1f - (1f - t) * (1f - t);
			}
			return t;
		}
	}
}
