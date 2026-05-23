using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreFilterView : MonoBehaviour
	{
		private const int REVIEW_COUNT_INPUT_CHARACTER_LIMIT = 7;

		private const int FULL_COMBO_RATE_INPUT_CHARACTER_LIMIT = 3;

		[SerializeField]
		private CustomIndexToggleGroup _clearStatusToggleGroup;

		[SerializeField]
		private CustomDropdown _minPlayLevelDropdown;

		[SerializeField]
		private CustomDropdown _maxPlayLevelDropdown;

		[SerializeField]
		private CustomIndexToggleGroup _fullComboRateToggleGroup;

		[SerializeField]
		private CustomInputFieldTextMesh _minFullComboRateInputField;

		[SerializeField]
		private CustomInputFieldTextMesh _maxFullComboRateInputField;

		[SerializeField]
		private CustomIndexToggleGroup _reviewCountToggleGroup;

		[SerializeField]
		private CustomIndexToggleGroup _derivativeToggleGroup;

		[SerializeField]
		private CustomInputFieldTextMesh _reviewCountInputField;

		private int _currentMinFullComboRate;

		private int _currentMaxFullComboRate;

		private int _currentReviewCount;

		private Action<string> _onMinFullComboRateEnd;

		private Action<string> _onMaxFullComboRateEnd;

		private Action<string> _onReviewCountEnd;

		public void SetupClearStatus(int selectedIndex, Action<int> onChanged)
		{
			throw null;
		}

		public void SetupPlayLevelDropdowns([NotNull] List<int> playLevelList, int minIndex, int maxIndex, Action<int> onMinChanged, Action<int> onMaxChanged)
		{
			throw null;
		}

		public void RefreshPlayLevelDropdowns(int minIndex, int maxIndex)
		{
			throw null;
		}

		public void SetupFullComboRate(int selectedIndex, Action<int> onTypeChanged, Action<string> onMinEnd, Action<string> onMaxEnd)
		{
			throw null;
		}

		public void RefreshFullComboRateInputs(int minValue, int maxValue)
		{
			throw null;
		}

		public void RefreshReviewCountInput(int value)
		{
			throw null;
		}

		public void SetupReviewCount(int selectedIndex, int value, Action<int> onTypeChanged, Action<string> onValueEnd)
		{
			throw null;
		}

		public void SetupDerivative(int selectedIndex, Action<int> onChanged)
		{
			throw null;
		}

		private void OnMinFullComboRateEndEdit(string value)
		{
			throw null;
		}

		private void OnMaxFullComboRateEndEdit(string value)
		{
			throw null;
		}

		private void OnReviewCountEndEdit(string value)
		{
			throw null;
		}

		private void ValidateAndRestoreIntValue(string value, ref int currentValue, CustomInputFieldTextMesh inputField, Action<string> onValidValueEnd)
		{
			throw null;
		}

		public MusicScoreFilterView()
		{
			throw null;
		}
	}
}
