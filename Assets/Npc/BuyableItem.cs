using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableItem : MonoBehaviour
{
    public SpawnGameObject sgo;
    public Coins c;

    private Dictionary<string, int> Items = new Dictionary<string, int>();    


    // Start is called before the first frame update
    void Start()
    {
        sgo = FindObjectOfType<SpawnGameObject>();
        c = FindObjectOfType<Coins>();
        
        Items["Frikandellen"] = 10;


    }

    // Update is called once per frame
    void buyItem(GameObject item)
    {
            Debug.Log("Exit");
            // Current position of object you buy
            // sgo.SpawnObject(other.transform.position);
            // Original position of the scritp
            sgo.SpawnObject(transform.position);
            item.tag = "Untagged";
            string itemName = item.name;
            c.buy(Items[itemName]);
            
    }


    void OnTriggerExit(Collider other)
    {
        if(other.tag == "BuyableItem")
        {
            buyItem(other.gameObject);
        }
    }
}
