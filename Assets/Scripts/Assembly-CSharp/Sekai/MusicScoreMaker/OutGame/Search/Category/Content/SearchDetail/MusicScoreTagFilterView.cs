using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail
{
	public sealed class MusicScoreTagFilterView : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private MusicScoreTagCell _filteredTagCell;

		[SerializeField]
		private CustomButton _filterButton;

		[SerializeField]
		private CustomButton _clearButton;

		[SerializeField]
		private GameObject[] _unfilteredObjects;

		[SerializeField]
		private GameObject[] _filteredObjects;

		private MusicScoreTagFilterViewData _viewData;

		public void Setup(MusicScoreTagFilterViewData viewData, Action onFilter, Action onClear)
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreTagFilterView()
		{
			throw null;
		}
	}
}
