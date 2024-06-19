using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sliderbarfill : MonoBehaviour
{
    public Image barFill;
    public Transform slidecube;

    public void Update()
    {
        //the fillamount of the bar is the same as the location given in the x-way 
        float XValue = slidecube.localPosition.x;    
        float Xslide = (XValue + 45) / 90;    
        barFill.fillAmount = Xslide;     
    }
}