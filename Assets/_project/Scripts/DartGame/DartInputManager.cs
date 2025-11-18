using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Detects where the player clicks/taps and throws a dart towards that position.
/// </summary>
public class DartInputManager : MonoBehaviour
{
    #region Attributes
    [SerializeField] private Camera m_main;
    [SerializeField] private LayerMask m_balloonLayerMask;

    private bool _enableInput = false;
    private Balloon m_lastTouchedBalloon = null;
    #endregion

    public bool EnableInput
    {
        get { return _enableInput; }
        set { _enableInput = value; }
    }

    public Balloon LastTouchedBalloon
    {
        get { return m_lastTouchedBalloon; }
    }

    public UnityEvent<Balloon> OnBalloonTouched = new UnityEvent<Balloon>();

    #region Methods
    private void Update()
    {
        if (!_enableInput) 
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, m_balloonLayerMask))
            {
                Balloon balloon = hitInfo.collider.GetComponent<Balloon>();
                if (balloon != null)
                {
                    _enableInput = false;
                    OnBalloonTouched.Invoke(balloon);
                }
            }
        }
    }
    #endregion
}
