using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public class ScreenLayerMusicScoreDetail : ScreenLayer
	{
		public class BootArg : BootArgBase
		{
			public readonly MusicScoreData MusicScoreData;

			public BootArg(MusicScoreData musicScoreData)
			{
				throw null;
			}
		}

		[SerializeField]
		private ScreenLayerMusicScoreDetailView _view;

		private ScreenLayerMusicScoreDetailPresenter _presenter;

		protected override void OnBoot(BootArgBase bootArgBase)
		{
			throw null;
		}

		protected override void OnInitComponent()
		{
			throw null;
		}

		protected override void OnScreenStart()
		{
			throw null;
		}

		protected override void OnFinishStartAnimation()
		{
			throw null;
		}

		public override void OnWillExit()
		{
			throw null;
		}

		protected override void OnExitStart()
		{
			throw null;
		}

		protected override void OnExited()
		{
			throw null;
		}

		protected override void OnExitScene()
		{
			throw null;
		}

		private void Update()
		{
			throw null;
		}

		public ScreenLayerMusicScoreDetail()
		{
			throw null;
		}
	}
}
