using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace BalloonPop
{

    [RequireComponent(typeof(Rigidbody))]
    public class Dart : MonoBehaviour
    {
        [SerializeField] private DartCollider m_dartCollider;

        private Rigidbody m_rigidBody;

        public UnityEvent<Balloon> OnBalloonHit = new UnityEvent<Balloon>();

        private void Awake()
        {
            m_rigidBody = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            if (m_dartCollider != null)
                m_dartCollider.CollisionEnter.AddListener(CollisionCallback);
        }

        public void AddForce(Vector3 target, float force)
        {
            Debug.Log("Dart AddForce to " + target.ToString());

            //transform.DOMove(target, 0.2f).SetEase(Ease.Linear);

            m_rigidBody.AddForce((target - transform.position).normalized * force, ForceMode.VelocityChange);
        }
        private void CollisionCallback(Collider collision)
        {
            Debug.Log("yes");

            if (collision.gameObject.tag == "Balloon")
            {


                Balloon balloon = collision.gameObject.GetComponent<Balloon>();
                if (balloon == null)
                {
                    Debug.LogError("Balloon component not found on collided GameObject.");
                    return;
                }
                StickToBalloon(collision);


                OnBalloonHit.Invoke(balloon);
            }
        }

        private void StickToBalloon(Collider collision)
        {
            Debug.Log("stick to balloon");

            m_rigidBody.linearVelocity = Vector3.zero;
            m_rigidBody.isKinematic = true;

            //if (collision.contactCount > 0)
            {
                //ContactPoint contact = collision.GetContact(0);
                transform.position = collision.gameObject.transform.position; //contact.point;
                                                                              //transform.rotation = Quaternion.LookRotation(-contact.normal);
            }

            transform.SetParent(collision.transform, true);
        }
    }

}