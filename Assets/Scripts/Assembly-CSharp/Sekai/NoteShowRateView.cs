using UnityEngine;

namespace Sekai
{
	public class NoteShowRateView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer hideAreaView;

		[SerializeField]
		private SpriteRenderer showLine;

		[SerializeField]
		private Vector2 minShowLineSize;

		[SerializeField]
		private Vector2 maxShowLineSize;

		private Material material;

		public void Setup(LiveSettingData liveSetting)
		{
			if (liveSetting == null)
			{
				gameObject.SetActive(false);
				return;
			}
			float settingsRate = 1f - liveSetting._noteShowRate;
			if (Mathf.Approximately(settingsRate, 1f))
			{
				gameObject.SetActive(false);
				return;
			}

			Vector2 position = LiveUtility.CalcNoteShowRatePosition(settingsRate);
			float progress = LiveUtility.CalcNoteShowRate(position);
			if (showLine != null)
			{
				float t = Mathf.Clamp01(settingsRate);
				showLine.size = Vector2.Lerp(maxShowLineSize, minShowLineSize, t);
				showLine.transform.localPosition = new Vector3(0f, position.y + showLine.size.y * 0.5f, 0f);
			}

			if (hideAreaView != null)
			{
				if (material == null)
				{
					Shader shader = Shader.Find("Sekai/Live/Note/HideArea");
					if (shader != null)
					{
						material = new Material(shader);
					}
				}
				if (material != null)
				{
					hideAreaView.material = material;
					material.SetFloat("_NoteShowRate", progress);
				}
			}
			gameObject.SetActive(true);
		}

		private void OnDestroy()
		{
			if (material != null)
			{
				Destroy(material);
				material = null;
				if (hideAreaView != null)
				{
					hideAreaView.material = null;
				}
			}
		}

		public NoteShowRateView()
		{
		}
	}
}
