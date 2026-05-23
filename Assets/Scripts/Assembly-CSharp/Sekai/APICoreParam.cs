using System.Collections;
using System.Collections.Generic;

namespace Sekai
{
	public class APICoreParam
	{
		public delegate object ApiErrorHandlingMessageChangeHandler(object httpInfo, object clientErrorInfo);

		public enum Method
		{
			GET = 0,
			POST = 1,
			PUT = 2,
			PATCH = 3,
			DELETE = 4
		}

		public enum AfterErrorDetectionType
		{
			Ok = 0,
			RetryCancel = 1,
			Through = 2,
			SubWindow = 3
		}

		public enum AfterModuleMaintenanceType
		{
			Normal = 0,
			SubWindow = 1
		}

		public enum AfterInterruptionType
		{
			Stay = 0,
			Restart = 1
		}

		private static uint apiIndex;

		public string ApiID { get; protected set; }
		public string Name { get; protected set; }
		public Method CallMethod { get; protected set; }
		public bool IsRequireTokenAPI { get; protected set; }
		public IEnumerator RequestCoroutineID { get; protected set; }
		public bool IsUnarchive { get; set; }
		public bool IsStub { get; set; }
		public APIExecuteBehaviourParam ExecuteBehaviourParam { get; set; }
		public Dictionary<string, string> OptionalRequestHeader { get; set; } = new Dictionary<string, string>();
		public ApiErrorHandlingMessageChangeHandler ErrorHandlingMessageChange { get; set; }
		public AfterModuleMaintenanceType ModuleMaintenanceType { get; set; }

		public APICoreParam()
		{
			ApiID = (++apiIndex).ToString();
			Name = GetType().Name;
		}
	}
}
