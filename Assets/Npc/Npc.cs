using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Npc : MonoBehaviour
{
    public ShowText st;
    public WeaponController wc;


    public InputActionProperty buttonPressA;
    public InputActionProperty buttonPressB;
    public InputActionProperty buttonPressX;
    public InputActionProperty buttonPressY;




    private bool InRange = false;
    private string npc;
    private bool interactionStarted = false;
    private Dictionary<string, Dictionary<string, List<string>>> npcTextLists = new Dictionary<string, Dictionary<string, List<string>>>();    
        
        
    
    
    
    const string startInteraction = "startInteraction";
    const string endInteraction = "endInteraction";
    const string tooPoor = "tooPoor";
    const string options = "options";
    const string responseA = "responseA";
    const string responseB = "responseB";

    
    // Enite items for sale
    const string healingPotion = "healingPotion";
    const string apple = "apple";
    const string krakas = "krakas";
    const string torch = "torch";



    public class Ludar
    {
        public Dictionary<string, List<string>> LudarTexts = new Dictionary<string, List<string>>();

        public Ludar()
        {
            LudarTexts[startInteraction] = new List<string>();
            LudarTexts[options] = new List<string>();
            LudarTexts[responseA] = new List<string>();
            LudarTexts[responseB] = new List<string>();
            LudarTexts[endInteraction] = new List<string>();


            // Always said text on start interacttion
            LudarTexts[startInteraction].Add("A stranger! I have not seen a fellow human in two years! What brings you here? You seem to be stuck here too, aren’t you?");
            LudarTexts[startInteraction].Add("A stranger!");
            LudarTexts[startInteraction].Add("I have not seen a fellow human in two years!");
            LudarTexts[startInteraction].Add("What brings you here?");
            LudarTexts[startInteraction].Add("You seem to be stuck here too, aren’t you?");
            
            // Items that are for sale
            // none


            //You too poor 

            // Quistions 
            LudarTexts[options].Add("A: Is there no way to get out?"  + " \n"  +  "B: So what do I do then?");


            LudarTexts[responseA].Add("Well yes, but only if you manage to get through these chambers that is. See that door over there? Behind it are horrifying creatures you’ll need to defeat in order to pass. Behind that? More tests and puzzles. Escaping is seen as almost impossible. Take my sword and give it a shot!");

            LudarTexts[responseB].Add("You're free to try and escape. But only the bravest, only the most agile, and only the smartest will pass these tests. Behind these doors, different trials await you, but I’ve never managed to pass them all. Take my sword and give it a shot!");  



            // Always said text on end interaction
            LudarTexts[endInteraction].Add("Good luck adventurer!");
        }
        
    }

    public class Enitte
    {
        public Dictionary<string, List<string>> EnitteTexts = new Dictionary<string, List<string>>();
        public Enitte()
        {


            EnitteTexts[startInteraction] = new List<string>();
            EnitteTexts[options] = new List<string>();
            EnitteTexts[responseA] = new List<string>();
            EnitteTexts[responseB] = new List<string>();
            EnitteTexts[endInteraction] = new List<string>();


            // items
            EnitteTexts[healingPotion] = new List<string>();
            EnitteTexts[apple] = new List<string>();
            EnitteTexts[krakas] = new List<string>();
            EnitteTexts[torch] = new List<string>();

            // Always said text on start interacttion
            EnitteTexts[startInteraction].Add("Impressive, newling. You have passed your first trial, but many more await you. Can I interest you in any items to ease your journey? I can see you have made quite some WOMPS already! ");

            // Items that are for sale
            EnitteTexts[healingPotion].Add("Good choice, stranger. This will help you get back on your feet if you’re hurt.");
            EnitteTexts[apple].Add("Clever choice, rookie. Swiftness in battle can be a great advantage against any enemy.");
            EnitteTexts[krakas].Add("Excellent choice, very fitting for a starting adventurer like you. May you slay many enemies.");
            EnitteTexts[torch].Add("Amazing choice my friend, who knows if you might need this further on in your journey.");


            //You too poor 

            // Quistions 
            // none
            // Always said text on end interaction
            EnitteTexts[endInteraction].Add("Remember, you can always come back to make more deals.");
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        Ludar ludar = new Ludar();
        Enitte enitte = new Enitte();




        //Ludar
        npcTextLists["Ludar"] = ludar.LudarTexts;
        // Enitte
        npcTextLists["Enitte"] = enitte.EnitteTexts;

    

        wc = GetComponentInChildren<WeaponController>();
        st = FindObjectOfType<ShowText>();    
    }

    // Update is called once per frame
    void Update()
    {

        // InputActionPropertys
        if (InRange && (Input.GetKeyDown(KeyCode.E) ||buttonPressA.action.triggered) && !interactionStarted)
        {
            StartCoroutine(Interaction());       
        }
    }

    private IEnumerator Interaction()
    {
        interactionStarted = true;

        yield return StartCoroutine(startInteractionTexts());
        
        yield return StartCoroutine(questionsText());

        yield return StartCoroutine(endInteractionTexts());

        interactionStarted = false;
        StartCoroutine(st.fadeOutText());
    }




    private IEnumerator startInteractionTexts()
    {
        List<string> npcText = npcTextLists[npc][startInteraction];
        st.header = npc;
        st.textValue = npcText[0];

        for (int i = 1; i < npcText.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);

            yield return new WaitUntil(() => !InRange || buttonPressA.action.triggered || buttonPressB.action.triggered || buttonPressX.action.triggered || buttonPressY.action.triggered && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D));

            if (!InRange)
            {
                // Was i finished speaking ?????
                interactionStarted = false;
                yield break;
            }
            else
            {
                st.textValue = npcText[i];
                st.header = npc;
            }
        }
    }



    private IEnumerator questionsText()
    {
        if(npcTextLists[npc][options] != null)
        {
            List<string> questions = npcTextLists[npc][options];
            List<string> responseAList = npcTextLists[npc][responseA];
            List<string> responseBList = npcTextLists[npc][responseB];
            for(int i = 0; i < questions.Count; i++)
            {
            yield return new WaitForSeconds(0.1f);

            yield return new WaitUntil(() => !InRange || buttonPressA.action.triggered || buttonPressB.action.triggered || buttonPressX.action.triggered || buttonPressY.action.triggered && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D));

            if (!InRange)
            {
                // Was i finished speaking ?????
                interactionStarted = false;
                yield break;
            }
            else
            {   
                st.header = "Player";
                st.textValue = questions[i];
                // if(Input.GetButtonDown(buttonName))
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || buttonPressA.action.triggered || buttonPressB.action.triggered);

                if(Input.GetKey(KeyCode.Alpha1) || buttonPressA.action.inProgress)
                {
                    st.header = npc;
                    st.textValue = responseAList[i];
                }
                else if(Input.GetKey(KeyCode.Alpha2) || buttonPressB.action.inProgress)
                {
                    st.header = npc;
                    st.textValue = responseBList[i];
                }
                else {
                    Debug.Log("NO button pressed!");
                }
            }
            }
        }
    }






    private IEnumerator endInteractionTexts()
    {
        foreach(string text in npcTextLists[npc][endInteraction])
        {
            yield return new WaitForSeconds(0.1f);

            yield return new WaitUntil(() => !InRange || buttonPressA.action.triggered || buttonPressB.action.triggered || buttonPressX.action.triggered || buttonPressY.action.triggered && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D));

            if (!InRange)
            {
                // Was i finished speaking ?????
                interactionStarted = false;
                yield break;
            }
            else
            {
                st.textValue = text;
                st.header = npc;
            }
        }
    }






    public void buyInteraction(string itemBought , bool canBuy)
    {
        if(canBuy)
        {
            List<string> npcBuyText = npcTextLists[npc][itemBought];
            st.header = npc;
            st.textValue = npcBuyText[Random.Range(0, npcBuyText.Count)];
        }
        else
        {
            List<string> npcCannotBuyText = npcTextLists[npc][tooPoor];
            st.header = npc;
            st.textValue = npcCannotBuyText[Random.Range(0, npcCannotBuyText.Count)];
        }
    }




    private void OnTriggerExit(Collider other) {
        if(other.tag == "Npc")
        {
            Debug.Log("Exit");
            InRange = false;
            StartCoroutine(st.fadeOutText());
            // wc.CanAttack = true;
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Npc")
        {
            // wc.CanAttack = false;
            InRange = true;
            npc = other.gameObject.name;
            npc = npc.Remove(0,3);
        }
    }


}
