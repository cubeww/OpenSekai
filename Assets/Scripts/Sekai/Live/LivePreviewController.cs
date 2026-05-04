using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.Live
{
    public class LivePreviewController : BaseLiveController
    {
        [SerializeField] private LiveViewBase[] liveViews;
        [SerializeField] private int renderTextureWidth = 1920;
        [SerializeField] private int renderTextureHeight = 1080;
        [SerializeField] private int previewMusicId = 1;
        [SerializeField] private MusicCategory musicCategory = MusicCategory.image;
        [SerializeField] private LivePlayMode livePlayMode = LivePlayMode.Free;
        [SerializeField] private bool startOnAwake = true;
        [SerializeField] private bool musicStartOnAwake = true;

        private void Awake()
        {
            if (startOnAwake)
            {
                Setup();
            }
        }

        public void Setup()
        {
            EnsurePreviewBootData();
            EnsureBackgroundTexture();

            if (liveViews == null)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].Setup(this);
                }
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].OnLoad();
                }
            }

            if (!musicStartOnAwake)
            {
                return;
            }

            for (int i = 0; i < liveViews.Length; i++)
            {
                if (liveViews[i] != null)
                {
                    liveViews[i].MusicStart(0f);
                }
            }
        }

        private void EnsurePreviewBootData()
        {
            if (BootData != null)
            {
                return;
            }

            SetupBootDataForPreview(new LiveBootDataBase
            {
                MusicCategory = musicCategory,
                LivePlayMode = livePlayMode,
                MusicData = new LiveMusicData
                {
                    Music = new MasterMusic
                    {
                        id = previewMusicId
                    }
                }
            });
        }

        private void EnsureBackgroundTexture()
        {
            if (BackgroundTexture != null)
            {
                return;
            }

            BackgroundTexture = new RenderTexture(renderTextureWidth, renderTextureHeight, 24)
            {
                name = "LiveBackgroundTexture"
            };
            BackgroundTexture.Create();
        }

        private void OnDestroy()
        {
            if (BackgroundTexture != null)
            {
                BackgroundTexture.Release();
                BackgroundTexture = null;
            }
        }
    }
}
