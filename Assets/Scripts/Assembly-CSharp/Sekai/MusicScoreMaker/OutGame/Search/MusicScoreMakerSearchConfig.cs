using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search
{
	public sealed class MusicScoreMakerSearchConfig : ScriptableObject
	{
		[Serializable]
		public class CategoryInfo
		{
			public Sekai.MusicScoreMaker.OutGame.Defines.CategoryType Category;

			public Sekai.MusicScoreMaker.OutGame.Defines.ContentType DefaultContent;

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

		public Sekai.MusicScoreMaker.OutGame.Defines.ContentType GetDefaultContentType(Sekai.MusicScoreMaker.OutGame.Defines.CategoryType category)
		{
			throw null;
		}

		public MusicScoreMakerSearchConfig()
		{
			throw null;
		}
	}
}
