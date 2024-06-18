using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XaxisRestriction : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {


    
        if(transform.localposition.x >= 45)
        {
            transform.localpositionposition.x = 45;  //hier
            //if position x-axis exeeds 45 turn it into 45
        }
        else if(transform.localposition <= -45)
        {
            transform.localposition.x = -45;  //hier
            //if position x-axis exeeds -45 turn it into -45
        }
        else {
        //Do nothing    
        }

            
    }
}
