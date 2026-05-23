using UnityEngine;

namespace Sekai.UI
{
	public interface ICustomText
	{
		string WordingKey { get; set; }

		bool UseWordingKey { get; set; }

		string Text { get; }

		RectTransform RectTransform { get; }

		float PreferredHeight { get; }

		float FontSize { get; set; }

		void UpdateWordingText();

		void SetWordingKey(string key);

		void SetWordingText(string key);

		void SetWordingKey(string key, params object[] args);

		void SetWordingText(string key, params object[] args);

		void SetDefaultFontDB();

		void SetDefaultFontEB();

		void SetText(string value, bool breakSpace = false);
	}
}
