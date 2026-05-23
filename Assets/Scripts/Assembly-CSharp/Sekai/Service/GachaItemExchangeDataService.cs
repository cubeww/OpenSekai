using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CP;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;

namespace Sekai.Service
{
	public class GachaItemExchangeDataService : ServiceBase
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExchange_003Ed__4 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public GachaItemExchangeDataService _003C_003E4__this;

			public UserGachaCeilExchangeRequest request;

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

		public APICore<UserGachaCeilExchangeRequest, UserGachaCeilExchangeResponse> Param
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

		[AsyncStateMachine(typeof(_003CExchange_003Ed__4))]
		public UniTask Exchange(UserGachaCeilExchangeRequest request)
		{
			throw null;
		}

		private void OnFinishExchangeAPI(APICore<UserGachaCeilExchangeRequest, UserGachaCeilExchangeResponse> param)
		{
			throw null;
		}

		private APIErrorHandleActionAssign ErrorHandledExchange(UnityWebRequestClient httpInfo, ClientErrorInfomation clientErrorInfo)
		{
			throw null;
		}

		public GachaItemExchangeDataService()
		{
		}
	}
}
