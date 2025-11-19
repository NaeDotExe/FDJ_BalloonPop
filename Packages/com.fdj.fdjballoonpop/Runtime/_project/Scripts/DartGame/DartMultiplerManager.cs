using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
using DG.Tweening;

public class DartMultiplerManager : MonoBehaviour
{
    [SerializeField] private Camera m_main;

    [SerializeField] private LayerMask m_plushieLayerMask;

    [Space]
    [SerializeField] private DartFXPlayer m_fxPlayer;

    private float m_multiplier = 0f;
    private bool m_enableInput = false;

    public float Multiplier { get { return m_multiplier; } }

    public UnityEvent<float> OnMultiplierSelected = new UnityEvent<float>();

    private void Update()
    {
        if (!m_enableInput)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, m_plushieLayerMask))
            {
                Plushie plushie = hitInfo.collider.GetComponent<Plushie>();
                if (plushie != null)
                {
                    m_enableInput = false;
                    m_fxPlayer.ShowMultiplier(m_multiplier, plushie.transform);
                    plushie.Enable(false);

                    EndMuliplierScene(m_multiplier);
                }
                else
                {
                    if (hitInfo.collider.tag == "MegaBalloon")
                    {
                        GameObject balloon = hitInfo.collider.gameObject;
                        balloon.SetActive(false);
                        m_fxPlayer.ShowMultiplier(m_multiplier, balloon.transform, true);

                        EndMuliplierScene(m_multiplier);
                    }
                }
            }
        }
    }

    public void StartMultiplierScene(MultiplierData data, bool isLeftSide, int levelId)
    {
        Debug.Log("start multiplier scene");

        //InitPlushies(data, isLeftSide, levelId);

        m_multiplier = data.GetRandomMultiplier(levelId);

        Debug.Log("multiplier = " + m_multiplier);

        m_enableInput = true;
    }
    public void EndMuliplierScene(float value)
    {
        StartCoroutine(EndMultiplierSceneCoroutine(value));
    }
    private IEnumerator EndMultiplierSceneCoroutine(float value)
    {
        yield return new WaitForSeconds(2.0f);
        m_enableInput = false;
        OnMultiplierSelected.Invoke(value);
    }
}
