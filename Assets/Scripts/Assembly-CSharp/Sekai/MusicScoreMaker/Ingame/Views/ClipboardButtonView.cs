using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class ClipboardButtonView : MonoBehaviour
	{
		[SerializeField]
		private CustomButton _button;

		[SerializeField]
		private GameObject _badge;

		public void Setup()
		{
			SetupEventDispatcher();
			UpdateButtonState();
			HideBadge();
		}

		public void Dispose()
		{
			DisposeEventDispatcher();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<UpdateClipboardButtonEvent>(OnUpdateClipboardButton);
			dispatcher.Register<ShowClipboardCacheListEvent>(OnShowClipboardCacheList);
			dispatcher.Register<ClearClipboardCacheEvent>(OnClearClipboardCache);
			dispatcher.Register<ToggleEditRestrictedEvent>(OnToggleEditRestricted);
			dispatcher.Register<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
		}

		private void DisposeEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}

			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<UpdateClipboardButtonEvent>(OnUpdateClipboardButton);
			dispatcher.Remove<ShowClipboardCacheListEvent>(OnShowClipboardCacheList);
			dispatcher.Remove<ClearClipboardCacheEvent>(OnClearClipboardCache);
			dispatcher.Remove<ToggleEditRestrictedEvent>(OnToggleEditRestricted);
			dispatcher.Remove<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
		}

		private void OnUpdateClipboardButton(UpdateClipboardButtonEvent evt)
		{
			UpdateButtonState();
			ShowBadge();
		}

		private void OnShowClipboardCacheList(ShowClipboardCacheListEvent evt)
		{
			HideBadge();
		}

		private void OnClearClipboardCache(ClearClipboardCacheEvent evt)
		{
			UpdateButtonState();
			HideBadge();
		}

		private void UpdateButtonState()
		{
			if (_button == null)
			{
				return;
			}

			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			bool isEditRestricted = dispatcher.PublishFirst<IsEditRestrictedEvent, bool>(new IsEditRestrictedEvent());
			bool isEventSettingMode = dispatcher.PublishFirst<GetIsEventSettingModeEvent, bool>(new GetIsEventSettingModeEvent());
			int cacheCount = ClipboardCacheManager.Instance.GetAllCaches().Count;
			_button.enabled = !isEditRestricted && !isEventSettingMode && cacheCount > 0;
		}

		private void OnToggleEditRestricted(ToggleEditRestrictedEvent evt)
		{
			UpdateButtonState();
		}

		private void OnUpdateButtonSelectionState(UpdateButtonSelectionStateEvent obj)
		{
			UpdateButtonState();
		}

		private void ShowBadge()
		{
			if (_badge != null)
			{
				_badge.SetActive(true);
			}
		}

		private void HideBadge()
		{
			if (_badge != null)
			{
				_badge.SetActive(false);
			}
		}

		public ClipboardButtonView()
		{
		}
	}
}
