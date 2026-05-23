using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsCharacterIcon : ListViewItem
	{
		public enum Size
		{
			S = 0,
			SM = 1,
			M = 2,
			L = 3,
			WorldMap = 4,
			AreaExp = 5,
			Shop = 6,
			GachaMemberDetail = 7,
			Manual = 8
		}

		public enum ViewMode
		{
			Character = 0,
			Mob = 1,
			Direct = 2
		}

		private readonly Vector3 SIZE_S;

		private readonly Vector3 SIZE_SM;

		private readonly Vector3 SIZE_M;

		private readonly Vector3 SIZE_L;

		private readonly Vector3 SIZE_SHOP;

		private readonly Vector3 SIZE_WORLDMAP;

		private readonly Vector3 SIZE_AREAEXP;

		private readonly Vector3 SIZE_GACHAMEMBERDETAIL;

		[SerializeField]
		private CustomImage iconImage;

		[SerializeField]
		private CustomImage _assetBundleIconImage;

		[SerializeField]
		private int characterId;

		[SerializeField]
		private Size iconSize;

		[SerializeField]
		private CustomImage frameImage;

		[SerializeField]
		private GameObject grayMaskObj;

		private ViewMode viewMode;

		private AreaTimeType _areaTimeType;

		private IUIPartsCharacterIconConfiguration _configuration;

		public int CharacterId
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

		public Size IconSize
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

		public void Setup(IUIPartsCharacterIconConfiguration configuration)
		{
			throw null;
		}

		public void ApplySerializeFieldIconSettings()
		{
			throw null;
		}

		[Obsolete("Use Setup(IUIPartsCharacterIconConfiguration configuration)")]
		public void Setup(int characterId, Size size)
		{
			throw null;
		}

		[Obsolete("Use Setup(IUIPartsCharacterIconConfiguration configuration)")]
		public void Setup(int characterId, Color color)
		{
			throw null;
		}

		[Obsolete("Use Setup(IUIPartsCharacterIconConfiguration configuration)")]
		public void Setup(int characterId, Size size, Color color)
		{
			throw null;
		}

		[Obsolete("Use Setup(IUIPartsCharacterIconConfiguration configuration)")]
		public void Setup(int characterId, Size size, SupportUnitType supportUnitType, UnitType unitType)
		{
			throw null;
		}

		public void Empty()
		{
			throw null;
		}

		public void Mob()
		{
			throw null;
		}

		public void Mob(Size size)
		{
			throw null;
		}

		private void Awake()
		{
			throw null;
		}

		protected void UpdateIcon()
		{
			throw null;
		}

		private void UpdateSize()
		{
			throw null;
		}

		public void SetGrayMask(bool flag)
		{
			throw null;
		}

		private void Setup(int characterId)
		{
			throw null;
		}

		private void Setup(Color color)
		{
			throw null;
		}

		private void Setup(Size size)
		{
			throw null;
		}

		private void SetAreaTimeType(AreaTimeType areaTimeType)
		{
			throw null;
		}

		private void SetupIconSprite()
		{
			throw null;
		}

		private void SetAssetBundleIcon()
		{
			throw null;
		}

		private void ShowAssetBundleIconImage()
		{
			throw null;
		}

		public void HideAssetBundleIconImage()
		{
			throw null;
		}

		public UIPartsCharacterIcon()
		{
			throw null;
		}
	}
}
