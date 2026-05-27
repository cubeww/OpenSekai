using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreTimeSlider : MonoBehaviour
	{
		[SerializeField]
		private CustomSlider _slider;

		private long _cachedMaxTicks;

		public void Setup()
		{
			SetupPlaySpeedSlider();
			MusicScoreMakerEventDispatcher.Instance.Register<UpdateTimelineSliderValueWithoutNotifyEvent>(UpdateTimelineSliderValueWithoutNotify);
			MusicScoreMakerEventDispatcher.Instance.Register<PlayMusicEvent>(OnPlayMusic);
			MusicScoreMakerEventDispatcher.Instance.Register<PauseMusicEvent>(OnPauseMusic);
		}

		public void Dispose()
		{
			if (_slider != null)
			{
				_slider.RemoveAllListeners();
			}
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.Remove<UpdateTimelineSliderValueWithoutNotifyEvent>(UpdateTimelineSliderValueWithoutNotify);
			MusicScoreMakerEventDispatcher.Instance.Remove<PlayMusicEvent>(OnPlayMusic);
			MusicScoreMakerEventDispatcher.Instance.Remove<PauseMusicEvent>(OnPauseMusic);
		}

		private void SetupPlaySpeedSlider()
		{
			if (_slider == null)
			{
				return;
			}
			_slider.wholeNumbers = false;
			_cachedMaxTicks = MusicScoreMakerUtility.GetMusicScoreTicksMax();
			_slider.maxValue = Mathf.Max((float)_cachedMaxTicks, 0f);
			_slider.minValue = 0f;
			UpdateTimelineSliderValueWithoutNotify();
			_slider.RemoveAllAndAddListener(OnChangeSliderValue);
		}

		private void UpdateTimelineSliderValueWithoutNotify(UpdateTimelineSliderValueWithoutNotifyEvent value = null)
		{
			if (_slider != null)
			{
				_slider.SetValueWithoutNotify(MusicScoreMakerUtility.GetFocusTicks());
			}
		}

		public void UpdateView()
		{
			if (_slider == null)
			{
				return;
			}
			long ticksMax = MusicScoreMakerUtility.GetMusicScoreTicksMax();
			if (_cachedMaxTicks == ticksMax && _cachedMaxTicks != -1L)
			{
				return;
			}
			_cachedMaxTicks = ticksMax;
			_slider.RemoveAllListeners();
			_slider.maxValue = Mathf.Max((float)ticksMax, 1f);
			_slider.RemoveAllAndAddListener(OnChangeSliderValue);
		}

		private void OnChangeSliderValue(float value)
		{
			MusicScoreMakerUtility.SetFocusTicks((long)value);
		}

		private void OnPlayMusic(PlayMusicEvent evt)
		{
			if (_slider != null)
			{
				_slider.interactable = false;
			}
		}

		private void OnPauseMusic(PauseMusicEvent evt)
		{
			if (_slider != null)
			{
				_slider.interactable = true;
			}
		}

		public MusicScoreTimeSlider()
		{
			_cachedMaxTicks = -1L;
		}
	}
}
