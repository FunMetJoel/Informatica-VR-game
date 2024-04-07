using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Npc : MonoBehaviour
{
    public ShowText st;
    private bool InRange = false;
    private string npc;
    private bool interactionStarted = false;


    private string npcTextList;
    private Dictionary<string, List<string>> npcTextLists = new Dictionary<string, List<string>>();

    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        npcTextLists["Henk"] = new List<string> {"Geday Mate!", "Yodiyow gamer hoe is hett", "Dit is mijn tweede zin", "Dit is mijn derde zin." , "Vierde zin", "Vijfde zin"};
        npcTextLists["Alice"] = new List<string> {"Text for NpcAlice"};


        st = GetComponentInChildren<ShowText>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (InRange && Input.GetKeyDown(KeyCode.E) && !interactionStarted)
        {
            interactionStarted = true;
            StartCoroutine(interaction());       
        }
    }


    private IEnumerator interaction()
    {
        List <string> npcText = npcTextLists[npc];
        st.header = npc;
        st.textValue = npcText[0];

        for(int i=1; i < npcText.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.anyKeyDown);
            st.textValue = npcText[i];
        }   
        interactionStarted = false;        
    }


    private void OnTriggerExit(Collider other) {
        InRange = false;
        

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Npc")
        {
            InRange = true;
            npc = other.gameObject.name;
            npc = npc.Remove(0,3);
        }
    }


}
