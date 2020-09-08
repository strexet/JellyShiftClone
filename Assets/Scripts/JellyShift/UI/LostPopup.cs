using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JellyShift.UI
{
    public class LostPopup : MonoBehaviour
    {
        [SerializeField] private Button _touchZone;
        [SerializeField] private GameObject _gameObject;

        private void OnDisable()
        {
            _touchZone.onClick.RemoveAllListeners();
        }

        public void Show(UnityAction clickCallback)
        {
            _gameObject.SetActive(true);
            _touchZone.onClick.AddListener(clickCallback);
        }

        public void Hide()
        {
            _gameObject.SetActive(false);
        }
    }
}