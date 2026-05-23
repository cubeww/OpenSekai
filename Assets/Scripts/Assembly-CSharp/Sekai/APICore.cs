using System.Collections;
using UnityEngine;

namespace Sekai
{
	public class APICore<A, B> : APICoreParam
	{
		public delegate void OnAPIEventHandler(APICore<A, B> apiCore);

		private readonly A request;
		private B response;

		public A Request => request;
		public B Response => response;

		public event OnAPIEventHandler OnAPICompleted;
		public event OnAPIEventHandler OnAPICallBackReponse;
		public event OnAPIEventHandler OnAPIPrepareReponse;

		public APICore()
		{
		}

		public APICore(A request)
		{
			this.request = request;
		}

		public void AddCallback(OnAPIEventHandler callback)
		{
			OnAPICompleted += callback;
		}

		public void ClearCallBack()
		{
			OnAPICompleted = null;
			OnAPICallBackReponse = null;
			OnAPIPrepareReponse = null;
		}

		public IEnumerator SendRequest(MonoBehaviour parentObj, bool isUnarchive = false)
		{
			// TODO(original): restore UnityWebRequest serialization, retry and error handling.
			IsUnarchive = isUnarchive;
			OnAPIPrepareReponse?.Invoke(this);
			OnAPICallBackReponse?.Invoke(this);
			OnAPICompleted?.Invoke(this);
			yield break;
		}

		public void SetResponseForStub(B response)
		{
			this.response = response;
		}
	}
}
