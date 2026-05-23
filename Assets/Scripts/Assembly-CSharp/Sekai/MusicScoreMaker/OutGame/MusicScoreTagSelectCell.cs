using System;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreTagSelectCell : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private MusicScoreTagCell _tagCell;

		[SerializeField]
		private UIPartsCheckBox _checkbox;

		private MusicScoreTagSelectCellData _cellData;

		private Action<int, bool> _onToggleChanged;

		public void Setup(MusicScoreTagSelectCellData data, Action<int, bool> onToggleChanged)
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		public void Show()
		{
			throw null;
		}

		public void Hide()
		{
			throw null;
		}

		private void OnCheckValueChanged(bool isOn)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreTagSelectCell()
		{
			throw null;
		}
	}
}
