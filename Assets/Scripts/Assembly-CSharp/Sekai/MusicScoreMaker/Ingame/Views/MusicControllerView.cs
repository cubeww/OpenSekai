using System;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicControllerView : MonoBehaviour
	{
		[SerializeField]
		private CustomButton _playButton;

		[SerializeField]
		private CustomImage _playImage;

		[SerializeField]
		private CustomImage _stopImage;

		[SerializeField]
		private CustomTextMesh timeText;

		private void Awake()
		{
			Setup();
		}

		private void OnDestroy()
		{
			Dispose();
		}

		// OpenSekai: allow Space to trigger the same play/pause path as the UI button.
		private void Update()
		{
			if (!UnityEngine.Input.GetKeyDown(KeyCode.Space) || !CanHandlePlayPauseShortcut())
			{
				return;
			}

			CallMusicPlayButton(null);
		}

		public void Setup()
		{
			if (_playImage != null)
			{
				_playImage.SetActive(true);
			}
			if (_stopImage != null)
			{
				_stopImage.SetActive(false);
			}
			if (_playButton != null)
			{
				_playButton.onClick.RemoveAllListeners();
				_playButton.onClick.AddListener(SetPlayButtonImage);
			}
			UpdateMusicTimeText(0f);
			SetupEventDispatcher();
		}

		private void SetPlayButtonImage()
		{
			bool isPlaying = MusicScoreMakerEventDispatcher.Instance.PublishFirst<SwitchPlayPauseMusicEvent, bool>(new SwitchPlayPauseMusicEvent());
			if (_playImage != null)
			{
				_playImage.SetActive(!isPlaying);
			}
			if (_stopImage != null)
			{
				_stopImage.SetActive(isPlaying);
			}
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<CallMusicPlayButtonEvent>(CallMusicPlayButton);
			dispatcher.Remove<UpdateMusicTimeTextEvent>(UpdateMusicTimeText);
			dispatcher.Remove<PauseMusicEvent>(OnPauseMusic);
			dispatcher.Register<CallMusicPlayButtonEvent>(CallMusicPlayButton);
			dispatcher.Register<UpdateMusicTimeTextEvent>(UpdateMusicTimeText);
			dispatcher.Register<PauseMusicEvent>(OnPauseMusic);
		}

		public void Dispose()
		{
			if (_playButton != null)
			{
				_playButton.onClick.RemoveListener(SetPlayButtonImage);
			}
			DisposeEventDispatcher();
		}

		private void DisposeEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.Remove<CallMusicPlayButtonEvent>(CallMusicPlayButton);
			MusicScoreMakerEventDispatcher.Instance.Remove<UpdateMusicTimeTextEvent>(UpdateMusicTimeText);
			MusicScoreMakerEventDispatcher.Instance.Remove<PauseMusicEvent>(OnPauseMusic);
		}

		public void CallMusicPlayButton(CallMusicPlayButtonEvent value)
		{
			if (_playButton != null)
			{
				_playButton.onClick.Invoke();
			}
		}

		private bool CanHandlePlayPauseShortcut()
		{
			if (_playButton == null || !_playButton.isActiveAndEnabled || !_playButton.interactable)
			{
				return false;
			}
			if (IsInputFieldFocused())
			{
				return false;
			}
			if (ScreenManager.Instance != null && ScreenManager.Instance.ExistsDialog())
			{
				return false;
			}

			SubWindowSlideAnimationController[] subWindows = FindObjectsOfType<SubWindowSlideAnimationController>(true);
			foreach (SubWindowSlideAnimationController subWindow in subWindows)
			{
				if (subWindow != null && subWindow.gameObject.activeSelf)
				{
					return false;
				}
			}

			return true;
		}

		private static bool IsInputFieldFocused()
		{
			GameObject selectedObject = EventSystem.current != null ? EventSystem.current.currentSelectedGameObject : null;
			if (selectedObject == null)
			{
				return false;
			}

			return selectedObject.GetComponent<TMPro.TMP_InputField>() != null
				|| selectedObject.GetComponent<InputField>() != null;
		}

		private void OnPauseMusic(PauseMusicEvent evt)
		{
			if (_playImage != null)
			{
				_playImage.SetActive(true);
			}
			if (_stopImage != null)
			{
				_stopImage.SetActive(false);
			}
			ResetPlayButtonInteraction();
		}

		private void ResetPlayButtonInteraction()
		{
			if (_playButton == null)
			{
				return;
			}
			ButtonViewInteractionBase interaction = _playButton.GetComponent<ButtonViewInteractionBase>();
			interaction?.ResetInteraction();
		}

		private void UpdateMusicTimeText(UpdateMusicTimeTextEvent evt)
		{
			UpdateMusicTimeText(evt.CurrentTime);
		}

		private void UpdateMusicTimeText(float time)
		{
			if (timeText == null)
			{
				return;
			}
			int minutes = (int)Math.Floor(time / 60f);
			int seconds = (int)Math.Floor(time % 60f);
			int milliseconds = (int)Math.Floor(time * 1000f % 1000f);
			timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
		}

		public MusicControllerView()
		{
		}
	}
}
