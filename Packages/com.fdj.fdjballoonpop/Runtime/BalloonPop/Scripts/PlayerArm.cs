using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;

namespace BalloonPop
{
    public class PlayerArm : MonoBehaviour
    {
        #region Attributes
        [SerializeField] private Animator m_animator;
        [SerializeField] private Transform m_parent;
        [SerializeField] private SkinnedMeshRenderer m_meshRenderer;

        [Space]
        [SerializeField] private Transform m_dartOrigin;
        [SerializeField] private Dart m_dartPrefab;
        [SerializeField] private float m_throwForce = 20f;

        private Vector3 m_crtTarget;
        private Dart m_crtDart;
        private List<Dart> m_darts = new List<Dart>();
        #endregion

        #region Events
        public UnityEvent<Balloon> OnThrowComplete = new UnityEvent<Balloon>();
        #endregion

        #region Methods
        public void PlayThrowAnimation(Transform target)
        {
            m_animator.ResetTrigger("throwDart");
            m_animator.SetTrigger("throwDart");

            m_crtTarget = target.position;

            LookTowards(target);
        }
        private void LookTowards(Transform target)
        {
            m_parent.DOLookAt(target.position, 0.4f, AxisConstraint.Y);
        }

        public void StartThrow()
        {
            // tmp
            Dart newDart = Instantiate(m_dartPrefab, m_dartOrigin);
            m_crtDart = newDart;
            m_crtDart.OnBalloonHit.AddListener(OnThrowComplete.Invoke);
            m_darts.Add(newDart);
        }
        public void ThrowAnimEnded()
        {
            m_crtDart.transform.parent = null;
            // somehow default scale is negative
            m_crtDart.transform.localScale *= -1;
            Vector3 rot = m_crtDart.transform.eulerAngles;
            m_crtDart.transform.eulerAngles = new Vector3(rot.x, rot.y - 180, rot.z);

            m_crtDart.AddForce(m_crtTarget, m_throwForce);
        }
        public void Show(bool show)
        {
            m_meshRenderer.enabled = show;
        }

        public void DestroyDarts()
        {
            foreach (Dart d in m_darts)
                Destroy(d.gameObject);

            m_darts.Clear();
        }
        #endregion
    }

}