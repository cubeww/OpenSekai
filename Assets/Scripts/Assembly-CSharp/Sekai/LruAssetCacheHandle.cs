using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;

namespace Sekai
{
	public readonly struct LruAssetCacheHandle<TAsset> : IDisposable
	{
		private class InternalHandle : IUniTaskSource<Result>, IUniTaskSource
		{
			private static readonly Stack<InternalHandle> internalHandlePool;

			private readonly List<(Action<object> continuation, object state)> continuations;

			private Action<LruAssetCacheLoadingState, TAsset> cachedUniTaskSubscriptionDelegate;

			private Result? currentResult;

			private bool isInContinuationInvocation;

			private bool releaseAfterContinuationInvocationPromised;

			private bool uniTaskSubscribedForCurrentElement;

			private bool continuationInvoked;

			private bool isCanceled;

			public short Version
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

			public ILruAssetCacheElement<TAsset> Element
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public LruAssetCacheLoadingState State
			{
				get
				{
					throw null;
				}
			}

			public static InternalHandle GetNewInternalHandle(ILruAssetCacheElement<TAsset> element)
			{
				throw null;
			}

			public UniTask<Result> GetResultAsync()
			{
				throw null;
			}

			private void OnStateChangedForUniTask(LruAssetCacheLoadingState state, TAsset loadedObject)
			{
				throw null;
			}

			private void InvokeContinuation()
			{
				throw null;
			}

			public void Dispose()
			{
				throw null;
			}

			private void AssertUniTaskToken(short token)
			{
				throw null;
			}

			public UniTaskStatus GetStatus(short token)
			{
				throw null;
			}

			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				throw null;
			}

			Result IUniTaskSource<Result>.GetResult(short token)
			{
				throw null;
			}

			void IUniTaskSource.GetResult(short token)
			{
				throw null;
			}

			public UniTaskStatus UnsafeGetStatus()
			{
				throw null;
			}

			public InternalHandle()
			{
			}

			static InternalHandle()
			{
				}
		}

		public struct Result : IEquatable<Result>
		{
			public TAsset loadedObjectWhenSucceeded;

			public bool isError;

			public Result(TAsset loadedObjectWhenSucceeded, bool isError)
			{
				throw null;
			}

			public bool Equals(Result other)
			{
				throw null;
			}

			public override bool Equals(object obj)
			{
				throw null;
			}

			public override int GetHashCode()
			{
				throw null;
			}
		}

		private readonly short version;

		private readonly InternalHandle internalHandle;

		public LruAssetCacheLoadingState State
		{
			get
			{
				throw null;
			}
		}

		public LruAssetCacheHandle(ILruAssetCacheElement<TAsset> element)
		{
			throw null;
		}

		public bool IsValid()
		{
			throw null;
		}

		private void AssertValid()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public UniTask<Result> GetResultAsync()
		{
			throw null;
		}
	}
}
