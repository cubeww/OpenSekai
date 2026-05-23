using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CP;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai
{
	public class InputManager : SingletonMonoBehaviour<InputManager>
	{
		public enum IntervalUseType
		{
			None = 0,
			Use = 1
		}

		public enum ControlState
		{
			NoControl = 0,
			Press = 1,
			ClickCheck = 2
		}

		[CompilerGenerated]
		private sealed class _003CFlareRegularIntervalOnHoldPress_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public InputManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			[DebuggerHidden]
			public _003CFlareRegularIntervalOnHoldPress_003Ed__30(int _003C_003E1__state)
			{
				throw null;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				throw null;
			}

			private bool MoveNext()
			{
				throw null;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw null;
			}
		}

		[CompilerGenerated]
		private sealed class _003CUpdateLongPressCheck_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public InputManager _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					throw null;
				}
			}

			[DebuggerHidden]
			public _003CUpdateLongPressCheck_003Ed__29(int _003C_003E1__state)
			{
				throw null;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				throw null;
			}

			private bool MoveNext()
			{
				throw null;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw null;
			}
		}

		private const float INTERVAL_SEC = 0.2f;

		private const float LONG_PRESS_SEC = 0.4f;

		private float intervalStartTime;

		private float longPressStartTime;

		private float holdPressRegularIntervalSpeed;

		private bool isCountedLongPress;

		private Coroutine longPressCheckCoroutine;

		private Action onLongPress;

		private Action onHoldPress;

		private Selectable controlSelectable;

		private Action onFinishedControlSelectable;

		private bool enableHoldPress;

		public bool ControledSelectable
		{
			get
			{
				throw null;
			}
		}

		public Dictionary<UnityEngine.Object, int> ControlPointerIdDict
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		private void Awake()
		{
			throw null;
		}

		public bool CheckIntervalTime()
		{
			throw null;
		}

		public void ResetIntervalTime()
		{
			throw null;
		}

		public bool CheckAndResetIntervalTime(IntervalUseType intervalType)
		{
			throw null;
		}

		public void StartControlSelectable(Selectable controlSelectable, Action onFinished)
		{
			throw null;
		}

		public void FinishControlSelectable()
		{
			throw null;
		}

		public void StartLongPressCheck(Action onLongPress, Action onHoldPress, bool enableHoldPress)
		{
			throw null;
		}

		public void CancelLongPressCheck()
		{
			throw null;
		}

		private void InitLongPressCheck()
		{
			throw null;
		}

		[IteratorStateMachine(typeof(_003CUpdateLongPressCheck_003Ed__29))]
		private IEnumerator UpdateLongPressCheck()
		{
			throw null;
		}

		[IteratorStateMachine(typeof(_003CFlareRegularIntervalOnHoldPress_003Ed__30))]
		private IEnumerator FlareRegularIntervalOnHoldPress()
		{
			throw null;
		}

		public bool IsPassedLongPressTime()
		{
			throw null;
		}

		public bool EnableControledTouchCount()
		{
			throw null;
		}

		public bool EnableTouchControl(UnityEngine.Object source, PointerEventData eventData)
		{
			throw null;
		}

		public void RegisterTouchControl(UnityEngine.Object source, PointerEventData eventData)
		{
			throw null;
		}

		public void ReleaseTouchControl(UnityEngine.Object source)
		{
			throw null;
		}

		public InputManager()
		{
		}
	}
}
