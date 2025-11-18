using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class DartGameUIManager : MonoBehaviour
{
    [SerializeField] private MainMenu m_mainMenu;
    [SerializeField] private TutorialPanel m_tutorialPanel;
    [SerializeField] private GameplayPanel m_gameplayPanel;
    [SerializeField] private OptionsSubPanel m_optionsSubpanel;
    [SerializeField] private MegaBalloonPanel m_megaballoonPanel;

    public UnityEvent OnMainMenuShown = new UnityEvent();
    public UnityEvent<bool>  OnGameUIShown = new UnityEvent<bool>();
    public UnityEvent OnTutorialShown = new UnityEvent();

    // to remove later
    public GameplayPanel GameplayPanel
    {
        get { return m_gameplayPanel; }
    }

    private void Start()
    {
        m_mainMenu.OnStartInvoked.AddListener(ShowTutorial);
        m_tutorialPanel.OnStartButtonClicked.AddListener(() => ShowGameUI(true));
        m_optionsSubpanel.OnQuitClicked.AddListener(ShowMainMenu);
        m_optionsSubpanel.OnContinueClicked.AddListener(() => ShowGameUI(false));
    }

    public void ShowMainMenu()
    {
        m_mainMenu.Show();
        m_tutorialPanel.Hide();
        m_gameplayPanel.Hide();
        m_megaballoonPanel.Hide();
        
        OnMainMenuShown.Invoke();
    }
    public void ShowTutorial()
    {
        m_mainMenu.Hide();
        m_tutorialPanel.Show();
        m_gameplayPanel.Hide();
        m_megaballoonPanel.Hide();

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

        OnGameUIShown.Invoke(fromTutorial);
    }

    public void ShowFailPanel()
    {
        m_gameplayPanel.ShowFailPanel();
    }
    public void ShowSuccessPanel(int lvl, Action callback)
    {
        m_gameplayPanel.ShowSuccessPanel(lvl, () => callback?.Invoke());
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
