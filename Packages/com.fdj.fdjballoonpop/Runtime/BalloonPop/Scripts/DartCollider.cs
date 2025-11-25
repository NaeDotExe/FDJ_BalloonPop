using UnityEngine;
using UnityEngine.Events;

namespace BalloonPop
{

    [RequireComponent(typeof(BoxCollider))]
    public class DartCollider : MonoBehaviour
    {
        #region Events
        public UnityEvent<Collider> CollisionEnter = new UnityEvent<Collider>();
        #endregion

        private void OnTriggerEnter(Collider collider)
        {
            Debug.Log("dart collision");

            CollisionEnter.Invoke(collider);
        }
    }

}