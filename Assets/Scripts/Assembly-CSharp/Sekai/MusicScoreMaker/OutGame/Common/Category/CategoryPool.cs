using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common.Category
{
	public class CategoryPool : GameObjectPool<Defines.CategoryType>
	{
		public CategoryPool(Transform parent)
			: base(parent)
		{
		}

		protected override string GetPrefabPath(Defines.CategoryType key)
		{
			return $"Prefabs/Common/Category/{key}";
		}

		protected override void OnGameObjectCreated(Defines.CategoryType key, GameObject gameObject)
		{
			gameObject.name = key.ToString();
		}
	}
}
