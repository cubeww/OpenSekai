using System.Collections;
using UnityEngine;

namespace Sekai
{
    public class CountdownView : MonoBehaviour
    {
        private GameObject[] countdownParticleArray;
        private IEnumerator coroutine;

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
                    countdownParticleArray[i].SetActive(true);
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
    }
}
