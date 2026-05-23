using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Created
{
	public sealed class MusicScoreMakerCreatedConfig : ScriptableObject
	{
		[Serializable]
		public class CategoryInfo
		{
			public Defines.CategoryType Category;

			public Defines.ContentType DefaultContent;

			public string TitleWordingKey;

			public CategoryInfo()
			{
				throw null;
			}
		}

		[SerializeField]
		private List<CategoryInfo> _categoryInfos;

		public CategoryInfo[] GetCategoryInfoArray()
		{
			throw null;
		}

		public Defines.ContentType GetDefaultContentType(Defines.CategoryType category)
		{
			throw null;
		}

		public MusicScoreMakerCreatedConfig()
		{
			throw null;
		}
	}
}
