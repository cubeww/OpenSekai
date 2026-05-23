using System;
using Beebyte.Obfuscator;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public abstract class UIPartsToggleBase : MonoBehaviour
	{
		public enum State
		{
			Disable = 0,
			Off = 1,
			On = 2
		}

		[SerializeField]
		private SeType _seToggleOn;

		[SerializeField]
		private SeType _seToggleOff;

		private State currentState;

		public Action OnToggleOn;

		public Action OnToggleOff;

		public State ToggleState
		{
			get
			{
				return currentState;
			}
		}

		public virtual void Setup(State state = State.Disable)
		{
			currentState = state;
			UpdateView(currentState);
		}

		[Skip]
		public void OnClick()
		{
			if (currentState == State.Disable)
			{
				return;
			}
			if (currentState == State.On)
			{
				ToggleOff();
			}
			else
			{
				ToggleOn();
			}
		}

		public void ToggleOn()
		{
			currentState = State.On;
			UpdateView(currentState);
			OnToggleOn?.Invoke();
		}

		public void ToggleOff()
		{
			currentState = State.Off;
			UpdateView(currentState);
			OnToggleOff?.Invoke();
		}

		public void ToggleDisable()
		{
			currentState = State.Disable;
			UpdateView(currentState);
		}

		protected abstract void UpdateView(State state);

		protected UIPartsToggleBase()
		{
		}
	}
}
