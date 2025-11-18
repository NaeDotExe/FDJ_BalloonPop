using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerArm : MonoBehaviour
{
    #region Attributes
    [SerializeField] private Animator m_animator;
    [SerializeField] private Transform m_parent;

    [Space]
    [SerializeField] private Transform m_dartOrigin;
    [SerializeField] private Dart m_dartPrefab;
    [SerializeField] private float m_throwForce = 20f;

    private Vector3 m_crtTarget;
    private Dart m_crtDart;
    #endregion

    public UnityEvent<Balloon> OnThrowComplete = new UnityEvent<Balloon>();

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
        Vector3 rot = Vector3.zero;

        m_parent.DORotate(rot, 0.5f);

        m_parent.LookAt(target);
    }


    public void StartThrow()
    {
        // tmp
        m_crtDart = Instantiate(m_dartPrefab, m_dartOrigin);
        m_crtDart.OnBalloonHit.AddListener(OnThrowComplete.Invoke);
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
    #endregion
}
