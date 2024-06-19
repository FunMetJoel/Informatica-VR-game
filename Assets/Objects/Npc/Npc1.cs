using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Npc1 : MonoBehaviour
{
    public ShowText st;
    public WeaponController wc;

    public InputActionProperty buttonPressA;
    public InputActionProperty buttonPressB;
    public InputActionProperty buttonPressX;
    public InputActionProperty buttonPressY;

    public Dictionary<string, Dictionary<string, List<string>>> npcTextLists = new Dictionary<string, Dictionary<string, List<string>>>();
    private bool interactionStarted;

    private bool InRange = false;
    private string npc;

    public static class TextTypes
    {
        public const string StartInteraction = "startInteraction";
        public const string EndInteraction = "endInteraction";
        public const string TooPoor = "tooPoor";
        public const string Options = "options";
        public const string ResponseA = "responseA";
        public const string ResponseB = "responseB";
    }

    public static class Items
    {
        public const string HealingPotion = "HealingPotion";
        public const string Apple = "Apple";
        public const string Krakas = "Krakas";
        public const string Torch = "Torch";
    }

    public abstract class Character
    {
        public Dictionary<string, List<string>> Texts { get; private set; }

        protected Character(Dictionary<string, List<string>> texts)
        {
            Texts = texts;
        }
    }

    public class Ludar : Character
    {
        public Ludar() : base(TextFactory.CreateLudarTexts()) { }
    }

    public class Enitte : Character
    {
        public Enitte() : base(TextFactory.CreateEnitteTexts()) { }
    }

    public static class TextFactory
    {
        public static Dictionary<string, List<string>> CreateLudarTexts()
        {
            return new Dictionary<string, List<string>>
            {
                { TextTypes.StartInteraction, new List<string>
                    {
                        "A stranger! I have not seen a fellow human in two years! What brings you here? You seem to be stuck here too, aren’t you?",
                        "A stranger!",
                        "I have not seen a fellow human in two years!",
                        "What brings you here?",
                        "You seem to be stuck here too, aren’t you?"
                    }
                },
                { TextTypes.Options, new List<string>
                    {
                        "A: Is there no way to get out?\nB: So what do I do then?"
                    }
                },
                { TextTypes.ResponseA, new List<string>
                    {
                        "Well yes, but only if you manage to get through these chambers that is. See that door over there? Behind it are horrifying creatures you’ll need to defeat in order to pass. Behind that? More tests and puzzles. Escaping is seen as almost impossible. Take my sword and give it a shot!"
                    }
                },
                { TextTypes.ResponseB, new List<string>
                    {
                        "You're free to try and escape. But only the bravest, only the most agile, and only the smartest will pass these tests. Behind these doors, different trials await you, but I’ve never managed to pass them all. Take my sword and give it a shot!"
                    }
                },
                { TextTypes.EndInteraction, new List<string>
                    {
                        "Good luck adventurer!"
                    }
                }
            };
        }

        public static Dictionary<string, List<string>> CreateEnitteTexts()
        {
            return new Dictionary<string, List<string>>
            {
                { TextTypes.StartInteraction, new List<string>
                    {
                        "Impressive, newling. You have passed your first trial, but many more await you. Can I interest you in any items to ease your journey? I can see you have made quite some WOMPS already!"
                    }
                },
                { Items.HealingPotion, new List<string>
                    {
                        "Good choice, stranger. This will help you get back on your feet if you’re hurt."
                    }
                },
                { Items.Apple, new List<string>
                    {
                        "Clever choice, rookie. Swiftness in battle can be a great advantage against any enemy."
                    }
                },
                { Items.Krakas, new List<string>
                    {
                        "Excellent choice, very fitting for a starting adventurer like you. May you slay many enemies."
                    }
                },
                { Items.Torch, new List<string>
                    {
                        "Amazing choice my friend, who knows if you might need this further on in your journey."
                    }
                },
                { TextTypes.EndInteraction, new List<string>
                    {
                        "Remember, you can always come back to make more deals."
                    }
                }
            };
        }
    }

    public static class CharacterFactory
    {
        public static Character CreateCharacter(string characterType)
        {
            return characterType switch
            {
                "Ludar" => new Ludar(),
                "Enitte" => new Enitte(),
                _ => throw new ArgumentException("Invalid character type")
            };
        }
    }

    void Start()
    {
        Character ludar = CharacterFactory.CreateCharacter("Ludar");
        Character enitte = CharacterFactory.CreateCharacter("Enitte");

        npcTextLists["Ludar"] = ludar.Texts;
        npcTextLists["Enitte"] = enitte.Texts;

        wc = GetComponentInChildren<WeaponController>();
        st = FindObjectOfType<ShowText>();
    }

    void Update()
    {
        if (InRange && (Input.GetKeyDown(KeyCode.E) || buttonPressA.action.triggered) && !interactionStarted)
        {
            StartCoroutine(Interaction());
        }
    }

    private IEnumerator Interaction()
    {
        interactionStarted = true;
        st.NewText = true;

        yield return StartCoroutine(StartInteractionTexts());

        yield return StartCoroutine(QuestionsText());

        yield return StartCoroutine(EndInteractionTexts());

        interactionStarted = false;
        StartCoroutine(st.fadeOutText());
    }

    private IEnumerator StartInteractionTexts()
    {
        List<string> npcText = npcTextLists[npc][TextTypes.StartInteraction];
        st.header = npc;
        st.textValue = npcText[0];

        for (int i = 1; i < npcText.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);

            yield return new WaitUntil(() => !InRange || buttonPressA.action.triggered || buttonPressB.action.triggered || buttonPressX.action.triggered || buttonPressY.action.triggered && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D));

            if (!InRange)
            {
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

    private IEnumerator QuestionsText()
    {
        if (npcTextLists[npc].ContainsKey(TextTypes.Options))
        {
            List<string> questions = npcTextLists[npc][TextTypes.Options];
            List<string> responseAList = npcTextLists[npc][TextTypes.ResponseA];
            List<string> responseBList = npcTextLists[npc][TextTypes.ResponseB];

            for (int i = 0; i < questions.Count; i++)
            {
                yield return new WaitForSeconds(0.1f);

                yield return new WaitUntil(() => !InRange || buttonPressA.action.triggered || buttonPressB.action.triggered || buttonPressX.action.triggered || buttonPressY.action.triggered && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D));

                if (!InRange)
                {
                    interactionStarted = false;
                    yield break;
                }
                else
                {
                    st.header = "Player";
                    st.textValue = questions[i];
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || buttonPressA.action.triggered || buttonPressB.action.triggered);

                    if (Input.GetKey(KeyCode.Alpha1) || buttonPressA.action.inProgress)
                    {
                        st.header = npc;
                        st.textValue = responseAList[i];
                    }
                    else if (Input.GetKey(KeyCode.Alpha2) || buttonPressB.action.inProgress)
                    {
                        st.header = npc;
                        st.textValue = responseBList[i];
                    }
                    else
                    {
                        Debug.Log("No button pressed!");
                    }
                }
            }
        }
    }

    private IEnumerator EndInteractionTexts()
    {
        foreach (string text in npcTextLists[npc][TextTypes.EndInteraction])
        {
            yield return new WaitForSeconds(0.1f);

            yield return new WaitUntil(() => !InRange || buttonPressA.action.triggered || buttonPressB.action.triggered || buttonPressX.action.triggered || buttonPressY.action.triggered && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D));

            if (!InRange)
            {
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

    public void BuyInteraction(string itemBought, bool canBuy)
    {
        if (canBuy)
        {
            List<string> npcBuyText = npcTextLists[npc][itemBought];
            st.header = npc;
            st.textValue = npcBuyText[UnityEngine.Random.Range(0, npcBuyText.Count)];
        }
        else
        {
            List<string> npcCannotBuyText = npcTextLists[npc][TextTypes.TooPoor];
            st.header = npc;
            st.textValue = npcCannotBuyText[UnityEngine.Random.Range(0, npcCannotBuyText.Count)];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Npc"))
        {
            Debug.Log("Exit");
            InRange = false;
            StartCoroutine(st.fadeOutText());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Npc"))
        {
            Debug.Log("Enter");
            InRange = true;
            npc = other.gameObject.name.Substring(3);
        }
    }
}
