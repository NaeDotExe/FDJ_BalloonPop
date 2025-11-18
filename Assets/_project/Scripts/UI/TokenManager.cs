using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TokensManager : MonoBehaviour
{
    [Header("Token Settings")]
    public GameObject tokenPrefab;       // UI token prefab (RectTransform + Image)
    public RectTransform pointA;         // Start UI position
    public RectTransform pointB;         // Target UI position
    public int tokenCount = 5;           // Number of tokens to spawn
    public float travelTime = 0.5f;      // Duration to fly to target
    public float staggerTime = 0.1f;     // Delay between each token spawn
    public float pullBackDistance = 50f; // How far the token pulls back initially
    public float spreadAmount = 50f;     // How far tokens are spread at spawn

    [Header("VFX")]
    public GameObject vfxAtPointB;       // Optional VFX prefab

    [Header("Optional Parent")]
    public RectTransform tokensParent;   // Where tokens will be spawned (Canvas or container)

    private void Start()
    {
        if (tokensParent == null && tokenPrefab != null)
        {
            // Use the Canvas as parent by default
            Canvas canvas = tokenPrefab.GetComponentInParent<Canvas>();
            if (canvas != null) tokensParent = canvas.transform as RectTransform;
        }

        SpawnTokens();
    }

    void SpawnTokens()
    {
        for (int i = 0; i < tokenCount; i++)
        {
            float delay = i * staggerTime;
            SpawnSingleToken(delay, i, tokenCount);
        }
    }

    void SpawnSingleToken(float delay, int index, int totalTokens)
    {
        GameObject token = Instantiate(tokenPrefab, tokensParent);
        RectTransform tokenRect = token.GetComponent<RectTransform>();

        // Base spawn position at pointA
        Vector2 spawnPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tokensParent, pointA.position, null, out spawnPos);

        // Spread tokens slightly (circular)
        float angleStep = 360f / totalTokens;
        float angle = index * angleStep * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spreadAmount;
        tokenRect.anchoredPosition = spawnPos + offset;

        // Target position in local space
        Vector2 targetPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tokensParent, pointB.position, null, out targetPos);

        // Pull-back vector (small backward move)
        Vector2 pullBack = (spawnPos - targetPos).normalized * pullBackDistance;

        // Create animation sequence
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(delay);

        // 1?? Pull-back from spawn
        seq.Append(tokenRect.DOAnchorPos(pullBack, 0.1f).SetRelative(true).SetEase(Ease.OutQuad));

        // 2?? Fly to target + scale down at same time
        seq.Append(tokenRect.DOAnchorPos(targetPos, travelTime).SetEase(Ease.OutBack));
        seq.Join(tokenRect.DOScale(Vector3.zero, travelTime).SetEase(Ease.InQuad));

        // 3?? On complete: spawn VFX and destroy token
        seq.OnComplete(() =>
        {
            if (vfxAtPointB != null)
            {
                GameObject vfx = Instantiate(vfxAtPointB, tokensParent);
                vfx.SetActive(true);
                RectTransform vfxRect = vfx.GetComponent<RectTransform>();
                if (vfxRect != null)
                    vfxRect.position = pointB.position;
                else
                    vfx.transform.position = pointB.position;

                var ps = vfx.GetComponent<ParticleSystem>();
                if (ps != null) ps.Play();
                Destroy(vfx, 1f);
            }

            Destroy(token);
        });
    }
}
