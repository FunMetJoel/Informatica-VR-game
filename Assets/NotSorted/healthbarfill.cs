using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbarfill : MonoBehaviour
{
    public Image Hpbar; 
    public Health hp;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    int currenthp = hp.PublicHp;
    int maxhp = hp.PublicMaxHealth;     
    Hpbar.fillAmount = (float)currenthp / (float)maxhp;
    }
}
