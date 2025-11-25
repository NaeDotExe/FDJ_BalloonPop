using UnityEngine;

public class ScaleUpDown : MonoBehaviour
{
    [Header("Scale Settings")]
    public float baseScale = 1f;       // Default/base scale
    public float intensity = 0.2f;     // How much it scales up/down
    public float speed = 2f;           // How fast it pulses
    public bool loop = true;           // Toggle looping on/off

    private Vector3 initialScale;
    private float elapsedTime = 0f;

    void Start()
    {
        // Store the original scale
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (loop)
        {
            // Looping behavior: continuous pulse
            float scaleOffset = Mathf.Sin(Time.time * speed) * intensity;
            transform.localScale = initialScale * (baseScale + scaleOffset);
        }
        else
        {
            // Non-looping behavior: run once
            elapsedTime += Time.deltaTime;
            float scaleOffset = Mathf.Sin(elapsedTime * speed) * intensity;

            // Clamp to prevent going below baseScale once cycle finishes
            if (elapsedTime * speed >= Mathf.PI) // half sine wave = scale up then back to normal
            {
                transform.localScale = initialScale * baseScale;
            }
            else
            {
                transform.localScale = initialScale * (baseScale + scaleOffset);
            }
        }
    }
}
