using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;

namespace Sekai
{
	public static class LruAssetCache
	{
		public static LruAssetCacheGeneral<UnityEngine.Object> MusicJacket
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

		static LruAssetCache()
		{
			}

		public static void InitializeAll()
		{
			throw null;
		}
	}
	public abstract class LruAssetCache<TKey, TAsset> : IDisposable
	{
		private class Element : ILruAssetCacheElement<TAsset>
		{
			[StructLayout((LayoutKind)3)]
			[CompilerGenerated]
			private struct _003CUpdateLoad_003Ed__35 : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

				public Element _003C_003E4__this;

				private UniTask<TAsset>.Awaiter _003C_003Eu__1;

				private UniTask.Awaiter _003C_003Eu__2;

				private void MoveNext()
				{
					throw null;
				}

				void IAsyncStateMachine.MoveNext()
				{
					//ILSpy generated this explicit interface implementation from .override directive in MoveNext
					this.MoveNext();
				}

				[DebuggerHidden]
				private void SetStateMachine(IAsyncStateMachine stateMachine)
				{
					throw null;
				}

				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
					//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
					this.SetStateMachine(stateMachine);
				}
			}

			private bool isLoadUpdating;

			private LruAssetCacheLoadingState loadingState;

			private int referenceCount;

			private bool toBeLoaded;

			private LruAssetCache<TKey, TAsset> Cache
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			private TKey Key
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public Element NewerPendingUnload
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

			public Element OlderPendingUnload
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

			public TAsset LoadedAsset
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

			public LruAssetCacheLoadingState LoadingState
			{
				get
				{
					throw null;
				}
				private set
				{
					throw null;
				}
			}

			public event Action<LruAssetCacheLoadingState, TAsset> LoadingStateChanged
			{
				[CompilerGenerated]
				add
				{
					throw null;
				}
				[CompilerGenerated]
				remove
				{
					throw null;
				}
			}

			public Element(LruAssetCache<TKey, TAsset> cache, TKey key)
			{
				throw null;
			}

			public void DecrementReference()
			{
				throw null;
			}

			private void RemoveFromPendingUnloadList()
			{
				throw null;
			}

			private void SetAsNewestPendingUnload()
			{
				throw null;
			}

			public void IncrementReference()
			{
				throw null;
			}

			private void Load()
			{
				throw null;
			}

			public void Unload()
			{
				throw null;
			}

			[AsyncStateMachine(typeof(LruAssetCache<, >.Element._003CUpdateLoad_003Ed__35))]
			private UniTaskVoid UpdateLoad()
			{
				throw null;
			}
		}

		private readonly Dictionary<TKey, Element> elements;

		private int maxCachedAssetCount;

		private int unusedLoadedElementCount;

		private int usedElementCount;

		public string LogName
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		protected bool IsDisposed
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

		private Element NewestPendingUnload
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

		private Element OldestPendingUnload
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

		public int MaxCachedAssetCount
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		protected LruAssetCache(int maxCachedAssetCount, string logName)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public void Clear()
		{
			throw null;
		}

		private void UnloadExceededCache()
		{
			throw null;
		}

		public LruAssetCacheHandle<TAsset> GetHandle(TKey key)
		{
			throw null;
		}

		protected abstract UniTask<TAsset> LoadAsync(TKey key);

		protected abstract UniTask UnloadAsync(TKey key);
	}
}
