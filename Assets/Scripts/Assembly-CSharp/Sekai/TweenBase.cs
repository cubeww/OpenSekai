using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Sekai
{
	public class TweenBase : MonoBehaviour
	{
		public enum BehaviourType
		{
			Master = 0,
			Reference = 1
		}

		public enum PlayTiming
		{
			PlayOnAwake = 0,
			PlayOnStart = 1,
			PlayOnEnable = 2,
			Manual = 3
		}

		public enum PlayDirection
		{
			Forward = 0,
			Back = 1
		}

		public enum PlayLoopType
		{
			Once = 0,
			Loop = 1,
			PingPong = 2
		}

		public enum PlayComponentType
		{
			Normal = 0,
			UnityUI = 1,
			CanvasGroup = 2
		}

		[Serializable]
		public class TweenEvent : UnityEvent<TweenBase>
		{
			public TweenEvent()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CPlayReferenceCore_003Ed__70 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public TweenBase _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
					return _003C_003E2__current;
			}
		}

			object IEnumerator.Current
			{
			[DebuggerHidden]
			get
			{
					return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CPlayReferenceCore_003Ed__70(int _003C_003E1__state)
		{
				this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
				return false;
		}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
				throw new NotSupportedException();
		}
	}

		[SerializeField]
		protected BehaviourType behaviour;

		[SerializeField]
		protected AnimationCurve curve;

		[SerializeField]
		protected float duration;

		[SerializeField]
		protected float delay;

		[SerializeField]
		protected PlayLoopType loopType;

		[SerializeField]
		protected int loopCount;

		[SerializeField]
		protected PlayComponentType componentType;

		[SerializeField]
		protected PlayDirection direction;

		[SerializeField]
		protected PlayTiming startTiming;

		[SerializeField]
		protected TweenBase reference;

		[SerializeField]
		protected bool dontKillIfDisable;

		[SerializeField]
		protected TweenEvent onFinished;

		[SerializeField]
		protected bool isReflesh;

		[SerializeField]
		protected bool syncGameTime;

		protected Tweener tweener;

		protected ITweenTargetComponent tweenTarget;

		protected IEnumerator referenceCoroutine;

		public AnimationCurve Curve
		{
			get
			{
				return curve;
			}
			set
			{
				curve = value;
			}
		}

		public float Duration
		{
			get
			{
				return duration;
			}
			set
			{
				duration = value;
			}
		}

		public float Delay
		{
			get
			{
				return delay;
			}
			set
			{
				delay = value;
			}
		}

		public PlayLoopType Loop
		{
			get
			{
				return loopType;
			}
			set
			{
				loopType = value;
			}
		}

		public int LoopCount
		{
			get
			{
				return loopCount;
			}
			set
			{
				loopCount = value;
			}
		}

		public PlayComponentType ComponentType
		{
			get
			{
				return componentType;
			}
			set
			{
				componentType = value;
			}
		}

		public PlayDirection Direction
		{
			get
			{
				return direction;
			}
			set
			{
				direction = value;
			}
		}

		public PlayTiming StartTiming
		{
			get
			{
				return startTiming;
			}
			set
			{
				startTiming = value;
			}
		}

		public bool DontKillIfDisable
		{
			get
			{
				return dontKillIfDisable;
			}
			set
			{
				dontKillIfDisable = value;
			}
		}

		public BehaviourType Behaviour
		{
			get
			{
				return behaviour;
			}
			set
			{
				behaviour = value;
			}
		}

		public ITweenTargetComponent TweenTarget
		{
			get
			{
				return tweenTarget;
			}
		}

		public TweenBase ReferenceTween
		{
			get
			{
				return reference;
			}
			set
			{
				reference = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return tweener != null && tweener.IsActive();
			}
		}

		protected virtual void Awake()
		{
			tweener = null;
			if (onFinished == null)
			{
				onFinished = new TweenEvent();
			}
			SetupTarget();
			if (startTiming == PlayTiming.PlayOnAwake)
			{
				Play(direction);
			}
		}

		public virtual void SetupTarget()
		{
			switch (componentType)
			{
				case PlayComponentType.Normal:
					tweenTarget = new TweenTargetNormal();
					break;
				case PlayComponentType.CanvasGroup:
					tweenTarget = new TweenTargetCanvasAlpha();
					break;
				default:
					tweenTarget = new TweenTargetUnityUI();
					break;
			}
			tweenTarget.Initialize(gameObject);
		}

		public void AddCallback(UnityAction<TweenBase> callback)
		{
			if (onFinished == null)
			{
				onFinished = new TweenEvent();
			}
			onFinished.AddListener(callback);
		}

		public void SetCallback(UnityAction<TweenBase> callback)
		{
			ClearCallback();
			AddCallback(callback);
		}

		public void ClearCallback()
		{
			onFinished?.RemoveAllListeners();
		}

		protected virtual void Start()
		{
			if (startTiming == PlayTiming.PlayOnStart)
			{
				Play(direction);
			}
		}

		protected virtual void OnEnable()
		{
			if (startTiming == PlayTiming.PlayOnEnable)
			{
				Play(direction);
			}
		}

		[Skip]
		public void Refresh()
		{
			SetupTarget();
			Stop();
			Play(direction);
		}

		public virtual void Play(PlayDirection playDirection)
		{
			KillTween();
			if (behaviour == BehaviourType.Reference)
			{
				PlayReference();
				return;
			}
			PlayCore(playDirection);
		}

		public virtual void PlayCore(PlayDirection playDirection)
		{
		}

		public virtual void PlayReference()
		{
			referenceCoroutine = PlayReferenceCore();
			StartCoroutine(referenceCoroutine);
		}

		[IteratorStateMachine(typeof(_003CPlayReferenceCore_003Ed__70))]
		protected IEnumerator PlayReferenceCore()
		{
			return PlayReferenceCoreRoutine();
		}

		protected virtual void OnReference()
		{
		}

		public virtual void Stop()
		{
			KillTween();
		}

		public virtual void Pause()
		{
			if (tweener != null && tweener.IsActive())
			{
				tweener.Pause();
			}
		}

		public virtual void Resume()
		{
			if (tweener != null)
			{
				tweener.Play();
			}
		}

		public virtual void Restart()
		{
			if (tweener != null)
			{
				tweener.Restart(true);
			}
		}

		public virtual void ForceFinish()
		{
			ToTween(1f);
			onFinished?.Invoke(this);
		}

		protected void SetTweenOptionCommon()
		{
			if (tweener == null)
			{
				return;
			}
			int loops = loopCount <= 0 ? -1 : loopCount;
			LoopType dotweenLoopType = LoopType.Restart;
			if (loopType == PlayLoopType.Once)
			{
				loops = 1;
			}
			else if (loopType == PlayLoopType.PingPong)
			{
				dotweenLoopType = LoopType.Yoyo;
			}
			if (curve != null)
			{
				tweener.SetEase(curve);
			}
			tweener.SetDelay(delay);
			tweener.SetLoops(loops, dotweenLoopType);
			tweener.OnComplete(OnTweenComplete);
			SyncGameTime();
		}

		protected void SyncGameTime()
		{
			if (!syncGameTime || tweener == null || duration <= 0f)
			{
				return;
			}
			float cycleDuration = loopType == PlayLoopType.PingPong ? duration * 2f : duration;
			float elapsed = Mathf.Repeat(Time.realtimeSinceStartup, cycleDuration);
			if (elapsed >= duration)
			{
				elapsed = Mathf.Repeat(Time.realtimeSinceStartup, duration);
			}
			tweener.Goto(elapsed, true);
		}

		protected virtual void KillTween()
		{
			if (tweener != null)
			{
				tweener.Kill(false);
				tweener = null;
			}
			if (referenceCoroutine != null)
			{
				StopCoroutine(referenceCoroutine);
				referenceCoroutine = null;
			}
		}

		protected virtual void ToTween(float normalizeTime)
		{
		}

		protected virtual void OnDisable()
		{
			if (!dontKillIfDisable)
			{
				KillTween();
			}
		}

		protected virtual void OnDestroy()
		{
			KillTween();
		}

		public TweenBase()
		{
			duration = 1f;
			componentType = PlayComponentType.UnityUI;
			startTiming = PlayTiming.PlayOnEnable;
		}

		private IEnumerator PlayReferenceCoreRoutine()
		{
			if (reference == null)
			{
				yield break;
			}
			reference.Play(direction);
			while (reference != null && reference.IsActive)
			{
				yield return null;
			}
			OnReference();
			referenceCoroutine = null;
		}

		private void OnTweenComplete()
		{
			tweener = null;
			onFinished?.Invoke(this);
		}
	}
}
