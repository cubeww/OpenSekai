using Sekai.Core.Live;
using UnityEngine;

namespace Sekai.Live
{
    public partial class LiveFrontView
    {
        [SerializeField] private Transform liveRoot;
        [SerializeField] private SpriteRenderer fade;
        [SerializeField] private LaneView laneView;
        [SerializeField] private MusicInfoView musicInfo;
        [SerializeField] private JudgmentView judgmentView;
        [SerializeField] private JudgmentDescriptionView judgmentDescriptionView;
        [SerializeField] private ComboView comboView;
        [SerializeField] private ScoreView scoreView;
        [SerializeField] private LifeView lifeView;
        [SerializeField] private LiveResultView liveResultView;
        [SerializeField] private CutinManager cutinManager;
        [SerializeField] private SkillView skillView;
        [SerializeField] private CountdownView countdownView;
        [SerializeField] private ScreenEffectView screenEffectView;
        [SerializeField] private Transform effectRoot;
        [SerializeField] private Camera effectCamera;
        [SerializeField] private GameObject autoLabel;
        [SerializeField] private ConsecutiveAutoLiveView consecutiveAutoLiveView;
    }
}
