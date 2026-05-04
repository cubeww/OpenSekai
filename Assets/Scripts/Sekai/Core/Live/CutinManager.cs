using UnityEngine;

namespace Sekai.Core.Live
{
    public class CutinManager : MonoBehaviour
    {
        [SerializeField] private CutinView cutinView;
        [SerializeField] private SoloSkillTextViewGroup skillTextViewGroup;

        public void Setup(LiveBootDataBase bootData, BaseLiveController baseLiveController) { }
        public void SetupScore(LiveScore score) { }
        public void Load() { }
        public void Unload() { }
        public void Clear() { }
    }
}
