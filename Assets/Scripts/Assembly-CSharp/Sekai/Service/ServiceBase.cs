using System.Runtime.CompilerServices;

namespace Sekai.Service
{
	public abstract class ServiceBase
	{
		protected enum Status
		{
			None = 1,
			Success = 2,
			Error = 3
		}

		public const int PURCHASE_LIMIT_OVER_CODE = 429;

		public const int HTTP_STATUS_CODE_RULE_AGREEMENT = 406;

		protected Status status;

		protected int httpStatusCode
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		protected void Check()
		{
			throw null;
		}

		protected ServiceBase()
		{
		}
	}
}
