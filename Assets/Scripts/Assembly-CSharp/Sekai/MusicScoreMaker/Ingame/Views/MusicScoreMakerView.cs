using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMakerView : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSetup_003Ed__34 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerView _003C_003E4__this;

			public int musicId;

			public CancellationToken cancellationToken;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					_003C_003E4__this?.SetupCoreAsync(cancellationToken, musicId).Forget();
					_003C_003Et__builder.SetResult();
				}
				catch (Exception exception)
				{
					_003C_003Et__builder.SetException(exception);
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CUpdateBackground2DJacketAsync_003Ed__42 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerView _003C_003E4__this;

			public int musicId;

			public CancellationToken ct;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					_003C_003E4__this?.UpdateBackground2DJacketCoreAsync(musicId, ct).Forget();
					_003C_003Et__builder.SetResult();
				}
				catch (Exception exception)
				{
					_003C_003Et__builder.SetException(exception);
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[SerializeField]
		private MusicScorePreview _musicScorePreview;

		[SerializeField]
		private MusicScoreSelectArea _musicScoreSelectArea;

		[SerializeField]
		private MusicPlayTimeView _musicPlayTimeView;

		[SerializeField]
		private MusicScoreMakerBackground2DView _background2DView;

		[SerializeField]
		private ClipboardButtonView _clipboardButtonView;

		[SerializeField]
		private CustomButton _backButton;

		[SerializeField]
		private CustomButton _scrollLowerLimitButton;

		[SerializeField]
		private CustomButton _scrollUpperLimitButton;

		[SerializeField]
		private CustomButton _undoButton;

		[SerializeField]
		private CustomButton _redoButton;

		[SerializeField]
		private UIPartsLeftTabList _leftTabList;

		private static readonly IsEditRestrictedEvent IsEditRestrictedEventCache;

		private static readonly IsMusicPlayingEvent IsMusicPlayingEventCache;

		[SerializeField]
		private InvalidPlacementMessageView _invalidPlacementMessageView;

		private Action _presenterUpdateAction;

		[SerializeField]
		private GameObject _toolWindowObject;

		private SubWindowSlideAnimationController[] _subWindowSlideAnimationControllers;

		[SerializeField]
		private QuantizeSettingsView _quantizeSettingsView;

		[SerializeField]
		private ZoomScaleInputView _zoomScaleInputView;

		private Vector2 _originalSize;

		public RectTransform NotesViewRectTransform
		{
			get
			{
				return _musicScorePreview != null ? _musicScorePreview.NotesViewRectTransform : null;
			}
		}

		public RectTransform EventsViewRectTransform
		{
			get
			{
				return _musicScorePreview != null ? _musicScorePreview.EventsViewRectTransform : null;
			}
		}

		public RectTransform RectTransform
		{
			get
			{
				return _musicScorePreview != null ? _musicScorePreview.RectTransform : null;
			}
		}

		public Dictionary<int, NotePreview> NoteDict
		{
			get
			{
				return _musicScorePreview != null ? _musicScorePreview.NotesPreview?.NoteDict : null;
			}
		}

		public Dictionary<long, MusicEventBalloonPreview> BalloonDict
		{
			get
			{
				return _musicScorePreview != null ? _musicScorePreview.MusicScoreEventsPreview?.BalloonDict : null;
			}
		}

		public MusicScorePreview MusicScorePreview
		{
			get
			{
				return _musicScorePreview;
			}
		}

		public MusicPlayTimeView MusicPlayTimeView
		{
			get
			{
				return _musicPlayTimeView;
			}
		}

		[AsyncStateMachine(typeof(_003CSetup_003Ed__34))]
		public UniTask Setup(CancellationToken cancellationToken, int musicId = 0)
		{
			return SetupCoreAsync(cancellationToken, musicId);
		}

		private async UniTask SetupCoreAsync(CancellationToken cancellationToken, int musicId)
		{
			InitializeToolWindowViews();
			SetToolWindowChildScale();
			SetToolWindowChildScale();
			SetScoreDisplayScale();

			// The original value comes from ClientConfig.MusicScoreMaker.LongNoteLinePoolCount.
			// ClientConfig is not restored in OpenSekai yet, so keep the original fallback count.
			const int longNoteLinePoolCount = 100;
			if (_musicScorePreview != null)
			{
				_musicScorePreview.Setup(600, longNoteLinePoolCount);
			}
			if (_musicPlayTimeView != null)
			{
				_musicPlayTimeView.Setup();
			}
			if (_musicScoreSelectArea != null)
			{
				_musicScoreSelectArea.Setup(_musicScorePreview);
			}
			if (_clipboardButtonView != null)
			{
				_clipboardButtonView.Setup();
			}
			if (_invalidPlacementMessageView != null)
			{
				_invalidPlacementMessageView.Setup();
			}
			if (_background2DView != null)
			{
				await _background2DView.SetupAsync(musicId, cancellationToken);
			}

			UpdateUndoRedoButtonsState();
			UpdateScrollLimitButtonsState();
			RegisterEvents();

			if (_backButton != null)
			{
				_backButton.onClick.AddListener(OnBackButtonClicked);
			}
			// Original calls UIPartsLeftTabListBase.SetScrollRect here. The local UI base
			// is still an empty shell with a mismatched signature, so defer it to that restore pass.
		}

		private void SetToolWindowChildScale()
		{
			if (_toolWindowObject == null)
			{
				return;
			}

			float scale = MusicScoreMakerSettingsManager.ToolWindowChildScale;
			foreach (Transform child in _toolWindowObject.transform)
			{
				child.localScale = new Vector3(scale, scale, 1f);
			}
		}

		private void OnToolWindowChildScaleChanged(ToolWindowChildScaleChangedEvent evt)
		{
			SetToolWindowChildScale();
		}

		private void OnScoreDisplayScaleChanged(ScoreDisplayScaleChangedEvent evt)
		{
			SetScoreDisplayScale();
		}

		private void SetScoreDisplayScale()
		{
			RectTransform rectTransform = _musicScorePreview != null ? _musicScorePreview.RectTransform : null;
			if (rectTransform == null)
			{
				return;
			}

			if ((_originalSize - Vector2.zero).sqrMagnitude < 1.0e-10f)
			{
				Rect rect = rectTransform.rect;
				_originalSize = new Vector2(rect.width, rect.height);
			}

			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _originalSize.x * MusicScoreMakerSettingsManager.ScoreDisplayScaleHorizontal);
			rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _originalSize.y * MusicScoreMakerSettingsManager.ScoreDisplayScaleVertical);
		}

		private void SetupToolWindow()
		{
			InitializeToolWindowViews();
			SetToolWindowChildScale();
		}

		private void InitializeToolWindowViews()
		{
			if (_toolWindowObject == null)
			{
				return;
			}

			_subWindowSlideAnimationControllers = _toolWindowObject.GetComponentsInChildren<SubWindowSlideAnimationController>(true);
			NoteChangeButtonView noteChangeButtonView = _toolWindowObject.GetComponentInChildren<NoteChangeButtonView>(true);
			if (noteChangeButtonView != null)
			{
				noteChangeButtonView.Initialize();
			}
			ClipboardCacheListView clipboardCacheListView = _toolWindowObject.GetComponentInChildren<ClipboardCacheListView>(true);
			if (clipboardCacheListView != null)
			{
				clipboardCacheListView.Initialize();
			}
			if (_quantizeSettingsView != null)
			{
				_quantizeSettingsView.Setup();
			}
			if (_zoomScaleInputView != null)
			{
				_zoomScaleInputView.Setup();
			}
		}

		public bool TryExecuteBackKeyProcessOnOpenedSubWindow()
		{
			if (_subWindowSlideAnimationControllers == null)
			{
				return false;
			}

			foreach (SubWindowSlideAnimationController controller in _subWindowSlideAnimationControllers)
			{
				if (controller != null && controller.gameObject.activeSelf)
				{
					controller.ExecuteBackKeyProcess();
					return true;
				}
			}
			return false;
		}

		[AsyncStateMachine(typeof(_003CUpdateBackground2DJacketAsync_003Ed__42))]
		public UniTask UpdateBackground2DJacketAsync(int musicId, CancellationToken ct = default(CancellationToken))
		{
			return UpdateBackground2DJacketCoreAsync(musicId, ct);
		}

		public UniTask UpdateBackground2DJacketAsync(string jacketPath, CancellationToken ct = default(CancellationToken))
		{
			return UpdateBackground2DJacketCoreAsync(jacketPath, ct);
		}

		private async UniTask UpdateBackground2DJacketCoreAsync(int musicId, CancellationToken ct)
		{
			if (_background2DView != null && musicId >= 1)
			{
				await _background2DView.SetJacketAsync(musicId, ct);
			}
		}

		private async UniTask UpdateBackground2DJacketCoreAsync(string jacketPath, CancellationToken ct)
		{
			if (_background2DView != null)
			{
				await _background2DView.SetJacketFileAsync(jacketPath, ct);
			}
		}

		public void DeactivatePreviewForTransition()
		{
			if (_musicScorePreview != null)
			{
				_musicScorePreview.gameObject.SetActive(false);
			}
		}

		public void ReactivatePreviewAfterTransition()
		{
			if (_musicScorePreview != null)
			{
				_musicScorePreview.gameObject.SetActive(true);
			}
		}

		public void Dispose()
		{
			RemoveEvents();
			if (_backButton != null)
			{
				_backButton.onClick.RemoveListener(OnBackButtonClicked);
			}

			_subWindowSlideAnimationControllers = null;
			if (_toolWindowObject != null)
			{
				Destroy(_toolWindowObject.gameObject);
				_toolWindowObject = null;
			}
			if (_musicScorePreview != null)
			{
				_musicScorePreview.Dispose();
			}
			if (_musicPlayTimeView != null)
			{
				_musicPlayTimeView.Dispose();
			}
			if (_clipboardButtonView != null)
			{
				_clipboardButtonView.Dispose();
			}
			if (_background2DView != null)
			{
				_background2DView.Dispose();
			}
			if (_musicScoreSelectArea != null)
			{
				_musicScoreSelectArea.Dispose();
			}
		}

		private void OnBackButtonClicked()
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new BackKeyPressedEvent());
		}

		public void SetSelectAreaRect(PointerEventData pointerEventData)
		{
			_musicScoreSelectArea?.SetSelectAreaRect(pointerEventData);
		}

		public void SetActiveSelectArea(bool value)
		{
			_musicScoreSelectArea?.SetActiveSelectArea(value);
		}

		public int FindRayHitNoteLine(PointerEventData pointerEventData)
		{
			return _musicScorePreview?.NotesPreview?.LongNoteLinesPreview != null ? _musicScorePreview.NotesPreview.LongNoteLinesPreview.FindRayHitNoteLine(pointerEventData) : -1;
		}

		public void SetUpdatePresenterAction(Action action)
		{
			_presenterUpdateAction = action;
		}

		private void OnUpdateMusicScore(UpdateMusicScoreEvent obj)
		{
			UpdateScrollLimitButtonsState();
		}

		private void UpdateScrollLimitButtonsState()
		{
			bool isMusicPlaying = MusicScoreMakerUtility.IsMusicPlaying();
			long focusTicks = MusicScoreMakerUtility.GetFocusTicks();
			long ticksMax = MusicScoreMakerUtility.GetMusicScoreTicksMax();
			if (_scrollLowerLimitButton != null)
			{
				bool enabled = focusTicks > 0L && !isMusicPlaying;
				if (!enabled && _scrollLowerLimitButton.enabled)
				{
					_scrollLowerLimitButton.StopHoldRepeat();
				}
				_scrollLowerLimitButton.enabled = enabled;
			}
			if (_scrollUpperLimitButton != null)
			{
				bool enabled = focusTicks < ticksMax && !isMusicPlaying;
				if (!enabled && _scrollUpperLimitButton.enabled)
				{
					_scrollUpperLimitButton.StopHoldRepeat();
				}
				_scrollUpperLimitButton.enabled = enabled;
			}
		}

		private void OnPlayMusic(PlayMusicEvent evt)
		{
			UpdateScrollLimitButtonsState();
			UpdateUndoRedoButtonsState();
		}

		private void OnPauseMusic(PauseMusicEvent evt)
		{
			UpdateScrollLimitButtonsState();
			UpdateUndoRedoButtonsState();
		}

		private void OnToggleEditRestricted(ToggleEditRestrictedEvent evt)
		{
			UpdateUndoRedoButtonsState();
		}

		private void OnUndoRedoStackChanged(UndoRedoStackChangedEvent evt)
		{
			if (evt == null)
			{
				UpdateUndoRedoButtonsState();
				return;
			}
			UpdateUndoRedoButtonsState(evt.CanUndo, evt.CanRedo);
		}

		private void UpdateUndoRedoButtonsState()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			UpdateUndoRedoButtonsState(dispatcher.CanUndo, dispatcher.CanRedo);
		}

		private void UpdateUndoRedoButtonsState(bool canUndo, bool canRedo)
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			bool isEditRestricted = dispatcher.PublishFirst<IsEditRestrictedEvent, bool>(IsEditRestrictedEventCache);
			bool isMusicPlaying = dispatcher.PublishFirst<IsMusicPlayingEvent, bool>(IsMusicPlayingEventCache);
			bool enabled = !isEditRestricted && !isMusicPlaying;
			if (_undoButton != null)
			{
				_undoButton.enabled = canUndo && enabled;
			}
			if (_redoButton != null)
			{
				_redoButton.enabled = canRedo && enabled;
			}
		}

		private void Update()
		{
			_presenterUpdateAction?.Invoke();
		}

		private void RegisterEvents()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<ScoreDisplayScaleChangedEvent>(OnScoreDisplayScaleChanged);
			dispatcher.Register<ToolWindowChildScaleChangedEvent>(OnToolWindowChildScaleChanged);
			dispatcher.Register<UpdateMusicScoreEvent>(OnUpdateMusicScore);
			dispatcher.Register<PlayMusicEvent>(OnPlayMusic);
			dispatcher.Register<PauseMusicEvent>(OnPauseMusic);
			dispatcher.Register<ToggleEditRestrictedEvent>(OnToggleEditRestricted);
			dispatcher.Register<UndoRedoStackChangedEvent>(OnUndoRedoStackChanged);
		}

		private void RemoveEvents()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}

			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<ScoreDisplayScaleChangedEvent>(OnScoreDisplayScaleChanged);
			dispatcher.Remove<ToolWindowChildScaleChangedEvent>(OnToolWindowChildScaleChanged);
			dispatcher.Remove<UpdateMusicScoreEvent>(OnUpdateMusicScore);
			dispatcher.Remove<PlayMusicEvent>(OnPlayMusic);
			dispatcher.Remove<PauseMusicEvent>(OnPauseMusic);
			dispatcher.Remove<ToggleEditRestrictedEvent>(OnToggleEditRestricted);
			dispatcher.Remove<UndoRedoStackChangedEvent>(OnUndoRedoStackChanged);
		}

		public MusicScoreMakerView()
		{
		}

		static MusicScoreMakerView()
		{
			IsEditRestrictedEventCache = new IsEditRestrictedEvent();
			IsMusicPlayingEventCache = new IsMusicPlayingEvent();
		}
	}
}
