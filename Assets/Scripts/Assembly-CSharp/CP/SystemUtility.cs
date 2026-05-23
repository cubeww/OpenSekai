using System;
using System.Collections;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace CP
{
	public static class SystemUtility
	{
		public static void GCCollect(int iteration = 8)
		{
			for (var i = 0; i < Math.Max(1, iteration); i++)
			{
				GC.Collect();
			}
		}

		public static void SafeCallback(Action action)
		{
			action?.Invoke();
		}

		public static void SafeCallback<T>(Action<T> action, T param1)
		{
			action?.Invoke(param1);
		}

		public static void SafeCallback<T, T2>(Action<T, T2> action, T param1, T2 param2)
		{
			action?.Invoke(param1, param2);
		}

		public static void SafeCallback<T, T2, T3>(Action<T, T2, T3> action, T param1, T2 param2, T3 param3)
		{
			action?.Invoke(param1, param2, param3);
		}

		public static void SafeCallback<T, T2, T3, T4>(Action<T, T2, T3, T4> action, T param1, T2 param2, T3 param3, T4 param4)
		{
			action?.Invoke(param1, param2, param3, param4);
		}

		public static T2 SafeCallback<T, T2>(Func<T, T2> action, T param1)
		{
			return action != null ? action(param1) : default;
		}

		public static Type GetTypeByClassName(string className)
		{
			if (string.IsNullOrEmpty(className))
			{
				return null;
			}

			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				var type = assembly.GetType(className);
				if (type != null)
				{
					return type;
				}
			}

			return null;
		}

		public static void StopCoroutine(MonoBehaviour executor, IEnumerator coroutine)
		{
			if (executor != null && coroutine != null)
			{
				executor.StopCoroutine(coroutine);
			}
		}

		public static void StopRefCoroutine(MonoBehaviour executor, ref IEnumerator coroutine)
		{
			StopCoroutine(executor, coroutine);
			coroutine = null;
		}

		public static IEnumerator WaitForSeconds(float duration)
		{
			yield return new UnityEngine.WaitForSeconds(duration);
		}

		public static IEnumerator DelayCall(MonoBehaviour executer, Action callback, float delay)
		{
			var coroutine = DelayCallCore(callback, delay);
			if (executer != null)
			{
				executer.StartCoroutine(coroutine);
			}

			return coroutine;
		}

		private static IEnumerator DelayCallCore(Action callback, float delay)
		{
			if (delay > 0f)
			{
				yield return new UnityEngine.WaitForSeconds(delay);
			}

			callback?.Invoke();
		}

		public static void DelayInboke(float delay, Action callback)
		{
			// TODO(original): the game schedules this through a shared coroutine runner.
			callback?.Invoke();
		}

		public static void DelayInboke(int frameCount, Action callback)
		{
			// TODO(original): restore frame-delayed invocation when the global runner is copied.
			callback?.Invoke();
		}

		private static IEnumerator WaitForSecondsCore(float delay, Action callback)
		{
			if (delay > 0f)
			{
				yield return new UnityEngine.WaitForSeconds(delay);
			}

			callback?.Invoke();
		}

		private static IEnumerator WaitForSecondsCore(int frameCount, Action callback)
		{
			for (var i = 0; i < frameCount; i++)
			{
				yield return null;
			}

			callback?.Invoke();
		}

		public static void InvertedBytes(ref byte[] bytes, int bufferSize)
		{
			if (bytes == null)
			{
				return;
			}

			Array.Reverse(bytes, 0, Math.Min(bufferSize, bytes.Length));
		}

		public static void InvertedBytesAB(ref byte[] bytes, int bufferSize, int offset = 0)
		{
			if (bytes == null || offset < 0 || offset >= bytes.Length)
			{
				return;
			}

			Array.Reverse(bytes, offset, Math.Min(bufferSize, bytes.Length - offset));
		}

		public static void ApplicationQuit()
		{
			Application.Quit();
		}

		public static string ToDebugString<T>(this T target, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
		{
			if (target == null)
			{
				return "null";
			}

			var builder = new StringBuilder();
			var type = target.GetType();
			builder.Append(type.Name).Append(" { ");
			foreach (var field in type.GetFields(bindingFlags))
			{
				builder.Append(field.Name).Append('=').Append(field.GetValue(target)).Append(", ");
			}

			builder.Append('}');
			return builder.ToString();
		}
	}
}
