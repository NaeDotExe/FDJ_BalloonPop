using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialPanel : UIPanel
{
    [SerializeField] private Button m_startButton;
    [SerializeField] private Button m_scrollButton;

    public UnityEvent OnStartButtonClicked = new UnityEvent();
    private void Start()
    {
        m_startButton.onClick.AddListener(StartButtonClicked);
    }

    private void StartButtonClicked()
    {
        OnStartButtonClicked.Invoke();
    }
}
