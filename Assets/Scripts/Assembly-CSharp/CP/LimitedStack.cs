using System;
using System.Collections.Generic;

namespace CP
{
	public class LimitedStack<T>
	{
		private readonly List<T> list;
		private readonly int maxStacks;

		public int Count => list.Count;

		public T this[int index] => list[index];

		public LimitedStack(int maxStacks)
		{
			this.maxStacks = Math.Max(1, maxStacks);
			list = new List<T>(this.maxStacks);
		}

		public void Push(T obj)
		{
			list.Add(obj);
			while (list.Count > maxStacks)
			{
				list.RemoveAt(0);
			}
		}

		public T Pop()
		{
			if (list.Count == 0)
			{
				return default;
			}

			var index = list.Count - 1;
			var value = list[index];
			list.RemoveAt(index);
			return value;
		}

		public T Peek()
		{
			return list.Count > 0 ? list[list.Count - 1] : default;
		}

		public void Clear()
		{
			list.Clear();
		}
	}
}
