using UnityEngine;

namespace JellyShift.Loading
{
    public class LevelAutoLoader : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager.LoadLevel();
        }
    }
}