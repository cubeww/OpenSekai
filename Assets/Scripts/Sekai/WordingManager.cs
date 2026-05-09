using System.Collections.Generic;

namespace Sekai
{
    public static class WordingManager
    {
        private static readonly Dictionary<string, string> TextByKey = new Dictionary<string, string>
        {
            { "Cancel", "Cancel" },
            { "Resume", "Resume" },
            { "Retry", "Retry" },
            { "WORD_CANCEL", "Cancel" },
            { "WORD_ABORT", "Abort" },
            { "WORD_RETRY", "Retry" },
            { "MSG_PAUSE_LIVE_ABORT", "Abort live?" },
            { "MSG_PAUSE_LIVE_RETRY", "Retry live?" },
            { "MSG_CONSECUTIVE_AUTO_LIVE_UNTILE_NOT_ENOUGH", "Continue Auto Live until resources run out." },
            { "WORD_CONSECUTIVE_AUTO_LIVE_NUMBER_OF_PLAY", "{0} plays remaining" }
        };

        public static string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            return TextByKey.TryGetValue(key, out string text) ? text : key;
        }
    }
}
