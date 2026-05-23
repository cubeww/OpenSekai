using UnityEngine;

namespace Sekai
{
	public class ScreenLayerData : ScriptableObject
	{
		public MenuScreenType ScreenType;
		public DisplayLayerType DisplayLayer;
		public ScreenInAnimType StartAnimationType;
		public Texture2D MaskInTexture;
		public Rect MaskInUV;
		public bool MaskInHaveDirection;
		public ScreenOutAnimType ExitAnimationType;
		public Texture2D MaskOutTexture;
		public Rect MaskOutUV;
		public bool MaskOutHaveDirection;
		public HeaderDisplay DisplayHeader;
		public HeaderCategory DisplayCategory;
		public HeaderDisplay DisplayPlayerInfo;
		public HeaderDisplay DisplayBackUIScreen;
		public bool EnableBackUIScreen;
		public HeaderDisplay DisplayScreenName;
		public string ScreenName;
		public string ScreenSubName;
		public bool EnableTapScreenAnimation;
		public bool IncludeChildCanvasForAlphaTransiton;
		public bool hasLayerCamera;
		public int layerCameraPriority;
		public ScreenLayerBgmType bgmType;
		public ScreenLayerBackgroundType BackgroundType;

		public GameObject LoadScreenLayerPrefab()
		{
			// IDA shows the original path as "Screen/Prefabs/" + data asset name.
			var prefab = Resources.Load<GameObject>("Screen/Prefabs/" + name);
			return prefab != null ? prefab : Resources.Load<GameObject>("screen/prefabs/" + name);
		}
	}
}
