using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class CountdownView : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CCountdown_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CountdownView _003C_003E4__this;

			private int _003Ci_003E5__2;

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
			public _003CCountdown_003Ed__6(int _003C_003E1__state)
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
				_003C_003E1__state = -2;
				_003C_003E2__current = null;
			}
		}

		private GameObject[] countDonwParticleArray;

		private IEnumerator coroutine;

		private const float FirstAnimationFrameTime = 1f / 60f;

		public void Setup()
		{
			Transform root = transform;
			countDonwParticleArray = new GameObject[root.childCount];
			for (int i = 0; i < countDonwParticleArray.Length; i++)
			{
				countDonwParticleArray[i] = root.GetChild(i).gameObject;
				countDonwParticleArray[i].SetActive(false);
			}
		}

		private void Reset()
		{
			if (countDonwParticleArray == null)
			{
				return;
			}
			for (int i = 0; i < countDonwParticleArray.Length; i++)
			{
				if (countDonwParticleArray[i] != null)
				{
					countDonwParticleArray[i].SetActive(false);
				}
			}
		}

		public void StartCountdown()
		{
			StopCountdown();
			coroutine = Countdown();
			StartCoroutine(coroutine);
		}

		public void StopCountdown()
		{
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
				coroutine = null;
			}
			Reset();
		}

		[IteratorStateMachine(typeof(_003CCountdown_003Ed__6))]
		private IEnumerator Countdown()
		{
			return CountdownCore();
		}

		public CountdownView()
		{
		}

		private IEnumerator CountdownCore()
		{
			if (countDonwParticleArray == null)
			{
				Setup();
			}
			for (int i = countDonwParticleArray.Length - 1; i >= 0; i--)
			{
				SoundManager soundManager = SoundManager.Instance;
				if (soundManager != null)
				{
					soundManager.PlaySEOneShot(LiveSoundDefine.SE_LIVE_COUNT_DOWN, 0);
				}
				if (countDonwParticleArray[i] != null)
				{
					countDonwParticleArray[i].SetActive(true);
					RestartAnimation(countDonwParticleArray[i]);
				}
				yield return new WaitForSeconds(1f);
			}
			Reset();
			coroutine = null;
		}

		private static void RestartAnimation(GameObject effectObject)
		{
			if (effectObject == null)
			{
				return;
			}

			// The original effect relies on each child Animator replaying from the default state
			// when fx_live_countdown_3/2/1 is activated. Rewind explicitly for the restored prefab.
			Animator[] animators = effectObject.GetComponentsInChildren<Animator>(true);
			for (int i = 0; i < animators.Length; i++)
			{
				Animator animator = animators[i];
				if (animator == null || animator.runtimeAnimatorController == null)
				{
					continue;
				}
				animator.Rebind();
				animator.Play(0, 0, 0f);
				animator.Update(0f);
				animator.Update(FirstAnimationFrameTime);
			}
		}
	}
}
