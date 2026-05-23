using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Beebyte.Obfuscator;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.Service;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class GachaHeaderExtension : HeaderExtensionBase
	{
		private enum Status
		{
			show = 0,
			hide = 1
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteExchange_003Ed__86 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CHideGachaBonus_003Ed__59 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private struct _003CHideGachaBonusItemDoubleGaugeAsync_003Ed__64 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private struct _003CHideGachaBonusItemSingleGaugeAsync_003Ed__69 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private struct _003CHideGachaBonusItemSingleShortGaugeAsync_003Ed__74 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private struct _003CHideGachaCeilItem_003Ed__52 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private struct _003CShowGachaBonus_003Ed__58 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private struct _003CShowGachaBonusItemDoubleGaugeAsync_003Ed__63 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private struct _003CShowGachaBonusItemSingleGaugeAsync_003Ed__68 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private struct _003CShowGachaBonusItemSingleShortGaugeAsync_003Ed__73 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private struct _003CShowGachaCeilItem_003Ed__50 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public GachaHeaderExtension _003C_003E4__this;

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
		private GameObject currencyRoot;

		[SerializeField]
		private CustomTextMesh currencyTotalText;

		[SerializeField]
		private CustomTextMesh currencyPaidChargeText;

		[SerializeField]
		private TweenPosition currencyTween;

		[SerializeField]
		private GameObject ticketRoot;

		[SerializeField]
		private CustomTextMesh tikectNumText;

		[SerializeField]
		private TweenPosition ticketTween;

		[SerializeField]
		private GameObject ceilItemRoot;

		[SerializeField]
		protected UITextureLoader ceilItemTextureLoader;

		[SerializeField]
		private CustomTextMesh ceilItemNumText;

		[SerializeField]
		private TweenPosition ceilItemTween;

		[SerializeField]
		private CustomButton ceilCurrencyButton;

		[SerializeField]
		private GameObject ceilCurrencyCover;

		[SerializeField]
		private ResourceIconLoader ticketIconIconLoader;

		[SerializeField]
		private GameObject gachaBonusRoot;

		[SerializeField]
		private TweenPosition gachaBonusTween;

		[SerializeField]
		private GachaBonusGauge gachaBonusGauge;

		[SerializeField]
		private GameObject gachaBonusItemDoubleGaugeRoot;

		[SerializeField]
		private TweenPosition gachaBonusItemDoubleGaugeTween;

		[SerializeField]
		private GachaBonusItemReceivableDoubleGauge _gachaBonusItemReceivableDoubleGauge;

		[SerializeField]
		private GameObject _gachaBonusItemSingleGaugeRoot;

		[SerializeField]
		private TweenPosition _gachaBonusItemSingleGaugeTween;

		[SerializeField]
		private GachaBonusItemReceivableSingleGauge _gachaBonusItemReceivableSingleGauge;

		[SerializeField]
		private GameObject _gachaBonusItemSingleShortGaugeRoot;

		[SerializeField]
		private TweenPosition _gachaBonusItemSingleShortGaugeTween;

		[SerializeField]
		private GachaBonusItemReceivableSingleGauge _gachaBonusItemReceivableSingleShortGauge;

		private MasterGacha masterGacha;

		private MasterGachaModel _masterGachaModel;

		private MasterGachaCeilItem masterCeilItem;

		private MasterGachaCeilExchange masterGachaCeilExchange;

		private GachaCeilTicketExchangeConfirmDialog gachaCeilTicketExchangeConfirmDialog;

		private bool isFirst;

		private GachaItemExchangeDataService gachaItemExchangeDataService;

		private const int CURRENCY_NOT_USE_WIDTH = -55;

		private const int CURRENCY_USE_WIDTH = -151;

		private Status currencyStatus;

		private Status ticketStatus;

		private Status ceilItemStatus;

		private Status gachaBonusSatus;

		private Status gachaBonusItemDoubleGaugeSatus;

		private Status _gachaBonusItemSingleGaugeStatus;

		private Status _gachaBonusItemSingleShortGaugeStatus;

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

		public void Setup(int gachaId)
		{
			throw null;
		}

		private void SetupMaterial()
		{
			throw null;
		}

		private void SetupGachaCeilItem()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowGachaCeilItem_003Ed__50))]
		private UniTask ShowGachaCeilItem()
		{
			throw null;
		}

		private void UpdateCeilItemCurrencyButton()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CHideGachaCeilItem_003Ed__52))]
		private void HideGachaCeilItem()
		{
			throw null;
		}

		private void ShowCurrency()
		{
			throw null;
		}

		private void HideCurrency()
		{
			throw null;
		}

		private void ShowTicket()
		{
			throw null;
		}

		private void HideTicket()
		{
			throw null;
		}

		private void SetupGachBonus()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowGachaBonus_003Ed__58))]
		public void ShowGachaBonus()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CHideGachaBonus_003Ed__59))]
		public void HideGachaBonus()
		{
			throw null;
		}

		public void ExecuteGachaBonusUpAnimation(int spinCount)
		{
			throw null;
		}

		public void StopGachaBonusAnimation()
		{
			throw null;
		}

		private void SetupGachaBonusItemDoubleGauge()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowGachaBonusItemDoubleGaugeAsync_003Ed__63))]
		public UniTask ShowGachaBonusItemDoubleGaugeAsync()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CHideGachaBonusItemDoubleGaugeAsync_003Ed__64))]
		public UniTask HideGachaBonusItemDoubleGaugeAsync()
		{
			throw null;
		}

		public void ExecuteGachaBonusItemDoubleGaugeUpAnimation(int spinCount)
		{
			throw null;
		}

		public void StopGachaBonusItemDoubleGaugeAnimation()
		{
			throw null;
		}

		private void SetupGachaBonusItemSingleGauge()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowGachaBonusItemSingleGaugeAsync_003Ed__68))]
		public UniTask ShowGachaBonusItemSingleGaugeAsync()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CHideGachaBonusItemSingleGaugeAsync_003Ed__69))]
		public UniTask HideGachaBonusItemSingleGaugeAsync()
		{
			throw null;
		}

		public void ExecuteGachaBonusItemSingleGaugeUpAnimation(int spinCount)
		{
			throw null;
		}

		public void StopGachaBonusItemSingleGaugeAnimation()
		{
			throw null;
		}

		private void SetupGachaBonusItemSingleShortGauge()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowGachaBonusItemSingleShortGaugeAsync_003Ed__73))]
		public UniTask ShowGachaBonusItemSingleShortGaugeAsync()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CHideGachaBonusItemSingleShortGaugeAsync_003Ed__74))]
		public UniTask HideGachaBonusItemSingleShortGaugeAsync()
		{
			throw null;
		}

		public void ExecuteGachaBonusItemSingleShortGaugeUpAnimation(int spinCount)
		{
			throw null;
		}

		public void StopGachaBonusItemSingleShortGaugeAnimation()
		{
			throw null;
		}

		public override void UpdateInfo()
		{
			throw null;
		}

		private void UpdateCurrencyDetail()
		{
			throw null;
		}

		private void UpdateCeilItem()
		{
			throw null;
		}

		private void UpdateTicket()
		{
			throw null;
		}

		public void OnUpdateCeilItem(UserGachaCeilItem[] update)
		{
			throw null;
		}

		[Skip]
		public void OnClickExchange()
		{
			throw null;
		}

		[Skip]
		public void OnClickCurrencyUp()
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

		[AsyncStateMachine(typeof(_003CExecuteExchange_003Ed__86))]
		private void ExecuteExchange()
		{
			throw null;
		}

		public GachaHeaderExtension()
		{
		}
	}
}
