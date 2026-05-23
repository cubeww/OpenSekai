using System.IO;
using Sekai.Core.Live;
using Sekai.MusicScoreMaker.Common;
using UnityEngine;

namespace Sekai.Live
{
	public class Background2DView : LiveViewBase
	{
		private const string DefaultBackgroundBundleName = "live/2dmode/background/default";
		private const string DefaultBackgroundAssetName = "default";

		[SerializeField]
		private Camera movieCamera;

		[SerializeField]
		private Camera jacketCamera;

		[SerializeField]
		private GameObject jacketModeRoot;

		[SerializeField]
		private GameObject movieModeRoot;

		[SerializeField]
		private SpriteRenderer backgroundRenderer;

		[SerializeField]
		private SpriteRenderer movieRenderer;

		[SerializeField]
		private SpriteRenderer[] jackets;

		private BaseLiveController baseController;
		private Texture2D externalJacketTexture;
		private Sprite externalJacketSprite;

		private void Awake()
		{
			DisableMovieMode();
			if (jacketCamera != null)
			{
				jacketCamera.enabled = false;
			}
		}

		public override void Setup(BaseLiveController controller)
		{
			baseController = controller;
			DisableMovieMode();
			SetupRenderTarget();
			SetJacketsActive(false);
		}

		public override void OnLoad()
		{
			// Movie playback is still being restored. For test play and custom scores,
			// render the jacket/default 2D background into BaseLiveController.BackgroundTexture,
			// which is what FrontUIView/Background displays.
			SetupJacket();
		}

		private void SetupRenderTarget()
		{
			// 2D movie playback is not restored yet, so only the jacket camera is allowed
			// to write BaseLiveController.BackgroundTexture. Leaving movieCamera bound can
			// clear the same RenderTexture later and turn FrontUIView/Background gray.
			if (jacketCamera != null)
			{
				jacketCamera.enabled = false;
				jacketCamera.targetTexture = baseController?.BackgroundTexture;
			}

			if (movieCamera != null)
			{
				movieCamera.targetTexture = null;
			}
		}

		private void SetupJacket()
		{
			if (jacketModeRoot != null)
			{
				jacketModeRoot.SetActive(true);
			}
			DisableMovieMode();

			LoadDefaultBackground();
			Texture2D jacketTexture = LoadJacketTexture();
			if (jacketTexture != null)
			{
				ApplyJacketTexture(jacketTexture, destroyWhenReplaced: IsExternalJacketTexture(jacketTexture));
			}

			if (jacketCamera != null)
			{
				DisableMovieMode();
				jacketCamera.targetTexture = baseController?.BackgroundTexture;
				jacketCamera.enabled = true;
				jacketCamera.Render();
				jacketCamera.enabled = false;
			}

			gameObject.SetActive(false);
		}

		private void LoadDefaultBackground()
		{
			if (backgroundRenderer != null)
			{
				Sprite sprite = AssetBundleUtility.LoadAsset<Sprite>(DefaultBackgroundBundleName, DefaultBackgroundAssetName);
				if (sprite != null)
				{
					backgroundRenderer.sprite = sprite;
				}
				backgroundRenderer.gameObject.SetActive(true);
			}
		}

		private void DisableMovieMode()
		{
			if (movieModeRoot != null)
			{
				movieModeRoot.SetActive(false);
			}
			if (movieRenderer != null)
			{
				movieRenderer.enabled = false;
				movieRenderer.gameObject.SetActive(false);
			}
			if (movieCamera != null)
			{
				movieCamera.enabled = false;
				movieCamera.targetTexture = null;
			}
		}

		private Texture2D LoadJacketTexture()
		{
			Texture2D customTexture = LoadCustomJacketTexture(baseController?.BootData as FreeLiveBootData);
			if (customTexture != null)
			{
				return customTexture;
			}

			int musicId = baseController?.BootData?.MusicData?.Music?.id ?? 0;
			if (musicId <= 0)
			{
				return null;
			}

			string resourceName = $"jacket_s_{musicId}";
			Texture2D texture = AssetBundleUtility.LoadAsset<Texture2D>("thumbnail/music_jacket", resourceName, false);
			return texture != null
				? texture
				: AssetBundleUtility.LoadAsset<Texture2D>("startapp/thumbnail/music_jacket", resourceName, false);
		}

		private Texture2D LoadCustomJacketTexture(FreeLiveBootData bootData)
		{
			if (bootData == null || string.IsNullOrEmpty(bootData.CustomMusicScorePath))
			{
				return null;
			}

			CustomMusicScorePackage package = CustomMusicScoreStorage.LoadPackage(bootData.CustomMusicScorePath);
			string jacketPath = package?.JacketPath;
			if (string.IsNullOrEmpty(jacketPath) || !File.Exists(jacketPath))
			{
				return null;
			}

			ClearExternalJacketAssets();
			Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			if (ImageConversion.LoadImage(texture, File.ReadAllBytes(jacketPath)))
			{
				externalJacketTexture = texture;
				return texture;
			}

			Destroy(texture);
			return null;
		}

		private bool IsExternalJacketTexture(Texture2D texture)
		{
			return texture != null && texture == externalJacketTexture;
		}

		private void ApplyJacketTexture(Texture2D texture, bool destroyWhenReplaced)
		{
			if (jackets == null)
			{
				return;
			}

			if (destroyWhenReplaced && externalJacketSprite != null)
			{
				Destroy(externalJacketSprite);
				externalJacketSprite = null;
			}

			Sprite sprite = Sprite.Create(
				texture,
				new Rect(0f, 0f, texture.width, texture.height),
				new Vector2(0.5f, 0.5f),
				100f);
			if (destroyWhenReplaced)
			{
				externalJacketSprite = sprite;
			}

			foreach (SpriteRenderer jacket in jackets)
			{
				if (jacket != null)
				{
					jacket.sprite = sprite;
					jacket.gameObject.SetActive(true);
				}
			}
		}

		private void SetJacketsActive(bool active)
		{
			if (jackets == null)
			{
				return;
			}

			foreach (SpriteRenderer jacket in jackets)
			{
				if (jacket != null)
				{
					jacket.gameObject.SetActive(active);
				}
			}
		}

		private void OnDestroy()
		{
			ClearExternalJacketAssets();
		}

		private void ClearExternalJacketAssets()
		{
			if (externalJacketSprite != null)
			{
				Destroy(externalJacketSprite);
				externalJacketSprite = null;
			}
			if (externalJacketTexture != null)
			{
				Destroy(externalJacketTexture);
				externalJacketTexture = null;
			}
		}

		// Original also supports movie background seeking via CRI movie playback.
		// OpenSekai currently keeps test play on the local jacket/background path.
	}
}
