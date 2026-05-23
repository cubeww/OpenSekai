using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using DG.Tweening;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreMakerLoading : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CFadeOutAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public MusicScoreMakerLoading _003C_003E4__this;

			public float delay;

			public float duration;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayInAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public MusicScoreMakerLoading _003C_003E4__this;

			private CancellationTokenSource _003ClinkedTokenSource_003E5__2;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayLoopAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public MusicScoreMakerLoading _003C_003E4__this;

			private CancellationTokenSource _003ClinkedTokenSource_003E5__2;

			private float _003CoriginalSpeed_003E5__3;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private static readonly string InAnimationStateName;

		private static readonly string LoopAnimationStateName;

		private static readonly string ActiveAnimationStateName;

		[SerializeField]
		private Animator _animator;

		[SerializeField]
		private CanvasGroup _fadeCanvasGroup;

		public void Initialize()
		{
			SetActive(false);
		}

		[AsyncStateMachine(typeof(_003CPlayInAsync_003Ed__6))]
		public UniTask PlayInAsync(CancellationToken ct)
		{
			return PlayInAsyncCore(ct);
		}

		[AsyncStateMachine(typeof(_003CPlayLoopAsync_003Ed__7))]
		public UniTask PlayLoopAsync(CancellationToken ct)
		{
			return PlayLoopAsyncCore(ct);
		}

		public void PlayIdle()
		{
			SetActive(true);
			if (HasAnimatorController())
			{
				_animator.Play(ActiveAnimationStateName, 0, 0f);
			}
		}

		[AsyncStateMachine(typeof(_003CFadeOutAsync_003Ed__9))]
		public UniTask FadeOutAsync(float delay, float duration, CancellationToken ct)
		{
			return FadeOutAsyncCore(delay, duration, ct);
		}

		public void Finish()
		{
			SetActive(false);
		}

		private void SetActive(bool value)
		{
			gameObject.SetActive(value);
		}

		public MusicScoreMakerLoading()
		{
		}

		static MusicScoreMakerLoading()
		{
			InAnimationStateName = "In";
			LoopAnimationStateName = "Loop";
			ActiveAnimationStateName = "Idle";
		}

		private async UniTask PlayInAsyncCore(CancellationToken ct)
		{
			using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(ct, this.GetCancellationTokenOnDestroy());
			var token = linkedTokenSource.Token;
			SetActive(true);
			if (_fadeCanvasGroup != null)
			{
				_fadeCanvasGroup.alpha = 1f;
			}
			if (!HasAnimatorController())
			{
				return;
			}

			_animator.speed = 1f;
			_animator.Play(InAnimationStateName, 0, 0f);
			await WaitForAnimatorStateAsync(InAnimationStateName, token);
		}

		private async UniTask PlayLoopAsyncCore(CancellationToken ct)
		{
			using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(ct, this.GetCancellationTokenOnDestroy());
			var token = linkedTokenSource.Token;
			SetActive(true);
			if (HasAnimatorController())
			{
				_animator.Play(LoopAnimationStateName, 0, 0f);
				_animator.Update(0f);
				float originalSpeed = _animator.speed;
				_animator.speed = 0f;
				await FadeCanvasGroupAsync(1f, 0f, 0.15f, token);
				if (token.IsCancellationRequested)
				{
					return;
				}
				_animator.speed = originalSpeed;
				_animator.Play(LoopAnimationStateName, 0, 0f);
			}
			else
			{
				await FadeCanvasGroupAsync(1f, 0f, 0.15f, token);
			}

			await UniTask.WaitUntil(() => token.IsCancellationRequested, cancellationToken: token).SuppressCancellationThrow();
		}

		private async UniTask FadeOutAsyncCore(float delay, float duration, CancellationToken ct)
		{
			using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(ct, this.GetCancellationTokenOnDestroy());
			await FadeCanvasGroupAsync(0f, delay, duration, linkedTokenSource.Token);
		}

		private async UniTask FadeCanvasGroupAsync(float targetAlpha, float delay, float duration, CancellationToken ct)
		{
			if (_fadeCanvasGroup == null)
			{
				if (delay > 0f)
				{
					await UniTask.Delay(Mathf.RoundToInt(delay * 1000f), cancellationToken: ct).SuppressCancellationThrow();
				}
				return;
			}

			if (duration <= 0f)
			{
				if (delay > 0f)
				{
					await UniTask.Delay(Mathf.RoundToInt(delay * 1000f), cancellationToken: ct).SuppressCancellationThrow();
				}
				if (!ct.IsCancellationRequested)
				{
					_fadeCanvasGroup.alpha = targetAlpha;
				}
				return;
			}

			var tween = _fadeCanvasGroup.DOFade(targetAlpha, duration).SetDelay(delay);
			while (tween.IsActive() && !tween.IsComplete() && !ct.IsCancellationRequested)
			{
				await UniTask.Yield(PlayerLoopTiming.Update, ct).SuppressCancellationThrow();
			}
			if (ct.IsCancellationRequested && tween.IsActive())
			{
				tween.Kill();
			}
		}

		private async UniTask WaitForAnimatorStateAsync(string stateName, CancellationToken ct)
		{
			await UniTask.Yield(PlayerLoopTiming.Update, ct).SuppressCancellationThrow();
			while (!ct.IsCancellationRequested)
			{
				var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
				if (stateInfo.IsName(stateName) && stateInfo.normalizedTime >= 1f && !_animator.IsInTransition(0))
				{
					return;
				}

				await UniTask.Yield(PlayerLoopTiming.Update, ct).SuppressCancellationThrow();
			}
		}

		private bool HasAnimatorController()
		{
			return _animator != null && _animator.runtimeAnimatorController != null;
		}
	}
}
