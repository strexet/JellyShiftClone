using System;
using UnityEngine;

namespace JellyShift.Game.Physics
{
    public class CollisionDetector : MonoBehaviour
    {
        public event Action<GameObject> Collided;

        public void OnCollisionEnter(Collision collision)
        {
            var otherGameObject = collision.gameObject;

            Collided?.Invoke(otherGameObject);

            NotifyCollisionReceiver(otherGameObject);
        }

        private void NotifyCollisionReceiver(GameObject otherGameObject)
        {
            var collisionReceiver = otherGameObject.GetComponent<CollisionReceiver>();

            if (collisionReceiver != null)
            {
                collisionReceiver.OnCollisionReceived(gameObject);
            }
        }
    }
}