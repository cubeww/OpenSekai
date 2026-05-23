using System;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Created.Content
{
	public sealed class AutoSaveMusicScoreListSelectView : ContentViewBase<AutoSaveMusicScoreListSelectViewData>
	{
		[SerializeField]
		private ListView _listView;

		[SerializeField]
		private CustomTextMesh _countText;

		[SerializeField]
		private CustomTextMesh _notFoundText;

		[SerializeField]
		private CustomButton _createRestartButton;

		[SerializeField]
		private GameObject _footer;

		private Action<int> _onSelectedSlot;

		public void Setup(Action<int> onSelectedSlot, Action onCreateRestart)
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

		private void OnCreateCell(ListViewItem cell, int index)
		{
			throw null;
		}

		public override void Dispose()
		{
			throw null;
		}

		public AutoSaveMusicScoreListSelectView()
		{
			throw null;
		}
	}
}
