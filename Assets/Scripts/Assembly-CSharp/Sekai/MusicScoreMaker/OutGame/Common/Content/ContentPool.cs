using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common.Content
{
	public class ContentPool : GameObjectPool<Defines.ContentType>
	{
		public ContentPool(Transform parent)
			: base(parent)
		{
		}

		protected override string GetPrefabPath(Defines.ContentType key)
		{
			return "Prefabs/Common/Contents/" + key;
		}

		protected override void OnGameObjectCreated(Defines.ContentType key, GameObject gameObject)
		{
			gameObject.name = key.ToString();
		}
	}
}
