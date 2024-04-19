using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [serializeField] private Image _healthbarSprite; 
    public void UpdateHealthBar(float MaxHealth, float Hp){
        _healthbarSprite.fillAmount = Hp / MaxHealth; 
    }


}   
