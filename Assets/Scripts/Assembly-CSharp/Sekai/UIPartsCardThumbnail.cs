using System;
using Beebyte.Obfuscator;
using CP;
using Sekai.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sekai
{
	public class UIPartsCardThumbnail : UIPartsThumbnail
	{
		[SerializeField]
		protected CustomButton button;

		[SerializeField]
		protected ClickDetector clickDetector;

		[SerializeField]
		protected Transform gettingResourceRarityRoot;

		[SerializeField]
		[FormerlySerializedAs("cardView")]
		private UIPartsCardInfoView cardInfoView;

		[SerializeField]
		protected TweenBase newDisplay;

		[SerializeField]
		protected GameObject checkDisplay;

		protected CardThumbnailViewDataBase viewData;

		protected Action<CardThumbnailViewDataBase> onClickEvent;

		protected Action<CardThumbnailViewDataBase> onLongPressEvent;

		protected bool isEnableLongPress;

		public UIPartsCardInfoView CardInfoView
		{
			get
			{
				throw null;
			}
		}

		public bool IsCheck
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

		public bool VisibleNew
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

		public GameObject NewDisplay
		{
			get
			{
				throw null;
			}
		}

		public bool Interactable
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

		protected virtual void Awake()
		{
			throw null;
		}

		public void SetReferenceTween(UIPartsMasterLevel master)
		{
			throw null;
		}

		public void SetupTween()
		{
			throw null;
		}

		public virtual void Setup(CardThumbnailViewDataBase data, Action<CardThumbnailViewDataBase> onClick, Action<CardThumbnailViewDataBase> onLongPress = null)
		{
			throw null;
		}

		public virtual void Setup(CardThumbnailViewDataBase data, string imageSuffix, Action<CardThumbnailViewDataBase> onClick, Action<CardThumbnailViewDataBase> onLongPress = null, Action<AssetManager.BundleElement> onSuccessLoadThumbnail = null, Action<AssetManager.BundleElement> onErrorLoadThumbnail = null)
		{
			throw null;
		}

		public virtual void Setup(CardThumbnailViewDataBase data, bool afterSpecialTraining, Action<CardThumbnailViewDataBase> onClick, Action<CardThumbnailViewDataBase> onLongPress = null, Action<AssetManager.BundleElement> onSuccessLoadThumbnail = null, Action<AssetManager.BundleElement> onErrorLoadThumbnail = null)
		{
			throw null;
		}

		public virtual void Setup(UserResource resource)
		{
			throw null;
		}

		public void DisableButton()
		{
			throw null;
		}

		protected void SetupParamaters()
		{
			throw null;
		}

		public void SetupNewDisplay(TweenBase newReferenceTween = null, bool newFlag = false)
		{
			throw null;
		}

		public void SetWaitingRoomResourceDisplay(CardThumbnailViewDataBase data, Action<CardThumbnailViewDataBase> onClick, Action<CardThumbnailViewDataBase> onLongPress = null)
		{
			throw null;
		}

		public void RefreshRarity(bool isSpecialTrainig)
		{
			throw null;
		}

		public void SetActiveLvParam(bool active)
		{
			throw null;
		}

		public void SetActiveMasterLv(bool active)
		{
			throw null;
		}

		public void SetActiveAttribute(bool active)
		{
			throw null;
		}

		public void SetActiveRarity(bool active)
		{
			throw null;
		}

		public void SetActiveCostume(bool active)
		{
			throw null;
		}

		[Skip]
		public void OnClick()
		{
			throw null;
		}

		protected void OnLongPress()
		{
			throw null;
		}

		public void SetEnableLongPress(bool isEnable)
		{
			throw null;
		}

		public new virtual void Unload()
		{
			throw null;
		}

		public void RefreshParamText(string value)
		{
			throw null;
		}

		public void SetActiveParamText(bool active)
		{
			throw null;
		}

		public void SetTraining(bool training)
		{
			throw null;
		}

		public UIPartsCardThumbnail()
		{
			throw null;
		}
	}
}
