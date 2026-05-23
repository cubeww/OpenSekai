using System.Runtime.CompilerServices;

namespace Sekai
{
	public class APIResult
	{
		public APIState State
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ClientErrorInfomation ClientErrorInfo
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public APIResult(APIState state, ClientErrorResponse errorResponse)
		{
			State = state;
			ClientErrorInfo = errorResponse == null ? null : new ClientErrorInfomation { Response = errorResponse };
		}
	}
}
