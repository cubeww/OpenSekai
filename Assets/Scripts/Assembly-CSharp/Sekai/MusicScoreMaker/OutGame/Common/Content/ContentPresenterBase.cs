using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common.Content
{
	public abstract class ContentPresenterBase<TBootData, TViewData, TView, TModel> : MonoBehaviour, IContentPresenter, IDisposable where TBootData : IContentBootData where TViewData : ContentViewDataBase, new() where TView : ContentViewBase<TViewData> where TModel : ContentModelBase<TViewData>, new()
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CBootAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ContentPresenterBase<TBootData, TViewData, TView, TModel> _003C_003E4__this;

			public bool enableBack;

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
		private struct _003CExitAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ContentPresenterBase<TBootData, TViewData, TView, TModel> _003C_003E4__this;

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
		private struct _003CPauseAsync_003Ed__26 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ContentPresenterBase<TBootData, TViewData, TView, TModel> _003C_003E4__this;

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
		private struct _003CPlayBootAnimationAsync_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ContentPresenterBase<TBootData, TViewData, TView, TModel> _003C_003E4__this;

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
		private struct _003CPlayExitAnimationAsync_003Ed__23 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ContentPresenterBase<TBootData, TViewData, TView, TModel> _003C_003E4__this;

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
		private struct _003CPlayPostResumeAnimationAsync_003Ed__25 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ContentPresenterBase<TBootData, TViewData, TView, TModel> _003C_003E4__this;

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
		private struct _003CPushContentWithTapGuardAsync_003Ed__33 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ContentPresenterBase<TBootData, TViewData, TView, TModel> _003C_003E4__this;

			public Defines.ContentType contentType;

			public IContentBootData bootData;

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

			public ContentPresenterBase<TBootData, TViewData, TView, TModel> _003C_003E4__this;

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
		private struct _003CSetupAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ContentPresenterBase<TBootData, TViewData, TView, TModel> _003C_003E4__this;

			public IScreenNavigator screenNavigator;

			public IContentNavigator contentNavigator;

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

		[SerializeField]
		protected TView _view;

		protected TModel _model;

		protected IScreenNavigator _screenNavigator;

		protected IContentNavigator _contentNavigator;

		protected TBootData _bootData;

		private bool _isBooted;

		private bool _isPaused;

		public bool IsBooted
		{
			get
			{
				throw null;
			}
		}

		public bool IsPaused
		{
			get
			{
				throw null;
			}
		}

		[AsyncStateMachine(typeof(ContentPresenterBase<, , , >._003CSetupAsync_003Ed__11))]
		public UniTask SetupAsync(IScreenNavigator screenNavigator, IContentNavigator contentNavigator, CancellationToken ct)
		{
			throw null;
		}

		public void SetBootData(IContentBootData bootData)
		{
			throw null;
		}

		public IContentBootData GetBootData()
		{
			throw null;
		}

		public virtual IContentBootData CreateDefaultBootData()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(ContentPresenterBase<, , , >._003CBootAsync_003Ed__15))]
		public UniTask BootAsync(Defines.ApplyContentType applyContentType, bool enableBack, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(ContentPresenterBase<, , , >._003CExitAsync_003Ed__16))]
		public UniTask ExitAsync(CancellationToken ct)
		{
			throw null;
		}

		protected virtual UniTask OnPreSetupAsync(CancellationToken ct)
		{
			throw null;
		}

		protected virtual UniTask OnPostSetupAsync(CancellationToken ct)
		{
			throw null;
		}

		protected virtual UniTask OnPreBootAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(ContentPresenterBase<, , , >._003CPlayBootAnimationAsync_003Ed__20))]
		protected virtual UniTask PlayBootAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		protected virtual UniTask OnPostBootAsync(CancellationToken ct)
		{
			throw null;
		}

		protected virtual UniTask OnPreExitAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(ContentPresenterBase<, , , >._003CPlayExitAnimationAsync_003Ed__23))]
		protected UniTask PlayExitAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		protected virtual UniTask OnPostExitAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(ContentPresenterBase<, , , >._003CPlayPostResumeAnimationAsync_003Ed__25))]
		protected virtual UniTask PlayPostResumeAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(ContentPresenterBase<, , , >._003CPauseAsync_003Ed__26))]
		public UniTask PauseAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(ContentPresenterBase<, , , >._003CResumeAsync_003Ed__27))]
		public UniTask ResumeAsync(CancellationToken ct)
		{
			throw null;
		}

		public virtual void OnWillExit()
		{
			throw null;
		}

		protected virtual void OnPause()
		{
			throw null;
		}

		protected virtual void OnResume()
		{
			throw null;
		}

		public virtual void Dispose()
		{
			throw null;
		}

		protected void PushContentWithTapGuard(Defines.ContentType contentType, IContentBootData bootData)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(ContentPresenterBase<, , , >._003CPushContentWithTapGuardAsync_003Ed__33))]
		private UniTask PushContentWithTapGuardAsync(Defines.ContentType contentType, IContentBootData bootData, CancellationToken ct)
		{
			throw null;
		}

		protected ContentPresenterBase()
		{
			throw null;
		}
	}
}
