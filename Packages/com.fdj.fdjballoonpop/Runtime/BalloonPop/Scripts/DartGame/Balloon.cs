using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer)/*, typeof(BoxCollider)*/)]
public class Balloon : MonoBehaviour
{
    #region Attributes
    [SerializeField] private ParticleSystem m_particleSystem;
    [SerializeField] private GameObject m_number;

    private MeshRenderer m_meshRenderer;
    private MeshCollider m_collider;

    private int m_index = -1;
    private bool m_isTrapped = false;
    #endregion

    #region Properties
    public int Index
    {
        get { return m_index; }
    }
    public bool IsTrapped
    {
        get { return m_isTrapped; }
    }
    #endregion

    #region Events
    public UnityEvent OnBalloonExploded = new UnityEvent();
    #endregion

    #region Methods
    private void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        if (m_meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component not found on Balloon GameObject.");
        }

        m_collider = GetComponent<MeshCollider>();
        if (m_collider == null)
        {
            Debug.LogError("MeshCollider component not found on Balloon GameObject.");
        }
    }

    public void Init(int id, bool isTrapped)
    {
        m_index = id;
        m_isTrapped = isTrapped;

        if (m_isTrapped)
        {
            // test
            //m_meshRenderer.material.color = Color.black;
        }
    }

    public void Explode()
    {
        Enable(false);

        OnBalloonExploded.Invoke();
    }

    private void Enable(bool enable)
    {
        m_meshRenderer.enabled = enable;
        m_collider.enabled = enable;
        m_number.SetActive(enable);

        m_particleSystem.gameObject.SetActive(enable);
    }
    #endregion
}