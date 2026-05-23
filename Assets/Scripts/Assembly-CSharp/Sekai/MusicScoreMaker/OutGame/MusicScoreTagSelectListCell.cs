using System;
using JetBrains.Annotations;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreTagSelectListCell : ListViewItem, IDisposable
	{
		[SerializeField]
		private MusicScoreTagSelectCell[] _tagCellArray;

		private MusicScoreTagSelectCellData[] _tagCellDataArray;

		private Action<int, bool> _onToggleChanged;

		public void Setup([NotNull] MusicScoreTagSelectCellData[] tagCellDataArray, Action<int, bool> onToggleChanged)
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

		public MusicScoreTagSelectListCell()
		{
			throw null;
		}
	}
}
