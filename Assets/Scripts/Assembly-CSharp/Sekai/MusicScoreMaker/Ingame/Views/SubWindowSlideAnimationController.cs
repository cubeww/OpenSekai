using Cysharp.Threading.Tasks;
using Sekai.MusicScoreMaker.Ingame.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class SubWindowSlideAnimationController : MonoBehaviour
	{
		[SerializeField]
		[FormerlySerializedAs("_eventClassName")]
		private string _openEventClassName;

		[SerializeField]
		private string _closeEventClassName;

		[SerializeField]
		private SubWindowSlideAnimation _subWindowSlideAnimation;

		[SerializeField]
		protected SubWindowComponent _subWindowComponent;

		[SerializeField]
		private bool _muteSeOnEventOpen;

		[SerializeField]
		private bool _muteSeOnEventClose;

		private bool _isPlaying;

		private const string OpenSeCueName = "SE_UI_DIALOG_OPEN";

		private const string CloseSeCueName = "SE_CANCEL";

		private void Awake()
		{
			Setup();
		}

		private void OnDestroy()
		{
			Dispose();
		}

		private void Setup()
		{
			if (_subWindowSlideAnimation != null)
			{
				_subWindowSlideAnimation.InitializeAnimation();
				_subWindowSlideAnimation.CloseImmediately();
			}
			if (_subWindowComponent != null)
			{
				_subWindowComponent.Setup(CloseProcess);
			}

			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			if (!string.IsNullOrEmpty(_openEventClassName))
			{
				dispatcher.RegisterWithEventData(_openEventClassName, OpenAnimation);
			}
			if (!string.IsNullOrEmpty(_closeEventClassName))
			{
				dispatcher.RegisterWithEventData(_closeEventClassName, CloseAnimation);
			}
			gameObject.SetActive(false);
		}

		private void CloseProcess()
		{
			CloseAnimationCore(playSe: true);
		}

		private void Dispose()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			if (!string.IsNullOrEmpty(_openEventClassName))
			{
				dispatcher.RemoveWithEventData(_openEventClassName, OpenAnimation);
			}
			if (!string.IsNullOrEmpty(_closeEventClassName))
			{
				dispatcher.RemoveWithEventData(_closeEventClassName, CloseAnimation);
			}
		}

		private void OpenAnimation()
		{
			if (_isPlaying)
			{
				return;
			}

			_isPlaying = true;
			gameObject.SetActive(true);
			if (!_muteSeOnEventOpen)
			{
				SoundManager.Instance.PlaySEOneShot(OpenSeCueName, 0);
			}
			if (_subWindowSlideAnimation != null)
			{
				_subWindowSlideAnimation.OpenAnimation().ContinueWith(() => _isPlaying = false).Forget();
			}
			else
			{
				_isPlaying = false;
			}
		}

		private void CloseAnimation()
		{
			CloseAnimationCore(playSe: true);
		}

		private void CloseAnimationCore(bool playSe)
		{
			if (!gameObject.activeSelf || _isPlaying)
			{
				return;
			}

			_isPlaying = true;
			if (playSe && !_muteSeOnEventClose)
			{
				SoundManager.Instance.PlaySEOneShot(CloseSeCueName, 0);
			}
			if (_subWindowSlideAnimation != null)
			{
				_subWindowSlideAnimation.CloseAnimation().ContinueWith(delegate
				{
					gameObject.SetActive(false);
					_isPlaying = false;
				}).Forget();
			}
			else
			{
				gameObject.SetActive(false);
				_isPlaying = false;
			}
		}

		public void CloseAnimationSilently()
		{
			CloseAnimationCore(playSe: false);
		}

		protected virtual void OnHardwareBackKeyProcess()
		{
			CloseAnimation();
		}

		public void ExecuteBackKeyProcess()
		{
			OnHardwareBackKeyProcess();
		}

		public SubWindowSlideAnimationController()
		{
		}
	}
}
