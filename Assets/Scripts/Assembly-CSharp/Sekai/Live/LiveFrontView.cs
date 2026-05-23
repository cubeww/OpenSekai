using System;
using System.Collections.Generic;
using Sekai.Core;
using Sekai.Core.Live;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

namespace Sekai.Live
{
	public class LiveFrontView : LiveViewBase
	{
		[SerializeField]
		private Transform liveRoot;

		[SerializeField]
		private SpriteRenderer fade;

		[SerializeField]
		private SpriteRenderer deadMask;

		[SerializeField]
		private LaneView laneView;

		[SerializeField]
		private MusicInfoView musicInfo;

		[SerializeField]
		private Renderer backgroundRenderer;

		[SerializeField]
		private JudgmentView judgmentView;

		[SerializeField]
		private JudgmentDescriptionView judgmentDescriptionView;

		[SerializeField]
		private ComboView comboView;

		[SerializeField]
		private ScoreView scoreView;

		[SerializeField]
		private LifeView lifeView;

		[SerializeField]
		private LiveResultView liveResultView;

		[SerializeField]
		private CutinManager cutinManager;

		[SerializeField]
		private SkillView skillView;

		[SerializeField]
		private CountdownView countdownView;

		[SerializeField]
		private ScreenEffectView screenEffectView;

		[SerializeField]
		private Transform effectRoot;

		[SerializeField]
		private Camera effectCamera;

		[SerializeField]
		private CameraSizeUpdater cameraSizeUpdater;

		[SerializeField]
		private Camera frontViewCamera;

		[SerializeField]
		private GameObject autoLabel;

		[SerializeField]
		private ConsecutiveAutoLiveView consecutiveAutoLiveView;

		[SerializeField]
		private NoteShowRateView noteShowRateView;

		private Material backgroundMaterial;

		private BaseLiveController baseController;
		private NotesViewManager notesViewManager;
		private TapEffectView tapEffect;
		private LongTapEffectView longTapEffectView;
		private MusicInfoView resolvedMusicInfo;
		private Tween musicStartTween;
		private Tween backgroundTween;
		private Tween startVoiceTween;
		private Tween spriteAlphaTween;
		private Tween fadeTween;
		private readonly Dictionary<SpriteRenderer, float> spriteRendererAlphaDict = new Dictionary<SpriteRenderer, float>();
		private LiveMusicData.CollaborationModeState collaborationModeState;
		private bool isAuto;
		private bool isPlayedHaptic;
		private int life;

		private static readonly int TapEffectViewMatrixId = Shader.PropertyToID("_TapEffectViewMatrix");
		private static readonly int TapEffectProjectionMatrixId = Shader.PropertyToID("_TapEffectProjectionMatrix");

		public ConsecutiveAutoLiveView ConsecutiveAutoLiveView => consecutiveAutoLiveView;

		public ScoreView ScoreView => scoreView;

		public override void Setup(BaseLiveController controller)
		{
			base.Setup(controller);
			baseController = controller;
			collaborationModeState = controller?.BootData?.MusicData?.CollaboModeState ?? LiveMusicData.CollaborationModeState.Off;
			life = LiveConfig.Life;
			isAuto = controller?.BootData?.IsAuto == true;
			SetupCameras();
			CriWare.CriAtomAudioRuntime.ReleaseFallbackAudioListenerIfExternalExists();
			SetupBackgroundMaterial(controller);
			SetupDeadMask();
			ResolveChildViews();
			lifeView?.Setup(OnPause);
			scoreView?.Setup(controller?.BootData?.MusicData);
			skillView?.Setup();
			comboView?.Setup(controller?.Settings?.UseAllPerfectEffect ?? false);
			countdownView?.Setup();
			screenEffectView?.Setup();
			cutinManager?.Setup(controller?.BootData);
			musicInfo?.Setup(controller?.BootData);
			noteShowRateView?.Setup(controller?.Settings);
			consecutiveAutoLiveView?.Setup(OnConsecutiveAutoLivePause, OnConsecutiveAutoLiveResume, OnConsecutiveAutoLiveFinish, frontViewCamera);
			laneView?.Setup(controller?.Settings);
			SetupNotesView();
			SetupTapEffect();
			CacheSpriteRendererAlpha();
			UpdateSpriteAlpha(0f);
			if (autoLabel != null)
			{
				autoLabel.SetActive(false);
			}
			liveResultView?.Setup(ShouldPlayVoiceSE());
		}

		public override void OnLoad()
		{
			SetupCameras();
			UpdateSpriteAlpha(0f);
			cutinManager?.Load();
			skillView?.Load();
		}

		public override void OnUnload()
		{
			liveResultView?.Unload();
			skillView?.Unload();
			cutinManager?.Dispose();
			tapEffect?.Dispose();
		}

		public override void MusicStart(float musicTime)
		{
			if (baseController?.BootData?.MusicData?.PlayStartEffectEnabled == true)
			{
				UpdateSpriteAlpha(0f);
				musicStartTween?.Kill();
				musicStartTween = DOVirtual.DelayedCall(0.1f, () =>
				{
					ResolveMusicInfo()?.Play(1f, false, 0f, null, null);
					FadeInBackground(() => PlayStartVoice(1f));
				}, true);
				return;
			}

			ResolveMusicInfo()?.Play(1f, false, 0f, null, null);
			SetBackgroundBrightness(GetTargetBackgroundBrightness());
		}

		public override void RhythmGameStart()
		{
			lifeView?.CalculateButtonBounds(frontViewCamera);
			consecutiveAutoLiveView?.CalculateButtonBounds(frontViewCamera);
			FadeIn();
		}

		public override void OnUpdate(float musicTime)
		{
			isPlayedHaptic = false;
			float viewTime = musicTime - (baseController?.BootData?.MusicData?.Music?.fillerSec ?? 0f);
			notesViewManager?.OnUpdate(viewTime);
			longTapEffectView?.Excute(viewTime);
			skillView?.OnUpdate(viewTime);
		}

		public override void CreateNotePool(Dictionary<(NoteCategory, NoteType), int> notePoolCount)
		{
			notesViewManager?.CreateNotePool(baseController, notePoolCount);
		}

		public override void SpawnNote(NoteBase note)
		{
			if (note == null)
			{
				return;
			}

			notesViewManager?.SpawnNote(note);
			if (note is LongNote)
			{
				longTapEffectView?.Add(note);
			}
		}

		public override void UnspawnNote(NoteBase note)
		{
			if (note == null)
			{
				return;
			}

			notesViewManager?.UnspawnNote(note);
			if (note is LongNote)
			{
				longTapEffectView?.Remove(note);
			}
		}

		public override void JudgmentNote(NoteBase note)
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

		public override void UpdateCombo(LiveScore score)
		{
			cutinManager?.UpdateCombo(score);
			comboView?.Excute(score);
		}

		public override void SetupScore(LiveScore score)
		{
			scoreView?.SetupScore(score);
		}

		public override void UpdateScore(LiveScore score)
		{
			UpdateScore(ref score, 0);
		}

		public override void UpdateScore(ref LiveScore score, int addScore)
		{
			scoreView?.UpdateScore(ref score, addScore);
		}

		public override void UpdateLife(LiveScore score)
		{
			life = score.life;
			lifeView?.Excute(life);
		}

		public override void Result(int result)
		{
			liveResultView?.Execute((LiveResultAnimationType)result);
		}

		public override void Pause()
		{
			countdownView?.StopCountdown();
		}

		public override void Resume()
		{
			countdownView?.StartCountdown();
		}

		public override void Retry()
		{
			musicStartTween?.Kill();
			backgroundTween?.Kill();
			startVoiceTween?.Kill();
			spriteAlphaTween?.Kill();
			fadeTween?.Kill();
			comboView?.Clear();
			scoreView?.Clear();
			lifeView?.Clear();
			cutinManager?.Clear();
			skillView?.Clear();
			notesViewManager?.Clear();
			tapEffect?.Clear();
			longTapEffectView?.Clear();
			life = LiveConfig.Life;
			if (autoLabel != null)
			{
				autoLabel.SetActive(false);
			}
			UpdateSpriteAlpha(0f);
		}

		public override void Finish()
		{
			FadeOut(1f);
		}

		public override void Finish(float duration)
		{
			FadeOut(duration);
		}

		public override void Countdown()
		{
			countdownView?.StartCountdown();
		}

		public void UpdateSkill(SkillData skillData, float time, LiveScore score, bool isEncore)
		{
			skillView?.UpdateSkill(skillData, time);
			cutinManager?.UpdateSkill(skillData, time, score, isEncore);
		}

		private void SetupBackgroundMaterial(BaseLiveController controller)
		{
			ResolveBackgroundRenderer();
			if (backgroundRenderer == null)
			{
				return;
			}

			if (backgroundMaterial != null)
			{
				backgroundMaterial.DOKill();
				Destroy(backgroundMaterial);
			}

			Material sourceMaterial = backgroundRenderer.sharedMaterial ?? backgroundRenderer.material;
			if (sourceMaterial != null)
			{
				backgroundMaterial = new Material(sourceMaterial);
				backgroundRenderer.material = backgroundMaterial;
				backgroundMaterial.mainTexture = controller?.BackgroundTexture;
				SetBackgroundBrightness(0f);
			}

			backgroundRenderer.enabled = true;
		}

		private void SetupDeadMask()
		{
			if (deadMask == null || cameraSizeUpdater == null)
			{
				return;
			}

			float orthographicSize = cameraSizeUpdater.OrthographicSize;
			float height = orthographicSize * 2f;
			float aspect = Screen.height > 0 ? (float)Screen.width / Screen.height : 16f / 9f;
			deadMask.size = new Vector2(height * aspect, height);
		}

		private void ResolveBackgroundRenderer()
		{
			if (backgroundRenderer != null)
			{
				return;
			}

			Transform backgroundTransform = transform.Find("Background");
			backgroundRenderer = backgroundTransform != null ? backgroundTransform.GetComponent<Renderer>() : null;
		}

		private void FadeInBackground(TweenCallback onComplete = null)
		{
			float brightness = GetTargetBackgroundBrightness();
			if (backgroundMaterial == null)
			{
				onComplete?.Invoke();
				return;
			}

			backgroundTween?.Kill();
			backgroundTween = backgroundMaterial.DOColor(new Color(brightness, brightness, brightness, 1f), 2f).OnComplete(onComplete);
		}

		private float GetTargetBackgroundBrightness()
		{
			return Mathf.Clamp01(baseController?.Settings?.Brightness ?? 1f);
		}

		private void SetBackgroundBrightness(float brightness)
		{
			if (backgroundMaterial == null)
			{
				return;
			}

			float clamped = Mathf.Clamp01(brightness);
			backgroundMaterial.color = new Color(clamped, clamped, clamped, 1f);
		}

		private void SetFadeAlpha(float alpha)
		{
			if (fade == null)
			{
				return;
			}

			Color color = fade.color;
			float clamped = Mathf.Clamp01(alpha);
			color.r = 0f;
			color.g = 0f;
			color.b = 0f;
			color.a = clamped;
			fade.color = color;
		}

		private void FadeIn()
		{
			fadeTween?.Kill();
			if (fade != null)
			{
				fade.enabled = false;
				SetFadeAlpha(0f);
			}

			if (baseController?.BootData?.MusicData?.PlayStartEffectEnabled == true)
			{
				spriteAlphaTween?.Kill();
				spriteAlphaTween = DOVirtual.Float(0f, 1f, 2f, UpdateSpriteAlpha)
					.OnComplete(UpdateAutoLabel);
				return;
			}

			UpdateSpriteAlpha(1f);
			UpdateAutoLabel();
		}

		private void FadeOut(float duration)
		{
			if (autoLabel != null)
			{
				autoLabel.SetActive(false);
			}
			if (fade == null)
			{
				return;
			}

			fade.enabled = true;
			fadeTween?.Kill();
			fadeTween = DOVirtual.Float(0f, 1f, Mathf.Max(0f, duration), SetFadeAlpha);
		}

		private void UpdateSpriteAlpha(float alpha)
		{
			if (spriteRendererAlphaDict.Count == 0)
			{
				return;
			}

			foreach (KeyValuePair<SpriteRenderer, float> pair in spriteRendererAlphaDict)
			{
				SpriteRenderer renderer = pair.Key;
				if (renderer == null)
				{
					continue;
				}

				Color color = renderer.color;
				color.a = pair.Value * alpha;
				renderer.color = color;
			}
		}

		private void CacheSpriteRendererAlpha()
		{
			spriteRendererAlphaDict.Clear();
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

		private void UpdateAutoLabel()
		{
			if (autoLabel != null)
			{
				autoLabel.SetActive(isAuto);
			}
		}

		private void SetupCameras()
		{
			ResolveCameraReferences();
			if (frontViewCamera == null || effectCamera == null)
			{
				return;
			}

			var frontCameraData = GetOrAddCameraData(frontViewCamera);
			var effectCameraData = GetOrAddCameraData(effectCamera);
			if (frontCameraData == null || effectCameraData == null)
			{
				return;
			}

			frontCameraData.renderType = CameraRenderType.Base;
			effectCameraData.renderType = CameraRenderType.Overlay;

			// The original prefab is built for the legacy multi-camera path. In URP,
			// leaving this camera as a standalone Base camera makes its transparent
			// blue clear color overwrite the screen, so it has to render as an overlay
			// stacked after FrontCamera.
			effectCamera.clearFlags = CameraClearFlags.Depth;
			if (!frontCameraData.cameraStack.Contains(effectCamera))
			{
				frontCameraData.cameraStack.Add(effectCamera);
			}

			frontViewCamera.GetSekaiAdditionalCameraData()?.Setup(0, 2);
			effectCamera.GetSekaiAdditionalCameraData()?.Setup(1, 2);
			if (baseController != null)
			{
				baseController.BaseCamera = frontViewCamera;
			}
		}

		private void SetupNotesView()
		{
			Transform parent = liveRoot != null ? liveRoot : transform;
			notesViewManager = new NotesViewManager();
			notesViewManager.Setup(parent);
		}

		private void SetupTapEffect()
		{
			Transform parent = effectRoot != null ? effectRoot : transform;
			if (tapEffect == null)
			{
				tapEffect = parent.GetComponent<TapEffectView>();
				if (tapEffect == null)
				{
					tapEffect = parent.gameObject.AddComponent<TapEffectView>();
				}
			}
			tapEffect.Setup(baseController);

			if (longTapEffectView == null)
			{
				GameObject longTapObject = new GameObject("LongTapEffectView", typeof(LongTapEffectView));
				longTapObject.transform.SetParent(parent, false);
				longTapEffectView = longTapObject.GetComponent<LongTapEffectView>();
			}
			longTapEffectView.Setup();

			parent.position = transform.position;
			if (effectCamera != null)
			{
				effectCamera.fieldOfView = SekaiCameraAspect.CalculateVerticalFov(effectCamera.fieldOfView);
				Shader.SetGlobalMatrix(TapEffectViewMatrixId, effectCamera.worldToCameraMatrix);
				Shader.SetGlobalMatrix(TapEffectProjectionMatrixId, GL.GetGPUProjectionMatrix(effectCamera.projectionMatrix, false));
				effectCamera.enabled = false;
			}
		}

		private MusicInfoView ResolveMusicInfo()
		{
			if (resolvedMusicInfo != null)
			{
				return resolvedMusicInfo;
			}

			resolvedMusicInfo = musicInfo as MusicInfoView;
			if (resolvedMusicInfo == null)
			{
				resolvedMusicInfo = GetComponentInChildren<MusicInfoView>(true);
			}
			return resolvedMusicInfo;
		}

		private void ResolveChildViews()
		{
			laneView ??= GetComponentInChildren<LaneView>(true);
			resolvedMusicInfo = musicInfo != null ? musicInfo : GetComponentInChildren<MusicInfoView>(true);
			musicInfo = resolvedMusicInfo;
			judgmentView ??= GetComponentInChildren<JudgmentView>(true);
			judgmentDescriptionView ??= GetComponentInChildren<JudgmentDescriptionView>(true);
			comboView ??= GetComponentInChildren<ComboView>(true);
			scoreView ??= GetComponentInChildren<ScoreView>(true);
			lifeView ??= GetComponentInChildren<LifeView>(true);
			liveResultView ??= GetComponentInChildren<LiveResultView>(true);
			cutinManager ??= GetComponentInChildren<CutinManager>(true);
			skillView ??= GetComponentInChildren<SkillView>(true);
			countdownView ??= GetComponentInChildren<CountdownView>(true);
			screenEffectView ??= GetComponentInChildren<ScreenEffectView>(true);
			consecutiveAutoLiveView ??= GetComponentInChildren<ConsecutiveAutoLiveView>(true);
			noteShowRateView ??= GetComponentInChildren<NoteShowRateView>(true);
		}

		private void PlayStartVoice(float durationScale)
		{
			if (collaborationModeState != LiveMusicData.CollaborationModeState.On)
			{
				return;
			}

			startVoiceTween?.Kill();
			startVoiceTween = DOVirtual.DelayedCall(Mathf.Max(0f, durationScale) * 0.1f, () =>
			{
				SoundManager.Instance.PlayIngameVoiceOneShot(LiveSoundDefine.SE_LIVE_START);
			}, true);
		}

		private void ResolveCameraReferences()
		{
			if (frontViewCamera == null)
			{
				var frontCameraTransform = transform.Find("FrontCamera");
				frontViewCamera = frontCameraTransform != null ? frontCameraTransform.GetComponent<Camera>() : null;
			}

			if (effectCamera == null)
			{
				var effectCameraTransform = transform.Find("EffectCameraRoot/Camera");
				effectCamera = effectCameraTransform != null ? effectCameraTransform.GetComponent<Camera>() : null;
			}

			if (effectRoot == null)
			{
				effectRoot = transform.Find("EffectCameraRoot");
			}

			if (cameraSizeUpdater == null)
			{
				cameraSizeUpdater = GetComponent<CameraSizeUpdater>();
			}
		}

		private static UniversalAdditionalCameraData GetOrAddCameraData(Camera camera)
		{
			if (camera == null)
			{
				return null;
			}

			var cameraData = camera.GetComponent<UniversalAdditionalCameraData>();
			if (cameraData == null)
			{
				cameraData = camera.gameObject.AddComponent<UniversalAdditionalCameraData>();
			}

			return cameraData;
		}

		private void OnPause()
		{
			countdownView?.StopCountdown();
			baseController?.Pause();
		}

		private void OnConsecutiveAutoLivePause()
		{
			if (baseController is SoloLiveController soloLiveController && soloLiveController.IsPlayableState)
			{
				soloLiveController.PauseLive();
			}
		}

		private void OnConsecutiveAutoLiveResume()
		{
			if (baseController is SoloLiveController soloLiveController)
			{
				soloLiveController.ResumeNoCountDown();
			}
			else
			{
				baseController?.Resume();
			}
		}

		private void OnConsecutiveAutoLiveFinish()
		{
			consecutiveAutoLiveView?.Hide();
			if (baseController is SoloLiveController soloLiveController)
			{
				soloLiveController.ShowConsecutiveAutoLiveRetireDialog();
			}
			else
			{
				baseController?.CallPreExit();
			}
		}

		private void Effect(INote note)
		{
			tapEffect?.Excute(note, CheckPlayedHaptic);
			screenEffectView?.Excute(note);
		}

		private bool CheckPlayedHaptic()
		{
			if (isPlayedHaptic)
			{
				return true;
			}

			isPlayedHaptic = true;
			return false;
		}

		private bool ShouldPlayVoiceSE()
		{
			return collaborationModeState == LiveMusicData.CollaborationModeState.On && baseController?.BootData?.IsAuto != true;
		}

		private void OnDestroy()
		{
			musicStartTween?.Kill();
			backgroundTween?.Kill();
			startVoiceTween?.Kill();
			spriteAlphaTween?.Kill();
			fadeTween?.Kill();
			if (backgroundMaterial != null)
			{
				backgroundMaterial.DOKill();
				Destroy(backgroundMaterial);
				backgroundMaterial = null;
			}
		}
	}
}
