using System.Collections.Generic;
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
	public class MusicScoreListSelectModel : ContentModelBase<MusicScoreListSelectViewData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCreateMusicScoreDataArrayAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListSelectModel _003C_003E4__this;

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
		private struct _003CSetupAsync_003Ed__13 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListSelectModel _003C_003E4__this;

			public int? specifiedMusicId;

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

		private int? _specifiedMusicId;

		private readonly MusicScoreFilterEvaluator _filterEvaluator;

		private MusicScoreFilterData _musicScoreFilterData;

		private SortOrderBy _sortOrder;

		public int SpecifiedMusicId
		{
			get
			{
				throw null;
			}
		}

		public bool IsMusicIdSpecified
		{
			get
			{
				throw null;
			}
		}

		public MusicScoreFilterData MusicScoreFilterData
		{
			get
			{
				throw null;
			}
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__13))]
		public UniTask SetupAsync(int? specifiedMusicId, CancellationToken ct)
		{
			throw null;
		}

		public void UpdateSelectedMusicScore(int index)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CCreateMusicScoreDataArrayAsync_003Ed__15))]
		private UniTask CreateMusicScoreDataArrayAsync(CancellationToken ct)
		{
			throw null;
		}

		private void ApplyMusicScoreCellDataArray()
		{
			throw null;
		}

		public void ApplySortOrder(SortOrderBy sortOrder)
		{
			throw null;
		}

		private void ApplySortOrderToCellDataArray()
		{
			throw null;
		}

		private void ApplySelectedIndexToViewData()
		{
			throw null;
		}

		private void SetupSelectedMusicScore()
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

		private IEnumerable<int> GetRandomMusicIdsExcludingPlayHistory(IEnumerable<int> recentPlayedMusicIds, IEnumerable<int> totalPlayedMusicIds, int count)
		{
			throw null;
		}

		public MusicScoreListSelectModel()
		{
			throw null;
		}
	}
}
