using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.Live
{
    public partial class LiveFrontView : LiveViewBase
    {
        [SerializeField] private Renderer backgroundRenderer;
        [SerializeField] private SpriteRenderer deadMask;
        [SerializeField] protected CameraSizeUpdater cameraSizeUpdater;
        [SerializeField] private Camera frontViewCamera;
        [SerializeField] private float previewBrightness = 1f;

        private BaseLiveController baseController;
        private Material backgroundMaterial;
        private Mesh runtimeBackgroundMesh;

        public override void Setup(BaseLiveController baseController)
        {
            this.baseController = baseController;

            if (baseController != null)
            {
                baseController.BaseCamera = frontViewCamera;
            }

            SetupBackground();
        }

        public override void MusicStart(float time)
        {
            if (backgroundMaterial == null)
            {
                return;
            }

            backgroundMaterial.color = new Color(previewBrightness, previewBrightness, previewBrightness, 1f);
        }

        private void SetupBackground()
        {
            if (backgroundRenderer == null)
            {
                return;
            }

            EnsureBackgroundMesh();

            float height = cameraSizeUpdater != null && cameraSizeUpdater.OrthographicSize > 0f ? cameraSizeUpdater.OrthographicSize * 2f : 10f;
            float aspect = Screen.height == 0 ? 1f : (float)Screen.width / Screen.height;
            backgroundRenderer.transform.localScale = new Vector3(height * aspect, height, 1f);

            Material source = backgroundRenderer.material;
            if (source == null)
            {
                return;
            }

            backgroundMaterial = new Material(source);
            backgroundRenderer.material = backgroundMaterial;

            if (baseController != null)
            {
                backgroundMaterial.mainTexture = baseController.BackgroundTexture;
            }

            backgroundMaterial.color = Color.black;

            if (deadMask != null)
            {
                deadMask.size = new Vector2(height * aspect, height);
            }
        }

        private void EnsureBackgroundMesh()
        {
            MeshFilter meshFilter = backgroundRenderer.GetComponent<MeshFilter>();
            if (meshFilter == null || meshFilter.sharedMesh != null)
            {
                return;
            }

            runtimeBackgroundMesh = new Mesh
            {
                name = "LiveFrontViewBackgroundQuad"
            };
            runtimeBackgroundMesh.vertices = new[]
            {
                new Vector3(-0.5f, -0.5f, 0f),
                new Vector3(0.5f, -0.5f, 0f),
                new Vector3(-0.5f, 0.5f, 0f),
                new Vector3(0.5f, 0.5f, 0f)
            };
            runtimeBackgroundMesh.uv = new[]
            {
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(0f, 1f),
                new Vector2(1f, 1f)
            };
            runtimeBackgroundMesh.triangles = new[]
            {
                0, 2, 1,
                2, 3, 1
            };
            runtimeBackgroundMesh.RecalculateBounds();
            meshFilter.sharedMesh = runtimeBackgroundMesh;
        }

        private void OnDestroy()
        {
            if (backgroundMaterial != null)
            {
                Destroy(backgroundMaterial);
                backgroundMaterial = null;
            }

            if (runtimeBackgroundMesh != null)
            {
                Destroy(runtimeBackgroundMesh);
                runtimeBackgroundMesh = null;
            }
        }
    }
}
