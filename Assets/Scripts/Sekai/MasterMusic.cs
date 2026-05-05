namespace Sekai
{
    public class MasterMusic
    {
        public int id;

        public string title;

        public string lyricist;

        public string composer;

        public string arranger;

        public float fillerSec;

        public int musicCollaborationId;

        public string Title
        {
            get { return string.IsNullOrEmpty(title) ? $"music_{id:0000}" : title; }
        }

        public string Lyricist
        {
            get { return lyricist ?? string.Empty; }
        }

        public string Composer
        {
            get { return composer ?? string.Empty; }
        }

        public string Arranger
        {
            get { return arranger ?? string.Empty; }
        }
    }
}
