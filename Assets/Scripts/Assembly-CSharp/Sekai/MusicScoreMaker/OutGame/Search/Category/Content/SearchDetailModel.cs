using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public class SearchDetailModel : ContentModelBase<SearchDetailViewData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSearchMusicScoreByIdAsync_003Ed__24 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<MusicScoreData> _003C_003Et__builder;

			public string musicScoreId;

			private UniTask<Sekai.ApiData.UserCustomMusicScorePublishedResponse>.Awaiter _003C_003Eu__1;

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

		private readonly MusicFilterModel _musicFilterModel;

		public int SelectedMusicId
		{
			get
			{
				throw null;
			}
		}

		public MusicDifficulty SettingMusicDifficulty
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

		public int SelectedTagId
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

		public SearchDetailDefines.SortCategoryType SelectedSortCategory
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

		public void Setup()
		{
			throw null;
		}

		public IEnumerable<int> GetMusicIdsByWord(string word)
		{
			throw null;
		}

		public void ApplyMusicFilterFreeWord(string word)
		{
			throw null;
		}

		public void ApplyFilteredMusicElement(int index)
		{
			throw null;
		}

		public void ClearMusicFilter()
		{
			throw null;
		}

		public void ApplyFilteredTag(int tagId)
		{
			throw null;
		}

		public void ClearFilteredTag()
		{
			throw null;
		}

		public void SetMusicDifficulty(MusicDifficulty musicDifficulty)
		{
			throw null;
		}

		public void ApplySortCategory(int index)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSearchMusicScoreByIdAsync_003Ed__24))]
		[ItemCanBeNull]
		public UniTask<MusicScoreData> SearchMusicScoreByIdAsync(string musicScoreId)
		{
			throw null;
		}

		public SearchDetailModel()
		{
			throw null;
		}
	}
}
