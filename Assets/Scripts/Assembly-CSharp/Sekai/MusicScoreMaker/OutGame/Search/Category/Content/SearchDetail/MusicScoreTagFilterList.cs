using System;
using System.Runtime.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail
{
	public sealed class MusicScoreTagFilterList : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private ListView _listView;

		[SerializeField]
		[Min(1f)]
		private int _tagsPerCell;

		private MusicScoreTagFilterCellData[] _tagDataArray;

		public int SelectedTagId
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public void Setup(int initialSelectedTagId = -1)
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		private void BuildTagsFromMaster(int initialSelectedTagId)
		{
			throw null;
		}

		private void OnCreateCell(ListViewItem cell, int index)
		{
			throw null;
		}

		private void OnClickedTag(int tagId)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreTagFilterList()
		{
			throw null;
		}
	}
}
