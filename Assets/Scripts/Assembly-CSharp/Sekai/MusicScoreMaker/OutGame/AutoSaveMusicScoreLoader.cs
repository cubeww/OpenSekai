using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.Common;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class AutoSaveMusicScoreLoader
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CLoadAllAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<AutoSaveMusicScoreLoadResult[]> _003C_003Et__builder;

			public AutoSaveMusicScoreLoader _003C_003E4__this;

			public int maxCount;

			public CancellationToken ct;

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

		public (string, DateTime)[] GetAutoSaveFileInfos()
		{
			return MusicScoreMakerRepository.GetFileInfos("autosave_*");
		}

		[AsyncStateMachine(typeof(_003CLoadAllAsync_003Ed__1))]
		public UniTask<AutoSaveMusicScoreLoadResult[]> LoadAllAsync(int maxCount, CancellationToken ct)
		{
			return LoadAllCoreAsync(maxCount, ct);
		}

		public AutoSaveMusicScoreLoader()
		{
		}

		private async UniTask<AutoSaveMusicScoreLoadResult[]> LoadAllCoreAsync(int maxCount, CancellationToken ct)
		{
			(string fileName, DateTime lastWriteTime)[] infos = GetAutoSaveFileInfos();
			if (maxCount > 0 && infos.Length > maxCount)
			{
				Array.Resize(ref infos, maxCount);
			}

			AutoSaveMusicScoreLoadResult[] results = new AutoSaveMusicScoreLoadResult[infos.Length];
			for (int i = 0; i < infos.Length; i++)
			{
				ct.ThrowIfCancellationRequested();
				(string fileName, DateTime lastWriteTime) = infos[i];
				MusicScoreMakerRepository repository = MusicScoreMakerRepository.LoadFromStorage(fileName);
				results[i] = new AutoSaveMusicScoreLoadResult(
					fileName,
					lastWriteTime,
					repository.MusicScoreMakerData,
					repository.BaseMusicScoreId,
					repository.BaseMusicDifficultyId);
				await UniTask.Yield(ct);
			}

			return results;
		}
	}
}
