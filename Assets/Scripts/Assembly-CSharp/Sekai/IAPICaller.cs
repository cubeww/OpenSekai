namespace Sekai
{
	public interface IAPICaller<A, B>
	{
		string Execute(APICore<A, B>.OnAPIEventHandler onCallBackResponse);

		string ExecuteFirst(APICore<A, B>.OnAPIEventHandler onCallBackResponse);

		void AddCallback(APICore<A, B>.OnAPIEventHandler onCallBackResponse);

		void AddExecuteBehaviour(APIExecuteBehaviourParam param);

		void ClearCallBack();
	}
}
