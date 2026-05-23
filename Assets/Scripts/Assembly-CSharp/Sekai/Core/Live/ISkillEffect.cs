namespace Sekai.Core.Live
{
	public interface ISkillEffect
	{
		SkillType Type { get; }

		bool IsInstant { get; }

		int Level { get; }

		float LifeTime { get; }

		int Order { get; }
	}
}
