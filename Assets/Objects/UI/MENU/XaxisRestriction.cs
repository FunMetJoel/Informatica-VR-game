using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XaxisRestriction : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {


    
        if(transform.localPosition.x >= 45)
        {
            transform.localPosition = new Vector3(45, 0, 0);  //hier
            //if position x-axis exeeds 45 turn it into 45
        }
        else if(transform.localPosition.x <= -45)
        {
            transform.localPosition = new Vector3(-45, 0, 0);  //hier
            //if position x-axis exeeds -45 turn it into -45
        }
        //else do nothing

            
    }
}
