using System;
using System.Runtime.CompilerServices;
using System.Threading;
using DG.Tweening;

namespace Sekai
{
	public struct TweenAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		private Tween tween;

		private CancellationToken cancellationToken;

		public bool IsCompleted
		{
			get
			{
				throw null;
			}
		}

		public TweenAwaiter(Tween tween, CancellationToken cancellationToken)
		{
			throw null;
		}

		public void GetResult()
		{
			throw null;
		}

		public void OnCompleted(Action continuation)
		{
			throw null;
		}

		public void UnsafeOnCompleted(Action continuation)
		{
			throw null;
		}

		public TweenAwaiter GetAwaiter()
		{
			throw null;
		}
	}
}
