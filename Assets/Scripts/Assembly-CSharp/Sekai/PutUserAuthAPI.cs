namespace Sekai
{
	public class PutUserAuthAPI : IAPICaller<object, object>
	{
		private readonly APICore<object, object> core = new APICore<object, object>();

		public PutUserAuthAPI(string credential, string deviceId, string triggerType, bool forceUpdate)
		{
			// TODO(original): store UserAuthRequest once API DTOs are copied.
		}

		public string Execute(APICore<object, object>.OnAPIEventHandler onCallBackResponse)
		{
			onCallBackResponse?.Invoke(core);
			return core.ApiID;
		}

		public string ExecuteFirst(APICore<object, object>.OnAPIEventHandler onCallBackResponse)
		{
			return Execute(onCallBackResponse);
		}

		public void AddCallback(APICore<object, object>.OnAPIEventHandler onCallBackResponse)
		{
			core.AddCallback(onCallBackResponse);
		}

		public void AddExecuteBehaviour(APIExecuteBehaviourParam param)
		{
			core.ExecuteBehaviourParam = param;
		}

		public void ClearCallBack()
		{
			core.ClearCallBack();
		}
	}
}
