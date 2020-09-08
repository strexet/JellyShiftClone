using JellyShift.Player.Controller;
using UnityEngine;

namespace JellyShift.Player.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _playerStart;

        public Vector3 PlayerPosition => _playerTransform.position;

        private void OnValidate()
        {
            _playerTransform = FindObjectOfType<PlayerController>().transform;
        }

        public void SetupPlayer()
        {
            _playerTransform.SetPositionAndRotation(_playerStart.position, _playerStart.rotation);
        }
    }
}