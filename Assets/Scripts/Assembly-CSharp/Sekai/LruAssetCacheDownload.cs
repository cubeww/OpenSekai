using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;

namespace Sekai
{
	public class LruAssetCacheDownload<TAsset> : LruAssetCacheGeneral<TAsset> where TAsset : Object
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1_0
		{
			public AssetBundleLoader loader;

			public _003C_003Ec__DisplayClass1_0()
			{
			}

			internal bool _003CLoadAsync_003Eb__0()
			{
				throw null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CLoadAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<TAsset> _003C_003Et__builder;

			public (string assetBundleName, string fileName) key;

			private _003C_003Ec__DisplayClass1_0 _003C_003E8__1;

			private UniTask.Awaiter _003C_003Eu__1;

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

		public LruAssetCacheDownload(int maxCachedAssetCount, string logName)
			: base(maxCachedAssetCount, logName)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(LruAssetCacheDownload<>._003CLoadAsync_003Ed__1))]
		protected override UniTask<TAsset> LoadAsync((string assetBundleName, string fileName) key)
		{
			throw null;
		}

		protected override UniTask UnloadAsync((string assetBundleName, string fileName) key)
		{
			throw null;
		}
	}
}
