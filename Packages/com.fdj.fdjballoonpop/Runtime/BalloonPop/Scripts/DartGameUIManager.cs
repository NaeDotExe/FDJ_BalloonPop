using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace BalloonPop
{

public class DartGameUIManager : MonoBehaviour
{
    [SerializeField] private MainMenu m_mainMenu;
    [SerializeField] private TutorialPanel m_tutorialPanel;
    [SerializeField] private GameplayPanel m_gameplayPanel;
    [SerializeField] private OptionsSubPanel m_optionsSubpanel;
    [SerializeField] private UIPanel m_megaballoonPanel;
    [SerializeField] private GameOverPanel m_gameOverPanel;

    public UnityEvent OnMainMenuShown = new UnityEvent();
    public UnityEvent<bool> OnGameUIShown = new UnityEvent<bool>();
    public UnityEvent OnTutorialShown = new UnityEvent();
    public UnityEvent OnQuitGameInvoked = new UnityEvent();
    public UnityEvent OnRestartGameInvoked = new UnityEvent();

    // to remove later
    public GameplayPanel GameplayPanel
    {
        get { return m_gameplayPanel; }
    }

    private void Start()
    {
        m_mainMenu.OnStartInvoked.AddListener(ShowTutorial);
        m_tutorialPanel.OnStartButtonClicked.AddListener(() => ShowGameUI(true));
            m_optionsSubpanel.OnQuitClicked.AddListener(/*ShowMainMenu*//*()=>SceneManager.LoadScene("Main")*/FindFirstObjectByType<DartGameManager>().QuitGame ); 
        m_optionsSubpanel.OnContinueClicked.AddListener(() => ShowGameUI(false));
        m_gameOverPanel.OnQuitInvoked.AddListener(OnQuitGameInvoked.Invoke);
        m_gameOverPanel.OnRestartInvoked.AddListener(OnRestartGameInvoked.Invoke);
    }

    public void ShowMainMenu()
    {
        m_mainMenu.Show();
        m_tutorialPanel.Hide();
        m_gameplayPanel.Hide();
        m_megaballoonPanel.Hide();
        m_gameOverPanel.Hide();


        OnMainMenuShown.Invoke();
    }
    public void ShowTutorial()
    {
        m_mainMenu.Hide();
        m_tutorialPanel.Show();
        m_gameplayPanel.Hide();
        m_megaballoonPanel.Hide();
        m_gameOverPanel.Hide();

        OnTutorialShown.Invoke();
    }
    public void ShowGameUI(bool fromTutorial)
    {
        // tmp
        m_mainMenu.Hide();
        m_tutorialPanel.Hide();
        m_gameplayPanel.Show();
        m_gameplayPanel.ShowBottomPanel(true);
        m_megaballoonPanel.Hide();
        m_gameOverPanel.Hide();


        OnGameUIShown.Invoke(fromTutorial);
    }

    public void ShowGameOverPanel(int coinCount = 0)
    {
            m_gameOverPanel.SetCoinsCount(coinCount);

        m_mainMenu.Hide();
        m_tutorialPanel.Hide();
        m_gameplayPanel.Hide();
        m_megaballoonPanel.Hide();
        m_gameOverPanel.Show();
    }

    public void ShowFailPanel()
    {
        m_gameplayPanel.ShowFailPanel();
    }
    public void ShowSuccessPanel(int lvl, Action callback)
    {
        m_gameplayPanel.ShowSuccessPanel(lvl, () => callback?.Invoke());
    }
    public void HidePlushiePanel()
    {
        m_gameplayPanel.HidePlushiePanel();
    }
    public void ShowMultiplierPanel()
    {
        m_gameplayPanel.ShowBottomPanel(false);
        m_gameplayPanel.ShowPlushiePanel();
    }

    public void ShowContinuePanel()
    {
        m_optionsSubpanel.Show();
    }
    public void ShowMegaballonPanel()
    {
        m_megaballoonPanel.Show();

        m_mainMenu.Hide();
        m_tutorialPanel.Hide();
        m_gameplayPanel.Hide();
    }
    public void SetTexts(float currentGain, float chanceOfWinning, float minMultiplier, float maxMultiplier)
    {
        m_optionsSubpanel.SetTexts(currentGain, chanceOfWinning, minMultiplier, maxMultiplier);
    }
}

}