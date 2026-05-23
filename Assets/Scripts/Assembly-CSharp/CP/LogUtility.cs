using System;
using UnityEngine;

namespace CP
{
	public enum LogType
	{
		Log = 0,
		Warning = 1,
		Error = 2
	}

	public enum LogFilterType
	{
		None = 0,
		Default = 1
	}

	public enum LogReportType
	{
		WebRequestError = 1,
		Error = 2,
		Info = 3,
		Diarkis = 4
	}

	public interface ILogTimeGetter
	{
		string GetLogWritingTime();
	}

	public interface ILogUtilityIO
	{
		public enum LogType
		{
			Log = 0,
			Warning = 1,
			Error = 2
		}
	}

	public static class LogUtility
	{
		public delegate void OutputLogDelegateFormat(LogFilterType filterType, LogType logType, string formatMessage, object[] args);
		public delegate void OutputLogDelegate(LogFilterType filterType, LogType logType, string formatMessage);
		public delegate void OutputErrorDelegate(string formatMessage, object[] args);
		public delegate void OutputErrorReportDelegate(LogReportType reportType, string formatMessage);

		public static Action<Exception> OnException;
		public static OutputErrorDelegate OnError;
		public static OutputErrorReportDelegate OnErrorReport;
		public static Action<LogType> PlayLogSE;

		public static void Initialize(ILogUtilityIO io, ILogTimeGetter logTimeGetter)
		{
			// TODO(original): restore filter map, file logger and report callbacks from dump.cs.
		}

		public static void WriteLog(int priority, string formatMessage, params object[] args)
		{
			Debug.Log(Format(formatMessage, args));
		}

		public static void WriteLog(LogFilterType filterType, string formatMessage, params object[] args)
		{
			Debug.Log(Format(formatMessage, args));
		}

		public static void Log(string formatMessage, params object[] args)
		{
			Debug.Log(Format(formatMessage, args));
		}

		public static void Log(LogFilterType filterType, string formatMessage, params object[] args)
		{
			Debug.Log(Format(formatMessage, args));
		}

		public static void LogWarning(string formatMessage, params object[] args)
		{
			Debug.LogWarning(Format(formatMessage, args));
		}

		public static void LogError(string formatMessage, params object[] args)
		{
			Debug.LogError(Format(formatMessage, args));
			OnError?.Invoke(formatMessage, args);
		}

		public static void LogError(LogFilterType filterType, string formatMessage, params object[] args)
		{
			LogError(formatMessage, args);
		}

		public static void LogError(LogReportType reportType, string formatMessage, params object[] args)
		{
			Debug.LogError(Format(formatMessage, args));
			OnErrorReport?.Invoke(reportType, Format(formatMessage, args));
		}

		public static void LogException(Exception exception)
		{
			Debug.LogException(exception);
			OnException?.Invoke(exception);
		}

		public static bool IsFilterEnabled(LogFilterType filterType)
		{
			return true;
		}

		private static string Format(string formatMessage, object[] args)
		{
			if (string.IsNullOrEmpty(formatMessage) || args == null || args.Length == 0)
			{
				return formatMessage;
			}

			try
			{
				return string.Format(formatMessage, args);
			}
			catch (FormatException)
			{
				return formatMessage;
			}
		}
	}
}
