using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public ShowText st;
    private bool InRange = false;
    



    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        st = GetComponentInChildren<ShowText>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (InRange && Input.GetKeyDown(KeyCode.E))
        {
            interaction();
        }
    }


    private void interaction()
    {
        st.header = "Npc 1";
        st.textValue = "Geday Mate!";
        print("Geday Mate!");
                

            
        
    }


    private void OnTriggerExit(Collider other) {
        InRange = false;  
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Npc")
        {
            InRange = true;
        }
    }


}
