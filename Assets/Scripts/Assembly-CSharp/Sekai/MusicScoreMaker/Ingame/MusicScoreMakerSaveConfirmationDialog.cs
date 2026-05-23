namespace Sekai.MusicScoreMaker.Ingame
{
	public sealed class MusicScoreMakerSaveConfirmationDialog : CommonMultiButtonDialog
	{
		protected override void OnHardwareBackKeyProcess()
		{
			Close();
		}

		public MusicScoreMakerSaveConfirmationDialog()
		{
		}
	}
}
