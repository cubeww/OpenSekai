using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.Live
{
    public class Background2DView : LiveViewBase
    {
        private const string DefaultBackgroundName = "default";
        private const string OriginalMovieIdFormat = "0000";
        private const string MovieFilterProperty = "_Filter";

        [SerializeField] private Camera movieCamera;
        [SerializeField] private Camera jacketCamera;
        [SerializeField] private GameObject jacketModeRoot;
        [SerializeField] private GameObject movieModeRoot;
        [SerializeField] private SpriteRenderer backgroundRenderer;
        [SerializeField] private SpriteRenderer movieRenderer;
        [SerializeField] private SpriteRenderer[] jackets;

        [SerializeField] private Sprite defaultBackgroundSprite;
        [SerializeField] private Texture2D defaultJacketTexture;
        [SerializeField] private Material moviePreparedMaterial;

        private BaseLiveController baseController;
        private string movieAssetBundleName;

        public override void Setup(BaseLiveController baseController)
        {
            this.baseController = baseController;

            MusicCategory category = GetMusicCategory();
            if (category == MusicCategory.original || category == MusicCategory.mv_2d)
            {
                if (movieCamera != null)
                {
                    movieCamera.targetTexture = baseController != null ? baseController.BackgroundTexture : null;
                }
            }
            else if (jacketCamera != null)
            {
                jacketCamera.targetTexture = baseController != null ? baseController.BackgroundTexture : null;
            }

            if (jackets != null)
            {
                for (int i = 0; i < jackets.Length; i++)
                {
                    SpriteRenderer spriteRenderer = jackets[i];
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.gameObject.SetActive(false);
                    }
                }
            }

            if (movieRenderer != null)
            {
                movieRenderer.gameObject.SetActive(false);
            }
        }

        public override void OnLoad()
        {
            MusicCategory category = GetMusicCategory();
            if (category == MusicCategory.original || category == MusicCategory.mv_2d)
            {
                SetupMovie(GetMusicData(), category);
            }
            else
            {
                SetupJacket();
            }

            ChangeLayer(gameObject, gameObject.layer, true);
        }

        private void SetupJacket()
        {
            movieAssetBundleName = string.Empty;

            if (jacketModeRoot != null)
            {
                jacketModeRoot.SetActive(true);
            }

            if (movieModeRoot != null)
            {
                movieModeRoot.SetActive(false);
            }

            if (backgroundRenderer != null)
            {
                backgroundRenderer.sprite = defaultBackgroundSprite;
            }

            Sprite jacketSprite = null;
            if (defaultJacketTexture != null)
            {
                jacketSprite = Sprite.Create(
                    defaultJacketTexture,
                    new Rect(0f, 0f, defaultJacketTexture.width, defaultJacketTexture.height),
                    new Vector2(0.5f, 0.5f));
            }

            if (jackets != null)
            {
                for (int i = 0; i < jackets.Length; i++)
                {
                    SpriteRenderer jacket = jackets[i];
                    if (jacket == null)
                    {
                        continue;
                    }

                    if (jacketSprite != null)
                    {
                        jacket.sprite = jacketSprite;
                        jacket.gameObject.SetActive(true);
                    }
                }
            }

            if (jacketCamera != null)
            {
                jacketCamera.Render();
            }

            gameObject.SetActive(false);
        }

        private void SetupMovie(LiveMusicData musicData, MusicCategory category)
        {
            if (category == MusicCategory.mv_2d)
            {
                movieAssetBundleName = Get2DModeSekaiMVAssetBundleNameByMusicData(musicData);
            }

            if (category == MusicCategory.original)
            {
                int musicId = musicData != null && musicData.Music != null ? musicData.Music.id : 0;
                movieAssetBundleName = Get2DModeOriginalMVAssetBundleName(musicId.ToString(OriginalMovieIdFormat));
            }

            if (movieRenderer != null)
            {
                movieRenderer.gameObject.SetActive(true);
            }

            if (TryPrepareMovie())
            {
                if (jacketModeRoot != null)
                {
                    jacketModeRoot.SetActive(false);
                }

                return;
            }

            movieAssetBundleName = string.Empty;
            if (movieRenderer != null)
            {
                movieRenderer.gameObject.SetActive(false);
            }

            if (movieCamera != null)
            {
                movieCamera.gameObject.SetActive(false);
            }

            if (jacketCamera != null && baseController != null)
            {
                jacketCamera.targetTexture = baseController.BackgroundTexture;
            }

            SetupJacket();
        }

        public override void MusicStart(float time)
        {
            if (string.IsNullOrEmpty(movieAssetBundleName) || movieRenderer == null)
            {
                return;
            }

            Material material = movieRenderer.material;
            if (material != null && material.HasProperty(MovieFilterProperty))
            {
                material.SetFloat(MovieFilterProperty, GetLivePlayMode() == LivePlayMode.MusicVideo ? 0.95f : 0.75f);
            }
        }

        public override void Pause(float time)
        {
        }

        public override void Resume(float time)
        {
        }

        public override void Retry()
        {
            movieAssetBundleName = string.Empty;
        }

        public override void OnFailure(float time)
        {
            Pause(time);
        }

        private bool TryPrepareMovie()
        {
            if (movieRenderer == null || moviePreparedMaterial == null)
            {
                return false;
            }

            movieRenderer.sharedMaterial = moviePreparedMaterial;
            return !string.IsNullOrEmpty(movieAssetBundleName);
        }

        private MusicCategory GetMusicCategory()
        {
            return baseController != null && baseController.BootData != null ? baseController.BootData.MusicCategory : MusicCategory.image;
        }

        private LivePlayMode GetLivePlayMode()
        {
            return baseController != null && baseController.BootData != null ? baseController.BootData.LivePlayMode : LivePlayMode.Free;
        }

        private LiveMusicData GetMusicData()
        {
            return baseController != null && baseController.BootData != null ? baseController.BootData.MusicData : null;
        }

        private static string Get2DModeSekaiMVAssetBundleNameByMusicData(LiveMusicData musicData)
        {
            int musicId = musicData != null && musicData.Music != null ? musicData.Music.id : 0;
            return "mv_2d_" + musicId.ToString(OriginalMovieIdFormat);
        }

        private static string Get2DModeOriginalMVAssetBundleName(string musicId)
        {
            return "original_" + musicId;
        }

        private static void ChangeLayer(GameObject target, int layer, bool recursive)
        {
            if (target == null)
            {
                return;
            }

            target.layer = layer;
            if (!recursive)
            {
                return;
            }

            for (int i = 0; i < target.transform.childCount; i++)
            {
                ChangeLayer(target.transform.GetChild(i).gameObject, layer, true);
            }
        }
    }
}
