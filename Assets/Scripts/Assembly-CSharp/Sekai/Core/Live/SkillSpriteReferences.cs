using System;
using UnityEngine;

namespace Sekai.Core.Live
{
	public class SkillSpriteReferences : MonoBehaviour
	{
		[Serializable]
		public class SkillSprite
		{
			[SerializeField]
			private Sprite baseSprite;

			[SerializeField]
			private Sprite iconSprite;

			[SerializeField]
			private Sprite textSprite;

			[SerializeField]
			private Sprite effectPostfixSprites;

			public Sprite BaseSprite
			{
				get
				{
					return baseSprite;
				}
			}

			public Sprite IconSprite
			{
				get
				{
					return iconSprite;
				}
			}

			public Sprite TextSprite
			{
				get
				{
					return textSprite;
				}
			}

			public SkillSprite()
			{
			}
		}

		[Serializable]
		public class LevelSprite
		{
			[SerializeField]
			private Sprite textSprite;

			[SerializeField]
			private Sprite shadowSprite;

			public Sprite TextSprite
			{
				get
				{
					return textSprite;
				}
			}

			public Sprite ShadowSprite
			{
				get
				{
					return shadowSprite;
				}
			}

			public LevelSprite()
			{
			}
		}

		[SerializeField]
		private LevelSprite[] levelSprites;

		[SerializeField]
		private SkillSprite[] skillSprites;

		public SkillSprite GetSkillSprite(SkillSpriteType spriteType)
		{
			int index = (int)spriteType;
			if (skillSprites == null || skillSprites.Length == 0)
			{
				return null;
			}

			if (index < 0 || index >= skillSprites.Length)
			{
				index = 0;
			}

			return skillSprites[index];
		}

		public LevelSprite GetLevelSprite(int skillLevel)
		{
			if (levelSprites == null || levelSprites.Length == 0)
			{
				return null;
			}

			int index = Mathf.Clamp(skillLevel - 1, 0, levelSprites.Length - 1);
			return levelSprites[index];
		}

		public SkillSpriteReferences()
		{
		}
	}
}
