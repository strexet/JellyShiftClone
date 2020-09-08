using UnityEngine;

namespace JellyShift.Collectibles
{
    public class Diamond : MonoBehaviour
    {
        public event CollectibleDelegate Collected = delegate { };

        private void OnTriggerEnter(Collider other)
        {
            Collected.Invoke();
            gameObject.SetActive(false);
        }
    }
}