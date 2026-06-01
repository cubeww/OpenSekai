using System.IO;
using Sekai.Core.Live;
using Sekai.MusicScoreMaker.Common;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

namespace Sekai.Live
{
	public class Background2DView : LiveViewBase
	{
		private const string DefaultBackgroundBundleName = "live/2dmode/background/default";
		private const string DefaultBackgroundAssetName = "default";
		private const float BaseAspect = 16f / 9f;
		private const string MovieQuadName = "OpenSekaiMovieQuad";
		private const int DefaultMovieTextureWidth = 1920;
		private const int DefaultMovieTextureHeight = 1080;

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
		private int lastRenderedScreenWidth;
		private int lastRenderedScreenHeight;
		private VideoPlayer videoPlayer;
		private RenderTexture movieTexture;
		private GameObject movieQuadObject;
		private Mesh movieQuadMesh;
		private MeshRenderer movieQuadRenderer;
		private Material movieMaterial;
		private bool movieModeActive;
		private bool pendingMoviePlay;
		private float pendingMovieStartTime;
		private bool deferMovieStartUntilRhythmGameStart;
		private string moviePlaybackPath;

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
			if (TrySetupMovie())
			{
				return;
			}

			SetupJacket();
		}

		private void SetupRenderTarget()
		{
			bool useMovieCamera = ShouldUseMovieCamera();
			if (jacketCamera != null)
			{
				jacketCamera.enabled = false;
				jacketCamera.targetTexture = useMovieCamera ? null : baseController?.BackgroundTexture;
			}

			if (movieCamera != null)
			{
				movieCamera.enabled = false;
				movieCamera.targetTexture = useMovieCamera ? baseController?.BackgroundTexture : null;
			}
		}

		private bool ShouldUseMovieCamera()
		{
			return baseController?.BootData?.MusicCategory == MusicCategory.mv_2d;
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

			RenderJacketBackground();
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

		public override void OnUpdate(float musicTime)
		{
			if (movieModeActive)
			{
				if (Screen.width != lastRenderedScreenWidth || Screen.height != lastRenderedScreenHeight)
				{
					UpdateMovieRenderTarget();
				}
				return;
			}

			if (Screen.width == lastRenderedScreenWidth && Screen.height == lastRenderedScreenHeight)
			{
				return;
			}

			RenderJacketBackground();
		}

		public override void MusicStart(float musicTime)
		{
			if (!movieModeActive)
			{
				return;
			}

			if (ShouldDeferMovieStartUntilRhythmGameStart())
			{
				deferMovieStartUntilRhythmGameStart = true;
				return;
			}

			PlayMovieAt(GetInitialMovieStartTime());
		}

		public override void RhythmGameStart()
		{
			if (!movieModeActive || !deferMovieStartUntilRhythmGameStart)
			{
				return;
			}

			deferMovieStartUntilRhythmGameStart = false;
			PlayMovieAt(GetInitialMovieStartTime());
		}

		public override void Pause()
		{
			if (movieModeActive && videoPlayer != null && videoPlayer.isPlaying)
			{
				videoPlayer.Pause();
			}
		}

		public override void Resume(float musicTime)
		{
			if (!movieModeActive)
			{
				return;
			}

			if (videoPlayer == null || !videoPlayer.isPrepared)
			{
				PlayMovieAt(GetInitialMovieStartTime());
				return;
			}

			videoPlayer.Play();
		}

		public override void Retry()
		{
			if (!movieModeActive)
			{
				return;
			}

			pendingMoviePlay = false;
			pendingMovieStartTime = 0f;
			if (videoPlayer != null)
			{
				videoPlayer.Stop();
				videoPlayer.Prepare();
			}
		}

		public override void Finish()
		{
			StopMovie();
		}

		public override void Finish(float duration)
		{
			Finish();
		}

		public override void OnUnload()
		{
			StopMovie();
		}

		private void RenderJacketBackground()
		{
			if (jacketCamera == null)
			{
				return;
			}

			bool wasActive = gameObject.activeSelf;
			if (!wasActive)
			{
				gameObject.SetActive(true);
			}

			if (jacketModeRoot != null)
			{
				jacketModeRoot.SetActive(true);
			}
			DisableMovieMode();

			baseController?.EnsureBackgroundTextureSizeForCurrentScreen();
			jacketCamera.targetTexture = baseController?.BackgroundTexture;
			float aspect = GetRenderTargetAspect();
			if (aspect > 0f)
			{
				jacketCamera.aspect = aspect;
				jacketCamera.ResetProjectionMatrix();
			}
			jacketCamera.enabled = true;
			jacketCamera.Render();
			jacketCamera.enabled = false;
			lastRenderedScreenWidth = Screen.width;
			lastRenderedScreenHeight = Screen.height;

			if (!wasActive)
			{
				gameObject.SetActive(false);
			}
		}

		private bool TrySetupMovie()
		{
			string moviePath = ResolveCustomMoviePath();
			if (!ShouldUseMovieCamera() || string.IsNullOrEmpty(moviePath) || !File.Exists(moviePath) || movieCamera == null || movieRenderer == null)
			{
				return false;
			}
			moviePlaybackPath = PrepareMoviePlaybackPath(moviePath);
			if (string.IsNullOrEmpty(moviePlaybackPath) || !File.Exists(moviePlaybackPath))
			{
				return false;
			}

			movieModeActive = true;
			pendingMoviePlay = false;
			pendingMovieStartTime = 0f;
			SetJacketsActive(false);
			if (jacketModeRoot != null)
			{
				jacketModeRoot.SetActive(false);
			}
			if (movieModeRoot != null)
			{
				movieModeRoot.SetActive(true);
			}
			movieRenderer.gameObject.SetActive(true);
			movieRenderer.enabled = false;

			EnsureMovieQuad();
			SetupVideoPlayer(moviePlaybackPath);
			UpdateMovieRenderTarget();
			movieCamera.enabled = true;
			return true;
		}

		private string ResolveCustomMoviePath()
		{
			FreeLiveBootData bootData = baseController?.BootData as FreeLiveBootData;
			if (bootData == null || string.IsNullOrEmpty(bootData.CustomMusicScorePath))
			{
				return null;
			}

			CustomMusicScoreEntry entry = CustomMusicScoreStorage.LoadEntry(bootData.CustomMusicScorePath);
			string moviePath = entry?.VideoPath;
			return string.IsNullOrEmpty(moviePath) ? null : moviePath;
		}

		private void SetupVideoPlayer(string moviePath)
		{
			if (videoPlayer == null)
			{
				videoPlayer = movieRenderer.GetComponent<VideoPlayer>();
			}
			if (videoPlayer == null)
			{
				videoPlayer = movieRenderer.gameObject.AddComponent<VideoPlayer>();
			}

			videoPlayer.prepareCompleted -= OnMoviePrepared;
			videoPlayer.errorReceived -= OnMovieError;
			videoPlayer.prepareCompleted += OnMoviePrepared;
			videoPlayer.errorReceived += OnMovieError;
			videoPlayer.playOnAwake = false;
			videoPlayer.isLooping = false;
			videoPlayer.waitForFirstFrame = true;
			videoPlayer.skipOnDrop = true;
			videoPlayer.timeReference = VideoTimeReference.Freerun;
			videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
			videoPlayer.renderMode = VideoRenderMode.RenderTexture;
			videoPlayer.source = VideoSource.Url;
			videoPlayer.url = ToFileUri(moviePath);
			videoPlayer.targetTexture = EnsureMovieTexture(DefaultMovieTextureWidth, DefaultMovieTextureHeight);
			ApplyMovieTexture();
			videoPlayer.Prepare();
		}

		private void OnMoviePrepared(VideoPlayer source)
		{
			if (!movieModeActive || source != videoPlayer)
			{
				return;
			}

			int width = source.width > 0 ? (int)source.width : DefaultMovieTextureWidth;
			int height = source.height > 0 ? (int)source.height : DefaultMovieTextureHeight;
			source.targetTexture = EnsureMovieTexture(width, height);
			ApplyMovieTexture();
			UpdateMovieRenderTarget();
			if (pendingMoviePlay)
			{
				PlayMovieAt(pendingMovieStartTime);
			}
		}

		private void OnMovieError(VideoPlayer source, string message)
		{
			if (source != videoPlayer)
			{
				return;
			}

			Debug.LogWarningFormat("2DMV playback failed. url:{0} error:{1}", source != null ? source.url : string.Empty, message);
			movieModeActive = false;
			StopMovie();
			SetupJacket();
		}

		private RenderTexture EnsureMovieTexture(int width, int height)
		{
			width = Mathf.Max(1, width);
			height = Mathf.Max(1, height);
			if (movieTexture != null && movieTexture.width == width && movieTexture.height == height)
			{
				return movieTexture;
			}

			if (movieTexture != null)
			{
				movieTexture.Release();
				Destroy(movieTexture);
			}
			movieTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
			movieTexture.name = "OpenSekai 2DMV Texture";
			movieTexture.Create();
			return movieTexture;
		}

		private void EnsureMovieQuad()
		{
			if (movieQuadObject != null)
			{
				movieQuadObject.SetActive(true);
				return;
			}

			Transform parent = movieModeRoot != null ? movieModeRoot.transform : transform;
			movieQuadObject = new GameObject(MovieQuadName);
			movieQuadObject.layer = gameObject.layer;
			movieQuadObject.transform.SetParent(parent, false);
			movieQuadObject.transform.localPosition = Vector3.zero;
			movieQuadObject.transform.localRotation = Quaternion.identity;
			MeshFilter meshFilter = movieQuadObject.AddComponent<MeshFilter>();
			movieQuadMesh = CreateMovieQuadMesh();
			meshFilter.sharedMesh = movieQuadMesh;
			movieQuadRenderer = movieQuadObject.AddComponent<MeshRenderer>();
			movieQuadRenderer.shadowCastingMode = ShadowCastingMode.Off;
			movieQuadRenderer.receiveShadows = false;
			movieMaterial = CreateMovieMaterial();
			movieQuadRenderer.sharedMaterial = movieMaterial;
		}

		private static Mesh CreateMovieQuadMesh()
		{
			Mesh mesh = new Mesh();
			mesh.name = "OpenSekai 2DMV Quad";
			mesh.vertices = new[]
			{
				new Vector3(-0.5f, -0.5f, 0f),
				new Vector3(0.5f, -0.5f, 0f),
				new Vector3(-0.5f, 0.5f, 0f),
				new Vector3(0.5f, 0.5f, 0f)
			};
			mesh.uv = new[]
			{
				new Vector2(0f, 0f),
				new Vector2(1f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 1f)
			};
			mesh.triangles = new[] { 0, 2, 1, 2, 3, 1 };
			mesh.RecalculateBounds();
			return mesh;
		}

		private Material CreateMovieMaterial()
		{
			Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
			if (shader == null)
			{
				shader = Shader.Find("Unlit/Texture");
			}
			if (shader == null)
			{
				shader = Shader.Find("Sprites/Default");
			}

			Material material = shader != null
				? new Material(shader)
				: new Material(movieRenderer.sharedMaterial);
			material.name = "OpenSekai 2DMV Material";
			if (material.HasProperty("_Cull"))
			{
				material.SetFloat("_Cull", (float)CullMode.Off);
			}
			return material;
		}

		private void ApplyMovieTexture()
		{
			if (movieMaterial == null || movieTexture == null)
			{
				return;
			}

			movieMaterial.mainTexture = movieTexture;
			if (movieMaterial.HasProperty("_BaseMap"))
			{
				movieMaterial.SetTexture("_BaseMap", movieTexture);
			}
			if (movieMaterial.HasProperty("_Color"))
			{
				movieMaterial.SetColor("_Color", Color.white);
			}
			if (movieMaterial.HasProperty("_BaseColor"))
			{
				movieMaterial.SetColor("_BaseColor", Color.white);
			}
		}

		private void UpdateMovieRenderTarget()
		{
			if (movieCamera == null)
			{
				return;
			}

			baseController?.EnsureBackgroundTextureSizeForCurrentScreen();
			movieCamera.targetTexture = baseController?.BackgroundTexture;
			float aspect = GetRenderTargetAspect(movieCamera);
			if (aspect > 0f)
			{
				movieCamera.aspect = aspect;
				movieCamera.ResetProjectionMatrix();
			}
			UpdateMovieQuadScale(aspect);
			lastRenderedScreenWidth = Screen.width;
			lastRenderedScreenHeight = Screen.height;
		}

		private void UpdateMovieQuadScale(float targetAspect)
		{
			if (movieQuadObject == null || movieCamera == null)
			{
				return;
			}

			if (targetAspect <= 0f)
			{
				targetAspect = BaseAspect;
			}
			float videoAspect = GetMovieAspect();
			float viewHeight = movieCamera.orthographic ? movieCamera.orthographicSize * 2f : 10f;
			float viewWidth = viewHeight * targetAspect;
			float quadWidth = viewWidth;
			float quadHeight = viewHeight;
			if (videoAspect > targetAspect)
			{
				quadWidth = quadHeight * videoAspect;
			}
			else if (videoAspect > 0f)
			{
				quadHeight = quadWidth / videoAspect;
			}

			movieQuadObject.transform.localScale = new Vector3(quadWidth, quadHeight, 1f);
		}

		private float GetMovieAspect()
		{
			if (videoPlayer != null && videoPlayer.width > 0 && videoPlayer.height > 0)
			{
				return (float)videoPlayer.width / videoPlayer.height;
			}
			if (movieTexture != null && movieTexture.height > 0)
			{
				return (float)movieTexture.width / movieTexture.height;
			}
			return BaseAspect;
		}

		private void PlayMovieAt(float musicTime)
		{
			if (videoPlayer == null)
			{
				return;
			}

			pendingMoviePlay = true;
			pendingMovieStartTime = Mathf.Max(0f, musicTime);
			if (!videoPlayer.isPrepared)
			{
				videoPlayer.Prepare();
				return;
			}

			pendingMoviePlay = false;
			if (videoPlayer.canSetTime && pendingMovieStartTime > 0.05f)
			{
				videoPlayer.time = pendingMovieStartTime;
			}
			videoPlayer.Play();
		}

		private float GetInitialMovieStartTime()
		{
			long startMusicTimeMs = baseController?.BootData?.MusicData?.StartMusicTimeMs ?? 0L;
			if (startMusicTimeMs >= 1L)
			{
				return startMusicTimeMs / 1000f;
			}
			return 0f;
		}

		private bool ShouldDeferMovieStartUntilRhythmGameStart()
		{
			return baseController?.BootData?.MusicData?.PlayStartEffectEnabled == true
				&& baseController?.BootData?.ReleaseTransitionBeforeMusicStart == true;
		}

		private string PrepareMoviePlaybackPath(string moviePath)
		{
#if UNITY_ANDROID
			return CopyMovieToAsciiCachePath(moviePath);
#else
			return moviePath;
#endif
		}

#if UNITY_ANDROID
		private string CopyMovieToAsciiCachePath(string moviePath)
		{
			try
			{
				FileInfo sourceInfo = new FileInfo(moviePath);
				if (!sourceInfo.Exists)
				{
					return null;
				}

				string cacheDirectory = Path.Combine(Application.temporaryCachePath, "OpenSekai2DMV");
				Directory.CreateDirectory(cacheDirectory);
				string cachePath = Path.Combine(cacheDirectory, CreateAsciiMovieCacheFileName(sourceInfo));
				FileInfo cacheInfo = new FileInfo(cachePath);
				if (!cacheInfo.Exists || cacheInfo.Length != sourceInfo.Length || cacheInfo.LastWriteTimeUtc != sourceInfo.LastWriteTimeUtc)
				{
					File.Copy(sourceInfo.FullName, cachePath, true);
					File.SetLastWriteTimeUtc(cachePath, sourceInfo.LastWriteTimeUtc);
				}
				return cachePath;
			}
			catch (System.Exception exception)
			{
				Debug.LogWarningFormat("Failed to prepare 2DMV cache. path:{0} error:{1}", moviePath, exception.Message);
				return null;
			}
		}

		private static string CreateAsciiMovieCacheFileName(FileInfo sourceInfo)
		{
			string extension = NormalizeMovieExtension(sourceInfo.Extension);
			uint hash = 2166136261U;
			AddStringToHash(ref hash, sourceInfo.FullName);
			AddStringToHash(ref hash, sourceInfo.Length.ToString());
			AddStringToHash(ref hash, sourceInfo.LastWriteTimeUtc.Ticks.ToString());
			return "video_" + hash.ToString("x8") + extension;
		}

		private static string NormalizeMovieExtension(string extension)
		{
			if (string.IsNullOrEmpty(extension) || extension.Length > 8)
			{
				return ".mp4";
			}

			extension = extension.ToLowerInvariant();
			for (int i = 0; i < extension.Length; i++)
			{
				char c = extension[i];
				if (i == 0)
				{
					if (c != '.')
					{
						return ".mp4";
					}
				}
				else if ((c < '0' || c > '9') && (c < 'a' || c > 'z'))
				{
					return ".mp4";
				}
			}
			return extension;
		}

		private static void AddStringToHash(ref uint hash, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}

			unchecked
			{
				for (int i = 0; i < value.Length; i++)
				{
					hash ^= value[i];
					hash *= 16777619U;
				}
			}
		}
#endif

		private void StopMovie()
		{
			pendingMoviePlay = false;
			pendingMovieStartTime = 0f;
			deferMovieStartUntilRhythmGameStart = false;
			if (videoPlayer != null)
			{
				videoPlayer.Stop();
			}
		}

		private float GetRenderTargetAspect()
		{
			return GetRenderTargetAspect(jacketCamera);
		}

		private float GetRenderTargetAspect(Camera targetCamera)
		{
			RenderTexture targetTexture = targetCamera != null ? targetCamera.targetTexture : null;
			if (targetTexture != null && targetTexture.height > 0)
			{
				return (float)targetTexture.width / targetTexture.height;
			}

			return Screen.height > 0 ? (float)Screen.width / Screen.height : BaseAspect;
		}

		private void DisableMovieMode()
		{
			movieModeActive = false;
			StopMovie();
			if (movieModeRoot != null)
			{
				movieModeRoot.SetActive(false);
			}
			if (movieQuadObject != null)
			{
				movieQuadObject.SetActive(false);
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

			CustomMusicScoreEntry entry = CustomMusicScoreStorage.LoadEntry(bootData.CustomMusicScorePath);
			string jacketPath = entry?.JacketPath;
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
			if (videoPlayer != null)
			{
				videoPlayer.prepareCompleted -= OnMoviePrepared;
				videoPlayer.errorReceived -= OnMovieError;
			}
			ClearExternalJacketAssets();
			if (movieTexture != null)
			{
				movieTexture.Release();
				Destroy(movieTexture);
				movieTexture = null;
			}
			if (movieMaterial != null)
			{
				Destroy(movieMaterial);
				movieMaterial = null;
			}
			if (movieQuadMesh != null)
			{
				Destroy(movieQuadMesh);
				movieQuadMesh = null;
			}
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

		private static string ToFileUri(string path)
		{
			return new System.Uri(Path.GetFullPath(path)).AbsoluteUri;
		}
	}
}
