using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class ClientErrorResponse
	{
		[IgnoreMember]
		public const string ERROR_CODE_APPLICATION_UPDATE_REQUIRE = "application";

		[IgnoreMember]
		public const string ERROR_CODE_DATA_UPDATE_REQUIRE = "data";

		[IgnoreMember]
		public const string ERROR_CODE_MASTER_UPDATE_REQUIRE = "master";

		[Key("httpStatus")]
		public uint HttpStatus
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key("errorCode")]
		public string ErrorCode
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key("errorMessage")]
		public string ErrorMessage
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public ClientErrorResponse()
		{
		}
	}
}
