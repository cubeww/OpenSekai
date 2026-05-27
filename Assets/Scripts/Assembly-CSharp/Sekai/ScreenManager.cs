using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using CP;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.Live;
using Sekai.ScreenLayerExtention;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai
{
	[ExecuteInEditMode]
	public class ScreenManager : MonoBehaviour
	{
		public class SceneBackKeyHandler
		{
			public Action OnExecuteBackKeyProcess;

			public bool EnableBackKeyProcess;

			public SceneBackKeyHandler()
			{
			}
		}

		public enum BlurStackDirection
		{
			Forward = 0,
			Back = 1
		}

		public enum BlurDialogProcessState
		{
			Ready = 0,
			Opening = 1
		}

		public class BlurLayerTarget : IEquatable<BlurLayerTarget>
		{
			private readonly IBlurTarget target;

			private readonly GameObject targetObject;

			private readonly DisplayLayerType layerType;

			private IBlurTarget Target
			{
				[CompilerGenerated]
				get
				{
					return target;
				}
			}

			public GameObject TargetObject
			{
				[CompilerGenerated]
				get
				{
					return targetObject;
				}
			}

			public DisplayLayerType LayerType
			{
				[CompilerGenerated]
				get
				{
					return layerType;
				}
			}

			public Canvas TargetCanvas
			{
				get
				{
					return targetObject != null ? targetObject.GetComponent<Canvas>() : null;
				}
			}

			public bool EnableBlur
			{
				get
				{
					return target != null && target.EnableBlur;
				}
			}

			public bool ExistsTarget
			{
				get
				{
					return target != null && targetObject != null;
				}
			}

			public BlurLayerTarget(IBlurTarget target, GameObject targetObject, DisplayLayerType layerType)
			{
				this.target = target;
				this.targetObject = targetObject;
				this.layerType = layerType;
				SetLayer(DefaultLayerName);
			}

			public void AddCanvasCopyFrom(Canvas baseCanvas)
			{
				if (targetObject == null || baseCanvas == null)
				{
					return;
				}

				var canvas = targetObject.GetComponent<Canvas>();
				if (canvas == null)
				{
					canvas = targetObject.AddComponent<Canvas>();
				}
				if (canvas == null)
				{
					return;
				}

				canvas.renderMode = baseCanvas.renderMode;
				canvas.worldCamera = baseCanvas.worldCamera;
				canvas.planeDistance = baseCanvas.planeDistance;
				canvas.overrideSorting = baseCanvas.overrideSorting;
				canvas.sortingLayerID = baseCanvas.sortingLayerID;
				canvas.sortingOrder = baseCanvas.sortingOrder + 1;

				if (baseCanvas.gameObject.GetComponent<GraphicRaycaster>() != null &&
				    targetObject.GetComponent<GraphicRaycaster>() == null)
				{
					targetObject.AddComponent<GraphicRaycaster>();
				}
			}

			public void RemoveCanvasIfExists()
			{
				if (targetObject == null)
				{
					return;
				}

				var raycaster = targetObject.GetComponent<GraphicRaycaster>();
				if (raycaster != null)
				{
					if (Application.isPlaying)
					{
						UnityEngine.Object.Destroy(raycaster);
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(raycaster);
					}
				}

				var canvas = targetObject.GetComponent<Canvas>();
				if (canvas != null)
				{
					if (Application.isPlaying)
					{
						UnityEngine.Object.Destroy(canvas);
					}
					else
					{
						UnityEngine.Object.DestroyImmediate(canvas);
					}
				}
			}

			public bool Equals(BlurLayerTarget other)
			{
				return other != null && targetObject == other.targetObject && layerType.Equals(other.layerType);
			}

			private void SetLayer(string layerName)
			{
				if (targetObject == null)
				{
					return;
				}

				var targetLayer = LayerMask.NameToLayer(layerName);
				if (targetLayer < 0)
				{
					return;
				}

				SetLayerRecursive(targetObject.transform, targetLayer);
			}

			private static void SetLayerRecursive(Transform target, int layer)
			{
				if (target == null)
				{
					return;
				}

				target.gameObject.layer = layer;
				for (var i = 0; i < target.childCount; i++)
				{
					SetLayerRecursive(target.GetChild(i), layer);
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003CAddScreenCore_003Ed__143 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public bool isForward;

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
			public _003CAddScreenCore_003Ed__143(int _003C_003E1__state)
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
		private sealed class _003CAddScreenCore_003Ed__144 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public ScreenLayer.BootArgBase bootArg;

			public bool isForward;

			public bool applyHeaderSetting;

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
			public _003CAddScreenCore_003Ed__144(int _003C_003E1__state)
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
		private sealed class _003CAddScreenCore_003Ed__146 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenLayerState layer;

			public ScreenManager _003C_003E4__this;

			public object layerObject;

			public bool isForward;

			public ScreenLayer.BootArgBase bootArg;

			public bool applyHeaderSetting;

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
			public _003CAddScreenCore_003Ed__146(int _003C_003E1__state)
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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CAddScreenWithoutHeaderSetting_003Ed__117 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public ScreenLayer.BootArgBase bootArg;

			public CancellationToken ct;

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

		[CompilerGenerated]
		private sealed class _003CChangeUIScreenCore_003Ed__136 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public bool isWaitExitAnimation;

			public bool isForward;

			public bool nextScreenAnimationIsForward;

			public bool isClearBootDataOnExitCancel;

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
			public _003CChangeUIScreenCore_003Ed__136(int _003C_003E1__state)
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
		private sealed class _003CChangeUIScreenCore_003Ed__137 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public bool isClearBootDataOnExitCancel;

			public bool isWaitExitAnimation;

			public bool isForward;

			public ScreenLayer.BootArgBase bootArg;

			public bool nextScreenAnimationIsForward;

			private ScreenLayerState _003Clayer_003E5__2;

			private ScreenLayerState _003CtmpNextUI_003E5__3;

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
			public _003CChangeUIScreenCore_003Ed__137(int _003C_003E1__state)
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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCheckModuleMaintenance_003Ed__126 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenManager _003C_003E4__this;

			public Action onAllow;

			public MenuScreenType type;

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
		private struct _003CDequeueBlur_003Ed__270 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenManager _003C_003E4__this;

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
		private struct _003CDisableBlurAsync_003Ed__264 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenManager _003C_003E4__this;

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

		[CompilerGenerated]
		private sealed class _003CExitScreen_003Ed__148 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenLayerState layer;

			public bool executeOnWillExit;

			public bool isClearBootDataOnExitCancel;

			public ScreenManager _003C_003E4__this;

			public bool isForward;

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
			public _003CExitScreen_003Ed__148(int _003C_003E1__state)
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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CFadeOutAsync_003Ed__172 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenManager _003C_003E4__this;

			public CancellationToken token;

			public float duration;

			private Color _003CbaseColor_003E5__2;

			private Color _003CtargetColor_003E5__3;

			private CancellationTokenSource _003Ccts_003E5__4;

			private CancellationToken _003Cct_003E5__5;

			private float _003CelapsedTime_003E5__6;

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

		[CompilerGenerated]
		private sealed class _003CPopUIScreen_003Ed__140 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public bool isWaitExitAnimation;

			private Tuple<ScreenLayerState, object, ScreenLayer.BootArgBase> _003ClayerData_003E5__2;

			private ScreenLayerState _003CtmpNextUI_003E5__3;

			private ScreenLayerState _003Clayer_003E5__4;

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
			public _003CPopUIScreen_003Ed__140(int _003C_003E1__state)
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
		private sealed class _003CPreloadUIScreenCore_003Ed__141 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

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
			public _003CPreloadUIScreenCore_003Ed__141(int _003C_003E1__state)
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
		private sealed class _003CPushUIScreenCore_003Ed__138 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public bool isWaitExitAnimation;

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
			public _003CPushUIScreenCore_003Ed__138(int _003C_003E1__state)
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
		private sealed class _003CPushUIScreenCore_003Ed__139 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public ScreenLayer.BootArgBase bootArg;

			public bool isWaitExitAnimation;

			private ScreenLayerState _003Clayer_003E5__2;

			private ScreenLayerState _003CtmpNextUI_003E5__3;

			private Tuple<ScreenLayerState, object, ScreenLayer.BootArgBase> _003ClayerData_003E5__4;

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
			public _003CPushUIScreenCore_003Ed__139(int _003C_003E1__state)
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
		private sealed class _003CRebuildUIScreenCore_003Ed__142 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public bool isFoward;

			public bool isClearBootDataOnExitCancel;

			public Action<ScreenLayer> onFinished;

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
			public _003CRebuildUIScreenCore_003Ed__142(int _003C_003E1__state)
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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CRemoveScreenAsync_003Ed__120 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public CancellationToken ct;

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

		[CompilerGenerated]
		private sealed class _003CRemoveScreenCore_003Ed__147 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

			public bool isForward;

			public bool isClearBootDataOnExitCancel;

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
			public _003CRemoveScreenCore_003Ed__147(int _003C_003E1__state)
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
		private sealed class _003CRemoveScreenCoroutine_003Ed__119 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ScreenManager _003C_003E4__this;

			public MenuScreenType screenType;

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
			public _003CRemoveScreenCoroutine_003Ed__119(int _003C_003E1__state)
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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSetBlur_003Ed__271 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenManager _003C_003E4__this;

			public BlurLayerTarget target;

			public BlurStackDirection dir;

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
		private struct _003CSetUIBlurCaptureMode_003Ed__260 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenManager _003C_003E4__this;

			public UIBlurLayerManager.Mode mode;

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
		private struct _003CSetUIBlurCaptureModeCore_003Ed__261 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenManager _003C_003E4__this;

			public UIBlurLayerManager.Mode mode;

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

		public const float SCREEN_TRANSITION_TIME = 0.2f;

		public static readonly Vector2 BASE_SCREEN_SIZE;

		private const string DIALOG_PREFAB_PATH_BASE = "Dialog/";

		private static readonly string EffectOverlayLayerName;

		private static ScreenManager instance;

		private ScreenLayerState currentUI;

		private ScreenLayerState nextUI;

		private ScreenLayerState prevUI;

		private LimitedStack<Tuple<ScreenLayerState, object, ScreenLayer.BootArgBase>> uiScreenStack;

		private Vector2 screenToUIRatio;

		private Vector2 baseScreenToUIRatio;

		private RectTransform rootRect;

		private Canvas rootCanvas;

		private CanvasGroup rootCanvasGroup;

		private ScreenCanvasScaler rootCanvasScaler;

		private float rootScale;

		private float inverseRootScale;

		private Vector2 contentSize;

		private Color fadeInColor;

		private Color fadeOutColor;

		private Camera mainUICamera;

		private Camera backUICamera;

		private ScreenCameraStack cameraStack;

		private Dictionary<DisplayLayerType, GameObject> displayLayersCache;

		private Dictionary<string, Dictionary<UnityEngine.Object, bool>> disableTapCommandObjects;

		private IEnumerator changeUILayerCoroutine;

		public Action<bool> OnBackUIScreenOverride;

		public bool IsAutoClearBackUIScreenOverride;

		private List<DialogBase> dialogQueue;

		private Action onFinishDialogQueue;

		private IBgmPlayer layerBgmPlayer;

		private IBackgroundChanger layerBackgroundChanger;

		private APIExecutor apiExecutor;

		private List<ChainDialogPlayer> dialogQueuePlayers;

		private ScreenConfig.ScreenSize screenSize;

		public Action<MenuScreenType> OnChangeUILayer;

		public Action<ScreenConfig.ScreenSize> OnChangeScreenSize;

		public Action OnExecuteBackKeyProcess;

		private Dictionary<SceneManager.Scene, SceneBackKeyHandler> _sceneBackKeyHandlers;

		public List<MenuScreenType> MaintainableScreenList;

		protected ScreenLayerDataList entryScreenLayers;

		protected Dictionary<MenuScreenType, ScreenLayerState> screenMap;

		[SerializeField]
		protected ColorFader fadeController;

		[SerializeField]
		private int uiCameraPriority;

		private static readonly string uiBlurLayerManagerPrefabPath;

		private static readonly string DefaultLayerName;

		private static readonly string BlurLayerName;

		private static readonly List<SceneManager.Scene> EnableBlurSceneList;

		private UIBlurLayerManager uiBlurLayerManager;

		private readonly List<BlurLayerTarget> blurTargets;

		private CancellationTokenSource _blurSettingCts;

		private UIBlurLayerManager.Mode blurCaptureModeCached;

		private BlurDialogProcessState blurDlalogProcessState;

		private Queue<(BlurLayerTarget, BlurStackDirection)> blurQueue;

		private Action onExecuteMysekaiBackKeyProcess;

		private bool enableMysekaiBackKeyProcess;

		private object bootArgObject;

		private bool isPopUIScreen;

		private BackKeyChecker backKeyChecker;

		public Action OnExecuteMysekaiBackKeyProcess
		{
			get
			{
				return onExecuteMysekaiBackKeyProcess;
			}
			set
			{
				onExecuteMysekaiBackKeyProcess = value;
			}
		}

		public bool EnableMysekaiBackKeyProcess
		{
			get
			{
				return enableMysekaiBackKeyProcess;
			}
			set
			{
				enableMysekaiBackKeyProcess = value;
			}
		}

		public static ScreenManager Instance
		{
			get
			{
				if (instance == null)
				{
					var canvasRoot = GameObject.Find("CanvasRoot");
					if (canvasRoot != null)
					{
						instance = canvasRoot.GetComponent<ScreenManager>();
					}
				}

				return instance;
			}
		}

		public static bool ExistsInstance
		{
			get
			{
				return instance != null;
			}
		}

		public object BootArgObject
		{
			[CompilerGenerated]
			get
			{
				return bootArgObject;
			}
			[CompilerGenerated]
			set
			{
				bootArgObject = value;
			}
		}

		public Vector2 ScreenToUIRatio
		{
			get
			{
				return screenToUIRatio;
			}
		}

		public Vector2 ContentSize
		{
			get
			{
				return contentSize;
			}
		}

		public Vector2 BaseScreenToUIRatio
		{
			get
			{
				return baseScreenToUIRatio;
			}
		}

		public float RootScale
		{
			get
			{
				return rootScale;
			}
		}

		public float InverseRootScale
		{
			get
			{
				return inverseRootScale;
			}
		}

		public Canvas RootCanvas
		{
			get
			{
				return rootCanvas;
			}
		}

		public Camera RenderCamera
		{
			get
			{
				return mainUICamera != null ? mainUICamera : GetUICamera();
			}
		}

		public bool IsUILayerWorking
		{
			get
			{
				return changeUILayerCoroutine != null;
			}
		}

		public LimitedStack<Tuple<ScreenLayerState, object, ScreenLayer.BootArgBase>> UiScreenStack
		{
			get
			{
				return uiScreenStack;
			}
		}

		public bool IsPlayFading
		{
			get
			{
				return fadeController != null && fadeController.IsPlayFading;
			}
		}

		public ScreenCameraStack CameraStack
		{
			get
			{
				return cameraStack;
			}
		}

		public bool IsActiveDialogQueue
		{
			get
			{
				return dialogQueue != null && dialogQueue.Count > 0;
			}
		}

		public bool IsContentSizeFixed
		{
			get
			{
				return contentSize.x > 0f && contentSize.y > 0f;
			}
		}

		public bool IsPopUIScreen
		{
			[CompilerGenerated]
			get
			{
				return isPopUIScreen;
			}
			[CompilerGenerated]
			private set
			{
				isPopUIScreen = value;
			}
		}

		public BackKeyChecker BackKeyChecker
		{
			[CompilerGenerated]
			get
			{
				return backKeyChecker;
			}
			[CompilerGenerated]
			set
			{
				backKeyChecker = value;
			}
		}

		public Action GetSceneExecuteBackKeyProcess(SceneManager.Scene scene)
		{
			return GetSceneBackKeyHandler(scene).OnExecuteBackKeyProcess;
		}

		public void SetSceneExecuteBackKeyProcess(SceneManager.Scene scene, Action action)
		{
			GetSceneBackKeyHandler(scene).OnExecuteBackKeyProcess = action;
		}

		public bool GetSceneEnableBackKeyProcess(SceneManager.Scene scene)
		{
			return GetSceneBackKeyHandler(scene).EnableBackKeyProcess;
		}

		public void SetSceneEnableBackKeyProcess(SceneManager.Scene scene, bool enable)
		{
			GetSceneBackKeyHandler(scene).EnableBackKeyProcess = enable;
		}

		public void RemoveSceneBackKeyHandler(SceneManager.Scene scene)
		{
			_sceneBackKeyHandlers?.Remove(scene);
		}

		public SceneBackKeyHandler GetSceneBackKeyHandler(SceneManager.Scene scene)
		{
			_sceneBackKeyHandlers ??= new Dictionary<SceneManager.Scene, SceneBackKeyHandler>();
			if (!_sceneBackKeyHandlers.TryGetValue(scene, out var handler))
			{
				handler = new SceneBackKeyHandler();
				_sceneBackKeyHandlers[scene] = handler;
			}

			return handler;
		}

		public static void SetupInstance(ScreenManager target)
		{
			instance = target;
		}

		private void Awake()
		{
			SetupInstance(this);

			BackKeyChecker = new BackKeyChecker();
			BackKeyChecker.Initialize(OnBackKey);

			layerBgmPlayer = null;
			layerBackgroundChanger = null;
			entryScreenLayers = LoadEntryScreenLayers();
			screenMap = new Dictionary<MenuScreenType, ScreenLayerState>();
			displayLayersCache = new Dictionary<DisplayLayerType, GameObject>();
			disableTapCommandObjects = new Dictionary<string, Dictionary<UnityEngine.Object, bool>>();
			OnChangeUILayer = null;

			rootCanvas = GetComponent<Canvas>();
			rootCanvasGroup = GetComponent<CanvasGroup>();
			rootCanvasScaler = GetComponent<ScreenCanvasScaler>();
			if (rootCanvas != null)
			{
				rootRect = rootCanvas.GetComponent<RectTransform>();
			}

			apiExecutor = GetComponent<APIExecutor>() ?? gameObject.AddComponent<APIExecutor>();
			apiExecutor.SetupInstance(apiExecutor);

			if (entryScreenLayers != null && entryScreenLayers.Entries != null)
			{
				foreach (var entry in entryScreenLayers.Entries)
				{
					if (entry == null || screenMap.ContainsKey(entry.ScreenType))
					{
						continue;
					}

					screenMap.Add(entry.ScreenType, new ScreenLayerState { Data = entry });
				}
			}

			foreach (DisplayLayerType layerType in Enum.GetValues(typeof(DisplayLayerType)))
			{
				var layerObject = GetLayerObject(layerType);
				if (layerObject != null && !disableTapCommandObjects.ContainsKey(layerObject.name))
				{
					disableTapCommandObjects.Add(layerObject.name, new Dictionary<UnityEngine.Object, bool>());
				}
			}

			if (!disableTapCommandObjects.ContainsKey("all"))
			{
				disableTapCommandObjects.Add("all", new Dictionary<UnityEngine.Object, bool>());
			}

			SetUpScreenResolution();
			cameraStack = new ScreenCameraStack();
			var uiCamera = GetUICamera();
			if (uiCamera != null)
			{
				cameraStack.SetBaseCamera(uiCamera);
			}

			uiScreenStack = new LimitedStack<Tuple<ScreenLayerState, object, ScreenLayer.BootArgBase>>(16);
			screenSize = new ScreenConfig.ScreenSize { width = Screen.width, height = Screen.height };
			OnChangeScreenSize += ForceUpdateScreenResolution;
		}

		private static ScreenLayerDataList LoadEntryScreenLayers()
		{
			const string path = "Screen/SceneEntry/EntryScreenLayers";
			var entryScreenLayers = Resources.Load<ScreenLayerDataList>(path);
			if (entryScreenLayers == null)
			{
				LogUtility.LogError("entryScreenLayers\u304c\u30ed\u30fc\u30c9\u3067\u304d\u307e\u305b\u3093\u3067\u3057\u305f path=" + path, Array.Empty<object>());
				return null;
			}

			if (entryScreenLayers.Entries != null)
			{
				for (var i = 0; i < entryScreenLayers.Entries.Count; i++)
				{
					if (entryScreenLayers.Entries[i] == null)
					{
						LogUtility.LogError(string.Format("entryScreenLayers.Entries[{0}]\u304cnull\u3067\u3059\u3002{1}\u306eInspector\u3092\u78ba\u8a8d\u3057\u3066\u304f\u3060\u3055\u3044", i, path), Array.Empty<object>());
					}
				}
			}

			return entryScreenLayers;
		}

		private void Start()
		{
			CalcContentsSize();
		}

		private void SetUpScreenResolution()
		{
			if (rootCanvasScaler != null)
			{
				var screenAspect = Screen.width > 0 ? (float)Screen.height / Screen.width : BASE_SCREEN_SIZE.y / BASE_SCREEN_SIZE.x;
				rootCanvasScaler.matchWidthOrHeight = screenAspect >= BASE_SCREEN_SIZE.y / BASE_SCREEN_SIZE.x ? 0f : 1f;
				rootCanvasScaler.ForceUpdate();
			}

			Canvas.ForceUpdateCanvases();
			if (rootRect != null)
			{
				rootRect.ForceUpdateRectTransforms();
			}
		}

		private void CalcContentsSize()
		{
			if (rootRect == null)
			{
				contentSize = BASE_SCREEN_SIZE;
				screenToUIRatio = Vector2.one;
				baseScreenToUIRatio = Vector2.one;
				rootScale = 1f;
				inverseRootScale = 1f;
				return;
			}

			var size = rootRect.sizeDelta;
			screenToUIRatio = new Vector2(
				Screen.width > 0 ? size.x / Screen.width : 1f,
				Screen.height > 0 ? size.y / Screen.height : 1f);
			rootScale = transform.localScale.x;
			inverseRootScale = rootCanvas != null ? 1f / rootCanvas.transform.localScale.x : 1f;
			contentSize = size;
			baseScreenToUIRatio = new Vector2(
				BASE_SCREEN_SIZE.x != 0f ? size.x / BASE_SCREEN_SIZE.x : 1f,
				BASE_SCREEN_SIZE.y != 0f ? size.y / BASE_SCREEN_SIZE.y : 1f);
		}

		private void ForceUpdateScreenResolution(ScreenConfig.ScreenSize screenSize)
		{
			SetUpScreenResolution();
			CalcContentsSize();
		}

		private void Update()
		{
			if (Screen.width != screenSize.width || Screen.height != screenSize.height)
			{
				screenSize.width = Screen.width;
				screenSize.height = Screen.height;
				OnChangeScreenSize?.Invoke(screenSize);
			}

			BackKeyChecker?.Check();
		}

		private void OnDestroy()
		{
			screenMap?.Clear();
			screenMap = null;
			if (instance == this)
			{
				instance = null;
			}
		}

		public Camera GetUICamera()
		{
			if (mainUICamera == null)
			{
				mainUICamera = GetComponentInChildren<Camera>(true);
			}

			return mainUICamera;
		}

		public Camera GetBackCamera()
		{
			return backUICamera;
		}

		public Vector2 WorldToScreenPoint(Vector3 position, Vector2? referencePoint = null)
		{
			var camera = RenderCamera != null ? RenderCamera : Camera.main;
			if (camera == null)
			{
				return referencePoint ?? Vector2.zero;
			}

			var screenPoint = camera.WorldToScreenPoint(position);
			var uiPoint = new Vector2(screenPoint.x * screenToUIRatio.x, screenPoint.y * screenToUIRatio.y);
			return referencePoint.HasValue ? uiPoint - referencePoint.Value : uiPoint;
		}

		public void SetupBgmPlayer(IBgmPlayer layerBgmPlayer)
		{
			this.layerBgmPlayer = layerBgmPlayer;
		}

		public void SetupLayerBackgroundChanger(IBackgroundChanger layerBgChanger)
		{
			layerBackgroundChanger = layerBgChanger;
		}

		public void AddScreen(MenuScreenType screenType)
		{
			StartCoroutine(AddScreenCore(screenType, true));
		}

		public void AddScreen(MenuScreenType screenType, bool isForward)
		{
			StartCoroutine(AddScreenCore(screenType, isForward));
		}

		public void AddScreen(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg)
		{
			StartCoroutine(AddScreenCore(screenType, bootArg, true));
		}

		public void AddScreen(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg, bool isForward)
		{
			StartCoroutine(AddScreenCore(screenType, bootArg, isForward));
		}

		[AsyncStateMachine(typeof(_003CAddScreenWithoutHeaderSetting_003Ed__117))]
		public UniTask AddScreenWithoutHeaderSetting(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg = null, CancellationToken ct = default(CancellationToken))
		{
			if (!ct.IsCancellationRequested)
			{
				StartCoroutine(AddScreenCore(screenType, bootArg, true, false));
			}

			return UniTask.CompletedTask;
		}

		public void RemoveScreen(MenuScreenType screenType)
		{
			StartCoroutine(RemoveScreenCore(screenType));
		}

		[IteratorStateMachine(typeof(_003CRemoveScreenCoroutine_003Ed__119))]
		public IEnumerator RemoveScreenCoroutine(MenuScreenType screenType)
		{
			return RemoveScreenCore(screenType);
		}

		[AsyncStateMachine(typeof(_003CRemoveScreenAsync_003Ed__120))]
		public UniTask RemoveScreenAsync(MenuScreenType screenType, CancellationToken ct = default(CancellationToken))
		{
			if (!ct.IsCancellationRequested)
			{
				StartCoroutine(RemoveScreenCore(screenType));
			}

			return UniTask.CompletedTask;
		}

		public bool IsExistInScreenMap(MenuScreenType screenType)
		{
			return screenMap != null && screenMap.ContainsKey(screenType);
		}

		public void ChangeUIScreen(MenuScreenType screenType, bool isWaitExitAnimation = false, bool isForward = true, bool nextScreenAnimationIsForward = true, bool isClearBootDataOnExitCancel = false)
		{
			ChangeUIScreen(screenType, null, isWaitExitAnimation, isForward, nextScreenAnimationIsForward, isClearBootDataOnExitCancel);
		}

		public void ChangeUIScreen(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg, bool isWaitExitAnimation = false, bool isForward = true, bool nextScreenAnimationIsForward = true, bool isClearBootDataOnExitCancel = false)
		{
			if (changeUILayerCoroutine != null)
			{
				return;
			}

			changeUILayerCoroutine = ChangeUIScreenCore(screenType, bootArg, isWaitExitAnimation, isForward, nextScreenAnimationIsForward, isClearBootDataOnExitCancel);
			StartCoroutine(changeUILayerCoroutine);
		}

		public void PushUIScreen(MenuScreenType screenType, bool isWaitExitAnimation = false)
		{
			PushUIScreen(screenType, null, isWaitExitAnimation);
		}

		public void PushUIScreen(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg, bool isWaitExitAnimation = false)
		{
			if (changeUILayerCoroutine != null)
			{
				return;
			}

			if (currentUI?.Data != null && currentUI.Data.ScreenType == screenType)
			{
				return;
			}

			StartCoroutine(PushUIScreenCore(screenType, bootArg, isWaitExitAnimation));
		}

		[AsyncStateMachine(typeof(_003CCheckModuleMaintenance_003Ed__126))]
		private UniTask CheckModuleMaintenance(MenuScreenType type, Action onAllow)
		{
			onAllow?.Invoke();
			return UniTask.CompletedTask;
		}

		public void ShowModuleMaintenanceDialog()
		{
			ShowModuleMaintenanceSubWindowDialog();
		}

		public void ShowModuleMaintenanceSubWindowDialog()
		{
			LogUtility.LogError("Module maintenance dialog requested.", Array.Empty<object>());
		}

		public void BackUIScreen(bool isWaitExitAnimation = false)
		{
			if (OnBackUIScreenOverride != null)
			{
				ExecuteBackUIScreenOverride(isWaitExitAnimation);
				return;
			}

			if (changeUILayerCoroutine != null)
			{
				return;
			}

			changeUILayerCoroutine = PopUIScreen(isWaitExitAnimation);
			StartCoroutine(changeUILayerCoroutine);
		}

		public void BackUIScreenAndDestroyExitedScreen(MenuScreenType screenType, bool isWaitExitAnimation = false)
		{
			if (OnBackUIScreenOverride != null)
			{
				ExecuteBackUIScreenOverride(isWaitExitAnimation);
				DestroyScreenInstance(screenType);
				return;
			}

			if (changeUILayerCoroutine != null)
			{
				return;
			}

			changeUILayerCoroutine = BackUIScreenAndDestroyExitedScreenRoutine(screenType, isWaitExitAnimation);
			StartCoroutine(changeUILayerCoroutine);
		}

		public void ForceBackUIScreen(bool isWaitExitAnimation = false)
		{
			OnBackUIScreenOverride = null;
			BackUIScreen(isWaitExitAnimation);
		}

		private void ExecuteBackUIScreenOverride(bool isWaitExitAnimation)
		{
			var callback = OnBackUIScreenOverride;
			if (IsAutoClearBackUIScreenOverride)
			{
				OnBackUIScreenOverride = null;
			}

			callback?.Invoke(isWaitExitAnimation);
		}

		public void PreloadUIScreen(MenuScreenType screenType)
		{
			StartCoroutine(PreloadUIScreenCore(screenType));
		}

		public void RebuildScreen(MenuScreenType screenType, Action<ScreenLayer> onFinished = null, bool isFoward = true, bool isClearBootDataOnExitCancel = false)
		{
			StartCoroutine(RebuildUIScreenCore(screenType, onFinished, isFoward, isClearBootDataOnExitCancel));
		}

		public UniTask RebuildScreenAsync(MenuScreenType screenType, Action<ScreenLayer> onFinished = null, bool isFoward = true, bool isClearBootDataOnExitCancel = false)
		{
			StartCoroutine(RebuildUIScreenCore(screenType, onFinished, isFoward, isClearBootDataOnExitCancel));
			return UniTask.CompletedTask;
		}

		public GameObject GetLayerObject(DisplayLayerType layerType)
		{
			displayLayersCache ??= new Dictionary<DisplayLayerType, GameObject>();
			if (displayLayersCache.TryGetValue(layerType, out var layerObject))
			{
				return layerObject;
			}

			var layerTransform = transform.Find(layerType.ToString());
			if (layerTransform == null)
			{
				return null;
			}

			layerObject = layerTransform.gameObject;
			displayLayersCache.Add(layerType, layerObject);
			return layerObject;
		}

		[IteratorStateMachine(typeof(_003CChangeUIScreenCore_003Ed__136))]
		private IEnumerator ChangeUIScreenCore(MenuScreenType screenType, bool isWaitExitAnimation = true, bool isForward = true, bool nextScreenAnimationIsForward = true, bool isClearBootDataOnExitCancel = false)
		{
			return ChangeUIScreenCore(screenType, null, isWaitExitAnimation, isForward, nextScreenAnimationIsForward, isClearBootDataOnExitCancel);
		}

		[IteratorStateMachine(typeof(_003CChangeUIScreenCore_003Ed__137))]
		private IEnumerator ChangeUIScreenCore(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg, bool isWaitExitAnimation = true, bool isForward = true, bool nextScreenAnimationIsForward = true, bool isClearBootDataOnExitCancel = false)
		{
			return ChangeUIScreenCoreRoutine(screenType, bootArg, isWaitExitAnimation, isForward, nextScreenAnimationIsForward, isClearBootDataOnExitCancel);
		}

		[IteratorStateMachine(typeof(_003CPushUIScreenCore_003Ed__138))]
		private IEnumerator PushUIScreenCore(MenuScreenType screenType, bool isWaitExitAnimation = true)
		{
			return PushUIScreenCore(screenType, null, isWaitExitAnimation);
		}

		[IteratorStateMachine(typeof(_003CPushUIScreenCore_003Ed__139))]
		private IEnumerator PushUIScreenCore(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg = null, bool isWaitExitAnimation = true)
		{
			return PushUIScreenCoreRoutine(screenType, bootArg, isWaitExitAnimation);
		}

		[IteratorStateMachine(typeof(_003CPopUIScreen_003Ed__140))]
		private IEnumerator PopUIScreen(bool isWaitExitAnimation = true)
		{
			return PopUIScreenRoutine(isWaitExitAnimation);
		}

		private IEnumerator BackUIScreenAndDestroyExitedScreenRoutine(MenuScreenType screenType, bool isWaitExitAnimation)
		{
			yield return StartCoroutine(PopUIScreenRoutine(isWaitExitAnimation));
			DestroyScreenInstance(screenType);
			changeUILayerCoroutine = null;
		}

		[IteratorStateMachine(typeof(_003CPreloadUIScreenCore_003Ed__141))]
		private IEnumerator PreloadUIScreenCore(MenuScreenType screenType)
		{
			return PreloadUIScreenCoreRoutine(screenType);
		}

		[IteratorStateMachine(typeof(_003CRebuildUIScreenCore_003Ed__142))]
		private IEnumerator RebuildUIScreenCore(MenuScreenType screenType, Action<ScreenLayer> onFinished, bool isFoward = true, bool isClearBootDataOnExitCancel = false)
		{
			return RebuildUIScreenCoreRoutine(screenType, onFinished, isFoward, isClearBootDataOnExitCancel);
		}

		[IteratorStateMachine(typeof(_003CAddScreenCore_003Ed__143))]
		private IEnumerator AddScreenCore(MenuScreenType screenType, bool isForward = true)
		{
			return AddScreenCore(screenType, null, isForward);
		}

		[IteratorStateMachine(typeof(_003CAddScreenCore_003Ed__144))]
		private IEnumerator AddScreenCore(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg, bool isForward = true, bool applyHeaderSetting = true)
		{
			if (screenMap == null || !screenMap.TryGetValue(screenType, out var layer))
			{
				LogUtility.LogError("Not Register ScreenLayer;" + screenType, Array.Empty<object>());
				return EmptyCoroutine();
			}

			return AddScreenCore(layer, bootArg, isForward, null, applyHeaderSetting);
		}

		private void InstantiateScreen(ScreenLayerState layer)
		{
			if (layer == null)
			{
				return;
			}

			if (layer.RefInstance == null)
			{
				if (!InstantiateLayerScreen(layer))
				{
					return;
				}
			}

			if (layer.ScreenLayer != null && layer.ScreenLayer.gameObject.activeSelf)
			{
				layer.ScreenLayer.gameObject.SetActive(false);
			}
		}

		[IteratorStateMachine(typeof(_003CAddScreenCore_003Ed__146))]
		private IEnumerator AddScreenCore(ScreenLayerState layer, ScreenLayer.BootArgBase bootArg, bool isForward = true, object layerObject = null, bool applyHeaderSetting = true)
		{
			return AddScreenCoreRoutine(layer, bootArg, isForward, layerObject, applyHeaderSetting);
		}

		[IteratorStateMachine(typeof(_003CRemoveScreenCore_003Ed__147))]
		private IEnumerator RemoveScreenCore(MenuScreenType screenType, bool isForward = true, bool isClearBootDataOnExitCancel = false)
		{
			return RemoveScreenCoreRoutine(screenType, isForward, isClearBootDataOnExitCancel);
		}

		[IteratorStateMachine(typeof(_003CExitScreen_003Ed__148))]
		private IEnumerator ExitScreen(ScreenLayerState layer, bool executeOnWillExit, bool isForward = true, bool isClearBootDataOnExitCancel = false)
		{
			return ExitScreenRoutine(layer, executeOnWillExit, isForward, isClearBootDataOnExitCancel);
		}

		public void ClearScreenStack()
		{
			uiScreenStack?.Clear();
		}

		public void PopScreenStack()
		{
			uiScreenStack?.Pop();
		}

		public void PushScreenStack(MenuScreenType screenType, object screenObject = null)
		{
			if (screenMap != null && screenMap.TryGetValue(screenType, out var layer))
			{
				uiScreenStack?.Push(Tuple.Create(layer, screenObject, (ScreenLayer.BootArgBase)null));
			}
		}

		public List<MenuScreenType> GetScreenStackList()
		{
			var result = new List<MenuScreenType>();
			if (uiScreenStack == null)
			{
				return result;
			}

			for (var i = 0; i < uiScreenStack.Count; i++)
			{
				var layer = uiScreenStack[i]?.Item1;
				if (layer?.Data != null)
				{
					result.Add(layer.Data.ScreenType);
				}
			}

			return result;
		}

		private bool InstantiateLayerScreen(ScreenLayerState layer)
		{
			if (layer?.Data == null)
			{
				return false;
			}

			var layerObject = GetLayerObject(layer.Data.DisplayLayer);
			if (layerObject == null)
			{
				LogUtility.LogError("Not Found Layer;" + layer.Data.DisplayLayer, Array.Empty<object>());
				return false;
			}

			var prefab = layer.Data.LoadScreenLayerPrefab();
			if (prefab == null)
			{
				LogUtility.LogError("Not Found ScreenLayer;" + layer.Data.name, Array.Empty<object>());
				return false;
			}

			var instanceObject = Instantiate(prefab, layerObject.transform);
			instanceObject.transform.localScale = Vector3.one;
			instanceObject.transform.localPosition = Vector3.zero;
			instanceObject.name = layer.Data.name;
			layer.RefInstance = instanceObject;
			layer.ScreenLayer = instanceObject.GetComponent<ScreenLayer>();
			if (layer.ScreenLayer == null)
			{
				LogUtility.LogError("Not Found ScreenLayer;" + instanceObject.name, Array.Empty<object>());
				return false;
			}

			if (layer.Data.hasLayerCamera && layer.ScreenLayer.LayerCamera != null && cameraStack != null)
			{
				cameraStack.AddToStack(layer.ScreenLayer.LayerCamera, layer.Data.layerCameraPriority);
			}

			return true;
		}

		public void DestroyScreenInstance(MenuScreenType screenType)
		{
			if (screenMap == null || !screenMap.TryGetValue(screenType, out var layer) || layer == null)
			{
				return;
			}

			if (layer.ScreenLayer != null)
			{
				if (layer.Data != null && layer.Data.hasLayerCamera && layer.ScreenLayer.LayerCamera != null && cameraStack != null)
				{
					cameraStack.RemoveFromStack(layer.ScreenLayer.LayerCamera);
				}
				layer.ScreenLayer.OnScreenLayerExitScene();
			}

			if (layer.RefInstance != null)
			{
				if (Application.isPlaying)
				{
					Destroy(layer.RefInstance);
				}
				else
				{
					DestroyImmediate(layer.RefInstance);
				}
			}

			layer.RefInstance = null;
			layer.ScreenLayer = null;
		}

		public T GetLayerComponent<T>(MenuScreenType screenType) where T : ScreenLayer
		{
			if (screenMap != null && screenMap.TryGetValue(screenType, out var layer))
			{
				return layer.ScreenLayer as T;
			}

			return null;
		}

		public bool IsActiveScreen(MenuScreenType screenType)
		{
			return currentUI?.Data != null && currentUI.Data.ScreenType == screenType;
		}

		public bool IsInstantiatedScreen(MenuScreenType screenType)
		{
			return screenMap != null && screenMap.TryGetValue(screenType, out var layer) && layer.RefInstance != null;
		}

		private static IEnumerator EmptyCoroutine()
		{
			yield break;
		}

		private IEnumerator ChangeUIScreenCoreRoutine(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg, bool isWaitExitAnimation, bool isForward, bool nextScreenAnimationIsForward, bool isClearBootDataOnExitCancel)
		{
			if (screenMap == null || !screenMap.TryGetValue(screenType, out var layer))
			{
				LogUtility.LogError("Not Register ScreenLayer;" + screenType, Array.Empty<object>());
				changeUILayerCoroutine = null;
				yield break;
			}

			var oldCurrent = currentUI;
			nextUI = layer;
			if (oldCurrent != null)
			{
				var exitCoroutine = ExitScreen(oldCurrent, true, isForward, isClearBootDataOnExitCancel);
				if (isWaitExitAnimation)
				{
					yield return StartCoroutine(exitCoroutine);
				}
				else
				{
					StartCoroutine(exitCoroutine);
				}
			}

			prevUI = oldCurrent;
			currentUI = layer;
			yield return StartCoroutine(AddScreenCore(currentUI, bootArg, nextScreenAnimationIsForward));
			OnChangeUILayer?.Invoke(currentUI.Data.ScreenType);
			yield return WaitUntilPlaying(currentUI);
			changeUILayerCoroutine = null;
		}

		private IEnumerator PushUIScreenCoreRoutine(MenuScreenType screenType, ScreenLayer.BootArgBase bootArg, bool isWaitExitAnimation)
		{
			if (screenMap == null || !screenMap.TryGetValue(screenType, out var layer))
			{
				LogUtility.LogError("Not Register ScreenLayer;" + screenType, Array.Empty<object>());
				changeUILayerCoroutine = null;
				yield break;
			}

			var oldCurrent = currentUI;
			nextUI = layer;
			if (oldCurrent != null && oldCurrent.ScreenLayer != null)
			{
				oldCurrent.ScreenLayer.OnWillExit();
				while (oldCurrent.ScreenLayer.WillExitBehaviour == ScreenLayer.WillExitBehaviourStatus.None)
				{
					yield return null;
				}

				if (oldCurrent.ScreenLayer.WillExitBehaviour == ScreenLayer.WillExitBehaviourStatus.Cancel)
				{
					nextUI = null;
					changeUILayerCoroutine = null;
					yield break;
				}

				uiScreenStack?.Push(Tuple.Create(oldCurrent, oldCurrent.ScreenLayer.GetStackObject(), bootArg));
				var exitCoroutine = ExitScreen(oldCurrent, false, true);
				if (isWaitExitAnimation)
				{
					yield return StartCoroutine(exitCoroutine);
				}
				else
				{
					StartCoroutine(exitCoroutine);
				}
			}

			prevUI = oldCurrent;
			currentUI = layer;
			yield return StartCoroutine(AddScreenCore(currentUI, bootArg, true));
			OnChangeUILayer?.Invoke(currentUI.Data.ScreenType);
			yield return WaitUntilPlaying(currentUI);
			changeUILayerCoroutine = null;
		}

		private IEnumerator PopUIScreenRoutine(bool isWaitExitAnimation)
		{
			if (uiScreenStack == null || uiScreenStack.Count <= 0)
			{
				changeUILayerCoroutine = null;
				yield break;
			}

			IsPopUIScreen = true;
			var layerData = uiScreenStack.Pop();
			var layer = layerData?.Item1;
			if (layer == null)
			{
				changeUILayerCoroutine = null;
				IsPopUIScreen = false;
				yield break;
			}

			var oldCurrent = currentUI;
			nextUI = layer;
			if (oldCurrent != null)
			{
				var exitCoroutine = ExitScreen(oldCurrent, true, false);
				if (isWaitExitAnimation)
				{
					yield return StartCoroutine(exitCoroutine);
				}
				else
				{
					StartCoroutine(exitCoroutine);
				}
			}

			prevUI = oldCurrent;
			currentUI = layer;
			yield return StartCoroutine(AddScreenCore(layer, layerData.Item3, false, layerData.Item2));
			OnChangeUILayer?.Invoke(layer.Data.ScreenType);
			yield return WaitUntilPlaying(layer);
			IsPopUIScreen = false;
			changeUILayerCoroutine = null;
		}

		private IEnumerator PreloadUIScreenCoreRoutine(MenuScreenType screenType)
		{
			if (screenMap != null && screenMap.TryGetValue(screenType, out var layer))
			{
				InstantiateScreen(layer);
			}

			yield break;
		}

		private IEnumerator RebuildUIScreenCoreRoutine(MenuScreenType screenType, Action<ScreenLayer> onFinished, bool isFoward, bool isClearBootDataOnExitCancel)
		{
			if (screenMap != null && screenMap.TryGetValue(screenType, out var layer))
			{
				yield return StartCoroutine(ExitScreen(layer, true, isFoward, isClearBootDataOnExitCancel));
				yield return StartCoroutine(AddScreenCore(layer, null, isFoward));
				onFinished?.Invoke(layer.ScreenLayer);
			}
		}

		private IEnumerator AddScreenCoreRoutine(ScreenLayerState layer, ScreenLayer.BootArgBase bootArg, bool isForward, object layerObject, bool applyHeaderSetting)
		{
			if (layer == null)
			{
				yield break;
			}

			if (layer.RefInstance == null && !InstantiateLayerScreen(layer))
			{
				yield break;
			}

			if (layer.ScreenLayer == null)
			{
				yield break;
			}

			layer.ScreenLayer.gameObject.SetActive(true);
			layer.ScreenLayer.LayerStackObject = layerObject;
			var rectTransform = layer.ScreenLayer.GetComponent<RectTransform>();
			if (rectTransform != null)
			{
				rectTransform.SetAsLastSibling();
			}

			layer.ScreenLayer.ScreenInsertDirection = isForward ? ScreenLayer.InsertDirection.Forward : ScreenLayer.InsertDirection.Back;
			if (!isForward)
			{
				layer.ScreenLayer.ReverseStartAnimationType();
			}

			BootArgObject = bootArg;
			yield return StartCoroutine(layer.ScreenLayer.OnScreenLayerBoot(layer.Data, bootArg));
			if (layer.Data.ScreenType != MenuScreenType.NetworkIndicator)
			{
				BootArgObject = null;
			}

			if (!isForward)
			{
				layer.ScreenLayer.ReverseStartAnimationType();
			}

			layerBgmPlayer?.Play(layer.Data.bgmType);
			layerBackgroundChanger?.Show(layer.Data.BackgroundType);
			if (applyHeaderSetting)
			{
				HeaderUtility.SetDisplayHeader(layer.Data);
			}
			layer.ScreenLayer.OnScreenLayerInitComponent();
			layer.ScreenLayer.OnScreenLayerStart();
		}

		private IEnumerator RemoveScreenCoreRoutine(MenuScreenType screenType, bool isForward, bool isClearBootDataOnExitCancel)
		{
			if (screenMap != null && screenMap.TryGetValue(screenType, out var layer))
			{
				yield return StartCoroutine(ExitScreen(layer, true, isForward, isClearBootDataOnExitCancel));
			}
		}

		private IEnumerator ExitScreenRoutine(ScreenLayerState layer, bool executeOnWillExit, bool isForward, bool isClearBootDataOnExitCancel)
		{
			if (layer?.ScreenLayer == null)
			{
				yield break;
			}

			if (executeOnWillExit)
			{
				layer.ScreenLayer.OnWillExit();
				while (layer.ScreenLayer.WillExitBehaviour == ScreenLayer.WillExitBehaviourStatus.None)
				{
					yield return null;
				}

				if (layer.ScreenLayer.WillExitBehaviour == ScreenLayer.WillExitBehaviourStatus.Cancel)
				{
					if (isClearBootDataOnExitCancel)
					{
						BootArgObject = null;
					}
					yield break;
				}
			}

			if (!isForward)
			{
				layer.ScreenLayer.ReverseExitAnimationType();
			}

			yield return StartCoroutine(layer.ScreenLayer.OnScreenLayerExitStart());

			if (!isForward)
			{
				layer.ScreenLayer.ReverseExitAnimationType();
			}

			if (layer.ScreenLayer != null)
			{
				layer.ScreenLayer.gameObject.SetActive(false);
			}
		}

		private static IEnumerator WaitUntilPlaying(ScreenLayerState layer)
		{
			while (layer?.ScreenLayer != null && layer.ScreenLayer.ScreenLayerState != ScreenLayer.State.Playing)
			{
				yield return null;
			}
		}

		public void SwitchNetworkIndicatorDisplayType(ScreenLayerNetworkIndicator.DisplayType displayType)
		{
			ScreenLayerNetworkIndicator.CurrentDisplayType = displayType;
			var layer = GetLayerComponent<ScreenLayerNetworkIndicator>(MenuScreenType.NetworkIndicator);
			if (layer != null)
			{
				layer.UpdateDisplayType();
			}
		}

		public ScreenLayerNetworkIndicator ShowNetworkIndicatorScreen(UnityEngine.Object holdObject)
		{
			var layer = GetLayerComponent<ScreenLayerNetworkIndicator>(MenuScreenType.NetworkIndicator);
			if (layer == null)
			{
				AddScreen(MenuScreenType.NetworkIndicator);
				layer = GetLayerComponent<ScreenLayerNetworkIndicator>(MenuScreenType.NetworkIndicator);
			}

			layer?.Hold(holdObject);
			return layer;
		}

		public void ReleaseNetworkIndicatorScreen(UnityEngine.Object holdObject)
		{
			GetLayerComponent<ScreenLayerNetworkIndicator>(MenuScreenType.NetworkIndicator)?.Release(holdObject);
		}

		public void ReleaseNetworkIndicatorScreenForce()
		{
			GetLayerComponent<ScreenLayerNetworkIndicator>(MenuScreenType.NetworkIndicator)?.ForceRelease();
		}

		public ScreenLayerLoading ShowLoadingScreen(UnityEngine.Object holdObject)
		{
			var layer = GetLayerComponent<ScreenLayerLoading>(MenuScreenType.Loading);
			if (layer == null)
			{
				AddScreen(MenuScreenType.Loading);
				layer = GetLayerComponent<ScreenLayerLoading>(MenuScreenType.Loading);
			}

			layer?.Hold(holdObject);
			return layer;
		}

		public void ReleaseLoadingScreen(UnityEngine.Object holdObject)
		{
			GetLayerComponent<ScreenLayerLoading>(MenuScreenType.Loading)?.Release(holdObject);
		}

		public ScreenLayerAssetRemoving ShowAssetRemovingScreen(UnityEngine.Object holdObject)
		{
			var layer = GetLayerComponent<ScreenLayerAssetRemoving>(MenuScreenType.AssetRemoving);
			if (layer == null)
			{
				AddScreen(MenuScreenType.AssetRemoving);
				layer = GetLayerComponent<ScreenLayerAssetRemoving>(MenuScreenType.AssetRemoving);
			}

			layer?.Hold(holdObject);
			return layer;
		}

		public void ReleaseAssetRemovingScreen(UnityEngine.Object holdObject)
		{
			GetLayerComponent<ScreenLayerAssetRemoving>(MenuScreenType.AssetRemoving)?.Release(holdObject);
		}

		public void ReleaseLoadingScreenForce()
		{
			GetLayerComponent<ScreenLayerLoading>(MenuScreenType.Loading)?.ForceRelease();
		}

		public ScreenLayerLoading.DownloadStatus GetLoadingScreenState()
		{
			return GetLayerComponent<ScreenLayerLoading>(MenuScreenType.Loading)?.CurrentDownloadStatus ?? default;
		}

		public int GetLoadingScreenHoldingCount()
		{
			return GetLayerComponent<ScreenLayerLoading>(MenuScreenType.Loading)?.CurrentHoldObjectCount ?? 0;
		}

		public void SetScreenCoverDirect(Color color)
		{
			fadeController.Set(color);
		}

		public void FadeIn(float delay, float duration, Action onFinish = null)
		{
			fadeController.Play(fadeInColor, delay, duration, onFinish);
		}

		public void FadeIn(Color color, float delay, float duration, Action onFinish = null)
		{
			fadeController.Play(color, delay, duration, onFinish);
		}

		public void FadeOut(float delay, float duration, Action onFinish = null)
		{
			fadeController.Play(fadeOutColor, delay, duration, onFinish);
		}

		[AsyncStateMachine(typeof(_003CFadeOutAsync_003Ed__172))]
		public UniTask FadeOutAsync(float duration, CancellationToken token)
		{
			if (!token.IsCancellationRequested)
			{
				FadeOut(0f, duration);
			}

			return UniTask.CompletedTask;
		}

		public void FadeOut(Color color, float delay, float duration, Action onFinish = null)
		{
			fadeController.Play(color, delay, duration, onFinish);
		}

		public void WhiteOut(float delay, float duration, Action onFinish = null)
		{
			fadeController.Play(Color.white, delay, duration, onFinish);
		}

		public void WhiteIn(float delay, float duration, Action onFinish = null)
		{
			var color = Color.white;
			color.a = 0f;
			fadeController.Play(color, delay, duration, onFinish);
		}

		public void FadeRoot(bool isShow, bool interactable, bool blockRaycasts = true, float duration = 0.2f, float delay = 0f, Action onFinish = null)
		{
			if (rootCanvasGroup != null)
			{
				rootCanvasGroup.alpha = isShow ? 1f : 0f;
				rootCanvasGroup.interactable = interactable;
				rootCanvasGroup.blocksRaycasts = blockRaycasts;
			}

			onFinish?.Invoke();
		}

		private void SetAllLayerCanvasGroup(bool interactable, bool blockRaycasts = true)
		{
			if (displayLayersCache == null)
			{
				return;
			}

			foreach (var layer in displayLayersCache.Keys)
			{
				SetLayerCanvasGroup(layer, interactable, blockRaycasts);
			}
		}

		public void SetLayerCanvasGroup(DisplayLayerType layer, bool interactable, bool blockRaycasts = true)
		{
			var layerObject = GetLayerObject(layer);
			if (layerObject == null)
			{
				return;
			}

			var canvasGroup = layerObject.GetComponent<CanvasGroup>() ?? layerObject.AddComponent<CanvasGroup>();
			canvasGroup.interactable = interactable;
			canvasGroup.blocksRaycasts = blockRaycasts;
		}

		public void FadeOutLayer(DisplayLayerType layer, float delay, float duration, Action onFinish = null)
		{
			SetLayerCanvasGroup(layer, false, false);
			onFinish?.Invoke();
		}

		public void FadeInLayer(DisplayLayerType layer, float delay, float duration, Action onFinish = null)
		{
			SetLayerCanvasGroup(layer, true, true);
			onFinish?.Invoke();
		}

		private Dictionary<UnityEngine.Object, bool> GetDisableCommandMap(string key)
		{
			disableTapCommandObjects ??= new Dictionary<string, Dictionary<UnityEngine.Object, bool>>();
			if (!disableTapCommandObjects.TryGetValue(key, out var map))
			{
				map = new Dictionary<UnityEngine.Object, bool>();
				disableTapCommandObjects[key] = map;
			}

			return map;
		}

		public void EnableTapScreenForce()
		{
			GetDisableCommandMap("all").Clear();
			if (rootCanvasGroup != null)
			{
				rootCanvasGroup.interactable = true;
				rootCanvasGroup.blocksRaycasts = true;
			}
		}

		public void EnableTapScreenForce(DisplayLayerType layer)
		{
			var layerObject = GetLayerObject(layer);
			if (layerObject == null)
			{
				return;
			}

			GetDisableCommandMap(layerObject.name).Clear();
			SetLayerCanvasGroup(layer, true, true);
		}

		public void DisableTapScreen(UnityEngine.Object commandObject)
		{
			DisableTapScreen(commandObject, default);
		}

		public void DisableTapScreen(UnityEngine.Object commandObject, DisplayLayerType layer)
		{
			if (commandObject == null)
			{
				return;
			}

			if (EqualityComparer<DisplayLayerType>.Default.Equals(layer, default))
			{
				GetDisableCommandMap("all")[commandObject] = true;
				if (rootCanvasGroup != null)
				{
					rootCanvasGroup.interactable = false;
					rootCanvasGroup.blocksRaycasts = true;
				}
				return;
			}

			var layerObject = GetLayerObject(layer);
			if (layerObject != null)
			{
				GetDisableCommandMap(layerObject.name)[commandObject] = true;
				SetLayerCanvasGroup(layer, false, true);
			}
		}

		public void EnableTapScreen(UnityEngine.Object commandObject)
		{
			EnableTapScreen(commandObject, default);
		}

		public void EnableTapScreen(UnityEngine.Object commandObject, DisplayLayerType layer)
		{
			if (commandObject == null)
			{
				return;
			}

			if (EqualityComparer<DisplayLayerType>.Default.Equals(layer, default))
			{
				var map = GetDisableCommandMap("all");
				map.Remove(commandObject);
				if (rootCanvasGroup != null && map.Count == 0)
				{
					rootCanvasGroup.interactable = true;
					rootCanvasGroup.blocksRaycasts = true;
				}
				return;
			}

			var layerObject = GetLayerObject(layer);
			if (layerObject != null)
			{
				var map = GetDisableCommandMap(layerObject.name);
				map.Remove(commandObject);
				if (map.Count == 0)
				{
					SetLayerCanvasGroup(layer, true, true);
				}
			}
		}

		private void EnableTapScreenCore(string key, CanvasGroup canvasGroup, UnityEngine.Object commandObject)
		{
			if (canvasGroup == null || commandObject == null)
			{
				return;
			}

			var map = GetDisableCommandMap(key);
			map.Remove(commandObject);
			if (map.Count == 0)
			{
				canvasGroup.interactable = true;
				canvasGroup.blocksRaycasts = true;
			}
		}

		public bool CanTapByRootCanvas()
		{
			return rootCanvasGroup == null || rootCanvasGroup.interactable;
		}

		public bool CanTapByDisplayLayerType(DisplayLayerType layer)
		{
			var layerObject = GetLayerObject(layer);
			if (layerObject == null)
			{
				return true;
			}

			var canvasGroup = layerObject.GetComponent<CanvasGroup>();
			return canvasGroup == null || canvasGroup.interactable;
		}

		public string GetDisplayLayerName(DisplayLayerType layerTye)
		{
			return layerTye.ToString();
		}

		public T GetBootData<T>()
		{
			if (BootArgObject is T value)
			{
				return value;
			}

			return default;
		}

		public MenuScreenType GetPeekScreenType()
		{
			var layer = uiScreenStack?.Peek()?.Item1;
			return layer?.Data != null ? layer.Data.ScreenType : MenuScreenType.None;
		}

		public bool TryGetScreenLayerData(MenuScreenType screenType, out ScreenLayerData screenLayerData)
		{
			screenLayerData = null;
			if (screenMap == null || !screenMap.TryGetValue(screenType, out var layer))
			{
				return false;
			}

			screenLayerData = layer.Data;
			return screenLayerData != null;
		}

		public MenuScreenType GetCurrentUIScreenType()
		{
			return currentUI?.Data != null ? currentUI.Data.ScreenType : MenuScreenType.None;
		}

		public MenuScreenType GetNextUIScreenType()
		{
			return nextUI?.Data != null ? nextUI.Data.ScreenType : MenuScreenType.None;
		}

		public MenuScreenType GetPrevUIScreenType()
		{
			return prevUI?.Data != null ? prevUI.Data.ScreenType : MenuScreenType.None;
		}

		public void ExitScene()
		{
			foreach (var pair in screenMap ?? new Dictionary<MenuScreenType, ScreenLayerState>())
			{
				pair.Value.ScreenLayer?.OnScreenLayerExitScene();
			}
		}

		public T InstantiateDialog<T>(DialogType dialogType, DisplayLayerType layerType) where T : DialogBase
		{
			var layerObject = GetLayerObject(layerType);
			return layerObject != null ? InstantiateDialog<T>(dialogType, layerObject.transform) : null;
		}

		public T InstantiateDialog<T>(DialogType dialogType, Transform parent) where T : DialogBase
		{
			string dialogName = dialogType.ToString();
			GameObject prefab = Resources.Load<GameObject>(DIALOG_PREFAB_PATH_BASE + dialogName);
			if (prefab == null)
			{
				prefab = Resources.Load<GameObject>("dialog/" + dialogName);
			}
			if (prefab == null)
			{
				UnityEngine.Debug.LogError($"Dialog prefab not found: {dialogName}");
				return null;
			}

			GameObject instance = UnityEngine.Object.Instantiate(prefab, parent, false);
			instance.name = dialogName;
			T dialog = instance.GetComponent<T>() ?? instance.GetComponentInChildren<T>(true);
			if (dialog == null)
			{
				UnityEngine.Debug.LogError($"Dialog component {typeof(T).Name} not found on prefab: {dialogName}");
				UnityEngine.Object.Destroy(instance);
			}
			return dialog;
		}

		public T ShowDialog<T>(DialogType dialogType, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true) where T : DialogBase
		{
			T dialog = InstantiateDialog<T>(dialogType, layerType);
			dialog?.Initialize(dialogSize, allowCloseExternal);
			OpenDialog(dialog, layerType);
			return dialog;
		}

		public T Show1ButtonDialog<T>(DialogType dialogType, string messageBodyKey, string okButtonLabelKey, Action onClickOK, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true) where T : Common1ButtonDialog
		{
			T dialog = InstantiateDialog<T>(dialogType, layerType);
			dialog?.Initialize(messageBodyKey, okButtonLabelKey, onClickOK, dialogSize, allowCloseExternal);
			OpenDialog(dialog, layerType);
			return dialog;
		}

		public T Show1ButtonDialog<T>(DialogType dialogType, string titleKey, string messageBodyKey, string okButtonLabelKey, Action onClickOK, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true) where T : Common1ButtonDialog
		{
			T dialog = InstantiateDialog<T>(dialogType, layerType);
			dialog?.Initialize(titleKey, messageBodyKey, okButtonLabelKey, onClickOK, dialogSize, allowCloseExternal);
			OpenDialog(dialog, layerType);
			return dialog;
		}

		public T Show1ButtonDialog<T>(DialogType dialogType, Action onClickOK, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true) where T : Common1ButtonDialog
		{
			return Show1ButtonDialog<T>(dialogType, null, null, onClickOK, layerType, dialogSize, allowCloseExternal);
		}

		public T Show2ButtonDialog<T>(DialogType dialogType, string messageBodyKey, string okButtonLabelKey, string cancelButtonLabelKey, Action onClickOK, Action onClickCancel, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true) where T : Common2ButtonDialog
		{
			T dialog = InstantiateDialog<T>(dialogType, layerType);
			dialog?.Initialize(messageBodyKey, okButtonLabelKey, cancelButtonLabelKey, onClickOK, onClickCancel, dialogSize, allowCloseExternal);
			OpenDialog(dialog, layerType);
			return dialog;
		}

		public T Show2ButtonDialog<T>(DialogType dialogType, string titleKey, string messageBodyKey, string okButtonLabelKey, string cancelButtonLabelKey, Action onClickOK, Action onClickCancel, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true) where T : Common2ButtonDialog
		{
			T dialog = InstantiateDialog<T>(dialogType, layerType);
			dialog?.Initialize(titleKey, messageBodyKey, okButtonLabelKey, cancelButtonLabelKey, onClickOK, onClickCancel, dialogSize, allowCloseExternal);
			OpenDialog(dialog, layerType);
			return dialog;
		}

		public T Show2ButtonDialog<T>(DialogType dialogType, Action onClickOK, Action onClickCancel, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true) where T : Common2ButtonDialog
		{
			return Show2ButtonDialog<T>(dialogType, null, null, null, onClickOK, onClickCancel, layerType, dialogSize, allowCloseExternal);
		}

		public T Show2ButtonDialog<T>(DialogType dialogType) where T : Common2ButtonDialog
		{
			return Show2ButtonDialog<T>(dialogType, null, null);
		}

		public T ShowMultiButtonDialog<T>(DialogType dialogType, string messageBodyKey, Dictionary<string, string> labelKeyDic, Dictionary<string, Action> actionDic, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true) where T : CommonMultiButtonDialog
		{
			T dialog = InstantiateDialog<T>(dialogType, layerType);
			if (dialog == null)
			{
				return null;
			}

			dialog.Initialize(messageBodyKey, labelKeyDic, actionDic, dialogSize, allowCloseExternal);
			OpenDialog(dialog, layerType);
			return dialog;
		}

		public T ShowMultiButtonDialog<T>(DialogType dialogType, Dictionary<string, Action> actionDic, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog, DialogSize dialogSize = DialogSize.Manual, bool allowCloseExternal = true) where T : CommonMultiButtonDialog
		{
			T dialog = InstantiateDialog<T>(dialogType, layerType);
			if (dialog == null)
			{
				return null;
			}

			dialog.Initialize(actionDic, dialogSize, allowCloseExternal);
			OpenDialog(dialog, layerType);
			return dialog;
		}

		public T ShowSubWindowDialog<T>(string messageBody = null, Action onClose = null, bool allowCloseExternal = true, DialogType dialogType = DialogType.SubWindowDialog, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog) where T : SubWindowDialog
		{
			T dialog = InstantiateDialog<T>(dialogType, layerType);
			if (dialog == null)
			{
				return null;
			}

			dialog.Initialize(messageBody, onClose, allowCloseExternal);
			dialog.Open();
			return dialog;
		}

		public void CloseDialogs(DisplayLayerType layerType = DisplayLayerType.Layer_Dialog)
		{
			foreach (DialogBase dialog in GetActiveDialogs(layerType))
			{
				if (dialog != null)
				{
					dialog.Close();
				}
			}
		}

		public bool IsActiveDialog(string dialogName, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog)
		{
			return GetActiveDialog(dialogName, layerType) != null;
		}

		public DialogBase GetActiveDialog(string dialogName, DisplayLayerType layerType = DisplayLayerType.Layer_Dialog)
		{
			foreach (DialogBase dialog in GetActiveDialogs(layerType))
			{
				if (dialog != null && dialog.name == dialogName)
				{
					return dialog;
				}
			}
			return null;
		}

		public T GetActiveDialog<T>(DisplayLayerType layerType = DisplayLayerType.Layer_Dialog) where T : DialogBase
		{
			TryGetActiveDialog(layerType, out T dialog);
			return dialog;
		}

		public bool TryGetActiveDialog<T>(out T dialog) where T : DialogBase
		{
			return TryGetActiveDialog(DisplayLayerType.Layer_Dialog, out dialog);
		}

		public bool TryGetActiveDialog<T>(DisplayLayerType layerType, out T dialog) where T : DialogBase
		{
			dialog = null;
			foreach (T activeDialog in GetActiveDialogs<T>(layerType))
			{
				if (activeDialog != null)
				{
					dialog = activeDialog;
					return true;
				}
			}
			return false;
		}

		public T[] GetActiveDialogs<T>(DisplayLayerType layerType = DisplayLayerType.Layer_Dialog) where T : DialogBase
		{
			var result = new List<T>();
			var layerObject = GetLayerObject(layerType);
			if (layerObject == null)
			{
				return result.ToArray();
			}

			foreach (T dialog in layerObject.GetComponentsInChildren<T>(true))
			{
				if (dialog != null && dialog.gameObject.activeSelf)
				{
					result.Add(dialog);
				}
			}
			return result.ToArray();
		}

		public bool ExistsDialog(DisplayLayerType layerType = DisplayLayerType.Layer_Dialog)
		{
			return GetActiveDialogs(layerType).Length > 0;
		}

		public DialogBase[] GetActiveDialogs(DisplayLayerType layerType = DisplayLayerType.Layer_Dialog)
		{
			return GetActiveDialogs<DialogBase>(layerType);
		}

		public bool IsActiveDialog<T>(DisplayLayerType layerType = DisplayLayerType.Layer_Dialog) where T : UnityEngine.Object
		{
			var layerObject = GetLayerObject(layerType);
			if (layerObject == null)
			{
				return false;
			}

			foreach (T component in layerObject.GetComponentsInChildren<T>(true))
			{
				if (component != null)
				{
					return true;
				}
			}
			return false;
		}

		public void CloseCommonWebviewDialog(WebviewPageType type)
		{
			throw null;
		}

		public ChainDialogPlayer CreateChainDialogPlayer()
		{
			throw null;
		}

		public void ShowBackground(Action<CrossFader.FinishedStatus> onFinished = null)
		{
			throw null;
		}

		public void ShowBackground(ScreenLayerBackground.BackgroundType type, Action<CrossFader.FinishedStatus> onFinished = null)
		{
			throw null;
		}

		public void ShowBackground(string bundleName, string fileName, Action<CrossFader.FinishedStatus> onFinished = null)
		{
			throw null;
		}

		public void ShowBackgroundSync(string bundleName, string fileName, Action<CrossFader.FinishedStatus> onFinished = null, ScreenLayerBackground.BackgroundType backgroundType = ScreenLayerBackground.BackgroundType.Unknown)
		{
			throw null;
		}

		private void ShowBackgroundEnableBlur(ScreenLayerBackground bg)
		{
			throw null;
		}

		public void ActiveArea(bool isActive, bool isRenderAreaSync = true)
		{
			throw null;
		}

		public void EnableRenderArea(bool isRendering)
		{
			// Original flow toggles ScreenLayerArea.AreaController.IsRenderArea while blur capture mode changes.
			// MusicScoreMaker does not currently instantiate/copy the large area stack, so keep this hook
			// dependency-free until ScreenLayerArea is restored for area scenes.
			if (screenMap == null)
			{
				return;
			}

			foreach (var state in screenMap.Values)
			{
				var layer = state?.ScreenLayer;
				if (layer == null || layer.GetType().Name != "ScreenLayerArea")
				{
					continue;
				}

				var areaControllerProperty = layer.GetType().GetProperty("AreaController", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
				var areaController = areaControllerProperty?.GetValue(layer);
				if (areaController == null)
				{
					return;
				}

				var renderAreaProperty = areaController.GetType().GetProperty("IsRenderArea", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
				if (renderAreaProperty != null && renderAreaProperty.CanWrite)
				{
					renderAreaProperty.SetValue(areaController, isRendering);
				}
				return;
			}
		}

		public void PlayArea()
		{
			throw null;
		}

		public void PauseArea()
		{
			throw null;
		}

		public void HideBackground(Action<CrossFader.FinishedStatus> onFinished = null, bool isPlayArea = true)
		{
			throw null;
		}

		public void HideBackgroundDisableBlur()
		{
			throw null;
		}

		public ScreenLayerBackground.BackgroundType GetCurrentBackgroundType()
		{
			throw null;
		}

		private void OnBackKey(List<RaycastResult> results)
		{
			if (EnableMysekaiBackKeyProcess)
			{
				OnExecuteMysekaiBackKeyProcess?.Invoke();
				return;
			}

			if (SceneManager.Instance != null && GetSceneEnableBackKeyProcess(SceneManager.Instance.CurrentScene))
			{
				GetSceneExecuteBackKeyProcess(SceneManager.Instance.CurrentScene)?.Invoke();
				return;
			}

			if (OnExecuteBackKeyProcess != null)
			{
				OnExecuteBackKeyProcess.Invoke();
				return;
			}

			BackUIScreen();
		}

		private void ShowApplicationQuitDialog()
		{
			throw null;
		}

		private void OnApplicationQuitDialogOK()
		{
			throw null;
		}

		public void StartCoroutineInExternal(IEnumerator coroutine)
		{
			if (coroutine != null)
			{
				StartCoroutine(coroutine);
			}
		}

		public Camera CreateEmptyBaseCamera()
		{
			if (cameraStack != null)
			{
				var baseCamera = cameraStack.CreateEmptyBaseCamera(transform);
				SetBaseCamera(baseCamera);
				return baseCamera;
			}

			var cameraObject = new GameObject("BaseCamera");
			cameraObject.transform.SetParent(transform, false);
			var camera = cameraObject.AddComponent<Camera>();
			SetBaseCamera(camera);
			return camera;
		}

		public void SetBaseCamera(Camera camera)
		{
			if (cameraStack != null)
			{
				cameraStack.SetBaseCamera(camera);
				var uiCamera = GetUICamera();
				if (uiCamera != null)
				{
					cameraStack.AddToStack(uiCamera, uiCameraPriority);
				}
			}
			else
			{
				backUICamera = camera;
			}
		}

		public void AddToUICameraStack(Camera camera, int priority)
		{
			cameraStack?.AddToStack(camera, priority);
		}

		public void AddEffectOverlayLayer()
		{
			throw null;
		}

		public void RemoveEffectOverlayLayer()
		{
			throw null;
		}

		public void ShowOutOfPeriodPlayLoginStoryDialog(Action onClickOKCallback = null)
		{
			throw null;
		}

		public void ShowConfirmPlayLoginStoryDialog(MasterSpecialStoryEpisode masterSpecialStoryEpisode, int lastOpenedSpecialStoryCellViewDataId, Action<MasterSpecialStoryEpisode> setMasterSpecialStoryEpisodeCallback, Action<int> showNewsDialogCallback)
		{
			throw null;
		}

		public UniTask SetUIBlurCaptureMode(UIBlurLayerManager.Mode mode)
		{
			if (uiBlurLayerManager != null)
			{
				blurCaptureModeCached = uiBlurLayerManager.CaptureMode;
			}
			return SetUIBlurCaptureModeCore(mode);
		}

		private async UniTask SetUIBlurCaptureModeCore(UIBlurLayerManager.Mode mode)
		{
			SetUIBlurLayerManager();
			if (uiBlurLayerManager == null)
			{
				return;
			}

			await uiBlurLayerManager.SetCaptureMode(mode);
			if (!uiBlurLayerManager.IsEnabledBlurImage)
			{
				return;
			}

			if (uiBlurLayerManager.CaptureMode == UIBlurLayerManager.Mode.Always)
			{
				EnableRenderArea(true);
				return;
			}

			if (uiBlurLayerManager.CaptureMode == UIBlurLayerManager.Mode.OnShot)
			{
				await UniTask.WaitUntil(() => uiBlurLayerManager == null || !uiBlurLayerManager.BlurExecuting, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
				await UniTask.WaitForEndOfFrame(this, this.GetCancellationTokenOnDestroy());
				EnableRenderArea(false);
			}
		}

		public void EnableBlur()
		{
			SetUIBlurLayerManager();
			if (uiBlurLayerManager == null)
			{
				return;
			}

			SetUICameraCullingMask(true);
			uiBlurLayerManager.Enable();
		}

		public void DisableBlur()
		{
			_blurSettingCts?.Cancel();
			_blurSettingCts?.Dispose();
			_blurSettingCts = null;
			foreach (var target in blurTargets.ToArray())
			{
				target?.RemoveCanvasIfExists();
				if (target != null)
				{
					SetLayer(DefaultLayerName, target);
				}
			}
			blurTargets.Clear();
			blurQueue.Clear();
			blurDlalogProcessState = BlurDialogProcessState.Ready;
			uiBlurLayerManager?.Disable();
			SetUICameraCullingMask(false);
		}

		[AsyncStateMachine(typeof(_003CDisableBlurAsync_003Ed__264))]
		private UniTask DisableBlurAsync()
		{
			DisableBlur();
			return UniTask.CompletedTask;
		}

		private void SetUIBlurLayerManager()
		{
			if (uiBlurLayerManager != null)
			{
				return;
			}

			var prefab = Resources.Load<GameObject>(uiBlurLayerManagerPrefabPath)
				?? Resources.Load<GameObject>(uiBlurLayerManagerPrefabPath.ToLowerInvariant());
			GameObject blurObject;
			var parent = transform;
			if (prefab != null)
			{
				blurObject = UnityEngine.Object.Instantiate(prefab, parent, false);
			}
			else
			{
				blurObject = new GameObject("UIBlurLayerManager", typeof(RectTransform), typeof(Canvas), typeof(CanvasGroup), typeof(GraphicRaycaster), typeof(UIBlurLayerManager));
				blurObject.transform.SetParent(parent, false);
				var rect = blurObject.GetComponent<RectTransform>();
				rect.anchorMin = Vector2.zero;
				rect.anchorMax = Vector2.one;
				rect.offsetMin = Vector2.zero;
				rect.offsetMax = Vector2.zero;
			}

			blurObject.name = "UIBlurLayerManager";
			uiBlurLayerManager = blurObject.GetComponent<UIBlurLayerManager>() ?? blurObject.AddComponent<UIBlurLayerManager>();
		}

		private void EnableBlur(BlurLayerTarget target)
		{
			if (target == null || !target.ExistsTarget || !target.EnableBlur)
			{
				return;
			}
			if (!IsEnabledBlurScene() || IsStacked(target))
			{
				return;
			}

			SetUIBlurLayerManager();
			if (uiBlurLayerManager == null)
			{
				return;
			}

			if ((int)target.LayerType >= 2)
			{
				HideCommonBgParticle();
			}
			AddTarget(target);
			SetBlurCore(target);
		}

		private bool IsStacked(BlurLayerTarget target)
		{
			return target != null && blurTargets.Exists(x => x != null && x.Equals(target));
		}

		private void AddTarget(BlurLayerTarget target)
		{
			if (target != null && !IsStacked(target))
			{
				blurTargets.Add(target);
			}
		}

		private void EnqueueBlur((BlurLayerTarget, BlurStackDirection) blurParam)
		{
			blurQueue.Enqueue(blurParam);
		}

		[AsyncStateMachine(typeof(_003CDequeueBlur_003Ed__270))]
		private UniTask DequeueBlur()
		{
			while (blurQueue.Count > 0)
			{
				var item = blurQueue.Dequeue();
				SetBlurCore(item.Item1, item.Item2);
			}
			return UniTask.CompletedTask;
		}

		[AsyncStateMachine(typeof(_003CSetBlur_003Ed__271))]
		private UniTask SetBlur(BlurLayerTarget target, CancellationTokenSource cts, BlurStackDirection dir = BlurStackDirection.Forward)
		{
			if (cts == null || !cts.IsCancellationRequested)
			{
				SetBlurCore(target, dir);
			}
			return UniTask.CompletedTask;
		}

		private void SetBlurCore(BlurLayerTarget target, BlurStackDirection dir = BlurStackDirection.Forward)
		{
			if (target == null || !target.ExistsTarget)
			{
				return;
			}

			var blurTarget = target.EnableBlur ? target : GetLastEnabledBlurTarget();
			if (blurTarget == null)
			{
				foreach (var blurTargetEntry in blurTargets.ToArray())
				{
					blurTargetEntry?.RemoveCanvasIfExists();
				}
				SetLayer(DefaultLayerName, target);
				DisableBlur();
				return;
			}

			var blurLayerCanvas = GetLayerObject(blurTarget.LayerType)?.GetComponent<Canvas>();
			if (blurLayerCanvas == null)
			{
				return;
			}

			uiBlurLayerManager?.SetCanvas(blurLayerCanvas);
			SetLayer(BlurLayerName, blurTarget);
			EnableBlur();

			var dirTarget = dir == BlurStackDirection.Back ? blurTarget : target;
			if (dirTarget == null)
			{
				return;
			}

			var targetCanvas = GetLayerObject(dirTarget.LayerType)?.GetComponent<Canvas>();
			if (dirTarget.EnableBlur)
			{
				foreach (var blurTargetEntry in blurTargets.ToArray())
				{
					if (blurTargetEntry != null && !blurTargetEntry.Equals(dirTarget))
					{
						blurTargetEntry.RemoveCanvasIfExists();
					}
				}
			}
			else
			{
				var sameLayerBlurTarget = GetLastSameLayerBlurTarget(dirTarget);
				if (sameLayerBlurTarget != null)
				{
					targetCanvas = sameLayerBlurTarget.TargetCanvas;
				}
			}

			if (targetCanvas == null)
			{
				return;
			}

			dirTarget.AddCanvasCopyFrom(targetCanvas);
			SetLayer(DefaultLayerName, dirTarget.TargetObject, false);
		}

		private BlurLayerTarget GetLastEnabledBlurTarget()
		{
			for (var i = blurTargets.Count - 1; i >= 0; i--)
			{
				var target = blurTargets[i];
				if (target != null && target.EnableBlur)
				{
					return target;
				}
			}

			return null;
		}

		private BlurLayerTarget GetLastSameLayerBlurTarget(BlurLayerTarget dirTarget)
		{
			if (dirTarget == null)
			{
				return null;
			}

			for (var i = blurTargets.Count - 1; i >= 0; i--)
			{
				var target = blurTargets[i];
				if (target != null && !target.Equals(dirTarget) && target.LayerType == dirTarget.LayerType)
				{
					return target;
				}
			}

			return null;
		}

		private void SetLayer(string layerName, BlurLayerTarget target)
		{
			if (target == null || target.LayerType < 0)
			{
				return;
			}

			var layer = LayerMask.NameToLayer(layerName);
			if (layer < 0)
			{
				return;
			}

			// Original ScreenManager does not recurse the whole target tree here.
			// It changes only Canvas GameObjects in display layers up to the target
			// layer, excluding the target root, so the blur renderer can draw those
			// canvases into the BlurUI capture while the target remains above it.
			for (var i = 0; i <= (int)target.LayerType; i++)
			{
				var layerObject = GetLayerObject((DisplayLayerType)i);
				if (layerObject == null)
				{
					continue;
				}

				var canvases = layerObject.GetComponentsInChildren<Canvas>(true);
				foreach (var canvas in canvases)
				{
					if (canvas == null || canvas.gameObject == target.TargetObject)
					{
						continue;
					}

					canvas.gameObject.layer = layer;
				}
			}
		}

		private void SetLayer(string layerName, GameObject target)
		{
			SetLayer(layerName, target, true);
		}

		private void SetLayer(string layerName, GameObject target, bool needSetChildren)
		{
			if (target == null)
			{
				return;
			}

			var layer = LayerMask.NameToLayer(layerName);
			if (layer < 0)
			{
				return;
			}

			if (needSetChildren)
			{
				SetLayerRecursiveForBlur(target.transform, layer);
				return;
			}

			target.layer = layer;
		}

		private void DisableBlur(BlurLayerTarget target)
		{
			if (target == null)
			{
				return;
			}

			RemoveTarget(target);
			target.RemoveCanvasIfExists();
			SetLayer(DefaultLayerName, target);
			if (blurTargets.Count == 0)
			{
				uiBlurLayerManager?.Disable();
				SetUICameraCullingMask(false);
			}
		}

		private void RemoveTarget(BlurLayerTarget target)
		{
			if (target == null)
			{
				return;
			}

			blurTargets.RemoveAll(x => x == null || x.Equals(target));
		}

		private void SetUICameraCullingMask(bool addBlur)
		{
			var camera = GetUICamera();
			if (camera == null)
			{
				return;
			}

			var uiLayer = LayerMask.NameToLayer(DefaultLayerName);
			var blurLayer = LayerMask.NameToLayer(BlurLayerName);
			if (uiLayer < 0 || blurLayer < 0)
			{
				return;
			}

			var cullingMask = 1 << uiLayer;
			if (addBlur)
			{
				cullingMask |= 1 << blurLayer;
			}

			camera.cullingMask = cullingMask;
		}

		public void EnableBlurByScreenLayer(ScreenLayer screenLayer, DisplayLayerType layerType)
		{
			if (screenLayer != null)
			{
				EnableBlurCore(new BlurLayerTarget(screenLayer, screenLayer.gameObject, layerType));
			}
		}

		private void EnableBlurCore(BlurLayerTarget target)
		{
			if (target == null || !target.ExistsTarget || !target.EnableBlur)
			{
				return;
			}

			if (blurDlalogProcessState == BlurDialogProcessState.Ready)
			{
				blurCaptureModeCached = uiBlurLayerManager != null ? uiBlurLayerManager.CaptureMode : UIBlurLayerManager.Mode.OnShot;
				if (blurTargets.Count == 0)
				{
					SetUIBlurCaptureModeCore(UIBlurLayerManager.Mode.Always).Forget();
				}
				blurDlalogProcessState = BlurDialogProcessState.Opening;
			}

			EnableBlur(target);
		}

		public void RemoveBlurByScreenLayer(ScreenLayer screenLayer, DisplayLayerType layerType)
		{
			if (screenLayer != null)
			{
				RemoveBlurTarget(new BlurLayerTarget(screenLayer, screenLayer.gameObject, layerType));
			}
		}

		private void OpenDialog(DialogBase dialog, DisplayLayerType layerType)
		{
			if (dialog == null)
			{
				return;
			}

			dialog.OnOpenPreprocess += () => OnOpenPreprocessDialog(dialog, layerType);
			dialog.OnOpen += OnOpenedDialog;
			dialog.OnClose += () => OnCloseDialog(dialog, layerType);
			dialog.Open();
		}

		private void OnOpenPreprocessDialog(DialogBase dialog, DisplayLayerType layerType)
		{
			if (dialog != null && !dialogQueue.Contains(dialog))
			{
				dialogQueue.Add(dialog);
			}
			if (dialog != null && IsEnabledBlurScene())
			{
				EnableBlurCore(new BlurLayerTarget(dialog, dialog.gameObject, layerType));
			}
		}

		private void OnOpenedDialog()
		{
			if (blurDlalogProcessState != BlurDialogProcessState.Opening)
			{
				return;
			}

			var restoreMode = blurCaptureModeCached;
			blurDlalogProcessState = BlurDialogProcessState.Ready;
			if (restoreMode != UIBlurLayerManager.Mode.None)
			{
				SetUIBlurCaptureModeCore(restoreMode).Forget();
			}
		}

		private void OnCloseDialog(DialogBase dialog, DisplayLayerType layerType)
		{
			if (dialog != null)
			{
				dialogQueue.Remove(dialog);
			}
			if (dialog != null && IsEnabledBlurScene())
			{
				RemoveBlurTarget(new BlurLayerTarget(dialog, dialog.gameObject, layerType));
			}
		}

		private void RemoveBlurTarget(BlurLayerTarget target)
		{
			if (target == null || !target.EnableBlur)
			{
				return;
			}

			ShowCommonBgParticle();
			DisableBlur(target);
			if (blurTargets.Count > 0)
			{
				var backTarget = blurTargets[blurTargets.Count - 1];
				EnqueueBlur((backTarget, BlurStackDirection.Back));
				DequeueBlur().Forget();
			}
		}

		private void HideCommonBgParticle()
		{
		}

		private void ShowCommonBgParticle()
		{
		}

		private bool IsEnabledBlurScene()
		{
			if (SceneManager.Instance == null)
			{
				return true;
			}

			return EnableBlurSceneList == null || EnableBlurSceneList.Count == 0 || EnableBlurSceneList.Contains(SceneManager.Instance.CurrentScene);
		}

		private static void SetLayerRecursiveForBlur(Transform target, int layer)
		{
			if (target == null)
			{
				return;
			}

			target.gameObject.layer = layer;
			for (var i = 0; i < target.childCount; i++)
			{
				SetLayerRecursiveForBlur(target.GetChild(i), layer);
			}
		}

		public ScreenManager()
		{
			fadeInColor = new Color(0f, 0f, 0f, 0f);
			fadeOutColor = new Color(0f, 0f, 0f, 1f);
			IsAutoClearBackUIScreenOverride = true;
			contentSize = Vector2.zero;
			dialogQueue = new List<DialogBase>();
			dialogQueuePlayers = new List<ChainDialogPlayer>();
			_sceneBackKeyHandlers = new Dictionary<SceneManager.Scene, SceneBackKeyHandler>();
			MaintainableScreenList = new List<MenuScreenType>
			{
				(MenuScreenType)26,
				(MenuScreenType)37,
				(MenuScreenType)123,
				(MenuScreenType)89,
				(MenuScreenType)77
			};
			blurTargets = new List<BlurLayerTarget>();
			blurQueue = new Queue<(BlurLayerTarget, BlurStackDirection)>();
		}

		static ScreenManager()
		{
			BASE_SCREEN_SIZE = new Vector2(1920f, 1080f);
			EffectOverlayLayerName = "EffectOverlay";
			instance = null;
			uiBlurLayerManagerPrefabPath = "UI/Blur/UIBlurLayerManager";
			DefaultLayerName = "UI";
			BlurLayerName = "BlurUI";
			EnableBlurSceneList = new List<SceneManager.Scene>
			{
				SceneManager.Scene.Title,
				SceneManager.Scene.MusicScoreMaker
			};
		}
	}
}
