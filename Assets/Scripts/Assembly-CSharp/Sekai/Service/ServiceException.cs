using System;

namespace Sekai.Service
{
	public class ServiceException : Exception
	{
		public int HttpStatusCode;

		public ServiceException(string message)
		{
			throw null;
		}

		public ServiceException(int httpStatusCode, string message)
		{
			throw null;
		}
	}
}
