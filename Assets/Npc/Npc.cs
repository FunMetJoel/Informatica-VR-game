using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Npc : MonoBehaviour
{
    public ShowText st;
    public WeaponController wc;

    private bool InRange = false;
    private string npc;
    private bool interactionStarted = false;
    private Dictionary<string, List<string>> npcTextLists = new Dictionary<string, List<string>>();    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        npcTextLists["Henk"] = new List<string> {"Geday Mate!", "Yodiyow gamer hoe is hett", "Dit is mijn tweede zin", "Dit is mijn derde zin." , "Vierde zin", "Vijfde zin"};
        npcTextLists["Alice"] = new List<string> {"Text for Alice"};

        wc = GetComponentInChildren<WeaponController>();
        st = GetComponentInChildren<ShowText>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (InRange && Input.GetKeyDown(KeyCode.E) && !interactionStarted)
        {
            StartCoroutine(Interaction());       
        }
    }

    private IEnumerator Interaction()
    {
        interactionStarted = true;
        List<string> npcText = npcTextLists[npc];
        st.header = npc;
        st.textValue = npcText[0];
        Debug.Log("before the for");

        for (int i = 1; i < npcText.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);

            yield return new WaitUntil(() => Input.anyKeyDown && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D) || !InRange);

            if (!InRange)
            {
                interactionStarted = false;
                yield break;
            }
            else
            {
                st.textValue = npcText[i];
            }
        }

    interactionStarted = false;
    }

    private void OnTriggerExit(Collider other) {
        InRange = false;
        st.header = null;
        st.textValue = null;
        wc.CanAttack = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Npc")
        {
            wc.CanAttack = false;
            InRange = true;
            npc = other.gameObject.name;
            npc = npc.Remove(0,3);
        }
    }


}
