using System.Collections.Generic;
using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.Live
{
	public class SkillView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer life;

		[SerializeField]
		private SpriteRenderer score;

		private List<ActiveSkill> lifeSkills;

		private List<ActiveSkill> scoreSkills;

		private List<ActiveSkill> judgeSkills;

		private ParticleSystem judgeEffect;

		public void Setup()
		{
			if (life != null)
			{
				life.enabled = false;
			}
			if (score != null)
			{
				score.enabled = false;
			}
		}

		public void Load()
		{
			judgeEffect = LoadSkillParticle("fx_skill_JudgeLine");
			if (judgeEffect != null)
			{
				judgeEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			}
		}

		public void Unload()
		{
			AssetBundleUtility.UnloadAssetBundle(LiveConfig.SkillBundleName);
		}

		private ParticleSystem LoadSkillParticle(string name)
		{
			GameObject prefab = AssetBundleUtility.LoadAsset<GameObject>(LiveConfig.SkillBundleName, name, false);
			if (prefab == null)
			{
				prefab = Resources.Load<GameObject>(name);
			}
			if (prefab == null)
			{
				return null;
			}

			GameObject instance = Instantiate(prefab, transform);
			return instance.GetComponentInChildren<ParticleSystem>(true);
		}

		public void Clear()
		{
			lifeSkills.Clear();
			scoreSkills.Clear();
			judgeSkills.Clear();
			Setup();
			if (judgeEffect != null)
			{
				judgeEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			}
		}

		public void UpdateSkill(SkillData skillData, float time)
		{
			BaseSkillEffect[] skillEffects;
			try
			{
				skillEffects = skillData?.SkillEffects;
			}
			catch
			{
				return;
			}
			if (skillEffects == null)
			{
				return;
			}

			foreach (BaseSkillEffect skillEffect in skillEffects)
			{
				if (skillEffect != null)
				{
					AddSkill(new ActiveSkill(skillEffect, time));
				}
			}
		}

		private void AddSkill(ActiveSkill activeSkill)
		{
			switch (activeSkill.skillEffect?.Type)
			{
				case SkillType.LifeRecovery:
					lifeSkills.Add(activeSkill);
					break;
				case SkillType.ScoreUp:
				case SkillType.ScoreUpConditionLife:
				case SkillType.ScoreUpKeep:
					scoreSkills.Add(activeSkill);
					break;
				case SkillType.JudgmentEasy:
					judgeSkills.Add(activeSkill);
					break;
			}
		}

		public void OnUpdate(float time)
		{
			RemoveExpired(lifeSkills, time);
			RemoveExpired(scoreSkills, time);
			RemoveExpired(judgeSkills, time);

			if (life != null)
			{
				life.enabled = lifeSkills.Count > 0;
			}
			if (score != null)
			{
				score.enabled = scoreSkills.Count > 0;
			}

			if (judgeEffect == null)
			{
				return;
			}
			if (judgeSkills.Count > 0)
			{
				if (!judgeEffect.isPlaying)
				{
					judgeEffect.Play(true);
				}
			}
			else if (judgeEffect.isPlaying)
			{
				judgeEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
		}

		public SkillView()
		{
			lifeSkills = new List<ActiveSkill>();
			scoreSkills = new List<ActiveSkill>();
			judgeSkills = new List<ActiveSkill>();
		}

		private static void RemoveExpired(List<ActiveSkill> skills, float time)
		{
			for (int i = 0; i < skills.Count; i++)
			{
				if (skills[i].lifeTime <= time)
				{
					skills.RemoveAt(i--);
				}
			}
		}
	}
}
