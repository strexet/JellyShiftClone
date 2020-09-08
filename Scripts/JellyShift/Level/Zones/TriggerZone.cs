using UnityEngine;

namespace JellyShift.Level.Zones
{
    public abstract class TriggerZone : MonoBehaviour
    {
        public event ZoneDelegate EnteredZone = delegate { };

        private void OnTriggerEnter(Collider other)
        {
            EnteredZone.Invoke(this);
        }

        public void OnDrawGizmos()
        {
            var color = Color.gray;

            switch (this)
            {
                case StartTransitionZone _:
                    color = Color.green;
                    break;
                case FinishTransitionZone _:
                    color = Color.red;
                    break;
                case FinishLevelZone _:
                    color = Color.blue;
                    break;
            }

            color.a = 0.55f;
            Gizmos.color = color;

            var t = transform;
            Gizmos.DrawCube(t.position, t.localScale);
        }
    }
}