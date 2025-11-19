using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DartFXPlayer : MonoBehaviour
{
    [SerializeField] private GameObject m_successParticleSystem;
    [SerializeField] private GameObject m_failureParticleSystem;

    [Space]
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_successAudioClip;
    [SerializeField] private AudioClip m_failureAudioClip;

    [SerializeField] private float m_zOffset = 0.1f;
    [SerializeField] private float m_successScaleMultiplier = 1f;
    [SerializeField] private float m_failScaleMultiplier = 0.8f;

    [Space]
    [SerializeField] private ParticleSystem m_multiplier15;
    [SerializeField] private ParticleSystem m_multiplier2;
    [SerializeField] private ParticleSystem m_multiplier3;
    [SerializeField] private ParticleSystem m_multiplier4;
    [SerializeField] private ParticleSystem m_multiplier5;
    [SerializeField] private ParticleSystem m_multiplier40;
    [SerializeField] private float m_multiplierScale = 0.6f;
    [SerializeField] private float m_multiplierXOffset = 1f;

    public void PlaySFX(Balloon balloon)
    {
        Vector3 pos = balloon.IsTrapped ? new Vector3(balloon.transform.position.x, balloon.transform.position.y, balloon.transform.position.z + m_zOffset) :
            balloon.transform.position;

        // tmp
        GameObject obj = Instantiate(balloon.IsTrapped ? m_failureParticleSystem : m_successParticleSystem, pos, Quaternion.identity);
        obj.transform.localScale = balloon.IsTrapped ? obj.transform.localScale * m_failScaleMultiplier :
            obj.transform.localScale * m_successScaleMultiplier;

        m_audioSource.PlayOneShot(balloon.IsTrapped ? m_failureAudioClip : m_successAudioClip);
    }

    public void ShowMultiplier(float value, Transform tr, bool megaBallon=false)
    {
        ParticleSystem obj;
        float scale = m_multiplierScale;
        if(megaBallon)
        {
            scale *= 10f;
        }

        if (value == 1.5f)
        {
            obj = Instantiate(m_multiplier15, new Vector3(tr.position.x + m_multiplierXOffset, tr.position.y, tr.position.z), tr.rotation);
            Debug.Log(obj.gameObject.transform.localScale);
            obj.gameObject.transform.localScale *= scale;
            Debug.Log(obj.gameObject.transform.localScale);

            obj.Play();
            StartCoroutine(LifeCycleCoroutine(obj.gameObject));
        }
        else if (value == 2f)
        {
            obj = Instantiate(m_multiplier2, new Vector3(tr.position.x + m_multiplierXOffset, tr.position.y, tr.position.z), tr.rotation);
            Debug.Log(obj.gameObject.transform.localScale);
            obj.gameObject.transform.localScale *= scale;

            Debug.Log(obj.gameObject.transform.localScale);

            obj.Play();
            StartCoroutine(LifeCycleCoroutine(obj.gameObject));
        }
        else if (value == 3f)
        {
            obj = Instantiate(m_multiplier3, new Vector3(tr.position.x + m_multiplierXOffset, tr.position.y, tr.position.z), tr.rotation);
            Debug.Log(obj.gameObject.transform.localScale);
            obj.gameObject.transform.localScale *= scale;

            Debug.Log(obj.gameObject.transform.localScale);

            obj.Play();
            StartCoroutine(LifeCycleCoroutine(obj.gameObject));
        }
        else if (value == 4f)
        {
            obj = Instantiate(m_multiplier4, new Vector3(tr.position.x + m_multiplierXOffset, tr.position.y, tr.position.z), tr.rotation);
            Debug.Log(obj.gameObject.transform.localScale);
            obj.gameObject.transform.localScale *= scale;

            Debug.Log(obj.gameObject.transform.localScale);

            obj.Play();
            StartCoroutine(LifeCycleCoroutine(obj.gameObject));
        }
        else if (value == 5f)
        {
            obj = Instantiate(m_multiplier5, new Vector3(tr.position.x + m_multiplierXOffset, tr.position.y, tr.position.z), tr.rotation);
            Debug.Log(obj.gameObject.transform.localScale);
            obj.gameObject.transform.localScale *= scale;

            Debug.Log(obj.gameObject.transform.localScale);

            obj.Play();
            StartCoroutine(LifeCycleCoroutine(obj.gameObject));
        }
        else if (value == 40f)
        {
            obj = Instantiate(m_multiplier40, new Vector3(tr.position.x + m_multiplierXOffset, tr.position.y, tr.position.z), tr.rotation);
            Debug.Log(obj.gameObject.transform.localScale);
            obj.gameObject.transform.localScale *= scale;

            Debug.Log(obj.gameObject.transform.localScale);

            obj.Play();
            StartCoroutine(LifeCycleCoroutine(obj.gameObject));
        }
    }

    private IEnumerator LifeCycleCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }
}
