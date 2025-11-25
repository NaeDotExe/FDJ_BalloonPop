using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace BalloonPop
{

    public class OptionsSubPanel : UIPanel
    {
        #region Attributes
        [SerializeField] private Button m_quit;
        [SerializeField] private Button m_continue;

        [Space]
        [SerializeField, TextArea(2, 10)] private string m_quitString;
        [SerializeField, TextArea(2, 10)] private string m_chanceOfWinningString;
        [SerializeField, TextArea(2, 10)] private string m_minMultiplierString;
        [SerializeField, TextArea(2, 10)] private string m_maxMultiplierString;

        [Space]
        [SerializeField] private TextMeshProUGUI m_quitText;
        [SerializeField] private TextMeshProUGUI m_chanceOfWinningText;
        [SerializeField] private TextMeshProUGUI m_minMultiplierText;
        [SerializeField] private TextMeshProUGUI m_maxMultiplierText;

        [Space]
        [SerializeField] private GameObject m_coin;

        #endregion

        #region Events
        public UnityEvent OnQuitClicked = new UnityEvent();
        public UnityEvent OnContinueClicked = new UnityEvent();
        #endregion


        private void Start()
        {
            m_quit.onClick.AddListener(() =>
            {
                OnQuitClicked.Invoke();

            });
            m_continue.onClick.AddListener(() =>
            {
                Hide();

                OnContinueClicked.Invoke();
            });

            m_coin.SetActive(false);
        }
        public void SetTexts(float currentGain, float chanceOfWinning, float minMultiplier, float maxMultiplier)
        {
            m_quitText.text = string.Format(m_quitString, currentGain);

            m_chanceOfWinningText.text = string.Format(m_chanceOfWinningString, chanceOfWinning);
            m_minMultiplierText.text = string.Format(m_minMultiplierString, minMultiplier, currentGain * minMultiplier);
            m_maxMultiplierText.text = string.Format(m_maxMultiplierString, maxMultiplier, currentGain * maxMultiplier);
        }

        public override void Hide()
        {
            base.Hide();

            m_coin.SetActive(false);
        }
        public override void Show()
        {
            base.Show();

            m_coin.SetActive(true);
        }
    }

}