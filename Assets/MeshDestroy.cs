// using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;
// using System.Numerics;


public class MeshDestroy : MonoBehaviour
{

    public VelocityEstimator velocityEstimator;
    public Material cutMaterial;
    public Rigidbody rb;
    
    public float cutForce = 2000;


    public int  amountOfCuts = 5;
    public float destroyForce = 20;
    public int cutsPreformed = 0;
    public bool destroyStarted = false;

    private float force;
    private Vector3 acceleration;
    private float mass;
    
    void Start()
       {
        if(destroyStarted)
        {
            rb = GetComponent<Rigidbody>();
            velocityEstimator = GetComponent<VelocityEstimator>();
        }
       }




   void FixedUpdate()
   {

   }


    public void destroyMesh()
    {
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

        // Generate a random direction as the normal vector of the plane
        Vector3 planeNormal = Random.onUnitSphere;

        GameObject target = this.transform.gameObject;

        // Slice the target mesh using the random point and normal vector
        SlicedHull hull = target.Slice(randomPoint, planeNormal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
            setupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);
            setupSlicedComponent(lowerHull);


            // Destroy the original target object
            Destroy(gameObject);
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
        meshDestroyComponent.cutsPreformed = cutsPreformed +=1;
        meshDestroyComponent.cutMaterial = cutMaterial;


        MeshFadeOut meshFadeOutComponent = slicedObject.AddComponent<MeshFadeOut>();
        meshFadeOutComponent.fadeDelay = 3;
        meshFadeOutComponent.waitBeforeFade = 7.5f;

         slicedObject.AddComponent<XRGrabInteractable>();


        slicedObject.AddComponent<VelocityEstimator>();

        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }


    private void OnTriggerEnter(Collider other)
    {
        mass = rb.mass;
        acceleration = velocityEstimator.GetAccelerationEstimate();
        force = acceleration.magnitude * mass;
        if(other.tag != "Player")
        {
        if((cutsPreformed <= amountOfCuts && destroyStarted) || force >= destroyForce)
            {
                destroyMesh();
            }
        }
    }

}




