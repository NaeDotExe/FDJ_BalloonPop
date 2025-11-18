using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : UIPanel
{
    #region Attributes
    [SerializeField] private Button m_startButton;
    #endregion

    #region Events
    public UnityEvent OnStartInvoked = new UnityEvent();
    #endregion

    #region Methods
    private void Start()
    {
        m_startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        OnStartInvoked.Invoke();
    }
    #endregion
}
