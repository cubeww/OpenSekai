using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CP;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;

namespace Sekai.Service
{
	public abstract class MusicScoreSaveDraftServiceBase<TRequest> : ServiceBase
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<UserCustomMusicScoreDraftListResponse> _003C_003Et__builder;

			public MusicScoreSaveDraftServiceBase<TRequest> _003C_003E4__this;

			private APICoreParam _003CapiCoreParam_003E5__2;

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

		protected readonly int _slotNo;

		protected readonly TRequest _request;

		protected UserCustomMusicScoreDraftListResponse _response;

		private APICoreParam.AfterErrorDetectionType _afterErrorDetectionType;

		public bool IsError
		{
			get
			{
				throw null;
			}
		}

		public bool IsBanned
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

		public void SetAfterErrorDetectionType(APICoreParam.AfterErrorDetectionType type)
		{
			throw null;
		}

		protected MusicScoreSaveDraftServiceBase(int slotNo, TRequest request)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(MusicScoreSaveDraftServiceBase<>._003CExecuteAsync_003Ed__12))]
		[ItemCanBeNull]
		public UniTask<UserCustomMusicScoreDraftListResponse> ExecuteAsync()
		{
			throw null;
		}

		protected abstract APICaller<TRequest, UserCustomMusicScoreDraftListResponse> CreateApi(long userId, int slotNo);

		protected virtual void OnFinishAPI(APICore<TRequest, UserCustomMusicScoreDraftListResponse> apiCore)
		{
			throw null;
		}

		protected virtual void OnInterruption()
		{
			throw null;
		}

		private APIErrorHandleActionAssign OverrideAPIErrorChangeHandler(UnityWebRequestClient httpInfo, ClientErrorInfomation clientErrorInfo)
		{
			throw null;
		}
	}
}
