using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Sekai
{
	public class AtlasImage : Image
	{
		[SerializeField]
		protected SpriteAtlas atlas;

		[SerializeField]
		protected string spriteName;

		[SerializeField]
		protected bool useSharedSprite;

		public bool UseSharedSprite
		{
			get
			{
				return useSharedSprite;
			}
			set
			{
				useSharedSprite = value;
			}
		}

		public SpriteAtlas Atlas
		{
			get
			{
				return atlas;
			}
			set
			{
				atlas = value;
			}
		}

		public string SpriteName
		{
			get
			{
				return spriteName;
			}
			set
			{
				spriteName = value;
				if (atlas != null)
				{
					sprite = useSharedSprite ? UIUtility.GetSharedSprite(atlas, spriteName) : atlas.GetSprite(spriteName);
				}
			}
		}

		public static void UnloadSharedSprites()
		{
			UIUtility.UnloadSharedSprites();
		}

		public static void UnloadSharedSprites(string atlasName)
		{
			UIUtility.UnloadSharedSprites(atlasName);
		}

		protected override void Awake()
		{
		}

		protected override void Start()
		{
		}

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		public AtlasImage()
		{
			useSharedSprite = true;
		}
	}
}
