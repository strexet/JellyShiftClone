using System;
using UnityEngine;

namespace JellyShift.Game.Physics
{
    public class CollisionDetector : MonoBehaviour
    {
        public event Action<GameObject> Collided; 

        public void OnCollision(GameObject other)
        {
            Collided?.Invoke(other);
        }
    }
}