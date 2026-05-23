using System.Runtime.CompilerServices;

namespace CP
{
	public class HttpResult
	{
		public enum Status
		{
			None = 0,
			Informational = 1,
			Success = 2,
			Redirection = 3,
			ClientError = 4,
			ServerError = 5,
			Timeout = 6,
			ConnectionError = 7,
			Exception = 8,
			WebException = 9,
			Interrupted = 10
		}

		public Status HttpStatus
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

		public int StatusCode
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

		public string StatusMessage
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

		public string ErrorMessage
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

		public HttpResult()
		{
			}

		public void SetHttpStatus(int statusCode, string statusMessage)
		{
			throw null;
		}

		public void SetResult(Status status, int statusCode, string statusMessage, string errorMessage)
		{
			throw null;
		}

		public void SetStatus(Status status)
		{
			throw null;
		}

		public void SetErrorMessage(string message)
		{
			throw null;
		}
	}
}
