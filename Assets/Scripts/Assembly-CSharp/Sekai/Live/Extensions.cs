using UnityEngine;

namespace Sekai.Live
{
	public static class Extensions
	{
		public static Transform FindDeep(this Transform self, string name, bool includeInactive = false)
		{
			if (self == null)
			{
				return null;
			}

			foreach (Transform child in self)
			{
				if ((includeInactive || child.gameObject.activeInHierarchy) && child.name == name)
				{
					return child;
				}

				var found = child.FindDeep(name, includeInactive);
				if (found != null)
				{
					return found;
				}
			}

			return null;
		}
	}
}
