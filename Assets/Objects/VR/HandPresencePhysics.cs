using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{

    public Transform target;
    [SerializeField]
    private Rigidbody rb;

    private Collider[] handColliders;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>();
    }

    public void EnableHandColliders()
    {
        foreach (var collider in handColliders)
        {
            collider.enabled = true;
        }
    }
    public void DisableHandColliders()
    {
        foreach (var collider in handColliders)
        {
            collider.enabled = false;
        }
    }

    public void EnableColliderDelay(float delay)
    {
        Invoke("EnableHandColliders", delay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(target.position + " " + transform.position + " " + (target.position - transform.position) / Time.fixedDeltaTime);
        rb.velocity = (target.position - transform.position)/Time.fixedDeltaTime;
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);

        Vector3 rotationDifferenceInDegree = angle * axis;

        rb.angularVelocity = rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime;
    }
}
