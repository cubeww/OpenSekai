using System.Collections;
using UnityEngine;

namespace Sekai
{
    public class CountdownView : MonoBehaviour
    {
        private GameObject[] countdownParticleArray;
        private IEnumerator coroutine;
        private static readonly int DefaultStateHash = Animator.StringToHash("clip_live_countdown_1");

        public void Setup()
        {
            countdownParticleArray = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                countdownParticleArray[i] = child;
                child.SetActive(false);
            }
        }

        public void StartCountdown()
        {
            StopCountdown();
            coroutine = Countdown();
            StartCoroutine(coroutine);
        }

        public void StopCountdown()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            ResetCountdown();
        }

        private IEnumerator Countdown()
        {
            for (int i = 2; i >= 0; i--)
            {
                if (countdownParticleArray != null && i < countdownParticleArray.Length && countdownParticleArray[i] != null)
                {
                    PlayCountdownObject(countdownParticleArray[i]);
                }
                yield return new WaitForSeconds(1f);
            }

            ResetCountdown();
            coroutine = null;
        }

        private void ResetCountdown()
        {
            if (countdownParticleArray == null)
            {
                return;
            }

            for (int i = 0; i < countdownParticleArray.Length; i++)
            {
                if (countdownParticleArray[i] != null)
                {
                    countdownParticleArray[i].SetActive(false);
                }
            }
        }

        private static void PlayCountdownObject(GameObject countdownObject)
        {
            countdownObject.SetActive(true);

            Animator[] animators = countdownObject.GetComponentsInChildren<Animator>(true);
            for (int i = 0; i < animators.Length; i++)
            {
                Animator animator = animators[i];
                if (animator == null || animator.runtimeAnimatorController == null)
                {
                    continue;
                }

                animator.enabled = true;
                animator.Rebind();
                animator.Update(0f);
                animator.Play(DefaultStateHash, 0, 0f);
            }

            ParticleSystem[] particles = countdownObject.GetComponentsInChildren<ParticleSystem>(true);
            for (int i = 0; i < particles.Length; i++)
            {
                ParticleSystem particle = particles[i];
                if (particle == null)
                {
                    continue;
                }

                particle.Clear(true);
                particle.Play(true);
            }
        }
    }
}
