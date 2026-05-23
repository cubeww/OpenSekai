using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common.Content;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public sealed class SearchResultMusicScoreListSelectModel : ContentModelBase<SearchResultMusicScoreListSelectViewData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CApplyRankingFilterAsync_003Ed__28 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SearchResultMusicScoreListSelectModel _003C_003E4__this;

			public int index;

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
		private struct _003CCreateMusicScoreDataArrayAsync_003Ed__27 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SearchResultMusicScoreListSelectModel _003C_003E4__this;

			private UniTask<Sekai.Service.CustomMusicScorePublishedListResult>.Awaiter _003C_003Eu__1;

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
		private struct _003CSetupAsync_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SearchResultMusicScoreListSelectModel _003C_003E4__this;

			public MusicScoreSearchCondition searchCondition;

			public Action onAPIInterruption;

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

		private MusicScoreData[] _musicScoreDataArray;

		private MusicScoreData[] _filteredMusicScoreDataArray;

		private string _selectedMusicScoreId;

		private MusicScoreSearchCondition _searchCondition;

		private readonly MusicScoreFilterEvaluator _filterEvaluator;

		private MusicScoreFilterData _musicScoreFilterData;

		private SortOrderBy _sortOrder;

		private CustomMusicScoreSearchDisplayOrderCategory _displayOrderCategory;

		private Action _onAPIInterruption;

		public MusicScoreFilterData MusicScoreFilterData
		{
			get
			{
				throw null;
			}
		}

		public bool IsRankingFilterValid
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public Defines.RankingFilterType SelectedRankingFilter
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__19))]
		public UniTask SetupAsync([NotNull] MusicScoreSearchCondition searchCondition, Action onAPIInterruption, CancellationToken ct)
		{
			throw null;
		}

		private void SetupRankingFilter()
		{
			throw null;
		}

		private void UpdateDisplayOrderCategory(Defines.RankingFilterType rankingFilterType)
		{
			throw null;
		}

		public void ApplySelectedMusicScore(int index)
		{
			throw null;
		}

		public void ApplyFilter(MusicScoreFilterData result)
		{
			throw null;
		}

		public void SyncBookmarkStatus()
		{
			throw null;
		}

		[CanBeNull]
		public MusicScoreData GetSelectedMusicScoreData()
		{
			throw null;
		}

		public void RemoveMusicScore(string id)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CCreateMusicScoreDataArrayAsync_003Ed__27))]
		private UniTask CreateMusicScoreDataArrayAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CApplyRankingFilterAsync_003Ed__28))]
		public UniTask ApplyRankingFilterAsync(int index, CancellationToken ct)
		{
			throw null;
		}

		public void ApplySortOrder(SortOrderBy sortOrder)
		{
			throw null;
		}

		private void ApplyMusicScoreCellDataArray()
		{
			throw null;
		}

		private void ApplySortOrderToCellDataArray()
		{
			throw null;
		}

		private void ApplySelectedMusicScore()
		{
			throw null;
		}

		private void ApplySelectedIndexToViewData()
		{
			throw null;
		}

		private UIPartsSelectorCell.ViewData[] CreateRankingFilterTabViewDataArray()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public SearchResultMusicScoreListSelectModel()
		{
			throw null;
		}
	}
}
