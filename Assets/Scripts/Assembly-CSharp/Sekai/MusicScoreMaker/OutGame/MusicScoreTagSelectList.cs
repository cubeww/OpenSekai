using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreTagSelectList : MonoBehaviour, IDisposable
	{
		private Action _onSelectionChanged;

		[SerializeField]
		private ListView _listView;

		[Min(1f)]
		[SerializeField]
		private int _tagsPerCell;

		private MusicScoreTagSelectCellData[] _tagDataArray;

		public List<int> SelectedTagIds
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

		public bool IsValidSelection
		{
			get
			{
				throw null;
			}
		}

		public void Setup(int[] alreadySelectedTagIds = null, Action onSelectionChanged = null)
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		private void BuildTagsFromMaster()
		{
			throw null;
		}

		private void OnCreateCell(ListViewItem cell, int index)
		{
			throw null;
		}

		private void OnToggleChanged(int tagId, bool isOn)
		{
			throw null;
		}

		private void UpdateTagsEnabledState()
		{
			throw null;
		}

		private void RefreshAllCells()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreTagSelectList()
		{
			throw null;
		}
	}
}
