using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using DG.Tweening;
using System;

public class GameplayPanel : UIPanel
{
    #region Attributes
    [SerializeField] private Button m_quit;
    [SerializeField] private List<DartImage> m_remainingDarts;

    [Space]
    [SerializeField] private GameObject m_failPanel;
    [SerializeField] private GameObject m_successPanel;
    [SerializeField] private TextMeshProUGUI m_successText;
    [SerializeField] private string m_successStr = "NIVEAU {0} COMPLETE";
    [SerializeField] private GameObject m_plushiePanel;

    [Space]
    [SerializeField] private GameObject m_optionsPanel;
    [SerializeField] private OptionsSubPanel m_optionsSubPanel;

    [Space]
    [SerializeField] private TextMeshProUGUI m_currentLevel;

    [SerializeField] private string m_levelString = "Niveau {0}";

    [Space]
    [SerializeField] private GameObject m_dartHolder = null;
    #endregion

    #region Methods
    public void ShowFailPanel()
    {
        StartCoroutine(FailPanelCoroutine());
    }
    public void ShowSuccessPanel(int crtLevel, Action callback)
    {
        m_successText.text = string.Format(m_successStr, crtLevel);
        m_successPanel.SetActive(true);

        //m_successPanel.transform.DOScale(0, 0);
        m_successPanel.transform.DOPunchScale(Vector3.one * 1.2f, 0.4f, 2).onComplete += () => StartCoroutine(SuccessCoroutine(callback));
    }
    private IEnumerator SuccessCoroutine(Action callback)
    {
        yield return new WaitForSeconds(2f);
        m_successPanel.SetActive(false);
        callback?.Invoke();
    }
    public void ShowPlushiePanel()
    {
        //StartCoroutine(PlushiePanelCoroutine());

        m_plushiePanel.SetActive(true);
        m_plushiePanel.transform.DOPunchScale(Vector3.one * 1.2f, 0.4f, 2).onComplete += () => StartCoroutine(HidePlushiePanelCoroutine());
    }
    private IEnumerator HidePlushiePanelCoroutine()
    {
        yield return new WaitForSeconds(3f);
        m_plushiePanel.SetActive(false);
    }

    public void ShowBottomPanel(bool show)
    {
        m_dartHolder.SetActive(show);
    }

    private IEnumerator FailPanelCoroutine()
    {
        m_failPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        m_failPanel.SetActive(false);
    }
    private IEnumerator PlushiePanelCoroutine()
    {
        m_plushiePanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        m_plushiePanel.SetActive(false);
    }

    public void OnNewLevelStarted(int level, int dartsCount)
    {
        Debug.Log("LEVEL START : " + level + " DARTS COUNT : " + dartsCount);

        m_currentLevel.text = string.Format(m_levelString, level);

        // set darts count
        for (int i = 0; i < m_remainingDarts.Count; i++)
        {
            m_remainingDarts[i].Show(i < dartsCount);
            m_remainingDarts[i].Enable(i < dartsCount);
        }
    }
    public void OnDartThrown(int remainingDartCount)
    {
        Debug.Log("dart remainign " + remainingDartCount);

        for (int i = 0; i < m_remainingDarts.Count; ++i)
        {
            if (m_remainingDarts[i].Enabled)
                m_remainingDarts[i].Show(i < remainingDartCount);
        }
    }
    #endregion
}
