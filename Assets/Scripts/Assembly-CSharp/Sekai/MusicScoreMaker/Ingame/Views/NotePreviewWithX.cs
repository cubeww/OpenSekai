namespace Sekai.MusicScoreMaker.Ingame.Views
{
	internal struct NotePreviewWithX
	{
		public NotePreview note;

		public float x;

		public NotePreviewWithX(NotePreview note, float x)
		{
			this.note = note;
			this.x = x;
		}
	}
}
