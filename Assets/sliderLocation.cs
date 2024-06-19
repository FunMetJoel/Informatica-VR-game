using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sliderLocation : MonoBehaviour
{
    [serializefield] int Xslide;

    private string SlideCube = "SlideCube";

        // Update is called once per frame 
    void Update()
    {
        int XValue =  GetComponent<SlideCube>().transform.localPosition.x;    
        Xslide = (XValue + 45) / 90;   // hier
    }   
}
