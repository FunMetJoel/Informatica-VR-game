using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sliderbarfill : MonoBehaviour
{
    public Image barFill;
    public Transform slidecube;
    //public sliderLocation slide;
    //public Loadtime Xslide;
    // Update is called once per frame
    void Update()
    {
        //the fillamount of the bar is the same as the location given in the x-way 
        int XValue =  slidecube.localposition.x;    
        Xslide = (XValue + 45) / 90;   // hier   
        barFill.fillAmount = Xslide;     
    }
}