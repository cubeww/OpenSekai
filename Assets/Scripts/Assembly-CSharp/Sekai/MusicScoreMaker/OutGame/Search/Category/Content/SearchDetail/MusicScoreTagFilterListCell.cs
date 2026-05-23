using System;
using JetBrains.Annotations;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail
{
	public sealed class MusicScoreTagFilterListCell : ListViewItem, IDisposable
	{
		[SerializeField]
		private MusicScoreTagFilterCell[] _tagCellArray;

		private MusicScoreTagFilterCellData[] _tagCellDataArray;

		private Action<int> _onTagClicked;

		public void Setup([NotNull] MusicScoreTagFilterCellData[] tagCellDataArray, Action<int> onTagClicked)
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

		private void OnDestroy()
		{
			throw null;
		}

		public MusicScoreTagFilterListCell()
		{
			throw null;
		}
	}
}
