using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Common.PublishedMusicScoreList;
using Sekai.Sound;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Create.Content
{
	public sealed class MyMusicScoreSelectForCreateView : ContentViewBase<MyMusicScoreSelectForCreateViewData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CRefreshAsync_003Ed__2 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MyMusicScoreSelectForCreateView _003C_003E4__this;

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
		private PublishedMusicScoreListView _publishedListView;

		public void Setup(Action<int> onSelectMusicScore, Action<SortOrderBy> onChangeSortOrder, Func<CancellationToken, UniTask> onPreEnterDetailAsync, Func<CancellationToken, UniTask> onPostExitDetailAsync, Action onDecide, Action onClickFilterButton = null)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CRefreshAsync_003Ed__2))]
		public override UniTask RefreshAsync()
		{
			throw null;
		}

		public void ApplySelectedMusicScore(MusicScoreData musicScoreData)
		{
			throw null;
		}

		public IngameBGMPlayer TakeIngameBGMPlayer()
		{
			throw null;
		}

		public void OnPause()
		{
			throw null;
		}

		public void OnResume()
		{
			throw null;
		}

		public override void Dispose()
		{
			throw null;
		}

		public UniTask PlayCellStaggerFadeInAsync(CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		public MyMusicScoreSelectForCreateView()
		{
			throw null;
		}
	}
}
