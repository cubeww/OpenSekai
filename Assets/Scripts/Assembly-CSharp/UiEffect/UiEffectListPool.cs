using System.Collections.Generic;

namespace UiEffect
{
	public static class UiEffectListPool<T>
	{
		private static readonly UiEffectObjectPool<List<T>> s_ListPool;

		public static List<T> Get()
		{
			throw null;
		}

		public static void Release(List<T> toRelease)
		{
			throw null;
		}

		static UiEffectListPool()
		{
			throw null;
		}
	}
}
