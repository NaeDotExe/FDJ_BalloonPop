using UnityEngine;
using DG.Tweening;

public class TokensMove : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public GameObject vfxObj;  // VFX to play on arrival
    public float travelTime = 0.5f;

    private void Start()
    {
        transform.DOMove(pointB.position, travelTime)
     .SetEase(Ease.OutBack)
     .OnComplete(() =>
     {
         Debug.Log("COIN REACHED TARGET!");
         PlayVFX();
     });

    }

    void PlayVFX()
    {
        if (vfxObj == null)
        {
            Debug.LogError("VFX OBJECT IS NOT ASSIGNED!");
            return;
        }

        vfxObj.SetActive(true);

        // If it's a Particle System, play it:
        var ps = vfxObj.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }
    }


    void OnArrived()
    {
        // Enable VFX
        vfxObj.SetActive(true);

        // Optional: Play particle system manually
        var ps = vfxObj.GetComponent<ParticleSystem>();
        if (ps != null) ps.Play();

        // Destroy the coin (optional)
        Destroy(gameObject, 0.1f);
    }
}
