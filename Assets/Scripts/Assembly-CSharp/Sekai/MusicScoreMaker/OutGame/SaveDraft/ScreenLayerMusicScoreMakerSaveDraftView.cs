using System;
using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.MusicScoreMaker.OutGame.Created.Content;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public sealed class ScreenLayerMusicScoreMakerSaveDraftView : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private UITextureLoader _backgroundLoader;

		[SerializeField]
		private CategoryTabList _categoryTabList;

		[SerializeField]
		private SaveDraftMusicScoreListSelectView _listSelectView;

		private SaveDraftScreenViewData _viewData;

		public void Setup(SaveDraftScreenViewData viewData, Action<int> onSelectedSlot, Action onSave, Action onDelete)
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

		public void Dispose()
		{
			throw null;
		}

		public ScreenLayerMusicScoreMakerSaveDraftView()
		{
			throw null;
		}
	}
}
