using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;

public class MeshDestroy : MonoBehaviour
{
    // public VelocityEstimator velocityEstimator;
    public Material cutMaterial;
    public Rigidbody rb;

    private Vector3 globalPosition;

    public float cutForce = 2000;
    public int amountOfCuts = 5;
    public float destroyVelocity = 5;
    public int cutsPerformed = 0;
    public bool destroyStarted = false;

    private float force;
    private float lastforce;
    private Vector3 acceleration;
    private float mass;

    Vector3 lastVelocity;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        // velocityEstimator = GetComponent<VelocityEstimator>();
        // mass = rb.mass;
        if(destroyStarted && cutsPerformed < amountOfCuts){
            destroyMesh();
        }
    }

    void FixedUpdate()
    {
        // acceleration = (rb.velocity - lastVelocity) / Time.fixedDeltaTime;
        // lastVelocity = rb.velocity;
        // force = acceleration.magnitude * mass;
        // // Debug.Log("Force ALways " + force);
        // lastforce = force;

        //     // acceleration = velocityEstimator.GetAccelerationEstimate();
        //     // force = acceleration.magnitude * mass;
        //     // Debug.Log("Force ALways " + force);
        if(destroyStarted && cutsPerformed < amountOfCuts){
            destroyMesh();
        }    
     
    }

     public void destroyMesh()
    {
        globalPosition = transform.position;
        Debug.Log("MeshDestroy");
  // Get the mesh filter component of the target object
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.mesh == null)
        {
            Debug.LogError("MeshFilter component or mesh not found on the target object.");
            return;
        }

        // Get the mesh bounds of the target object
        Bounds bounds = meshFilter.mesh.bounds;

        // Generate a random point within the bounds of the target mesh
        Vector3 randomPoint = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
        randomPoint = transform.TransformPoint(randomPoint);
        // Generate a random direction as the normal vector of the plane
        Vector3 planeNormal = Random.onUnitSphere;

        GameObject target = this.transform.gameObject;

        // Slice the target mesh using the random point and normal vector
        SlicedHull hull = target.Slice(randomPoint, planeNormal);
        // SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
        Debug.Log(hull);


        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
            setupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);
            setupSlicedComponent(lowerHull);


            // Destroy the original target object
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Slicing did not produce a hull.");
        }
    }

    public void setupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        
        slicedObject.tag = "Cut";
        slicedObject.layer = LayerMask.NameToLayer("Interactable");
        collider.convex = true;
        
        MeshDestroy meshDestroyComponent = slicedObject.AddComponent<MeshDestroy>();
        meshDestroyComponent.destroyStarted = true;
        meshDestroyComponent.amountOfCuts = amountOfCuts;
        meshDestroyComponent.cutsPerformed = cutsPerformed +=1;
        meshDestroyComponent.cutMaterial = cutMaterial;


        MeshFadeOut meshFadeOutComponent = slicedObject.AddComponent<MeshFadeOut>();
        meshFadeOutComponent.fadeDelay = 3;
        meshFadeOutComponent.waitBeforeFade = 7.5f;

         slicedObject.AddComponent<XRGrabInteractable>();


        slicedObject.AddComponent<VelocityEstimator>();

        // transform.localToWorldMatrix


        rb.AddExplosionForce(cutForce, globalPosition, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.tag != "Player")
        {
            Vector3 velocity = collision.relativeVelocity;
            // acceleration = velocityEstimator.GetAccelerationEstimate();
            // force = acceleration.magnitude * mass;
            // Debug.Log("collidingforce" + lastforce);
            float idk = velocity.magnitude;
            Debug.Log("Velocity" + idk);

            if ( idk >= destroyVelocity) //lastforce
            {
                Debug.Log("Velocity Destroyyyy");
                destroyMesh();
            }
        }
    }
}
