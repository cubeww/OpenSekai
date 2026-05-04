using UnityEngine;
using UnityEngine.Events;

namespace Sekai
{
    public class TweenAlpha : MonoBehaviour
    {
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
    }
}
