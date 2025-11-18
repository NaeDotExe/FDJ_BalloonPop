using DG.Tweening;
using UnityEngine;

public class WonMultiplierCanvas : MonoBehaviour
{
    [SerializeField] private GameObject m_multiplier15;
    [SerializeField] private GameObject m_multiplier2;
    [SerializeField] private GameObject m_multiplier3;
    [SerializeField] private GameObject m_multiplier4;
    [SerializeField] private GameObject m_multiplier5;

    public void ShowMultiplier(float value)
    {
        m_multiplier15.SetActive(false);
        m_multiplier2.SetActive(false);
        m_multiplier3.SetActive(false);
        m_multiplier4.SetActive(false);
        m_multiplier5.SetActive(false);

        if (value == 1.5f)
        {
            m_multiplier15.SetActive(true);
            m_multiplier15.transform.DOPunchScale(transform.localScale * 1.1f, 0.5f, 2).onComplete +=()=> m_multiplier15.SetActive(false);
        }
        else if (value == 2f)
        {
            m_multiplier2.SetActive(true);
            m_multiplier2.transform.DOPunchScale(transform.localScale * 1.1f, 0.5f, 2).onComplete += () => m_multiplier2.SetActive(false);
        }
        else if (value == 3f)
        {
            m_multiplier3.SetActive(true);
            m_multiplier3.transform.DOPunchScale(transform.localScale * 1.1f, 0.5f, 2).onComplete += () => m_multiplier3.SetActive(false);

        }
        else if (value == 4f)
        {
            m_multiplier4.SetActive(true);
            m_multiplier4.transform.DOPunchScale(transform.localScale * 1.1f, 0.5f, 2).onComplete += () => m_multiplier4.SetActive(false);

        }
        else if (value == 5f)
        {
            m_multiplier5.SetActive(true);
            m_multiplier5.transform.DOPunchScale(transform.localScale * 1.1f, 0.5f, 2).onComplete += () => m_multiplier5.SetActive(false);
        }
    }
}
