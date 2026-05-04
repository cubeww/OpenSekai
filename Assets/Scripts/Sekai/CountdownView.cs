using UnityEngine;

namespace Sekai
{
    public class CountdownView : MonoBehaviour
    {
        public void Setup()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public void StartCountdown() { }
        public void StopCountdown() { }
    }
}
