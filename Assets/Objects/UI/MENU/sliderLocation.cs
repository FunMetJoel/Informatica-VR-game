using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sliderLocation : MonoBehaviour
{
    [SerializeField] int Xslide;

    private string SlideCube = "SlideCube";

    public Transform slidecube;

    // Update is called once per frame 
    void Update()
    {
        //float XValue = slidecube.localPosition.x;    
        //float Xslide = (XValue + 45) / 90;   // hier


    }   
}
