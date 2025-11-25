using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatAmplitude = 0.1f; // height of bob
    public float floatFrequency = 1f;   // speed of bob

    [Header("Facing Camera")]
    public bool faceCamera = true;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition; // use local position so it floats relative to parent
    }

    void LateUpdate()
    {
        // Float effect
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);

        // Face the camera
        if (faceCamera && Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}
