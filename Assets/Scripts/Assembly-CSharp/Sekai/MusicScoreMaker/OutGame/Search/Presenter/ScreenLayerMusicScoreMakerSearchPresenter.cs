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
using Sekai.MusicScoreMaker.OutGame.Search.Model;
using Sekai.MusicScoreMaker.OutGame.Search.View;

namespace Sekai.MusicScoreMaker.OutGame.Search.Presenter
{
	public class ScreenLayerMusicScoreMakerSearchPresenter : IDisposable, IScreenNavigator
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CActivateCategoryAsync_003Ed__22 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

			public Sekai.MusicScoreMaker.OutGame.Defines.CategoryType categoryType;

			public ICategoryPresenter category;

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
		private struct _003CApplyCategoryAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

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
		private struct _003CApplyCategoryAsync_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

			public CancellationToken ct;

			public Sekai.MusicScoreMaker.OutGame.Defines.CategoryType categoryType;

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
		private struct _003CApplyCategoryWithMusicScoreAsync_003Ed__25 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

			public string musicScoreId;

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
		private struct _003CExecuteBackProcessAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

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
		private struct _003CGetOrCreateCategoryAsync_003Ed__23 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<ICategoryPresenter> _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

			public Sekai.MusicScoreMaker.OutGame.Defines.CategoryType categoryType;

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
		private struct _003CHideHeaderAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

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
		private struct _003COnBackBeforeContentAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

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
		private struct _003CPauseAsync_003Ed__26 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

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
		private struct _003CPlayInAnimationAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

			public bool isFromTop;

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
		private struct _003CResumeAsync_003Ed__27 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

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
		private struct _003CShowHeaderAsync_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchPresenter _003C_003E4__this;

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

		private ScreenLayerMusicScoreMakerSearchView _view;

		private ScreenLayerMusicScoreMakerSearchModel _model;

		private readonly Dictionary<Sekai.MusicScoreMaker.OutGame.Defines.CategoryType, ICategoryPresenter> _categoryRegistry;

		private bool _isProcessingBack;

		public UniTask SetupAsync(ScreenLayerMusicScoreMakerSearchView view, CancellationToken ct)
		{
			throw null;
		}

		public void InitializeAnimation(bool isFromTop)
		{
			throw null;
		}

		public void SetupCategoryTab()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayInAnimationAsync_003Ed__7))]
		public UniTask PlayInAnimationAsync(bool isFromTop, CancellationToken ct)
		{
			throw null;
		}

		private void OnBackUIScreenOverride(bool _)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteBackProcessAsync_003Ed__9))]
		private UniTask ExecuteBackProcessAsync()
		{
			throw null;
		}

		private void ClearBackUIScreenOverride()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CApplyCategoryAsync_003Ed__11))]
		public UniTask ApplyCategoryAsync(CancellationToken ct)
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

		[AsyncStateMachine(typeof(_003CShowHeaderAsync_003Ed__14))]
		public UniTask ShowHeaderAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CHideHeaderAsync_003Ed__15))]
		public UniTask HideHeaderAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnBackBeforeContentAsync_003Ed__16))]
		public UniTask<bool> OnBackBeforeContentAsync()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public void OnWillExit()
		{
			throw null;
		}

		private void OnChangeCategory(Sekai.MusicScoreMaker.OutGame.Defines.CategoryType categoryType)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CApplyCategoryAsync_003Ed__20))]
		private UniTask ApplyCategoryAsync(Sekai.MusicScoreMaker.OutGame.Defines.CategoryType categoryType, CancellationToken ct)
		{
			throw null;
		}

		[ItemCanBeNull]
		private ICategoryPresenter CreateAndRegisterCategory(Sekai.MusicScoreMaker.OutGame.Defines.CategoryType categoryType)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CActivateCategoryAsync_003Ed__22))]
		private UniTask ActivateCategoryAsync(ICategoryPresenter category, Sekai.MusicScoreMaker.OutGame.Defines.CategoryType categoryType, CancellationToken ct)
		{
			throw null;
		}

		[ItemCanBeNull]
		[AsyncStateMachine(typeof(_003CGetOrCreateCategoryAsync_003Ed__23))]
		private UniTask<ICategoryPresenter> GetOrCreateCategoryAsync(Sekai.MusicScoreMaker.OutGame.Defines.CategoryType categoryType, CancellationToken ct)
		{
			throw null;
		}

		private void GoToBookmarkScreen()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CApplyCategoryWithMusicScoreAsync_003Ed__25))]
		public UniTask ApplyCategoryWithMusicScoreAsync(string musicScoreId, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPauseAsync_003Ed__26))]
		public UniTask PauseAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CResumeAsync_003Ed__27))]
		public UniTask ResumeAsync(CancellationToken ct)
		{
			throw null;
		}

		private void OnScreenTitleImageVisibilityChange(ScreenTitleImageVisibilityChangeEvent data)
		{
			throw null;
		}

		public MenuScreenType GetScreenType()
		{
			throw null;
		}

		public ScreenLayerMusicScoreMakerSearchPresenter()
		{
			throw null;
		}
	}
}
