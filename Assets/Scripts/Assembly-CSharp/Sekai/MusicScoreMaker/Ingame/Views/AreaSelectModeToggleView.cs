using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class AreaSelectModeToggleView : MonoBehaviour
	{
		[SerializeField]
		private UIPartsToggle _toggle;

		private CustomButton _button;

		private static readonly IsMusicPlayingEvent IsMusicPlayingEventCache;

		private void Awake()
		{
			Setup();
		}

		private void OnDestroy()
		{
			Dispose();
		}

		public void Setup()
		{
			if (_toggle == null)
			{
				_toggle = GetComponent<UIPartsToggle>();
			}
			if (_toggle == null)
			{
				return;
			}
			_button = _toggle.GetComponent<CustomButton>();
			_toggle.Setup(UIPartsToggleBase.State.Off);
			_toggle.OnToggleOn = OnToggleOn;
			_toggle.OnToggleOff = OnToggleOff;
			SetupEventDispatcher();
			UpdateInteractable();
		}

		public void Dispose()
		{
			if (_toggle != null)
			{
				_toggle.OnToggleOn = null;
				_toggle.OnToggleOff = null;
			}
			DisposeEventDispatcher();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
			dispatcher.Remove<PlayMusicEvent>(OnPlayMusicEvent);
			dispatcher.Remove<PauseMusicEvent>(OnPauseMusicEvent);
			dispatcher.Register<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
			dispatcher.Register<PlayMusicEvent>(OnPlayMusicEvent);
			dispatcher.Register<PauseMusicEvent>(OnPauseMusicEvent);
		}

		private void DisposeEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.Remove<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
			MusicScoreMakerEventDispatcher.Instance.Remove<PlayMusicEvent>(OnPlayMusicEvent);
			MusicScoreMakerEventDispatcher.Instance.Remove<PauseMusicEvent>(OnPauseMusicEvent);
		}

		private void OnUpdateButtonSelectionState(UpdateButtonSelectionStateEvent obj)
		{
			UpdateInteractable();
		}

		private void OnPlayMusicEvent(PlayMusicEvent obj)
		{
			UpdateInteractable();
		}

		private void OnPauseMusicEvent(PauseMusicEvent obj)
		{
			UpdateInteractable();
		}

		private void UpdateInteractable()
		{
			if (_toggle == null)
			{
				return;
			}
			bool isEventSettingMode = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetIsEventSettingModeEvent, bool>(new GetIsEventSettingModeEvent());
			bool isPlaying = MusicScoreMakerEventDispatcher.Instance.PublishFirst<IsMusicPlayingEvent, bool>(IsMusicPlayingEventCache);
			bool canSelect = !isEventSettingMode && !isPlaying;
			if (_button != null)
			{
				_button.interactable = canSelect;
			}
			_toggle.Setup(!canSelect ? UIPartsToggleBase.State.Disable : MusicScoreMakerUtility.IsAreaSelectMode() ? UIPartsToggleBase.State.On : UIPartsToggleBase.State.Off);
		}

		private void OnToggleOn()
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new SwitchAreaSelectModeEvent());
		}

		private void OnToggleOff()
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new SwitchAreaSelectModeEvent());
		}

		public AreaSelectModeToggleView()
		{
		}

		static AreaSelectModeToggleView()
		{
			IsMusicPlayingEventCache = new IsMusicPlayingEvent();
		}
	}
}
