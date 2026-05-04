using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
    [ExecuteInEditMode]
    public class AlphaMask : MonoBehaviour
    {
        [SerializeField] private Image maskImage;
        [SerializeField] private bool unmask;
    }
}
