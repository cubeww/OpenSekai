using System;
using Sekai.Honor;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public class MusicScoreCreatorTopCreatorInfoView : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private UIPartsCardThumbnail _cardThumbnail;

		[SerializeField]
		private CustomTextMesh _nameText;

		[SerializeField]
		private RectTransform[] _honorParents;

		[SerializeField]
		private CustomTextMesh _createdMusicScoreCountText;

		[SerializeField]
		private CustomTextMesh _totalFavoriteCountText;

		[SerializeField]
		private CustomButton _toProfileButton;

		private IUIPartsHonor[] _honorImages;

		public void Setup(MusicScoreCreatorInfoData data, Action onClickToProfileButton)
		{
			throw null;
		}

		private void SetupHonors(HonorInfo[] infos)
		{
			throw null;
		}

		private void RemoveBeforeHonorImages()
		{
			throw null;
		}

		public void RefreshName(MusicScoreCreatorInfoData data)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreCreatorTopCreatorInfoView()
		{
			throw null;
		}
	}
}
