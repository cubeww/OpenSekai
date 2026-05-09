using System.Collections.Generic;
using Sekai.Live;
using UnityEngine;

namespace Sekai.Core.Live
{
    public abstract class LiveViewBase : MonoBehaviour
    {
        public virtual void Setup(BaseLiveController baseController)
        {
        }

        public virtual void OnLoad()
        {
        }

        public virtual void OnUnload()
        {
        }

        public virtual void CreateNotePool(Dictionary<(NoteCategory, NoteType), int> notePoolCount)
        {
        }

        public virtual void SetupScore(LiveScore score)
        {
        }

        public virtual void ChangeScoreCalculator(ScoreGaugeCalculator scoreGaugeCalculator)
        {
        }

        public virtual void OnUpdate(float time)
        {
        }

        public virtual void MusicStart(float time)
        {
        }

        public virtual void RhythmGameStart()
        {
        }

        public virtual void OnScreenSizeChanged()
        {
        }

        public virtual void Pause(float time)
        {
        }

        public virtual void Resume(float time)
        {
        }

        public virtual void Retry()
        {
        }

        public virtual void Countdown()
        {
        }

        public virtual void OnFailure(float time)
        {
        }

        public virtual void Result(LiveResultAnimationType animationType)
        {
        }

        public virtual void Finish()
        {
        }

        public virtual void SpawnNote(INote note)
        {
        }

        public virtual void UnspawnNote(INote note)
        {
        }

        public virtual void JudgmentNote(INote note)
        {
        }

        public virtual void Unpicked(int lane, ref LiveTouch touch)
        {
        }

        public virtual void UpdateScore(ref LiveScore score, int addScore)
        {
        }

        public virtual void UpdateLife(int life)
        {
        }

        public virtual void UpdateCombo(LiveScore score)
        {
        }
    }
}
