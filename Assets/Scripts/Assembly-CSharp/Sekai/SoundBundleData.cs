using System;
using UnityEngine;

namespace Sekai
{
    [Serializable]
    public class SoundBundleData
    {
        public enum FormatType
        {
            Normal = 0,
            Split = 1
        }

        [SerializeField]
        private FormatType formatType;

        [SerializeField]
        private string assetBundleFileName;

        [SerializeField]
        private string cueSheetName;

        // Keep the original serialized field typo so copied assets deserialize correctly.
        [SerializeField]
        private int spilitFileNum;

        public FormatType Format
        {
            get => formatType;
            set => formatType = value;
        }

        public string AssetBundleFileName
        {
            get => assetBundleFileName;
            set => assetBundleFileName = value;
        }

        public string CueSheetName
        {
            get => cueSheetName;
            set => cueSheetName = value;
        }

        public int SpilitFileNum
        {
            get => spilitFileNum;
            set => spilitFileNum = value;
        }
    }
}
