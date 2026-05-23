using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class InvalidPlacementButtonView : MonoBehaviour
	{
		[SerializeField]
		private GameObject _buttonObject;

		[SerializeField]
		private CustomTextMesh _countText;

		private void Awake()
		{
			Setup();
		}

		private void Setup()
		{
			MusicScoreMakerEventDispatcher.Instance.Register<ValidateViewEvent>(OnValidateView);
			UpdateDisplay(0);
		}

		private void OnDestroy()
		{
			Dispose();
		}

		private void Dispose()
		{
			MusicScoreMakerEventDispatcher.Instance.Remove<ValidateViewEvent>(OnValidateView);
		}

		private void OnValidateView(ValidateViewEvent evt)
		{
			UpdateDisplay(evt.InvalidPlacementCount);
		}

		private void UpdateDisplay(int invalidPlacementCount)
		{
			if (_buttonObject != null)
			{
				_buttonObject.SetActive(invalidPlacementCount > 0);
			}
			if (invalidPlacementCount > 0 && _countText != null)
			{
				_countText.text = invalidPlacementCount.ToString();
			}
		}

		public InvalidPlacementButtonView()
		{
		}
	}
}
