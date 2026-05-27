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

		public void Setup()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<ValidateViewEvent>(OnValidateView);
			dispatcher.Register<ValidateViewEvent>(OnValidateView);
			UpdateDisplay(0);
		}

		private void OnDestroy()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
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
