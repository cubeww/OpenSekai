using UnityEngine;

namespace Sekai.Core.Live
{
    [ExecuteInEditMode]
    public class ParticleShaderSettings : MonoBehaviour
    {
        public enum Mode
        {
            Additive,
            AlphaBlend
        }

        [SerializeField] private Mode mode;
    }
}
