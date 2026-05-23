using Newtonsoft.Json;

namespace Sekai.MusicScoreMaker.Ingame.Utilities
{
	public static class DeepCopyHelper
	{
		private static readonly JsonSerializerSettings Settings;

		public static T DeepCopy<T>(T obj)
		{
			if (obj == null)
			{
				return default;
			}
			return FromJson<T>(ToJsonString(obj));
		}

		public static T FromJson<T>(string json)
		{
			return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json, Settings);
		}

		public static string ToJsonString<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj, Settings);
		}

		public static bool IsEqual<T>(T a, T b)
		{
			return string.Equals(ToJsonString(a), ToJsonString(b));
		}

		static DeepCopyHelper()
		{
			Settings = new JsonSerializerSettings
			{
				PreserveReferencesHandling = PreserveReferencesHandling.Objects,
				TypeNameHandling = TypeNameHandling.Auto,
				ReferenceLoopHandling = ReferenceLoopHandling.Serialize
			};
		}
	}
}
