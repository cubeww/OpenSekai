using System;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Created.Content
{
	public sealed class SaveDraftMusicScoreListSelectView : ContentViewBase<SaveDraftMusicScoreListSelectViewData>
	{
		[SerializeField]
		private ListView _listView;

		[SerializeField]
		private CustomTextMesh _countText;

		[SerializeField]
		private CustomButton _saveButton;

		[SerializeField]
		private CustomButton _deleteButton;

		[SerializeField]
		private CustomButton _loadButton;

		private Action<int> _onSelectedSlot;

		public void Setup(Action<int> onSelectedSlot, Action onSave, Action onDelete, Action onLoad)
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		public void RefreshSlotSelected()
		{
			throw null;
		}

		private void RefreshButton()
		{
			throw null;
		}

		private void OnCreateCell(ListViewItem cell, int index)
		{
			throw null;
		}

		public override void Dispose()
		{
			throw null;
		}

		public SaveDraftMusicScoreListSelectView()
		{
			throw null;
		}
	}
}
