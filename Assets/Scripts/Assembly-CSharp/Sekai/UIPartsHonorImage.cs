using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using Sekai.Honor;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsHonorImage : MonoBehaviour, IUIPartsHonor
	{
		public enum Size
		{
			S = 0,
			M = 1,
			L = 2,
			LL = 3,
			Manual = 4
		}

		[CompilerGenerated]
		private sealed class _003CLoadAsync_003Ed__46 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public string honorAssetBundleName;

			public UIPartsHonorImage _003C_003E4__this;

			public HonorSlot slotType;

			public bool emitEffect;

			public string honorRankAssetBundleName;

			public string honorType;

			private AssetBundleLoader _003Cloader_003E5__2;

			private GameObject _003CeffectPrefab_003E5__3;

			private AssetBundleLoader _003ChonorRankLoader_003E5__4;

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
			public _003CLoadAsync_003Ed__46(int _003C_003E1__state)
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

		private const int EFFECT_BASE_ORDER_IN_LAYER = 200;

		private const string CORE_IMAGE_MAIN = "degree_main.png";

		private const string CORE_IMAGE_SUB = "degree_sub.png";

		private const string CORE_IMAGE_SCROLL = "scroll.png";

		private const string RANK_IMAGE_MAIN = "rank_main.png";

		private const string RANK_IMAGE_SUB = "rank_sub.png";

		private const string EFFECT_MAIN = "effect_main.prefab";

		private const string EFFECT_SUB = "effect_sub.prefab";

		[SerializeField]
		private CustomRawImage coreImage;

		[SerializeField]
		private CustomImage frameSprite;

		[SerializeField]
		private CustomRawImage rankImage;

		[SerializeField]
		private UIPartsHonorLevel levelView;

		[SerializeField]
		private UIPartsUniqueHonorLevel _uniqueLevelView;

		[SerializeField]
		private UIPartsLiveMasterHonorLevel _liveMasterLevel;

		[SerializeField]
		private RectTransform _liveMasterLevelMainPosition;

		[SerializeField]
		private RectTransform _liveMasterLevelSubPosition;

		private GameObject effectObject;

		private MasterHonor masterData;

		private int level;

		private int _clearCount;

		private bool enable;

		private Size imageSize;

		private UIPartsHonorImageViewModel _viewModel;

		private IEnumerator loadSlotHandle;

		public bool IsLoading
		{
			get
			{
				throw null;
			}
		}

		public Vector2 SizeDelta
		{
			get
			{
				throw null;
			}
		}

		public MasterHonor MasterData
		{
			get
			{
				throw null;
			}
		}

		public int Id
		{
			get
			{
				throw null;
			}
		}

		public GameObject Obj
		{
			get
			{
				throw null;
			}
		}

		public HonorType Type
		{
			get
			{
				throw null;
			}
		}

		public bool Enabled
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

		[Skip]
		public void Refresh(MasterHonor master, int level, int clearCount, HonorSlot slotType, Size size = Size.L, bool emitEffect = true)
		{
			throw null;
		}

		public void KillIfLoading()
		{
			throw null;
		}

		public void SetSlot(HonorSlot slotType)
		{
			throw null;
		}

		public void SetScale(Size size)
		{
			throw null;
		}

		public void Release()
		{
			throw null;
		}

		public void SetActive(bool active)
		{
			throw null;
		}

		[IteratorStateMachine(typeof(_003CLoadAsync_003Ed__46))]
		private IEnumerator LoadAsync(string honorAssetBundleName, string honorRankAssetBundleName, string honorType, HonorSlot slotType, bool emitEffect)
		{
			throw null;
		}

		private void SetHonorFrame(HonorSlot slotType)
		{
			throw null;
		}

		private void SetupLevelView(MasterHonor masterHonor, string honorType, HonorSlot slotType, AssetBundleLoader assetBundleLoader, bool emitEffect)
		{
			throw null;
		}

		public UIPartsHonorImage()
		{
			throw null;
		}
	}
}
