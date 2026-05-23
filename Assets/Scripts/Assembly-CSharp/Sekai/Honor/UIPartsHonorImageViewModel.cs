using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sekai.Honor
{
	public sealed class UIPartsHonorImageViewModel
	{
		[CompilerGenerated]
		private sealed class _003CLoadAssetBundle_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIPartsHonorImageViewModel _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			[DebuggerHidden]
			public _003CLoadAssetBundle_003Ed__11(int _003C_003E1__state)
			{
				throw null;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				throw null;
			}

			private bool MoveNext()
			{
				throw null;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw null;
			}
		}

		private readonly MasterHonor _masterHonor;

		private readonly MasterHonorGroup _masterHonorGroup;

		private readonly uint _honorLevel;

		private IEnumerator _loadFrameLoaderHandle;

		public AssetBundleLoader FrameLoader
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

		public string MasterHonorAssetBundleName
		{
			get
			{
				throw null;
			}
		}

		public UIPartsHonorImageViewModel(MasterHonor masterHonor, uint honorLevel)
		{
			throw null;
		}

		[IteratorStateMachine(typeof(_003CLoadAssetBundle_003Ed__11))]
		public IEnumerator LoadAssetBundle()
		{
			throw null;
		}

		public void UnloadAssetBundle()
		{
			throw null;
		}

		public void KillIfLoading()
		{
			throw null;
		}

		public string GetAssetBundleName()
		{
			throw null;
		}

		public MasterHonorGroup GetMasterHonorGroup()
		{
			throw null;
		}

		public HonorRarity GetHonorRarity()
		{
			throw null;
		}

		public HonorType? TryGetHonorType()
		{
			throw null;
		}

		public string GetMasterHonorGroupType()
		{
			throw null;
		}

		public bool EnableDedicatedFrame()
		{
			throw null;
		}
	}
}
