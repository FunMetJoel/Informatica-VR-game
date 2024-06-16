using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzlePiece : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    public bool isInPlace = false; // Track if the piece is in place

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Allow movement only in two directions
        // E.g., Lock the y-axis (allow only x and z movement)
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Check if the piece is in the correct position
        // Implement the logic to determine if the piece is correctly placed
        CheckPiecePosition();
    }

    private void CheckPiecePosition()
    {
        // Define the target position and acceptable margin of error
        Vector3 targetPosition = new Vector3(1, 0, 1); // Example target position
        float margin = 0.1f;

        if (Vector3.Distance(transform.position, targetPosition) < margin)
        {
            isInPlace = true;
            transform.position = targetPosition; // Snap to exact position
        }
        else
        {
            isInPlace = false;
        }
    }
}