using Sekai.UI;
using UnityEngine;

namespace Sekai
{
    public class ConsecutiveAutoLivePauseDialog : Common2ButtonDialog
    {
        [SerializeField] private CustomTextMesh modeText;

        public void Setup()
        {
            ConsecutiveAutoLiveData data = AutoLiveUtility.LoadConsecutiveAutoLiveData();
            if (data == null)
            {
                Close();
                return;
            }

            if (modeText == null)
            {
                return;
            }

            if (data.runMode == (int)ConsecutiveAutoLiveData.RunMode.UntilNotEnough)
            {
                modeText.text = WordingManager.Get("MSG_CONSECUTIVE_AUTO_LIVE_UNTILE_NOT_ENOUGH");
            }
            else if (data.runMode == (int)ConsecutiveAutoLiveData.RunMode.NumberOfPlay)
            {
                modeText.text = string.Format(
                    WordingManager.Get("WORD_CONSECUTIVE_AUTO_LIVE_NUMBER_OF_PLAY"),
                    AutoLiveUtility.GetCurrentConsecutiveAutoLiveRemainCount());
            }
        }

        protected override void OnCloseExternal()
        {
            OnClickOK();
        }
    }
}
