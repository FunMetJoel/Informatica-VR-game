using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerHealth : MonoBehaviour
{

    public Health healthScript; 
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxShield;
    [SerializeField] private int health;

    [SerializeField] private Image RedHeart;
    [SerializeField] private Image YellowHeart;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = healthScript.PublicHp;
        RedHeart.fillAmount = Mathf.Clamp((float)health / (float)maxHealth, 0f, 1f);
        //Debug.Log((float)health / (float)maxHealth);
        YellowHeart.fillAmount = 0;//Mathf.Clamp(((float)health - maxHealth) / (float)maxShield, 0f, 1f);
    }
}
