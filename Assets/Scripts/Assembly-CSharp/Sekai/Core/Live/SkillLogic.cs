using Sekai.Live;

namespace Sekai.Core.Live
{
	public class SkillLogic
	{
		public virtual void Setup(LiveBootDataBase bootData, LiveScore score)
		{
		}

		public virtual void Update(double currentGameTime)
		{
		}

		public virtual void ExcuteEvent(EventBase eventBase)
		{
		}

		public virtual void ActivateSkill()
		{
		}

		public virtual float ScoreFactor(NoteBase note)
		{
			return 1f;
		}

		public virtual int AddScore(int score)
		{
			return score;
		}

		public virtual (NoteResult result, NoteResultDescription description) UpdateResult((NoteResult result, NoteResultDescription description) result)
		{
			return result;
		}
	}
}
