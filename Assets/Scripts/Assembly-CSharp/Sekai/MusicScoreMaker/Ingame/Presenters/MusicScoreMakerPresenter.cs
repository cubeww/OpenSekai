using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using CriWare;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.Live;
using Sekai.MusicScoreMaker.Common;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.MusicScoreMaker.Ingame.Views;
using Sekai.Service;
using UnityEngine;
using UnityEngine.EventSystems;
using MusicScorePreviewPlayData = Sekai.MusicScoreMaker.OutGame.MusicScorePreviewPlayData;

namespace Sekai.MusicScoreMaker.Ingame.Presenters
{
	public class MusicScoreMakerPresenter
	{
		private struct ConnectedNoteTypeChangeInfo
		{
			public MusicScoreNoteBase Note;

			public NoteType BeforeType;

			public MusicScoreNoteBase.NoteBaseType BeforeBaseType;
		}

		private struct ConnectedNoteGuideConversionInfo
		{
			public MusicScoreNoteBase Note;

			public NoteCategory BeforeCategory;

			public NoteType BeforeType;

			public MusicScoreNoteBase.NoteBaseType BeforeBaseType;
		}

		private struct NoteDataSnapshot
		{
			public MusicScoreNoteBase Note;

			public NoteCategory Category;

			public NoteDirection Direction;

			public NoteType Type;

			public NoteLineType LineType;

			public bool IsSkip;

			public MusicScoreNoteBase.NoteBaseType BaseType;
		}

		private struct ChangeNoteDataInfo
		{
			public NoteCategory BeforeCategory;

			public NoteDirection BeforeDirection;

			public NoteType BeforeType;

			public NoteLineType BeforeLineType;

			public bool BeforeIsSkip;

			public NoteCategory AfterCategory;

			public NoteDirection AfterDirection;

			public NoteType AfterType;

			public NoteLineType AfterLineType;

			public bool AfterIsSkip;

			public MusicScoreNoteBase TargetNote;
		}

		[StructLayout((LayoutKind)0, Size = 1)]
		private struct NoteTicksComparer : IComparer<MusicScoreNoteBase>
		{
			public int Compare(MusicScoreNoteBase x, MusicScoreNoteBase y)
			{
				if (ReferenceEquals(x, y))
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				int ticksCompare = x.ticks.CompareTo(y.ticks);
				return ticksCompare != 0 ? ticksCompare : x.id.CompareTo(y.id);
			}
		}

		private enum AreaSelectDragMode
		{
			Undecided = 0,
			Scroll = 1,
			AreaSelect = 2
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass309_0
		{
			public ScreenLayerMusicScoreMaker screenLayer;

			public string difficultyStr;

			public _003C_003Ec__DisplayClass309_0()
			{
			}

			internal bool _003CPlayTestLive_003Eb__0()
			{
				return screenLayer != null && screenLayer.IsSetupComplete;
			}

			internal bool _003CPlayTestLive_003Eb__1(MasterMusicDifficulty x)
			{
				return x != null && string.Equals(x.musicDifficulty, difficultyStr, StringComparison.OrdinalIgnoreCase);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass323_0
		{
			public int slotNo;

			public _003C_003Ec__DisplayClass323_0()
			{
			}

			internal bool _003CExecuteQuickSaveDraftAsync_003Eb__0(UserCustomMusicScoreDraft x)
			{
				return x != null && x.slotNo == slotNo;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass96_0
		{
			public int vocalId;

			public MasterMusicVocal vocal;

			public _003C_003Ec__DisplayClass96_0()
			{
			}

			internal bool _003CMusicReady_003Eb__0(MasterMusicVocal x)
			{
				return x != null && x.id == vocalId;
			}

			internal bool _003CMusicReady_003Eb__1(CriAtomEx.CueInfo x)
			{
				// TODO(original): match the original Cri cue name once MasterMusicVocal audio metadata is fully restored.
				return vocal != null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CApplyMusicSelectorDialog_003Ed__93 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerMusicSelectorDialog dialog;

			public MusicScoreMakerPresenter _003C_003E4__this;

			private int _003CmusicId_003E5__2;

			private int _003CvocalId_003E5__3;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCalculateBaseCustomScoreChangeRateAsync_003Ed__350 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<float?> _003C_003Et__builder;

			public string baseMusicScoreId;

			public CancellationToken ct;

			public MusicScoreMakerPresenter _003C_003E4__this;

			private UniTask<UserCustomMusicScorePublishedResponse>.Awaiter _003C_003Eu__1;

			private UniTask<MusicScoreMakerData>.Awaiter _003C_003Eu__2;

			private UniTask<float?>.Awaiter _003C_003Eu__3;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCheckBaseCustomScoreChangeAsync_003Ed__336 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public string baseMusicScoreId;

			public MusicScoreMakerPresenter _003C_003E4__this;

			private UniTask<UserCustomMusicScorePublishedResponse>.Awaiter _003C_003Eu__1;

			private UniTask<MusicScoreMakerData>.Awaiter _003C_003Eu__2;

			private UniTask<bool>.Awaiter _003C_003Eu__3;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCreate_003Ed__18 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<MusicScoreMakerPresenter> _003C_003Et__builder;

			public MusicScoreMakerModel model;

			public MusicScoreMakerView MusicScoreMakerView;

			public Action<Action, Action> finishTransitionCallback;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCreateMusicScore_003Ed__94 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			public string musicDifficulty;

			public int vocalId;

			public int musicId;

			public CancellationToken token;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCreateMusicScore_003Ed__95 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			public string musicDifficulty;

			public int vocalId;

			public MusicScoreMakerData musicScore;

			public int musicId;

			public long focusTicks;

			public CancellationToken token;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecutePublishAsync_003Ed__346 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			public MusicScorePreviewPlayData previewPlayData;

			private UniTask<bool>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteQuickSaveDraftAsync_003Ed__323 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			private _003C_003Ec__DisplayClass323_0 _003C_003E8__1;

			public bool isExitOnSave;

			private MusicScoreSaveDraftUpdateService _003Cservice_003E5__2;

			private UniTask<UserCustomMusicScoreDraftListResponse>.Awaiter _003C_003Eu__1;

			private UniTask.Awaiter _003C_003Eu__2;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CMusicReady_003Ed__96 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public int vocalId;

			public MusicScoreMakerPresenter _003C_003E4__this;

			public int musicId;

			private _003C_003Ec__DisplayClass96_0 _003C_003E8__1;

			private CancellationTokenSource _003CcancellationTokenSource_003E5__2;

			private string _003CbundleName_003E5__3;

			private CriAtomExPlayback? _003Cplayback_003E5__4;

			private UniTask<AssetManager.BundleElement>.Awaiter _003C_003Eu__1;

			private Cysharp.Threading.Tasks.YieldAwaitable.Awaiter _003C_003Eu__2;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayMusic_003Ed__79 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			private CriAtomExPlayback _003Cplayback_003E5__2;

			private CancellationTokenSource _003CcancellationTokenSource_003E5__3;

			private Cysharp.Threading.Tasks.YieldAwaitable.Awaiter _003C_003Eu__1;

			private UniTask.Awaiter _003C_003Eu__2;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayTestLive_003Ed__309 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerTestPlayDialog dialog;

			public MusicScoreMakerPresenter _003C_003E4__this;

			public bool isFromFullComboCheck;

			private _003C_003Ec__DisplayClass309_0 _003C_003E8__1;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSaveAndPostMusicScoreAsync_003Ed__334 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			private UniTask<bool>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSetup_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			public MenuScreenType fromScreenType;

			public CancellationToken token;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CShowNoteAndComboCountDialogAsync_003Ed__349 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			private MusicScoreMakerNoteAndComboCountDialog _003Cdialog_003E5__2;

			private CancellationToken _003Cct_003E5__3;

			private UniTask<float?>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CUpdateBackground2DJacketAsync_003Ed__22 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public int musicId;

			public MusicScoreMakerPresenter _003C_003E4__this;

			public CancellationToken ct;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CUpdatePlayingMusicCurrentMusicScoreStartTicks_003Ed__80 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			public CancellationTokenSource cancellationTokenSource;

			private long _003CbeforeTicks_003E5__2;

			private bool _003CisMusicEndedFlag_003E5__3;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CWaitForValidPlaybackTime_003Ed__90 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationTokenSource cancellationTokenSource;

			public MusicScoreMakerPresenter _003C_003E4__this;

			private int _003Ci_003E5__2;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CWaitMusicReady_003Ed__21 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerPresenter _003C_003E4__this;

			public CancellationToken token;

			private UniTask<int>.Awaiter _003C_003Eu__1;

			private UniTask.Awaiter _003C_003Eu__2;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private MusicScoreMakerModel _model;

		private MusicScoreMakerView _view;

		private MenuScreenType _fromScreenType;

		private readonly Action<Action, Action> _finishTransitionCallback;

		private bool _isFinishedFadeBGM;

		private CancellationTokenSource _musicReadyCts;

		private bool _isFirstMusicReady;

		private CancellationTokenSource _musicUpdateCts;

		private Dictionary<int, string> _tapSeDict;

		private Dictionary<NoteType, uint> _longPlaybackIds;

		private const string LIVE_NOTE_SE_BUNDLE_PREFIX = "live/tap_se/";

		private const string SE_CANCEL_CUE_NAME = "SE_CANCEL";

		private const string SE_DECIDE_CUE_NAME = "SE_DECIDE1";

		private const string SE_SCORE_NOTES_SET_CUE_NAME = "SE_SCORE_NOTES_SET";

		private const string SE_SCORE_NOTES_MOVE_CUE_NAME = "SE_SCORE_NOTES_MOVE";

		private const string SE_SCORE_NOTES_EXTEND_CUE_NAME = "SE_SCORE_NOTES_EXTEND";

		private const string SE_SCORE_NOTES_SHRINK_CUE_NAME = "SE_SCORE_NOTES_SHRINK";

		private const string SE_SCORE_NOTES_CHOICE_CUE_NAME = "SE_SCORE_NOTES_CHOICE";

		private const string SE_UI_SCROLL_CUE_NAME = "SE_UI_SCROLL";

		private const string SE_UI_GAUGE_SLIDER_UP_CUE_NAME = "SE_UI_GAUGE_SLIDER_UP";

		private const string SE_UI_GAUGE_SLIDER_DOWN_CUE_NAME = "SE_UI_GAUGE_SLIDER_DOWN";

		private readonly List<NoteType> _workStopLongNoteTypes;

		private const long DefaultLongNoteLengthTicks = 240L;

		private readonly List<long> _workTicksForLongNoteCategoryRecheck;

		private static readonly NoteTicksComparer _noteTicksComparer;

		private PointerEventData _musicScorePreviewDragPointerEventData;

		private float _accumulatedScrollDeltaTicks;

		private float _scrollVelocityTicksPerSecond;

		private float _smoothedScrollVelocity;

		private const float SCROLL_VELOCITY_SMOOTHING = 0.4f;

		private bool _isApplyingInertiaScroll;

		private readonly SetFocusTicksEvent _setFocusTicksEventForInertia;

		private readonly UpdateTimelineSliderValueWithoutNotifyEvent _updateTimelineSliderEventForInertia;

		private const float SCROLL_INERTIA_DECAY_PER_SECOND = 0.04f;

		private const float SCROLL_INERTIA_MAX_VELOCITY = 200000f;

		private const float SCROLL_INERTIA_VELOCITY_EPSILON = 10f;

		private const float SCROLL_INERTIA_MIN_DELTA_TIME = 1E-06f;

		private const float AREA_SELECT_DRAG_DEAD_ZONE = 15f;

		private const float VERTICAL_DRAG_ANGLE_THRESHOLD = 30f;

		private AreaSelectDragMode _areaSelectDragMode;

		private bool _isTemporaryAreaSelectionActive;

		private static readonly GetFocusTicksEvent _getFocusTicksEvent;

		private List<MusicScoreNoteBase> _pasteNoteListCache;

		private Dictionary<int, int> _pasteIdMappingCache;

		private List<MusicScoreEventData> _pasteEventListCache;

		private readonly HashSet<long> _pasteEditedTicksCache;

		private List<int> _tempSelectedNoteIds;

		private long _prevDragDeltaTicks;

		private int _prevDragDeltaLane;

		private long _prevDragQuantizedTicks;

		private int _prevExpandLeftDeltaLane;

		private int _prevExpandRightDeltaLane;

		private long _dragStartFocusTicks;

		private readonly List<int> _cycleSelectionNoteIds;

		private int _cycleSelectionIndex;

		private readonly List<int> _overlappingNotesCache;

		private bool _maxFocusableTicksCacheValid;

		private int _maxFocusableTicksEventHashCache;

		private int _maxFocusableTicksMasterMusicSecCache;

		private float _maxFocusableTicksFillerSecCache;

		private long _maxFocusableTicksCache;

		private readonly List<long> _workInvalidTicks;

		private long _lastJumpedTicks;

		private int _sameTicksCount;

		private int _sameTicksSubIndex;

		public MusicScoreMakerModel Model
		{
			get
			{
				return _model;
			}
		}

		private void SetupBackKeyEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Register<BackKeyPressedEvent>(OnBackKeyPressed);
		}

		private void DisposeBackKeyEventDispatcher()
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Remove<BackKeyPressedEvent>(OnBackKeyPressed);
			}
		}

		private void OnBackKeyPressed(BackKeyPressedEvent e)
		{
			HandleBackKey();
		}

		private void OnExecuteBackKeyProcess()
		{
			HandleBackKey();
		}

		private void HandleBackKey()
		{
			if (_model != null && _model.HasUnsavedChanges())
			{
				OpenSaveConfirmationDialog();
				return;
			}
			ExitToOutGame();
		}

		private void OpenSaveConfirmationDialog()
		{
			// TODO(original): restore localized CommonMultiButtonDialog. The simplified path keeps
			// back navigation functional while save/load dialogs are being restored.
			ExitToOutGame();
		}

		private void OnCancelDialog(CommonMultiButtonDialog dialog)
		{
			dialog?.Close();
		}

		private void OnSaveAndExit(CommonMultiButtonDialog dialog)
		{
			dialog?.Close();
			ExitToOutGame();
		}

		private void OnDiscardAndExit(CommonMultiButtonDialog dialog)
		{
			dialog?.Close();
			ExitToOutGame();
		}

		private void ExitToOutGame()
		{
			if (_model?.CustomMusicScoreEntry != null || _fromScreenType == MenuScreenType.MusicScoreMakerTop)
			{
				ReturnToMusicScoreMakerTop();
				return;
			}

			Dispose();
			if (ScreenManager.Instance != null)
			{
				ScreenManager.Instance.RemoveScreen(MenuScreenType.MusicScoreMaker);
			}
		}

		private void ReturnToMusicScoreMakerTop()
		{
			SaveCustomMusicScoreBeforeReturningToManager();
			MusicScoreMakerEntryPoint.BootData = null;
			ScreenManager screenManager = ScreenManager.Instance;
			if (screenManager != null && screenManager.GetScreenStackList().Contains(MenuScreenType.MusicScoreMakerTop))
			{
				Dispose();
				screenManager.BackUIScreenAndDestroyExitedScreen(MenuScreenType.MusicScoreMaker);
				return;
			}

			MusicScoreMakerUtility.RequestTransitionToOutGame(MenuScreenType.MusicScoreMakerTop);
			Dispose();
		}

		private void SaveCustomMusicScoreBeforeReturningToManager()
		{
			if (_model?.CustomMusicScoreEntry == null)
			{
				return;
			}

			if (!TrySaveCustomMusicScoreEntry())
			{
				UnityEngine.Debug.LogWarning("Failed to save custom music score before returning to manager.");
			}
		}

		private MusicScoreMakerPresenter(MusicScoreMakerModel model, MusicScoreMakerView view, Action<Action, Action> finishTransitionCallback)
		{
			_isFirstMusicReady = true;
			_longPlaybackIds = new Dictionary<NoteType, uint>();
			_workStopLongNoteTypes = new List<NoteType>(4);
			_workTicksForLongNoteCategoryRecheck = new List<long>(64);
			_setFocusTicksEventForInertia = new SetFocusTicksEvent();
			_updateTimelineSliderEventForInertia = new UpdateTimelineSliderValueWithoutNotifyEvent();
			_pasteNoteListCache = new List<MusicScoreNoteBase>();
			_pasteIdMappingCache = new Dictionary<int, int>();
			_pasteEventListCache = new List<MusicScoreEventData>();
			_pasteEditedTicksCache = new HashSet<long>();
			_tempSelectedNoteIds = new List<int>();
			_cycleSelectionNoteIds = new List<int>();
			_overlappingNotesCache = new List<int>();
			_workInvalidTicks = new List<long>(64);
			_lastJumpedTicks = -1L;
			_model = model;
			_view = view;
			_finishTransitionCallback = finishTransitionCallback;
		}

		[AsyncStateMachine(typeof(_003CCreate_003Ed__18))]
		public static UniTask<MusicScoreMakerPresenter> Create(MusicScoreMakerModel model, MusicScoreMakerView MusicScoreMakerView, CancellationToken token, Action<Action, Action> finishTransitionCallback)
		{
			token.ThrowIfCancellationRequested();
			return UniTask.FromResult(new MusicScoreMakerPresenter(model, MusicScoreMakerView, finishTransitionCallback));
		}

		public void RestoreQuantizeDivision(int division)
		{
			_model?.SetQuantizeDivision(division);
		}

		[AsyncStateMachine(typeof(_003CSetup_003Ed__20))]
		public UniTask Setup(MenuScreenType fromScreenType, CancellationToken token)
		{
			return SetupCore(fromScreenType, token);
		}

		[AsyncStateMachine(typeof(_003CWaitMusicReady_003Ed__21))]
		private UniTask WaitMusicReady(CancellationToken token)
		{
			return WaitMusicReadyCore(token);
		}

		[AsyncStateMachine(typeof(_003CUpdateBackground2DJacketAsync_003Ed__22))]
		private UniTask UpdateBackground2DJacketAsync(int musicId, CancellationToken ct = default(CancellationToken))
		{
			return _view != null ? _view.UpdateBackground2DJacketAsync(musicId, ct) : UniTask.CompletedTask;
		}

		private UniTask UpdateBackground2DJacketAsync(string jacketPath, CancellationToken ct = default(CancellationToken))
		{
			return _view != null ? _view.UpdateBackground2DJacketAsync(jacketPath, ct) : UniTask.CompletedTask;
		}

		public void Dispose()
		{
			DisposeTapSe();
			_model?.Dispose();
			_view?.Dispose();
			DisposeEventDispatcher();
			_musicReadyCts?.Cancel();
			_musicReadyCts?.Dispose();
			_musicReadyCts = null;
			_musicUpdateCts?.Cancel();
			_musicUpdateCts?.Dispose();
			_musicUpdateCts = null;
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.ClearSavedUndoRedoStackIfNotTestPlay();
				MusicScoreMakerEventDispatcher.Instance.Dispose();
			}
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.EditGuard = OnEditGuardForFullCombo;
			SetupEventDataEventDispatcher();
			SetupNoteEventDispatcher();
			SetupOperationEventDispatcher();
			SetupPlaybackEventDispatcher();
			SetupPreviewEventDispatcher();
			SetupTimelineEventDispatcher();
			SetupToolEventDispatcher();
			SetupBackKeyEventDispatcher();
		}

		private void DisposeEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.EditGuard = null;
			DisposeEventDataEventDispatcher();
			DisposeNoteEventDispatcher();
			DisposeOperationEventDispatcher();
			DisposePlaybackEventDispatcher();
			DisposePreviewEventDispatcher();
			DisposeTimelineEventDispatcher();
			DisposeToolEventDispatcher();
			DisposeBackKeyEventDispatcher();
		}

		private void Update()
		{
			UpdateMusicTime();
			UpdateScrollInertia();
		}

		private async UniTask SetupCore(MenuScreenType fromScreenType, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			_fromScreenType = fromScreenType;
			if (MusicScoreMakerEventDispatcher.ExistsInstance && MusicScoreMakerEventDispatcher.Instance.HasSavedUndoRedoStack())
			{
				MusicScoreMakerEventDispatcher.Instance.RestoreUndoRedoStack();
			}
			SetupEventDispatcher();
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.ResetAutoSaveTimer();
			}

			_isFinishedFadeBGM = true;
			if (_finishTransitionCallback != null)
			{
				_isFinishedFadeBGM = false;
				_finishTransitionCallback(null, () => _isFinishedFadeBGM = true);
			}

			await UniTask.WhenAll(WaitMusicReady(token), _view != null ? _view.Setup(token, _model?.MusicId ?? 0) : UniTask.CompletedTask);
			if (_model?.CustomMusicScoreEntry != null)
			{
				// MusicScoreMakerView.Setup initializes Background2DView and clears jacket renderers.
				// Re-apply the local jacket after that setup pass so entry artwork survives boot.
				await UpdateBackground2DJacketAsync(_model.CustomMusicScoreEntry.JacketPath, token);
			}
			if (_view != null)
			{
				_view.SetUpdatePresenterAction(Update);
			}
			MusicScoreMakerRuleSlideTutorialUtility.TryShowTutorialSlideIfFirstTime("music_score_maker");
		}

		private async UniTask WaitMusicReadyCore(CancellationToken token)
		{
			int result = await UniTask.WhenAny(UniTask.WaitUntil(() => _isFinishedFadeBGM, cancellationToken: token), UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: token));
			if (result == 1)
			{
				UnityEngine.Debug.LogError("MusicScoreMaker fade callback timeout.");
				_isFinishedFadeBGM = true;
			}
			await MusicReady(_model?.MusicId ?? 0, _model?.VocalId ?? 0);
		}

		private void SetupEventDataEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<ShowAddMusicScoreEventDataDialogEvent>(ShowAddMusicScoreEventDataDialog);
			dispatcher.Register<RemoveMusicScoreEventDataEvent>(RemoveMusicScoreEventData);
			dispatcher.Register<OnClickMusicScoreEventDataEvent>(OnClickMusicScoreEventData);
			dispatcher.Register<OnEventPreviewDragEvent>(OnEventPreviewDrag);
			dispatcher.Register<OnEventPreviewPointerUpEvent>(OnEventPreviewPointerUp);
			dispatcher.Register<EnterBPMEventSettingModeEvent>(EnterBPMEventSettingMode);
			dispatcher.Register<EnterHighSpeedEventSettingModeEvent>(EnterHighSpeedEventSettingMode);
			dispatcher.Register<EnterTimeSignatureEventSettingModeEvent>(EnterTimeSignatureEventSettingMode);
			dispatcher.Register<OnClickEventSettingTicksEvent>(OnClickEventSettingTicks);
			dispatcher.Register<GetIsEventSettingModeEvent, bool>(GetIsEventSettingMode);
			dispatcher.Register<GetSelectedEventSettingModeTypeEvent, MusicScoreEventType?>(GetSelectedEventSettingModeType);
		}

		private void DisposeEventDataEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<ShowAddMusicScoreEventDataDialogEvent>(ShowAddMusicScoreEventDataDialog);
			dispatcher.Remove<RemoveMusicScoreEventDataEvent>(RemoveMusicScoreEventData);
			dispatcher.Remove<OnClickMusicScoreEventDataEvent>(OnClickMusicScoreEventData);
			dispatcher.Remove<OnEventPreviewDragEvent>(OnEventPreviewDrag);
			dispatcher.Remove<OnEventPreviewPointerUpEvent>(OnEventPreviewPointerUp);
			dispatcher.Remove<EnterBPMEventSettingModeEvent>(EnterBPMEventSettingMode);
			dispatcher.Remove<EnterHighSpeedEventSettingModeEvent>(EnterHighSpeedEventSettingMode);
			dispatcher.Remove<EnterTimeSignatureEventSettingModeEvent>(EnterTimeSignatureEventSettingMode);
			dispatcher.Remove<OnClickEventSettingTicksEvent>(OnClickEventSettingTicks);
			dispatcher.Remove<GetIsEventSettingModeEvent, bool>(GetIsEventSettingMode);
			dispatcher.Remove<GetSelectedEventSettingModeTypeEvent, MusicScoreEventType?>(GetSelectedEventSettingModeType);
		}

		private void ShowAddMusicScoreEventDataDialog(ShowAddMusicScoreEventDataDialogEvent obj)
		{
			if (_model?.MusicScoreMakerData == null || obj == null)
			{
				return;
			}
			MusicScoreMakerData data = _model.MusicScoreMakerData;
			if (data.MusicScoreEventDataList == null)
			{
				UnityEngine.Debug.LogError("MusicScoreEventDataList is null.");
				return;
			}

			MusicScoreEventData existing = FindExistingEventAtTicks(data.MusicScoreEventDataList, obj.Ticks, obj.EventType);
			bool isUpdate = existing != null;
			int eventId = isUpdate ? existing.id : data.GetNewId();
			AddMusicScoreEventDataDialog dialog = null;
			Action onClickDelete = isUpdate ? () => RemoveMusicScoreEventData(eventId) : null;
			dialog = ScreenManager.Instance?.Show2ButtonDialog<AddMusicScoreEventDataDialog>(
				DialogType.AddMusicScoreEventDataDialog,
				null,
				"WORD_DECIDE",
				"WORD_CANCEL",
				() => AddMusicScoreEventData(dialog, data, eventId, obj.EventType, obj.Ticks, isUpdate),
				null,
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				allowCloseExternal: true);
			if (dialog == null)
			{
				return;
			}

			switch (obj.EventType)
			{
			case MusicScoreEventType.BPM:
				dialog.Setup(obj.EventType, existing != null
					? GetBpmFromChangeValue(existing.changeValue)
					: (decimal)MusicScoreMakerUtility.GetBpmAtTicks(obj.Ticks, data.MusicScoreEventDataList));
				break;
			case MusicScoreEventType.HighSpeed:
				dialog.Setup(obj.EventType, initialHighSpeed: existing != null
					? GetHighSpeedFromChangeValue(existing.changeValue)
					: MusicScoreMakerUtility.GetHighSpeedAtTicks(obj.Ticks, data.MusicScoreEventDataList));
				break;
			case MusicScoreEventType.TimeSignature:
				dialog.Setup(obj.EventType, initialTimeSignature: existing != null
					? MusicScoreMakerUtility.GetTimeSignatureFromChangeValue(existing.changeValue)
					: MusicScoreMakerUtility.GetTimeSignatureAtTicks(obj.Ticks, data.MusicScoreEventDataList));
				break;
			default:
				dialog.Setup(obj.EventType);
				break;
			}

			if (isUpdate && obj.Ticks != 0L)
			{
				dialog.ShowDeleteButton(onClickDelete);
			}
			else
			{
				dialog.HideDeleteButton();
			}
		}

		private static MusicScoreEventData FindExistingEventAtTicks(List<MusicScoreEventData> list, long ticks, MusicScoreEventType eventType)
		{
			if (list == null)
			{
				return null;
			}
			return list.Find(x => x != null && x.ticks == ticks && x.eventType == eventType);
		}

		private static MusicScoreEventData FindEventById(List<MusicScoreEventData> list, int id)
		{
			if (list == null)
			{
				return null;
			}
			return list.Find(x => x != null && x.id == id);
		}

		private static object GetDefaultEventChangeValue(MusicScoreEventType eventType)
		{
			return eventType switch
			{
				MusicScoreEventType.BPM => 120m,
				MusicScoreEventType.HighSpeed => 1f,
				MusicScoreEventType.SeVolume => 1f,
				MusicScoreEventType.TimeSignature => "4/4",
				_ => null
			};
		}

		private long SnapTicksForTimeSignatureEvent(long ticks, MusicScoreMakerData data, int eventId, bool isUpdate)
		{
			if (data == null)
			{
				return MusicScoreMakerUtility.SnapToBarStartOrTimeSignature(ticks, null);
			}

			MusicScoreEventData removedEventData = null;
			if (isUpdate)
			{
				removedEventData = FindEventById(data.MusicScoreEventDataList, eventId);
				if (removedEventData != null)
				{
					data.RemoveEvent(removedEventData);
				}
			}

			try
			{
				return MusicScoreMakerUtility.SnapToBarStartOrTimeSignature(ticks, data.MusicScoreEventDataList);
			}
			finally
			{
				if (removedEventData != null)
				{
					data.AddEvent(removedEventData);
				}
			}
		}

		private bool TryApplyBpmChangeWithNotesBeyondRangeConfirmation(MusicScoreEventData musicScoreEventData, MusicScoreMakerData data, MusicScoreEventData oldEventData, bool isUpdate, long ticks)
		{
			if (musicScoreEventData == null || data == null)
			{
				return false;
			}
			List<MusicScoreEventData> simulatedEventList = BuildSimulatedEventList(data.MusicScoreEventDataList, musicScoreEventData.id, musicScoreEventData);
			long newMaxTicks = ComputeMaxTicksForEventList(simulatedEventList);
			if (!CollectBeyondRangeData(data, musicScoreEventData.id, newMaxTicks, out List<MusicScoreNoteBase> removeNoteList, out HashSet<long> removeTicks, out List<MusicScoreEventData> removeEventList))
			{
				return false;
			}

			List<MusicScoreNoteBase> removeNoteListCopy = CopyList(removeNoteList);
			List<MusicScoreEventData> removeEventListCopy = removeEventList.Count > 0 ? CopyList(removeEventList) : null;
			CaptureSelectionState(out List<int> selectedNoteIdListCopy, out List<int> selectedEventIdListCopy, out List<int> selectedTemporaryEventIdListCopy);
			ScreenManager.Instance?.Show2ButtonDialog<Common2ButtonDialog>(
				DialogType.Common2ButtonDialog,
				"MSG_BPM_CHANGE_NOTES_OUTSIDE_AUDIO_REMOVE_CONFIRM",
				"WORD_DECIDE",
				"WORD_CANCEL",
				() => PushUndoableAction(
					() => UndoBpmChangeWithNotesRemoval(musicScoreEventData, data, oldEventData, isUpdate, removeNoteListCopy, removeEventListCopy, selectedNoteIdListCopy, selectedEventIdListCopy, selectedTemporaryEventIdListCopy, removeTicks),
					() => ExecuteBpmChangeWithNotesRemoval(musicScoreEventData, data, removeNoteList, removeEventList, removeTicks),
					skipEditGuard: false),
				null,
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				allowCloseExternal: true);
			return true;
		}

		private void ExecuteBpmChangeWithNotesRemoval(MusicScoreEventData musicScoreEventData, MusicScoreMakerData data, List<MusicScoreNoteBase> removeNoteList, List<MusicScoreEventData> removeEventList, HashSet<long> removeTicks)
		{
			if (data == null || musicScoreEventData == null)
			{
				return;
			}
			RemoveNotesAndEventsForBpmOperation(data, removeNoteList, removeEventList, removeTicks);
			data.RemoveEventsWhere(e => e != null && e.id == musicScoreEventData.id);
			data.AddEvent(musicScoreEventData);
			PublishEditedTicksAndRecheck(musicScoreEventData.ticks);
			NotifyMusicScoreAndTimelineChanged(refresh: true);
		}

		private void UndoBpmChangeWithNotesRemoval(MusicScoreEventData musicScoreEventData, MusicScoreMakerData data, MusicScoreEventData oldEventData, bool isUpdate, List<MusicScoreNoteBase> removeNoteListCopy, List<MusicScoreEventData> removeEventListCopy, List<int> selectedNoteIdListCopy, List<int> selectedEventIdListCopy, List<int> selectedTemporaryEventIdListCopy, HashSet<long> removeTicks)
		{
			if (data == null)
			{
				return;
			}
			InvalidateMaxFocusableTicksCache();
			data.RemoveEventsWhere(e => musicScoreEventData != null && e != null && e.id == musicScoreEventData.id);
			if (isUpdate && oldEventData != null)
			{
				data.AddEvent(oldEventData);
			}
			RestoreNotesAndEventsForBpmOperation(data, removeNoteListCopy, removeEventListCopy, selectedNoteIdListCopy, selectedEventIdListCopy, selectedTemporaryEventIdListCopy, removeTicks);
			if (musicScoreEventData != null)
			{
				PublishEditedTicksAndRecheck(musicScoreEventData.ticks);
			}
			NotifyMusicScoreAndTimelineChanged(refresh: true);
		}

		private bool TryRemoveBpmEventWithNotesBeyondRangeConfirmation(MusicScoreEventData musicScoreEventData, MusicScoreMakerData data)
		{
			if (musicScoreEventData == null || data == null)
			{
				return false;
			}
			List<MusicScoreEventData> simulatedEventList = BuildSimulatedEventList(data.MusicScoreEventDataList, musicScoreEventData.id, null);
			long newMaxTicks = ComputeMaxTicksForEventList(simulatedEventList);
			if (!CollectBeyondRangeData(data, musicScoreEventData.id, newMaxTicks, out List<MusicScoreNoteBase> removeNoteList, out HashSet<long> removeTicks, out List<MusicScoreEventData> removeEventList))
			{
				return false;
			}

			List<MusicScoreNoteBase> removeNoteListCopy = CopyList(removeNoteList);
			List<MusicScoreEventData> removeEventListCopy = removeEventList.Count > 0 ? CopyList(removeEventList) : null;
			CaptureSelectionState(out List<int> selectedNoteIdListCopy, out List<int> selectedEventIdListCopy, out List<int> selectedTemporaryEventIdListCopy);
			ScreenManager.Instance?.Show2ButtonDialog<Common2ButtonDialog>(
				DialogType.Common2ButtonDialog,
				"MSG_BPM_CHANGE_NOTES_OUTSIDE_AUDIO_REMOVE_CONFIRM",
				"WORD_DECIDE",
				"WORD_CANCEL",
				() => PushUndoableAction(
					() => UndoBpmRemovalWithNotesRemoval(musicScoreEventData, data, removeNoteListCopy, removeEventListCopy, selectedNoteIdListCopy, selectedEventIdListCopy, selectedTemporaryEventIdListCopy, removeTicks),
					() => ExecuteBpmRemovalWithNotesRemoval(musicScoreEventData, data, removeNoteList, removeEventList, removeTicks),
					skipEditGuard: false),
				null,
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				allowCloseExternal: true);
			return true;
		}

		private void ExecuteBpmRemovalWithNotesRemoval(MusicScoreEventData musicScoreEventData, MusicScoreMakerData data, List<MusicScoreNoteBase> removeNoteList, List<MusicScoreEventData> removeEventList, HashSet<long> removeTicks)
		{
			if (data == null || musicScoreEventData == null)
			{
				return;
			}
			data.RemoveEvent(musicScoreEventData);
			RemoveNotesAndEventsForBpmOperation(data, removeNoteList, removeEventList, removeTicks);
			PublishEditedTicksAndRecheck(musicScoreEventData.ticks);
			NotifyMusicScoreAndTimelineChanged(refresh: true);
		}

		private void UndoBpmRemovalWithNotesRemoval(MusicScoreEventData musicScoreEventData, MusicScoreMakerData data, List<MusicScoreNoteBase> removeNoteListCopy, List<MusicScoreEventData> removeEventListCopy, List<int> selectedNoteIdListCopy, List<int> selectedEventIdListCopy, List<int> selectedTemporaryEventIdListCopy, HashSet<long> removeTicks)
		{
			if (data == null)
			{
				return;
			}
			InvalidateMaxFocusableTicksCache();
			data.AddEvent(musicScoreEventData);
			RestoreNotesAndEventsForBpmOperation(data, removeNoteListCopy, removeEventListCopy, selectedNoteIdListCopy, selectedEventIdListCopy, selectedTemporaryEventIdListCopy, removeTicks);
			if (musicScoreEventData != null)
			{
				PublishEditedTicksAndRecheck(musicScoreEventData.ticks);
			}
			NotifyMusicScoreAndTimelineChanged(refresh: true);
		}

		private List<MusicScoreEventData> BuildSimulatedEventList(List<MusicScoreEventData> sourceList, int excludeId, MusicScoreEventData addEvent)
		{
			List<MusicScoreEventData> result = new List<MusicScoreEventData>();
			if (sourceList != null)
			{
				foreach (MusicScoreEventData eventData in sourceList)
				{
					if (eventData != null && eventData.id != excludeId)
					{
						result.Add(eventData);
					}
				}
			}
			if (addEvent != null)
			{
				result.Add(addEvent);
			}
			result.Sort((a, b) => a.ticks.CompareTo(b.ticks));
			return result;
		}

		private bool CollectBeyondRangeData(MusicScoreMakerData data, int excludeEventId, long newMaxTicks, out List<MusicScoreNoteBase> removeNoteList, out HashSet<long> removeTicks, out List<MusicScoreEventData> removeEventList)
		{
			removeNoteList = new List<MusicScoreNoteBase>();
			removeTicks = new HashSet<long>();
			removeEventList = new List<MusicScoreEventData>();
			if (data == null)
			{
				return false;
			}
			foreach (MusicScoreNoteBase note in data.NoteList ?? new List<MusicScoreNoteBase>())
			{
				if (note != null && note.ticks > newMaxTicks)
				{
					removeNoteList.Add(note);
					removeTicks.Add(note.ticks);
				}
			}
			foreach (MusicScoreEventData eventData in data.MusicScoreEventDataList ?? new List<MusicScoreEventData>())
			{
				if (eventData != null && eventData.id != excludeEventId && eventData.ticks > newMaxTicks)
				{
					removeEventList.Add(eventData);
					removeTicks.Add(eventData.ticks);
				}
			}
			return removeNoteList.Count > 0 || removeEventList.Count > 0;
		}

		private void CaptureSelectionState(out List<int> selectedNoteIdListCopy, out List<int> selectedEventIdListCopy, out List<int> selectedTemporaryEventIdListCopy)
		{
			MusicScoreMakerData data = _model?.MusicScoreMakerData;
			selectedNoteIdListCopy = CopyList(data?.SelectedNoteIdList);
			selectedEventIdListCopy = CopyList(data?.SelectedEventIdList);
			selectedTemporaryEventIdListCopy = CopyList(data?.SelectedTemporaryEventIdList);
		}

		private void RemoveNotesAndEventsForBpmOperation(MusicScoreMakerData data, List<MusicScoreNoteBase> removeNoteList, List<MusicScoreEventData> removeEventList, HashSet<long> removeTicks)
		{
			if (data == null)
			{
				return;
			}
			if (removeNoteList != null && removeNoteList.Count > 0)
			{
				RemoveNoteList(removeNoteList);
			}
			if (removeEventList != null && removeEventList.Count > 0)
			{
				foreach (MusicScoreEventData eventData in removeEventList)
				{
					if (eventData == null)
					{
						continue;
					}
					data.RemoveSelectedEvent(eventData.id);
					data.RemoveSelectedTemporaryEvent(eventData.id);
				}
				data.RemoveEventRange(removeEventList);
			}
			MusicScoreMakerUtility.AddEditedTicks(removeTicks);
			InvalidateMaxFocusableTicksCache();
		}

		private void RestoreNotesAndEventsForBpmOperation(MusicScoreMakerData data, List<MusicScoreNoteBase> removeNoteListCopy, List<MusicScoreEventData> removeEventListCopy, List<int> selectedNoteIdListCopy, List<int> selectedEventIdListCopy, List<int> selectedTemporaryEventIdListCopy, HashSet<long> removeTicks)
		{
			if (data == null)
			{
				return;
			}
			RestoreRemovedNoteList(removeNoteListCopy ?? new List<MusicScoreNoteBase>());
			data.AddEventRange(removeEventListCopy ?? new List<MusicScoreEventData>());
			data.ClearSelectedNotes();
			data.ClearSelectedEvents();
			data.ClearSelectedTemporaryEvents();
			data.AddSelectedNoteRange(selectedNoteIdListCopy ?? new List<int>());
			data.AddSelectedEventRange(selectedEventIdListCopy ?? new List<int>());
			data.AddSelectedTemporaryEventRange(selectedTemporaryEventIdListCopy ?? new List<int>());
			MusicScoreMakerUtility.AddEditedTicks(removeTicks);
			InvalidateMaxFocusableTicksCache();
		}

		private void PublishEditedTicksAndRecheck(long ticks)
		{
			MusicScoreMakerUtility.AddEditedTick(ticks);
			RecheckJudgmentNoteGapIfEditedInGapRange();
		}

		private void MarkEventEditTicksAndRecheck(MusicScoreEventData newEventData, MusicScoreEventData oldEventData, bool isUpdate)
		{
			if (newEventData == null)
			{
				return;
			}
			List<long> editedTicks = new List<long> { newEventData.ticks };
			if (isUpdate && oldEventData != null && oldEventData.ticks != newEventData.ticks)
			{
				editedTicks.Add(oldEventData.ticks);
			}
			MusicScoreMakerUtility.AddEditedTicks(editedTicks);
			RecheckJudgmentNoteGapIfEditedInGapRange();
		}

		private static bool IsTimelineLengthEvent(MusicScoreEventType eventType)
		{
			return eventType == MusicScoreEventType.BPM || eventType == MusicScoreEventType.TimeSignature;
		}

		private void LogBpmOperation(string operation, MusicScoreEventData eventData, int removeCount = -1)
		{
			UnityEngine.Debug.Log($"MusicScoreMaker BPM operation: {operation}, id={eventData?.id}, ticks={eventData?.ticks}, removed={removeCount}");
		}

		private List<T> CopyList<T>(List<T> source)
		{
			return source == null ? new List<T>() : new List<T>(source);
		}

		private static MusicScoreEventData CloneEvent(MusicScoreEventData source, bool preserveId)
		{
			if (source == null)
			{
				return null;
			}
			return new MusicScoreEventData
			{
				id = preserveId ? source.id : 0,
				eventType = source.eventType,
				ticks = source.ticks,
				changeValue = source.changeValue
			};
		}

		private static List<MusicScoreEventData> CloneEventsForUndo(List<MusicScoreEventData> events, bool preserveId)
		{
			List<MusicScoreEventData> result = new List<MusicScoreEventData>();
			if (events == null)
			{
				return result;
			}
			foreach (MusicScoreEventData eventData in events)
			{
				MusicScoreEventData clone = CloneEvent(eventData, preserveId);
				if (clone != null)
				{
					result.Add(clone);
				}
			}
			return result;
		}

		private void AddMusicScoreEventData(AddMusicScoreEventDataDialog dialog, MusicScoreMakerData data, int id, MusicScoreEventType eventType, long ticks, bool isUpdate)
		{
			if (data == null)
			{
				return;
			}
			MusicScoreEventData oldEventData = FindEventById(data.MusicScoreEventDataList, id);
			long eventTicks = eventType == MusicScoreEventType.TimeSignature
				? SnapTicksForTimeSignatureEvent(ticks, data, id, isUpdate)
				: ticks;
			eventTicks = ClampTicksToValidRange(eventTicks);
			MusicScoreEventData newEventData = new MusicScoreEventData
			{
				id = id > 0 ? id : data.GetNewId(),
				eventType = eventType,
				ticks = eventTicks,
				changeValue = GetEventChangeValue(eventType, dialog)
			};
			if (eventType == MusicScoreEventType.BPM && TryApplyBpmChangeWithNotesBeyondRangeConfirmation(newEventData, data, oldEventData, isUpdate, ticks))
			{
				return;
			}

			Action redo = () =>
			{
				if (IsTimelineLengthEvent(eventType))
				{
					InvalidateMaxFocusableTicksCache();
				}
				data.RemoveEventsWhere(e => e != null && e.id == newEventData.id);
				data.AddEvent(newEventData);
				MarkEventEditTicksAndRecheck(newEventData, oldEventData, isUpdate);
				NotifyMusicScoreAndTimelineChanged(refresh: false);
			};
			Action undo = () =>
			{
				if (IsTimelineLengthEvent(eventType))
				{
					InvalidateMaxFocusableTicksCache();
				}
				data.RemoveEventsWhere(e => e != null && e.id == newEventData.id);
				if (isUpdate && oldEventData != null)
				{
					data.AddEvent(oldEventData);
				}
				MarkEventEditTicksAndRecheck(newEventData, oldEventData, isUpdate);
				NotifyMusicScoreAndTimelineChanged(refresh: true);
			};
			PushUndoableAction(undo, redo, oldEventData?.ticks ?? newEventData.ticks, newEventData.ticks);
		}

		private object GetEventChangeValue(MusicScoreEventType eventType, AddMusicScoreEventDataDialog dialog)
		{
			if (dialog == null)
			{
				return GetDefaultEventChangeValue(eventType);
			}
			try
			{
				switch (eventType)
				{
				case MusicScoreEventType.BPM:
					return decimal.TryParse(dialog.InputFieldText, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out decimal bpm)
						|| decimal.TryParse(dialog.InputFieldText, out bpm)
						? bpm
						: 120m;
				case MusicScoreEventType.HighSpeed:
				case MusicScoreEventType.SeVolume:
					return float.TryParse(dialog.InputFieldText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float value)
						|| float.TryParse(dialog.InputFieldText, out value)
						? value
						: (float)GetDefaultEventChangeValue(eventType);
				case MusicScoreEventType.TimeSignature:
					string numerator = string.IsNullOrEmpty(dialog.InputFieldText) ? "4" : dialog.InputFieldText;
					string denominator = string.IsNullOrEmpty(dialog.DropdownText) ? "4" : dialog.DropdownText;
					return $"{numerator}/{denominator}";
				default:
					return null;
				}
			}
			catch
			{
				return GetDefaultEventChangeValue(eventType);
			}
		}

		private void RemoveMusicScoreEventData(RemoveMusicScoreEventDataEvent obj)
		{
			if (obj != null)
			{
				RemoveMusicScoreEventData(obj.Id);
			}
		}

		private void OnClickMusicScoreEventData(OnClickMusicScoreEventDataEvent obj)
		{
			if (_model?.MusicScoreMakerData == null || obj == null)
			{
				return;
			}
			if (_model.IsEditRestricted)
			{
				return;
			}

			int id = obj.Id;
			if (_model.IsEventSettingMode)
			{
				if (TryGetMusicScoreEventTypeById(id, out MusicScoreEventType eventType)
					&& _model.CurrentEventSettingType.HasValue
					&& eventType == _model.CurrentEventSettingType.Value)
				{
					EditMusicScoreEventData(id);
				}
				return;
			}

			if (_model.RemoveMode || _model.SelectedToolType == MusicScoreMakerUtility.ToolType.Remove)
			{
				RemoveMusicScoreEventData(id);
				return;
			}

			EditMusicScoreEventData(id);
		}

		private void EditMusicScoreEventData(int id)
		{
			MusicScoreMakerData data = _model?.MusicScoreMakerData;
			MusicScoreEventData eventData = FindEventById(data?.MusicScoreEventDataList, id);
			if (eventData == null)
			{
				return;
			}
			ShowAddMusicScoreEventDataDialog(new ShowAddMusicScoreEventDataDialogEvent
			{
				EventType = eventData.eventType,
				Ticks = eventData.ticks
			});
		}

		private static decimal? GetBpmFromChangeValue(object changeValue)
		{
			if (changeValue == null)
			{
				return null;
			}
			if (changeValue is decimal decimalValue)
			{
				return decimalValue;
			}
			if (changeValue is float floatValue)
			{
				return (decimal)floatValue;
			}
			if (changeValue is double doubleValue)
			{
				return (decimal)doubleValue;
			}
			if (changeValue is int intValue)
			{
				return intValue;
			}
			return decimal.TryParse(changeValue.ToString(), out decimal parsed) ? parsed : null;
		}

		private static float? GetHighSpeedFromChangeValue(object changeValue)
		{
			if (changeValue == null)
			{
				return null;
			}
			if (changeValue is float floatValue)
			{
				return floatValue;
			}
			if (changeValue is double doubleValue)
			{
				return (float)doubleValue;
			}
			if (changeValue is decimal decimalValue)
			{
				return (float)decimalValue;
			}
			if (changeValue is int intValue)
			{
				return intValue;
			}
			return float.TryParse(changeValue.ToString(), out float parsed) ? parsed : null;
		}

		private void OnEventPreviewDrag(OnEventPreviewDragEvent obj)
		{
			if (_model?.MusicScoreMakerData == null || obj == null)
			{
				return;
			}
			MusicScoreEventData eventData = FindEventById(_model.MusicScoreMakerData.MusicScoreEventDataList, obj.Id);
			if (eventData != null)
			{
				_model.MusicScoreMakerData.SelectedTargetOperation.deltaTicks = 0L;
			}
		}

		private void OnEventPreviewPointerUp(OnEventPreviewPointerUpEvent obj)
		{
			NotifyMusicScoreAndTimelineChanged();
		}

		private void RemoveMusicScoreEventData(int id)
		{
			MusicScoreMakerData data = _model?.MusicScoreMakerData;
			MusicScoreEventData eventData = FindEventById(data?.MusicScoreEventDataList, id);
			if (data == null || eventData == null)
			{
				return;
			}
			if (IsTimelineLengthEvent(eventData.eventType) && TryRemoveBpmEventWithNotesBeyondRangeConfirmation(eventData, data))
			{
				return;
			}

			Action redo = () =>
			{
				if (IsTimelineLengthEvent(eventData.eventType))
				{
					InvalidateMaxFocusableTicksCache();
				}
				data.RemoveEvent(eventData);
				data.RemoveSelectedEvent(id);
				data.RemoveSelectedTemporaryEvent(id);
				PublishEditedTicksAndRecheck(eventData.ticks);
				NotifyMusicScoreAndTimelineChanged(refresh: true);
			};
			Action undo = () =>
			{
				if (IsTimelineLengthEvent(eventData.eventType))
				{
					InvalidateMaxFocusableTicksCache();
				}
				data.AddEvent(eventData);
				PublishEditedTicksAndRecheck(eventData.ticks);
				NotifyMusicScoreAndTimelineChanged(refresh: true);
			};
			PushUndoableAction(undo, redo);
		}

		private void EnterBPMEventSettingMode(EnterBPMEventSettingModeEvent obj)
		{
			EnterEventSettingMode(MusicScoreEventType.BPM);
		}

		private void EnterHighSpeedEventSettingMode(EnterHighSpeedEventSettingModeEvent obj)
		{
			EnterEventSettingMode(MusicScoreEventType.HighSpeed);
		}

		private void EnterTimeSignatureEventSettingMode(EnterTimeSignatureEventSettingModeEvent obj)
		{
			EnterEventSettingMode(MusicScoreEventType.TimeSignature);
		}

		private void DisableEventSettingModeInternal()
		{
			if (_model == null)
			{
				return;
			}

			MusicScoreEventType? currentEventSettingType = _model.CurrentEventSettingType;
			bool restoreTimeSignatureScale = _model.IsEventSettingMode && currentEventSettingType == MusicScoreEventType.TimeSignature;
			_view?.MusicPlayTimeView?.SetFocusTickActive(true);
			_view?.MusicPlayTimeView?.HideEventSettingModeObject();
			_model.DisableEventSettingMode();
			_model.SelectedEventSettingModeType = null;
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateButtonSelectionStateEvent());
				if (currentEventSettingType.HasValue)
				{
					MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateMusicScoreEvent());
				}
			}
			if (restoreTimeSignatureScale)
			{
				_model.CurrentMusicScoreScale = _model.PreviousMusicScoreScale;
				PublishEventSettingViewportRefresh();
				PublishZoomScaleChanged();
			}
		}

		private void PublishZoomScaleChanged()
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance && _model != null)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new ZoomTimelineScaleChangedEvent
				{
					Scale = _model.CurrentMusicScoreScale
				});
			}
		}

		private void EnterEventSettingMode(MusicScoreEventType eventType)
		{
			if (_model == null)
			{
				return;
			}
			if (_model.IsEditRestricted)
			{
				return;
			}
			if (_model.IsEventSettingMode && _model.CurrentEventSettingType == eventType)
			{
				DisableEventSettingModeInternal();
				return;
			}

			if (_model.SelectedEventSettingModeType == eventType)
			{
				_model.SelectedEventSettingModeType = null;
			}
			else
			{
				_model.SelectedEventSettingModeType = eventType;
			}

			_model.IsNoteDataSelected = false;
			if (_model.IsEventSettingMode && _model.CurrentEventSettingType == MusicScoreEventType.TimeSignature)
			{
				_model.CurrentMusicScoreScale = _model.PreviousMusicScoreScale;
				PublishEventSettingViewportRefresh();
				PublishZoomScaleChanged();
			}

			MusicScoreMakerData data = _model.MusicScoreMakerData;
			data?.ClearSelectedNotes();
			data?.ClearSelectedEvents();
			_view?.MusicPlayTimeView?.SetFocusTickActive(false);
			MusicScoreMakerUtility.SetFocusTicks(MusicScoreMakerUtility.CalculateSnapQuantizedTicks(0L, MusicScoreMakerUtility.GetFocusTicks()));

			if (eventType == MusicScoreEventType.TimeSignature)
			{
				float previousScale = _model.CurrentMusicScoreScale;
				_model.CurrentMusicScoreScale = 4f;
				_model.PreviousMusicScoreScale = previousScale;
				PublishEventSettingViewportRefresh();
				PublishZoomScaleChanged();
			}

			_view?.MusicPlayTimeView?.ShowEventSettingModeObject(eventType);
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateMusicScoreEvent());
			}
			_model.EnableEventSettingMode(eventType);
			if (eventType == MusicScoreEventType.BPM || eventType == MusicScoreEventType.TimeSignature)
			{
				MusicScoreMakerRuleSlideTutorialUtility.TryShowTutorialSlideIfFirstTime("bpm");
			}
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateButtonSelectionStateEvent());
			}
		}

		private bool GetIsEventSettingMode(GetIsEventSettingModeEvent obj)
		{
			return _model != null && _model.IsEventSettingMode;
		}

		private MusicScoreEventType? GetSelectedEventSettingModeType(GetSelectedEventSettingModeTypeEvent obj)
		{
			return _model?.SelectedEventSettingModeType;
		}

		private void OnClickEventSettingTicks(OnClickEventSettingTicksEvent obj)
		{
			if (_model == null || !_model.CurrentEventSettingType.HasValue)
			{
				return;
			}
			MusicScoreEventType eventType = _model.CurrentEventSettingType.Value;
			long ticks = _view?.MusicPlayTimeView != null ? _view.MusicPlayTimeView.GetEventSettingModeSnappedTicks(eventType) : obj?.Ticks ?? 0L;
			MusicScoreMakerEventDispatcher.Instance.Publish(new ShowAddMusicScoreEventDataDialogEvent
			{
				EventType = eventType,
				Ticks = ticks
			});
		}

		private static void PublishEventSettingViewportRefresh()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Publish(new UpdateMusicScoreEvent());
			dispatcher.Publish(new UpdateMusicScoreSelectAreaEvent());
			dispatcher.Publish(new OnMusicScorePreviewDragEvent());
		}

		private void SetupPlaybackEventDispatcher()
		{
			SetupTapSe();
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<SwitchPlayPauseMusicEvent, bool>(SwitchPlayPauseMusic);
			dispatcher.Register<PlayMusicEvent>(OnPlayMusic);
			dispatcher.Register<PauseMusicEvent>(OnPauseMusic);
			dispatcher.Register<IsMusicPlayingEvent, bool>(IsMusicPlaying);
		}

		private void DisposePlaybackEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<SwitchPlayPauseMusicEvent, bool>(SwitchPlayPauseMusic);
			dispatcher.Remove<PlayMusicEvent>(OnPlayMusic);
			dispatcher.Remove<PauseMusicEvent>(OnPauseMusic);
			dispatcher.Remove<IsMusicPlayingEvent, bool>(IsMusicPlaying);
		}

		private bool SwitchPlayPauseMusic(SwitchPlayPauseMusicEvent evt)
		{
			if (_model == null || !MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return false;
			}
			_model.IsMusicPlaying = !_model.IsMusicPlaying;
			if (_model.IsMusicPlaying)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new PlayMusicEvent());
			}
			else
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new PauseMusicEvent());
			}
			return _model.IsMusicPlaying;
		}

		private void PauseMusic()
		{
			StopLongNoteSe();
			_musicUpdateCts?.Cancel();
			_musicUpdateCts?.Dispose();
			_musicUpdateCts = null;
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new HideMusicPlayTimePreviewEvent());
			}
			if (_model == null || !_model.IsMusicPlaying)
			{
				if (SoundManager.Instance.IsAudioSyncedUnityTimerEnabled)
				{
					SoundManager.Instance.StopIngame();
				}
			}
		}

		private void OnPlayMusic(PlayMusicEvent evt)
		{
			if (_model != null)
			{
				_model.IsMusicPlaying = true;
			}
			PlayMusic().Forget();
		}

		private void OnPauseMusic(PauseMusicEvent evt)
		{
			if (_model != null)
			{
				_model.IsMusicPlaying = false;
			}
			PauseMusic();
		}

		private bool IsMusicPlaying(IsMusicPlayingEvent evt)
		{
			return _model != null && _model.IsMusicPlaying;
		}

		[AsyncStateMachine(typeof(_003CPlayMusic_003Ed__79))]
		private async UniTask PlayMusic()
		{
			if (_model == null || _model.MusicScoreMakerData == null)
			{
				return;
			}
			_model.IsMusicPlaying = true;
			float currentTime = _model.MusicScoreMakerData.GetTimeFromTicks(_model.FocusTicks);
			if (_model.FillerSec < 0f)
			{
				_model.FillerSec = 0f;
			}
			float playbackStartTime = Mathf.Max(currentTime, 0f) + _model.FillerSec;
			// OpenSekai: official data keeps secForMusicScoreMaker within the playable BGM range.
			// Custom charts can exceed the audio length, so avoid seeking CRI/our shim past EOF.
			if (_model.MusicLength > 0L && playbackStartTime * 1000f >= _model.MusicLength)
			{
				_model.IsMusicPlaying = false;
				if (MusicScoreMakerEventDispatcher.ExistsInstance)
				{
					MusicScoreMakerEventDispatcher.Instance.Publish(new PauseMusicEvent());
				}
				return;
			}
			uint playbackId = SoundManager.Instance.PrepareIngameBGM(_model.AssetbundleName, playbackStartTime);
			CriAtomExPlayback? playback = SoundManager.Instance.GetPlayback(playbackId);
			_musicUpdateCts?.Cancel();
			_musicUpdateCts?.Dispose();
			_musicUpdateCts = new CancellationTokenSource();
			CancellationTokenSource cancellationTokenSource = _musicUpdateCts;
			while (playback.HasValue
				&& playback.Value.GetStatus() != CriAtomExPlayback.Status.Playing
				&& !cancellationTokenSource.IsCancellationRequested)
			{
				await UniTask.Yield();
			}
			await UniTask.Yield();
			if (cancellationTokenSource.IsCancellationRequested)
			{
				return;
			}
			SoundManager.Instance.ResumePreparedPlaybackIngame();
			await WaitForValidPlaybackTime(cancellationTokenSource);
			if (cancellationTokenSource.IsCancellationRequested)
			{
				return;
			}
			UpdatePlayingMusicCurrentMusicScoreStartTicks(cancellationTokenSource).Forget();
			if (_model != null && !_model.IsMusicPlaying)
			{
				PauseMusic();
			}
		}

		[AsyncStateMachine(typeof(_003CUpdatePlayingMusicCurrentMusicScoreStartTicks_003Ed__80))]
		private async UniTask UpdatePlayingMusicCurrentMusicScoreStartTicks(CancellationTokenSource cancellationTokenSource)
		{
			long beforeTicks = _model?.FocusTicks ?? 0L;
			StartActiveLongNoteSe(beforeTicks);
			bool isMusicEnded = false;
			while (_model != null && _model.IsMusicPlaying && cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
			{
				await UniTask.Yield();
				bool isEnded = UpdateMusicTime();
				if (_model == null || _model.MusicScoreMakerData == null)
				{
					continue;
				}
				long ticks = MusicScoreMakerUtility.GetTicksFromTime(_model.CurrentMusicTime, _model.MusicScoreMakerData.MusicScoreEventDataList);
				SetFocusTicks(ticks);
				UpdateMusicScoreSe(ticks, beforeTicks);
				beforeTicks = ticks;
				if (MusicScoreMakerEventDispatcher.ExistsInstance)
				{
					MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
					dispatcher.Publish(new UpdateMusicPlayTimePreviewEvent
					{
						Ticks = _model.FocusTicks
					});
					dispatcher.Publish(new UpdateMusicScoreEvent());
					dispatcher.Publish(new UpdateTimelineSliderValueWithoutNotifyEvent());
					dispatcher.Publish(new UpdateMusicTimeTextEvent(_model.CurrentMusicTime));
				}
				if (isEnded)
				{
					isMusicEnded = true;
					break;
				}
			}
			StopLongNoteSe();
			if (isMusicEnded && cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
			{
				PauseMusic();
				if (_model != null)
				{
					_model.IsMusicPlaying = false;
				}
				if (MusicScoreMakerEventDispatcher.ExistsInstance)
				{
					MusicScoreMakerEventDispatcher.Instance.Publish(new PauseMusicEvent());
				}
			}
		}

		private void UpdateMusicTimeTextEventFromFocusTicks()
		{
			if (_model == null)
			{
				return;
			}
			// Original behavior: while BGM is playing, the audio clock owns CurrentMusicTime.
			// FocusTicks may be clamped at the editor top edge, but the time display keeps advancing.
			if (_model.IsMusicPlaying)
			{
				return;
			}
			float currentTime = _model.MusicScoreMakerData != null ? _model.MusicScoreMakerData.GetTimeFromTicks(_model.FocusTicks) : 0f;
			_model.CurrentMusicTime = currentTime;
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateMusicTimeTextEvent(currentTime));
			}
		}

		private void SetupTapSe()
		{
			LiveSettingData liveSettingData = LiveSettingData.LoadFromStorage();
			LiveConfig.SetNoteSeName(liveSettingData?.NoteSeIndex ?? 0);
			SoundManager.Instance.LoadSoundBundle(GetLiveNoteSeAssetPathName(LiveConfig.NoteSeName), true);
			SoundManager.Instance.ResumeIngameSe();

			_tapSeDict = new Dictionary<int, string>
			{
				[GetTapSeKey(NoteCategory.Normal, NoteType.Default)] = LiveSoundDefine.SE_LIVE_PERFECT,
				[GetTapSeKey(NoteCategory.Normal, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_CRITICAL,
				[GetTapSeKey(NoteCategory.Flick, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FLICK,
				[GetTapSeKey(NoteCategory.Flick, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FLICK_CRITICAL,
				[GetTapSeKey(NoteCategory.Long, NoteType.Default)] = LiveSoundDefine.SE_LIVE_PERFECT,
				[GetTapSeKey(NoteCategory.Long, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_PERFECT,
				[GetTapSeKey(NoteCategory.Friction, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FRICTION,
				[GetTapSeKey(NoteCategory.Friction, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FRICTION_CRITICAL,
				[GetTapSeKey(NoteCategory.FrictionLong, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FRICTION,
				[GetTapSeKey(NoteCategory.FrictionLong, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FRICTION_CRITICAL,
				[GetTapSeKey(NoteCategory.FrictionFlick, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FLICK,
				[GetTapSeKey(NoteCategory.FrictionFlick, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FLICK_CRITICAL,
				[GetTapSeKey(NoteCategory.Connection, NoteType.Default)] = LiveSoundDefine.SE_LIVE_CONNECT,
				[GetTapSeKey(NoteCategory.Connection, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_CONNECT_CRITICAL
			};
		}

		private void DisposeTapSe()
		{
			_tapSeDict?.Clear();
			_longPlaybackIds?.Clear();
		}

		private void UpdateMusicScoreSe(long ticks, long beforeTicks)
		{
			if (ticks == beforeTicks || !MusicScoreMakerSettingsManager.PlayMusicSEEnabled)
			{
				return;
			}
			MusicScoreMakerData data = _model?.MusicScoreMakerData;
			if (data?.NoteList == null)
			{
				return;
			}

			data.EnsureNoteListTicksOrderIfNeeded();
			Dictionary<int, MusicScoreNoteBase> noteIdCache = data.GetNoteIdCacheOrRebuild();
			foreach (MusicScoreNoteBase note in data.NoteList)
			{
				if (note == null)
				{
					continue;
				}
				if (note.ticks < beforeTicks)
				{
					continue;
				}
				if (note.ticks > ticks)
				{
					break;
				}

				NoteCategory category = note.category;
				NoteType type = note.type;
				bool isLongFirst = false;
				bool isLongEnd = false;
				NoteType longEndStopType = type;

				if (!MusicScoreMakerUtility.IsNoteCategoryWithoutSe(category))
				{
					if (note.IsConnectedLast)
					{
						MusicScoreNoteBase startNote = note.FindStartNote(noteIdCache);
						if (startNote != null)
						{
							longEndStopType = startNote.type;
						}
						isLongEnd = true;
					}
					isLongFirst = note.IsConnectedFirst;
				}

				PlaySe(category, type, isLongFirst, isLongEnd, longEndStopType);
			}
		}

		private void PlayToolTypeToNoteSe()
		{
			PlayScoreMakerSe(SE_SCORE_NOTES_SET_CUE_NAME, SE_DECIDE_CUE_NAME);
		}

		private void PlayNotePlacementFailSe()
		{
			PlayScoreMakerSe(SE_CANCEL_CUE_NAME);
		}

		private void PlaySe(NoteCategory category, NoteType type, bool isLongFirst, bool isLongEnd, NoteType? longEndStopType = null)
		{
			if (MusicScoreMakerUtility.IsNoteCategoryWithoutSe(category))
			{
				return;
			}

			if (_tapSeDict != null && _tapSeDict.TryGetValue(GetTapSeKey(category, type), out string cueName))
			{
				SoundManager.Instance.PlayIngameSEOneShot(cueName);
			}

			if (isLongFirst)
			{
				PlayLongNoteSe(type);
			}

			if (isLongEnd && longEndStopType.HasValue)
			{
				StopLongNoteSe(longEndStopType);
			}
		}

		private static void PlayScoreMakerSe(string cueName, string fallbackCueName = null)
		{
			if (string.IsNullOrEmpty(cueName))
			{
				return;
			}

			SoundManager soundManager = SoundManager.Instance;
			string playCueName = cueName;
			if (!soundManager.ExistsCueName(playCueName))
			{
				if (string.IsNullOrEmpty(fallbackCueName) || !soundManager.ExistsCueName(fallbackCueName))
				{
					return;
				}
				playCueName = fallbackCueName;
			}
			soundManager.PlaySEOneShot(playCueName, 0);
		}

		private void PlayNoteDragSeIfNeeded(SelectedTargetOperation operation)
		{
			if (operation == null)
			{
				return;
			}

			long minTicks = GetMinTicksFromSelectedNotes();
			long quantizedTicks = minTicks != long.MaxValue
				? MusicScoreMakerUtility.CalculateSnapQuantizedTicks(operation.deltaTicks, minTicks)
				: operation.deltaTicks;
			if (quantizedTicks == _prevDragQuantizedTicks && operation.deltaLane == _prevDragDeltaLane)
			{
				return;
			}

			bool movedFromOriginalPosition = operation.deltaLane != 0;
			if (minTicks != long.MaxValue)
			{
				movedFromOriginalPosition |= quantizedTicks != minTicks;
			}
			else
			{
				movedFromOriginalPosition |= operation.deltaTicks != 0;
			}
			if (movedFromOriginalPosition)
			{
				PlayScoreMakerSe(SE_SCORE_NOTES_MOVE_CUE_NAME, SE_UI_SCROLL_CUE_NAME);
			}

			_prevDragQuantizedTicks = quantizedTicks;
			_prevDragDeltaLane = operation.deltaLane;
		}

		private void PlayExpandInputDragSeIfNeeded(SelectedTargetOperation.NoteTapPosition noteTapPosition, int deltaLane)
		{
			int previousDeltaLane;
			string cueName;
			string fallbackCueName;
			if (noteTapPosition == SelectedTargetOperation.NoteTapPosition.left)
			{
				previousDeltaLane = _prevExpandLeftDeltaLane;
				cueName = deltaLane < previousDeltaLane ? SE_SCORE_NOTES_EXTEND_CUE_NAME : SE_SCORE_NOTES_SHRINK_CUE_NAME;
				fallbackCueName = deltaLane < previousDeltaLane ? SE_UI_GAUGE_SLIDER_UP_CUE_NAME : SE_UI_GAUGE_SLIDER_DOWN_CUE_NAME;
			}
			else if (noteTapPosition == SelectedTargetOperation.NoteTapPosition.right)
			{
				previousDeltaLane = _prevExpandRightDeltaLane;
				cueName = deltaLane > previousDeltaLane ? SE_SCORE_NOTES_EXTEND_CUE_NAME : SE_SCORE_NOTES_SHRINK_CUE_NAME;
				fallbackCueName = deltaLane > previousDeltaLane ? SE_UI_GAUGE_SLIDER_UP_CUE_NAME : SE_UI_GAUGE_SLIDER_DOWN_CUE_NAME;
			}
			else
			{
				return;
			}

			if (deltaLane != previousDeltaLane && deltaLane != 0)
			{
				PlayScoreMakerSe(cueName, fallbackCueName);
			}
		}

		private void StartActiveLongNoteSe(long currentTicks)
		{
			if (!MusicScoreMakerSettingsManager.PlayMusicSEEnabled)
			{
				return;
			}
			MusicScoreMakerData data = _model?.MusicScoreMakerData;
			if (data?.NoteList == null)
			{
				return;
			}

			data.EnsureNoteListTicksOrderIfNeeded();
			Dictionary<int, MusicScoreNoteBase> noteIdCache = data.GetNoteIdCacheOrRebuild();
			foreach (MusicScoreNoteBase note in data.NoteList)
			{
				if (note == null)
				{
					continue;
				}
				if (note.ticks >= currentTicks)
				{
					return;
				}
				if (!note.IsConnectedFirst || MusicScoreMakerUtility.IsNoteCategoryWithoutSe(note.category))
				{
					continue;
				}

				MusicScoreNoteBase endNote = note.FindEndNote(noteIdCache);
				if (endNote != null && endNote.ticks > currentTicks)
				{
					PlayLongNoteSe(note.type);
				}
			}
		}

		private void StopLongNoteSe(NoteType? type = null)
		{
			if (_longPlaybackIds == null)
			{
				return;
			}
			if (type.HasValue)
			{
				if (_longPlaybackIds.TryGetValue(type.Value, out uint playbackId) && playbackId != 0)
				{
					SoundManager.Instance.StopSE(playbackId);
				}
				_longPlaybackIds[type.Value] = 0;
			}
			else
			{
				_workStopLongNoteTypes.Clear();
				foreach (KeyValuePair<NoteType, uint> pair in _longPlaybackIds)
				{
					if (pair.Value != 0)
					{
						_workStopLongNoteTypes.Add(pair.Key);
					}
				}
				foreach (NoteType noteType in _workStopLongNoteTypes)
				{
					if (_longPlaybackIds.TryGetValue(noteType, out uint playbackId) && playbackId != 0)
					{
						SoundManager.Instance.StopSE(playbackId);
					}
					_longPlaybackIds[noteType] = 0;
				}
			}
		}

		private static string GetLiveNoteSeAssetPathName(string bundleName)
		{
			return LIVE_NOTE_SE_BUNDLE_PREFIX + bundleName;
		}

		private static int GetTapSeKey(NoteCategory category, NoteType type)
		{
			return (int)category + 10 * (int)type;
		}

		private void PlayLongNoteSe(NoteType type)
		{
			if (_longPlaybackIds != null && _longPlaybackIds.TryGetValue(type, out uint currentPlaybackId) && currentPlaybackId != 0)
			{
				return;
			}

			string cueName = type == NoteType.Critical ? LiveSoundDefine.SE_LIVE_LONG_CRITICAL : LiveSoundDefine.SE_LIVE_LONG;
			uint playbackId = SoundManager.Instance.PlayIngameSE(cueName);
			if (_longPlaybackIds != null)
			{
				_longPlaybackIds[type] = playbackId;
			}
		}

		[AsyncStateMachine(typeof(_003CWaitForValidPlaybackTime_003Ed__90))]
		private async UniTask WaitForValidPlaybackTime(CancellationTokenSource cancellationTokenSource)
		{
			int waitCount = 0;
			while (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
			{
				await UniTask.Yield();
				long playbackTime = SoundManager.Instance.GetAudioSyncedUnityTimer();
				if (playbackTime >= 1L && (_model == null || _model.MusicLength <= 0L || playbackTime <= _model.MusicLength))
				{
					return;
				}
				waitCount++;
				if (waitCount >= 30)
				{
					return;
				}
			}
		}

		private bool UpdateMusicTime()
		{
			if (_model == null || !_model.IsMusicPlaying)
			{
				return false;
			}
			long playbackTimeMs = SoundManager.Instance.GetAudioSyncedUnityTimer();
			if (playbackTimeMs > 0L && (_model.MusicLength <= 0L || playbackTimeMs <= _model.MusicLength))
			{
				_model.CurrentMusicTime = Mathf.Max(0f, playbackTimeMs / 1000f - _model.FillerSec);
				return false;
			}
			if (playbackTimeMs > _model.MusicLength && _model.MusicLength > 0L)
			{
				_model.CurrentMusicTime = Mathf.Max(0f, _model.MusicLength / 1000f - _model.FillerSec);
				return true;
			}
			if (_model.CurrentMusicTime > 0f)
			{
				_model.CurrentMusicTime = Mathf.Max(0f, _model.MusicLength / 1000f - _model.FillerSec);
				return true;
			}
			return false;
		}

		private void OpenMusicSelectorDialog()
		{
			OnMusicSelector(new OnMusicSelectorEvent());
		}

		[AsyncStateMachine(typeof(_003CApplyMusicSelectorDialog_003Ed__93))]
		private UniTask ApplyMusicSelectorDialog(MusicScoreMakerMusicSelectorDialog dialog)
		{
			return UniTask.CompletedTask;
		}

		[AsyncStateMachine(typeof(_003CCreateMusicScore_003Ed__94))]
		public async UniTask CreateMusicScore(int musicId, string musicDifficulty, int vocalId, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			if (_model == null)
			{
				return;
			}
			_model.Difficulty = musicDifficulty;
			_model.VocalId = vocalId;
			bool isAppend = string.Equals(musicDifficulty, "append", StringComparison.OrdinalIgnoreCase);
			MusicScoreMakerData data = LoadMusicScoreMakerData(musicId, isAppend ? "expert" : musicDifficulty, isAppend);
			if (data == null)
			{
				UnityEngine.Debug.LogError($"MusicScoreMaker: failed to load base music score. musicId={musicId}, difficulty={musicDifficulty}");
				return;
			}
			_model.MusicScoreMakerData = data;
			_model.MusicId = musicId;
			ApplyMusicScoreStartupData(musicId);
			SetFocusTicks(0L);
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.ClearUndoRedoStack();
			}
			_model.UpdateSavedDataHash();
			await UpdateBackground2DJacketAsync(musicId, token);
		}

		[AsyncStateMachine(typeof(_003CCreateMusicScore_003Ed__95))]
		public async UniTask CreateMusicScore(int musicId, string musicDifficulty, int vocalId, MusicScoreMakerData musicScore, long focusTicks, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			if (_model == null)
			{
				return;
			}
			_model.Difficulty = musicDifficulty;
			_model.VocalId = vocalId;
			_model.MusicScoreMakerData = musicScore ?? LoadMusicScoreMakerData(musicId, musicDifficulty, clearNotes: false);
			if (_model.MusicScoreMakerData == null)
			{
				UnityEngine.Debug.LogError($"MusicScoreMaker: failed to restore music score. musicId={musicId}, difficulty={musicDifficulty}");
				return;
			}
			_model.MusicScoreMakerData.InitializeIdCount();
			_model.SetFullComboDataHash(_model.MusicScoreMakerData.FullComboDataHash, _model.MusicScoreMakerData.FullComboDataHash != null);
			_model.MusicId = musicId;
			ApplyMusicScoreStartupData(musicId);
			_model.FocusTicks = ClampTicksToValidRange(focusTicks);
			await UpdateBackground2DJacketAsync(musicId, token);
			NotifyMusicScoreAndTimelineChanged();
			_model.UpdateSavedDataHash();
		}

		public UniTask CreateCustomMusicScore(CustomMusicScoreEntry entry, MusicScoreMakerData musicScore, long focusTicks, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			if (_model == null || entry == null)
			{
				return UniTask.CompletedTask;
			}

			CustomMusicScoreManifest manifest = entry.Manifest;
			_model.CustomMusicScoreEntry = entry;
			_model.Difficulty = manifest.musicDifficultyType;
			_model.VocalId = 0;
			_model.MasterMusicSec = entry.MusicDurationSec;
			_model.FillerSec = manifest.fillerSec;
			_model.AssetbundleName = entry.AudioCueName;
			_model.LastSelectFile = manifest.scoreFileName;

			MusicScoreMakerData data = musicScore ?? entry.LoadScore();
			if (data == null)
			{
				data = LoadMusicScoreMakerData(entry.MusicId, manifest.musicDifficultyType, clearNotes: true);
			}
			if (data == null)
			{
				UnityEngine.Debug.LogError($"MusicScoreMaker: failed to load custom score. id={manifest.id}");
				return UniTask.CompletedTask;
			}

			data.MusicId = entry.MusicId;
			data.InitializeIdCount();
			_model.MusicScoreMakerData = data;
			_model.MusicId = entry.MusicId;
			_model.LiveBundleBuildData = Resources.Load<LiveBundleBuildData>(LiveConfig.ConfigBundleNamePath);
			_model.FocusTicks = ClampTicksToValidRange(focusTicks);
			NotifyMusicScoreAndTimelineChanged();
			_model.UpdateSavedDataHash();
			return UniTask.CompletedTask;
		}

		[AsyncStateMachine(typeof(_003CMusicReady_003Ed__96))]
		private async UniTask MusicReady(int musicId, int vocalId)
		{
			if (_model == null)
			{
				return;
			}
			_musicReadyCts?.Cancel();
			_musicReadyCts?.Dispose();
			_musicReadyCts = new CancellationTokenSource();
			if (!_model.IsMusicPlaying && !_isFirstMusicReady)
			{
				PauseMusic();
			}
			_isFirstMusicReady = false;
			_model.LiveSettings = LiveSettingData.LoadFromStorage();
			if (_model.LiveSettings != null)
			{
				_model.TimingAdjust = _model.LiveSettings.TimingAdjustData * (1f / 60f);
			}
			if (_model.CustomMusicScoreEntry != null)
			{
				await MusicReadyForCustomEntry(_model.CustomMusicScoreEntry);
				return;
			}
			_model.AssetbundleName = ResolveMusicVocalAssetbundleName(musicId, vocalId);
			string musicSoundBundleName = ResolveLiveMusicSoundBundleName(_model.AssetbundleName);
			if (!string.IsNullOrEmpty(musicSoundBundleName))
			{
				SoundManager.Instance.LoadSoundBundle(musicSoundBundleName, isResident: true);
				CriAtomEx.CueInfo[] cueInfos = SoundManager.Instance.GetCueInfos(musicSoundBundleName);
				long fallbackLength = 0L;
				for (int i = 0; i < cueInfos.Length; i++)
				{
					if (cueInfos[i].length <= 0)
					{
						continue;
					}

					if (fallbackLength <= 0L)
					{
						fallbackLength = cueInfos[i].length;
					}

					if (string.Equals(cueInfos[i].name, _model.AssetbundleName, StringComparison.OrdinalIgnoreCase))
					{
						_model.MusicLength = cueInfos[i].length;
						break;
					}
				}

				if (_model.MusicLength <= 0L)
				{
					_model.MusicLength = fallbackLength;
				}
			}
			if (_model.MasterMusicSec <= 0)
			{
				_model.MasterMusicSec = ResolveMasterMusicSec(musicId);
			}
			if (_model.MusicLength <= 0L)
			{
				_model.MusicLength = Math.Max(_model.MasterMusicSec, 0) * 1000L;
			}
			_model.UpdateComboCountMinimum(_model.MasterMusicSec);
			SetFocusTicks(_model.FocusTicks);
			NotifyMusicScoreAndTimelineChanged(refresh: true);
			await UniTask.Yield();
			_musicReadyCts?.Dispose();
			_musicReadyCts = null;
		}

		private async UniTask MusicReadyForCustomEntry(CustomMusicScoreEntry entry)
		{
			if (_model == null || entry == null)
			{
				return;
			}

			bool audioRegistered = await entry.RegisterAudioAsync(_musicReadyCts != null ? _musicReadyCts.Token : CancellationToken.None);
			_model.AssetbundleName = entry.AudioCueName;
			_model.FillerSec = entry.Manifest.fillerSec;
			_model.MasterMusicSec = entry.MusicDurationSec;
			_model.MusicLength = audioRegistered && entry.AudioLengthMs > 0L
				? entry.AudioLengthMs
				: Math.Max(_model.MasterMusicSec, 0) * 1000L;
			_model.UpdateComboCountMinimum(_model.MasterMusicSec);
			SetFocusTicks(_model.FocusTicks);
			NotifyMusicScoreAndTimelineChanged(refresh: true);
			await UniTask.Yield();
			_musicReadyCts?.Dispose();
			_musicReadyCts = null;
		}

		private MusicScoreMakerData LoadMusicScoreMakerData(int musicId, string loadDifficulty, bool clearNotes)
		{
			// TODO(original): restore MusicScoreFactory.CreateDifficultyOnly and SUS_Converter info overlay loading.
			// This keeps the editor bootable with the same model shape while asset-backed score loading is restored.
			MusicScore baseScore = new MusicScore(
				new[]
				{
					new MusicScoreInfo(0, 0f, 0f, 120f, 4f, 1f, 1f)
				},
				Array.Empty<EventBase>(),
				Array.Empty<NoteBase>());
			MusicScoreMakerData data = new MusicScoreMakerData(baseScore)
			{
				MusicId = musicId
			};
			data.DiscardDifficultyScoreInfo();
			data.ApplyInfoOverlay(baseScore);
			if (clearNotes)
			{
				data.ClearSpeedChangeEvents();
				data.ClearNotes();
			}
			else
			{
				data.RemoveOverlappingTraceGuideSingleNotes();
				data.ClearSpeedChangeEvents();
			}
			data.InitializeIdCount();
			return data;
		}

		private void ApplyMusicScoreStartupData(int musicId)
		{
			if (_model == null)
			{
				return;
			}
			_model.MasterMusicSec = ResolveMasterMusicSec(musicId);
			_model.LiveBundleBuildData = Resources.Load<LiveBundleBuildData>(LiveConfig.ConfigBundleNamePath);
		}

		private static int ResolveMasterMusicSec(int musicId)
		{
			// TODO(original): restore MasterDataManager.GetMasterMusicAll(musicId).music.secForMusicScoreMaker.
			return 120;
		}

		private static string ResolveMusicVocalAssetbundleName(int musicId, int vocalId)
		{
			if (musicId <= 0 || vocalId <= 0)
			{
				return string.Empty;
			}

			MasterMusicVocal vocal = ResolveMasterMusicVocal(musicId, vocalId);
			if (!string.IsNullOrEmpty(vocal?.assetbundleName))
			{
				return vocal.assetbundleName;
			}

			string fallbackName = $"{musicId:D4}_{vocalId:D2}";
			return AssetBundleManager.Instance.HasBundle(ResolveLiveMusicSoundBundleName(fallbackName)) ? fallbackName : string.Empty;
		}

		private static string ResolveLiveMusicSoundBundleName(string musicAssetbundleName)
		{
			return string.IsNullOrEmpty(musicAssetbundleName) ? string.Empty : $"music/long/{musicAssetbundleName}";
		}

		private static MasterMusicVocal ResolveMasterMusicVocal(int musicId, int vocalId)
		{
			MasterMusicAll musicAll = LoadMasterMusicAll(musicId);
			MasterMusicVocal[] vocals = musicAll?.musicVocals;
			if (vocals == null)
			{
				return null;
			}

			for (int i = 0; i < vocals.Length; i++)
			{
				if (vocals[i] != null && vocals[i].id == vocalId)
				{
					return vocals[i];
				}
			}

			return null;
		}

		private static MasterMusicAll LoadMasterMusicAll(int musicId)
		{
			object manager = GetSingletonInstance("Sekai.MasterDataManager");
			object music = InvokeMember(manager, "GetMasterMusicAll", musicId);
			if (music is MasterMusicAll masterMusicAll)
			{
				return masterMusicAll;
			}

			object map = InvokeMember(manager, "GetMasterMusicAllMap");
			if (map is System.Collections.IDictionary dictionary)
			{
				foreach (object value in dictionary.Values)
				{
					if (value is MasterMusicAll candidate && candidate.music != null && candidate.music.id == musicId)
					{
						return candidate;
					}
				}
			}

			return null;
		}

		private static object GetSingletonInstance(string typeName)
		{
			Type type = FindType(typeName);
			if (type == null)
			{
				return null;
			}

			PropertyInfo instanceProperty = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
			return instanceProperty != null ? instanceProperty.GetValue(null) : null;
		}

		private static Type FindType(string typeName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				Type type = assemblies[i].GetType(typeName);
				if (type != null)
				{
					return type;
				}
			}

			return null;
		}

		private static object InvokeMember(object target, string memberName, params object[] args)
		{
			if (target == null || string.IsNullOrEmpty(memberName))
			{
				return null;
			}

			Type type = target.GetType();
			MethodInfo method = type.GetMethod(memberName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
			if (method != null)
			{
				return method.Invoke(method.IsStatic ? null : target, args);
			}

			PropertyInfo property = type.GetProperty(memberName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
			return property != null ? property.GetValue(property.GetGetMethod(true)?.IsStatic == true ? null : target) : null;
		}

		private void NotifyMusicScoreAndTimelineChanged(bool refresh = false)
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Publish(new UpdateMusicScoreEvent());
			dispatcher.Publish(new UpdateTimelineSliderValueWithoutNotifyEvent());
			if (_model != null && !_model.IsMusicPlaying)
			{
				dispatcher.Publish(new UpdateMusicPlayTimePreviewEvent
				{
					Ticks = _model.FocusTicks
				});
				UpdateMusicTimeTextEventFromFocusTicks();
			}
			if (refresh)
			{
				dispatcher.Publish(new RefreshMusicScoreEvent());
			}
		}

		private static void ApplyInfoScoreOverlay(int musicId, MusicScoreMakerData data)
		{
			// TODO(original): restore MasterData/MusicScoreFactory based info-score overlay lookup.
			data?.DiscardDifficultyScoreInfo();
		}

		private void ResetMusicScoreToCleanState()
		{
			MusicScoreMakerData data = _model?.MusicScoreMakerData;
			if (data == null)
			{
				return;
			}
			data.ClearNotesAndSpeedEvents();
			data.ClearSelectedEvents();
			data.ClearSelectedTemporaryEvents();
			data.ClearSelectedNotes();
			data.ClearSelectedTemporaryNotes();
			NotifyMusicScoreAndTimelineChanged(refresh: true);
		}

		private MusicScoreMakerData ConvertToMusicScoreMakerMusicScoreData(MusicScore musicScore)
		{
			return musicScore == null ? null : new MusicScoreMakerData(musicScore);
		}

		private static bool IsGuideChainConversion(NoteCategory beforeCategory, NoteCategory afterCategory)
		{
			return MusicScoreMakerUtility.IsGuideCategory(beforeCategory) != MusicScoreMakerUtility.IsGuideCategory(afterCategory);
		}

		private static MusicScoreNoteBase.NoteBaseType GetNoteBaseTypeForSingleCategoryChange(MusicScoreNoteBase note, NoteCategory afterCategory)
		{
			return FindNoteBaseType(afterCategory);
		}

		private static void GetLongGuideConvertedCategories(NoteCategory afterStartNoteCategory, out NoteCategory afterEndNoteCategory, out NoteCategory connectedNoteCategory)
		{
			if (MusicScoreMakerUtility.IsGuideCategory(afterStartNoteCategory))
			{
				afterEndNoteCategory = NoteCategory.GuideEnd;
				connectedNoteCategory = NoteCategory.GuideHidden;
				return;
			}
			afterEndNoteCategory = afterStartNoteCategory switch
			{
				NoteCategory.FrictionLong => NoteCategory.Friction,
				NoteCategory.FrictionHideLong => NoteCategory.FrictionHide,
				_ => afterStartNoteCategory
			};
			connectedNoteCategory = afterStartNoteCategory == NoteCategory.FrictionLong ? NoteCategory.FrictionHideLong : NoteCategory.Connection;
		}

		private MusicScoreMakerData CurrentData()
		{
			return _model?.MusicScoreMakerData;
		}

		private void PushUndoableAction(Action undoAction, Action redoAction, long focusTicksAfterUndo = -1L, long focusTicksAfterRedo = -1L, bool skipEditGuard = false)
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.PushUndoableActionAndDoAction(undoAction, redoAction, focusTicksAfterUndo, focusTicksAfterRedo, skipEditGuard);
			}
			else
			{
				redoAction?.Invoke();
			}
		}

		private List<MusicScoreNoteBase> GetSelectedOrSingleNotes(int noteId)
		{
			MusicScoreMakerData data = CurrentData();
			List<MusicScoreNoteBase> result = new List<MusicScoreNoteBase>();
			if (data == null)
			{
				return result;
			}
			if (noteId >= 0)
			{
				MusicScoreNoteBase note = data.FindNote(noteId);
				if (note != null)
				{
					result.Add(note);
				}
				return result;
			}
			foreach (int selectedId in data.SelectedNoteTargetIdSet)
			{
				MusicScoreNoteBase note = data.FindNote(selectedId);
				if (note != null)
				{
					result.Add(note);
				}
			}
			return result;
		}

		private static List<MusicScoreNoteBase> CloneNotesForUndo(List<MusicScoreNoteBase> notes)
		{
			List<MusicScoreNoteBase> snapshots = new List<MusicScoreNoteBase>();
			if (notes == null)
			{
				return snapshots;
			}
			foreach (MusicScoreNoteBase note in notes)
			{
				if (note != null)
				{
					snapshots.Add(note.Clone());
				}
			}
			return snapshots;
		}

		private static void CopyNoteMutableData(MusicScoreNoteBase target, MusicScoreNoteBase source)
		{
			if (target == null || source == null)
			{
				return;
			}
			target.ticks = source.ticks;
			target.laneStart = source.laneStart;
			target.laneEnd = source.laneEnd;
			target.category = source.category;
			target.type = source.type;
			target.speedRatio = source.speedRatio;
			target.noteLineType = source.noteLineType;
			target.noteBaseType = source.noteBaseType;
			target.previousConnectionId = source.previousConnectionId;
			target.nextConnectionId = source.nextConnectionId;
			target.direction = source.direction;
			target.isSkip = source.isSkip;
		}

		private void RestoreNoteSnapshots(List<MusicScoreNoteBase> snapshots)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || snapshots == null)
			{
				return;
			}
			Dictionary<int, MusicScoreNoteBase> cache = data.GetNoteIdCacheOrRebuild();
			foreach (MusicScoreNoteBase snapshot in snapshots)
			{
				if (snapshot != null && cache.TryGetValue(snapshot.id, out MusicScoreNoteBase target))
				{
					CopyNoteMutableData(target, snapshot);
				}
			}
			data.MarkNoteListOrderDirty();
			data.UpdateConnectionNotes();
		}

		private void MarkEditedAndRefresh(IEnumerable<MusicScoreNoteBase> notes)
		{
			if (notes != null)
			{
				foreach (MusicScoreNoteBase note in notes)
				{
					if (note != null)
					{
						MusicScoreMakerUtility.AddEditedTick(note.ticks);
					}
				}
			}
			RecheckJudgmentNoteGapIfEditedInGapRange();
			NotifyMusicScoreAndTimelineChanged();
		}

		private static NoteCategory ToolTypeToNoteCategory(MusicScoreMakerUtility.ToolType toolType)
		{
			int rawValue = (int)toolType;
			return Enum.IsDefined(typeof(NoteCategory), rawValue) ? (NoteCategory)rawValue : NoteCategory.Normal;
		}

		private void SetupNoteEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<SetSelectedNoteTypeEvent>(SetSelectedNoteType);
			dispatcher.Register<SetSelectedNoteDirectionEvent>(SetSelectedNoteDirection);
			dispatcher.Register<SetSelectedNoteLineTypeEvent>(SetSelectedNoteLineType);
			dispatcher.Register<ChangeNoteSkipEvent>(ChangeNoteSkip);
			dispatcher.Register<RemoveNoteEvent>(RemoveNote);
			dispatcher.Register<ChangeNoteTypeEvent>(ChangeNoteType);
			dispatcher.Register<AddNoteListEvent>(AddNoteList);
			dispatcher.Register<ChangeNoteLineTypeEvent>(ChangeNoteLineType);
			dispatcher.Register<ChangeNoteCategoryEvent>(ChangeNoteCategory);
			dispatcher.Register<SelectCategoryAndAddNoteEvent>(SelectCategoryAndAddNote);
			dispatcher.Register<FlipSelectedNotesHorizontallyEvent>(FlipSelectedNotesHorizontally);
			dispatcher.Register<FlipSelectedNotesVerticallyEvent>(FlipSelectedNotesVertically);
			dispatcher.Register<SetSelectedNoteDataEvent>(SetSelectedNoteData);
			dispatcher.Register<ChangeNoteDataEvent>(ChangeNoteData);
			dispatcher.Register<SelectedChangeButtonEvent>(SelectedChangeButton);
			dispatcher.Register<GetSelectedNoteCategoryEvent, NoteCategory>(GetSelectedNoteCategory);
			dispatcher.Register<GetIsNoteDataSelectedEvent, bool>(GetIsNoteDataSelected);
		}

		private void SelectedChangeButton(SelectedChangeButtonEvent obj)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || !MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			HashSet<int> selectedIds = data.SelectedNoteTargetIdSet;
			if (selectedIds.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> selectedNotes = new List<MusicScoreNoteBase>();
			foreach (int id in selectedIds)
			{
				MusicScoreNoteBase note = data.FindNote(id);
				if (note != null)
				{
					selectedNotes.Add(note);
				}
			}
			if (selectedNotes.Count == 0)
			{
				return;
			}
			if (MusicScoreMakerUtility.IsSameNoteGroup(selectedNotes.ToArray(), data.GetNoteIdCacheOrRebuild(), out NoteGroupType groupType))
			{
				MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
				dispatcher.Publish(new ShowNoteChangeSubWindowEvent());
				dispatcher.Publish(new ShowNoteChangeButtonsEvent
				{
					GroupType = groupType,
					SelectedNotes = selectedNotes.ToArray()
				});
			}
		}

		private void DisposeNoteEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<SetSelectedNoteTypeEvent>(SetSelectedNoteType);
			dispatcher.Remove<SetSelectedNoteDirectionEvent>(SetSelectedNoteDirection);
			dispatcher.Remove<SetSelectedNoteLineTypeEvent>(SetSelectedNoteLineType);
			dispatcher.Remove<ChangeNoteSkipEvent>(ChangeNoteSkip);
			dispatcher.Remove<RemoveNoteEvent>(RemoveNote);
			dispatcher.Remove<ChangeNoteTypeEvent>(ChangeNoteType);
			dispatcher.Remove<AddNoteListEvent>(AddNoteList);
			dispatcher.Remove<ChangeNoteLineTypeEvent>(ChangeNoteLineType);
			dispatcher.Remove<ChangeNoteCategoryEvent>(ChangeNoteCategory);
			dispatcher.Remove<SelectCategoryAndAddNoteEvent>(SelectCategoryAndAddNote);
			dispatcher.Remove<FlipSelectedNotesHorizontallyEvent>(FlipSelectedNotesHorizontally);
			dispatcher.Remove<FlipSelectedNotesVerticallyEvent>(FlipSelectedNotesVertically);
			dispatcher.Remove<SetSelectedNoteDataEvent>(SetSelectedNoteData);
			dispatcher.Remove<ChangeNoteDataEvent>(ChangeNoteData);
			dispatcher.Remove<SelectedChangeButtonEvent>(SelectedChangeButton);
			dispatcher.Remove<GetSelectedNoteCategoryEvent, NoteCategory>(GetSelectedNoteCategory);
			dispatcher.Remove<GetIsNoteDataSelectedEvent, bool>(GetIsNoteDataSelected);
		}

		private void AddNote(float lane, long ticks)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || float.IsNaN(lane) || float.IsInfinity(lane) || lane < 0f || lane > MusicScoreMakerModel.LaneCountMinus1)
			{
				return;
			}
			long clampedTicks = Math.Min(Math.Max(ticks, 0L), GetMaxFocusableTicks());
			MusicScoreNoteBase[] generatedNotes = GenerateNote(lane, clampedTicks);
			if (generatedNotes == null || generatedNotes.Length == 0)
			{
				UnityEngine.Debug.LogError($"MusicScoreMaker failed to generate note lane={lane}, ticks={clampedTicks}");
				return;
			}
			List<MusicScoreNoteBase> generatedList = new List<MusicScoreNoteBase>(generatedNotes);
			if (!MusicScoreMakerUtility.ValidateLongNoteTicks(generatedList))
			{
				return;
			}
			Action redo = () =>
			{
				data.AddNoteRange(generatedList);
				if (_model != null && generatedList.Count > 0 && generatedList[0] != null)
				{
					_model.LastNoteWidth = generatedList[0].laneEnd - generatedList[0].laneStart;
				}
				data.UpdateConnectionNotes();
				MarkEditedAndRefresh(generatedList);
			};
			Action undo = () =>
			{
				data.RemoveNoteRange(generatedList);
				MarkEditedAndRefresh(generatedList);
			};
			PushUndoableAction(undo, redo, clampedTicks, clampedTicks);
		}

		private static (int, int) GetLaneRangeFromCenterLane(int centerLane, int width = MusicScoreMakerModel.DEFAULT_LAST_NOTE_WIDTH)
		{
			int noteWidth = Math.Max(0, width);
			int start = Math.Max(centerLane - (noteWidth >> 1), 0);
			int end = start + noteWidth;
			if (end > MusicScoreMakerModel.LaneCountMinus1)
			{
				end = MusicScoreMakerModel.LaneCountMinus1;
				start = Math.Max(end - noteWidth, 0);
			}
			return (start, end);
		}

		private MusicScoreNoteBase[] GenerateNote(float lane, long ticks)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return Array.Empty<MusicScoreNoteBase>();
			}
			int centerLane = (int)lane;
			int width = _model != null && _model.LastNoteWidth > 0 ? _model.LastNoteWidth : MusicScoreMakerModel.DEFAULT_LAST_NOTE_WIDTH;
			(int laneStart, int laneEnd) = GetLaneRangeFromCenterLane(centerLane, width);
			NoteCategory category = _model?.SelectedNoteCategory ?? NoteCategory.Normal;
			long lengthTicks = DefaultLongNoteLengthTicks;
			if (IsLongRootCategory(category) || category == NoteCategory.Guide)
			{
				AdjustLongNoteStartTicksToFit(ref ticks, lengthTicks);
				int startId = data.GetNewId();
				int endId = data.GetNewId();
				GetLongEndNoteCategoryAndBaseType(category, out NoteCategory endCategory, out MusicScoreNoteBase.NoteBaseType endBaseType);
				MusicScoreNoteBase startNote = CreateMusicScoreNoteBaseWithCategory(ticks, startId, laneStart, laneEnd, category, FindNoteBaseType(category), -1, endId);
				MusicScoreNoteBase endNote = CreateMusicScoreNoteBaseWithCategory(ticks + lengthTicks, endId, laneStart, laneEnd, endCategory, endBaseType, startId, -1);
				return new[] { startNote, endNote };
			}
			int noteId = data.GetNewId();
			return new[]
			{
				CreateMusicScoreNoteBaseWithCategory(ticks, noteId, laneStart, laneEnd, category, FindNoteBaseType(category))
			};
		}

		private void AdjustLongNoteStartTicksToFit(ref long ticks, long lengthTicks)
		{
			long maxStartTicks = Math.Max(0L, GetMaxFocusableTicks() - Math.Max(1L, lengthTicks));
			ticks = Math.Min(Math.Max(0L, ticks), maxStartTicks);
		}

		private MusicScoreNoteBase CreateMusicScoreNoteBase(long ticks, int noteId, int laneStart, int laneEnd, MusicScoreNoteBase.NoteBaseType noteBaseType, int previousConnectionId = -1, int nextConnectionId = -1)
		{
			return CreateMusicScoreNoteBaseWithCategory(ticks, noteId, laneStart, laneEnd, _model?.SelectedNoteCategory ?? NoteCategory.Normal, noteBaseType, previousConnectionId, nextConnectionId);
		}

		private MusicScoreNoteBase CreateMusicScoreNoteBaseWithCategory(long ticks, int noteId, int laneStart, int laneEnd, NoteCategory category, MusicScoreNoteBase.NoteBaseType noteBaseType, int previousConnectionId = -1, int nextConnectionId = -1)
		{
			return new MusicScoreNoteBase(
				noteId,
				ClampTicksToValidRange(ticks),
				MusicScoreMakerUtility.ClampLaneStart(laneStart, laneEnd),
				MusicScoreMakerUtility.ClampLaneEnd(laneEnd, laneStart),
				category,
				_model?.SelectedNoteType ?? NoteType.Default,
				1f,
				_model?.SelectedNoteLineType ?? NoteLineType.Linear,
				noteBaseType,
				_model?.SelectedIsSkip ?? false,
				_model?.SelectedNoteDirection ?? NoteDirection.Default,
				previousConnectionId,
				nextConnectionId);
		}

		private void RemoveNote(OnNotePreviewClickEvent obj)
		{
			if (obj != null)
			{
				RemoveNote(obj.NoteId);
			}
		}

		private void RemoveNote(RemoveNoteEvent obj)
		{
			if (obj == null)
			{
				return;
			}
			if (obj.NoteId >= 0)
			{
				RemoveNote(obj.NoteId);
				return;
			}
			RemoveSelectedAndTemporaryNotesAndEventList();
		}

		private void RemoveNote(int noteId)
		{
			MusicScoreMakerData data = CurrentData();
			List<MusicScoreNoteBase> removeNoteList = FindRemoveNoteList(noteId);
			if (data == null || removeNoteList.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> removeNoteListCopy = CloneNotesForUndo(removeNoteList);
			List<int> selectedIds = CopyList(data.SelectedNoteIdList);
			List<int> selectedTempIds = CopyList(data.SelectedTemporaryNoteIdList);
			Action redo = () =>
			{
				RemoveNoteList(removeNoteList);
				MarkEditedAndRefresh(removeNoteList);
			};
			Action undo = () =>
			{
				RestoreRemovedNoteList(removeNoteListCopy);
				data.ClearSelectedNotes();
				data.ClearSelectedTemporaryNotes();
				data.AddSelectedNoteRange(selectedIds);
				data.AddSelectedTemporaryNoteRange(selectedTempIds);
				MarkEditedAndRefresh(removeNoteListCopy);
			};
			PushUndoableAction(undo, redo, removeNoteList[0].ticks, removeNoteList[0].ticks);
		}

		private void RestoreRemovedNoteList(List<MusicScoreNoteBase> removeNoteListCopy)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || removeNoteListCopy == null)
			{
				return;
			}
			data.AddNoteRange(removeNoteListCopy);
			foreach (MusicScoreNoteBase note in removeNoteListCopy)
			{
				if (note == null)
				{
					continue;
				}
				if (note.previousConnectionId != -1)
				{
					MusicScoreNoteBase previousNote = data.FindNote(note.previousConnectionId);
					if (previousNote != null)
					{
						previousNote.nextConnectionId = note.id;
					}
				}
				if (note.nextConnectionId != -1)
				{
					MusicScoreNoteBase nextNote = data.FindNote(note.nextConnectionId);
					if (nextNote != null)
					{
						nextNote.previousConnectionId = note.id;
					}
				}
			}
			data.UpdateConnectionNotes();
		}

		private void RemoveNoteList(List<MusicScoreNoteBase> removeNoteList)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || removeNoteList == null)
			{
				return;
			}
			foreach (MusicScoreNoteBase note in removeNoteList)
			{
				if (note == null)
				{
					continue;
				}
				if (note.previousConnectionId != -1)
				{
					MusicScoreNoteBase previousNote = data.FindNote(note.previousConnectionId);
					if (previousNote != null)
					{
						previousNote.nextConnectionId = note.nextConnectionId;
					}
				}
				if (note.nextConnectionId != -1)
				{
					MusicScoreNoteBase nextNote = data.FindNote(note.nextConnectionId);
					if (nextNote != null)
					{
						nextNote.previousConnectionId = note.previousConnectionId;
					}
				}
			}
			data.RemoveNoteRange(removeNoteList);
			foreach (MusicScoreNoteBase note in removeNoteList)
			{
				if (note != null)
				{
					data.RemoveSelectedNote(note.id);
					data.RemoveSelectedTemporaryNote(note.id);
				}
			}
			data.UpdateConnectionNotes();
		}

		private List<MusicScoreNoteBase> FindRemoveNoteList(int noteId)
		{
			MusicScoreMakerData data = CurrentData();
			MusicScoreNoteBase targetNote = data?.FindNote(noteId);
			if (targetNote == null)
			{
				return new List<MusicScoreNoteBase>();
			}
			return FindRemoveNoteListForSingleNote(targetNote);
		}

		private List<MusicScoreNoteBase> FindRemoveNoteListForSingleNote(MusicScoreNoteBase targetNote)
		{
			List<MusicScoreNoteBase> result = new List<MusicScoreNoteBase>();
			if (targetNote == null)
			{
				return result;
			}
			bool isFirstEdge = targetNote.previousConnectionId == -1;
			bool isLastEdge = targetNote.nextConnectionId == -1;
			if (isFirstEdge != isLastEdge)
			{
				MusicScoreMakerData data = CurrentData();
				if (data != null)
				{
					targetNote.FindConnectedNotes(data.GetNoteIdCacheOrRebuild(), result);
					return result;
				}
			}
			result.Add(targetNote);
			return result;
		}

		private List<MusicScoreNoteBase> BuildRemoveNoteListFromSelectedNotes(List<MusicScoreNoteBase> selectedNotes)
		{
			HashSet<MusicScoreNoteBase> unique = new HashSet<MusicScoreNoteBase>();
			if (selectedNotes == null)
			{
				return new List<MusicScoreNoteBase>();
			}
			foreach (MusicScoreNoteBase note in selectedNotes)
			{
				foreach (MusicScoreNoteBase removeNote in FindRemoveNoteListForSingleNote(note))
				{
					if (removeNote != null)
					{
						unique.Add(removeNote);
					}
				}
			}
			return new List<MusicScoreNoteBase>(unique);
		}

		private void ChangeNoteType(ChangeNoteTypeEvent obj)
		{
			ChangeNotesType(obj, GetSelectedOrSingleNotes(obj?.NoteId ?? -1));
		}

		private void ChangeNotesType(ChangeNoteTypeEvent obj, [ItemCanBeNull] List<MusicScoreNoteBase> noteList)
		{
			if (obj == null || noteList == null || noteList.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> snapshots = CloneNotesForUndo(noteList);
			Action redo = () =>
			{
				foreach (MusicScoreNoteBase note in noteList)
				{
					ChangeNoteType(obj, note);
				}
				MarkEditedAndRefresh(noteList);
			};
			Action undo = () =>
			{
				RestoreNoteSnapshots(snapshots);
				MarkEditedAndRefresh(snapshots);
			};
			PushUndoableAction(undo, redo, noteList[0].ticks, noteList[0].ticks);
		}

		private static void ChangeNoteType(ChangeNoteTypeEvent obj, MusicScoreNoteBase targetNote)
		{
			if (obj != null && targetNote != null)
			{
				targetNote.type = obj.NoteType;
			}
		}

		private void AddNoteList(AddNoteListEvent obj)
		{
			TryAddNoteList(obj);
		}

		private bool TryAddNoteList(AddNoteListEvent obj)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || obj == null)
			{
				return false;
			}
			if (obj.Lane < 0 || obj.Lane > MusicScoreMakerModel.LaneCountMinus1)
			{
				return false;
			}

			MusicScoreNoteBase parentNote = data.FindNote(obj.NoteId);
			if (parentNote == null)
			{
				CP.LogUtility.LogError($"noteId={obj.NoteId}のノーツが見つかりません");
				return false;
			}

			int noteId = data.GetNewId();
			long ticks = AbsTicks(obj.Ticks);
			ticks = Math.Min(ticks, GetMaxFocusableTicks());
			Dictionary<int, MusicScoreNoteBase> noteIdCache = data.GetNoteIdCacheOrRebuild();
			MusicScoreNoteBase startNote = parentNote.FindStartNote(noteIdCache);
			MusicScoreNoteBase endNote = parentNote.FindEndNote(noteIdCache);
			if (startNote != null && endNote != null && (ticks <= startNote.ticks || ticks >= endNote.ticks))
			{
				return false;
			}
			if (!MusicScoreMakerUtility.CanPlaceNoteAtTicks(data, ticks, -1, parentNote.id))
			{
				return false;
			}
			if (obj.IsSkip && HasSkipConnectionAtTicks(startNote ?? parentNote, noteIdCache, ticks))
			{
				return false;
			}
			if (!MusicScoreMakerUtility.CanPlaceConnectionNoteAtTicks(data, ticks, obj.Lane, parentNote.id))
			{
				return false;
			}

			int width = _model != null && _model.LastNoteWidth > 0 ? _model.LastNoteWidth : MusicScoreMakerModel.DEFAULT_LAST_NOTE_WIDTH;
			(int laneStart, int laneEnd) = GetLaneRangeFromCenterLane(obj.Lane, width);
			MusicScoreNoteBase rootNote = startNote ?? parentNote;
			NoteCategory category = obj.NoteCategory;
			bool isSkip = obj.IsSkip;
			MusicScoreNoteBase.NoteBaseType noteBaseType = FindNoteBaseType(category);
			if ((category == NoteCategory.Connection || category == NoteCategory.Hidden) && rootNote.category == NoteCategory.Guide)
			{
				category = NoteCategory.GuideHidden;
				noteBaseType = MusicScoreNoteBase.NoteBaseType.GuideHiddenConnection;
				isSkip = false;
			}
			else if (category == NoteCategory.GuideHidden && IsLongRootCategory(rootNote.category))
			{
				category = NoteCategory.Hidden;
				noteBaseType = MusicScoreNoteBase.NoteBaseType.HiddenConnection;
				isSkip = false;
			}

			MusicScoreNoteBase newNote = new MusicScoreNoteBase(
				noteId,
				ticks,
				laneStart,
				laneEnd,
				category,
				rootNote.type,
				1f,
				obj.NoteLineType,
				noteBaseType,
				isSkip,
				NoteDirection.Default,
				-1,
				-1);

			List<MusicScoreNoteBase> connectedNotes = new List<MusicScoreNoteBase>();
			rootNote.FindConnectedNotes(noteIdCache, connectedNotes);
			MusicScoreNoteBase[] connectedNotesSnapshot = connectedNotes.ToArray();
			FindInsertNeighbors(connectedNotes, ticks, out MusicScoreNoteBase previousNote, out MusicScoreNoteBase nextNote);
			Action redo = () =>
			{
				newNote.previousConnectionId = -1;
				newNote.nextConnectionId = -1;
				data.AddNote(newNote);
				if (_model != null)
				{
					_model.LastNoteWidth = newNote.laneEnd - newNote.laneStart;
				}
				if (previousNote != null)
				{
					newNote.previousConnectionId = previousNote.id;
					previousNote.nextConnectionId = newNote.id;
				}
				if (nextNote != null)
				{
					newNote.nextConnectionId = nextNote.id;
					nextNote.previousConnectionId = newNote.id;
				}
				RefreshConnectedNotesAfterConnectionInsert(data, newNote, connectedNotesSnapshot);
				MarkEditedAndRefresh(new[] { newNote });
			};
			Action undo = () =>
			{
				data.RemoveNote(newNote);
				if (previousNote != null)
				{
					previousNote.nextConnectionId = nextNote != null ? nextNote.id : -1;
				}
				if (nextNote != null)
				{
					nextNote.previousConnectionId = previousNote != null ? previousNote.id : -1;
				}
				RefreshConnectedNotesAfterConnectionInsert(data, null, connectedNotesSnapshot);
				ShouldCleanupAndCleanupSelectionState();
				MarkEditedAndRefresh(new[] { newNote });
			};
			PushUndoableAction(undo, redo, newNote.ticks, newNote.ticks);
			return true;
		}

		private static long AbsTicks(long ticks)
		{
			if (ticks == long.MinValue)
			{
				return long.MaxValue;
			}
			return ticks < 0L ? -ticks : ticks;
		}

		private static bool HasSkipConnectionAtTicks(MusicScoreNoteBase startNote, Dictionary<int, MusicScoreNoteBase> noteIdCache, long ticks)
		{
			if (startNote == null || noteIdCache == null)
			{
				return false;
			}
			List<MusicScoreNoteBase> connectedNotes = new List<MusicScoreNoteBase>();
			startNote.FindConnectedNotes(noteIdCache, connectedNotes);
			foreach (MusicScoreNoteBase note in connectedNotes)
			{
				if (note != null && note.isSkip && note.ticks == ticks)
				{
					return true;
				}
			}
			return false;
		}

		private static bool IsLongRootCategory(NoteCategory category)
		{
			return category == NoteCategory.Long
				|| category == NoteCategory.FrictionLong
				|| category == NoteCategory.FrictionHideLong;
		}

		private static void GetLongEndNoteCategoryAndBaseType(NoteCategory startCategory, out NoteCategory endCategory, out MusicScoreNoteBase.NoteBaseType endBaseType)
		{
			switch (startCategory)
			{
				case NoteCategory.Long:
					endCategory = NoteCategory.Long;
					endBaseType = MusicScoreNoteBase.NoteBaseType.Normal;
					break;
				case NoteCategory.FrictionLong:
					endCategory = NoteCategory.Friction;
					endBaseType = MusicScoreNoteBase.NoteBaseType.Friction;
					break;
				case NoteCategory.FrictionHideLong:
					endCategory = NoteCategory.FrictionHide;
					endBaseType = MusicScoreNoteBase.NoteBaseType.FrictionHide;
					break;
				case NoteCategory.Guide:
					endCategory = NoteCategory.GuideEnd;
					endBaseType = MusicScoreNoteBase.NoteBaseType.GuideEnd;
					break;
				default:
					endCategory = startCategory;
					endBaseType = FindNoteBaseType(startCategory);
					break;
			}
		}

		private static void FindInsertNeighbors(List<MusicScoreNoteBase> connectedNotes, long ticks, out MusicScoreNoteBase previousNote, out MusicScoreNoteBase nextNote)
		{
			previousNote = null;
			nextNote = null;
			if (connectedNotes == null)
			{
				return;
			}
			foreach (MusicScoreNoteBase note in connectedNotes)
			{
				if (note == null)
				{
					continue;
				}
				if (note.ticks > ticks)
				{
					nextNote = note;
					return;
				}
				previousNote = note;
			}
		}

		private static void RefreshConnectedNotesAfterConnectionInsert(MusicScoreMakerData data, MusicScoreNoteBase newNote, MusicScoreNoteBase[] connectedNotes)
		{
			if (data == null)
			{
				return;
			}
			Dictionary<int, MusicScoreNoteBase> noteIdCache = data.GetNoteIdCacheOrRebuild();
			newNote?.UpdateConnectedNotes(noteIdCache);
			if (connectedNotes == null)
			{
				return;
			}
			foreach (MusicScoreNoteBase note in connectedNotes)
			{
				note?.UpdateConnectedNotes(noteIdCache);
			}
		}

		private void ChangeNoteLineType(ChangeNoteLineTypeEvent obj)
		{
			List<MusicScoreNoteBase> notes = GetSelectedOrSingleNotes(obj?.NoteId ?? -1);
			if (obj == null || notes.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> snapshots = CloneNotesForUndo(notes);
			Action redo = () =>
			{
				foreach (MusicScoreNoteBase note in notes)
				{
					note.noteLineType = obj.noteLineType;
				}
				MarkEditedAndRefresh(notes);
			};
			Action undo = () =>
			{
				RestoreNoteSnapshots(snapshots);
				MarkEditedAndRefresh(snapshots);
			};
			PushUndoableAction(undo, redo, notes[0].ticks, notes[0].ticks);
		}

		private void ChangeNoteCategory(ChangeNoteCategoryEvent obj)
		{
			List<MusicScoreNoteBase> notes = GetSelectedOrSingleNotes(obj?.NoteId ?? -1);
			if (obj == null || notes.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> snapshots = CloneNotesForUndo(notes);
			Action redo = () =>
			{
				foreach (MusicScoreNoteBase note in notes)
				{
					note.category = obj.NoteCategory;
					note.direction = obj.NoteDirection;
					note.noteBaseType = GetNoteBaseTypeForSingleCategoryChange(note, obj.NoteCategory);
				}
				MarkEditedAndRefresh(notes);
			};
			Action undo = () =>
			{
				RestoreNoteSnapshots(snapshots);
				MarkEditedAndRefresh(snapshots);
			};
			PushUndoableAction(undo, redo, notes[0].ticks, notes[0].ticks);
		}

		private (Action, Action) ChangeLongNoteCategory(ChangeNoteCategoryEvent obj, MusicScoreNoteBase startNote, MusicScoreNoteBase endNote, MusicScoreNoteBase targetNote, NoteCategory afterStartNoteCategory, NoteCategory afterEndNoteCategory, NoteCategory connectedNoteCategory)
		{
			List<MusicScoreNoteBase> notes = new List<MusicScoreNoteBase>();
			if (startNote != null)
			{
				notes.Add(startNote);
			}
			if (endNote != null && endNote != startNote)
			{
				notes.Add(endNote);
			}
			List<MusicScoreNoteBase> snapshots = CloneNotesForUndo(notes);
			Action redo = () =>
			{
				if (startNote != null)
				{
					startNote.category = afterStartNoteCategory;
					startNote.noteBaseType = FindNoteBaseType(afterStartNoteCategory);
				}
				if (endNote != null)
				{
					endNote.category = afterEndNoteCategory;
					endNote.noteBaseType = GetLongEndNoteBaseType(afterEndNoteCategory);
				}
			};
			Action undo = () => RestoreNoteSnapshots(snapshots);
			return (undo, redo);
		}

		private void SelectCategoryAndAddNote(SelectCategoryAndAddNoteEvent obj)
		{
			if (obj == null || _model == null || obj.Lane < 0 || obj.Lane > MusicScoreMakerModel.LaneCountMinus1)
			{
				return;
			}
			_model.SelectedToolType = obj.NoteCategory;
			_model.SelectedNoteCategory = ToolTypeToNoteCategory(obj.NoteCategory);
			AddNote(obj.Lane, obj.Ticks);
			PlayToolTypeToNoteSe();
		}

		private void ClearSelectedList()
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			data.ClearSelectedNotes();
			data.ClearSelectedTemporaryNotes();
			data.ClearSelectedEvents();
			data.ClearSelectedTemporaryEvents();
			data.SelectedTargetOperation = null;
			data.LeftExpandOperation = null;
			data.RightExpandOperation = null;
		}

		private static MusicScoreNoteBase.NoteBaseType FindNoteBaseType(NoteCategory category)
		{
			return category switch
			{
				NoteCategory.Long => MusicScoreNoteBase.NoteBaseType.Long,
				NoteCategory.Connection => MusicScoreNoteBase.NoteBaseType.Connection,
				NoteCategory.Flick => MusicScoreNoteBase.NoteBaseType.Flick,
				NoteCategory.Friction => MusicScoreNoteBase.NoteBaseType.Friction,
				NoteCategory.FrictionHide => MusicScoreNoteBase.NoteBaseType.FrictionHide,
				NoteCategory.FrictionLong => MusicScoreNoteBase.NoteBaseType.FrictionLong,
				NoteCategory.FrictionHideLong => MusicScoreNoteBase.NoteBaseType.FrictionHideLong,
				NoteCategory.FrictionFlick => MusicScoreNoteBase.NoteBaseType.FrictionFlick,
				NoteCategory.Guide => MusicScoreNoteBase.NoteBaseType.Guide,
				NoteCategory.GuideEnd => MusicScoreNoteBase.NoteBaseType.GuideEnd,
				NoteCategory.GuideHidden => MusicScoreNoteBase.NoteBaseType.GuideHiddenConnection,
				NoteCategory.Combo => MusicScoreNoteBase.NoteBaseType.LongHoldCombo,
				NoteCategory.Hidden => MusicScoreNoteBase.NoteBaseType.HiddenConnection,
				_ => MusicScoreNoteBase.NoteBaseType.Normal
			};
		}

		private static MusicScoreNoteBase.NoteBaseType GetLongEndNoteBaseType(NoteCategory category)
		{
			return category == NoteCategory.Long ? MusicScoreNoteBase.NoteBaseType.Normal : FindNoteBaseType(category);
		}

		private void SetSelectedNoteType(SetSelectedNoteTypeEvent obj)
		{
			if (_model != null && obj != null)
			{
				_model.SelectedNoteType = obj.NoteType;
				_model.IsNoteDataSelected = true;
			}
		}

		private void SetSelectedNoteDirection(SetSelectedNoteDirectionEvent obj)
		{
			if (_model != null && obj != null)
			{
				_model.SelectedNoteDirection = obj.NoteDirection;
				_model.IsNoteDataSelected = true;
			}
		}

		private void SetSelectedNoteLineType(SetSelectedNoteLineTypeEvent obj)
		{
			if (_model != null && obj != null)
			{
				_model.SelectedNoteLineType = obj.NoteLineType;
				_model.IsNoteDataSelected = true;
			}
		}

		private void ChangeNoteSkip(ChangeNoteSkipEvent obj)
		{
			List<MusicScoreNoteBase> notes = GetSelectedOrSingleNotes(obj?.NoteId ?? -1);
			if (obj == null || notes.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> snapshots = CloneNotesForUndo(notes);
			Action redo = () =>
			{
				foreach (MusicScoreNoteBase note in notes)
				{
					note.isSkip = obj.IsSkip;
				}
				MarkEditedAndRefresh(notes);
			};
			Action undo = () =>
			{
				RestoreNoteSnapshots(snapshots);
				MarkEditedAndRefresh(snapshots);
			};
			PushUndoableAction(undo, redo, notes[0].ticks, notes[0].ticks);
		}

		private void FlipSelectedNotesHorizontally(FlipSelectedNotesHorizontallyEvent obj)
		{
			List<MusicScoreNoteBase> notes = GetSelectedOrSingleNotes(-1);
			if (notes.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> snapshots = CloneNotesForUndo(notes);
			Action redo = () =>
			{
				foreach (MusicScoreNoteBase note in notes)
				{
					FlipNoteLanePosition(note);
					FlipNoteDirection(note);
				}
				MarkEditedAndRefresh(notes);
			};
			Action undo = () =>
			{
				RestoreNoteSnapshots(snapshots);
				MarkEditedAndRefresh(snapshots);
			};
			PushUndoableAction(undo, redo, notes[0].ticks, notes[0].ticks);
		}

		private void FlipNoteDirection(MusicScoreNoteBase note)
		{
			if (note == null)
			{
				return;
			}
			if (note.direction == NoteDirection.Left)
			{
				note.direction = NoteDirection.Right;
			}
			else if (note.direction == NoteDirection.Right)
			{
				note.direction = NoteDirection.Left;
			}
		}

		private static void FlipNoteLanePosition(MusicScoreNoteBase note)
		{
			if (note == null)
			{
				return;
			}
			int newStart = MusicScoreMakerModel.LaneCountMinus1 - note.laneEnd;
			int newEnd = MusicScoreMakerModel.LaneCountMinus1 - note.laneStart;
			note.laneStart = MusicScoreMakerUtility.ClampLaneStart(newStart, newEnd);
			note.laneEnd = MusicScoreMakerUtility.ClampLaneEnd(newEnd, note.laneStart);
		}

		private void FlipSelectedNotesVertically(FlipSelectedNotesVerticallyEvent obj)
		{
			MusicScoreMakerData data = CurrentData();
			if (data?.NoteList == null)
			{
				return;
			}
			HashSet<int> selectedNoteIds = data.SelectedNoteTargetIdSet;
			List<MusicScoreNoteBase> selectedNotes = new List<MusicScoreNoteBase>();
			foreach (MusicScoreNoteBase note in data.NoteList)
			{
				if (note != null && selectedNoteIds.Contains(note.id))
				{
					selectedNotes.Add(note);
				}
			}
			if (selectedNotes.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> selectedSingleNotes = FindSelectedNoteList(selectedNotes);
			List<MusicScoreNoteBase> selectedLongRoots = FindSelectedLongNoteList(selectedNotes, selectedNoteIds, isOnlyStartNote: true);
			List<MusicScoreNoteBase> affectedNotes = CollectVerticalFlipAffectedNotes(selectedSingleNotes, selectedLongRoots);
			if (affectedNotes.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> snapshots = CloneNotesForUndo(affectedNotes);
			long minTicks = long.MaxValue;
			long maxTicks = long.MinValue;
			foreach (MusicScoreNoteBase note in selectedNotes)
			{
				minTicks = Math.Min(minTicks, note.ticks);
				maxTicks = Math.Max(maxTicks, note.ticks);
			}
			Action redo = () =>
			{
				HashSet<long> editedTicks = CollectNoteTicks(affectedNotes);
				foreach (MusicScoreNoteBase note in selectedSingleNotes)
				{
					note.ticks = ClampTicksToValidRange(maxTicks - (note.ticks - minTicks));
				}
				foreach (MusicScoreNoteBase note in selectedLongRoots)
				{
					FlipConnectedNoteChainVertically(data, note, minTicks, maxTicks);
				}
				data.MarkNoteListOrderDirty();
				data.UpdateConnectionNotes();
				editedTicks.UnionWith(CollectNoteTicks(affectedNotes));
				MarkEditedTicksAndRefresh(editedTicks);
			};
			Action undo = () =>
			{
				HashSet<long> editedTicks = CollectNoteTicks(affectedNotes);
				RestoreNoteSnapshots(snapshots);
				editedTicks.UnionWith(CollectNoteTicks(affectedNotes));
				MarkEditedTicksAndRefresh(editedTicks);
			};
			PushUndoableAction(undo, redo, minTicks, minTicks);
		}

		private List<MusicScoreNoteBase> CollectVerticalFlipAffectedNotes(List<MusicScoreNoteBase> selectedSingleNotes, List<MusicScoreNoteBase> selectedLongRoots)
		{
			List<MusicScoreNoteBase> result = new List<MusicScoreNoteBase>();
			HashSet<int> ids = new HashSet<int>();
			AddUniqueNotes(result, ids, selectedSingleNotes);
			if (selectedLongRoots != null)
			{
				foreach (MusicScoreNoteBase root in selectedLongRoots)
				{
					if (root?.ConnectedNotes != null && root.ConnectedNotes.Count > 0)
					{
						AddUniqueNotes(result, ids, root.ConnectedNotes);
					}
					else if (root != null && ids.Add(root.id))
					{
						result.Add(root);
					}
				}
			}
			return result;
		}

		private static void AddUniqueNotes(List<MusicScoreNoteBase> target, HashSet<int> ids, IEnumerable<MusicScoreNoteBase> notes)
		{
			if (target == null || ids == null || notes == null)
			{
				return;
			}
			foreach (MusicScoreNoteBase note in notes)
			{
				if (note != null && ids.Add(note.id))
				{
					target.Add(note);
				}
			}
		}

		private static HashSet<long> CollectNoteTicks(IEnumerable<MusicScoreNoteBase> notes)
		{
			HashSet<long> result = new HashSet<long>();
			if (notes == null)
			{
				return result;
			}
			foreach (MusicScoreNoteBase note in notes)
			{
				if (note != null)
				{
					result.Add(note.ticks);
				}
			}
			return result;
		}

		private void MarkEditedTicksAndRefresh(IEnumerable<long> editedTicks)
		{
			MusicScoreMakerUtility.AddEditedTicks(editedTicks);
			RecheckJudgmentNoteGapIfEditedInGapRange();
			NotifyMusicScoreAndTimelineChanged();
		}

		private void FlipConnectedNoteChainVertically(MusicScoreMakerData data, MusicScoreNoteBase root, long minTicks, long maxTicks)
		{
			if (data == null || root?.ConnectedNotes == null)
			{
				return;
			}
			foreach (MusicScoreNoteBase note in root.ConnectedNotes)
			{
				if (note == null)
				{
					continue;
				}
				note.ticks = ClampTicksToValidRange(maxTicks - (note.ticks - minTicks));
				SwapConnectionIds(note);
			}
			Dictionary<int, MusicScoreNoteBase> noteIdCache = data.GetNoteIdCacheOrRebuild();
			MusicScoreNoteBase startNote = root.FindStartNote(noteIdCache);
			MusicScoreNoteBase endNote = root.FindEndNote(noteIdCache);
			SwapEndpointNoteData(startNote, endNote);
		}

		private static void SwapConnectionIds(MusicScoreNoteBase note)
		{
			if (note == null)
			{
				return;
			}
			int previousConnectionId = note.previousConnectionId;
			note.previousConnectionId = note.nextConnectionId;
			note.nextConnectionId = previousConnectionId;
		}

		private static void SwapEndpointNoteData(MusicScoreNoteBase startNote, MusicScoreNoteBase endNote)
		{
			if (startNote == null || endNote == null || startNote == endNote)
			{
				return;
			}
			MusicScoreNoteBase.NoteBaseType noteBaseType = startNote.noteBaseType;
			startNote.noteBaseType = endNote.noteBaseType;
			endNote.noteBaseType = noteBaseType;

			NoteDirection direction = startNote.direction;
			startNote.direction = endNote.direction;
			endNote.direction = direction;

			NoteCategory category = startNote.category;
			startNote.category = endNote.category;
			endNote.category = category;
		}

		private static List<MusicScoreNoteBase> FindSelectedNoteList(List<MusicScoreNoteBase> selectedNoteList)
		{
			List<MusicScoreNoteBase> result = new List<MusicScoreNoteBase>();
			if (selectedNoteList == null)
			{
				return result;
			}
			foreach (MusicScoreNoteBase note in selectedNoteList)
			{
				if (note != null && note.IsSingle)
				{
					result.Add(note);
				}
			}
			return result;
		}

		private List<MusicScoreNoteBase> FindSelectedLongNoteList(List<MusicScoreNoteBase> selectedNoteList, HashSet<int> allSelectedIds, bool isOnlyStartNote = false)
		{
			List<MusicScoreNoteBase> result = new List<MusicScoreNoteBase>();
			MusicScoreMakerData data = CurrentData();
			if (selectedNoteList == null || allSelectedIds == null || data == null)
			{
				return result;
			}
			Dictionary<int, MusicScoreNoteBase> noteIdCache = data.GetNoteIdCacheOrRebuild();
			List<MusicScoreNoteBase> connectedNotes = new List<MusicScoreNoteBase>();
			foreach (MusicScoreNoteBase note in selectedNoteList)
			{
				if (note == null || !note.IsConnectedFirst)
				{
					continue;
				}
				note.FindConnectedNotes(noteIdCache, connectedNotes);
				bool isAllSelected = true;
				foreach (MusicScoreNoteBase connectedNote in connectedNotes)
				{
					if (connectedNote == null || !allSelectedIds.Contains(connectedNote.id))
					{
						isAllSelected = false;
						break;
					}
				}
				if (!isAllSelected)
				{
					continue;
				}
				note.ConnectedNotes.Clear();
				note.ConnectedNotes.AddRange(connectedNotes);
				if (isOnlyStartNote)
				{
					result.Add(note);
				}
				else
				{
					result.AddRange(connectedNotes);
				}
			}
			return result;
		}

		private void SetSelectedNoteData(SetSelectedNoteDataEvent obj)
		{
			if (_model == null || obj == null)
			{
				return;
			}

			if (_model.IsEventSettingMode)
			{
				DisableEventSettingModeInternal();
			}

			bool shouldSelect = !_model.IsNoteDataSelected
				|| _model.SelectedNoteCategory != obj.NoteCategory
				|| _model.SelectedNoteDirection != obj.NoteDirection
				|| _model.SelectedNoteType != obj.NoteType
				|| _model.SelectedNoteLineType != obj.NoteLineType
				|| _model.SelectedIsSkip != obj.IsSkip;

			_model.SelectedEventSettingModeType = null;
			if (shouldSelect)
			{
				_model.SelectedNoteDirection = obj.NoteDirection;
				_model.SelectedNoteLineType = obj.NoteLineType;
				_model.SelectedNoteCategory = obj.NoteCategory;
				_model.SelectedNoteType = obj.NoteType;
				_model.SelectedIsSkip = obj.IsSkip;
				_model.IsNoteDataSelected = true;
				if ((int)obj.NoteCategory <= 7 && ((1 << (int)obj.NoteCategory) & 0xC2) != 0)
				{
					MusicScoreMakerRuleSlideTutorialUtility.TryShowTutorialSlideIfFirstTime("long");
				}
			}
			else
			{
				_model.IsNoteDataSelected = false;
			}

			MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateButtonSelectionStateEvent());
		}

		private void ChangeNoteData(ChangeNoteDataEvent obj)
		{
			if (obj == null)
			{
				return;
			}
			MusicScoreMakerData data = CurrentData();
			if (data == null || data.SelectedNoteIdList == null || data.SelectedNoteIdList.Count == 0)
			{
				return;
			}

			List<int> selectedNoteIdList = new List<int>(data.SelectedNoteIdList);
			Dictionary<int, MusicScoreNoteBase> noteIdCache = data.GetNoteIdCacheOrRebuild();
			List<NoteDataSnapshot> snapshots = new List<NoteDataSnapshot>(selectedNoteIdList.Count);
			HashSet<int> addedNoteIdSet = new HashSet<int>();
			List<MusicScoreNoteBase> connectedNotesBuffer = new List<MusicScoreNoteBase>(16);
			List<ConnectedNoteTypeChangeInfo> connectedNotesForTypeChange = new List<ConnectedNoteTypeChangeInfo>();
			List<ConnectedNoteGuideConversionInfo> connectedNotesForGuideConversion = new List<ConnectedNoteGuideConversionInfo>();
			List<MusicScoreNoteBase> connectedNotesToDeleteForGuideConversion = new List<MusicScoreNoteBase>();
			CollectSnapshotsForChangeNoteData(
				selectedNoteIdList,
				obj,
				noteIdCache,
				snapshots,
				addedNoteIdSet,
				connectedNotesBuffer,
				connectedNotesForTypeChange,
				connectedNotesForGuideConversion,
				connectedNotesToDeleteForGuideConversion);
			if (snapshots.Count == 0)
			{
				return;
			}

			Action redoCore = CreateDoActionForChangeNoteData(
				obj,
				snapshots,
				connectedNotesForTypeChange,
				connectedNotesForGuideConversion,
				connectedNotesToDeleteForGuideConversion,
				noteIdCache,
				connectedNotesBuffer);
			Action undoCore = CreateUndoActionForChangeNoteData(
				snapshots,
				connectedNotesForTypeChange,
				connectedNotesForGuideConversion,
				connectedNotesToDeleteForGuideConversion);
			Action redo = () =>
			{
				redoCore?.Invoke();
				data.MarkNoteListOrderDirty();
				data.UpdateConnectionNotes();
				AddEditedTicksForNoteDataChange(snapshots, connectedNotesForTypeChange, connectedNotesForGuideConversion);
				RecheckJudgmentNoteGapIfEditedInGapRange();
				NotifyMusicScoreAndTimelineChanged();
				MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateButtonSelectionStateEvent());
			};
			Action undo = () =>
			{
				undoCore?.Invoke();
				data.MarkNoteListOrderDirty();
				data.UpdateConnectionNotes();
				AddEditedTicksForNoteDataChange(snapshots, connectedNotesForTypeChange, connectedNotesForGuideConversion);
				RecheckJudgmentNoteGapIfEditedInGapRange();
				NotifyMusicScoreAndTimelineChanged();
				MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateButtonSelectionStateEvent());
			};

			long focusTicks = long.MaxValue;
			foreach (int selectedNoteId in selectedNoteIdList)
			{
				if (noteIdCache.TryGetValue(selectedNoteId, out MusicScoreNoteBase note) && note != null && note.ticks < focusTicks)
				{
					focusTicks = note.ticks;
				}
			}
			if (focusTicks == long.MaxValue)
			{
				focusTicks = -1L;
			}
			PushUndoableAction(undo, redo, focusTicks, focusTicks);
			MusicScoreMakerEventDispatcher.Instance.Publish(new HideNoteChangeSubWindowEvent());
		}

		private void CollectSnapshotsForChangeNoteData(List<int> selectedNoteIdList, ChangeNoteDataEvent obj, Dictionary<int, MusicScoreNoteBase> noteIdCache, List<NoteDataSnapshot> snapshots, HashSet<int> addedNoteIdSet, List<MusicScoreNoteBase> connectedNotesBuffer, List<ConnectedNoteTypeChangeInfo> connectedNotesForTypeChange, List<ConnectedNoteGuideConversionInfo> connectedNotesForGuideConversion, List<MusicScoreNoteBase> connectedNotesToDeleteForGuideConversion)
		{
			if (selectedNoteIdList == null || noteIdCache == null || snapshots == null)
			{
				return;
			}
			foreach (int id in selectedNoteIdList)
			{
				if (!noteIdCache.TryGetValue(id, out MusicScoreNoteBase note) || note == null || addedNoteIdSet.Contains(id))
				{
					continue;
				}
				addedNoteIdSet.Add(id);
				snapshots.Add(new NoteDataSnapshot
				{
					Note = note,
					Category = note.category,
					Direction = note.direction,
					Type = note.type,
					LineType = note.noteLineType,
					IsSkip = note.isSkip,
					BaseType = note.noteBaseType
				});
			}
		}

		private static void CollectSnapshotsForGuideConversion(MusicScoreNoteBase targetNote, int noteId, Dictionary<int, MusicScoreNoteBase> noteIdCache, Action<MusicScoreNoteBase> addSnapshot, List<MusicScoreNoteBase> connectedNotesBuffer, List<ConnectedNoteGuideConversionInfo> connectedNotesForGuideConversion, List<MusicScoreNoteBase> connectedNotesToDeleteForGuideConversion, NoteCategory afterCategory)
		{
			addSnapshot?.Invoke(targetNote);
		}

		private static void CollectSnapshotsForTypeChange(MusicScoreNoteBase targetNote, Action<MusicScoreNoteBase> addSnapshot, Dictionary<int, MusicScoreNoteBase> noteIdCache, List<MusicScoreNoteBase> connectedNotesBuffer, List<ConnectedNoteTypeChangeInfo> connectedNotesForTypeChange)
		{
			addSnapshot?.Invoke(targetNote);
		}

		private static Action CreateUndoActionForChangeNoteData(List<NoteDataSnapshot> snapshots, List<ConnectedNoteTypeChangeInfo> connectedNotesForTypeChange, List<ConnectedNoteGuideConversionInfo> connectedNotesForGuideConversion, List<MusicScoreNoteBase> connectedNotesToDeleteForGuideConversion)
		{
			return () =>
			{
				if (snapshots == null)
				{
					return;
				}
				foreach (NoteDataSnapshot snapshot in snapshots)
				{
					if (snapshot.Note == null)
					{
						continue;
					}
					snapshot.Note.category = snapshot.Category;
					snapshot.Note.direction = snapshot.Direction;
					snapshot.Note.type = snapshot.Type;
					snapshot.Note.noteLineType = snapshot.LineType;
					snapshot.Note.isSkip = snapshot.IsSkip;
					snapshot.Note.noteBaseType = snapshot.BaseType;
				}
			};
		}

		private static Action CreateDoActionForChangeNoteData(ChangeNoteDataEvent obj, List<NoteDataSnapshot> snapshots, List<ConnectedNoteTypeChangeInfo> connectedNotesForTypeChange, List<ConnectedNoteGuideConversionInfo> connectedNotesForGuideConversion, List<MusicScoreNoteBase> connectedNotesToDeleteForGuideConversion, Dictionary<int, MusicScoreNoteBase> noteIdCache, List<MusicScoreNoteBase> connectedNotesBuffer)
		{
			return () => ApplyNoteChanges(snapshots, obj.NoteCategory, obj.NoteDirection, obj.NoteType, obj.NoteLineType, obj.IsSkip, noteIdCache, connectedNotesBuffer);
		}

		private static void AddEditedTicksForNoteDataChange(List<NoteDataSnapshot> snapshots, List<ConnectedNoteTypeChangeInfo> connectedNotesForTypeChange, List<ConnectedNoteGuideConversionInfo> connectedNotesForGuideConversion)
		{
			if (snapshots == null)
			{
				return;
			}
			foreach (NoteDataSnapshot snapshot in snapshots)
			{
				if (snapshot.Note != null)
				{
					MusicScoreMakerUtility.AddEditedTick(snapshot.Note.ticks);
				}
			}
		}

		private static void ApplyNoteChanges(List<NoteDataSnapshot> snapshots, NoteCategory afterCategory, NoteDirection afterDirection, NoteType afterType, NoteLineType afterLineType, bool afterIsSkip, Dictionary<int, MusicScoreNoteBase> noteIdCache, List<MusicScoreNoteBase> connectedNotesBuffer)
		{
			if (snapshots == null)
			{
				return;
			}
			foreach (NoteDataSnapshot snapshot in snapshots)
			{
				ApplyNormalChange(snapshot.Note, afterCategory, afterDirection, afterType, afterLineType, afterIsSkip, noteIdCache, connectedNotesBuffer);
			}
		}

		private static void ApplyGuideConversion(MusicScoreNoteBase note, NoteCategory afterCategory, NoteDirection afterDirection, NoteType afterType, NoteLineType afterLineType, bool afterIsSkip, Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			ApplyNormalChange(note, afterCategory, afterDirection, afterType, afterLineType, afterIsSkip, noteIdCache, null);
		}

		private static void ApplyNormalChange(MusicScoreNoteBase note, NoteCategory afterCategory, NoteDirection afterDirection, NoteType afterType, NoteLineType afterLineType, bool afterIsSkip, Dictionary<int, MusicScoreNoteBase> noteIdCache, List<MusicScoreNoteBase> connectedNotesBuffer)
		{
			if (note == null)
			{
				return;
			}
			note.category = afterCategory;
			note.direction = afterDirection;
			note.type = afterType;
			note.noteLineType = afterLineType;
			note.isSkip = afterIsSkip;
			note.noteBaseType = FindNoteBaseType(afterCategory);
		}

		private static void UpdateConnectedNotesForGuideConversion(List<ConnectedNoteGuideConversionInfo> connectedNotesForGuideConversion, NoteCategory afterCategory, NoteType afterType)
		{
			if (connectedNotesForGuideConversion == null)
			{
				return;
			}
			foreach (ConnectedNoteGuideConversionInfo info in connectedNotesForGuideConversion)
			{
				if (info.Note != null)
				{
					info.Note.category = afterCategory;
					info.Note.type = afterType;
					info.Note.noteBaseType = FindNoteBaseType(afterCategory);
				}
			}
		}

		private static void UpdateConnectedNotesForTypeChange(List<ConnectedNoteTypeChangeInfo> connectedNotesForTypeChange, NoteType afterType)
		{
			if (connectedNotesForTypeChange == null)
			{
				return;
			}
			foreach (ConnectedNoteTypeChangeInfo info in connectedNotesForTypeChange)
			{
				if (info.Note != null)
				{
					info.Note.type = afterType;
				}
			}
		}

		private static void CalcSkipNoteLaneStartEndPosition(bool wasSkip, bool afterIsSkip, MusicScoreNoteBase note, Dictionary<int, MusicScoreNoteBase> noteIdCache, List<MusicScoreNoteBase> connectedNotesBuffer)
		{
			if (note == null || !afterIsSkip || noteIdCache == null || connectedNotesBuffer == null)
			{
				return;
			}
			(int laneStart, int laneEnd) = MusicScoreMakerUtility.CalcSkipNoteLaneForMusicScoreNoteBase(note, noteIdCache, connectedNotesBuffer);
			note.laneStart = laneStart;
			note.laneEnd = laneEnd;
		}

		private NoteCategory GetSelectedNoteCategory(GetSelectedNoteCategoryEvent obj)
		{
			return _model?.SelectedNoteCategory ?? NoteCategory.Normal;
		}

		private bool GetIsNoteDataSelected(GetIsNoteDataSelectedEvent obj)
		{
			return _model?.IsNoteDataSelected ?? false;
		}

		private void SetupOperationEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<UndoEvent>(Undo);
			dispatcher.Register<RedoEvent>(Redo);
		}

		private void DisposeOperationEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<UndoEvent>(Undo);
			dispatcher.Remove<RedoEvent>(Redo);
		}

		private void Undo(UndoEvent evt)
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Undo();
				NotifyMusicScoreAndTimelineChanged();
			}
		}

		private void Redo(RedoEvent evt)
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Redo();
				NotifyMusicScoreAndTimelineChanged();
			}
		}

		private bool ShouldCleanupAndCleanupSelectionState()
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return false;
			}
			bool hasOperation = data.SelectedTargetOperation != null
				&& (data.SelectedTargetOperation.deltaLane != 0 || data.SelectedTargetOperation.deltaTicks != 0);
			if (!hasOperation)
			{
				data.SelectedTargetOperation = null;
				return false;
			}
			return true;
		}

		private void RegisterAndExecuteOperationAction(SelectedTargetOperation operation)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || operation == null || (operation.deltaLane == 0 && operation.deltaTicks == 0))
			{
				return;
			}
			Dictionary<int, (NoteOperation before, NoteOperation after)> noteDataDict = CalcBeforeAfterNotesData(operation);
			Dictionary<int, (EventOperation before, EventOperation after)> eventDataDict = CalcBeforeAfterEventData(operation.deltaTicks);
			if (noteDataDict.Count == 0 && eventDataDict.Count == 0)
			{
				data.SelectedTargetOperation = null;
				return;
			}
			long beforeFocusTicks = _model?.FocusTicks ?? -1L;
			Action redo = () =>
			{
				ApplyOperationData(noteDataDict, eventDataDict, useAfter: true, beforeFocusTicks);
				data.SelectedTargetOperation = null;
				MusicScoreMakerUtility.AddEditedTicks(CollectEditedTicksFromOperations(noteDataDict, eventDataDict, useAfter: true));
				RecheckJudgmentNoteGapIfEditedInGapRange();
				NotifyMusicScoreAndTimelineChanged();
			};
			Action undo = () =>
			{
				ApplyOperationData(noteDataDict, eventDataDict, useAfter: false, beforeFocusTicks);
				data.SelectedTargetOperation = null;
				MusicScoreMakerUtility.AddEditedTicks(CollectEditedTicksFromOperations(noteDataDict, eventDataDict, useAfter: false));
				RecheckJudgmentNoteGapIfEditedInGapRange();
				NotifyMusicScoreAndTimelineChanged();
			};
			PushUndoableAction(undo, redo, beforeFocusTicks, beforeFocusTicks);
			UpdateLastNoteWidthFromExpandOperation(operation, noteDataDict);
		}

		private void UpdateLastNoteWidthFromExpandOperation(SelectedTargetOperation operation, Dictionary<int, (NoteOperation before, NoteOperation after)> noteDataDict)
		{
			if (_model == null || operation == null || noteDataDict == null || noteDataDict.Count == 0)
			{
				return;
			}
			if (operation.noteTapPosition != SelectedTargetOperation.NoteTapPosition.left
				&& operation.noteTapPosition != SelectedTargetOperation.NoteTapPosition.right)
			{
				return;
			}
			foreach ((NoteOperation before, NoteOperation after) in noteDataDict.Values)
			{
				int width = after.EndLane - after.StartLane;
				if (width > 0)
				{
					_model.LastNoteWidth = width;
					break;
				}
			}
		}

		private Dictionary<int, (NoteOperation, NoteOperation)> CalcBeforeAfterNotesData(SelectedTargetOperation selectedTargetOperation)
		{
			Dictionary<int, (NoteOperation, NoteOperation)> result = new Dictionary<int, (NoteOperation, NoteOperation)>();
			MusicScoreMakerData data = CurrentData();
			if (data == null || selectedTargetOperation == null)
			{
				return result;
			}
			foreach (int noteId in data.SelectedNoteTargetIdSet)
			{
				CalcBeforeAfterNoteData(selectedTargetOperation, noteId, result);
			}
			return result;
		}

		private void CalcBeforeAfterNoteData(SelectedTargetOperation selectedTargetOperation, int noteId, Dictionary<int, (NoteOperation before, NoteOperation after)> result)
		{
			MusicScoreMakerData data = CurrentData();
			MusicScoreNoteBase note = data?.FindNote(noteId);
			if (data == null || note == null || result == null)
			{
				return;
			}
			NoteOperation before = note.GetCurrentNoteOperation();
			NoteOperation after = note.CalcMoveOperation(selectedTargetOperation, data);
			if (before != after)
			{
				result[noteId] = (before, after);
			}
		}

		private Dictionary<int, (EventOperation, EventOperation)> CalcBeforeAfterEventData(long deltaTicks)
		{
			Dictionary<int, (EventOperation, EventOperation)> result = new Dictionary<int, (EventOperation, EventOperation)>();
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return result;
			}
			foreach (int eventId in data.SelectedEventTargetIdSet)
			{
				MusicScoreEventData eventData = FindEventById(data.MusicScoreEventDataList, eventId);
				if (eventData == null)
				{
					continue;
				}
				EventOperation before = eventData.GetData();
				EventOperation after = new EventOperation(eventData.id, MusicScoreMakerUtility.CalcEventOperation(data, eventData.ticks, eventData.id));
				if (before.Ticks != after.Ticks)
				{
					result[eventId] = (before, after);
				}
			}
			return result;
		}

		private void ApplyOperationData(Dictionary<int, (NoteOperation before, NoteOperation after)> noteDataDict, Dictionary<int, (EventOperation before, EventOperation after)> eventDataDict, bool useAfter, long focusTicks)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			foreach (KeyValuePair<int, (NoteOperation before, NoteOperation after)> pair in noteDataDict)
			{
				MusicScoreNoteBase note = data.FindNote(pair.Key);
				if (note != null)
				{
					note.SetData(useAfter ? pair.Value.after : pair.Value.before);
				}
			}
			foreach (KeyValuePair<int, (EventOperation before, EventOperation after)> pair in eventDataDict)
			{
				MusicScoreEventData eventData = FindEventById(data.MusicScoreEventDataList, pair.Key);
				if (eventData != null)
				{
					eventData.SetData(useAfter ? pair.Value.after : pair.Value.before);
				}
			}
			data.MarkNoteListOrderDirty();
			data.UpdateConnectionNotes();
			if (focusTicks >= 0L)
			{
				SetFocusTicks(focusTicks);
			}
		}

		private static IEnumerable<long> CollectEditedTicksFromOperations(Dictionary<int, (NoteOperation before, NoteOperation after)> noteDataDict, Dictionary<int, (EventOperation before, EventOperation after)> eventDataDict, bool useAfter)
		{
			HashSet<long> ticks = new HashSet<long>();
			foreach (KeyValuePair<int, (NoteOperation before, NoteOperation after)> pair in noteDataDict)
			{
				ticks.Add(pair.Value.before.Ticks);
				ticks.Add(pair.Value.after.Ticks);
			}
			foreach (KeyValuePair<int, (EventOperation before, EventOperation after)> pair in eventDataDict)
			{
				ticks.Add(pair.Value.before.Ticks);
				ticks.Add(pair.Value.after.Ticks);
			}
			return ticks;
		}

		private void SetupPreviewEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<OnNotePreviewClickEvent>(OnNotePreviewClick);
			dispatcher.Register<OnNotePreviewDragEvent>(OnNotePreviewDrag);
			dispatcher.Register<OnNotePreviewPointerUpEvent>(OnNotePreviewPointerUp);
			dispatcher.Register<OnMusicScorePreviewClickEvent>(OnMusicScorePreviewClick);
			dispatcher.Register<OnMusicScorePreviewDragEvent>(OnMusicScorePreviewDrag);
			dispatcher.Register<OnMusicScorePreviewPointerUpEvent>(OnMusicScorePreviewPointerUp);
			dispatcher.Register<OnMusicScorePreviewPointerDownEvent>(OnMusicScorePreviewPointerDown);
			dispatcher.Register<CopySelectedNotesAndEventsEvent>(CopySelectedNotesAndEvents);
			dispatcher.Register<ShowClipboardCacheListEvent>(ShowClipboardCacheList);
			dispatcher.Register<PasteFromClipboardCacheEvent>(PasteFromClipboardCache);
			dispatcher.Register<OnExpandInputDragEvent>(OnExpandInputDrag);
			dispatcher.Register<OnExpandInputPointerUpEvent>(OnExpandInputPointerUp);
			dispatcher.Register<SelectAllConnectedNotesEvent>(SelectAllConnectedNotes);
			dispatcher.Register<SetFocusTicksEvent>(OnSetFocusTicksForAreaSelect);
		}

		private void DisposePreviewEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<OnNotePreviewClickEvent>(OnNotePreviewClick);
			dispatcher.Remove<OnNotePreviewDragEvent>(OnNotePreviewDrag);
			dispatcher.Remove<OnNotePreviewPointerUpEvent>(OnNotePreviewPointerUp);
			dispatcher.Remove<OnMusicScorePreviewClickEvent>(OnMusicScorePreviewClick);
			dispatcher.Remove<OnMusicScorePreviewDragEvent>(OnMusicScorePreviewDrag);
			dispatcher.Remove<OnMusicScorePreviewPointerUpEvent>(OnMusicScorePreviewPointerUp);
			dispatcher.Remove<OnMusicScorePreviewPointerDownEvent>(OnMusicScorePreviewPointerDown);
			dispatcher.Remove<CopySelectedNotesAndEventsEvent>(CopySelectedNotesAndEvents);
			dispatcher.Remove<ShowClipboardCacheListEvent>(ShowClipboardCacheList);
			dispatcher.Remove<PasteFromClipboardCacheEvent>(PasteFromClipboardCache);
			dispatcher.Remove<OnExpandInputDragEvent>(OnExpandInputDrag);
			dispatcher.Remove<OnExpandInputPointerUpEvent>(OnExpandInputPointerUp);
			dispatcher.Remove<SelectAllConnectedNotesEvent>(SelectAllConnectedNotes);
			dispatcher.Remove<SetFocusTicksEvent>(OnSetFocusTicksForAreaSelect);
		}

		private void OnNotePreviewClick(OnNotePreviewClickEvent obj)
		{
			if (obj == null)
			{
				return;
			}
			if (_model?.IsEditRestricted == true || _model?.IsEventSettingMode == true)
			{
				return;
			}
			if (_model?.RemoveMode == true)
			{
				RemoveNote(obj);
				return;
			}
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}

			bool shouldPlayChoiceSe = false;
			if (data.SelectedNoteIdList.Contains(obj.NoteId))
			{
				if (obj.PointerEventData != null)
				{
					FindNotesAtScreenPosition(obj.PointerEventData.position, _overlappingNotesCache);
				}
				else
				{
					FindOverlappingNotesByTicks(data.FindNote(obj.NoteId), _overlappingNotesCache);
				}

				if (_overlappingNotesCache.Count < 2)
				{
					data.RemoveSelectedNote(obj.NoteId);
					ResetCycleSelection();
				}
				else
				{
					if (!AreListsEqual(_cycleSelectionNoteIds, _overlappingNotesCache))
					{
						_cycleSelectionNoteIds.Clear();
						_cycleSelectionNoteIds.AddRange(_overlappingNotesCache);
						_cycleSelectionIndex = _cycleSelectionNoteIds.IndexOf(obj.NoteId);
					}

					_cycleSelectionIndex = (_cycleSelectionIndex + 1) % _cycleSelectionNoteIds.Count;
					int selectedNoteId = _cycleSelectionNoteIds[_cycleSelectionIndex];
					data.ClearSelectedNotes();
					data.ClearSelectedEvents();
					data.AddSelectedNote(selectedNoteId);
					shouldPlayChoiceSe = true;
				}
			}
			else
			{
				data.ClearSelectedNotes();
				data.ClearSelectedEvents();
				data.AddSelectedNote(obj.NoteId);
				ResetCycleSelection();
				shouldPlayChoiceSe = true;
			}
			if (shouldPlayChoiceSe)
			{
				PlayScoreMakerSe(SE_SCORE_NOTES_CHOICE_CUE_NAME, SE_DECIDE_CUE_NAME);
			}
			NotifyMusicScoreAndTimelineChanged();
		}

		private void FindNotesAtScreenPosition(Vector2 screenPosition, List<int> result)
		{
			result?.Clear();
			Dictionary<int, NotePreview> noteDict = _view != null ? _view.NoteDict : null;
			if (noteDict == null || result == null)
			{
				return;
			}
			foreach (KeyValuePair<int, NotePreview> pair in noteDict)
			{
				NotePreview notePreview = pair.Value;
				if (notePreview != null && notePreview.IsUsing && notePreview.ContainsScreenPoint(screenPosition))
				{
					result.Add(pair.Key);
				}
			}
			result.Sort();
		}

		private void FindOverlappingNotesByTicks(MusicScoreNoteBase targetNote, List<int> result)
		{
			result?.Clear();
			MusicScoreMakerData data = CurrentData();
			if (targetNote == null || data == null || result == null)
			{
				return;
			}
			foreach (MusicScoreNoteBase note in data.NoteList ?? new List<MusicScoreNoteBase>())
			{
				if (note != null && note.ticks == targetNote.ticks)
				{
					result.Add(note.id);
				}
			}
			result.Sort();
		}

		private void ResetCycleSelection()
		{
			_cycleSelectionNoteIds.Clear();
			_cycleSelectionIndex = 0;
		}

		private static bool AreListsEqual(List<int> a, List<int> b)
		{
			if (ReferenceEquals(a, b))
			{
				return true;
			}
			if (a == null || b == null || a.Count != b.Count)
			{
				return false;
			}
			for (int i = 0; i < a.Count; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		private void OnNotePreviewDrag(OnNotePreviewDragEvent obj)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || obj?.PointerEventData == null)
			{
				return;
			}
			CancelScrollInertia();
			if (_model?.IsEditRestricted == true || _model?.IsEventSettingMode == true)
			{
				return;
			}
			if (data.SelectedTemporaryNoteIdList.Count == 0)
			{
				if (!data.SelectedNoteIdList.Contains(obj.NoteId))
				{
					data.ClearSelectedNotes();
					data.ClearSelectedEvents();
				}
				_prevDragDeltaTicks = 0L;
				_prevDragDeltaLane = 0;
				_prevDragQuantizedTicks = long.MinValue;
				_dragStartFocusTicks = _model?.FocusTicks ?? MusicScoreMakerUtility.GetFocusTicks();
			}
			if (!data.SelectedTemporaryNoteIdList.Contains(obj.NoteId))
			{
				data.AddSelectedTemporaryNote(obj.NoteId);
			}
			if (!TryCreateNoteDragOperation(obj.PointerEventData, obj.NoteTapPosition, out SelectedTargetOperation operation))
			{
				return;
			}
			PlayNoteDragSeIfNeeded(operation);
			data.SelectedTargetOperation = operation;
			_prevDragDeltaTicks = operation.deltaTicks;
			NotifyMusicScoreAndTimelineChanged();
		}

		private void OnNotePreviewPointerUp(OnNotePreviewPointerUpEvent obj)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || obj == null)
			{
				return;
			}
			if (_model?.IsEditRestricted == true || _model?.IsEventSettingMode == true || obj.IsLongPress)
			{
				CleanupNoteDragState(data);
				return;
			}
			if (obj.IsDragging)
			{
				SelectedTargetOperation operation = data.SelectedTargetOperation;
				if (TryCreateNoteDragOperation(obj.PointerEventData, obj.NoteTapPosition, out SelectedTargetOperation finalOperation))
				{
					operation = finalOperation;
				}
				RegisterAndExecuteOperationAction(operation);
			}
			CleanupNoteDragState(data);
			NotifyMusicScoreAndTimelineChanged();
		}

		private bool TryCreateNoteDragOperation(PointerEventData pointerEventData, SelectedTargetOperation.NoteTapPosition noteTapPosition, out SelectedTargetOperation operation)
		{
			operation = null;
			RectTransform rect = _view != null ? _view.NotesViewRectTransform : null;
			if (pointerEventData == null || rect == null)
			{
				return false;
			}
			(int deltaLane, long deltaTicks) = MusicScoreMakerUtility.CreateNotePreviewDragEventWithTicks(
				pointerEventData.pressPosition,
				pointerEventData.position,
				noteTapPosition,
				rect,
				MusicScoreMakerUtility.GetPreviewStartTicks(),
				MusicScoreMakerUtility.GetPreviewEndTicks(),
				pointerEventData);
			if (noteTapPosition == SelectedTargetOperation.NoteTapPosition.center)
			{
				deltaTicks += (_model?.FocusTicks ?? _dragStartFocusTicks) - _dragStartFocusTicks;
			}
			deltaTicks = ClampDeltaTicksForSelectedNotes(deltaTicks);
			deltaTicks = SnapDeltaTicksForSelectedNotes(deltaTicks, noteTapPosition);
			deltaTicks = ClampDeltaTicksForSelectedNotes(deltaTicks);
			operation = MusicScoreMakerUtility.CreateSelectedOperation(
				noteTapPosition,
				ClampDeltaLaneForSelectedNotes(deltaLane, noteTapPosition),
				deltaTicks);
			return true;
		}

		private void CleanupNoteDragState(MusicScoreMakerData data)
		{
			if (data != null)
			{
				data.SelectedTargetOperation = null;
				data.ClearSelectedTemporaryNotes();
			}
			_prevDragDeltaTicks = 0L;
			_prevDragDeltaLane = 0;
			_prevDragQuantizedTicks = long.MinValue;
		}

		private long ClampDeltaTicksForSelectedNotes(long deltaTicks)
		{
			if (deltaTicks == 0L)
			{
				return 0L;
			}
			if (_model?.MusicScoreMakerData == null)
			{
				return 0L;
			}
			if (!TryGetSelectedNoteTicksRange(out long minTicks, out long maxTicks))
			{
				return deltaTicks;
			}
			deltaTicks = Math.Max(deltaTicks, -minTicks);
			return Math.Min(deltaTicks, GetMaxFocusableTicks() - maxTicks);
		}

		private long SnapDeltaTicksForSelectedNotes(long deltaTicks, SelectedTargetOperation.NoteTapPosition noteTapPosition)
		{
			if (noteTapPosition != SelectedTargetOperation.NoteTapPosition.center)
			{
				return deltaTicks;
			}
			long minTicks = GetMinTicksFromSelectedNotes();
			if (minTicks == long.MaxValue)
			{
				return deltaTicks;
			}
			long snappedTicks = MusicScoreMakerUtility.CalculateSnapQuantizedTicks(deltaTicks, minTicks);
			return snappedTicks - minTicks;
		}

		private long GetMinTicksFromSelectedNotes()
		{
			return TryGetSelectedNoteTicksRange(out long minTicks, out _) ? minTicks : long.MaxValue;
		}

		private bool TryGetSelectedNoteTicksRange(out long minTicks, out long maxTicks)
		{
			minTicks = long.MaxValue;
			maxTicks = long.MinValue;
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return false;
			}
			foreach (int id in data.SelectedNoteTargetIdSet)
			{
				MusicScoreNoteBase note = data.FindNote(id);
				if (note != null)
				{
					minTicks = Math.Min(minTicks, note.ticks);
					maxTicks = Math.Max(maxTicks, note.ticks);
				}
			}
			return minTicks != long.MaxValue;
		}

		private void OnMusicScorePreviewPointerDown(OnMusicScorePreviewPointerDownEvent obj)
		{
			CancelScrollInertia();
			_areaSelectDragMode = AreaSelectDragMode.Undecided;
			if (_model?.AreaSelectMode == true)
			{
				ClearTemporaryAreaSelection();
			}
			else
			{
				_isTemporaryAreaSelectionActive = false;
			}
		}

		private void OnMusicScorePreviewClick(OnMusicScorePreviewClickEvent obj)
		{
			if (obj == null || obj.EventData == null)
			{
				return;
			}
			if (_model?.IsEditRestricted == true || _model?.IsEventSettingMode == true)
			{
				return;
			}
			if (_model?.RemoveMode == true)
			{
				RemoveSelectedAndTemporaryNotesAndEventList();
				return;
			}
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			if (data.SelectedNoteIdList.Count > 0 || data.SelectedTemporaryNoteIdList.Count > 0)
			{
				ClearSelectedList();
				NotifyMusicScoreAndTimelineChanged();
				return;
			}
			if (_model?.AreaSelectMode == true)
			{
				ClearSelectedList();
			}
			SelectedToolTypeAction(obj);
		}

		private bool TryGetMusicScoreEventTypeById(int id, out MusicScoreEventType eventType)
		{
			MusicScoreEventData eventData = FindEventById(CurrentData()?.MusicScoreEventDataList, id);
			if (eventData == null)
			{
				eventType = default;
				return false;
			}
			eventType = eventData.eventType;
			return true;
		}

		private Action GenerateSelectCategoryAndAddNoteEventAction(PointerEventData pointerEventData, Sekai.MusicScoreMaker.Ingame.Utilities.MusicScoreMakerUtility.ToolType noteCategory)
		{
			return () =>
			{
				RectTransform rect = _view != null ? _view.NotesViewRectTransform : null;
				if (rect == null)
				{
					return;
				}
				(long ticks, int lane) = MusicScoreMakerUtility.CalcPressTicksAndLane(pointerEventData, rect);
				SelectCategoryAndAddNote(new SelectCategoryAndAddNoteEvent
				{
					Lane = lane,
					Ticks = ticks,
					NoteCategory = noteCategory
				});
			};
		}

		private void OnMusicScorePreviewDrag(OnMusicScorePreviewDragEvent obj)
		{
			if (obj?.EventData == null)
			{
				return;
			}
			_musicScorePreviewDragPointerEventData = obj.EventData;
			if (_model == null)
			{
				return;
			}
			if (_model.IsEventSettingMode || !_model.AreaSelectMode || _model.IsMusicPlaying)
			{
				DragScroll();
				return;
			}
			if (_areaSelectDragMode == AreaSelectDragMode.Undecided)
			{
				Vector2 dragVector = obj.EventData.position - obj.EventData.pressPosition;
				if (dragVector.sqrMagnitude < 225f)
				{
					return;
				}
				float dragAngle = Mathf.Abs(Vector2.Angle(dragVector, Vector2.up));
				if (dragAngle >= 30f && dragAngle <= 150f)
				{
					_areaSelectDragMode = AreaSelectDragMode.AreaSelect;
					MusicScoreMakerData data = CurrentData();
					if (data != null)
					{
						data.ClearSelectedNotes();
						data.ClearSelectedEvents();
					}
				}
				else
				{
					_areaSelectDragMode = AreaSelectDragMode.Scroll;
					ClearTemporaryAreaSelection();
				}
			}
			if (_areaSelectDragMode == AreaSelectDragMode.AreaSelect)
			{
				_scrollVelocityTicksPerSecond = 0f;
				_smoothedScrollVelocity = 0f;
				SelectedTemporaryInArea();
			}
			else if (_areaSelectDragMode == AreaSelectDragMode.Scroll)
			{
				DragScroll();
			}
		}

		private void ClearTemporaryAreaSelection()
		{
			if (!_isTemporaryAreaSelectionActive)
			{
				return;
			}
			MusicScoreMakerData data = CurrentData();
			if (data != null && data.SelectedTemporaryNoteIdList.Count > 0)
			{
				data.ClearSelectedTemporaryNotes();
			}
			_view?.SetActiveSelectArea(false);
			_isTemporaryAreaSelectionActive = false;
		}

		private void OnSetFocusTicksForAreaSelect(SetFocusTicksEvent obj)
		{
			if (_model?.AreaSelectMode == true && _isTemporaryAreaSelectionActive)
			{
				SelectedTemporaryInArea();
			}
		}

		private bool IsPressPositionOutsideNotesViewHorizontal()
		{
			PointerEventData pointerEventData = _musicScorePreviewDragPointerEventData;
			RectTransform rect = _view != null ? _view.NotesViewRectTransform : null;
			if (pointerEventData == null || rect == null)
			{
				return false;
			}
			Vector2 localPoint = MusicScoreMakerUtility.CalcLocalPoint(pointerEventData, pointerEventData.pressPosition, rect);
			Rect localRect = rect.rect;
			return localPoint.x < localRect.xMin || localPoint.x > localRect.xMin + localRect.width;
		}

		private void SelectedTemporaryInArea()
		{
			PointerEventData pointerEventData = _musicScorePreviewDragPointerEventData;
			RectTransform rect = _view != null ? _view.NotesViewRectTransform : null;
			if (_model == null || _model.IsEditRestricted || pointerEventData == null || rect == null || IsPressPositionOutsideNotesViewHorizontal())
			{
				return;
			}
			_view.SetSelectAreaRect(pointerEventData);
			_isTemporaryAreaSelectionActive = true;
			(long startTicks, int startLane, long endTicks, int endLane) = MusicScoreMakerUtility.CalcAreaTicksLane(rect, pointerEventData, false);
			SelectedTemporaryNotesInArea(startTicks, endTicks, startLane, endLane);
			MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateMusicScoreEvent());
		}

		private void SelectedTemporaryNotesInArea(long startTicks, long endTicks, int startLane, int endLane)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			data.ClearSelectedTemporaryNotes();
			bool partialOverlap = MusicScoreMakerSettingsManager.AreaSelectPartialOverlap;
			List<int> selectedNoteIds = new List<int>();
			foreach (MusicScoreNoteBase note in data.NoteList ?? new List<MusicScoreNoteBase>())
			{
				if (note == null || note.ticks < startTicks || note.ticks > endTicks)
				{
					continue;
				}
				bool isSelected = partialOverlap
					? note.laneEnd >= startLane && note.laneStart <= endLane
					: note.laneStart >= startLane && note.laneEnd <= endLane;
				if (isSelected)
				{
					selectedNoteIds.Add(note.id);
				}
			}
			data.AddSelectedTemporaryNoteRange(selectedNoteIds);
		}

		private void SelectedTemporaryEventsInArea(PointerEventData pointerEventData, long startTicks, long endTicks)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			data.ClearSelectedTemporaryEvents();
			foreach (MusicScoreEventData eventData in data.MusicScoreEventDataList ?? new List<MusicScoreEventData>())
			{
				if (eventData != null && eventData.ticks >= startTicks && eventData.ticks <= endTicks)
				{
					data.AddSelectedTemporaryEvent(eventData.id);
				}
			}
		}

		private void DragScroll()
		{
			PointerEventData pointerEventData = _musicScorePreviewDragPointerEventData;
			RectTransform rect = _view != null ? _view.RectTransform : null;
			if (_model == null
				|| _model.IsMusicPlaying
				|| pointerEventData == null
				|| rect == null
				|| rect.rect.height <= 0f
				|| !MusicScoreMakerSettingsManager.EnableSwipeScroll)
			{
				return;
			}
			long previewStartTicks = MusicScoreMakerUtility.GetPreviewStartTicks();
			long previewEndTicks = MusicScoreMakerUtility.GetPreviewEndTicks();
			float deltaTicks = pointerEventData.delta.y / rect.rect.height * (previewEndTicks - previewStartTicks);
			float deltaTime = Time.deltaTime;
			if (deltaTime >= SCROLL_INERTIA_MIN_DELTA_TIME)
			{
				_smoothedScrollVelocity += (deltaTicks / deltaTime - _smoothedScrollVelocity) * SCROLL_VELOCITY_SMOOTHING;
			}
			_accumulatedScrollDeltaTicks += deltaTicks;
			if (Mathf.Abs(_accumulatedScrollDeltaTicks) < 1f)
			{
				return;
			}
			long applyTicks = (long)_accumulatedScrollDeltaTicks;
			_accumulatedScrollDeltaTicks -= applyTicks;
			SetFocusTicks(MusicScoreMakerUtility.GetFocusTicks() - applyTicks);
			NotifyMusicScoreAndTimelineChanged();
		}

		private void TryStartScrollInertiaFromLastDrag()
		{
			if (!MusicScoreMakerSettingsManager.EnableSwipeScroll
				|| _musicScorePreviewDragPointerEventData == null
				|| float.IsNaN(_smoothedScrollVelocity)
				|| float.IsInfinity(_smoothedScrollVelocity))
			{
				return;
			}
			_scrollVelocityTicksPerSecond = Mathf.Clamp(_smoothedScrollVelocity, -SCROLL_INERTIA_MAX_VELOCITY, SCROLL_INERTIA_MAX_VELOCITY);
		}

		private void CancelScrollInertia()
		{
			_isApplyingInertiaScroll = false;
			_scrollVelocityTicksPerSecond = 0f;
			_smoothedScrollVelocity = 0f;
		}

		private void UpdateScrollInertia()
		{
			if (_scrollVelocityTicksPerSecond == 0f)
			{
				return;
			}
			if (_model == null)
			{
				return;
			}
			if (_model.IsMusicPlaying)
			{
				CancelScrollInertia();
				return;
			}

			float deltaTime = Mathf.Max(Time.deltaTime, SCROLL_INERTIA_MIN_DELTA_TIME);
			long deltaTicks = (long)(_scrollVelocityTicksPerSecond * deltaTime);
			_setFocusTicksEventForInertia.Ticks = MusicScoreMakerUtility.GetFocusTicks() - deltaTicks;
			_isApplyingInertiaScroll = true;
			MusicScoreMakerEventDispatcher.Instance.Publish(_setFocusTicksEventForInertia);
			_isApplyingInertiaScroll = false;
			MusicScoreMakerEventDispatcher.Instance.Publish(_updateTimelineSliderEventForInertia);

			_scrollVelocityTicksPerSecond *= Mathf.Pow(SCROLL_INERTIA_DECAY_PER_SECOND, deltaTime);
			if (Mathf.Abs(_scrollVelocityTicksPerSecond) < SCROLL_INERTIA_VELOCITY_EPSILON)
			{
				_scrollVelocityTicksPerSecond = 0f;
			}
		}

		private bool TryFindRayHitLongNote(PointerEventData pointerEventData, out MusicScoreNoteBase longNote)
		{
			longNote = null;
			int noteId = _view != null ? _view.FindRayHitNoteLine(pointerEventData) : int.MinValue;
			if (noteId == int.MinValue)
			{
				return false;
			}

			longNote = MusicScoreMakerUtility.GetMusicScoreMakerData()?.FindNote(noteId);
			return longNote != null;
		}

		private Action GenerateAddNoteListEventAction(PointerEventData pointerEventData, int noteId, NoteCategory noteCategory, NoteLineType noteLineType, bool isSkip)
		{
			return () =>
			{
				RectTransform rect = _view != null ? _view.NotesViewRectTransform : null;
				if (rect == null)
				{
					return;
				}
				(long ticks, int lane) = MusicScoreMakerUtility.CalcPressTicksAndLane(pointerEventData, rect);
				AddNoteList(new AddNoteListEvent
				{
					NoteId = noteId,
					Ticks = ticks,
					Lane = lane,
					NoteCategory = noteCategory,
					NoteLineType = noteLineType,
					IsSkip = isSkip
				});
			};
		}

		private static RectTransform TryGetPointerRectTransform(PointerEventData pointerEventData)
		{
			if (pointerEventData == null)
			{
				return null;
			}
			GameObject target = pointerEventData.pointerCurrentRaycast.gameObject
				?? pointerEventData.pointerEnter
				?? pointerEventData.pointerPress;
			return target == null ? null : target.GetComponent<RectTransform>();
		}

		private void ApplyAreaSelection()
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			data.ClearSelectedNotes();
			data.ClearSelectedEvents();
			data.AddSelectedNoteRange(data.SelectedTemporaryNoteIdList);
			data.AddSelectedEventRange(data.SelectedTemporaryEventIdList);
			data.ClearSelectedTemporaryNotes();
			data.ClearSelectedTemporaryEvents();
			_view?.SetActiveSelectArea(false);
			_isTemporaryAreaSelectionActive = false;
			NotifyMusicScoreAndTimelineChanged();
		}

		private void OnMusicScorePreviewPointerUp(OnMusicScorePreviewPointerUpEvent obj)
		{
			if (_model == null || obj == null)
			{
				return;
			}
			if (_model.IsEditRestricted)
			{
				if (obj.IsDragging)
				{
					TryStartScrollInertiaFromLastDrag();
				}
				else
				{
					CancelScrollInertia();
				}
				_musicScorePreviewDragPointerEventData = null;
				_areaSelectDragMode = AreaSelectDragMode.Undecided;
				return;
			}
			if (obj.IsLongPress || !obj.IsDragging)
			{
				CancelScrollInertia();
			}
			else if (!_model.AreaSelectMode || _model.IsEventSettingMode || _model.IsMusicPlaying || _areaSelectDragMode == AreaSelectDragMode.Scroll)
			{
				TryStartScrollInertiaFromLastDrag();
			}
			else if (_areaSelectDragMode == AreaSelectDragMode.AreaSelect)
			{
				if (_model.RemoveMode)
				{
					RemoveNotesInSelectedArea(obj);
				}
				else
				{
					ApplyAreaSelection();
				}
			}
			_musicScorePreviewDragPointerEventData = null;
			_areaSelectDragMode = AreaSelectDragMode.Undecided;
		}

		private void RemoveNotesInSelectedArea(OnMusicScorePreviewPointerUpEvent onMusicScorePreviewPointerUpEvent)
		{
			if (_model?.IsEditRestricted == true)
			{
				return;
			}
			_view?.SetActiveSelectArea(false);
			_isTemporaryAreaSelectionActive = false;
			RemoveSelectedAndTemporaryNotesAndEventList();
		}

		private void RemoveSelectedAndTemporaryNotesAndEventList(int deleteNoteId = -1)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			HashSet<int> selectedNoteIds = new HashSet<int>(data.SelectedNoteTargetIdSet);
			if (deleteNoteId >= 0)
			{
				selectedNoteIds.Add(deleteNoteId);
			}
			List<MusicScoreNoteBase> selectedNotes = new List<MusicScoreNoteBase>();
			if (data.NoteList != null)
			{
				foreach (MusicScoreNoteBase note in data.NoteList)
				{
					if (note != null && selectedNoteIds.Contains(note.id))
					{
						selectedNotes.Add(note);
					}
				}
			}
			List<MusicScoreNoteBase> notes = BuildRemoveNoteListFromSelectedNotes(selectedNotes);
			List<MusicScoreEventData> events = new List<MusicScoreEventData>();
			foreach (int id in data.SelectedEventTargetIdSet)
			{
				MusicScoreEventData eventData = FindEventById(data.MusicScoreEventDataList, id);
				if (eventData != null)
				{
					events.Add(eventData);
				}
			}
			if (notes.Count == 0 && events.Count == 0)
			{
				return;
			}
			List<MusicScoreNoteBase> noteCopies = CloneNotesForUndo(notes);
			List<MusicScoreEventData> eventCopies = CloneEventsForUndo(events, preserveId: true);
			List<int> selectedNoteIdsCopy = CopyList(data.SelectedNoteIdList);
			List<int> selectedTemporaryNoteIdsCopy = CopyList(data.SelectedTemporaryNoteIdList);
			List<int> selectedEventIdsCopy = CopyList(data.SelectedEventIdList);
			List<int> selectedTemporaryEventIdsCopy = CopyList(data.SelectedTemporaryEventIdList);
			Action redo = () =>
			{
				RemoveNoteList(notes);
				data.RemoveEventRange(events);
				ClearSelectedList();
				MarkEditedAndRefresh(notes);
			};
			Action undo = () =>
			{
				data.ClearSelectedNotes();
				data.AddSelectedNoteRange(selectedNoteIdsCopy);
				data.ClearSelectedTemporaryNotes();
				data.AddSelectedTemporaryNoteRange(selectedTemporaryNoteIdsCopy);
				RestoreRemovedNoteList(noteCopies);
				data.ClearSelectedEvents();
				data.AddSelectedEventRange(selectedEventIdsCopy);
				data.ClearSelectedTemporaryEvents();
				data.AddSelectedTemporaryEventRange(selectedTemporaryEventIdsCopy);
				data.AddEventRange(eventCopies);
				MarkEditedAndRefresh(noteCopies);
			};
			PushUndoableAction(undo, redo, _model?.FocusTicks ?? -1L, _model?.FocusTicks ?? -1L);
		}

		private void CopySelectedNotesAndEvents(CopySelectedNotesAndEventsEvent evt)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || _model?.IsEditRestricted == true)
			{
				return;
			}
			data.CopiedNoteList ??= new List<MusicScoreNoteBase>();
			data.CopiedEventDataList ??= new List<MusicScoreEventData>();
			data.CopiedNoteList.Clear();
			data.CopiedEventDataList.Clear();
			List<int> selectedNoteIds = data.SelectedNoteIdList;
			foreach (int id in selectedNoteIds)
			{
				MusicScoreNoteBase note = data.FindNote(id);
				if (note != null && CanCopyConnectedNote(note, selectedNoteIds))
				{
					data.CopiedNoteList.Add(note.Clone());
				}
			}
			Dictionary<int, MusicScoreEventData> eventCache = data.GetEventIdCacheOrRebuild();
			foreach (int id in data.SelectedEventIdList)
			{
				if (eventCache.TryGetValue(id, out MusicScoreEventData eventData) && eventData != null)
				{
					data.CopiedEventDataList.Add(eventData.Clone());
				}
			}
			if (data.CopiedNoteList.Count > 0 || data.CopiedEventDataList.Count > 0)
			{
				ClipboardCacheManager.Instance.AddCache(new ClipboardCacheData(data.CopiedNoteList, data.CopiedEventDataList));
				if (MusicScoreMakerEventDispatcher.ExistsInstance)
				{
					MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateClipboardButtonEvent());
				}
				MusicScoreMakerRuleSlideTutorialUtility.TryShowTutorialSlideIfFirstTime("copy");
			}
		}

		private static bool CanCopyConnectedNote(MusicScoreNoteBase note, List<int> selectedNoteIds)
		{
			if (note == null || selectedNoteIds == null)
			{
				return false;
			}
			bool previousSelected = note.previousConnectionId == -1 || selectedNoteIds.Contains(note.previousConnectionId);
			bool nextSelected = note.nextConnectionId == -1 || selectedNoteIds.Contains(note.nextConnectionId);
			return previousSelected && nextSelected;
		}

		private void ShowClipboardCacheList(ShowClipboardCacheListEvent evt)
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				_scrollVelocityTicksPerSecond = 0f;
				_smoothedScrollVelocity = 0f;
				long snappedFocusTicks = MusicScoreMakerUtility.CalculateSnapQuantizedTicks(0L, MusicScoreMakerUtility.GetFocusTicks());
				MusicScoreMakerUtility.SetFocusTicks(snappedFocusTicks);
				MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
				dispatcher.Publish(new ShowNoteChangeButtonsEvent());
				dispatcher.Publish(new ShowClipboardCacheSubWindowEvent());
			}
		}

		private void PasteFromClipboardCache(PasteFromClipboardCacheEvent evt)
		{
			if (_model?.IsEditRestricted == true)
			{
				return;
			}
			ClipboardCacheData cache = evt == null ? null : ClipboardCacheManager.Instance.GetCache(evt.CacheId);
			if (cache == null)
			{
				UnityEngine.Debug.LogError($"Clipboard cache not found: {evt?.CacheId}");
				return;
			}
			PasteNotesAndEvents(cache.CopiedNoteList, cache.CopiedEventDataList, evt.IsFlipHorizontal);
		}

		private void PasteNotesAndEvents(List<MusicScoreNoteBase> sourceNotes, List<MusicScoreEventData> sourceEvents, bool isFlipHorizontal)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || _model?.IsEditRestricted == true)
			{
				return;
			}
			if ((sourceNotes == null || sourceNotes.Count == 0) && (sourceEvents == null || sourceEvents.Count == 0))
			{
				return;
			}

			long minTicks = MusicScoreMakerUtility.FindMinTick(sourceNotes, sourceEvents);
			long maxTicks = MusicScoreMakerUtility.FindMaxTick(sourceNotes, sourceEvents);
			long spanTicks = Math.Max(0L, maxTicks - minTicks);
			long focusTicks = _model?.FocusTicks ?? 0L;
			long maxFocusableTicks = GetMaxFocusableTicks();
			if (maxFocusableTicks - focusTicks < spanTicks)
			{
				focusTicks = Math.Min(Math.Max(maxFocusableTicks - spanTicks, 0L), maxFocusableTicks);
			}

			_pasteNoteListCache.Clear();
			_pasteIdMappingCache.Clear();
			_pasteEventListCache.Clear();
			if (sourceNotes != null)
			{
				foreach (MusicScoreNoteBase source in sourceNotes)
				{
					if (source == null)
					{
						continue;
					}
					MusicScoreNoteBase clone = source.Clone();
					int oldId = clone.id;
					clone.id = data.GetNewId();
					_pasteIdMappingCache[oldId] = clone.id;
					clone.ticks = ClampTicksToValidRange(source.ticks - minTicks + focusTicks);
					if (isFlipHorizontal)
					{
						FlipNoteLanePosition(clone);
						FlipNoteDirection(clone);
					}
					_pasteNoteListCache.Add(clone);
				}
				foreach (MusicScoreNoteBase note in _pasteNoteListCache)
				{
					note.previousConnectionId = note.previousConnectionId != -1 && _pasteIdMappingCache.TryGetValue(note.previousConnectionId, out int prevId) ? prevId : -1;
					note.nextConnectionId = note.nextConnectionId != -1 && _pasteIdMappingCache.TryGetValue(note.nextConnectionId, out int nextId) ? nextId : -1;
				}
				NormalizePastedConnectedNoteTicks();
			}
			if (sourceEvents != null)
			{
				foreach (MusicScoreEventData source in sourceEvents)
				{
					if (source == null)
					{
						continue;
					}
					MusicScoreEventData clone = source.Clone();
					if (clone == null)
					{
						continue;
					}
					clone.id = data.GetNewId();
					clone.ticks = ClampTicksToValidRange(source.ticks - minTicks + focusTicks);
					_pasteEventListCache.Add(clone);
				}
			}
			if (_pasteNoteListCache.Count == 0 && _pasteEventListCache.Count == 0)
			{
				return;
			}
			MusicScoreNoteBase[] pastedNotes = _pasteNoteListCache.ToArray();
			MusicScoreEventData[] pastedEvents = _pasteEventListCache.ToArray();
			int[] pastedNoteIds = new int[pastedNotes.Length];
			for (int i = 0; i < pastedNotes.Length; i++)
			{
				pastedNoteIds[i] = pastedNotes[i].id;
			}
			int[] pastedEventIds = new int[pastedEvents.Length];
			for (int i = 0; i < pastedEvents.Length; i++)
			{
				pastedEventIds[i] = pastedEvents[i].id;
			}
			long minPastedTicks = FindMinPastedTicks(pastedNotes, pastedEvents);
			Action redo = () =>
			{
				data.AddNoteRange(pastedNotes);
				data.AddEventRange(pastedEvents);
				data.UpdateConnectionNotes();
				AddEditedTicksAndRecheckForPastedNotes(pastedNotes);
				data.ClearSelectedNotes();
				data.ClearSelectedTemporaryNotes();
				data.ClearSelectedEvents();
				data.ClearSelectedTemporaryEvents();
				if (pastedNoteIds.Length > 0)
				{
					data.AddSelectedNoteRange(pastedNoteIds);
				}
				if (pastedEventIds.Length > 0)
				{
					data.AddSelectedEventRange(pastedEventIds);
				}
				data.SelectedTargetOperation = null;
				if (MusicScoreMakerEventDispatcher.ExistsInstance)
				{
					MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateMusicScoreEvent());
				}
			};
			Action undo = () =>
			{
				data.RemoveNoteRange(pastedNotes);
				data.RemoveEventRange(pastedEvents);
				AddEditedTicksAndRecheckForPastedNotes(pastedNotes);
				data.ClearSelectedNotes();
				data.ClearSelectedTemporaryNotes();
				data.ClearSelectedEvents();
				data.ClearSelectedTemporaryEvents();
				data.SelectedTargetOperation = null;
				if (MusicScoreMakerEventDispatcher.ExistsInstance)
				{
					MusicScoreMakerEventDispatcher.Instance.Publish(new RefreshMusicScoreEvent());
				}
			};
			PushUndoableAction(undo, redo, minPastedTicks, minPastedTicks);
		}

		private void NormalizePastedConnectedNoteTicks()
		{
			if (_pasteNoteListCache.Count == 0)
			{
				return;
			}
			Dictionary<int, MusicScoreNoteBase> pastedNoteCache = new Dictionary<int, MusicScoreNoteBase>(_pasteNoteListCache.Count);
			foreach (MusicScoreNoteBase note in _pasteNoteListCache)
			{
				if (note != null)
				{
					pastedNoteCache[note.id] = note;
				}
			}
			HashSet<int> visitedIds = new HashSet<int>();
			List<MusicScoreNoteBase> connectedNotes = new List<MusicScoreNoteBase>();
			foreach (MusicScoreNoteBase note in _pasteNoteListCache)
			{
				if (note == null || visitedIds.Contains(note.id) || !note.IsConnectedFirst)
				{
					continue;
				}
				note.FindConnectedNotes(pastedNoteCache, connectedNotes);
				foreach (MusicScoreNoteBase connectedNote in connectedNotes)
				{
					if (connectedNote != null)
					{
						visitedIds.Add(connectedNote.id);
					}
				}
				if (connectedNotes.Count < 2)
				{
					continue;
				}
				connectedNotes.Sort(new NoteTicksComparer());
				for (int i = 1; i < connectedNotes.Count; i++)
				{
					if (connectedNotes[i] != null && connectedNotes[i - 1] != null && connectedNotes[i].ticks <= connectedNotes[i - 1].ticks)
					{
						connectedNotes[i].ticks = ClampTicksToValidRange(connectedNotes[i - 1].ticks + 1L);
					}
				}
			}
		}

		private void AddEditedTicksAndRecheckForPastedNotes(IEnumerable<MusicScoreNoteBase> notes)
		{
			_pasteEditedTicksCache.Clear();
			if (notes != null)
			{
				foreach (MusicScoreNoteBase note in notes)
				{
					if (note != null)
					{
						_pasteEditedTicksCache.Add(note.ticks);
					}
				}
			}
			if (_pasteEditedTicksCache.Count == 0)
			{
				return;
			}
			MusicScoreMakerUtility.AddEditedTicks(_pasteEditedTicksCache);
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new RecheckJudgmentNoteGapEvent());
			}
		}

		private static long FindMinPastedTicks(MusicScoreNoteBase[] notes, MusicScoreEventData[] events)
		{
			long minTicks = long.MaxValue;
			if (notes != null)
			{
				foreach (MusicScoreNoteBase note in notes)
				{
					if (note != null && note.ticks < minTicks)
					{
						minTicks = note.ticks;
					}
				}
			}
			if (events != null)
			{
				foreach (MusicScoreEventData eventData in events)
				{
					if (eventData != null && eventData.ticks < minTicks)
					{
						minTicks = eventData.ticks;
					}
				}
			}
			return minTicks == long.MaxValue ? -1L : minTicks;
		}

		private void OnExpandInputDrag(OnExpandInputDragEvent obj)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || obj?.PointerEventData == null)
			{
				return;
			}
			CancelScrollInertia();
			if (_model?.IsEditRestricted == true)
			{
				return;
			}
			if (!TryCreateExpandOperation(obj.PointerEventData, obj.PressPosition, obj.NoteTapPosition, out SelectedTargetOperation operation))
			{
				return;
			}
			PlayExpandInputDragSeIfNeeded(obj.NoteTapPosition, operation.deltaLane);
			SetExpandOperation(data, obj.NoteTapPosition, operation);
			UpdateSelectedTargetOperationFromExpandOperations();
			NotifyMusicScoreAndTimelineChanged();
		}

		private void UpdateSelectedTargetOperationFromExpandOperations()
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			data.SelectedTargetOperation = data.LeftExpandOperation ?? data.RightExpandOperation;
		}

		private void OnExpandInputPointerUp(OnExpandInputPointerUpEvent obj)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || obj == null)
			{
				return;
			}
			if (_model?.IsEditRestricted == true)
			{
				ClearExpandOperation(data, obj.NoteTapPosition);
				UpdateSelectedTargetOperationFromExpandOperations();
				return;
			}

			SelectedTargetOperation finalOperation = null;
			if (obj.IsDragging && !obj.IsLongPress)
			{
				TryCreateExpandOperation(obj.PointerEventData, obj.PressPosition, obj.NoteTapPosition, out finalOperation);
			}
			ClearExpandOperation(data, obj.NoteTapPosition);
			UpdateSelectedTargetOperationFromExpandOperations();
			if (finalOperation != null)
			{
				RegisterAndExecuteOperationAction(finalOperation);
			}
			data.SelectedTargetOperation = null;
			NotifyMusicScoreAndTimelineChanged();
		}

		private bool TryCreateExpandOperation(PointerEventData pointerEventData, Vector2 pressPosition, SelectedTargetOperation.NoteTapPosition noteTapPosition, out SelectedTargetOperation operation)
		{
			operation = null;
			RectTransform rect = _view != null ? _view.NotesViewRectTransform : null;
			if (pointerEventData == null || rect == null)
			{
				return false;
			}
			(int deltaLane, long deltaTicks) = MusicScoreMakerUtility.CreateNotePreviewDragEventWithTicks(
				pressPosition,
				pointerEventData.position,
				noteTapPosition,
				rect,
				MusicScoreMakerUtility.GetPreviewStartTicks(),
				MusicScoreMakerUtility.GetPreviewEndTicks(),
				pointerEventData);
			int clampedDeltaLane = ClampDeltaLaneForSelectedNotes(deltaLane, noteTapPosition);
			operation = MusicScoreMakerUtility.CreateSelectedOperation(noteTapPosition, clampedDeltaLane, deltaTicks);
			return true;
		}

		private void SetExpandOperation(MusicScoreMakerData data, SelectedTargetOperation.NoteTapPosition noteTapPosition, SelectedTargetOperation operation)
		{
			if (data == null)
			{
				return;
			}
			if (noteTapPosition == SelectedTargetOperation.NoteTapPosition.left)
			{
				data.LeftExpandOperation = operation;
				_prevExpandLeftDeltaLane = operation?.deltaLane ?? 0;
			}
			else if (noteTapPosition == SelectedTargetOperation.NoteTapPosition.right)
			{
				data.RightExpandOperation = operation;
				_prevExpandRightDeltaLane = operation?.deltaLane ?? 0;
			}
		}

		private void ClearExpandOperation(MusicScoreMakerData data, SelectedTargetOperation.NoteTapPosition noteTapPosition)
		{
			if (data == null)
			{
				return;
			}
			if (noteTapPosition == SelectedTargetOperation.NoteTapPosition.left)
			{
				data.LeftExpandOperation = null;
				_prevExpandLeftDeltaLane = 0;
			}
			else if (noteTapPosition == SelectedTargetOperation.NoteTapPosition.right)
			{
				data.RightExpandOperation = null;
				_prevExpandRightDeltaLane = 0;
			}
		}

		private void SelectAllConnectedNotes(SelectAllConnectedNotesEvent obj)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			Dictionary<int, MusicScoreNoteBase> cache = data.GetNoteIdCacheOrRebuild();
			List<MusicScoreNoteBase> buffer = new List<MusicScoreNoteBase>();
			List<int> selected = CopyList(data.SelectedNoteIdList);
			foreach (int id in selected)
			{
				MusicScoreNoteBase note = data.FindNote(id);
				if (note == null)
				{
					continue;
				}
				note.FindConnectedNotes(cache, buffer, true);
				foreach (MusicScoreNoteBase connected in buffer)
				{
					if (connected != null && !data.SelectedNoteIdList.Contains(connected.id))
					{
						data.AddSelectedNote(connected.id);
					}
				}
			}
			NotifyMusicScoreAndTimelineChanged();
		}

		private int ClampDeltaLaneForSelectedNotes(int deltaLane, SelectedTargetOperation.NoteTapPosition noteTapPosition)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return deltaLane;
			}
			int minAllowed = int.MinValue;
			int maxAllowed = int.MaxValue;
			foreach (int id in data.SelectedNoteTargetIdSet)
			{
				MusicScoreNoteBase note = data.FindNote(id);
				if (note == null || note.isSkip)
				{
					continue;
				}
				switch (noteTapPosition)
				{
				case SelectedTargetOperation.NoteTapPosition.left:
					minAllowed = Math.Max(minAllowed, -note.laneStart);
					maxAllowed = Math.Min(maxAllowed, note.laneEnd - note.laneStart);
					break;
				case SelectedTargetOperation.NoteTapPosition.right:
					minAllowed = Math.Max(minAllowed, note.laneStart - note.laneEnd);
					maxAllowed = Math.Min(maxAllowed, MusicScoreMakerModel.LaneCountMinus1 - note.laneEnd);
					break;
				default:
					minAllowed = Math.Max(minAllowed, -note.laneStart);
					maxAllowed = Math.Min(maxAllowed, MusicScoreMakerModel.LaneCountMinus1 - note.laneEnd);
					break;
				}
			}
			if (minAllowed == int.MinValue || maxAllowed == int.MaxValue)
			{
				return deltaLane;
			}
			return Math.Min(Math.Max(deltaLane, minAllowed), maxAllowed);
		}

		private void InvalidateMaxFocusableTicksCache()
		{
			_maxFocusableTicksCacheValid = false;
		}

		private void SetupTimelineEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<GetSelectedMusicScoreBarIndexEvent, float>(GetSelectedMusicScoreBarIndex);
			dispatcher.Register<SetSelectedMusicScoreBarIndexEvent>(SetSelectedMusicScoreBarIndex);
			dispatcher.Register<GetCurrentMusicScoreScaleEvent, float>(GetCurrentMusicScoreScale);
			dispatcher.Register<GetCurrentMusicScoreStartTicksEvent, long>(GetCurrentMusicScoreStartTicks);
			dispatcher.Register<SetFocusTicksEvent>(SetFocusTicks);
			dispatcher.Register<GetFocusTicksEvent, long>(GetFocusTicks);
			dispatcher.Register<GetQuantizeTicksEvent, long>(GetQuantizeTicks);
			dispatcher.Register<GetQuantizeDivisionEvent, int>(GetQuantizeDivision);
			dispatcher.Register<SetQuantizeDivisionEvent>(SetQuantizeDivision);
			dispatcher.Register<GetQuantizeTypeEvent, QuantizeSettings.QuantizeType>(GetQuantizeType);
			dispatcher.Register<SetQuantizeTypeEvent>(SetQuantizeType);
			dispatcher.Register<GetQuantizeStrengthEvent, float>(GetQuantizeStrength);
			dispatcher.Register<SetQuantizeStrengthEvent>(SetQuantizeStrength);
			dispatcher.Register<ZoomInTimelineEvent>(ZoomInTimeline);
			dispatcher.Register<ZoomOutTimelineEvent>(ZoomOutTimeline);
			dispatcher.Register<UpdateBarLinePreviewEvent>(UpdateBarLinePreview);
			dispatcher.Register<GetMusicScoreTicksMaxEvent, long>(GetMusicScoreTicksMax);
			dispatcher.Register<GetTimelineFocusDiffEvent, long>(GetTimelineFocusDiff);
			dispatcher.Register<InvalidateMaxFocusableTicksCacheEvent>(OnInvalidateMaxTicksCache);
			dispatcher.Register<AddMusicScoreTimeSliderValue>(AddSliderValue);
			dispatcher.Register<SubtractMusicScoreTimeSliderValue>(SubtractSliderValue);
			dispatcher.Register<SetZoomTimelineScaleEvent>(SetZoomTimelineScale);
		}

		private void DisposeTimelineEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<GetSelectedMusicScoreBarIndexEvent, float>(GetSelectedMusicScoreBarIndex);
			dispatcher.Remove<SetSelectedMusicScoreBarIndexEvent>(SetSelectedMusicScoreBarIndex);
			dispatcher.Remove<GetCurrentMusicScoreScaleEvent, float>(GetCurrentMusicScoreScale);
			dispatcher.Remove<GetCurrentMusicScoreStartTicksEvent, long>(GetCurrentMusicScoreStartTicks);
			dispatcher.Remove<SetFocusTicksEvent>(SetFocusTicks);
			dispatcher.Remove<GetFocusTicksEvent, long>(GetFocusTicks);
			dispatcher.Remove<GetQuantizeTicksEvent, long>(GetQuantizeTicks);
			dispatcher.Remove<GetQuantizeDivisionEvent, int>(GetQuantizeDivision);
			dispatcher.Remove<SetQuantizeDivisionEvent>(SetQuantizeDivision);
			dispatcher.Remove<GetQuantizeTypeEvent, QuantizeSettings.QuantizeType>(GetQuantizeType);
			dispatcher.Remove<SetQuantizeTypeEvent>(SetQuantizeType);
			dispatcher.Remove<GetQuantizeStrengthEvent, float>(GetQuantizeStrength);
			dispatcher.Remove<SetQuantizeStrengthEvent>(SetQuantizeStrength);
			dispatcher.Remove<ZoomInTimelineEvent>(ZoomInTimeline);
			dispatcher.Remove<ZoomOutTimelineEvent>(ZoomOutTimeline);
			dispatcher.Remove<UpdateBarLinePreviewEvent>(UpdateBarLinePreview);
			dispatcher.Remove<GetMusicScoreTicksMaxEvent, long>(GetMusicScoreTicksMax);
			dispatcher.Remove<GetTimelineFocusDiffEvent, long>(GetTimelineFocusDiff);
			dispatcher.Remove<InvalidateMaxFocusableTicksCacheEvent>(OnInvalidateMaxTicksCache);
			dispatcher.Remove<AddMusicScoreTimeSliderValue>(AddSliderValue);
			dispatcher.Remove<SubtractMusicScoreTimeSliderValue>(SubtractSliderValue);
			dispatcher.Remove<SetZoomTimelineScaleEvent>(SetZoomTimelineScale);
		}

		private void ZoomInTimeline(ZoomInTimelineEvent evt)
		{
			if (_model == null)
			{
				return;
			}
			CancelScrollInertia();
			float step = MusicScoreMakerSettingsManager.ZoomTimelineStep;
			_model.CurrentMusicScoreScale = Mathf.Min(MusicScoreMakerSettingsManager.ZoomTimelineScaleMax, _model.CurrentMusicScoreScale < step ? step : _model.CurrentMusicScoreScale + step);
			PublishZoomScaleChanged();
			NotifyMusicScoreAndTimelineChanged();
		}

		private void ZoomOutTimeline(ZoomOutTimelineEvent evt)
		{
			if (_model == null)
			{
				return;
			}
			CancelScrollInertia();
			float step = MusicScoreMakerSettingsManager.ZoomTimelineStep;
			_model.CurrentMusicScoreScale = Mathf.Max(MusicScoreMakerSettingsManager.ZoomTimelineScaleMin, _model.CurrentMusicScoreScale - step);
			PublishZoomScaleChanged();
			NotifyMusicScoreAndTimelineChanged();
		}

		private void SetZoomTimelineScale(SetZoomTimelineScaleEvent evt)
		{
			if (_model == null || evt == null)
			{
				return;
			}
			CancelScrollInertia();
			_model.CurrentMusicScoreScale = Mathf.Clamp(evt.Scale, MusicScoreMakerSettingsManager.ZoomTimelineScaleMin, MusicScoreMakerSettingsManager.ZoomTimelineScaleMax);
			PublishZoomScaleChanged();
			NotifyMusicScoreAndTimelineChanged();
		}

		private float GetSelectedMusicScoreBarIndex(GetSelectedMusicScoreBarIndexEvent arg)
		{
			return _model?.SelectedMusicScoreBarIndex ?? 0f;
		}

		private void SetSelectedMusicScoreBarIndex(SetSelectedMusicScoreBarIndexEvent obj)
		{
			if (_model != null && obj != null)
			{
				_model.SelectedMusicScoreBarIndex = obj.SelectedBarIndex;
			}
		}

		private float GetCurrentMusicScoreScale(GetCurrentMusicScoreScaleEvent arg)
		{
			return _model?.CurrentMusicScoreScale ?? 1f;
		}

		private void SetFocusTicks(SetFocusTicksEvent obj)
		{
			if (!_isApplyingInertiaScroll)
			{
				CancelScrollInertia();
			}
			if (obj != null)
			{
				SetFocusTicks(obj.Ticks);
				NotifyMusicScoreAndTimelineChanged();
			}
		}

		private void SetFocusTicks(long value)
		{
			if (_model == null)
			{
				return;
			}
			_model.FocusTicks = ClampFocusTicks(value);
			UpdateMusicTimeTextEventFromFocusTicks();
		}

		private long ClampFocusTicks(long value)
		{
			return ClampTicksToValidRange(value);
		}

		private long ClampTicksToValidRange(long value)
		{
			if (value < 0L)
			{
				return 0L;
			}
			return Math.Min(value, Math.Max(GetMaxFocusableTicks(), 0L));
		}

		private long GetMaxFocusableTicks()
		{
			if (_model == null || _model.MusicScoreMakerData == null)
			{
				return 0L;
			}
			if (_model.MasterMusicSec <= 0)
			{
				return _model.MusicScoreTicksMax;
			}
			List<MusicScoreEventData> eventList = _model.MusicScoreMakerData.MusicScoreEventDataList;
			if (eventList == null)
			{
				return 0L;
			}
			int eventHash = CalculateBpmAndTimeSignatureEventHash(eventList);
			if (ShouldUseMaxFocusableTicksCache(eventHash))
			{
				return _maxFocusableTicksCache;
			}
			long ticks = ComputeMaxTicksForEventList(eventList);
			_maxFocusableTicksCacheValid = true;
			_maxFocusableTicksEventHashCache = eventHash;
			_maxFocusableTicksMasterMusicSecCache = _model.MasterMusicSec;
			_maxFocusableTicksFillerSecCache = _model.FillerSec;
			_maxFocusableTicksCache = ticks;
			return ticks;
		}

		private long ComputeMaxTicksForEventList(List<MusicScoreEventData> eventList)
		{
			return eventList == null ? 0L : MusicScoreMakerUtility.GetTicksFromTime(_model?.MasterMusicSec ?? 0, eventList);
		}

		private static int CalculateBpmAndTimeSignatureEventHash(List<MusicScoreEventData> eventList)
		{
			if (eventList == null)
			{
				return 0;
			}
			int hash = 17;
			foreach (MusicScoreEventData eventData in eventList)
			{
				if (eventData == null || (eventData.eventType != MusicScoreEventType.BPM && eventData.eventType != MusicScoreEventType.TimeSignature))
				{
					continue;
				}
				unchecked
				{
					hash = hash * 31 + eventData.eventType.GetHashCode();
					hash = hash * 31 + eventData.ticks.GetHashCode();
					hash = hash * 31 + (eventData.changeValue?.GetHashCode() ?? 0);
				}
			}
			return hash;
		}

		private bool ShouldUseMaxFocusableTicksCache(int eventHash)
		{
			return _maxFocusableTicksCacheValid
				&& _maxFocusableTicksEventHashCache == eventHash
				&& _maxFocusableTicksMasterMusicSecCache == (_model?.MasterMusicSec ?? 0)
				&& Mathf.Approximately(_maxFocusableTicksFillerSecCache, _model?.FillerSec ?? 0f);
		}

		private long GetFocusTicks(GetFocusTicksEvent arg)
		{
			return _model?.FocusTicks ?? 0L;
		}

		private long GetCurrentMusicScoreStartTicks(GetCurrentMusicScoreStartTicksEvent arg)
		{
			if (_model == null)
			{
				return 0L;
			}
			long showTicksRange = (long)Math.Round(_model.CurrentMusicScoreScale * MusicScoreMakerUtility.TICKS_PER_BAR);
			long focusOffset = (long)Math.Round(showTicksRange * MusicScoreMakerSettingsManager.ShowFocusTicksRate);
			return _model.FocusTicks - focusOffset;
		}

		private long GetMusicScoreTicksMax(GetMusicScoreTicksMaxEvent arg)
		{
			return GetMaxFocusableTicks();
		}

		private void OnInvalidateMaxTicksCache(InvalidateMaxFocusableTicksCacheEvent arg)
		{
			InvalidateMaxFocusableTicksCache();
		}

		private void UpdateBarLinePreview(UpdateBarLinePreviewEvent arg)
		{
		}

		private long GetQuantizeTicks(GetQuantizeTicksEvent arg)
		{
			return _model?.QuantizeTicks ?? 0L;
		}

		private int GetQuantizeDivision(GetQuantizeDivisionEvent arg)
		{
			return _model?.QuantizeDivision ?? 16;
		}

		private void SetQuantizeDivision(SetQuantizeDivisionEvent obj)
		{
			if (_model != null && obj != null)
			{
				_model.SetQuantizeDivision(obj.Division);
				MusicScoreMakerEventDispatcher.Instance.Publish(new UpdateMusicScoreEvent());
			}
		}

		private QuantizeSettings.QuantizeType GetQuantizeType(GetQuantizeTypeEvent arg)
		{
			return _model?.QuantizeType ?? QuantizeSettings.QuantizeType.None;
		}

		private void SetQuantizeType(SetQuantizeTypeEvent obj)
		{
			if (_model != null && obj != null)
			{
				_model.QuantizeType = obj.QuantizeType;
			}
		}

		private float GetQuantizeStrength(GetQuantizeStrengthEvent arg)
		{
			return _model?.QuantizeStrength ?? 1f;
		}

		private void SetQuantizeStrength(SetQuantizeStrengthEvent obj)
		{
			if (_model != null && obj != null)
			{
				_model.QuantizeStrength = obj.Strength;
			}
		}

		private long GetTimelineFocusDiff(GetTimelineFocusDiffEvent arg)
		{
			return Math.Max(1L, _model?.QuantizeTicks ?? MusicScoreMakerUtility.TICKS_PER_BEAT);
		}

		private void UpdateInputStartTicks()
		{
			if (_model == null)
			{
				return;
			}
			if (_model.InputStartTicks == MusicScoreMakerModel.InputStartTicksDefault)
			{
				SetInputStartTicks();
			}
		}

		private void SetInputStartTicks()
		{
			if (_model != null)
			{
				_model.InputStartTicks = _model.FocusTicks;
			}
		}

		private void ClearInputStartTicks()
		{
			if (_model != null)
			{
				_model.InputStartTicks = MusicScoreMakerModel.InputStartTicksDefault;
			}
		}

		private void AddSliderValue(AddMusicScoreTimeSliderValue value)
		{
			MoveSliderValueByQuantizeUnit(isAdd: true);
		}

		private void SubtractSliderValue(SubtractMusicScoreTimeSliderValue value)
		{
			MoveSliderValueByQuantizeUnit(isAdd: false);
		}

		private void MoveSliderValueByQuantizeUnit(bool isAdd)
		{
			long delta = Math.Max(1L, _model?.QuantizeTicks ?? MusicScoreMakerUtility.TICKS_PER_BEAT);
			SetFocusTicks((_model?.FocusTicks ?? 0L) + (isAdd ? delta : -delta));
			NotifyMusicScoreAndTimelineChanged();
		}

		private static long GetQuantizeTicks()
		{
			return MusicScoreMakerUtility.GetQuantizeTicks();
		}

		private void SetupToolEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<GetMusicScoreMakerDataEvent, MusicScoreMakerData>(GetMusicScoreMakerData);
			dispatcher.Register<OnToolIconClickEvent>(OnToolIconClick);
			dispatcher.Register<IsAreaSelectModeEvent, bool>(IsAreaSelectMode);
			dispatcher.Register<IsRemoveModeEvent, bool>(IsRemoveMode);
			dispatcher.Register<OnTestPlayEvent>(OnTestPlay);
			dispatcher.Register<OnMusicSelectorEvent>(OnMusicSelector);
			dispatcher.Register<OpenMusicScoreSaveDataManagerDialogEvent>(OpenMusicScoreSaveDataManagerDialog);
			dispatcher.Register<AutoSaveMusicScoreEvent>(AutoSaveMusicScore);
			dispatcher.Register<SaveMusicScoreEvent>(SaveMusicScore);
			dispatcher.Register<QuickSaveMusicScoreEvent>(QuickSaveMusicScore);
			dispatcher.Register<SaveDraftScreenMusicScoreEvent>(SaveDraftScreenMusicScore);
			dispatcher.Register<OpenLoadScreenEvent>(OpenLoadScreen);
			dispatcher.Register<LoadMusicScoreEvent>(LoadMusicScore);
			dispatcher.Register<DeleteMusicScoreEvent>(DeleteMusicScore);
			dispatcher.Register<LoadMusicScoreNamesEvent, (string, DateTime)[]>(LoadMusicScoreNames);
			dispatcher.Register<GetSelectedToolTypeEvent, MusicScoreMakerUtility.ToolType>(GetSelectedToolType);
			dispatcher.Register<GetSelectedNoteCategoryEvent, NoteCategory>(GetSelectedNoteCategory);
			dispatcher.Register<GetSelectedNoteTypeEvent, NoteType>(GetSelectedNoteType);
			dispatcher.Register<GetSelectedNoteDirectionEvent, NoteDirection>(GetSelectedNoteDirection);
			dispatcher.Register<GetSelectedNoteLineTypeEvent, NoteLineType>(GetSelectedNoteLineType);
			dispatcher.Register<GetSelectedIsSkipEvent, bool>(GetSelectedIsSkip);
			dispatcher.Register<IsEditRestrictedEvent, bool>(IsEditRestricted);
			dispatcher.Register<ToggleEditRestrictedEvent>(ToggleEditRestricted);
			dispatcher.Register<OpenToolOptionDialogEvent>(OpenToolOptionDialog);
			dispatcher.Register<ShowRightSubWindowEvent>(OnShowRightSubWindow);
			dispatcher.Register<JumpToNextInvalidPlacementEvent>(OnJumpToNextInvalidPlacement);
			dispatcher.Register<EnableInvalidPlacementCheckChangedEvent>(OnEnableInvalidPlacementCheckChanged);
			dispatcher.Register<SwitchAreaSelectModeEvent>(SwitchAreaSelectMode);
			dispatcher.Register<SwitchCriticalFilterEvent>(SwitchCriticalFilter);
			dispatcher.Register<IsCriticalFilterEnabledEvent, bool>(IsCriticalFilterEnabled);
			dispatcher.Register<ClearNotesAndSpeedEventsEvent>(ClearNotesAndSpeedEvents);
			dispatcher.Register<ShowClearNotesAndSpeedEventsDialogEvent>(ShowClearNotesAndSpeedEventsDialog);
			dispatcher.Register<SaveAndPostMusicScoreEvent>(SaveAndPostMusicScore);
			dispatcher.Register<RecheckJudgmentNoteGapEvent>(OnRecheckJudgmentNoteGap);
			dispatcher.Register<CallTutorialEvent>(CallTutorial);
			dispatcher.Register<ShowNoteAndComboCountDialogEvent>(ShowNoteAndComboCountDialog);
			dispatcher.EditGuard = OnEditGuardForFullCombo;
		}

		private void DisposeToolEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<GetMusicScoreMakerDataEvent, MusicScoreMakerData>(GetMusicScoreMakerData);
			dispatcher.Remove<OnToolIconClickEvent>(OnToolIconClick);
			dispatcher.Remove<IsAreaSelectModeEvent, bool>(IsAreaSelectMode);
			dispatcher.Remove<IsRemoveModeEvent, bool>(IsRemoveMode);
			dispatcher.Remove<OnTestPlayEvent>(OnTestPlay);
			dispatcher.Remove<OnMusicSelectorEvent>(OnMusicSelector);
			dispatcher.Remove<OpenMusicScoreSaveDataManagerDialogEvent>(OpenMusicScoreSaveDataManagerDialog);
			dispatcher.Remove<AutoSaveMusicScoreEvent>(AutoSaveMusicScore);
			dispatcher.Remove<SaveMusicScoreEvent>(SaveMusicScore);
			dispatcher.Remove<QuickSaveMusicScoreEvent>(QuickSaveMusicScore);
			dispatcher.Remove<SaveDraftScreenMusicScoreEvent>(SaveDraftScreenMusicScore);
			dispatcher.Remove<OpenLoadScreenEvent>(OpenLoadScreen);
			dispatcher.Remove<LoadMusicScoreEvent>(LoadMusicScore);
			dispatcher.Remove<DeleteMusicScoreEvent>(DeleteMusicScore);
			dispatcher.Remove<LoadMusicScoreNamesEvent, (string, DateTime)[]>(LoadMusicScoreNames);
			dispatcher.Remove<GetSelectedToolTypeEvent, MusicScoreMakerUtility.ToolType>(GetSelectedToolType);
			dispatcher.Remove<GetSelectedNoteCategoryEvent, NoteCategory>(GetSelectedNoteCategory);
			dispatcher.Remove<GetSelectedNoteTypeEvent, NoteType>(GetSelectedNoteType);
			dispatcher.Remove<GetSelectedNoteDirectionEvent, NoteDirection>(GetSelectedNoteDirection);
			dispatcher.Remove<GetSelectedNoteLineTypeEvent, NoteLineType>(GetSelectedNoteLineType);
			dispatcher.Remove<GetSelectedIsSkipEvent, bool>(GetSelectedIsSkip);
			dispatcher.Remove<IsEditRestrictedEvent, bool>(IsEditRestricted);
			dispatcher.Remove<ToggleEditRestrictedEvent>(ToggleEditRestricted);
			dispatcher.Remove<OpenToolOptionDialogEvent>(OpenToolOptionDialog);
			dispatcher.Remove<ShowRightSubWindowEvent>(OnShowRightSubWindow);
			dispatcher.Remove<JumpToNextInvalidPlacementEvent>(OnJumpToNextInvalidPlacement);
			dispatcher.Remove<EnableInvalidPlacementCheckChangedEvent>(OnEnableInvalidPlacementCheckChanged);
			dispatcher.Remove<SwitchAreaSelectModeEvent>(SwitchAreaSelectMode);
			dispatcher.Remove<SwitchCriticalFilterEvent>(SwitchCriticalFilter);
			dispatcher.Remove<IsCriticalFilterEnabledEvent, bool>(IsCriticalFilterEnabled);
			dispatcher.Remove<ClearNotesAndSpeedEventsEvent>(ClearNotesAndSpeedEvents);
			dispatcher.Remove<ShowClearNotesAndSpeedEventsDialogEvent>(ShowClearNotesAndSpeedEventsDialog);
			dispatcher.Remove<SaveAndPostMusicScoreEvent>(SaveAndPostMusicScore);
			dispatcher.Remove<RecheckJudgmentNoteGapEvent>(OnRecheckJudgmentNoteGap);
			dispatcher.Remove<CallTutorialEvent>(CallTutorial);
			dispatcher.Remove<ShowNoteAndComboCountDialogEvent>(ShowNoteAndComboCountDialog);
		}

		private MusicScoreMakerData GetMusicScoreMakerData(GetMusicScoreMakerDataEvent dataEvent)
		{
			return _model?.MusicScoreMakerData;
		}

		private void OnToolIconClick(OnToolIconClickEvent obj)
		{
			if (_model == null || obj == null)
			{
				return;
			}
			switch (obj.ToolType)
			{
			case MusicScoreMakerUtility.ToolType.Undo:
				Undo(new UndoEvent());
				return;
			case MusicScoreMakerUtility.ToolType.Redo:
				Redo(new RedoEvent());
				return;
			case MusicScoreMakerUtility.ToolType.Remove:
				_model.RemoveMode = !_model.RemoveMode;
				_model.AreaSelectMode = false;
				break;
			case MusicScoreMakerUtility.ToolType.AreaRemove:
			case MusicScoreMakerUtility.ToolType.AreaSelect:
				TryToggleAreaSelectMode();
				break;
			case MusicScoreMakerUtility.ToolType.TestPlay:
				OnTestPlay(new OnTestPlayEvent());
				return;
			case MusicScoreMakerUtility.ToolType.MusicSelector:
				OnMusicSelector(new OnMusicSelectorEvent());
				return;
			case MusicScoreMakerUtility.ToolType.EditRestrict:
				ToggleEditRestricted(new ToggleEditRestrictedEvent());
				return;
			default:
				_model.SelectedToolType = obj.ToolType;
				_model.SelectedNoteCategory = ToolTypeToNoteCategory(obj.ToolType);
				_model.RemoveMode = false;
				_model.AreaSelectMode = false;
				break;
			}
			NotifyMusicScoreAndTimelineChanged();
		}

		private void SwitchAreaSelectMode(SwitchAreaSelectModeEvent e)
		{
			TryToggleAreaSelectMode();
		}

		private void TryToggleAreaSelectMode()
		{
			if (_model == null || _model.IsMusicPlaying)
			{
				return;
			}
			_model.AreaSelectMode = !_model.AreaSelectMode;
			if (_model.AreaSelectMode)
			{
				_model.RemoveMode = false;
			}
			ClearTemporaryAreaSelection();
			NotifyMusicScoreAndTimelineChanged();
		}

		private void SwitchCriticalFilter(SwitchCriticalFilterEvent e)
		{
			if (_model == null)
			{
				return;
			}
			_model.CriticalFilterEnabled = !_model.CriticalFilterEnabled;
			NotifyMusicScoreAndTimelineChanged(refresh: true);
		}

		private bool IsCriticalFilterEnabled(IsCriticalFilterEnabledEvent obj)
		{
			return _model?.CriticalFilterEnabled ?? false;
		}

		private void SelectedToolTypeAction(OnMusicScorePreviewClickEvent obj)
		{
			if (_model == null || _model.IsEditRestricted || _model.IsEventSettingMode || !_model.IsNoteDataSelected)
			{
				return;
			}
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			if (data.SelectedNoteIdList.Count > 0 || data.SelectedTemporaryNoteIdList.Count > 0)
			{
				data.ClearSelectedNotes();
				data.ClearSelectedTemporaryNotes();
				data.SelectedTargetOperation = null;
				NotifyMusicScoreAndTimelineChanged();
				return;
			}
			int category = (int)_model.SelectedNoteCategory;
			if (category < 0 || category > 13)
			{
				return;
			}
			if (((1 << category) & 0x7FB) != 0)
			{
				AddToolTypeNote(obj);
			}
			else
			{
				AddToolTypeConnectionNote(obj);
			}
		}

		private void AddToolTypeConnectionNote(OnMusicScorePreviewClickEvent onMusicScorePreviewClickEvent)
		{
			if (onMusicScorePreviewClickEvent?.EventData == null || _model == null)
			{
				return;
			}
			RectTransform rect = _view != null ? _view.NotesViewRectTransform : null;
			if (rect == null)
			{
				return;
			}

			Vector2 pressLocalPoint = MusicScoreMakerUtility.CalcLocalPoint(onMusicScorePreviewClickEvent.EventData, onMusicScorePreviewClickEvent.EventData.pressPosition, rect);
			Vector2 rectSize = rect.rect.size;
			long ticks = MusicScoreMakerUtility.CalcTapPositionToTicks(pressLocalPoint, MusicScoreMakerUtility.GetPreviewStartTicks(), MusicScoreMakerUtility.GetPreviewEndTicks(), rectSize, true);
			int lane = MusicScoreMakerUtility.CalcTapPositionToLane(pressLocalPoint, rectSize);
			if (lane < 0 || lane > MusicScoreMakerModel.LaneCountMinus1)
			{
				return;
			}
			if (!TryFindRayHitLongNote(onMusicScorePreviewClickEvent.EventData, out MusicScoreNoteBase longNote) || longNote == null)
			{
				return;
			}

			bool added = TryAddNoteList(new AddNoteListEvent
			{
				NoteId = longNote.id,
				Ticks = ticks,
				Lane = lane,
				NoteCategory = _model.SelectedNoteCategory,
				NoteLineType = _model.SelectedNoteLineType,
				IsSkip = _model.SelectedIsSkip
			});
			if (added)
			{
				PlayToolTypeToNoteSe();
			}
			else
			{
				PlayNotePlacementFailSe();
			}
		}

		private void AddToolTypeNote(OnMusicScorePreviewClickEvent obj)
		{
			if (obj?.EventData == null)
			{
				return;
			}
			RectTransform rect = _view != null ? _view.NotesViewRectTransform : null;
			if (rect == null)
			{
				return;
			}
			(long ticks, int lane) = MusicScoreMakerUtility.CalcPressTicksAndLane(obj.EventData, rect);
			AddNote(lane, ticks);
			PlayToolTypeToNoteSe();
		}

		private bool IsAreaSelectMode(IsAreaSelectModeEvent obj)
		{
			return _model?.AreaSelectMode ?? false;
		}

		private bool IsRemoveMode(IsRemoveModeEvent obj)
		{
			return _model?.RemoveMode ?? false;
		}

		private void OnMusicSelector(OnMusicSelectorEvent obj)
		{
			// TODO(original): restore MusicScoreMakerMusicSelectorDialog. Current path returns to out-game selector.
			StopMusicIfPlaying();
			MusicScoreMakerUtility.RequestTransitionToOutGame(MenuScreenType.MusicScoreMakerTop);
		}

		private void OnTestPlay(OnTestPlayEvent evt)
		{
			RecalculateInvalidPlacementsForTestPlay();
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			if (HasBlockingInvalidPlacementsForTestPlay())
			{
				dispatcher.Publish(new UpdateMusicScoreEvent());
				dispatcher.Publish(new JumpToNextInvalidPlacementEvent());
				ShowTestPlayWarning("MSG_MUSIC_SCORE_MAKER_INVALID_PLACEMENT");
				return;
			}

			if (evt != null && evt.IsFromFullComboCheck)
			{
				PlayTestLive(null, true).Forget();
				return;
			}

			MusicScoreMakerTestPlayDialog dialog = null;
			dialog = ScreenManager.Instance?.Show2ButtonDialog<MusicScoreMakerTestPlayDialog>(
				DialogType.MusicScoreMakerTestPlayDialog,
				() => PlayTestLive(dialog, false).Forget(),
				null,
				DisplayLayerType.Layer_Dialog,
				DialogSize.Manual,
				true);
			dialog?.Setup(_model?.MusicId ?? 0);
		}

		[AsyncStateMachine(typeof(_003CPlayTestLive_003Ed__309))]
		private UniTask PlayTestLive(MusicScoreMakerTestPlayDialog dialog, bool isFromFullComboCheck = false)
		{
			return PlayTestLiveCore(dialog, isFromFullComboCheck);
		}

		private async UniTask PlayTestLiveCore(MusicScoreMakerTestPlayDialog dialog, bool isFromFullComboCheck)
		{
			ScreenLayerMusicScoreMaker screenLayer = ScreenManager.Instance?.GetLayerComponent<ScreenLayerMusicScoreMaker>(MenuScreenType.MusicScoreMaker);
			if (screenLayer != null)
			{
				await UniTask.WaitUntil(() => screenLayer.IsSetupComplete);
			}

			MusicScoreMakerEventDispatcher.Instance.SaveUndoRedoStack();
			MusicCategory musicCategory = dialog != null ? dialog.GetSelectedMusicCategory() : GetSavedTestPlayMusicCategory();
			bool isAutoPlay = !isFromFullComboCheck && MusicScoreMakerSettingsManager.AutoPlayEnabled;
			bool savedStartMusicTimeMsEnabled = MusicScoreMakerSettingsManager.SetStartMusicTimeMsEnabled;
			if (isFromFullComboCheck)
			{
				MusicScoreMakerSettingsManager.SetStartMusicTimeMsEnabled = false;
			}

			try
			{
				long focusTicks = GetFocusTicksForTestPlay();
				if (HasNotesCheck(focusTicks))
				{
					return;
				}

				int musicId = _model?.MusicId ?? 0;
				int vocalId = _model?.VocalId ?? 0;
				int deckId = UserDataManager.Instance.SelectedDeckId;
				MasterMusicAll masterMusicAll = LoadMasterMusicAll(musicId);
				if (masterMusicAll == null && _model?.CustomMusicScoreEntry == null)
				{
					UnityEngine.Debug.LogErrorFormat("MasterMusicAll not found for musicId: {0}", musicId);
				}
				vocalId = ResolveTestPlayVocalId(masterMusicAll, vocalId);
				MasterMusicDifficulty difficulty = ResolveTestPlayDifficulty(masterMusicAll, musicId);
				SetFreeLiveBootData(musicId, vocalId, deckId, focusTicks, difficulty, musicCategory, isAutoPlay);
				SetScreenLayerMusicScoreMakerBootArg(isFromFullComboCheck, focusTicks == 0L);
				Sekai.Core.EntryPoint.PlayMode = Sekai.Core.PlayMode.SoloLive;
				LiveTransitioner.SafeForceFinish(null);
				_view?.DeactivatePreviewForTransition();
				ScreenManager.Instance?.PushUIScreen(MenuScreenType.LiveLoading, false);
				ScreenManager.Instance?.GetLayerComponent<ScreenLayerLiveLoading>(MenuScreenType.LiveLoading);
			}
			finally
			{
				MusicScoreMakerSettingsManager.SetStartMusicTimeMsEnabled = savedStartMusicTimeMsEnabled;
			}
		}

		private MusicCategory GetSavedTestPlayMusicCategory()
		{
			return MusicScoreMakerSettingsManager.TryGetTestPlayLiveModeType(out LiveSettingData.LiveModeType liveModeType)
				? MusicScoreMakerUtility.ConvertLiveModeTypeToMusicCategory(liveModeType)
				: MusicCategory.original;
		}

		private void SetScreenLayerMusicScoreMakerBootArg(bool isFromFullComboCheck = false, bool isAllNotesIncluded = false)
		{
			MusicScoreMakerData data = CurrentData();
			if (_model == null || data == null)
			{
				return;
			}

			MusicScoreMakerEntryPoint.BootData = new MusicScoreMakerEntryPoint.MusicScoreMakerBootData
			{
				bootData = new ScreenLayerMusicScoreMaker.BootArg
				{
					musicId = _model.MusicId,
					difficulty = _model.Difficulty,
					vocalId = _model.VocalId,
					baseMusicScoreId = _model.BaseMusicScoreId,
					baseMusicDifficultyId = _model.BaseMusicDifficultyId,
					MusicScoreMakerData = DeepCopyHelper.DeepCopy(data),
					FocusTicks = _model.FocusTicks,
					QuantizeDivision = _model.QuantizeDivision,
					FromScreenType = MenuScreenType.MusicScoreMaker,
					LastSavedDataHash = _model.GetSavedDataHash(),
					LastSavedDraftSlotNo = _model.LastSavedDraftSlotNo,
					LastSavedDraft = _model.LastSavedDraft,
					FullComboDataHash = _model.GetFullComboDataHash(),
					MusicScoreDataHashAtTestPlay = _model.ComputeFullComboHash(),
					IsReturnFromTestPlay = true,
					IsFromFullComboCheck = isFromFullComboCheck,
					IsAllNotesIncludedInTestPlay = isAllNotesIncluded,
					CurrentMusicScoreScale = _model.CurrentMusicScoreScale,
					CustomMusicScoreEntry = _model.CustomMusicScoreEntry
				}
			};
		}

		private void SetFreeLiveBootData(int musicId, int vocalId, int deckId, long focusTicks, MasterMusicDifficulty difficulty, MusicCategory musicCategory, bool isAutoPlay)
		{
			string difficultyString = difficulty?.musicDifficulty ?? _model?.Difficulty ?? "master";
			FreeLiveBootData bootData = new FreeLiveBootData(
				musicId,
				difficultyString,
				vocalId,
				deckId,
				LivePlayMode.Free,
				LiveMusicData.CollaborationModeState.Off,
				musicCategory);

			bootData.LiveEventData = CreateTestPlayLiveEventData(deckId, isAutoPlay);
			bootData.LiveSettingData = LiveSettingData.LoadFromStorage();
			bootData.MVQualityType = bootData.LiveSettingData?.QualityType ?? Sekai.MVQualityType.Default;
			bootData.MusicCategory = musicCategory;
			bootData.IsAuto = isAutoPlay;
			bootData.IsCustomMusicScore = true;
			bootData.IsOfficialMusicScore = _model?.CustomMusicScoreEntry == null;
			bootData.ReturnScreenType = MenuScreenType.MusicScoreMaker;
			bootData.canSkipDisplayMusicInfo = true;

			MusicScore musicScore = _model?.ToMusicScore(focusTicks);
			if (bootData.MusicData != null)
			{
				string cueName = ResolveTestPlayCueName(musicId, vocalId);
				MasterMusicAll masterMusicAll = _model?.CustomMusicScoreEntry == null ? LoadMasterMusicAll(musicId) : null;
				bootData.MusicData.Music = masterMusicAll?.music ?? CreateTestPlayMusic(musicId, cueName);
				bootData.MusicData.Difficulty = difficulty;
				bootData.MusicData.Vocal = FindMasterMusicVocal(masterMusicAll, vocalId) ?? CreateTestPlayVocal(musicId, vocalId, cueName, _model?.CustomMusicScoreEntry?.Manifest.singer);
				bootData.MusicData.Score = new MasterPlayLevelScore
				{
					liveType = LiveType.solo.ToString(),
					playLevel = difficulty?.playLevel ?? _model?.CustomMusicScoreEntry?.Manifest.playLevel ?? 0
				};
				bootData.MusicData.IsTestPlay = true;
				bootData.MusicData.IsUseCustomScore = true;
				bootData.MusicData.CustomPlayLevel = difficulty?.playLevel ?? _model?.CustomMusicScoreEntry?.Manifest.playLevel ?? 0;
				bootData.MusicData.MusicScore = musicScore;
				bootData.MusicData.StartMusicTimeMs = MusicScoreMakerSettingsManager.CalcStartMusicTimeMs(bootData, Mathf.FloorToInt(_model?.FillerSec ?? 0f));
				bootData.MusicData.PlayStartEffectEnabled = false;
			}

			CustomMusicScoreEntry entry = _model?.CustomMusicScoreEntry;
			if (entry != null)
			{
				bootData.CustomMusicScoreId = entry.Manifest.id;
				bootData.CustomMusicScorePath = entry.RootDirectory;
				bootData.CustomMusicScoreTitle = entry.Manifest.scoreTitle;
				bootData.CustomMusicScoreAuthorName = entry.Manifest.userName;
			}

			UserDataManager.Instance.FreeLiveBootData = bootData;
		}

		private LiveEventData CreateTestPlayLiveEventData(int deckId, bool isAutoPlay)
		{
			return new LiveEventData(Array.Empty<IngameLotterySkill>(), Array.Empty<IngameComboCutin>(), deckId, isAutoPlay);
		}

		private MasterMusicDifficulty CreateTestPlayDifficulty(int musicId)
		{
			NoteAndComboCountInfo countInfo = NoteAndComboCountInfo.Calculate(CurrentData());
			return new MasterMusicDifficulty
			{
				id = _model?.BaseMusicDifficultyId ?? MasterMusicDifficulty.INVALID_ID,
				musicId = musicId,
				musicDifficulty = NormalizeTestPlayDifficulty(_model?.Difficulty),
				playLevel = _model?.CustomMusicScoreEntry?.Manifest.playLevel ?? 0,
				totalNoteCount = countInfo.TotalComboCount
			};
		}

		private MasterMusicDifficulty ResolveTestPlayDifficulty(MasterMusicAll masterMusicAll, int musicId)
		{
			string difficulty = NormalizeTestPlayDifficulty(_model?.Difficulty);
			MasterMusicDifficulty[] difficulties = masterMusicAll?.musicDifficulties;
			if (difficulties != null)
			{
				for (int i = 0; i < difficulties.Length; i++)
				{
					MasterMusicDifficulty candidate = difficulties[i];
					if (candidate != null && string.Equals(candidate.musicDifficulty, difficulty, StringComparison.OrdinalIgnoreCase))
					{
						return candidate;
					}
				}
			}

			return CreateTestPlayDifficulty(musicId);
		}

		private static string NormalizeTestPlayDifficulty(string difficulty)
		{
			if (string.IsNullOrEmpty(difficulty) || string.Equals(difficulty, "none", StringComparison.OrdinalIgnoreCase))
			{
				return "master";
			}

			return difficulty.ToLowerInvariant();
		}

		private static int ResolveTestPlayVocalId(MasterMusicAll masterMusicAll, int vocalId)
		{
			if (vocalId > 0)
			{
				return vocalId;
			}

			MasterMusicVocal[] vocals = masterMusicAll?.musicVocals;
			if (vocals == null)
			{
				return vocalId;
			}

			for (int i = 0; i < vocals.Length; i++)
			{
				if (vocals[i] != null && vocals[i].id > 0)
				{
					return vocals[i].id;
				}
			}

			return vocalId;
		}

		private static MasterMusicVocal FindMasterMusicVocal(MasterMusicAll masterMusicAll, int vocalId)
		{
			MasterMusicVocal[] vocals = masterMusicAll?.musicVocals;
			if (vocals == null)
			{
				return null;
			}

			for (int i = 0; i < vocals.Length; i++)
			{
				if (vocals[i] != null && vocals[i].id == vocalId)
				{
					return vocals[i];
				}
			}

			return null;
		}

		private MasterMusic CreateTestPlayMusic(int musicId, string cueName)
		{
			CustomMusicScoreEntry entry = _model?.CustomMusicScoreEntry;
			if (entry != null)
			{
				return new MasterMusic
				{
					id = musicId,
					title = entry.Manifest.title,
					lyricist = entry.Manifest.lyricist,
					composer = entry.Manifest.composer,
					arranger = entry.Manifest.arranger,
					assetbundleName = cueName,
					fillerSec = entry.Manifest.fillerSec,
					secForMusicScoreMaker = entry.MusicDurationSec,
					isAvailableForMusicScoreMaker = true
				};
			}

			return new MasterMusic
			{
				id = musicId,
				title = string.Empty,
				assetbundleName = cueName,
				fillerSec = _model?.FillerSec ?? 0f,
				secForMusicScoreMaker = _model?.MasterMusicSec ?? 0,
				isAvailableForMusicScoreMaker = true
			};
		}

		private static MasterMusicVocal CreateTestPlayVocal(int musicId, int vocalId, string cueName, string singer)
		{
			return new MasterMusicVocal
			{
				id = vocalId,
				musicId = musicId,
				musicVocalType = MusicVocalType.original_song.ToString(),
				caption = singer ?? string.Empty,
				assetbundleName = cueName
			};
		}

		private string ResolveTestPlayCueName(int musicId, int vocalId)
		{
			CustomMusicScoreEntry entry = _model?.CustomMusicScoreEntry;
			if (entry != null)
			{
				return entry.AudioCueName;
			}
			MasterMusicVocal vocal = ResolveMasterMusicVocal(musicId, vocalId);
			if (!string.IsNullOrEmpty(vocal?.assetbundleName))
			{
				return vocal.assetbundleName;
			}
			if (!string.IsNullOrEmpty(_model?.AssetbundleName))
			{
				return _model.AssetbundleName;
			}
			return musicId > 0 ? $"music_{musicId}_{vocalId}" : string.Empty;
		}

		private bool HasNotesCheck(long focusTicks)
		{
			if (HasNotesAfterFocusTicks(focusTicks))
			{
				return false;
			}

			ShowNoNotesForTestPlayDialog();
			return true;
		}

		private void ShowNoNotesForTestPlayDialog()
		{
			if (ScreenManager.Instance != null)
			{
				ScreenManager.Instance.Show1ButtonDialog<Common1ButtonDialog>(
					DialogType.Common1ButtonDialog,
					null,
					"MSG_MUSIC_SCORE_MAKER_NO_NOTES_FOR_TEST_PLAY",
					"WORD_OK",
					null,
					DisplayLayerType.Layer_Dialog,
					DialogSize.Manual,
					true);
				return;
			}

			UnityEngine.Debug.LogWarning(WordingManager.Get("MSG_MUSIC_SCORE_MAKER_NO_NOTES_FOR_TEST_PLAY"));
		}

		private void ShowTestPlayWarning(string wordingKey)
		{
			string message = WordingManager.Get(wordingKey);
			if (ScreenManager.Instance != null)
			{
				DialogUtility.ShowCommonSubWindowDialog(message, null);
				return;
			}

			UnityEngine.Debug.LogWarning(message);
		}

		private bool OnEditGuardForFullCombo(Action proceedAction)
		{
			if (_model == null || !_model.IsFullComboCleared || _model.HasConfirmedEditAfterFullCombo)
			{
				return false;
			}
			// TODO(original): restore full-combo edit confirmation dialog. We auto-confirm so editing works.
			_model.ConfirmEditAfterFullCombo();
			proceedAction?.Invoke();
			return true;
		}

		private long GetFocusTicksForTestPlay()
		{
			if (!MusicScoreMakerSettingsManager.SetStartMusicTimeMsEnabled)
			{
				return 0L;
			}
			return _model?.FocusTicks ?? 0L;
		}

		private bool HasNotesAfterFocusTicks(long focusTicks)
		{
			MusicScoreMakerData data = CurrentData();
			if (data?.NoteList == null)
			{
				return false;
			}
			foreach (MusicScoreNoteBase note in data.NoteList)
			{
				if (note != null && note.ticks >= focusTicks)
				{
					return true;
				}
			}
			return false;
		}

		private void OpenMusicScoreSaveDataManagerDialog(OpenMusicScoreSaveDataManagerDialogEvent obj)
		{
			// TODO(original): restore MusicScoreSaveDataManagerDialog. Fallback opens load/save screen logic.
			OpenLoadScreen(new OpenLoadScreenEvent());
		}

		private void AutoSaveMusicScore(AutoSaveMusicScoreEvent e)
		{
			SaveMusicScore(MusicScoreMakerRepository.GenerateAutoSaveFileName());
		}

		private void QuickSaveMusicScore(QuickSaveMusicScoreEvent e)
		{
			if (!TrySaveCustomMusicScoreEntry())
			{
				SaveMusicScore(string.IsNullOrEmpty(_model?.LastSelectFile) ? "quick_save.json" : _model.LastSelectFile);
			}
			if (e != null && e.IsExitOnSave)
			{
				ExitToOutGame();
			}
		}

		private void SaveDraftScreenMusicScore(SaveDraftScreenMusicScoreEvent e)
		{
			SaveMusicScore(string.IsNullOrEmpty(_model?.LastSelectFile) ? "draft_save.json" : _model.LastSelectFile);
			if (e != null && e.IsExitOnSave)
			{
				ExitToOutGame();
			}
		}

		private void OpenSaveDraftScreen(bool isExitOnSave)
		{
			SaveDraftScreenMusicScore(new SaveDraftScreenMusicScoreEvent(isExitOnSave));
		}

		[AsyncStateMachine(typeof(_003CExecuteQuickSaveDraftAsync_003Ed__323))]
		private UniTask ExecuteQuickSaveDraftAsync(bool isExitOnSave)
		{
			SaveDraftScreenMusicScore(new SaveDraftScreenMusicScoreEvent(isExitOnSave));
			return UniTask.CompletedTask;
		}

		private void OpenLoadScreen(OpenLoadScreenEvent e)
		{
			if (_model != null && _model.HasUnsavedChanges())
			{
				OpenLoadScreenSaveConfirmationDialog();
			}
		}

		private void OpenLoadScreenSaveConfirmationDialog()
		{
			// TODO(original): restore confirmation dialog. For now leave the editor open.
		}

		private void OnSaveBeforeLoad(CommonMultiButtonDialog dialog)
		{
			dialog?.Close();
			QuickSaveMusicScore(new QuickSaveMusicScoreEvent());
		}

		private void OnLoadWithoutSave(CommonMultiButtonDialog dialog)
		{
			dialog?.Close();
		}

		private static string ResolveLocalMusicScoreDirectory()
		{
			return System.IO.Path.Combine(Application.persistentDataPath, "music_score_maker");
		}

		private static string ResolveLocalMusicScorePath(string fileName)
		{
			string safeFileName = string.IsNullOrEmpty(fileName) ? "music_score.json" : System.IO.Path.GetFileName(fileName);
			if (!safeFileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
			{
				safeFileName += ".json";
			}
			return System.IO.Path.Combine(ResolveLocalMusicScoreDirectory(), safeFileName);
		}

		private void SaveMusicScore(SaveMusicScoreEvent e)
		{
			if ((e == null || string.IsNullOrEmpty(e.FileName)) && TrySaveCustomMusicScoreEntry())
			{
				return;
			}
			SaveMusicScore(e?.FileName);
		}

		private void SaveMusicScore(string fileName)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null || string.IsNullOrEmpty(fileName))
			{
				return;
			}
			MusicScoreMakerRepository repository = new MusicScoreMakerRepository
			{
				MusicScoreMakerData = data,
				BaseMusicScoreId = _model?.BaseMusicScoreId,
				BaseMusicDifficultyId = _model?.BaseMusicDifficultyId ?? -1
			};
			MusicScoreMakerRepository.SaveToStorage(repository, fileName);
			if (_model != null && !fileName.StartsWith(MusicScoreMakerRepository.AutoSaveFilePrefix, StringComparison.Ordinal))
			{
				_model.LastSelectFile = System.IO.Path.GetFileName(fileName);
				_model.UpdateSavedDataHash();
			}
			UnityEngine.Debug.Log($"MusicScoreMaker saved: {fileName}");
		}

		private bool TrySaveCustomMusicScoreEntry()
		{
			MusicScoreMakerData data = CurrentData();
			CustomMusicScoreEntry entry = _model?.CustomMusicScoreEntry;
			if (data == null || entry == null)
			{
				return false;
			}

			CustomMusicScoreStorage.SaveScore(entry, data);
			_model.LastSelectFile = entry.Manifest.scoreFileName;
			_model.UpdateSavedDataHash();
			UnityEngine.Debug.Log($"Custom music score saved: {entry.ScorePath}");
			return true;
		}

		private void LoadMusicScore(LoadMusicScoreEvent e)
		{
			if (e == null)
			{
				return;
			}
			MusicScoreMakerRepository repository = MusicScoreMakerRepository.LoadFromStorage(e.FileName);
			MusicScoreMakerData data = repository.MusicScoreMakerData;
			if (data == null)
			{
				string path = ResolveLocalMusicScorePath(e.FileName);
				if (!System.IO.File.Exists(path))
				{
					UnityEngine.Debug.LogWarning($"MusicScoreMaker load target not found: {path}");
					return;
				}
				data = DeepCopyHelper.FromJson<MusicScoreMakerData>(System.IO.File.ReadAllText(path));
				if (data == null)
				{
					return;
				}
			}
			if (e.ClearNotesAndSpeedEvents)
			{
				data.ClearNotesAndSpeedEvents();
			}
			else if (e.DiscardSpeedChanges)
			{
				data.ClearSpeedChangeEvents();
			}
			data.InitializeIdCount();
			_model.BaseMusicScoreId = repository.BaseMusicScoreId;
			_model.BaseMusicDifficultyId = repository.BaseMusicDifficultyId;
			_model.SetMusicScoreMakerDataAndUpdateHash(data);
			_model.LastSelectFile = System.IO.Path.GetFileName(e.FileName);
			NotifyMusicScoreAndTimelineChanged(refresh: true);
		}

		private void ClearNotesAndSpeedEvents(ClearNotesAndSpeedEventsEvent e)
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			Action redo = () =>
			{
				data.ClearNotesAndSpeedEvents();
				ClearSelectedList();
				NotifyMusicScoreAndTimelineChanged(refresh: true);
			};
			MusicScoreMakerData backup = DeepCopyHelper.DeepCopy(data);
			Action undo = () =>
			{
				_model.MusicScoreMakerData = DeepCopyHelper.DeepCopy(backup);
				NotifyMusicScoreAndTimelineChanged(refresh: true);
			};
			PushUndoableAction(undo, redo, _model?.FocusTicks ?? -1L, _model?.FocusTicks ?? -1L);
		}

		private void ShowClearNotesAndSpeedEventsDialog(ShowClearNotesAndSpeedEventsDialogEvent e)
		{
			// TODO(original): restore confirmation dialog.
			ClearNotesAndSpeedEvents(new ClearNotesAndSpeedEventsEvent());
		}

		private void SaveAndPostMusicScore(SaveAndPostMusicScoreEvent e)
		{
			SaveMusicScore("publish_preview.json");
			ShowPreviewStartDialog();
		}

		[AsyncStateMachine(typeof(_003CSaveAndPostMusicScoreAsync_003Ed__334))]
		private UniTask SaveAndPostMusicScoreAsync()
		{
			SaveAndPostMusicScore(new SaveAndPostMusicScoreEvent());
			return UniTask.CompletedTask;
		}

		public void ShowPreviewStartDialog()
		{
			// TODO(original): restore MusicScoreMakerPreviewStartDialog. Recalculate validations and log for now.
			RecalculateInvalidPlacementsForTestPlay();
			UnityEngine.Debug.Log("MusicScoreMaker preview/test-play dialog is not restored yet.");
		}

		[AsyncStateMachine(typeof(_003CCheckBaseCustomScoreChangeAsync_003Ed__336))]
		private UniTask<bool> CheckBaseCustomScoreChangeAsync(string baseMusicScoreId)
		{
			return UniTask.FromResult(true);
		}

		private EventBase[] GetSkillFeverEventsForPreviewDialog()
		{
			return CurrentData()?.EventArray ?? Array.Empty<EventBase>();
		}

		private float GetScoreLengthSec()
		{
			if (_model == null)
			{
				return 0f;
			}
			return _model.MasterMusicSec > 0 ? _model.MasterMusicSec : Mathf.Max(0f, _model.MusicLength / 1000f);
		}

		private bool HasInvalidPlacements()
		{
			return CurrentData()?.InvalidPlacements?.Count > 0;
		}

		private bool HasNonSkippedInvalidPlacements()
		{
			List<InvalidPlacementInfo> invalidPlacements = CurrentData()?.InvalidPlacements;
			if (invalidPlacements == null)
			{
				return false;
			}
			foreach (InvalidPlacementInfo info in invalidPlacements)
			{
				if (info != null)
				{
					return true;
				}
			}
			return false;
		}

		private void RecalculateInvalidPlacementsForTestPlay()
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			float scoreLengthSec = GetScoreLengthSec();
			data.RecalculateInvalidPlacements();
			data.RecalculateNoteDensityPlacements(scoreLengthSec);
			data.RecalculateComboCountValidation(_model?.ComboCountMinimum ?? 1);
			data.RecalculateInvalidTimeSignaturePlacements();
			data.RecalculateLongNoteMeshOverflowPlacements(scoreLengthSec);
			data.RecalculateGuideMeshOverflowPlacements(scoreLengthSec);
			data.RecalculateTapNotesMinimumValidation();
		}

		private bool HasBlockingInvalidPlacementsForTestPlay()
		{
			List<InvalidPlacementInfo> invalidPlacements = CurrentData()?.InvalidPlacements;
			if (invalidPlacements == null)
			{
				return false;
			}
			foreach (InvalidPlacementInfo info in invalidPlacements)
			{
				if (info == null)
				{
					continue;
				}
				if (IsNonBlockingInvalidPlacementForTestPlay(info.Type))
				{
					continue;
				}
				return true;
			}
			return false;
		}

		private static bool IsNonBlockingInvalidPlacementForTestPlay(InvalidPlacementType type)
		{
			return type == InvalidPlacementType.JudgmentNoteGap
				|| type == InvalidPlacementType.ComboCountUnderflow
				|| type == InvalidPlacementType.TapNotesUnderflow;
		}

		private void OnRecheckJudgmentNoteGap(RecheckJudgmentNoteGapEvent evt)
		{
			RecheckJudgmentNoteGapIfEditedInGapRange();
		}

		private void RecheckJudgmentNoteGapIfEditedInGapRange()
		{
			MusicScoreMakerData data = CurrentData();
			if (data == null)
			{
				return;
			}
			IEnumerable<long> ticks = MusicScoreMakerUtility.GetRecentlyEditedTicksSnapshot();
			if (ticks == null)
			{
				return;
			}
			List<long> editedTicks = new List<long>(ticks);
			if (editedTicks.Count == 0 || data.InvalidPlacements == null)
			{
				return;
			}

			bool hasGapOrDensity = false;
			bool recalculateCombo = false;
			bool recalculateLongMesh = false;
			bool recalculateGuideMesh = false;
			bool recalculateTapNotes = false;
			foreach (InvalidPlacementInfo info in data.InvalidPlacements)
			{
				if (info == null)
				{
					continue;
				}
				switch (info.Type)
				{
					case InvalidPlacementType.JudgmentNoteGap:
					case InvalidPlacementType.NoteDensityOverflow:
						hasGapOrDensity = true;
						break;
					case InvalidPlacementType.ComboCountUnderflow:
					case InvalidPlacementType.ComboCountOverflow:
						recalculateCombo = true;
						break;
					case InvalidPlacementType.LongNoteMeshOverflow:
						recalculateLongMesh = true;
						break;
					case InvalidPlacementType.GuideMeshOverflow:
						recalculateGuideMesh = true;
						break;
					case InvalidPlacementType.TapNotesUnderflow:
						recalculateTapNotes = true;
						break;
				}
			}

			bool recalculateGapOrDensity = false;
			if (hasGapOrDensity)
			{
				foreach (long editedTick in editedTicks)
				{
					foreach (InvalidPlacementInfo info in data.InvalidPlacements)
					{
						if (info == null)
						{
							continue;
						}
						if ((info.Type == InvalidPlacementType.JudgmentNoteGap || info.Type == InvalidPlacementType.NoteDensityOverflow)
							&& editedTick >= info.Ticks
							&& editedTick <= info.EndTicks)
						{
							recalculateGapOrDensity = true;
							break;
						}
					}
					if (recalculateGapOrDensity)
					{
						break;
					}
				}
			}

			if (!recalculateGapOrDensity && !recalculateLongMesh && !recalculateGuideMesh && !recalculateCombo && !recalculateTapNotes)
			{
				return;
			}
			float scoreLengthSec = GetScoreLengthSec();
			if (recalculateGapOrDensity)
			{
				data.RecalculateJudgmentNoteGapPlacements(scoreLengthSec);
				data.RecalculateNoteDensityPlacements(scoreLengthSec);
			}
			if (recalculateLongMesh)
			{
				data.RecalculateLongNoteMeshOverflowPlacements(scoreLengthSec);
			}
			if (recalculateGuideMesh)
			{
				data.RecalculateGuideMeshOverflowPlacements(scoreLengthSec);
			}
			if (recalculateCombo)
			{
				data.RecalculateComboCountValidation(_model?.ComboCountMinimum ?? 1);
			}
			if (recalculateTapNotes)
			{
				data.RecalculateTapNotesMinimumValidation();
			}
		}

		private void OnPreviewStartDecide(MusicScorePreviewPlayData previewPlayData)
		{
			ShowPreviewStartDialog();
		}

		[AsyncStateMachine(typeof(_003CExecutePublishAsync_003Ed__346))]
		private UniTask ExecutePublishAsync([NotNull] MusicScorePreviewPlayData previewPlayData)
		{
			SaveMusicScore("publish_preview.json");
			return UniTask.CompletedTask;
		}

		private void CallTutorial(CallTutorialEvent e)
		{
			MusicScoreMakerRuleSlideTutorialUtility.TryShowTutorialSlideIfFirstTime("enter");
		}

		private void ShowNoteAndComboCountDialog(ShowNoteAndComboCountDialogEvent e)
		{
			ShowNoteAndComboCountDialogAsync().Forget();
		}

		[AsyncStateMachine(typeof(_003CShowNoteAndComboCountDialogAsync_003Ed__349))]
		private UniTaskVoid ShowNoteAndComboCountDialogAsync()
		{
			// TODO(original): restore MusicScoreMakerNoteAndComboCountDialog view wiring.
			return default;
		}

		[AsyncStateMachine(typeof(_003CCalculateBaseCustomScoreChangeRateAsync_003Ed__350))]
		private UniTask<float?> CalculateBaseCustomScoreChangeRateAsync(string baseMusicScoreId, CancellationToken ct = default(CancellationToken))
		{
			return UniTask.FromResult<float?>(null);
		}

		private void DeleteMusicScore(DeleteMusicScoreEvent e)
		{
			if (!string.IsNullOrEmpty(e?.FileName))
			{
				MusicScoreMakerRepository.Delete(e.FileName);
			}
		}

		private (string, DateTime)[] LoadMusicScoreNames(LoadMusicScoreNamesEvent e)
		{
			return MusicScoreMakerRepository.GetFileInfos();
		}

		private Sekai.MusicScoreMaker.Ingame.Utilities.MusicScoreMakerUtility.ToolType GetSelectedToolType(GetSelectedToolTypeEvent obj)
		{
			return _model?.SelectedToolType ?? MusicScoreMakerUtility.ToolType.None;
		}

		private NoteType GetSelectedNoteType(GetSelectedNoteTypeEvent obj)
		{
			return _model?.SelectedNoteType ?? NoteType.Default;
		}

		private NoteDirection GetSelectedNoteDirection(GetSelectedNoteDirectionEvent obj)
		{
			return _model?.SelectedNoteDirection ?? NoteDirection.Default;
		}

		private NoteLineType GetSelectedNoteLineType(GetSelectedNoteLineTypeEvent obj)
		{
			return _model?.SelectedNoteLineType ?? NoteLineType.Linear;
		}

		private bool GetSelectedIsSkip(GetSelectedIsSkipEvent obj)
		{
			return _model?.SelectedIsSkip ?? false;
		}

		private bool IsEditRestricted(IsEditRestrictedEvent e)
		{
			return _model?.IsEditRestricted ?? false;
		}

		private void ToggleEditRestricted(ToggleEditRestrictedEvent e)
		{
			if (_model == null)
			{
				return;
			}
			_model.IsEditRestricted = !_model.IsEditRestricted;
			if (_model.IsEditRestricted)
			{
				ClearSelectedList();
				_model.RemoveMode = false;
				_model.AreaSelectMode = false;
			}
			NotifyMusicScoreAndTimelineChanged();
		}

		private void StopMusicIfPlaying()
		{
			if (_model?.IsMusicPlaying == true)
			{
				PauseMusic();
			}
		}

		private void OnShowRightSubWindow(ShowRightSubWindowEvent e)
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new ShowBottomSubWindowEvent());
			}
		}

		private void OpenToolOptionDialog(OpenToolOptionDialogEvent e)
		{
			// TODO(original): restore MusicScoreMakerOptionDialog window creation.
			MusicScoreMakerSettingsManager.SaveSettingData();
		}

		private void OnEnableInvalidPlacementCheckChanged(EnableInvalidPlacementCheckChangedEvent evt)
		{
			MusicScoreMakerSettingsManager.EnableInvalidPlacementCheck = evt == null || evt.IsEnabled;
			RecalculateInvalidPlacementsForTestPlay();
			NotifyMusicScoreAndTimelineChanged(refresh: true);
		}

		private void OnJumpToNextInvalidPlacement(JumpToNextInvalidPlacementEvent evt)
		{
			if (!MusicScoreMakerSettingsManager.EnableInvalidPlacementCheck)
			{
				return;
			}
			List<InvalidPlacementInfo> invalidPlacements = CurrentData()?.InvalidPlacements;
			if (invalidPlacements == null || invalidPlacements.Count == 0)
			{
				ResetSameTicksState();
				return;
			}
			long currentFocus = MusicScoreMakerUtility.GetFocusTicks();
			if (TryShowNextErrorAtSameTicks(invalidPlacements, currentFocus))
			{
				return;
			}
			long targetTicks = FindJumpTargetTicks(invalidPlacements, currentFocus);
			JumpToInvalidPlacement(invalidPlacements, targetTicks);
		}

		private bool TryShowNextErrorAtSameTicks(List<InvalidPlacementInfo> invalidPlacements, long currentFocus)
		{
			if (invalidPlacements == null)
			{
				return false;
			}
			if (_lastJumpedTicks < 0L || _lastJumpedTicks != currentFocus || _sameTicksCount < 2)
			{
				return false;
			}
			_sameTicksSubIndex++;
			if (_sameTicksSubIndex >= _sameTicksCount)
			{
				return false;
			}
			InvalidPlacementInfo info = FindInvalidPlacementAtTicksWithSubIndex(invalidPlacements, currentFocus, _sameTicksSubIndex);
			if (info != null && MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new ShowInvalidPlacementMessageEvent { Info = info });
			}
			return info != null;
		}

		private long FindJumpTargetTicks(List<InvalidPlacementInfo> invalidPlacements, long currentFocus)
		{
			_workInvalidTicks.Clear();
			if (invalidPlacements != null)
			{
				foreach (InvalidPlacementInfo info in invalidPlacements)
				{
					if (info != null)
					{
						_workInvalidTicks.Add(info.Ticks);
					}
				}
			}
			MusicScoreMakerUtility.SortAndUniqueInPlace(_workInvalidTicks);
			if (_workInvalidTicks.Count == 0)
			{
				return currentFocus;
			}
			return _lastJumpedTicks >= 0L && _lastJumpedTicks == currentFocus
				? FindNextTicks(currentFocus)
				: FindClosestTicks(currentFocus);
		}

		private long FindClosestTicks(long currentFocus)
		{
			if (_workInvalidTicks == null || _workInvalidTicks.Count == 0)
			{
				return -1L;
			}
			long bestTicks = _workInvalidTicks[0];
			long bestDistance = Math.Abs(bestTicks - currentFocus);
			for (int i = 1; i < _workInvalidTicks.Count; i++)
			{
				long ticks = _workInvalidTicks[i];
				long distance = Math.Abs(ticks - currentFocus);
				if (distance < bestDistance)
				{
					bestDistance = distance;
					bestTicks = ticks;
				}
			}
			return bestTicks;
		}

		private long FindNextTicks(long currentFocus)
		{
			if (_workInvalidTicks == null || _workInvalidTicks.Count == 0)
			{
				return -1L;
			}
			foreach (long ticks in _workInvalidTicks)
			{
				if (ticks > currentFocus)
				{
					return ticks;
				}
			}
			return _workInvalidTicks[0];
		}

		private void JumpToInvalidPlacement(List<InvalidPlacementInfo> invalidPlacements, long targetTicks)
		{
			if (targetTicks < 0L)
			{
				return;
			}
			_lastJumpedTicks = targetTicks;
			_sameTicksCount = CountInvalidPlacementsAtTicks(invalidPlacements, targetTicks);
			_sameTicksSubIndex = 0;
			SetFocusTicks(targetTicks);
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Publish(new UpdateMusicScoreEvent());
			dispatcher.Publish(new UpdateTimelineSliderValueWithoutNotifyEvent());
			InvalidPlacementInfo info = FindInvalidPlacementAtTicks(targetTicks);
			if (info != null)
			{
				dispatcher.Publish(new ShowInvalidPlacementMessageEvent { Info = info });
			}
		}

		private void ResetSameTicksState()
		{
			_lastJumpedTicks = -1L;
			_sameTicksCount = 0;
			_sameTicksSubIndex = 0;
		}

		private InvalidPlacementInfo FindInvalidPlacementAtTicks(long ticks)
		{
			List<InvalidPlacementInfo> invalidPlacements = CurrentData()?.InvalidPlacements;
			if (invalidPlacements == null)
			{
				return null;
			}
			foreach (InvalidPlacementInfo info in invalidPlacements)
			{
				if (info != null && info.Ticks == ticks)
				{
					return info;
				}
			}
			return null;
		}

		private int CountInvalidPlacementsAtTicks(List<InvalidPlacementInfo> invalidPlacements, long ticks)
		{
			int count = 0;
			if (invalidPlacements == null)
			{
				return 0;
			}
			foreach (InvalidPlacementInfo info in invalidPlacements)
			{
				if (info != null && info.Ticks == ticks)
				{
					count++;
				}
			}
			return count;
		}

		private InvalidPlacementInfo FindInvalidPlacementAtTicksWithSubIndex(List<InvalidPlacementInfo> invalidPlacements, long ticks, int subIndex)
		{
			if (invalidPlacements == null)
			{
				return null;
			}
			int index = 0;
			foreach (InvalidPlacementInfo info in invalidPlacements)
			{
				if (info == null || info.Ticks != ticks)
				{
					continue;
				}
				if (index == subIndex)
				{
					return info;
				}
				index++;
			}
			return null;
		}

		static MusicScoreMakerPresenter()
		{
			}
	}
}
