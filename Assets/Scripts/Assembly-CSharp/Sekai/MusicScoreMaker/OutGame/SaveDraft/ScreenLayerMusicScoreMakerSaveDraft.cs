using System;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public class ScreenLayerMusicScoreMakerSaveDraft : ScreenLayer
	{
		public sealed class BootArg : BootArgBase
		{
			public readonly MusicScoreMakerData MusicScoreMakerData;

			public readonly bool IsExitOnSave;

			[CanBeNull]
			public readonly string BaseMusicScoreId;

			public readonly int BaseMusicDifficultyId;

			[CanBeNull]
			public readonly Action OnSaveCompleted;

			[CanBeNull]
			public readonly Action<int, UserCustomMusicScoreDraft> OnSaveDraftInfoCallback;

			[CanBeNull]
			public readonly Action<int> OnDraftDeletedCallback;

			public BootArg(MusicScoreMakerData musicScoreMakerData, bool isExitOnSave, [CanBeNull] string baseMusicScoreId, int baseMusicDifficultyId = -1, [CanBeNull] Action onSaveCompleted = null, [CanBeNull] Action<int, UserCustomMusicScoreDraft> onSaveDraftInfoCallback = null, [CanBeNull] Action<int> onDraftDeletedCallback = null)
			{
				throw null;
			}
		}

		[SerializeField]
		private ScreenLayerMusicScoreMakerSaveDraftView _view;

		private ScreenLayerMusicScoreMakerSaveDraftPresenter _presenter;

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

		public ScreenLayerMusicScoreMakerSaveDraft()
		{
			throw null;
		}
	}
}
