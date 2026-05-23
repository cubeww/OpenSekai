using System;

namespace Sekai
{
	public struct APIExecuteBehaviourParam
	{
		public Action OnInterruption { get; set; }
		public bool ShowIndicator { get; set; }
		public APICoreParam.AfterErrorDetectionType AfterErrorDetaction { get; set; }
		public APICoreParam.AfterInterruptionType AfterInterruption { get; set; }
		public APICoreParam.ApiErrorHandlingMessageChangeHandler ErrorHandlingMessageChange { get; set; }
		public APICoreParam.AfterModuleMaintenanceType ModuleMaintenanceType { get; set; }
		public Action OnRetry { get; set; }
	}
}
