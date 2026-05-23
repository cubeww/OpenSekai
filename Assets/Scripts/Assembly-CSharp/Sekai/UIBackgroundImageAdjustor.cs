using UnityEngine;

namespace Sekai
{
	public class UIBackgroundImageAdjustor : MonoBehaviour
	{
		private readonly Vector2 BASE_SIZE;

		private RectTransform rt;

		private void Awake()
		{
			rt = GetComponent<RectTransform>();
		}

		private void OnEnable()
		{
			if (rt == null)
			{
				rt = GetComponent<RectTransform>();
			}

			if (rt == null)
			{
				return;
			}

			Vector2 contentSize = ScreenManager.ExistsInstance ? ScreenManager.Instance.ContentSize : new Vector2(Screen.width, Screen.height);
			if (contentSize.x <= 0f || contentSize.y <= 0f)
			{
				contentSize = new Vector2(Screen.width, Screen.height);
			}

			float baseAspect = BASE_SIZE.x / BASE_SIZE.y;
			float targetAspect = contentSize.x / contentSize.y;
			Vector2 size = BASE_SIZE;
			if (targetAspect > baseAspect)
			{
				size.x = contentSize.x;
				size.y = contentSize.x / baseAspect;
			}
			else
			{
				size.y = contentSize.y;
				size.x = contentSize.y * baseAspect;
			}

			rt.sizeDelta = size;
			rt.localScale = Vector3.one;
		}

		public UIBackgroundImageAdjustor()
		{
			BASE_SIZE = new Vector2(2338f, 1440f);
		}
	}
}
