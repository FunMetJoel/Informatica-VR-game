using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using TMPro;
using JetBrains.Annotations;

public class SliceObject : MonoBehaviour
{
    //public WeaponController wc;

    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public LayerMask sliceableLayer;
    public VelocityEstimator velocityEstimator;

    public Material cutMaterial;

    public GameObject target;

    public float cutForce = 2000;

    public bool hasHit = false;
    public bool targetEmpty = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null){
            targetEmpty = true;
        }
        else{
            targetEmpty = false;
        }
        hasHit = Physics.Raycast(startSlicePoint.position, (endSlicePoint.position - startSlicePoint.position).normalized, out RaycastHit hit) &&  ( hit.collider.CompareTag("Cut") || hit.collider.CompareTag("Enemy"));
        
        if(hasHit && hit.collider.CompareTag("Cut"))
        {
            target = hit.transform.gameObject;
            slice(target);
        }
    }



    public void slice(GameObject target)
    {
        Debug.Log(target);

        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Debug.Log(velocity);
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();
        Debug.Log(planeNormal);

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
        Debug.Log(hull);


        if (hull !=null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
            setupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);
            setupSlicedComponent(lowerHull);

            Destroy(target);

        }
    }

    public void setupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        slicedObject.tag = "Cut";
        collider.convex = true;
        slicedObject.AddComponent<MeshFadeOut>();
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
    
}