using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class MusicSelectUnitTabList : MonoBehaviour
	{
		[SerializeField]
		private ListView listView;

		private Dictionary<int, MusicSelectUnitTab> itemCacheTable;

		private CategoryTabType[] dataList;

		private CategoryTabType selectedTabType;

		public Action<CategoryTabType> OnSelectCategory
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public void Initialize(CategoryTabType[] tabTypes, CategoryTabType selectTabType)
		{
			throw null;
		}

		private void OnCreateCell(ListViewItem item, int dataIndex)
		{
			throw null;
		}

		private void OnClickTab(CategoryTabType tabType)
		{
			throw null;
		}

		public void UpdateTabText(CategoryTabType tabType, string text)
		{
			throw null;
		}

		public string GetTabText(CategoryTabType tabType)
		{
			throw null;
		}

		public void Clear()
		{
			throw null;
		}

		public MusicSelectUnitTabList()
		{
			throw null;
		}
	}
}
