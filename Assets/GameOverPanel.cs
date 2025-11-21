using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverPanel : UIPanel
{
    [SerializeField] private Button m_quitButton;
    [SerializeField] private Button m_restartButton;
    [SerializeField] private GameObject m_coinVfx;
    [SerializeField] private TextMeshProUGUI m_coinText;

    [SerializeField] private TextMeshProUGUI m_title;
    [SerializeField] private string m_winStr = "Vous avez gagné";
    [SerializeField] private string m_lossStr = "Vous avez perdu";

    public UnityEvent OnQuitInvoked = new UnityEvent();
    public UnityEvent OnRestartInvoked = new UnityEvent();

    private void Start()
    {
        m_quitButton.onClick.AddListener(OnQuitInvoked.Invoke);
        m_restartButton.onClick.AddListener(OnRestartInvoked.Invoke);
    }

    public override void Show()
    {
        base.Show();
        m_coinVfx.SetActive(true);
    }
    public override void Hide()
    {
        base.Hide();
        m_coinVfx.SetActive(false);
    }

    public void SetCoinsCount(int coins)
    {
        if (coins == 0)
            m_title.text = m_lossStr;
        else
            m_title.text = m_winStr;

        m_coinText.text = coins.ToString();
    }
}
