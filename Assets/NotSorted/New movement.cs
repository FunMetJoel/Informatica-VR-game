using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newmovement : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector2 playerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    [SerializeField] private LayerMask FloorMask, StairsMask;
    
    [SerializeField] private Transform FeetTransForm;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivty;
    [SerializeField] private float Jumpforce;
    [SerializeField] private float climbForce;
    [SerializeField] private float Force;



    void Start()
    {
        PlayerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (IsOnStairs())
        {
                // Apply force to move upwards
                PlayerBody.AddForce(Vector3.up * climbForce, ForceMode.Force);
        }
        else
        {
            playerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            MovePlayer();
            MovePlayerCamera();
        }
    }

    private void MovePlayer()
    {
        Vector3 movement = transform.forward * playerMovementInput.y + transform.right * playerMovementInput.x;
        PlayerBody.AddForce(movement.normalized * Force * Time.deltaTime);
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.CheckSphere(FeetTransForm.position, 0.1f, FloorMask))
            {
                PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            }
        }
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivty;
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivty, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

    }


    bool IsOnStairs()
    {
        // Cast a ray downwards to detect stairs
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, StairsMask))
        {
            // Check if the hit object is tagged as stairs
            if (hit.collider.CompareTag("Stairs"))
            {
                return true;
            }
        }
        return false;
    }
}
