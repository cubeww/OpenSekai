using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common.Content;

namespace Sekai.MusicScoreMaker.OutGame.Created.Content
{
	public sealed class AutoSaveMusicScoreListSelectModel : ContentModelBase<AutoSaveMusicScoreListSelectViewData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSetupAsync_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public AutoSaveMusicScoreListSelectModel _003C_003E4__this;

			public CancellationToken ct;

			private UniTask<AutoSaveMusicScoreLoadResult[]>.Awaiter _003C_003Eu__1;

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

		private int _selectedSlotNumber;

		private readonly AutoSaveMusicScoreLoader _scoreLoader;

		private AutoSaveMusicScoreLoadResult[] _loadResults;

		public bool IsSelected
		{
			get
			{
				throw null;
			}
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__5))]
		public UniTask SetupAsync(CancellationToken ct)
		{
			throw null;
		}

		public void ApplySelectedSlot(int slotNumber)
		{
			throw null;
		}

		[CanBeNull]
		public AutoSaveMusicScoreLoadResult GetLoadResultBySelected(CancellationToken ct)
		{
			throw null;
		}

		private void CreateSlotCellDataArray(AutoSaveMusicScoreLoadResult[] loadResults)
		{
			throw null;
		}

		public AutoSaveMusicScoreListSelectModel()
		{
			throw null;
		}
	}
}
