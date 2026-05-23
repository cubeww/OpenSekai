using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.OutGame
{
	public class ScreenLayerMusicScoreDetailModel
	{
		private readonly MusicScoreData _musicScoreData;

		private readonly MusicScoreDetailViewData _viewData;

		public MusicScoreDetailViewData ViewData
		{
			get
			{
				throw null;
			}
		}

		public bool IsPlayingPreview
		{
			get
			{
				throw null;
			}
		}

		private bool ShouldPlayingPreview
		{
			get
			{
				throw null;
			}
		}

		private bool IsReadyPreviewPlaying
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public ScreenLayerMusicScoreDetailModel(MusicScoreData musicScoreData)
		{
			throw null;
		}

		public void Setup()
		{
			throw null;
		}

		public void AddPreviewCurrentMusicTime(float time)
		{
			throw null;
		}

		public void EnableReadyPreviewPlaying()
		{
			throw null;
		}

		private void DisableReadyPreviewPlaying()
		{
			throw null;
		}

		private void ApplyViewData()
		{
			throw null;
		}

		private void ApplyDetailInfoViewData(MusicDifficulty musicDifficulty)
		{
			throw null;
		}

		private void ApplyPreviewInfoViewData(MasterMusicAll masterMusicAll, MusicDifficulty musicDifficulty)
		{
			throw null;
		}
	}
}
