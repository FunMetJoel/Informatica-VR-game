using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[serializefield] int Xslide;
public class sliderLocation : MonoBehaviour
{
    public object slidecube;

        // Update is called once per frame 
    void Update()
    {
        int XValue =  GetComponent<slidecube>(localposition.x);    
        Xslide = (XValue + 45) / 90;   // hier
    }   
}
