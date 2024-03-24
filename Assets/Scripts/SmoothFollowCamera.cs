using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    public Transform target; // The target the camera should follow (Assign your player GameObject here)
    public float smoothSpeed = 0.125f; // Adjust for smoother or more rigid camera movement
    public Vector3 offset; // Offset from the target

    void LateUpdate()
    {
        // Target position with offset
        Vector3 desiredPosition = target.position + offset;
        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
