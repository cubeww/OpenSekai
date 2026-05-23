using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.Outgame;

namespace Sekai.MusicScoreMaker.OutGame.Created.Content
{
	public sealed class AutoSaveMusicScoreListSelectViewData : ContentViewDataBase
	{
		private AutoSaveMusicScoreSlotCellData[] _slotCellDataArray;

		public int CellCount
		{
			get
			{
				throw null;
			}
		}

		public bool IsEmpty
		{
			get
			{
				throw null;
			}
		}

		public bool IsCreateRestartButtonEnabled
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public void ApplySlotCellDataArray([NotNull] AutoSaveMusicScoreSlotCellData[] slotCellDataArray)
		{
			throw null;
		}

		[CanBeNull]
		public AutoSaveMusicScoreSlotCellData GetSlotCellData(int index)
		{
			throw null;
		}

		public void ApplySelectedSlot(int selectedSlotNumber)
		{
			throw null;
		}

		public string GetMusicScoreFileNameBySlotNumber(int slotNumber)
		{
			throw null;
		}

		public void ApplyCreateRestartButtonEnabled(bool enabled)
		{
			throw null;
		}

		public AutoSaveMusicScoreListSelectViewData()
		{
			throw null;
		}
	}
}
