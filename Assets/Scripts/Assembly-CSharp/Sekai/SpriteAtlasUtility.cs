using UnityEngine;
using UnityEngine.U2D;

namespace Sekai
{
	public static class SpriteAtlasUtility
	{
		public static Sprite GetSprite(SpriteAtlas atlas, string name)
		{
			return atlas != null ? atlas.GetSprite(name) : null;
		}
	}
}
