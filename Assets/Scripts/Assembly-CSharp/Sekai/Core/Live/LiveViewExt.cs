using Sekai.Live;
using System.Collections.Generic;

namespace Sekai.Core.Live
{
	public static class LiveViewExt
	{
		public static void Setup(LiveViewBase[] views, BaseLiveController controller)
		{
			ForEach(views, view => view.Setup(controller));
		}

		public static void OnLoad(LiveViewBase[] views)
		{
			ForEach(views, view => view.OnLoad());
		}

		public static void OnUnload(LiveViewBase[] views)
		{
			ForEach(views, view => view.OnUnload());
		}

		public static void MusicStart(LiveViewBase[] views, float musicTime)
		{
			ForEach(views, view => view.MusicStart(musicTime));
		}

		public static void RhythmGameStart(LiveViewBase[] views)
		{
			ForEach(views, view => view.RhythmGameStart());
		}

		public static void OnUpdate(LiveViewBase[] views, float musicTime)
		{
			ForEach(views, view => view.OnUpdate(musicTime));
		}

		public static void CreateNotePool(LiveViewBase[] views, Dictionary<(NoteCategory, NoteType), int> notePoolCount)
		{
			ForEach(views, view => view.CreateNotePool(notePoolCount));
		}

		public static void SpawnNote(LiveViewBase[] views, NoteBase note)
		{
			ForEach(views, view => view.SpawnNote(note));
		}

		public static void UnspawnNote(LiveViewBase[] views, NoteBase note)
		{
			ForEach(views, view => view.UnspawnNote(note));
		}

		public static void JudgmentNote(LiveViewBase[] views, NoteBase note)
		{
			ForEach(views, view => view.JudgmentNote(note));
		}

		public static void InputLane(LiveViewBase[] views, int lane)
		{
			ForEach(views, view => view.InputLane(lane));
		}

		public static void Unpicked(LiveViewBase[] views, int lane, ref LiveTouch touch)
		{
			if (views == null)
			{
				return;
			}

			foreach (LiveViewBase view in views)
			{
				if (view != null)
				{
					view.Unpicked(lane, ref touch);
				}
			}
		}

		public static void UpdateCombo(LiveViewBase[] views, LiveScore score)
		{
			ForEach(views, view => view.UpdateCombo(score));
		}

		public static void SetupScore(LiveViewBase[] views, LiveScore score)
		{
			ForEach(views, view => view.SetupScore(score));
		}

		public static void UpdateScore(LiveViewBase[] views, LiveScore score)
		{
			UpdateScore(views, ref score, 0);
		}

		public static void UpdateScore(LiveViewBase[] views, ref LiveScore score, int addScore)
		{
			if (views == null)
			{
				return;
			}

			foreach (LiveViewBase view in views)
			{
				if (view != null)
				{
					view.UpdateScore(ref score, addScore);
				}
			}
		}

		public static void UpdateLife(LiveViewBase[] views, LiveScore score)
		{
			ForEach(views, view => view.UpdateLife(score));
		}

		public static void Result(LiveViewBase[] views, int result)
		{
			ForEach(views, view => view.Result(result));
		}

		public static void Pause(LiveViewBase[] views)
		{
			ForEach(views, view => view.Pause());
		}

		public static void Countdown(LiveViewBase[] views)
		{
			ForEach(views, view => view.Countdown());
		}

		public static void Resume(LiveViewBase[] views)
		{
			ForEach(views, view => view.Resume());
		}

		public static void Resume(LiveViewBase[] views, float musicTime)
		{
			ForEach(views, view => view.Resume(musicTime));
		}

		public static void Retry(LiveViewBase[] views)
		{
			ForEach(views, view => view.Retry());
		}

		public static void Finish(LiveViewBase[] views)
		{
			ForEach(views, view => view.Finish());
		}

		public static void Finish(LiveViewBase[] views, float duration)
		{
			ForEach(views, view => view.Finish(duration));
		}

		public static void Finish3D(LiveViewBase[] views)
		{
			ForEach(views, view => view.Finish3D());
		}

		private static void ForEach(LiveViewBase[] views, System.Action<LiveViewBase> action)
		{
			if (views == null || action == null)
			{
				return;
			}

			foreach (LiveViewBase view in views)
			{
				if (view != null)
				{
					action(view);
				}
			}
		}
	}
}
