using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.OutGame.Created.Content;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public class ScreenLayerMusicScoreMakerSaveDraftModel
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCreateDataAsync_003Ed__26 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSaveDraftModel _003C_003E4__this;

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

		public readonly SaveDraftScreenViewData ViewData;

		private readonly SaveDraftMusicScoreListSelectModel _listSelectModel;

		public int SelectedSlotNo
		{
			get
			{
				throw null;
			}
		}

		public bool IsSelectedSlotValid
		{
			get
			{
				throw null;
			}
		}

		public bool IsSelectedSavedSlot
		{
			get
			{
				throw null;
			}
		}

		public MusicScoreMakerData MusicScoreMakerData
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

		public bool IsExitOnSave
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

		[CanBeNull]
		public string BaseMusicScoreId
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

		public int BaseMusicDifficultyId
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

		public ScreenLayerMusicScoreMakerSaveDraftModel()
		{
			throw null;
		}

		public void Setup([NotNull] MusicScoreMakerData musicScoreMakerData, bool isExitOnSave, [CanBeNull] string baseMusicScoreId, int baseMusicDifficultyId = -1)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CCreateDataAsync_003Ed__26))]
		public UniTask CreateDataAsync()
		{
			throw null;
		}

		public void ApplySlotData([CanBeNull] IEnumerable<UserCustomMusicScoreDraft> slotData)
		{
			throw null;
		}

		public void ApplySelectedSlot(int slotNumber)
		{
			throw null;
		}

		[CanBeNull]
		public UserCustomMusicScoreDraft GetSelectedMusicScoreDraft()
		{
			throw null;
		}

		public void ResetSelectedSlot()
		{
			throw null;
		}
	}
}
