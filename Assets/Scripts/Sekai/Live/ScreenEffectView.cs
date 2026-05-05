using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.Live
{
    public class ScreenEffectView : MonoBehaviour
    {
        [SerializeField] private GameObject damageEffectPrefab;

		private ParticleSystemController damageEffectController;

        public void Setup(BaseLiveController liveController = null)
        {
            if (damageEffectController == null && damageEffectPrefab != null)
            {
                GameObject instance = Instantiate(damageEffectPrefab, transform, false);
                damageEffectController = instance.GetComponent<ParticleSystemController>();
                if (damageEffectController == null)
                {
                    damageEffectController = instance.AddComponent<ParticleSystemController>();
                }
                damageEffectController.RegisterToLiveController(liveController);
                damageEffectController.Stop();
            }

            float screenAspect = Screen.height == 0 ? 1.7778f : (float)Screen.width / Screen.height;
            transform.localScale = Vector3.one * Mathf.Max(1f, 1.7778f / screenAspect);
        }

        public void Excute(INote note)
        {
            if (note == null || note.Result == NoteResult.Pass || note.Category == NoteCategory.Hidden)
            {
                return;
            }

            if (note.Result == NoteResult.Miss || note.Result == NoteResult.Bad)
            {
                damageEffectController?.Play();
            }
        }
    }
}
