using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int coins;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buy(int amount)
    {
        if(amount <= coins)
        {
            removeCoins(amount);
        }
        else
        {
            Debug.Log("You dont have enough money to buy this item");
        }
    }

    public void addCoins(int amount) => coins += amount;
    public void removeCoins(int amount) => coins += amount;
    

    

}
