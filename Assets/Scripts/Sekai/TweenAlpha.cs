using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sekai
{
    public class TweenAlpha : MonoBehaviour
    {
        private enum BehaviourType
        {
            Master = 0,
            Reference = 1
        }

        private enum PlayTiming
        {
            PlayOnAwake = 0,
            PlayOnStart = 1,
            PlayOnEnable = 2,
            Manual = 3
        }

        private enum PlayDirection
        {
            Forward = 0,
            Back = 1
        }

        private enum PlayLoopType
        {
            Once = 0,
            Loop = 1,
            PingPong = 2
        }

        private enum PlayComponentType
        {
            Normal = 0,
            UnityUI = 1,
            CanvasGroup = 2
        }

        [SerializeField] private int behaviour;
        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private float duration = 0.7f;
        [SerializeField] private float delay;
        [SerializeField] private int loopType;
        [SerializeField] private int loopCount;
        [SerializeField] private int componentType;
        [SerializeField] private int direction;
        [SerializeField] private int startTiming;
        [SerializeField] private Component reference;
        [SerializeField] private bool dontKillIfDisable;
        [SerializeField] private UnityEvent onFinished;
        [SerializeField] private bool isReflesh;
        [SerializeField] private float from;
        [SerializeField] private float to = 1f;

        private SpriteRenderer spriteRenderer;
        private Graphic graphic;
        private CanvasGroup canvasGroup;
        private Coroutine playCoroutine;
        private bool targetInitialized;

        public float From
        {
            get { return from; }
            set { from = value; }
        }

        public float To
        {
            get { return to; }
            set { to = value; }
        }

        private void Awake()
        {
            SetupTarget();
            if ((PlayTiming)startTiming == PlayTiming.PlayOnAwake)
            {
                Play((PlayDirection)direction);
            }
        }

        private void Start()
        {
            if ((PlayTiming)startTiming == PlayTiming.PlayOnStart)
            {
                Play((PlayDirection)direction);
            }
        }

        private void OnEnable()
        {
            if ((PlayTiming)startTiming == PlayTiming.PlayOnEnable)
            {
                Play((PlayDirection)direction);
            }
        }

        private void OnDisable()
        {
            if (!dontKillIfDisable)
            {
                Stop();
            }
        }

        private void OnDestroy()
        {
            Stop();
        }

        public void SetupTarget()
        {
            if (targetInitialized && !isReflesh)
            {
                return;
            }

            spriteRenderer = null;
            graphic = null;
            canvasGroup = null;

            Component target = reference != null && (BehaviourType)behaviour == BehaviourType.Reference ? reference : null;
            GameObject targetObject = target != null ? target.gameObject : gameObject;

            switch ((PlayComponentType)componentType)
            {
                case PlayComponentType.UnityUI:
                    graphic = targetObject.GetComponent<Graphic>();
                    if (graphic == null)
                    {
                        canvasGroup = targetObject.GetComponent<CanvasGroup>();
                    }
                    break;

                case PlayComponentType.CanvasGroup:
                    canvasGroup = targetObject.GetComponent<CanvasGroup>();
                    if (canvasGroup == null)
                    {
                        graphic = targetObject.GetComponent<Graphic>();
                    }
                    break;

                default:
                    spriteRenderer = targetObject.GetComponent<SpriteRenderer>();
                    if (spriteRenderer == null)
                    {
                        graphic = targetObject.GetComponent<Graphic>();
                    }
                    if (graphic == null)
                    {
                        canvasGroup = targetObject.GetComponent<CanvasGroup>();
                    }
                    break;
            }

            targetInitialized = true;
        }

        public void Play()
        {
            Play((PlayDirection)direction);
        }

        public void PlayForward()
        {
            Play(PlayDirection.Forward);
        }

        public void PlayBack()
        {
            Play(PlayDirection.Back);
        }

        private void Play(PlayDirection playDirection)
        {
            SetupTarget();
            Stop();
            playCoroutine = StartCoroutine(PlayCore(playDirection));
        }

        public void Stop()
        {
            if (playCoroutine != null)
            {
                StopCoroutine(playCoroutine);
                playCoroutine = null;
            }
        }

        public void Restart()
        {
            Play((PlayDirection)direction);
        }

        public void ForceFinish()
        {
            Stop();
            SetAlpha((PlayDirection)direction == PlayDirection.Forward ? to : from);
            onFinished?.Invoke();
        }

        private System.Collections.IEnumerator PlayCore(PlayDirection playDirection)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            int completedLoops = 0;
            bool forward = playDirection == PlayDirection.Forward;
            PlayLoopType playLoopType = (PlayLoopType)loopType;

            while (true)
            {
                yield return TweenOnce(forward);
                completedLoops++;

                if (playLoopType == PlayLoopType.Once || (loopCount > 0 && completedLoops >= loopCount))
                {
                    break;
                }

                if (playLoopType == PlayLoopType.PingPong)
                {
                    forward = !forward;
                }
            }

            playCoroutine = null;
            onFinished?.Invoke();
        }

        private System.Collections.IEnumerator TweenOnce(bool forward)
        {
            if (duration <= 0f)
            {
                SetAlpha(forward ? to : from);
                yield break;
            }

            float elapsed = 0f;
            while (elapsed < duration)
            {
                float normalizedTime = Mathf.Clamp01(elapsed / duration);
                if (!forward)
                {
                    normalizedTime = 1f - normalizedTime;
                }

                SetAlpha(Mathf.LerpUnclamped(from, to, curve.Evaluate(normalizedTime)));
                elapsed += Time.deltaTime;
                yield return null;
            }

            SetAlpha(forward ? to : from);
        }

        private void SetAlpha(float alpha)
        {
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }

            if (graphic != null)
            {
                Color color = graphic.color;
                color.a = alpha;
                graphic.color = color;
            }

            if (canvasGroup != null)
            {
                canvasGroup.alpha = alpha;
            }
        }
    }
}
