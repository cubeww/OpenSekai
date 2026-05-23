using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using DG.Tweening;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsGaugeExp : MonoBehaviour
	{
		public class SetupData
		{
			public int StartLevel
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public int StartTotalExp
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public int AfterTotalExp
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public int MaxLevel
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public string RestWordingKey
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public SetupData(int startLevel, int startTotalExp, int afterTotalExp, int maxLevel, string restWordingKey = "MSG_REST_VALUE")
			{
				throw null;
			}
		}

		public struct GaugeLevelData
		{
			public readonly int Level;

			public readonly int TotalExp;

			public GaugeLevelData(int level, int totalExp)
			{
				throw null;
			}
		}

		public class EaseInfo
		{
			public float Duration
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public Ease Ease
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public Action OnFinished
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public EaseInfo(float duration, Ease ease = Ease.Linear, Action onFinished = null)
			{
				throw null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CIncreaseExpGaugeAsync_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public int from;

			public int to;

			public UIPartsGaugeExp _003C_003E4__this;

			public int max;

			public string restWordingKey;

			public EaseInfo easeInfo;

			public CancellationToken token;

			private TweenAwaiter _003C_003Eu__1;

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
		private struct _003CSetupAsync_003Ed__10 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public UpdateExpResult updateExpResult;

			public string levelType;

			public string restWordingKey;

			public UIPartsGaugeExp _003C_003E4__this;

			public EaseInfo easeInfo;

			public Action<int> onRankUp;

			public CancellationToken token;

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
		private struct _003CSetupAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public string levelType;

			public SetupData setupData;

			public UIPartsGaugeExp _003C_003E4__this;

			public EaseInfo easeInfo;

			public Action<int> onRankUp;

			public CancellationToken token;

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
		private struct _003CSetupAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SetupData setupData;

			public UIPartsGaugeExp _003C_003E4__this;

			public int afterLevel;

			public EaseInfo easeInfo;

			public Func<int, GaugeLevelData> getCurrentGaugeLevelData;

			public Func<int, GaugeLevelData> getNextGaugeLevelData;

			public CancellationToken token;

			public Action<int> onRankUp;

			private EaseInfo _003CsplitEaseInfo_003E5__2;

			private int _003CcurrentLevel_003E5__3;

			private GaugeLevelData _003CnextGaugeLevelData_003E5__4;

			private int _003CrequiredTotalExpToLevelUp_003E5__5;

			private int _003CtargetTotalExp_003E5__6;

			private int _003Cto_003E5__7;

			private int _003Cmax_003E5__8;

			private UniTask<bool>.Awaiter _003C_003Eu__1;

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
		private UIPartsGauge gauge;

		[SerializeField]
		private CustomText restText;

		[SerializeField]
		private CustomTextMesh restTextMesh;

		public bool VisibleRestTextMesh
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public void Setup(int lvNow, int expNow, string levelType)
		{
			throw null;
		}

		public void Setup(int lvNow, int expNow, int lvMax, string levelType)
		{
			throw null;
		}

		public void Setup(int lvNow, int lvMax, int afterTotalExp, int expNeedNowLv, int expNeedNextLv)
		{
			throw null;
		}

		public void Setup(int lvNow, int lvMax, int afterTotalExp, int expNeedNowLv, int expNeedNextLv, string restWordingKey)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__10))]
		public UniTask SetupAsync(UpdateExpResult updateExpResult, EaseInfo easeInfo, string levelType, Action<int> onRankUp, CancellationToken token, string restWordingKey = "MSG_REST_VALUE")
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__11))]
		public UniTask SetupAsync(SetupData setupData, EaseInfo easeInfo, string levelType, Action<int> onRankUp, CancellationToken token)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__12))]
		public UniTask SetupAsync(SetupData setupData, EaseInfo easeInfo, int afterLevel, Func<int, GaugeLevelData> getCurrentGaugeLevelData, Func<int, GaugeLevelData> getNextGaugeLevelData, Action<int> onRankUp, CancellationToken token)
		{
			throw null;
		}

		public void SetMaxLevelView()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CIncreaseExpGaugeAsync_003Ed__14))]
		private UniTask IncreaseExpGaugeAsync(int from, int to, int max, string restWordingKey, EaseInfo easeInfo, CancellationToken token)
		{
			throw null;
		}

		public void UpdateView(int currentPoint, int maxPoint, string restWordingKey, bool lvMax)
		{
			throw null;
		}

		private void UpdateView(float currentPoint, float maxPoint, string restWordingKey, bool lvMax)
		{
			throw null;
		}

		public void UpdateView(int currentPoint, int maxPoint, bool lvMax)
		{
			throw null;
		}

		public UIPartsGaugeExp()
		{
		}
	}
}
