using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class BalloonManager : MonoBehaviour
{
    [SerializeField] private List<Balloon> m_balloons = new List<Balloon>();

    [Space]
    [SerializeField]private DartFXPlayer m_dartFXPlayer;

    private int m_trappedBalloons = 5;
    private int m_activeBalloons = 30;

    public UnityEvent<bool> OnBalloonExploded = new UnityEvent<bool>();

    public void InitBalloons(int trappedCount)
    {
        m_activeBalloons = m_balloons.Count;

        int totalCount = m_activeBalloons;
        int remainingToPick = trappedCount;

        // randomly assign trapped balloons
        for (int i = 0; i < m_balloons.Count; i++)
        {
            bool isTrapped = false;
            int remainingItems = totalCount - i;

            if (remainingToPick > 0)
            {
                float probability = (float)remainingToPick / (float)remainingItems;
                if (Random.value < probability)
                {
                    isTrapped = true;
                    remainingToPick--;
                }
            }

            // set balloon index and trapped status
            m_balloons[i].Init(i, isTrapped);
        }
    }

    public void ExplodeBalloon(Balloon balloon)
    {
        // hides balloon and plays explosion effect
        balloon.Explode();
        m_dartFXPlayer.PlaySFX(balloon);

        if (balloon.IsTrapped)
        {
            Debug.LogError("Trapped balloon exploded! Game Over.");
            // Handle game over logic here
            OnBalloonExploded.Invoke(false);

            StartCoroutine(Test());
        }
        else
        {
            OnBalloonExploded.Invoke(true);
            m_activeBalloons--;
        }
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Main");
    }
}
