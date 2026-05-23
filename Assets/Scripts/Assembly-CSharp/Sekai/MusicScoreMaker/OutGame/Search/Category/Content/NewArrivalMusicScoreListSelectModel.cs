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
	public sealed class NewArrivalMusicScoreListSelectModel : ContentModelBase<NewArrivalMusicScoreListSelectViewData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CApplyFilterAsync_003Ed__17 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public NewArrivalMusicScoreListSelectModel _003C_003E4__this;

			public int tabIndex;

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
		private struct _003CCreateMusicScoreDataArrayAsync_003Ed__23 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public NewArrivalMusicScoreListSelectModel _003C_003E4__this;

			private MusicDifficulty _003Ckey_003E5__2;

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
		private struct _003CSetupAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public NewArrivalMusicScoreListSelectModel _003C_003E4__this;

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

		private readonly Dictionary<MusicDifficulty, MusicScoreData[]> _musicScoreDataCache;

		private readonly Queue<MusicDifficulty> _fifoKeys;

		private const int MAX_CACHE_SIZE = 7;

		private readonly MusicScoreFilterEvaluator _filterEvaluator;

		private MusicScoreFilterData _musicScoreFilterData;

		private SortOrderBy _sortOrder;

		public MusicDifficulty SelectedMusicDifficulty
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

		public MusicScoreFilterData MusicScoreFilterData
		{
			get
			{
				throw null;
			}
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__15))]
		public UniTask SetupAsync(CancellationToken ct)
		{
			throw null;
		}

		public void ApplySelectedMusicScore(int index)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CApplyFilterAsync_003Ed__17))]
		public UniTask ApplyFilterAsync(int tabIndex, CancellationToken ct)
		{
			throw null;
		}

		public void ApplyFilter(MusicScoreFilterData result)
		{
			throw null;
		}

		public void ApplySortOrder(SortOrderBy sortOrder)
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

		[AsyncStateMachine(typeof(_003CCreateMusicScoreDataArrayAsync_003Ed__23))]
		private UniTask CreateMusicScoreDataArrayAsync(CancellationToken ct)
		{
			throw null;
		}

		private void AddToCache(MusicDifficulty key, MusicScoreData[] data)
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

		private void ApplySelectedIndexToViewData()
		{
			throw null;
		}

		private void ApplySelectedMusicScore()
		{
			throw null;
		}

		public bool IsCachedFilter(int tabIndex)
		{
			throw null;
		}

		private void CreateFilterTabs()
		{
			throw null;
		}

		public NewArrivalMusicScoreListSelectModel()
		{
			throw null;
		}
	}
}
