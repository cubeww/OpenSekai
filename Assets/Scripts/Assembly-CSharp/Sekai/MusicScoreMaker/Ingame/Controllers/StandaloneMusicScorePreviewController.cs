using CP;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.MusicScoreMaker.Ingame.Views;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Controllers
{
	public class StandaloneMusicScorePreviewController : MonoBehaviour
	{
		[SerializeField]
		private MusicScorePreview _musicScorePreview;

		private MusicScoreMakerData _musicScoreMakerData;

		private long _currentFocusTicks;

		private float _currentMusicScoreScale;

		private bool _isSetup;

		public MusicScoreMakerData MusicScoreMakerData
		{
			get
			{
				return _musicScoreMakerData;
			}
			set
			{
				_musicScoreMakerData = value;
				RefreshMusicScore();
			}
		}

		public long CurrentFocusTicks
		{
			get
			{
				return _currentFocusTicks;
			}
			set
			{
				if (_currentFocusTicks == value)
				{
					return;
				}

				_currentFocusTicks = value;
				UpdateMusicScore();
			}
		}

		public float CurrentMusicScoreScale
		{
			get
			{
				return _currentMusicScoreScale;
			}
			set
			{
				if (Mathf.Approximately(_currentMusicScoreScale, value))
				{
					return;
				}

				_currentMusicScoreScale = value;
				UpdateMusicScore();
			}
		}

		public MusicScorePreview MusicScorePreview
		{
			get
			{
				return _musicScorePreview;
			}
		}

		private void Awake()
		{
			if (_musicScorePreview == null)
			{
				_musicScorePreview = GetComponentInChildren<MusicScorePreview>();
			}
		}

		private void OnEnable()
		{
			if (!Application.isPlaying)
			{
				return;
			}

			if (_musicScorePreview != null)
			{
				SetupEventHandlers();
			}
			else
			{
				Debug.LogWarning("[StandaloneMusicScorePreviewController] MusicScorePreviewがnullです。子オブジェクトとして配置してください。");
			}
		}

		private void SetupMusicScorePreview()
		{
			if (_musicScorePreview == null || _isSetup)
			{
				return;
			}

			_musicScorePreview.Setup(50, 10);
			_musicScorePreview.SetHideNoInGameNotes(true);
			_isSetup = true;
		}

		private void OnDisable()
		{
			if (!Application.isPlaying)
			{
				return;
			}

			if (_musicScorePreview != null && _isSetup)
			{
				_musicScorePreview.Dispose();
				_isSetup = false;
			}

			DisposeEventHandlers();
		}

		private void SetupEventHandlers()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<GetMusicScoreMakerDataEvent, MusicScoreMakerData>(GetMusicScoreMakerDataHandler);
			dispatcher.Register<GetCurrentMusicScoreStartTicksEvent, long>(GetCurrentMusicScoreStartTicksHandler);
			dispatcher.Register<GetCurrentMusicScoreScaleEvent, float>(GetCurrentMusicScoreScaleHandler);
			dispatcher.Register<GetFocusTicksEvent, long>(GetFocusTicksHandler);
		}

		private void DisposeEventHandlers()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<GetMusicScoreMakerDataEvent, MusicScoreMakerData>(GetMusicScoreMakerDataHandler);
			dispatcher.Remove<GetCurrentMusicScoreStartTicksEvent, long>(GetCurrentMusicScoreStartTicksHandler);
			dispatcher.Remove<GetCurrentMusicScoreScaleEvent, float>(GetCurrentMusicScoreScaleHandler);
			dispatcher.Remove<GetFocusTicksEvent, long>(GetFocusTicksHandler);
		}

		private MusicScoreMakerData GetMusicScoreMakerDataHandler(GetMusicScoreMakerDataEvent evt)
		{
			return _musicScoreMakerData;
		}

		private long GetCurrentMusicScoreStartTicksHandler(GetCurrentMusicScoreStartTicksEvent evt)
		{
			return _currentFocusTicks;
		}

		private float GetCurrentMusicScoreScaleHandler(GetCurrentMusicScoreScaleEvent evt)
		{
			return _currentMusicScoreScale;
		}

		private long GetFocusTicksHandler(GetFocusTicksEvent _)
		{
			return _currentFocusTicks;
		}

		public void UpdateMusicScore()
		{
			if (_musicScoreMakerData != null)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateMusicScoreEvent());
			}
		}

		public void RefreshMusicScore()
		{
			if (_musicScoreMakerData != null)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new RefreshMusicScoreEvent());
			}
		}

		public void SeekToTicks(long ticks)
		{
			CurrentFocusTicks = ticks;
		}

		public void SeekToTime(float timeInSeconds)
		{
			if (_musicScoreMakerData?.MusicScoreEventDataList == null)
			{
				return;
			}

			CurrentFocusTicks = MusicScoreMakerUtility.GetTicksFromTime(timeInSeconds, _musicScoreMakerData.MusicScoreEventDataList);
		}

		public float GetCurrentTime()
		{
			return _musicScoreMakerData != null ? _musicScoreMakerData.GetTimeFromTicks(_currentFocusTicks) : 0f;
		}

		public void SyncWithMusicPlayback(float currentMusicTimeInSeconds)
		{
			SeekToTime(currentMusicTimeInSeconds);
		}

		public void ScrollByTicks(long deltaTicks)
		{
			if (deltaTicks == 0)
			{
				return;
			}

			_currentFocusTicks += deltaTicks;
			UpdateMusicScore();
		}

		public void ScrollByTime(float deltaTime)
		{
			if (_musicScoreMakerData?.MusicScoreEventDataList == null)
			{
				return;
			}

			SeekToTime(_musicScoreMakerData.GetTimeFromTicks(_currentFocusTicks) + deltaTime);
		}

		public void SetScale(float scale)
		{
			CurrentMusicScoreScale = scale;
		}

		public void Initialize(MusicScoreMakerData data, long initialTicks = 0L, float initialScale = 1f)
		{
			if (data == null)
			{
				LogUtility.LogError("MusicScoreMakerDataがnullです。初期化できません。");
				return;
			}

			SetupMusicScorePreview();
			_musicScoreMakerData = data;
			_currentFocusTicks = initialTicks;
			_currentMusicScoreScale = initialScale;
			RefreshMusicScore();
		}

		public void SetActivePreview(bool value)
		{
			_musicScorePreview.gameObject.SetActive(value);
		}

		public StandaloneMusicScorePreviewController()
		{
			_currentMusicScoreScale = 1f;
		}
	}
}
