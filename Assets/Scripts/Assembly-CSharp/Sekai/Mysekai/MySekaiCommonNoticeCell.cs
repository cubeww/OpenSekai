using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai.Mysekai
{
	public class MySekaiCommonNoticeCell : MonoBehaviour, ISlideNoticeCell
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CLoadItemImage_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MySekaiCommonNoticeCell _003C_003E4__this;

			public MySekaiCommonNoticeViewData viewData;

			private UniTask<(bool isSuccess, AssetManager.BundleElement element)>.Awaiter _003C_003Eu__1;

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

		[SerializeField]
		private CustomTextMesh messageText;

		[SerializeField]
		private UITextureLoader iconLoader;

		[SerializeField]
		private CustomImage iconAtlasImage;

		private string _seName;

		public void Setup(NoticeViewDataBase viewDataBase)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CLoadItemImage_003Ed__5))]
		private UniTask LoadItemImage(MySekaiCommonNoticeViewData viewData)
		{
			throw null;
		}

		private void LoadFromAtlas(MySekaiCommonNoticeViewData viewData)
		{
			throw null;
		}

		public void Open()
		{
			throw null;
		}

		public void Close()
		{
			throw null;
		}

		public void Sleep()
		{
			throw null;
		}

		public void Wakeup()
		{
			throw null;
		}

		public MySekaiCommonNoticeCell()
		{
		}
	}
}
