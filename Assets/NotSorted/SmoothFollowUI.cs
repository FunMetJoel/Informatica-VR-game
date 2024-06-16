using UnityEngine;

public class SmoothFollowUI : MonoBehaviour
{
public Transform targetCamera; // Reference to the camera to which the UI is attached
    public Vector3 offset; // Offset in all directions
    public float baseSmoothSpeed = 3f; // Default speed of smoothing
    public float walkingSmoothSpeed = 10f; // Speed of smoothing when walking
    public float accelerationFactor = 7f; // Speed acceleration factor

    private Vector3 lastCameraPosition; // Previous frame's camera position
    private bool isMoving = false; // Flag to determine if the player is moving

    void Start()
    {
        // Initialize lastCameraPosition
        lastCameraPosition = targetCamera.position;
    }

    void Update()
    {
        // Ensure the target camera is assigned
        if (targetCamera == null)
        {
            Debug.LogWarning("Target camera is not assigned to the script.");
            return;
        }

        // Calculate the new target position with offset
        Vector3 targetPosition = targetCamera.position + targetCamera.TransformDirection(offset);

        // Calculate the current speed based on the distance moved by the camera
        float currentSpeed = Vector3.Distance(targetCamera.position, lastCameraPosition) / Time.deltaTime;

        // Determine if the player is moving
        isMoving = currentSpeed > 0.1f; // You may need to adjust this threshold based on your game's movement sensitivity

        // Calculate the smooth speed based on whether the player is moving or not
        float smoothSpeed = isMoving ? walkingSmoothSpeed : baseSmoothSpeed;

        // Apply acceleration factor
        smoothSpeed += currentSpeed * accelerationFactor;

        // Smoothly interpolate the position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Smoothly interpolate the rotation
        Quaternion targetRotation = Quaternion.LookRotation(targetCamera.forward, targetCamera.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);

        // Update lastCameraPosition for the next frame
        lastCameraPosition = targetCamera.position;
    }
}