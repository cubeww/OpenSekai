using Sekai.Live;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai.Core.Live
{
	public class LiveViewBase : MonoBehaviour
	{
		public virtual void Setup(BaseLiveController controller)
		{
		}

		public virtual void OnLoad()
		{
		}

		public virtual void OnUnload()
		{
		}

		public virtual void MusicStart(float musicTime)
		{
		}

		public virtual void RhythmGameStart()
		{
		}

		public virtual void OnUpdate(float musicTime)
		{
		}

		public virtual void CreateNotePool(Dictionary<(NoteCategory, NoteType), int> notePoolCount)
		{
		}

		public virtual void SpawnNote(NoteBase note)
		{
		}

		public virtual void UnspawnNote(NoteBase note)
		{
		}

		public virtual void JudgmentNote(NoteBase note)
		{
		}

		public virtual void InputLane(int lane)
		{
		}

		public virtual void Unpicked(int lane, ref LiveTouch touch)
		{
		}

		public virtual void UpdateCombo(LiveScore score)
		{
		}

		public virtual void SetupScore(LiveScore score)
		{
		}

		public virtual void UpdateScore(LiveScore score)
		{
		}

		public virtual void UpdateScore(ref LiveScore score, int addScore)
		{
			UpdateScore(score);
		}

		public virtual void UpdateLife(LiveScore score)
		{
		}

		public virtual void Result(int result)
		{
		}

		public virtual void OnFailure(float time)
		{
		}

		public virtual void Pause()
		{
		}

		public virtual void Pause(float time)
		{
			Pause();
		}

		public virtual void Countdown()
		{
		}

		public virtual void Resume()
		{
		}

		public virtual void Resume(float time)
		{
		}

		public virtual void Retry()
		{
		}

		public virtual void Finish()
		{
		}

		public virtual void Finish(float duration)
		{
			Finish();
		}

		public virtual void Finish3D()
		{
		}
	}
}
