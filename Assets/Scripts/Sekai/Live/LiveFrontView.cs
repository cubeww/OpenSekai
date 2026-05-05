using Sekai.Core.Live;
using System.Collections.Generic;
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
        private Dictionary<SpriteRenderer, float> spriteRendererAlphaDict;
        private bool isAuto;
        private int life;

        public override void Setup(BaseLiveController baseController)
        {
            base.Setup(baseController);
            this.baseController = baseController;
            life = LiveConfig.Life;

            lifeView?.Setup(OnPause);
            if (baseController != null)
            {
                LiveBootDataBase bootData = baseController.BootData;
                scoreView?.Setup(bootData != null ? bootData.MusicData : null);
                comboView?.Setup(baseController.Settings == null || baseController.Settings.UseAllPerfectEffect);
                cutinManager?.Setup(bootData, baseController);
                musicInfo?.Setup(bootData);
                baseController.BaseCamera = frontViewCamera;
            }

            skillView?.Setup();
            countdownView?.Setup();
            screenEffectView?.Setup();
            consecutiveAutoLiveView?.Setup(OnConsecutiveAutoLivePause, OnConsecutiveAutoLiveResume, OnConsecutiveAutoLiveFinish, frontViewCamera);
            SetupBackground();

            if (fade != null)
            {
                fade.enabled = false;
            }
            if (autoLabel != null)
            {
                autoLabel.SetActive(false);
            }

            isAuto = baseController != null && baseController.BootData != null && baseController.BootData.IsAuto;
            laneView?.Setup(baseController != null ? baseController.Settings : null);
            CacheSpriteRendererAlphas();
            UpdateSpriteAlpha(0f);
        }

        public override void OnLoad()
        {
            SetupTapEffect();
            UpdateSpriteAlpha(0f);
            cutinManager?.Load();
            skillView?.Load();
        }

        public override void OnUnload()
        {
            liveResultView?.Unload();
            skillView?.Unload();
            cutinManager?.Unload();
        }

        public override void MusicStart(float time)
        {
            UpdateSpriteAlpha(0f);
            musicInfo?.Play();

            if (backgroundMaterial == null)
            {
                return;
            }

            float brightness = previewBrightness;
            if (baseController != null && baseController.Settings != null)
            {
                brightness = baseController.Settings.Brightness;
            }

            backgroundMaterial.color = new Color(brightness, brightness, brightness, 1f);
        }

        public override void RhythmGameStart()
        {
            lifeView?.CalculateButtonBounds(frontViewCamera);
            consecutiveAutoLiveView?.CalculateButtonBounds(frontViewCamera);
            FadeIn();
        }

        public override void Retry()
        {
            comboView?.Clear();
            cutinManager?.Clear();
            skillView?.Clear();
            life = LiveConfig.Life;
            if (autoLabel != null)
            {
                autoLabel.SetActive(false);
            }
            UpdateSpriteAlpha(0f);
        }

        public override void Finish()
        {
            FadeOut();
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

        private void SetupTapEffect()
        {
            if (effectRoot == null)
            {
                return;
            }

            effectRoot.position = transform.position;
            if (effectCamera != null)
            {
                effectCamera.enabled = false;
            }
        }

        private void CacheSpriteRendererAlphas()
        {
            spriteRendererAlphaDict = new Dictionary<SpriteRenderer, float>();
            if (liveRoot == null)
            {
                return;
            }

            SpriteRenderer[] renderers = liveRoot.GetComponentsInChildren<SpriteRenderer>(true);
            for (int i = 0; i < renderers.Length; i++)
            {
                SpriteRenderer renderer = renderers[i];
                if (renderer != null && !spriteRendererAlphaDict.ContainsKey(renderer))
                {
                    spriteRendererAlphaDict.Add(renderer, renderer.color.a);
                }
            }
        }

        private void UpdateSpriteAlpha(float alpha)
        {
            if (spriteRendererAlphaDict == null)
            {
                return;
            }

            foreach (KeyValuePair<SpriteRenderer, float> pair in spriteRendererAlphaDict)
            {
                if (pair.Key == null)
                {
                    continue;
                }

                Color color = pair.Key.color;
                color.a = pair.Value * alpha;
                pair.Key.color = color;
            }
        }

        private void FadeIn()
        {
            UpdateSpriteAlpha(1f);
            if (fade != null)
            {
                fade.enabled = false;
            }
            if (autoLabel != null)
            {
                autoLabel.SetActive(isAuto);
            }
        }

        private void FadeOut()
        {
            if (autoLabel != null)
            {
                autoLabel.SetActive(false);
            }
            if (fade != null)
            {
                fade.enabled = true;
                Color color = fade.color;
                color.a = 1f;
                fade.color = color;
            }
        }

        private void OnPause()
        {
            countdownView?.StopCountdown();
        }

        private void OnConsecutiveAutoLivePause()
        {
            consecutiveAutoLiveView?.Pause();
        }

        private void OnConsecutiveAutoLiveResume()
        {
        }

        private void OnConsecutiveAutoLiveFinish()
        {
            consecutiveAutoLiveView?.Hide();
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

        private void LateUpdate()
        {
            if (deadMask == null)
            {
                return;
            }

            Color color = deadMask.color;
            float targetAlpha = life == 0 ? 1f : 0f;
            color.a = Mathf.Lerp(color.a, targetAlpha, 0.1f);
            deadMask.color = color;
        }
    }
}
