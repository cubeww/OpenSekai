using Sekai.Live;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
    public class JudgmentView : MonoBehaviour
    {
        [SerializeField] private Sprite miss;
        [SerializeField] private Sprite bad;
        [SerializeField] private Sprite good;
        [SerializeField] private Sprite great;
        [SerializeField] private Sprite perfect;
        [SerializeField] private Sprite justPerfect;
        [SerializeField] private Sprite auto;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private readonly Vector3 farPos = new Vector3(100000f, 100000f, 0f);
        private Dictionary<NoteResult, Sprite> sprites;
        private float time = 1f;

        private void Start()
        {
            SetupSprites();
            Hide();
        }

        public void Excute((NoteResult, NoteResultDescription) judgeInfo)
        {
            if (spriteRenderer == null)
            {
                return;
            }

            if (sprites == null)
            {
                SetupSprites();
            }

            if (!sprites.TryGetValue(judgeInfo.Item1, out Sprite sprite) || sprite == null)
            {
                return;
            }

            spriteRenderer.sprite = sprite;
            time = 0f;
            spriteRenderer.transform.localPosition = farPos;
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
                if ((Time.frameCount & 1) == 0 || time == 0f)
                {
                    if (time == 0f)
                    {
                        target.localPosition = Vector3.zero;
                    }

                    float t = Mathf.Clamp01(time * 20f);
                    float scale = 1f - Mathf.Pow(1f - t, 3f);
                    target.localScale = Vector3.one * scale;
                }

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

            spriteRenderer.transform.localPosition = farPos;
        }

        private void SetupSprites()
        {
            sprites = new Dictionary<NoteResult, Sprite>
            {
                { NoteResult.Miss, miss },
                { NoteResult.Bad, bad },
                { NoteResult.Good, good },
                { NoteResult.Great, great },
                { NoteResult.Perfect, perfect },
                { NoteResult.JustPerfect, perfect },
                { NoteResult.Auto, auto }
            };
        }
    }
}
