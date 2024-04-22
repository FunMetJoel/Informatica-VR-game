using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{






    public void healingPotion()
    {
        int healAmount = 20;
        GetComponent<Health>().Heal(healAmount);
    }


    public void apple()
    {
        double speed = 10;
        speed = speed * 1.2; //?????
    }


}
