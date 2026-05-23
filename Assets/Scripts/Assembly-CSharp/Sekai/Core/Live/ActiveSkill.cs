namespace Sekai.Core.Live
{
	public struct ActiveSkill
	{
		public float lifeTime;
		public BaseSkillEffect skillEffect;

		public ActiveSkill(BaseSkillEffect skillEffect, float time)
		{
			this.skillEffect = skillEffect;
			lifeTime = (skillEffect != null ? skillEffect.LifeTime : 0f) + time;
		}
	}
}
