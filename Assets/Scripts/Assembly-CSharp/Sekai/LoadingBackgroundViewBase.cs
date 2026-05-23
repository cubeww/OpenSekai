using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public abstract class LoadingBackgroundViewBase : MonoBehaviour
	{
		[SerializeField]
		private CustomRawImage _backgroundImage;

		protected Texture2D _backgroundTexture2D;

		protected abstract string BackgroundImagePath { get; }

		public void Setup()
		{
			Load();
			SetImage();
		}

		public virtual void Unload()
		{
			if (_backgroundImage != null)
			{
				_backgroundImage.texture = null;
			}

			if (_backgroundTexture2D != null)
			{
				Resources.UnloadAsset(_backgroundTexture2D);
				_backgroundTexture2D = null;
			}
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		protected virtual void SetImage()
		{
			if (_backgroundImage != null)
			{
				_backgroundImage.texture = _backgroundTexture2D;
			}
		}

		protected virtual void Load()
		{
			_backgroundTexture2D = Resources.Load<Texture2D>(BackgroundImagePath);
		}

		protected LoadingBackgroundViewBase()
		{
		}
	}
}
