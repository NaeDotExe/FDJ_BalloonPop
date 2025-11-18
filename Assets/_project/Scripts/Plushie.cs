using DG.Tweening;
using UnityEngine;

public class Plushie : MonoBehaviour
{
    private MeshRenderer m_meshRenderer;
    private MeshCollider m_collider;

    private void Awake()
    {
        if (m_collider == null)
            m_collider = GetComponent<MeshCollider>();
        if (m_meshRenderer == null)
            m_meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Enable(bool enable)
    {
        if (enable)
        {
            gameObject.SetActive(true);

        }
        else
            OnDisablePlushie();
    }

    private void OnDisablePlushie()
    {
        //transform.DOScale(transform.localScale * 1.1f, 1f).OnComplete(() =>
        //{
        //    gameObject.SetActive(false);
        //});

        transform.DOScale(Vector3.zero , 0.5f).OnComplete(() => gameObject.SetActive(false));
    }
}
