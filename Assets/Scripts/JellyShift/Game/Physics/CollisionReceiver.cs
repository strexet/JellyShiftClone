using System;
using UnityEngine;

namespace JellyShift.Game.Physics
{
    public class CollisionReceiver : MonoBehaviour
    {
        public event Action<GameObject> Collided;

        public void OnCollisionReceived(GameObject other) => Collided?.Invoke(other);
    }
}