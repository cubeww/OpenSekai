using DG.Tweening;
using Sekai.Core;
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
        [SerializeField] private CameraFOVUpdater cameraFOVUpdater;
        [SerializeField] private Camera frontViewCamera;
        [SerializeField] private float previewBrightness = 1f;

        private BaseLiveController baseController;
        private Material backgroundMaterial;
        private Mesh runtimeBackgroundMesh;
        private Dictionary<SpriteRenderer, float> spriteRendererAlphaDict;
        private Tween liveUiFadeTween;
        private Tween musicStartDelayTween;
        private Tween backgroundBrightnessTween;
        private bool isAuto;
        private bool isPlayedHaptic;
        private bool hasEffectCameraBaseFov;
        private float effectCameraBaseFov;
        private int lastTapEffectMatrixScreenWidth = -1;
        private int lastTapEffectMatrixScreenHeight = -1;
        private int pendingTapEffectMatrixRefreshFrames;
        private int life;
        private NotesViewManager notesViewManager;
        private TapEffectView tapEffect;
        private LongTapEffectView longTapEffectView;
        // Original prefab keeps this at 0; URP 2D sorting draws it over negative-order lanes in this project.
        private const int BackgroundSortingOrder = -200;
        private const int TapEffectMatrixDeferredRefreshFrames = 2;

        public override void Setup(BaseLiveController baseController)
        {
            base.Setup(baseController);
            this.baseController = baseController;
            life = GetLiveMaxLife();

            lifeView?.Setup(OnPause, life);
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
            screenEffectView?.Setup(baseController);
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
            consecutiveAutoLiveView?.Hide();

            isAuto = baseController != null && baseController.BootData != null && baseController.BootData.IsAuto;
            laneView?.Setup(baseController != null ? baseController.Settings : null);
            notesViewManager = new NotesViewManager();
            notesViewManager.Setup(liveRoot);
            notesViewManager.SetAssets(notePrefabs, longNoteLineTexture, guideLineTexture, simultaneousLineTexture, longNoteLineMaterial, pairNoteLineMaterial);
            CacheSpriteRendererAlphas();
            UpdateSpriteAlpha(0f);
        }

        public override void CreateNotePool(Dictionary<(NoteCategory, NoteType), int> notePoolCount)
        {
            notesViewManager?.CreateNotePool(baseController, notePoolCount);
        }

        public override void SetupScore(LiveScore score)
        {
            cutinManager?.SetupScore(score);
            scoreView?.SetupScore(score);
            lifeView?.Excute(score.life);
            comboView?.Excute(score);
            life = score.life;
        }

        public override void ChangeScoreCalculator(ScoreGaugeCalculator scoreGaugeCalculator)
        {
            scoreView?.ChangeScoreCalculator(scoreGaugeCalculator);
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
            KillLiveTweens();
            UpdateSpriteAlpha(0f);
            musicStartDelayTween = DOVirtual.DelayedCall(0.1f, PlayMusicInfoAndFadeBackground, false).SetTarget(this);
        }

        public override void RhythmGameStart()
        {
            RefreshScreenDependentLayout();
            FadeIn();
        }

        public override void OnScreenSizeChanged()
        {
            RefreshScreenDependentLayout();
            QueueTapEffectMatrixRefresh();
        }

        public override void Retry()
        {
            notesViewManager?.Clear();
            longTapEffectView?.Clear();
            tapEffect?.Clear();
            comboView?.Clear();
            scoreView?.Clear();
            lifeView?.Clear();
            cutinManager?.Clear();
            skillView?.Clear();
            life = GetLiveMaxLife();
            if (autoLabel != null)
            {
                autoLabel.SetActive(false);
            }
            consecutiveAutoLiveView?.Hide();
            KillLiveTweens();
            UpdateSpriteAlpha(0f);
        }

        public override void Finish()
        {
            FadeOut();
        }

        public override void Result(LiveResultAnimationType animationType)
        {
            liveResultView?.Execute(animationType);
        }

        private void SetupBackground()
        {
            if (backgroundRenderer == null)
            {
                return;
            }

            EnsureBackgroundMesh();
            backgroundRenderer.sortingOrder = BackgroundSortingOrder;

            float height = cameraSizeUpdater != null && cameraSizeUpdater.OrthographicSize > 0f ? cameraSizeUpdater.OrthographicSize * 2f : 10f;
            float aspect = Screen.height == 0 ? 1f : (float)Screen.width / Screen.height;
            backgroundRenderer.transform.localScale = new Vector3(height * aspect, height, 1f);

            Material source = backgroundRenderer.material;
            if (source == null)
            {
                return;
            }

            backgroundMaterial = new Material(source);
            if (backgroundMaterial.HasProperty("_ZWrite"))
            {
                backgroundMaterial.SetFloat("_ZWrite", 0f);
            }
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

            tapEffect = effectRoot.GetComponent<TapEffectView>();
            if (tapEffect == null)
            {
                tapEffect = effectRoot.gameObject.AddComponent<TapEffectView>();
            }
            tapEffect.Setup(baseController, tapEffectPrefabs, tapSingleEffectPrefab, tapFlickEffectPrefab, tapLoopEffectPrefab, liveSoundPlayer);

            Transform longTapEffectTransform = effectRoot.Find("LongTapEffect");
            if (longTapEffectTransform != null)
            {
                longTapEffectView = longTapEffectTransform.GetComponent<LongTapEffectView>();
                if (longTapEffectView == null)
                {
                    longTapEffectView = longTapEffectTransform.gameObject.AddComponent<LongTapEffectView>();
                }
            }
            else
            {
                GameObject longTapEffectObject = new GameObject("LongTapEffect");
                longTapEffectView = longTapEffectObject.AddComponent<LongTapEffectView>();
                longTapEffectView.transform.SetParent(effectRoot, false);
            }
            longTapEffectView.Setup(longHoldAuraPrefab, longHoldGenPrefab, criticalLongHoldAuraPrefab, criticalLongHoldGenPrefab, longTapEffectPoolCount, baseController, liveSoundPlayer);

            effectRoot.position = transform.position;
            RefreshTapEffectCameraMatrices();
        }

        private void RefreshTapEffectCameraMatrices()
        {
            if (effectCamera == null)
            {
                CacheTapEffectMatrixScreenSize();
                return;
            }

            if (!hasEffectCameraBaseFov)
            {
                effectCameraBaseFov = effectCamera.fieldOfView;
                hasEffectCameraBaseFov = true;
            }

            if (effectRoot != null)
            {
                effectRoot.position = transform.position;
            }

            effectCamera.rect = frontViewCamera != null ? frontViewCamera.rect : new Rect(0f, 0f, 1f, 1f);
            effectCamera.aspect = GetTapEffectCameraAspect();
            effectCamera.fieldOfView = SekaiCameraAspect.CalculateVerticalFov(effectCameraBaseFov);
            effectCamera.ResetProjectionMatrix();

            Shader.SetGlobalMatrix(ShaderPropertyID.TAP_EFFECT_VIEW_MATRIX_ID, effectCamera.worldToCameraMatrix);
            Shader.SetGlobalMatrix(ShaderPropertyID.TAP_EFFECT_PROJECTION_MATRIX_ID, GL.GetGPUProjectionMatrix(effectCamera.projectionMatrix, false));
            effectCamera.enabled = false;
            CacheTapEffectMatrixScreenSize();
        }

        private float GetTapEffectCameraAspect()
        {
            Rect rect = frontViewCamera != null ? frontViewCamera.rect : new Rect(0f, 0f, 1f, 1f);
            float width = Screen.width * Mathf.Max(rect.width, 0.0001f);
            float height = Screen.height * Mathf.Max(rect.height, 0.0001f);
            return height > 0f ? width / height : SekaiCameraAspect.TargetAspect;
        }

        private void QueueTapEffectMatrixRefresh()
        {
            pendingTapEffectMatrixRefreshFrames = TapEffectMatrixDeferredRefreshFrames;
        }

        private void RefreshTapEffectCameraMatricesIfNeeded()
        {
            if (lastTapEffectMatrixScreenWidth == Screen.width &&
                lastTapEffectMatrixScreenHeight == Screen.height &&
                pendingTapEffectMatrixRefreshFrames <= 0)
            {
                return;
            }

            RefreshTapEffectCameraMatrices();
            if (pendingTapEffectMatrixRefreshFrames > 0)
            {
                pendingTapEffectMatrixRefreshFrames--;
            }
        }

        private void CacheTapEffectMatrixScreenSize()
        {
            lastTapEffectMatrixScreenWidth = Screen.width;
            lastTapEffectMatrixScreenHeight = Screen.height;
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
            if (liveUiFadeTween != null)
            {
                liveUiFadeTween.Kill();
                liveUiFadeTween = null;
            }

            UpdateSpriteAlpha(0f);
            liveUiFadeTween = DOVirtual.Float(0f, 1f, 2f, UpdateSpriteAlpha)
                .SetEase(Ease.InOutQuad)
                .SetTarget(this)
                .OnComplete(() =>
                {
                    liveUiFadeTween = null;
                    if (autoLabel != null)
                    {
                        bool showAutoLabel = isAuto && !AutoLiveUtility.IsRunningConsecutiveAutoLive();
                        autoLabel.SetActive(showAutoLabel);
                    }
                });
            if (fade != null)
            {
                fade.enabled = false;
            }
            if (autoLabel != null)
            {
                autoLabel.SetActive(false);
            }
        }

        private void FadeOut()
        {
            if (autoLabel != null)
            {
                autoLabel.SetActive(false);
            }
            KillLiveTweens();
            UpdateSpriteAlpha(0f);
            if (fade != null)
            {
                fade.enabled = true;
                Color color = fade.color;
                color.a = 1f;
                fade.color = color;
            }
        }

        private void PlayMusicInfoAndFadeBackground()
        {
            musicInfo?.Play();
            musicStartDelayTween = null;
            StartBackgroundBrightnessFade(GetTargetBrightness(), 2f);
        }

        private void StartBackgroundBrightnessFade(float brightness, float duration)
        {
            if (backgroundMaterial == null)
            {
                return;
            }

            if (backgroundBrightnessTween != null)
            {
                backgroundBrightnessTween.Kill();
                backgroundBrightnessTween = null;
            }

            Color targetColor = new Color(brightness, brightness, brightness, 1f);
            backgroundBrightnessTween = backgroundMaterial.DOColor(targetColor, duration)
                .SetTarget(this)
                .OnComplete(() => backgroundBrightnessTween = null);
        }

        private float GetTargetBrightness()
        {
            if (baseController != null && baseController.Settings != null)
            {
                return baseController.Settings.Brightness;
            }

            return previewBrightness;
        }

        private int GetLiveMaxLife()
        {
            LiveBundleBuildData data = baseController != null && baseController.BootData != null
                ? baseController.BootData.BundleBuildData
                : null;
            if (data != null)
            {
                return data.Life;
            }

            return LiveConfig.Life;
        }

        private void KillLiveTweens()
        {
            if (liveUiFadeTween != null)
            {
                liveUiFadeTween.Kill();
                liveUiFadeTween = null;
            }

            if (musicStartDelayTween != null)
            {
                musicStartDelayTween.Kill();
                musicStartDelayTween = null;
            }

            if (backgroundBrightnessTween != null)
            {
                backgroundBrightnessTween.Kill();
                backgroundBrightnessTween = null;
            }
        }

        private void OnPause()
        {
            countdownView?.StopCountdown();
            baseController?.Pause();
        }

        public override void Resume(float time)
        {
        }

        public override void Countdown()
        {
            countdownView?.StartCountdown();
        }

        private void OnConsecutiveAutoLivePause()
        {
            consecutiveAutoLiveView?.Pause();
            baseController?.Pause();
        }

        private void OnConsecutiveAutoLiveResume()
        {
            if (baseController is SoloLiveController soloLiveController)
            {
                soloLiveController.ResumeNoCountDown();
                return;
            }

            baseController?.Resume();
        }

        private void OnConsecutiveAutoLiveFinish()
        {
            consecutiveAutoLiveView?.Hide();
        }

        private void RefreshScreenDependentLayout()
        {
            cameraSizeUpdater?.ForceUpdate();
            cameraFOVUpdater?.ForceUpdate();
            UpdateBackgroundSize();
            RefreshTapEffectCameraMatrices();
            lifeView?.CalculateButtonBounds(frontViewCamera);
            consecutiveAutoLiveView?.CalculateButtonBounds(frontViewCamera);
        }

        private void UpdateBackgroundSize()
        {
            float height = cameraSizeUpdater != null && cameraSizeUpdater.OrthographicSize > 0f ? cameraSizeUpdater.OrthographicSize * 2f : 10f;
            float aspect = Screen.height == 0 ? 1f : (float)Screen.width / Screen.height;

            if (backgroundRenderer != null)
            {
                backgroundRenderer.transform.localScale = new Vector3(height * aspect, height, 1f);
            }

            if (deadMask != null)
            {
                deadMask.size = new Vector2(height * aspect, height);
            }
        }

        public override void OnUpdate(float time)
        {
            isPlayedHaptic = false;
            float viewTime = time;
            if (baseController != null &&
                baseController.BootData != null &&
                baseController.BootData.MusicData != null &&
                baseController.BootData.MusicData.Music != null)
            {
                viewTime -= baseController.BootData.MusicData.Music.fillerSec;
            }

            skillView?.OnUpdate(viewTime);
            notesViewManager?.OnUpdate(viewTime);
            longTapEffectView?.Excute(viewTime);
        }

        public override void SpawnNote(INote note)
        {
            notesViewManager?.SpawnNote(note);
            if (note is LongNote)
            {
                longTapEffectView?.Add(note);
            }
        }

        public override void UnspawnNote(INote note)
        {
            notesViewManager?.UnspawnNote(note);
            if (note is LongNote)
            {
                longTapEffectView?.Remove(note);
            }
        }

        public override void JudgmentNote(INote note)
        {
            if (note == null)
            {
                return;
            }

            Effect(note);
            judgmentView?.Excute(note.JudgeInfo);
            judgmentDescriptionView?.Excute(note.JudgeInfo);
        }

        public override void Unpicked(int lane, ref LiveTouch touch)
        {
            tapEffect?.Unpicked(lane, ref touch);
        }

        public override void UpdateScore(ref LiveScore score, int addScore)
        {
            scoreView?.UpdateScore(ref score, addScore);
        }

        public override void UpdateCombo(LiveScore score)
        {
            cutinManager?.PlayCombo(score);
            comboView?.Excute(score);
        }

        public override void UpdateLife(int life)
        {
            lifeView?.Excute(life);
            this.life = life;
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
            KillLiveTweens();

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

        private void Effect(INote note)
        {
            tapEffect?.Excute(note, CheckPlayedHaptic);
            screenEffectView?.Excute(note);
        }

        private bool CheckPlayedHaptic()
        {
            bool result = isPlayedHaptic;
            isPlayedHaptic = true;
            return result;
        }

        private void LateUpdate()
        {
            RefreshTapEffectCameraMatricesIfNeeded();
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
