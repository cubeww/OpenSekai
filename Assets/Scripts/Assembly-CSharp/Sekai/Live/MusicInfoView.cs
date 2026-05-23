using System;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using SafeArea;
using Sekai.MusicScoreMaker.Common;
using Sekai.UI;
using UnityEngine;

namespace Sekai.Live
{
	public class MusicInfoView : MonoBehaviour
	{
		[SerializeField]
		private SafeAreaAdjuster safeAreaAdjuster;

		[SerializeField]
		private CanvasGroup viewGroup;

		[SerializeField]
		private CanvasGroup[] infoGroup;

		[SerializeField]
		private Transform mask;

		[SerializeField]
		private CustomRawImage jacket;

		[SerializeField]
		private UIPartsMusicDifficulty difficultyJacket;

		[SerializeField]
		private CustomTextMesh difficultyJacketText;

		[SerializeField]
		private CustomTextMesh titleLabel;

		[SerializeField]
		private CustomTextMesh creatorLabel;

		[SerializeField]
		private CustomTextMesh singerLabel;

		[SerializeField]
		private UIPartsMusicCollaborationRibbon collaborationRibbon;

		[SerializeField]
		private GameObject _musicInfoRoot;

		[SerializeField]
		private GameObject _scoreInfoRoot;

		[SerializeField]
		private CustomTextMesh _scoreTitleLabel;

		[SerializeField]
		private CustomTextMesh _scoreCreatorLabel;

		[SerializeField]
		private GameObject _scoreIcon;

		public float JacketFadeOutDelaySec;

		private bool _playStartEffectEnabled;
		private readonly List<Tween> _activeTweens = new List<Tween>();
		private Vector3 _difficultyJacketInitialLocalPosition;
		private bool _hasDifficultyJacketInitialLocalPosition;

		public float AnimationDurationSec
		{
			get
			{
				return 6f;
			}
		}

		public void Setup(LiveBootDataBase bootData)
		{
			LiveMusicData musicData = bootData?.MusicData;
			_playStartEffectEnabled = musicData?.PlayStartEffectEnabled ?? true;
			CacheDifficultyJacketPosition();
			ResetDifficultyJacketPosition();
			KillTweens();

			if (viewGroup != null)
			{
				viewGroup.alpha = 0f;
			}
			if (infoGroup != null)
			{
				foreach (CanvasGroup group in infoGroup)
				{
					if (group != null)
					{
						group.alpha = 0f;
					}
				}
			}

			if (difficultyJacket != null)
			{
				difficultyJacket.Setup(musicData?.DifficultyEnum ?? MusicDifficulty.none);
			}
			SetupDifficulty(musicData?.DifficultyString);

			FreeLiveBootData freeLiveBootData = bootData as FreeLiveBootData;
			bool isCustomScore = freeLiveBootData != null && bootData.IsCustomMusicScore;
			if (_scoreIcon != null)
			{
				_scoreIcon.SetActive(isCustomScore);
			}
			if (_scoreInfoRoot != null)
			{
				_scoreInfoRoot.SetActive(isCustomScore);
			}

			string title = musicData?.Music?.title ?? string.Empty;
			string lyricist = musicData?.Music?.lyricist ?? string.Empty;
			string composer = musicData?.Music?.composer ?? string.Empty;
			string arranger = musicData?.Music?.arranger ?? string.Empty;
			string singer = musicData?.Vocal?.caption ?? string.Empty;

			if (isCustomScore)
			{
				ApplyCustomScoreInfo(freeLiveBootData, ref title, ref lyricist, ref composer, ref arranger, ref singer);
			}

			titleLabel?.SetText(title);
			creatorLabel?.SetText(BuildCreatorText(lyricist, composer, arranger));
			singerLabel?.SetText(singer);

			if (collaborationRibbon != null)
			{
				bool hasCollaboration = musicData != null && musicData.IsCollaboration && musicData.Collaboration != null;
				collaborationRibbon.gameObject.SetActive(hasCollaboration);
				if (hasCollaboration)
				{
					collaborationRibbon.SetText(musicData.Collaboration.label);
				}
			}

			if (isCustomScore)
			{
				_scoreTitleLabel?.SetText(freeLiveBootData.CustomMusicScoreTitle ?? title);
				_scoreCreatorLabel?.SetText(freeLiveBootData.CustomMusicScoreAuthorName ?? string.Empty);
			}

			Texture2D texture = LoadJacketTexture(bootData, musicData);
			if (jacket != null)
			{
				jacket.texture = texture;
				jacket.enabled = texture != null;
			}
		}

		private void SetupDifficulty(string difficulty)
		{
			if (string.IsNullOrEmpty(difficulty))
			{
				difficulty = "none";
			}

			difficultyJacket?.Setup(difficulty);
			if (difficultyJacketText != null)
			{
				difficultyJacketText.SetText(difficulty.ToUpperInvariant());
				difficultyJacketText.gameObject.SetActive(true);
			}
		}

		public void Play(float durationScale = 1f, bool disableOnlyJacketOnFinish = false, float bgFadeoutDuration = 0f, Action onFinish = null, Action onFinishViewGroupFade = null)
		{
			if (!_playStartEffectEnabled)
			{
				gameObject.SetActive(false);
				return;
			}

			KillTweens();
			safeAreaAdjuster?.Setup();
			safeAreaAdjuster?.Apply();
			if (difficultyJacket != null && jacket != null)
			{
				ResetDifficultyJacketPosition();
				difficultyJacket.transform.position = jacket.transform.position;
			}

			float scale = Mathf.Max(durationScale, 0.001f);
			float fadeDuration = 0.5f * scale;
			float finishDelay = Mathf.Max(0f, JacketFadeOutDelaySec * scale + bgFadeoutDuration);

			if (viewGroup != null)
			{
				viewGroup.alpha = 0f;
			}
			if (infoGroup != null)
			{
				foreach (CanvasGroup group in infoGroup)
				{
					if (group == null)
					{
						continue;
					}
					group.alpha = 0f;
				}
			}

			Track(DOVirtual.DelayedCall(0.01f, () =>
			{
				if (this == null)
				{
					return;
				}

				gameObject.SetActive(true);
				if (viewGroup != null)
				{
					Track(viewGroup.DOFade(0f, 0f));
					Track(viewGroup.DOFade(1f, fadeDuration));
				}

				Track(DOVirtual.DelayedCall(0.2f, () =>
				{
					if (infoGroup == null)
					{
						return;
					}
					foreach (CanvasGroup group in infoGroup)
					{
						if (group != null)
						{
							Track(group.DOFade(1f, fadeDuration));
						}
					}
				}, true));

				Track(DOVirtual.DelayedCall(0.5f, () => onFinishViewGroupFade?.Invoke(), true));

				Track(DOVirtual.DelayedCall(0.8f, () =>
				{
					if (difficultyJacket == null)
					{
						return;
					}

					Transform difficultyTransform = difficultyJacket.transform;
					Vector3 localPosition = difficultyTransform.localPosition;
					localPosition.x -= 44f;
					localPosition.y -= 44f;
					localPosition.z = 0f;
					Track(difficultyTransform.DOLocalMove(localPosition, 2f, false).SetEase(Ease.OutQuad));
				}, true));

				PlayBackgroundAnimation(scale * 1.5f, scale * 2.5f);
				PlayBackgroundAnimation(scale * 3.5f, scale * 2.5f);

				Track(DOVirtual.DelayedCall(finishDelay, () =>
				{
					if (disableOnlyJacketOnFinish)
					{
						if (infoGroup == null)
						{
							return;
						}
						foreach (CanvasGroup group in infoGroup)
						{
							if (group != null)
							{
								Track(group.DOFade(0f, fadeDuration));
							}
						}
						return;
					}

					if (viewGroup == null)
					{
						gameObject.SetActive(false);
						onFinish?.Invoke();
						return;
					}

					Track(viewGroup.DOFade(0f, fadeDuration).OnComplete(() =>
					{
						gameObject.SetActive(false);
						onFinish?.Invoke();
					}));
				}, true));
			}, true));
		}

		public void PlayBackgroundAnimation(float delayTimeSec, float duration)
		{
			Track(DOVirtual.DelayedCall(delayTimeSec, () =>
			{
				if (mask == null)
				{
					return;
				}

				Track(mask.DOLocalMoveY(-810f, 0f, false));
				Track(mask.DOLocalMoveY(810f, Mathf.Max(0f, duration), false));
			}, true));
		}

		public void FadeViewGroup(float alpha)
		{
			if (viewGroup != null)
			{
				viewGroup.alpha = alpha;
			}
		}

		public MusicInfoView()
		{
			JacketFadeOutDelaySec = 5.5f;
		}

		private void Awake()
		{
			CacheDifficultyJacketPosition();
		}

		private void OnDisable()
		{
			KillTweens();
		}

		private void OnDestroy()
		{
			KillTweens();
		}

		private void CacheDifficultyJacketPosition()
		{
			if (_hasDifficultyJacketInitialLocalPosition || difficultyJacket == null)
			{
				return;
			}

			_difficultyJacketInitialLocalPosition = difficultyJacket.transform.localPosition;
			_hasDifficultyJacketInitialLocalPosition = true;
		}

		private void ResetDifficultyJacketPosition()
		{
			if (_hasDifficultyJacketInitialLocalPosition && difficultyJacket != null)
			{
				difficultyJacket.transform.localPosition = _difficultyJacketInitialLocalPosition;
			}
		}

		private void Track(Tween tween)
		{
			if (tween != null)
			{
				_activeTweens.Add(tween);
			}
		}

		private void KillTweens()
		{
			for (int i = 0; i < _activeTweens.Count; i++)
			{
				Tween tween = _activeTweens[i];
				if (tween != null && tween.IsActive())
				{
					tween.Kill();
				}
			}
			_activeTweens.Clear();

			viewGroup?.DOKill();
			if (infoGroup != null)
			{
				foreach (CanvasGroup group in infoGroup)
				{
					group?.DOKill();
				}
			}
			mask?.DOKill();
			if (difficultyJacket != null)
			{
				difficultyJacket.transform.DOKill();
			}
		}

		private static void ApplyCustomScoreInfo(FreeLiveBootData bootData, ref string title, ref string lyricist, ref string composer, ref string arranger, ref string singer)
		{
			if (!string.IsNullOrEmpty(bootData.CustomMusicScoreTitle))
			{
				title = bootData.CustomMusicScoreTitle;
			}
			if (!string.IsNullOrEmpty(bootData.CustomMusicScoreAuthorName))
			{
				singer = bootData.CustomMusicScoreAuthorName;
			}

			try
			{
				CustomMusicScorePackage package = CustomMusicScoreStorage.LoadPackage(bootData.CustomMusicScorePath);
				CustomMusicScoreManifest manifest = package?.Manifest;
				if (manifest == null)
				{
					return;
				}
				title = string.IsNullOrEmpty(manifest.title) ? title : manifest.title;
				lyricist = manifest.lyricist ?? string.Empty;
				composer = manifest.composer ?? string.Empty;
				arranger = manifest.arranger ?? string.Empty;
				singer = string.IsNullOrEmpty(manifest.userName) ? singer : manifest.userName;
			}
			catch
			{
				// Custom score packages are optional during live restoration; keep boot-data text when manifest lookup fails.
			}
		}

		private static string BuildCreatorText(string lyricist, string composer, string arranger)
		{
			string[] values =
			{
				string.IsNullOrEmpty(lyricist) ? null : lyricist,
				string.IsNullOrEmpty(composer) ? null : composer,
				string.IsNullOrEmpty(arranger) ? null : arranger
			};
			return string.Join(" / ", Array.FindAll(values, value => !string.IsNullOrEmpty(value)));
		}

		private static Texture2D LoadJacketTexture(LiveBootDataBase bootData, LiveMusicData musicData)
		{
			Texture2D customTexture = LoadCustomJacketTexture(bootData as FreeLiveBootData);
			if (customTexture != null)
			{
				return customTexture;
			}

			int musicId = musicData?.Music?.id ?? 0;
			string resourceName = musicId > 0 ? $"jacket_s_{musicId}" : null;
			if (string.IsNullOrEmpty(resourceName))
			{
				return null;
			}

			Texture2D texture = AssetBundleUtility.LoadAsset<Texture2D>("thumbnail/music_jacket", resourceName, false);
			if (texture == null)
			{
				texture = AssetBundleUtility.LoadAsset<Texture2D>("startapp/thumbnail/music_jacket", resourceName, false);
			}
			return texture;
		}

		private static Texture2D LoadCustomJacketTexture(FreeLiveBootData bootData)
		{
			if (bootData == null || string.IsNullOrEmpty(bootData.CustomMusicScorePath))
			{
				return null;
			}

			try
			{
				CustomMusicScorePackage package = CustomMusicScoreStorage.LoadPackage(bootData.CustomMusicScorePath);
				string jacketPath = package?.JacketPath;
				if (string.IsNullOrEmpty(jacketPath) || !File.Exists(jacketPath))
				{
					return null;
				}

				Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
				return ImageConversion.LoadImage(texture, File.ReadAllBytes(jacketPath)) ? texture : null;
			}
			catch
			{
				return null;
			}
		}
	}
}
