using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//Reffrence https://www.youtube.com/watch?v=oZ2GWWjL4Fo

public class Healt : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    private int Hp;

    public int PublicMaxHealth => MaxHealth;

    public int PublicHp
    {
        get => Hp;
        private set
        {
            var isDamage = value < Hp;
            Hp = Mathf.Clamp(value, 0, MaxHealth);
            if (isDamage)
            {
                Damaged?.Invoke(Hp);
            }
            else
            {
                Healed?.Invoke(Hp);
            }
            if (Hp <= 0)
            {
                Died?.Invoke();
            }
        }
    }


    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent Died;

    void Awake()
    {
        Hp = MaxHealth;
    }


    public void Damage(int amount) => PublicHp -= amount;
    public void Heal(int amount) => PublicHp += amount;
    public void Healfull(int amount) => PublicHp = MaxHealth;
    public void Kill(int amount) => PublicHp = 0;
    public void Adjust(int amount) => PublicHp -= amount;

}

