using System;
using UnityEngine;

namespace Sekai.Extensions
{
	public static class GameObjectExtensions
	{
		public static void SetLayer(this GameObject gameObject, int layer, bool needSetChildren = true)
		{
			if (gameObject == null)
			{
				return;
			}

			gameObject.layer = layer;
			if (!needSetChildren)
			{
				return;
			}

			foreach (Transform child in gameObject.transform)
			{
				child.gameObject.SetLayer(layer, true);
			}
		}

		public static T GetComponentOrAdd<T>(this GameObject obj) where T : Component
		{
			return obj == null ? null : obj.GetComponent<T>() ?? obj.AddComponent<T>();
		}

		public static T GetComponentOrAdd<T>(this GameObject obj, Type type) where T : Component
		{
			if (obj == null || type == null)
			{
				return null;
			}

			return obj.GetComponent(type) as T ?? obj.AddComponent(type) as T;
		}

		public static void RemoveComponentIfExists<T>(this GameObject obj) where T : Component
		{
			if (obj == null)
			{
				return;
			}

			var component = obj.GetComponent<T>();
			if (component == null)
			{
				return;
			}

			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(component);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(component);
			}
		}
	}
}
