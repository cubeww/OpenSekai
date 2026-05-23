using System.Collections;
using UnityEngine;

namespace Sekai
{
	public class ScreenLayer : MonoBehaviour, IBlurTarget
	{
		public enum State
		{
			None = 0,
			Booting = 1,
			InitComponent = 2,
			Starting = 3,
			Playing = 4,
			Exiting = 5,
			Exit = 6
		}

		public enum InsertDirection
		{
			Forward = 0,
			Back = 1
		}

		public enum WillExitBehaviourStatus
		{
			None = 0,
			OK = 1,
			Cancel = 2
		}

		public class BootArgBase
		{
		}

		[SerializeField]
		private Camera layerCamera;

		protected GameObject screenRootObject;
		protected GameObject componentRootObject;
		protected ScreenLayerData screenData;
		protected State state;

		public virtual bool EnableBlur => false;
		public Camera LayerCamera => layerCamera;
		public ScreenLayerData Data => screenData;
		public WillExitBehaviourStatus WillExitBehaviour { get; protected set; }
		public State ScreenLayerState => state;
		public InsertDirection ScreenInsertDirection { get; set; }
		public object LayerStackObject { get; set; }

		protected virtual void OnInitComponent()
		{
		}

		protected virtual void OnFinishStartAnimation()
		{
		}

		protected virtual void OnScreenStart()
		{
		}

		protected virtual void OnExitStart()
		{
		}

		protected virtual void OnExited()
		{
		}

		protected virtual void OnExitScene()
		{
		}

		public virtual object GetStackObject()
		{
			return LayerStackObject;
		}

		protected virtual void Awake()
		{
			screenRootObject = gameObject;
			componentRootObject = transform.Find("ComponentRoot")?.gameObject ?? gameObject;
			if (layerCamera == null)
			{
				layerCamera = GetComponentInChildren<Camera>(true);
			}
		}

		protected virtual void OnBoot(BootArgBase bootData)
		{
		}

		protected virtual void ReceiveBootData<T>(T bootData)
		{
		}

		protected void HideComponentRoot()
		{
			if (componentRootObject != null)
			{
				componentRootObject.SetActive(false);
			}
		}

		protected void ShowComponentRoot()
		{
			if (componentRootObject != null)
			{
				componentRootObject.SetActive(true);
			}
		}

		public IEnumerator OnScreenLayerBoot(ScreenLayerData screenData, BootArgBase bootArg = null)
		{
			// TODO(original): restore the original boot coroutine and in-animation state machine.
			this.screenData = screenData;
			state = State.Booting;
			WillExitBehaviour = WillExitBehaviourStatus.None;
			OnBoot(bootArg);
			ScreenBootDone();
			yield break;
		}

		public void ChangeStartAnimationType(ScreenInAnimType animType)
		{
			// TODO(original): persist and apply ScreenInAnimType once animation helpers are copied.
		}

		public void ChangeExitAnimationType(ScreenOutAnimType animType)
		{
			// TODO(original): persist and apply ScreenOutAnimType once animation helpers are copied.
		}

		public void ReverseStartAnimationType()
		{
			// TODO(original): reverse slide/mask animation direction.
		}

		public void ReverseExitAnimationType()
		{
			// TODO(original): reverse slide/mask animation direction.
		}

		public void OnScreenLayerInitComponent()
		{
			state = State.InitComponent;
			OnInitComponent();
		}

		public void OnScreenLayerStart()
		{
			state = State.Starting;
			PlayStartAnimation();
		}

		protected virtual void PlayStartAnimation()
		{
			StartAnimationDone();
		}

		protected void StartAnimationDone()
		{
			state = State.Playing;
			OnFinishStartAnimation();
			OnScreenStart();
		}

		public virtual void OnWillExit()
		{
			ScreenWillExitDone();
		}

		public IEnumerator OnScreenLayerExitStart()
		{
			// TODO(original): restore exit animation coroutine and manual-exit mode.
			state = State.Exiting;
			OnExitStart();
			PlayExitAnimation();
			yield break;
		}

		protected void ExitModeManual()
		{
			// Placeholder: simplified layers always exit immediately.
		}

		protected void ExitModeAuto()
		{
			// Placeholder: simplified layers always exit immediately.
		}

		protected virtual void PlayExitAnimation()
		{
			ExitAnimationDone();
		}

		protected void ExitAnimationDone()
		{
			ScreenExitDone();
		}

		public void OnScreenLayerExited()
		{
			OnExited();
		}

		protected void ScreenBootDone()
		{
			if (state == State.Booting)
			{
				state = State.InitComponent;
			}
		}

		protected void ScreenExitDone()
		{
			state = State.Exit;
			OnScreenLayerExited();
		}

		protected void ScreenWillExitDone(WillExitBehaviourStatus status = WillExitBehaviourStatus.OK)
		{
			WillExitBehaviour = status;
		}

		public void OnScreenLayerExitScene()
		{
			OnExitScene();
		}
	}
}
