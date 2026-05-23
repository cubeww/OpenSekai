using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using DG.Tweening;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.MusicScoreMaker.OutGame.Common.Category;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.View
{
	public class ScreenLayerMusicScoreMakerSearchView : MonoBehaviour, IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CHideHeaderAsync_003Ed__17 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchView _003C_003E4__this;

			public CancellationToken ct;

			private TweenAwaiter _003C_003Eu__1;

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
		private struct _003CPlayInAnimationAsync_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public bool isFromTop;

			public ScreenLayerMusicScoreMakerSearchView _003C_003E4__this;

			public CancellationToken ct;

			private TweenAwaiter _003C_003Eu__1;

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
		private struct _003CShowHeaderAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearchView _003C_003E4__this;

			public CancellationToken ct;

			private TweenAwaiter _003C_003Eu__1;

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

		private CategoryPool _categoryPool;

		[SerializeField]
		private CanvasGroup _rootCanvasGroup;

		[SerializeField]
		private CanvasGroup _coverCanvasGroup;

		[SerializeField]
		private MusicScoreMakerLoading _loading;

		[SerializeField]
		private ScreenBackground _screenBackground;

		[SerializeField]
		private CategoryTabList _categoryTabList;

		[SerializeField]
		private RectTransform _categoryRoot;

		[SerializeField]
		private CanvasGroup _headerCanvasGroup;

		[SerializeField]
		private CustomImage _titleImage;

		private Tweener _titleImageFadeTween;

		[SerializeField]
		private CustomButton _gotoBookMarkButton;

		public void Setup(Action onClickGotoBookMark)
		{
			throw null;
		}

		public void InitializeAnimation(bool isFromTop)
		{
			throw null;
		}

		public void SetupCategoryTab(MusicScoreMakerSearchConfig.CategoryInfo[] categoryInfos, Action<Sekai.MusicScoreMaker.OutGame.Defines.CategoryType> onSelectCategory)
		{
			throw null;
		}

		[CanBeNull]
		public GameObject CreateCategory(Sekai.MusicScoreMaker.OutGame.Defines.CategoryType category)
		{
			throw null;
		}

		public void ShowHeaderImmediate()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowHeaderAsync_003Ed__16))]
		public UniTask ShowHeaderAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CHideHeaderAsync_003Ed__17))]
		public UniTask HideHeaderAsync(CancellationToken ct)
		{
			throw null;
		}

		public bool GetShowingHeader()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayInAnimationAsync_003Ed__19))]
		public UniTask PlayInAnimationAsync(bool isFromTop, CancellationToken ct)
		{
			throw null;
		}

		public void Pause()
		{
			throw null;
		}

		public void Resume()
		{
			throw null;
		}

		public void DoFadeTitleImage(bool isActive, float duration)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public ScreenLayerMusicScoreMakerSearchView()
		{
			throw null;
		}
	}
}
