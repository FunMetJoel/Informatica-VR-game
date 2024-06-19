using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] int Xslide;
    public void Volume()
    {
        //audiolistener VOLUME is the same as the Xslider, from the sliderbarfill script
        AudioListener.volume = Xslide;
    }
}
