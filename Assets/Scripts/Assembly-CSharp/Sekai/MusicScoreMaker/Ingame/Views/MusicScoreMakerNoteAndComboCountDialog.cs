using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMakerNoteAndComboCountDialog : Common1ButtonDialog
	{
		[SerializeField]
		private UITextureLoader _jacketLoader;

		[SerializeField]
		private CustomTextMesh _tapCountText;

		[SerializeField]
		private CustomTextMesh _longCountText;

		[SerializeField]
		private CustomTextMesh _flickCountText;

		[SerializeField]
		private CustomTextMesh _traceCountText;

		[SerializeField]
		private CustomTextMesh _middleNoteCountText;

		[SerializeField]
		private CustomTextMesh _totalComboCountText;

		[SerializeField]
		private CustomTextMesh _estimatedDifficultyText;

		[SerializeField]
		private UIPartsMusicDifficultyLabel[] _difficultyLabels;

		[SerializeField]
		private CustomTextMesh[] _difficultyThresholdTexts;

		[SerializeField]
		private GameObject _changeRateRoot;

		[SerializeField]
		private CustomTextMesh _changeRateText;

		[SerializeField]
		private CustomTextMesh _changeRateThresholdText;

		public void Setup(NoteAndComboCountInfo countInfo, int musicId, string estimatedDifficultyDisplayText, int durationSec)
		{
			SetText(_tapCountText, countInfo.TapCount);
			SetText(_longCountText, countInfo.LongCount);
			SetText(_flickCountText, countInfo.FlickCount);
			SetText(_traceCountText, countInfo.TraceCount);
			SetText(_middleNoteCountText, countInfo.MiddleNoteCount);
			SetText(_totalComboCountText, countInfo.TotalComboCount);
			if (_estimatedDifficultyText != null)
			{
				_estimatedDifficultyText.text = estimatedDifficultyDisplayText;
			}
			SetupDifficultyThresholdDisplay(durationSec);
			LoadJacket(musicId);
		}

		public void SetChangeRateSectionVisible(bool visible)
		{
			if (_changeRateRoot != null)
			{
				_changeRateRoot.SetActive(visible);
			}
		}

		public void SetupChangeRate(float? changeRate, float threshold)
		{
			if (_changeRateText != null)
			{
				if (changeRate.HasValue)
				{
					float displayedRate = MusicScoreMakerUtility.RoundToDisplayedPercent(changeRate.Value);
					float displayedThreshold = MusicScoreMakerUtility.RoundToDisplayedPercent(threshold);
					_changeRateText.text = displayedRate.ToString("0.0") + "%";
					_changeRateText.color = displayedRate < displayedThreshold ? Color.red : Color.white;
				}
				else
				{
					_changeRateText.text = "-";
					_changeRateText.color = Color.white;
				}
			}
			if (_changeRateThresholdText != null)
			{
				_changeRateThresholdText.text = string.Format("threshold {0:0.0}%", threshold * 100f);
			}
		}

		private void SetupDifficultyThresholdDisplay(int durationSec)
		{
			if (_difficultyLabels == null || _difficultyThresholdTexts == null)
			{
				return;
			}
			int[] thresholds = GetLevelDesignMaxValuesFromDuration(durationSec);
			for (int i = 0; i < _difficultyLabels.Length; i++)
			{
				MusicDifficulty difficulty = (MusicDifficulty)(i + 1);
				if (_difficultyLabels[i] != null)
				{
					_difficultyLabels[i].Setup(difficulty);
					_difficultyLabels[i].Show();
				}
				if (i < _difficultyThresholdTexts.Length && _difficultyThresholdTexts[i] != null)
				{
					_difficultyThresholdTexts[i].text = i < thresholds.Length ? "<= " + thresholds[i] : string.Empty;
				}
			}
		}

		private void LoadJacket(int musicId)
		{
			if (_jacketLoader == null)
			{
				return;
			}
			_jacketLoader.Unload();
			if (musicId < 1)
			{
				return;
			}
			// TODO(original): restore MasterDataManager + MusicUtility.GetJacketResourceInfo based loading.
		}

		private void OnDestroy()
		{
			if (_jacketLoader != null)
			{
				_jacketLoader.Unload();
			}
		}

		public MusicScoreMakerNoteAndComboCountDialog()
		{
		}

		private static void SetText(CustomTextMesh text, int value)
		{
			if (text != null)
			{
				text.text = value.ToString();
			}
		}

		private static int[] GetLevelDesignMaxValuesFromDuration(int durationSec)
		{
			// TODO(original): reconnect MusicScoreDifficultyEstimator.GetLevelDesignMaxValuesFromDuration.
			int safeDuration = Mathf.Max(1, durationSec);
			return new[]
			{
				safeDuration * 2,
				safeDuration * 3,
				safeDuration * 4,
				safeDuration * 6,
				safeDuration * 8
			};
		}
	}
}
