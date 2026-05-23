using UnityEngine;

namespace Sekai.Core
{
	public class BaseController : MonoBehaviour
	{
		private bool isExit;
		private bool isPause;
		private bool isQuitting;

		protected bool IsExit => isExit;

		protected bool IsPause => isPause;

		protected virtual void Awake()
		{
			OnAwake();
		}

		protected virtual void LateUpdate()
		{
			OnUpdate();
		}

		protected virtual void OnDestroy()
		{
			Destroy();
		}

		protected virtual void OnApplicationQuit()
		{
			isQuitting = Application.isEditor;
		}

		protected virtual void OnApplicationFocus(bool hasFocus)
		{
			SetSystemPause(!hasFocus);
		}

		protected virtual void OnApplicationPause(bool pauseStatus)
		{
			SetSystemPause(pauseStatus);
		}

		public void Exit()
		{
			if (isExit)
			{
				return;
			}

			isExit = true;
			OnExit();
		}

		public void CancelExit()
		{
			isExit = false;
		}

		public void Destroy()
		{
			if (!isQuitting)
			{
				Exit();
			}
		}

		protected virtual void OnSystemPause()
		{
		}

		protected virtual void OnAwake()
		{
		}

		protected virtual void OnUpdate()
		{
		}

		protected virtual void OnExit()
		{
		}

		protected virtual void OnRenderGUI()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		private void SetSystemPause(bool value)
		{
			if (isPause == value)
			{
				return;
			}

			isPause = value;
			OnSystemPause();
		}
	}
}
