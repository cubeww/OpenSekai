using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.OutGame
{
	public static class MusicScoreMakerUtility
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public bool isConfirmed;

			public bool isFinished;

			public _003C_003Ec__DisplayClass7_0()
			{
				throw null;
			}

			internal void _003CConfirmMusicScoreDeleteAsync_003Eb__0()
			{
				throw null;
			}

			internal void _003CConfirmMusicScoreDeleteAsync_003Eb__1()
			{
				throw null;
			}

			internal bool _003CConfirmMusicScoreDeleteAsync_003Eb__2()
			{
				throw null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CConfirmMusicScoreDeleteAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public CancellationToken ct;

			private _003C_003Ec__DisplayClass7_0 _003C_003E8__1;

			private Common2ButtonDialog _003Cdialog_003E5__2;

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
		private struct _003CShowSaveDraftCompletedAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public UserCustomMusicScoreDraft userDraftData;

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

		public static bool IsValidMusicForCreate([NotNull] MasterMusicAllModel target)
		{
			throw null;
		}

		public static bool IsValidMusicForCreate([NotNull] MasterMusicAll target)
		{
			throw null;
		}

		public static string CreateCompressedMusicScoreData([NotNull] MusicScoreMakerData musicScoreMakerData)
		{
			throw null;
		}

		public static LiveTransitioner RequestTransitionToInGame(int musicId, MenuScreenType fromScreenType)
		{
			throw null;
		}

		public static LiveTransitioner RequestTransitionToInGame(int musicId, MusicDifficulty difficulty, MenuScreenType fromScreenType)
		{
			throw null;
		}

		public static LiveTransitioner RequestTransitionToInGame([CanBeNull] string baseMusicScoreId, [NotNull] MusicScoreMakerData musicScoreMakerData, MenuScreenType fromScreenType, int baseMusicDifficultyId = -1, int lastSavedDraftSlotNo = -1, [CanBeNull] UserCustomMusicScoreDraft lastSavedDraft = null)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowSaveDraftCompletedAsync_003Ed__6))]
		public static UniTask ShowSaveDraftCompletedAsync([NotNull] UserCustomMusicScoreDraft userDraftData, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CConfirmMusicScoreDeleteAsync_003Ed__7))]
		public static UniTask<bool> ConfirmMusicScoreDeleteAsync(CancellationToken ct)
		{
			throw null;
		}

		public static void ShowMusicScoreDeleteCompleted()
		{
			throw null;
		}

		public static void ShowMusicScoreDeletedError(string wordingKey = "MSG_MUSIC_SCORE_DELETED")
		{
			throw null;
		}

		public static void ShowMusicScoreDeletedError(string wordingKey, Action onClickOK)
		{
			throw null;
		}

		public static void RemoveDeletedMusicScoreFromLocalCache(string userCustomMusicScoreId)
		{
			throw null;
		}

		public static void CopyCustomMusicScoreIdToClipboard(string customMusicScoreId)
		{
			throw null;
		}

		public static bool IsPublishedMusicScoreCountLimitReached()
		{
			throw null;
		}

		public static MusicScoreData[] SyncBookmarkStatus([NotNull] MusicScoreData[] dataArray)
		{
			throw null;
		}

		public static bool IsBookmarkCountLimitReached()
		{
			throw null;
		}

		public static void ExecuteWithPublishLimitConfirmation([NotNull] Action onConfirmed)
		{
			throw null;
		}

		public static UserCustomMusicScorePublishedResponse ConvertToUserCustomMusicScorePublishedResponse([NotNull] MusicScoreData musicScoreData)
		{
			throw null;
		}

		public static void TransitionForPlay([NotNull] MusicScoreData musicScoreData, MenuScreenType? fromScreenType = null)
		{
			throw null;
		}

		public static Dictionary<int, MusicSelectMusicCustomScoreData> CreateMusicCustomScoreDataDic()
		{
			throw null;
		}

		public static bool IsMusicScoreInfoReturnScreen(MenuScreenType? screenType)
		{
			throw null;
		}

		public static bool TryNavigateToMusicScoreInfo(MenuScreenType? returnScreenType, string customMusicScoreId)
		{
			throw null;
		}

		private static LiveTransitioner RequestTransitionToInGame(int musicId, MusicDifficulty difficulty, MusicScoreMakerData musicScoreMakerData, MenuScreenType fromScreenType)
		{
			throw null;
		}

		private static LiveTransitioner RequestTransitionToInGame(int musicId, MusicDifficulty difficulty, [CanBeNull] string baseMusicScoreId, int baseMusicDifficultyId, MusicScoreMakerData musicScoreMakerData, MenuScreenType fromScreenType, int lastSavedDraftSlotNo = -1, [CanBeNull] UserCustomMusicScoreDraft lastSavedDraft = null)
		{
			throw null;
		}

		private static void SetupBootDataAndRequestScene(int musicId, MusicDifficulty difficulty, [CanBeNull] string baseMusicScoreId, int baseMusicDifficultyId, MusicScoreMakerData musicScoreMakerData, MenuScreenType fromScreenType, int lastSavedDraftSlotNo, [CanBeNull] UserCustomMusicScoreDraft lastSavedDraft)
		{
			throw null;
		}
	}
}
