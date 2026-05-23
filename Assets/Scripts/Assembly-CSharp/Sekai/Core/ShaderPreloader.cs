namespace Sekai.Core
{
	public static class ShaderPreloader
	{
		public enum PreloadStatus
		{
			None = 0,
			Loading = 1,
			Compiling = 2
		}

		public static PreloadStatus Status { get; set; }
	}
}
