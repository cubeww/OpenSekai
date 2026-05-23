using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public sealed class CategoryTabItem : GenericTabItem<CategoryTabData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003COnDeselectedAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryTabItem _003C_003E4__this;

			public CancellationToken ct;

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
		private struct _003COnSelectedAsync_003Ed__8 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CategoryTabItem _003C_003E4__this;

			public CancellationToken ct;

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

		private static readonly string AnimationStateIn;

		private static readonly string AnimationStateOut;

		[SerializeField]
		private CustomTextMesh _titleText;

		[SerializeField]
		private Animator _animator;

		[SerializeField]
		private GraphicButtonTapEffect _tapEffect;

		protected override void OnSetupInternal()
		{
			throw null;
		}

		protected override void RefreshInternal()
		{
			throw null;
		}

		public override void ApplySelected()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnSelectedAsync_003Ed__8))]
		public override UniTask OnSelectedAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnDeselectedAsync_003Ed__9))]
		public override UniTask OnDeselectedAsync(CancellationToken ct)
		{
			throw null;
		}

		public CategoryTabItem()
		{
			throw null;
		}

		static CategoryTabItem()
		{
			throw null;
		}
	}
}
