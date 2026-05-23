using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.MusicScoreMaker.OutGame.Common.Category;

namespace Sekai.MusicScoreMaker.OutGame.Create
{
	public class ScreenLayerMusicScoreMakerMyMusicScoreForCreatePresenter : IDisposable, IScreenNavigator
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CApplyCategoryAsync_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerMyMusicScoreForCreatePresenter _003C_003E4__this;

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
		private struct _003CApplyCategoryAsync_003Ed__17 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerMyMusicScoreForCreatePresenter _003C_003E4__this;

			public CancellationToken ct;

			public Defines.CategoryType categoryType;

			private CancellationTokenSource _003ClinkedCts_003E5__2;

			private CancellationToken _003CswitchCt_003E5__3;

			private UniTask.Awaiter _003C_003Eu__1;

			private UniTask<ICategoryPresenter>.Awaiter _003C_003Eu__2;

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
		private struct _003CExecuteBackProcessAsync_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerMyMusicScoreForCreatePresenter _003C_003E4__this;

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
		private struct _003CGetOrCreateCategoryAsync_003Ed__21 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<ICategoryPresenter> _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerMyMusicScoreForCreatePresenter _003C_003E4__this;

			public Defines.CategoryType categoryType;

			public CancellationToken ct;

			private ICategoryPresenter _003Ccategory_003E5__2;

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
		private struct _003CHideHeaderAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerMyMusicScoreForCreatePresenter _003C_003E4__this;

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
		private struct _003COnBackBeforeContentAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerMyMusicScoreForCreatePresenter _003C_003E4__this;

			private UniTask<ICategoryPresenter>.Awaiter _003C_003Eu__1;

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
		private struct _003CShowHeaderAsync_003Ed__10 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerMyMusicScoreForCreatePresenter _003C_003E4__this;

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

		private readonly ScreenLayerMusicScoreMakerMyMusicScoreForCreateView _view;

		private readonly ScreenLayerMusicScoreMakerMyMusicScoreForCreateModel _model;

		private readonly Dictionary<Defines.CategoryType, ICategoryPresenter> _categoryRegistry;

		private CancellationTokenSource _categorySwitchCts;

		private bool _isProcessingBack;

		public ScreenLayerMusicScoreMakerMyMusicScoreForCreatePresenter(ScreenLayerMusicScoreMakerMyMusicScoreForCreateView view)
		{
			throw null;
		}

		public void Setup()
		{
			throw null;
		}

		public void SetupCategoryTab()
		{
			throw null;
		}

		public bool GetShowingHeader()
		{
			throw null;
		}

		public void ShowHeaderImmediate()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowHeaderAsync_003Ed__10))]
		public UniTask ShowHeaderAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CHideHeaderAsync_003Ed__11))]
		public UniTask HideHeaderAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnBackBeforeContentAsync_003Ed__12))]
		public UniTask<bool> OnBackBeforeContentAsync()
		{
			throw null;
		}

		public void SetOnBackUIScreenOverride()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CApplyCategoryAsync_003Ed__14))]
		public UniTask ApplyCategoryAsync(CancellationToken ct)
		{
			throw null;
		}

		private void OnCategorySelectedIndex(int index)
		{
			throw null;
		}

		private Defines.CategoryType ConvertIndexToCategory(int index)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CApplyCategoryAsync_003Ed__17))]
		private UniTask ApplyCategoryAsync(Defines.CategoryType categoryType, CancellationToken ct)
		{
			throw null;
		}

		private void OnBackUIScreenOverride(bool _)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteBackProcessAsync_003Ed__19))]
		private UniTask ExecuteBackProcessAsync()
		{
			throw null;
		}

		public void ClearBackUIScreenOverride()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CGetOrCreateCategoryAsync_003Ed__21))]
		[ItemCanBeNull]
		private UniTask<ICategoryPresenter> GetOrCreateCategoryAsync(Defines.CategoryType categoryType, CancellationToken ct)
		{
			throw null;
		}

		public MenuScreenType GetScreenType()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}
	}
}
