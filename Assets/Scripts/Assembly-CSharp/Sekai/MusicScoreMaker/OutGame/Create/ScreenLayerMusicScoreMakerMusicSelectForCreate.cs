using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Create
{
	public class ScreenLayerMusicScoreMakerMusicSelectForCreate : ScreenLayer
	{
		public class BootArg : BootArgBase
		{
			public readonly bool IsCreateFromOfficialScore;

			public BootArg(bool isCreateFromOfficialScore)
			{
				throw null;
			}
		}

		[SerializeField]
		private ScreenLayerMusicScoreMakerMusicSelectForCreateView _view;

		private ScreenLayerMusicScoreMakerMusicSelectForCreatePresenter _presenter;

		private bool _isBackExit;

		protected override void OnBoot(BootArgBase bootArg)
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

		public ScreenLayerMusicScoreMakerMusicSelectForCreate()
		{
			throw null;
		}
	}
}
