using System.Collections.Generic;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class JudgmentDescriptionView : MonoBehaviour
	{
		[SerializeField]
		private Sprite fast;

		[SerializeField]
		private Sprite late;

		[SerializeField]
		private Sprite flickMiss;

		[SerializeField]
		private SpriteRenderer spriteRenderer;

		private Dictionary<NoteResultDescription, Sprite> sprites;

		private float time;

		private bool isFastLateFlick;

		private void Start()
		{
			sprites = new Dictionary<NoteResultDescription, Sprite>
			{
				{ NoteResultDescription.Fast, fast },
				{ NoteResultDescription.Late, late },
				{ NoteResultDescription.FlickMiss, flickMiss }
			};
			LiveSettingData liveSetting = LiveSettingData.LoadFromStorage();
			isFastLateFlick = liveSetting == null || liveSetting.IsFastLateFlick;
		}

		public void Excute((NoteResult result, NoteResultDescription description) judgeInfo)
		{
			if (!isFastLateFlick || judgeInfo.result < NoteResult.Bad || judgeInfo.result > NoteResult.Great)
			{
				return;
			}
			if (sprites == null)
			{
				Start();
			}
			if (spriteRenderer == null)
			{
				return;
			}
			sprites.TryGetValue(judgeInfo.description, out Sprite sprite);
			spriteRenderer.sprite = sprite;
			time = 0f;
			spriteRenderer.transform.localScale = Vector3.zero;
		}

		private void Update()
		{
			if (spriteRenderer == null)
			{
				return;
			}
			if (time > 0.3f)
			{
				spriteRenderer.transform.localScale = Vector3.zero;
				return;
			}
			float t = Mathf.Clamp01(time * 20f);
			float inv = 1f - t;
			float scale = 1f - inv * inv * inv;
			spriteRenderer.transform.localScale = Vector3.one * scale;
			time += Time.deltaTime;
		}

		public JudgmentDescriptionView()
		{
		}
	}
}
