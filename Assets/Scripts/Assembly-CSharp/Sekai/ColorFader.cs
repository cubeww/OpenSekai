using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai
{
	public class ColorFader : MonoBehaviour
	{
		public enum State
		{
			None = 0,
			Fading = 1
		}

		public enum Style
		{
			Once = 0,
			Loop = 1,
			PingPong = 2
		}

		public enum BlendType
		{
			Direct = 0,
			Mul = 1
		}

		public interface ITargetColorObj
		{
			Color TargetColor { get; set; }

			void Init(GameObject targetObj, bool containChildren);
		}

		public class GraphicColor : ITargetColorObj
		{
			private List<Graphic> sprites;

			private bool includeChildren;

			public Color TargetColor
			{
				get
				{
					if (sprites == null || sprites.Count == 0)
					{
						return Color.white;
					}

					return sprites[0].color;
				}
				set
				{
					if (sprites == null)
					{
						return;
					}

					foreach (var graphic in sprites)
					{
						if (graphic != null)
						{
							graphic.color = value;
						}
					}
				}
			}

			public void Init(GameObject targetObj, bool containChildren)
			{
				sprites = new List<Graphic>();
				includeChildren = containChildren;
				if (targetObj != null)
				{
					CollectTargets(targetObj.transform);
				}
			}

			private void CollectTargets(Transform targetTrans)
			{
				if (targetTrans == null)
				{
					return;
				}

				var graphic = targetTrans.GetComponent<Graphic>();
				if (graphic != null)
				{
					sprites.Add(graphic);
				}

				if (!includeChildren)
				{
					return;
				}

				for (var i = 0; i < targetTrans.childCount; i++)
				{
					CollectTargets(targetTrans.GetChild(i));
				}
			}

			public GraphicColor()
			{
				sprites = new List<Graphic>();
			}
		}

		public class SpriteRendererColor : ITargetColorObj
		{
			private List<SpriteRenderer> sprites;

			private bool includeChildren;

			public Color TargetColor
			{
				get
				{
					if (sprites == null || sprites.Count == 0)
					{
						return Color.white;
					}

					return sprites[0].color;
				}
				set
				{
					if (sprites == null)
					{
						return;
					}

					foreach (var sprite in sprites)
					{
						if (sprite != null)
						{
							sprite.color = value;
						}
					}
				}
			}

			public void Init(GameObject targetObj, bool containChildren)
			{
				sprites = new List<SpriteRenderer>();
				includeChildren = containChildren;
				if (targetObj != null)
				{
					CollectTargets(targetObj.transform);
				}
			}

			private void CollectTargets(Transform targetTrans)
			{
				if (targetTrans == null)
				{
					return;
				}

				var sprite = targetTrans.GetComponent<SpriteRenderer>();
				if (sprite != null)
				{
					sprites.Add(sprite);
				}

				if (!includeChildren)
				{
					return;
				}

				for (var i = 0; i < targetTrans.childCount; i++)
				{
					CollectTargets(targetTrans.GetChild(i));
				}
			}

			public SpriteRendererColor()
			{
				sprites = new List<SpriteRenderer>();
			}
		}

		public class AreaSpriteRendererMultiplicationColor : ITargetColorObj
		{
			private List<AreaSpriteRenderer> sprites;

			private bool includeChildren;

			public Color TargetColor
			{
				get
				{
					if (sprites == null || sprites.Count == 0)
					{
						return Color.white;
					}

					return sprites[0].MultiplicationColor;
				}
				set
				{
					if (sprites == null)
					{
						return;
					}

					foreach (var sprite in sprites)
					{
						if (sprite != null)
						{
							sprite.MultiplicationColor = value;
						}
					}
				}
			}

			public void Init(GameObject targetObj, bool containChildren)
			{
				sprites = new List<AreaSpriteRenderer>();
				includeChildren = containChildren;
				if (targetObj != null)
				{
					CollectTargets(targetObj.transform);
				}
			}

			private void CollectTargets(Transform targetTrans)
			{
				if (targetTrans == null)
				{
					return;
				}

				var sprite = targetTrans.GetComponent<AreaSpriteRenderer>();
				if (sprite != null)
				{
					sprites.Add(sprite);
				}

				if (!includeChildren)
				{
					return;
				}

				for (var i = 0; i < targetTrans.childCount; i++)
				{
					CollectTargets(targetTrans.GetChild(i));
				}
			}

			public AreaSpriteRendererMultiplicationColor()
			{
				sprites = new List<AreaSpriteRenderer>();
			}
		}

		public class AreaSpriteRendererColor : ITargetColorObj
		{
			private List<AreaSpriteRenderer> sprites;

			private bool includeChildren;

			public Color TargetColor
			{
				get
				{
					if (sprites == null || sprites.Count == 0)
					{
						return Color.white;
					}

					return sprites[0].BaseColor;
				}
				set
				{
					if (sprites == null)
					{
						return;
					}

					foreach (var sprite in sprites)
					{
						if (sprite != null)
						{
							sprite.BaseColor = value;
						}
					}
				}
			}

			public void Init(GameObject targetObj, bool containChildren)
			{
				sprites = new List<AreaSpriteRenderer>();
				includeChildren = containChildren;
				if (targetObj != null)
				{
					CollectTargets(targetObj.transform);
				}
			}

			private void CollectTargets(Transform targetTrans)
			{
				if (targetTrans == null)
				{
					return;
				}

				var sprite = targetTrans.GetComponent<AreaSpriteRenderer>();
				if (sprite != null)
				{
					sprites.Add(sprite);
				}

				if (!includeChildren)
				{
					return;
				}

				for (var i = 0; i < targetTrans.childCount; i++)
				{
					CollectTargets(targetTrans.GetChild(i));
				}
			}

			public AreaSpriteRendererColor()
			{
				sprites = new List<AreaSpriteRenderer>();
			}
		}

		public class SpineModelColor : ITargetColorObj
		{
			private List<SpineModel> sprites;

			private bool includeChildren;

			public Color TargetColor
			{
				get
				{
					if (sprites == null || sprites.Count == 0)
					{
						return Color.white;
					}

					return sprites[0].BaseColor;
				}
				set
				{
					if (sprites == null)
					{
						return;
					}

					foreach (var sprite in sprites)
					{
						if (sprite != null)
						{
							sprite.BaseColor = value;
						}
					}
				}
			}

			public void Init(GameObject targetObj, bool containChildren)
			{
				sprites = new List<SpineModel>();
				includeChildren = containChildren;
				if (targetObj != null)
				{
					CollectTargets(targetObj.transform);
				}
			}

			private void CollectTargets(Transform targetTrans)
			{
				if (targetTrans == null)
				{
					return;
				}

				var sprite = targetTrans.GetComponent<SpineModel>();
				if (sprite != null)
				{
					sprites.Add(sprite);
				}

				if (!includeChildren)
				{
					return;
				}

				for (var i = 0; i < targetTrans.childCount; i++)
				{
					CollectTargets(targetTrans.GetChild(i));
				}
			}

			public SpineModelColor()
			{
				sprites = new List<SpineModel>();
			}
		}

		public class SpineModelMultiplicationColor : ITargetColorObj
		{
			private List<SpineModel> sprites;

			private bool includeChildren;

			public Color TargetColor
			{
				get
				{
					if (sprites == null || sprites.Count == 0)
					{
						return Color.white;
					}

					return sprites[0].MultiplicationColor;
				}
				set
				{
					if (sprites == null)
					{
						return;
					}

					foreach (var sprite in sprites)
					{
						if (sprite != null)
						{
							sprite.MultiplicationColor = value;
						}
					}
				}
			}

			public void Init(GameObject targetObj, bool containChildren)
			{
				sprites = new List<SpineModel>();
				includeChildren = containChildren;
				if (targetObj != null)
				{
					CollectTargets(targetObj.transform);
				}
			}

			private void CollectTargets(Transform targetTrans)
			{
				if (targetTrans == null)
				{
					return;
				}

				var sprite = targetTrans.GetComponent<SpineModel>();
				if (sprite != null)
				{
					sprites.Add(sprite);
				}

				if (!includeChildren)
				{
					return;
				}

				for (var i = 0; i < targetTrans.childCount; i++)
				{
					CollectTargets(targetTrans.GetChild(i));
				}
			}

			public SpineModelMultiplicationColor()
			{
				sprites = new List<SpineModel>();
			}
		}

		[CompilerGenerated]
		private sealed class _003CPlayChainFade_003Ed__64 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ColorFader _003C_003E4__this;

			public Action onFinish;

			public Color[] targetColors;

			public float delay;

			public float oneFadeDuration;

			private Color _003CbaseColor_003E5__2;

			private Color _003CtargetColor_003E5__3;

			private float _003Ctime_003E5__4;

			private int _003CtableLength_003E5__5;

			private int _003CindexVec_003E5__6;

			private bool _003CisPlayable_003E5__7;

			private int _003Ci_003E5__8;

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
			public _003CPlayChainFade_003Ed__64(int _003C_003E1__state)
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
		private sealed class _003CPlaySimpleAlphaFade_003Ed__63 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ColorFader _003C_003E4__this;

			public Action onFinish;

			public float delay;

			public float targetAlpha;

			public float duration;

			private Color _003CbaseColor_003E5__2;

			private float _003Ctime_003E5__3;

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
			public _003CPlaySimpleAlphaFade_003Ed__63(int _003C_003E1__state)
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
		private sealed class _003CPlaySimpleFade_003Ed__62 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ColorFader _003C_003E4__this;

			public Action onFinish;

			public float delay;

			public Color targetColor;

			public float duration;

			private Color _003CbaseColor_003E5__2;

			private float _003Ctime_003E5__3;

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
			public _003CPlaySimpleFade_003Ed__62(int _003C_003E1__state)
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

		[SerializeField]
		private GameObject targetGameObject;

		[SerializeField]
		private BlendType blendType;

		[SerializeField]
		private Color defaultColor;

		[SerializeField]
		private Color[] defaultColorTable;

		[SerializeField]
		private Style defaultStyle;

		[SerializeField]
		private float defaultDelay;

		[SerializeField]
		private float defaultDuration;

		[SerializeField]
		private bool playOnAwake;

		[SerializeField]
		private bool playOnEnable;

		[SerializeField]
		private bool includeChildren;

		private ITargetColorObj targetSprite;

		private Color fadeTargetColor;

		private IEnumerator fadeCoroutine;

		private bool isAlphaZeroDeactive;

		private int currentColorTableIndex;

		private Style style;

		private State state;

		private MonoBehaviour coroutineExecutor;

		private Action onFinishedNotExecuted;

		public bool IsAlphaZeroDeactive
		{
			[CompilerGenerated]
			get
			{
				return isAlphaZeroDeactive;
			}
			[CompilerGenerated]
			set
			{
				isAlphaZeroDeactive = value;
			}
		}

		public Color[] DefaultColorTable
		{
			get
			{
				return defaultColorTable;
			}
			set
			{
				defaultColorTable = value;
			}
		}

		public float DefaultDuration
		{
			get
			{
				return defaultDuration;
			}
			set
			{
				defaultDuration = value;
			}
		}

		public Color FadeTargetColor
		{
			get
			{
				return fadeTargetColor;
			}
		}

		public bool IsPlayFading
		{
			get
			{
				return state == State.Fading;
			}
		}

		public State CurrentState
		{
			get
			{
				return state;
			}
		}

		public Color CurrentColor
		{
			get
			{
				return targetSprite != null ? targetSprite.TargetColor : defaultColor;
			}
			set
			{
				if (targetSprite != null)
				{
					targetSprite.TargetColor = value;
				}
			}
		}

		public Color DefaultColor
		{
			get
			{
				return defaultColor;
			}
			set
			{
				defaultColor = value;
			}
		}

		public bool IncludeChildren
		{
			get
			{
				return includeChildren;
			}
			set
			{
				includeChildren = value;
			}
		}

		public bool IsFoundFadeTarget
		{
			get
			{
				return targetSprite != null;
			}
		}

		private void Awake()
		{
			if (playOnAwake)
			{
				Initialize();
				PlayColorTable(null);
			}
		}

		public bool Initialize(bool isForce = false)
		{
			if (!isForce && targetSprite != null)
			{
				return true;
			}

			return FindFadeTarget(isForce);
		}

		public void Rebuild(bool isForce = false)
		{
			Initialize(isForce);
		}

		private void OnEnable()
		{
			if (playOnEnable)
			{
				Initialize();
				PlayColorTable(null);
			}
		}

		public bool FindFadeTarget(bool isForce = false)
		{
			if (!isForce && targetSprite != null)
			{
				return true;
			}

			targetSprite = FindSpriteType();
			targetSprite?.Init(targetGameObject != null ? targetGameObject : gameObject, includeChildren);
			return targetSprite != null;
		}

		private ITargetColorObj FindSpriteType()
		{
			var target = targetGameObject != null ? targetGameObject : gameObject;
			if (target == null)
			{
				return null;
			}

			if (target.GetComponent<Graphic>() != null || target.GetComponentInChildren<Graphic>(true) != null)
			{
				return new GraphicColor();
			}

			if (target.GetComponent<SpriteRenderer>() != null || target.GetComponentInChildren<SpriteRenderer>(true) != null)
			{
				return new SpriteRendererColor();
			}

			return null;
		}

		public void Set(Color targetColor)
		{
			Initialize();
			Stop();
			fadeTargetColor = targetColor;
			CurrentColor = targetColor;
			if (isAlphaZeroDeactive || IsAlphaZeroDeactive)
			{
				if (targetColor.a <= 0f)
				{
					DeactiveSprite();
				}
				else
				{
					ActiveSprite();
				}
			}
		}

		public void Play(Color targetColor, float delay, float duration, Action onFinish = null)
		{
			Initialize();
			Stop();
			fadeTargetColor = targetColor;
			fadeCoroutine = PlaySimpleFade(targetColor, delay, duration, onFinish);
			(coroutineExecutor != null ? coroutineExecutor : this).StartCoroutine(fadeCoroutine);
		}

		public void PlayAlpha(float targetAlpha, float delay, float duration, Action onFinish = null)
		{
			Initialize();
			var color = CurrentColor;
			color.a = targetAlpha;
			Play(color, delay, duration, onFinish);
		}

		public void PlayColorTable(Color[] targetColors, float delay, float oneFadeDuration, Style style, Action onFinish = null)
		{
			defaultColorTable = targetColors;
			defaultDelay = delay;
			defaultDuration = oneFadeDuration;
			this.style = style;
			Stop();
			fadeCoroutine = PlayChainFade(targetColors, delay, oneFadeDuration, onFinish);
			(coroutineExecutor != null ? coroutineExecutor : this).StartCoroutine(fadeCoroutine);
		}

		public void PlayColorTable(Action onFinish = null)
		{
			PlayColorTable(defaultColorTable, defaultDelay, defaultDuration, defaultStyle, onFinish);
		}

		public void Stop()
		{
			if (fadeCoroutine != null)
			{
				(coroutineExecutor != null ? coroutineExecutor : this).StopCoroutine(fadeCoroutine);
				fadeCoroutine = null;
			}

			state = State.None;
		}

		private IEnumerator PlaySimpleFade(Color targetColor, float delay, float duration, Action onFinish = null)
		{
			state = State.Fading;
			if (delay > 0f)
			{
				yield return new WaitForSeconds(delay);
			}

			var startColor = CurrentColor;
			var elapsed = 0f;
			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;
				var rate = duration <= 0f ? 1f : Mathf.Clamp01(elapsed / duration);
				CurrentColor = Color.Lerp(startColor, targetColor, rate);
				yield return null;
			}

			fadeTargetColor = targetColor;
			CurrentColor = targetColor;
			state = State.None;
			onFinish?.Invoke();
		}

		private IEnumerator PlaySimpleAlphaFade(float targetAlpha, float delay, float duration, Action onFinish = null)
		{
			var color = CurrentColor;
			color.a = targetAlpha;
			yield return PlaySimpleFade(color, delay, duration, onFinish);
		}

		private IEnumerator PlayChainFade(Color[] targetColors, float delay, float oneFadeDuration, Action onFinish = null)
		{
			if (targetColors == null || targetColors.Length == 0)
			{
				onFinish?.Invoke();
				yield break;
			}

			if (delay > 0f)
			{
				yield return new WaitForSeconds(delay);
			}

			state = State.Fading;
			currentColorTableIndex = 0;
			do
			{
				yield return PlaySimpleFade(targetColors[currentColorTableIndex], 0f, oneFadeDuration, null);
				currentColorTableIndex++;
				if (currentColorTableIndex >= targetColors.Length)
				{
					if (style == Style.Loop)
					{
						currentColorTableIndex = 0;
					}
					else
					{
						break;
					}
				}
			}
			while (style == Style.Loop);

			state = State.None;
			onFinish?.Invoke();
		}

		private void DeactiveSprite()
		{
			var target = targetGameObject != null ? targetGameObject : gameObject;
			if (target != null)
			{
				target.SetActive(false);
			}
		}

		private void ActiveSprite()
		{
			var target = targetGameObject != null ? targetGameObject : gameObject;
			if (target != null)
			{
				target.SetActive(true);
			}
		}

		public ColorFader()
		{
			defaultColor = Color.white;
			defaultDuration = 0.2f;
			defaultStyle = Style.Once;
			state = State.None;
		}
	}
}
