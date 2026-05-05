using Sekai.Live;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
    public class JudgmentDescriptionView : MonoBehaviour
    {
        [SerializeField] private Sprite fast;
        [SerializeField] private Sprite late;
        [SerializeField] private Sprite flickMiss;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Dictionary<NoteResultDescription, Sprite> sprites;
        private float time = 1f;
        private bool isFastLateFlick = true;

        private void Start()
        {
            SetupSprites();
            Hide();
        }

        public void Excute((NoteResult, NoteResultDescription) judgeInfo)
        {
            if (spriteRenderer == null || !isFastLateFlick || judgeInfo.Item1 < NoteResult.Bad || judgeInfo.Item1 > NoteResult.Great)
            {
                return;
            }

            if (sprites == null)
            {
                SetupSprites();
            }

            Sprite sprite = null;
            sprites.TryGetValue(judgeInfo.Item2, out sprite);
            spriteRenderer.sprite = sprite;
            spriteRenderer.transform.localScale = Vector3.zero;

            if (sprite != null)
            {
                time = 0f;
            }
        }

        private void Update()
        {
            if (spriteRenderer == null)
            {
                return;
            }

            Transform target = spriteRenderer.transform;
            if (time <= 0.3f)
            {
                float t = Mathf.Clamp01(time * 20f);
                float scale = 1f - Mathf.Pow(1f - t, 3f);
                target.localScale = Vector3.one * scale;
                time += Time.deltaTime;
            }
            else
            {
                Hide();
            }
        }

        private void Hide()
        {
            if (spriteRenderer == null)
            {
                return;
            }

            spriteRenderer.transform.localScale = Vector3.zero;
        }

        private void SetupSprites()
        {
            sprites = new Dictionary<NoteResultDescription, Sprite>
            {
                { NoteResultDescription.Fast, fast },
                { NoteResultDescription.Late, late },
                { NoteResultDescription.FlickMiss, flickMiss }
            };
        }
    }
}
