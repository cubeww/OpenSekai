using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common.Category
{
	public abstract class CategoryPresenterBase : MonoBehaviour, ICategoryPresenter, IContentNavigator, IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CChangeContentAsync_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryPresenterBase _003C_003E4__this;

			public Defines.ContentType nextType;

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
		private struct _003CChangeContentCoreAsync_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryPresenterBase _003C_003E4__this;

			public CancellationToken ct;

			public Defines.ContentType nextType;

			public bool pushToStack;

			public IContentBootData bootData;

			private Defines.ContentType _003CcurrentType_003E5__2;

			private IContentPresenter _003CcurrentContent_003E5__3;

			private IContentPresenter _003CnextContent_003E5__4;

			private UniTask<IContentPresenter>.Awaiter _003C_003Eu__1;

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
		private struct _003CGetOrCreateContentAsync_003Ed__18 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<IContentPresenter> _003C_003Et__builder;

			public CategoryPresenterBase _003C_003E4__this;

			public Defines.ContentType contentType;

			public CancellationToken ct;

			private IContentPresenter _003Ccontent_003E5__2;

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
		private struct _003COnEnterAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryPresenterBase _003C_003E4__this;

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
		private struct _003COnExitAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryPresenterBase _003C_003E4__this;

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
		private struct _003CPushContentAsync_003Ed__13 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryPresenterBase _003C_003E4__this;

			public Defines.ContentType nextType;

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
		private struct _003CRequestBackAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryPresenterBase _003C_003E4__this;

			public CancellationToken ct;

			private ContentNavigationData _003ChistoryData_003E5__2;

			private Defines.ContentType _003CcurrentContentType_003E5__3;

			private IContentPresenter _003CcurrentContent_003E5__4;

			private IContentPresenter _003CnextContent_003E5__5;

			private UniTask<IContentPresenter>.Awaiter _003C_003Eu__1;

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
		private struct _003CSetupAsync_003Ed__8 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryPresenterBase _003C_003E4__this;

			public IScreenNavigator screenNavigator;

			public Defines.ContentType defaultContentType;

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
		private struct _003CSetupAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryPresenterBase _003C_003E4__this;

			public IScreenNavigator screenNavigator;

			public Defines.ContentType defaultContentType;

			public CancellationToken ct;

			public IContentBootData customBootData;

			private UniTask<IContentPresenter>.Awaiter _003C_003Eu__1;

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
		private struct _003CSetupForCustomInitialContentAsync_003Ed__10 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ContentNavigationData[] contentsToAddToHistory;

			public CategoryPresenterBase _003C_003E4__this;

			public IScreenNavigator screenNavigator;

			public ContentNavigationData initialContent;

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
		private CategoryView _view;

		private readonly CategoryModel _model;

		private IScreenNavigator _screenNavigator;

		private readonly Dictionary<Defines.ContentType, IContentPresenter> _contentRegistry;

		public IScreenNavigator ScreenNavigator
		{
			get
			{
				throw null;
			}
		}

		public bool IsEnableBackContent
		{
			get
			{
				throw null;
			}
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__8))]
		public UniTask SetupAsync(IScreenNavigator screenNavigator, Defines.ContentType defaultContentType, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__9))]
		protected UniTask SetupAsync(IScreenNavigator screenNavigator, Defines.ContentType defaultContentType, IContentBootData customBootData, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSetupForCustomInitialContentAsync_003Ed__10))]
		public UniTask SetupForCustomInitialContentAsync(IScreenNavigator screenNavigator, ContentNavigationData initialContent, ContentNavigationData[] contentsToAddToHistory, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnEnterAsync_003Ed__11))]
		public UniTask OnEnterAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnExitAsync_003Ed__12))]
		public UniTask OnExitAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPushContentAsync_003Ed__13))]
		public UniTask PushContentAsync(Defines.ContentType nextType, IContentBootData bootData, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CChangeContentAsync_003Ed__14))]
		public UniTask ChangeContentAsync(Defines.ContentType nextType, IContentBootData bootData, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CRequestBackAsync_003Ed__15))]
		public UniTask RequestBackAsync(CancellationToken ct)
		{
			throw null;
		}

		public virtual void OnWillExit()
		{
			throw null;
		}

		public virtual void Dispose()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CGetOrCreateContentAsync_003Ed__18))]
		[ItemCanBeNull]
		private UniTask<IContentPresenter> GetOrCreateContentAsync(Defines.ContentType contentType, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CChangeContentCoreAsync_003Ed__19))]
		private UniTask ChangeContentCoreAsync(Defines.ContentType nextType, IContentBootData bootData, bool pushToStack, CancellationToken ct)
		{
			throw null;
		}

		[CanBeNull]
		private IContentPresenter GetCurrentContent()
		{
			throw null;
		}

		protected CategoryPresenterBase()
		{
			throw null;
		}
	}
}
