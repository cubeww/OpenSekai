using UnityEngine;

namespace Sekai
{
    public class SoundBundleBuildData : ScriptableObject
    {
        [SerializeField]
        private string acfFile;

        [SerializeField]
        private SoundBundleData[] acbFiles;

        public string AcfFile
        {
            get => acfFile;
            set => acfFile = value;
        }

        public SoundBundleData[] AcbFiles
        {
            get => acbFiles;
            set => acbFiles = value;
        }
    }
}
