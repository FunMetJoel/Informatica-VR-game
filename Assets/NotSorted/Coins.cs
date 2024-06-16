using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public Npc npc;
    public int coins = 100;



    // Start is called before the first frame update
    void Start()
    {
        npc = GetComponentInChildren<Npc>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buy(int amount, string itemBought, GameObject item)
    {
        if(amount <= coins)
        {
            bool enoughMoney = true;
            Debug.Log($"Removing {amount} of coins");
            removeCoins(amount);
            npc.buyInteraction(itemBought, enoughMoney);
        }
        else
        {
            item.AddComponent<MeshFadeOut>(); 
            bool enoughMoney = false;
            npc.buyInteraction(itemBought, enoughMoney);
            Debug.Log("You dont have enough money to buy this item");
        }
    }

    public void addCoins(int amount) => coins += amount;
    public void removeCoins(int amount) => coins -= amount;
    

    

}
