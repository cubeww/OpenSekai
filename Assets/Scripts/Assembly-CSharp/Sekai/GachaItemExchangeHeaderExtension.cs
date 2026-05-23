using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Beebyte.Obfuscator;
using Cysharp.Threading.Tasks;
using Sekai.Service;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class GachaItemExchangeHeaderExtension : HeaderExtensionBase
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteExchange_003Ed__23 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public GachaItemExchangeHeaderExtension _003C_003E4__this;

			private int _003Ccount_003E5__2;

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

		[SerializeField]
		protected UITextureLoader ceilItemTextureLoader;

		[SerializeField]
		private CustomTextMesh ceilItemNumText;

		[SerializeField]
		private CustomTextMesh ceilItemNameText;

		[SerializeField]
		private CustomButton ceilCurrencyButton;

		[SerializeField]
		private GameObject ceilCurrencyCover;

		private MasterGachaCeilItem masterCeilItem;

		private MasterGachaCeilExchange masterGachaCeilExchange;

		private GachaCeilTicketExchangeConfirmDialog gachaCeilTicketExchangeConfirmDialog;

		private GachaItemExchangeDataService gachaItemExchangeDataService;

		private const int CURRENCY_NOT_USE_WIDTH = 230;

		private const int CURRENCY_USE_WIDTH = 180;

		public override HeaderCategory HeaderCategory
		{
			get
			{
				throw null;
			}
		}

		private void Awake()
		{
			throw null;
		}

		private void OnDestroy()
		{
			throw null;
		}

		public void Setup(MasterGachaCeilItem masterGachaCeilItem)
		{
			throw null;
		}

		private void SetupGachaCeilItem(MasterGachaCeilItem masterGachaCeilItem)
		{
			throw null;
		}

		public override void UpdateInfo()
		{
			throw null;
		}

		private void UpdateCeilItem()
		{
			throw null;
		}

		private void UpdateCeilItemCurrencyButton()
		{
			throw null;
		}

		public void OnUpdateCeilItem(UserGachaCeilItem[] update)
		{
			throw null;
		}

		[Skip]
		public void OnClickCeilItemCurrencyUp()
		{
			throw null;
		}

		private void OnClickDialogOK()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteExchange_003Ed__23))]
		private void ExecuteExchange()
		{
			throw null;
		}

		public GachaItemExchangeHeaderExtension()
		{
		}
	}
}
