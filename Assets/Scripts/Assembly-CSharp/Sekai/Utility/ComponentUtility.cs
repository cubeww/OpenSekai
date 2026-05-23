using System.Reflection;
using UnityEngine;

namespace Sekai.Utility
{
	public static class ComponentUtility
	{
		public static T CopyFrom<T>(this T self, T other) where T : Component
		{
			if (self == null || other == null)
			{
				return self;
			}

			var type = other.GetType();
			foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!field.IsInitOnly)
				{
					field.SetValue(self, field.GetValue(other));
				}
			}

			return self;
		}

		public static T AddComponentCopyFrom<T>(this GameObject self, T other) where T : Component
		{
			if (self == null || other == null)
			{
				return null;
			}

			return self.AddComponent<T>().CopyFrom(other);
		}

		public static T GetOrAddComponentCopyFrom<T>(this GameObject self, T other) where T : Component
		{
			if (self == null)
			{
				return null;
			}

			var component = self.GetComponent<T>() ?? self.AddComponent<T>();
			return other != null ? component.CopyFrom(other) : component;
		}
	}
}
