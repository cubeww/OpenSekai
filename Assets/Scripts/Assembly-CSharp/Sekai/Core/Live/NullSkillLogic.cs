using Sekai.Live;

namespace Sekai.Core.Live
{
	public class NullSkillLogic : SkillLogic
	{
		public override float ScoreFactor(NoteBase note)
		{
			return 1f;
		}
	}
}
