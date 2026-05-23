using System.Collections.Generic;

namespace Sekai
{
	public abstract class APICaller<A, B> : IAPICaller<A, B>
	{
		protected static readonly EmptyRequest RequestEmpty;

		protected APICore<A, B>.OnAPIEventHandler onFinishAPI;

		protected APIExecuteBehaviourParam executeBehaviourParam;

		protected string Call(string api, APICoreParam.Method method, A request, APICore<A, B>.OnAPIEventHandler onCallBackResponse, bool isUnarchive = true, Dictionary<string, string> optionalHeader = null)
		{
			throw null;
		}

		protected string CallFirst(string api, APICoreParam.Method method, A request, APICore<A, B>.OnAPIEventHandler onCallBackResponse, bool isUnarchive = true, Dictionary<string, string> optionalHeader = null)
		{
			throw null;
		}

		protected string CallFullURL(string api, APICoreParam.Method method, A request, APICore<A, B>.OnAPIEventHandler onCallBackResponse, APICore<A, B>.OnAPIEventHandler onPreProcess = null, bool isUnarchive = true, Dictionary<string, string> headers = null)
		{
			throw null;
		}

		protected string StubCall(string api, APICoreParam.Method method, A request, APICore<A, B>.OnAPIEventHandler onCallBackResponse, bool isUnarchive = true)
		{
			throw null;
		}

		protected string StubCallFullPath(string api, APICoreParam.Method method, A request, APICore<A, B>.OnAPIEventHandler onCallBackResponse, bool isUnarchive = true)
		{
			throw null;
		}

		public abstract string Execute(APICore<A, B>.OnAPIEventHandler onCallBackResponse);

		public virtual string ExecuteFirst(APICore<A, B>.OnAPIEventHandler onCallBackResponse)
		{
			throw null;
		}

		public virtual void AddCallback(APICore<A, B>.OnAPIEventHandler onCallBackResponse)
		{
			throw null;
		}

		public virtual void AddExecuteBehaviour(APIExecuteBehaviourParam param)
		{
			throw null;
		}

		public virtual void ClearCallBack()
		{
			throw null;
		}

		protected APICaller()
		{
		}

		static APICaller()
		{
			}
	}
}
