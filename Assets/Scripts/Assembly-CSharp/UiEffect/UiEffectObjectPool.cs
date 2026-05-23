using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Events;

namespace UiEffect
{
	public class UiEffectObjectPool<T> where T : new()
	{
		private readonly Stack<T> m_Stack;

		private readonly UnityAction<T> m_ActionOnGet;

		private readonly UnityAction<T> m_ActionOnRelease;

		public int countAll
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

		public int countActive
		{
			get
			{
				throw null;
			}
		}

		public int countInactive
		{
			get
			{
				throw null;
			}
		}

		public UiEffectObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			throw null;
		}

		public T Get()
		{
			throw null;
		}

		public void Release(T element)
		{
			throw null;
		}
	}
}
