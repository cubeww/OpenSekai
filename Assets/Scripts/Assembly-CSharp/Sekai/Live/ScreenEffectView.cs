using UnityEngine;

namespace Sekai.Live
{
	public class ScreenEffectView : MonoBehaviour
	{
		private ParticleSystemController damageEffectController;

		public void Setup()
		{
			damageEffectController = CreateEffect(LivePrefabDefine.FX_SCREEN_DAMAGE);
			RefreshScreenSize();
		}

		public void RefreshScreenSize()
		{
			float aspect = Screen.height > 0 ? (float)Screen.width / Screen.height : 1.7778f;
			transform.localScale = Vector3.one * Mathf.Max(1.7778f / aspect, 1f);
			if (damageEffectController != null)
			{
				damageEffectController.transform.localScale = new Vector3(aspect * 10f, 10f, 1f);
			}
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

		private ParticleSystemController CreateEffect(string effectName)
		{
			float aspect = Screen.height > 0 ? (float)Screen.width / Screen.height : 1.7778f;
			GameObject prefab = AssetBundleUtility.LoadAsset<GameObject>(LiveConfig.EffectBundleName, effectName);
			if (prefab == null)
			{
				return null;
			}

			GameObject instance = Instantiate(prefab, transform, false);
			ParticleSystemController controller = instance.AddComponent<ParticleSystemController>();
			controller.transform.localScale = new Vector3(aspect * 10f, 10f, 1f);
			return controller;
		}
	}
}
