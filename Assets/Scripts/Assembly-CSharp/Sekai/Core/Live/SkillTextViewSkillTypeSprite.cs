using UnityEngine;

namespace Sekai.Core.Live
{
	public class SkillTextViewSkillTypeSprite : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer baseSprite;

		[SerializeField]
		private SpriteRenderer icon;

		[SerializeField]
		private SpriteRenderer text;

		[SerializeField]
		private SpriteRenderer level;

		[SerializeField]
		private SpriteRenderer levelShadow;

		public void Initialize(Sprite baseSprite, Sprite iconSprite, Sprite textSprite, Sprite levelSprite, Sprite levelShadowSprite)
		{
			SetSprite(this.baseSprite, baseSprite);
			SetSprite(icon, iconSprite);
			SetSprite(text, textSprite);
			SetSprite(level, levelSprite);
			SetSprite(levelShadow, levelShadowSprite);
		}

		public SkillTextViewSkillTypeSprite()
		{
		}

		private static void SetSprite(SpriteRenderer renderer, Sprite sprite)
		{
			if (renderer == null)
			{
				return;
			}

			renderer.sprite = sprite;
			renderer.enabled = sprite != null;
		}
	}
}
