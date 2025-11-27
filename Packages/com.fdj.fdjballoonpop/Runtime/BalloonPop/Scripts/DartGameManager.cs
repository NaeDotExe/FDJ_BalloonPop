using NUnit.Framework.Constraints;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace BalloonPop
{

    public class DartGameManager : MonoBehaviour
    {
        #region Attributes
        [SerializeField] private DartInputManager m_dartInputManager;
        [SerializeField] private BalloonManager m_balloonManager;
        [SerializeField] private DartMultiplerManager m_multiplierManager;
        [SerializeField] private DartGameUIManager m_uiManager;

        [Space]
        [SerializeField] private PlayerArm m_playerArm;

        [Space]
        [SerializeField] private DartGameData m_gameData;

        [SerializeField] private GameObject m_megaBalloon;

        //[SerializeField] private TextMeshProUGUI _tmpText;

        [SerializeField] private CameraController m_cameraController;

        private int m_currentLevel = -1;
        private int m_dartsThrown = 0;
        private int m_maxBallonsToPop = 0;

        private float m_bet = 10;
        private float m_coins = 0;
        private float m_multiplier = 1f;
        #endregion

        #region Events
        private UnityEvent OnLevelComplete = new UnityEvent();
        private UnityEvent<bool> OnGameOver = new UnityEvent<bool>();

        public UnityEvent RestartGameRequest = new UnityEvent();
        public UnityEvent QuitGameRequest = new UnityEvent();
        #endregion

        #region Methods
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            m_balloonManager.OnBalloonExploded.AddListener(OnBalloonExplodedCallback);
            m_dartInputManager.OnBalloonTouched.AddListener(OnBalloonTouchedCallback);

            m_multiplierManager.OnMultiplierSelected.AddListener(OnMultiplierSelectedCallback);
            m_uiManager.OnGameUIShown.AddListener((isFromTuto) =>
            {
                //m_dartInputManager.EnableInput = true;
                StartNextLevel();
            });
            m_uiManager.OnTutorialShown.AddListener(() =>
            {
                m_cameraController.SwitchToDartGameplay();
            });
            m_uiManager.OnQuitGameInvoked.AddListener(QuitGame);
            m_uiManager.OnRestartGameInvoked.AddListener(RestartGame);
            m_playerArm.OnThrowComplete.AddListener(m_balloonManager.ExplodeBalloon);

            m_balloonManager.InitBalloons(5);
            m_megaBalloon.SetActive(false);

            m_coins = m_bet;
            StartMainMenu();
            //StartLevel(m_currentLevel);
        }

        private void OnBalloonExplodedCallback(bool success)
        {
            m_uiManager.GameplayPanel.OnDartThrown(m_maxBallonsToPop - m_dartsThrown - 1);
            if (success)
            {
                ++m_dartsThrown;
                if (m_dartsThrown >= m_maxBallonsToPop)
                {
                    LevelComplete();
                }
                else
                {
                    m_dartInputManager.EnableInput = true;
                }
            }
            else
            {
                GameOver(false);
                Debug.LogError("A trapped balloon was popped! Game Over.");
            }
        }
        private void OnBalloonTouchedCallback(Balloon balloon)
        {
            m_playerArm.PlayThrowAnimation(balloon.transform);
        }
        private void OnMultiplierSelectedCallback(float value)
        {
            m_multiplier = value;
            m_coins *= m_multiplier;
            m_uiManager.HidePlushiePanel();

            if (m_currentLevel + 1 >= m_gameData.LevelsCount)
            {
                // Show victory panel if we reach the last level
                int coins = (int)m_coins;
                m_uiManager.ShowGameOverPanel(coins);
            }
            else
            {
                m_cameraController.SwitchToDartGameplay();

                // show continue panel
                ShowContinuePanel();
            }
        }
        private void GameOver(bool success)
        {
            StartCoroutine(GameOverCoroutine(success));
        }

        private IEnumerator GameOverCoroutine(bool success)
        {
            m_coins = 0;

            OnGameOver.Invoke(success);
            yield return new WaitForSeconds(1f);
            m_uiManager.ShowFailPanel();
            yield return new WaitForSeconds(2f);
            m_uiManager.ShowGameOverPanel();
        }
        private void LevelComplete()
        {
            OnLevelComplete.Invoke();

            StartMultiplierPhase();
        }

        private void StartMainMenu()
        {
            m_playerArm.Show(false);
            m_cameraController.SwitchToMainMenu();
            m_uiManager.ShowMainMenu();
        }

        private void StartMultiplierPhase()
        {
            m_dartInputManager.EnableInput = false;
            m_playerArm.Show(false);

            m_uiManager.ShowSuccessPanel(m_currentLevel + 1, () =>
            {
                if (m_currentLevel + 1 == m_gameData.LevelsCount)
                {
                    // hide balloons and darts
                    m_balloonManager.ShowBalloons(false);
                    m_playerArm.DestroyDarts();

                    // mega ballon 
                    m_uiManager.ShowMegaballonPanel();
                    m_megaBalloon.SetActive(true);
                    m_multiplierManager.StartMultiplierScene(m_gameData.MultiplierData, true, m_currentLevel);
                }
                else
                {
                    m_multiplierManager.StartMultiplierScene(m_gameData.MultiplierData, true, m_currentLevel);
                    m_uiManager.ShowMultiplierPanel();
                    m_cameraController.SwitchToDartPlushies();
                }
            });
        }
        private void StartNextLevel()
        {
            ++m_currentLevel;
            m_dartInputManager.EnableInput = true;

            m_dartsThrown = 0;
            m_maxBallonsToPop = m_gameData.GetBalloonsToPop(m_currentLevel);
            m_cameraController.SwitchToDartGameplay();
            m_playerArm.Show(true);

            // tmp
            m_uiManager.GameplayPanel.OnNewLevelStarted(m_currentLevel + 1, m_maxBallonsToPop);
        }
        #endregion

        private void ShowContinuePanel()
        {
            m_uiManager.SetTexts(m_coins, m_gameData.LevelDatas[m_currentLevel].Chance,
    m_gameData.MultiplierData.GetMinMultiplier(m_currentLevel),
    m_gameData.MultiplierData.GetMaxMultiplier(m_currentLevel));

            m_uiManager.ShowContinuePanel();
        }

        public void QuitGame()
        {
            // go back to world view
            QuitGameRequest.Invoke();
        }
        public void RestartGame()
        {
            RestartGameRequest.Invoke();
            //SceneManager.LoadScene("Main");
        }
    }
}