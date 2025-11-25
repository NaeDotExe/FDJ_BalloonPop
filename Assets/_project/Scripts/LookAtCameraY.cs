using UnityEngine;

public class LookAtCameraY : MonoBehaviour
{
    public Transform cameraTransform;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector3 direction = cameraTransform.position - transform.position;

        // Ignore vertical rotation (Y-axis only)
        direction.y = 0;

        // If direction is not zero, rotate toward the camera
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }
}
