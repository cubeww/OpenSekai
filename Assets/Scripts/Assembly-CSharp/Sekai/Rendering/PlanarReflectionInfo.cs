using System;

namespace Sekai.Rendering
{
	[Serializable]
	public struct PlanarReflectionInfo
	{
		public int width;
		public int height;
		public float clipPlaneOffset;
		public float planeOffset;
	}
}
