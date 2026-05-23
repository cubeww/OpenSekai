using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public class SearchResultMusicScoreListSelectHeaderView : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CFadeInAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SearchResultMusicScoreListSelectHeaderView _003C_003E4__this;

			public float duration;

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
		private struct _003CFadeOutAsync_003Ed__10 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SearchResultMusicScoreListSelectHeaderView _003C_003E4__this;

			public float duration;

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

		[SerializeField]
		private CanvasGroup _canvasGroup;

		[SerializeField]
		private FilteredMusicElementCell _filterMusicElementCell;

		[SerializeField]
		private MusicScoreTagCell _musicScoreTagCell;

		[SerializeField]
		private UIPartsMusicDifficultyLabel _musicDifficultyLabel;

		[SerializeField]
		private FilteredMusicElementCell _searchCategoryCell;

		[SerializeField]
		private CustomButton _searchConditionButton;

		private Action _onClickSearchCondition;

		public void Setup(MusicScoreSearchCondition condition, Action onClickSearchCondition)
		{
			throw null;
		}

		private void OnDestroy()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeInAsync_003Ed__9))]
		public UniTask FadeInAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeOutAsync_003Ed__10))]
		public UniTask FadeOutAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		private string GetSearchCategoryText(SearchDetailDefines.SortCategoryType categoryType)
		{
			throw null;
		}

		public SearchResultMusicScoreListSelectHeaderView()
		{
			throw null;
		}
	}
}
