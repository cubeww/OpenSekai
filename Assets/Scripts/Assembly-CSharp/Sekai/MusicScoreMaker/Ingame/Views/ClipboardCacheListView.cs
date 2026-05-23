using System.Collections.Generic;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class ClipboardCacheListView : MonoBehaviour
	{
		[SerializeField]
		private Transform _listParent;

		[SerializeField]
		private ClipboardCacheListItem _listItemPrefab;

		[SerializeField]
		private Toggle _flipHorizontalToggle;

		private List<ClipboardCacheListItem> _listItems;

		private bool _isInitialized;

		private bool _isFlipHorizontal;

		private SubWindowSlideAnimationController _parentSubWindowController;

		private void OnDestroy()
		{
			Dispose();
		}

		public void Initialize()
		{
			if (_isInitialized)
			{
				return;
			}

			gameObject.SetActive(false);
			_listItems = new List<ClipboardCacheListItem>();
			if (_flipHorizontalToggle != null)
			{
				_flipHorizontalToggle.SetIsOnWithoutNotify(false);
				_flipHorizontalToggle.onValueChanged.RemoveListener(OnFlipHorizontalToggleChanged);
				_flipHorizontalToggle.onValueChanged.AddListener(OnFlipHorizontalToggleChanged);
			}
			_parentSubWindowController = GetComponentInParent<SubWindowSlideAnimationController>(true);
			SetupEventDispatcher();
			_isInitialized = true;
		}

		private void Dispose()
		{
			if (!_isInitialized)
			{
				return;
			}

			if (_flipHorizontalToggle != null)
			{
				_flipHorizontalToggle.onValueChanged.RemoveListener(OnFlipHorizontalToggleChanged);
			}
			DisposeEventDispatcher();
			ClearListItems();
			_isInitialized = false;
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<ShowClipboardCacheListEvent>(OnShowClipboardCacheList);
			dispatcher.Register<ShowNoteChangeButtonsEvent>(OnShowNoteChangeButtons);
			dispatcher.Register<ClearClipboardCacheEvent>(OnClearClipboardCache);
		}

		private void DisposeEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<ShowClipboardCacheListEvent>(OnShowClipboardCacheList);
			dispatcher.Remove<ShowNoteChangeButtonsEvent>(OnShowNoteChangeButtons);
			dispatcher.Remove<ClearClipboardCacheEvent>(OnClearClipboardCache);
		}

		private void OnShowClipboardCacheList(ShowClipboardCacheListEvent eventData)
		{
			RefreshList();
			gameObject.SetActive(true);
		}

		private void OnShowNoteChangeButtons(ShowNoteChangeButtonsEvent eventData)
		{
			Hide();
		}

		private void OnClearClipboardCache(ClearClipboardCacheEvent eventData)
		{
			RefreshList();
			Hide();
		}

		private void RefreshList()
		{
			ClearListItems();

			if (_listParent == null || _listItemPrefab == null)
			{
				return;
			}

			IReadOnlyList<ClipboardCacheData> caches = ClipboardCacheManager.Instance.GetAllCaches();
			for (int i = 0; i < caches.Count; i++)
			{
				ClipboardCacheListItem item = Instantiate(_listItemPrefab, _listParent);
				item.Setup(caches[i], OnPasteClicked);
				item.SetFlipHorizontal(_isFlipHorizontal);
				_listItems.Add(item);
			}
		}

		private void OnFlipHorizontalToggleChanged(bool isOn)
		{
			_isFlipHorizontal = isOn;
			if (_listItems == null)
			{
				return;
			}
			for (int i = 0; i < _listItems.Count; i++)
			{
				if (_listItems[i] != null)
				{
					_listItems[i].SetFlipHorizontal(isOn);
				}
			}
		}

		private void OnPasteClicked(ClipboardCacheData cache, bool isFlipHorizontal)
		{
			if (cache == null)
			{
				return;
			}

			MusicScoreMakerEventDispatcher.Instance.Publish(new PasteFromClipboardCacheEvent
			{
				CacheId = cache.Id,
				IsFlipHorizontal = isFlipHorizontal
			});
			HideForPaste();
		}

		private void HideForPaste()
		{
			Hide();
			if (_parentSubWindowController != null)
			{
				_parentSubWindowController.CloseAnimationSilently();
			}
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}

		private void ClearListItems()
		{
			if (_listItems == null)
			{
				_listItems = new List<ClipboardCacheListItem>();
				return;
			}

			for (int i = 0; i < _listItems.Count; i++)
			{
				if (_listItems[i] != null)
				{
					Destroy(_listItems[i].gameObject);
				}
			}
			_listItems.Clear();
		}

		public ClipboardCacheListView()
		{
			_listItems = new List<ClipboardCacheListItem>();
		}
	}
}
