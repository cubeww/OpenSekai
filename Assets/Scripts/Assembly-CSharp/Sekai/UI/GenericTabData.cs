namespace Sekai.UI
{
	public abstract class GenericTabData
	{
		public int Index { get; }

		public bool IsEnabled { get; private set; }

		protected GenericTabData(int index, bool isEnabled = true)
		{
			Index = index;
			IsEnabled = isEnabled;
		}

		public void ApplyEnabled(bool value)
		{
			IsEnabled = value;
		}
	}
}
