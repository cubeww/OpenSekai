using System;
using JetBrains.Annotations;
using Sekai.Sound;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreMakerMusicSelectConfirmDialog : Common2ButtonDialog, IDisposable
	{
		[SerializeField]
		private UITextureLoader _musicJacketLoader;

		[SerializeField]
		private CustomTextMesh _musicTitleText;

		[SerializeField]
		private CustomTextMesh _musicArtistNameText;

		private MusicShortBGMPlayer _musicShortBGMPlayer;

		private Action<MusicShortBGMPlayer> _onClickOk;

		public static void Show(int musicId, string messageBodyKey, Action<MusicShortBGMPlayer> onClickOk, Action onClickCancel)
		{
			throw null;
		}

		private void Setup([NotNull] MasterMusicAll masterMusicAll, Action<MusicShortBGMPlayer> onClickOk)
		{
			throw null;
		}

		protected override void OnClickOK()
		{
			throw null;
		}

		public override void Close()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreMakerMusicSelectConfirmDialog()
		{
			throw null;
		}
	}
}
