using Sekai.Core.Live;
using DG.Tweening;
using UnityEngine;

namespace Sekai.Live
{
	public class LiveResultView : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer background;

		private bool _shouldPlayVoiceSE;

		public void Setup(bool shouldPlayVoiceSE)
		{
			_shouldPlayVoiceSE = shouldPlayVoiceSE;
			if (background != null)
			{
				background.gameObject.SetActive(false);
				Color color = background.color;
				color.a = 0f;
				background.color = color;
			}
		}

		public void Execute(LiveResultAnimationType liveResultAnimationType)
		{
			switch (liveResultAnimationType)
			{
				case LiveResultAnimationType.None:
					return;
				case LiveResultAnimationType.Failure:
					PlayEndVoice(LiveSoundDefine.SE_LIVE_END_FINISH);
					Fade("fx_result_failed_v2");
					break;
				case LiveResultAnimationType.LifeZero:
					PlayEndVoice(LiveSoundDefine.SE_LIVE_END_FINISH);
					Fade("fx_result_finish");
					break;
				case LiveResultAnimationType.Clear:
					SoundManager.Instance.PlayIngameSE(LiveSoundDefine.SE_LIVE_CLEAR);
					PlayEndVoice(LiveSoundDefine.SE_LIVE_END_CLEAR);
					Fade("fx_result_clear_v2");
					break;
				case LiveResultAnimationType.FullCombo:
					SoundManager.Instance.PlayIngameSE(LiveSoundDefine.SE_LIVE_FULL_COMBO);
					PlayEndVoice(LiveSoundDefine.SE_LIVE_END_PERFECT);
					Fade("fx_result_full_combo_v2");
					break;
				case LiveResultAnimationType.AllPerfect:
					SoundManager.Instance.PlayIngameSE(LiveSoundDefine.SE_LIVE_ALL_PERFECT);
					PlayEndVoice(LiveSoundDefine.SE_LIVE_END_ALL_PERFECT);
					Fade("fx_result_all_perfect_v2");
					break;
				default:
					Debug.LogErrorFormat("Unsupported live result animation type: {0}", liveResultAnimationType);
					break;
			}
		}

		public void Unload()
		{
			AssetBundleUtility.UnloadAssetBundle(LiveConfig.ResultBundleName);
		}

		private void Fade(string prefabName)
		{
			if (background != null)
			{
				background.gameObject.SetActive(true);
				background.DOKill();
				background.DOColor(new Color(0f, 0f, 0f, 0.5f), 0.2f).SetEase(Ease.OutQuad);
			}

			DOVirtual.DelayedCall(0.2f, () =>
			{
				GameObject prefab = AssetBundleUtility.LoadAsset<GameObject>(LiveConfig.ResultBundleName, prefabName, false);
				if (prefab == null)
				{
					prefab = Resources.Load<GameObject>(prefabName);
				}
				if (prefab != null)
				{
					Instantiate(prefab, transform);
				}
			});
		}

		public LiveResultView()
		{
		}

		private void PlayEndVoice(string cueName)
		{
			if (_shouldPlayVoiceSE)
			{
				SoundManager.Instance.PlayIngameSEOneShot(cueName);
			}
		}
	}
}
