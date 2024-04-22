using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Events;


//Reffrence https://www.youtube.com/watch?v=oZ2GWWjL4Fo

public class Health : MonoBehaviour
{
    [SerializeField] private Healthbar _healthbar;

    public SpawnGameObject sgo;
 
     void Start(){
        sgo = FindObjectOfType<SpawnGameObject>();
        _healthbar.UpdateHealthBar(MaxHealth,Hp);

    }
    [SerializeField] private int MaxHealth;
    [SerializeField] private int Hp;

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
                if(gameObject.CompareTag("Enemy")){
                    sgo.SpawnObject(transform.position, "womps");   
                }
                Died?.Invoke();
            }
        }
    }


    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent Died;

    private void Awake()
    {
        Hp = MaxHealth;
    }


        


    public void Damage(int amount) => PublicHp -= amount;
    public void Heal(int amount) => PublicHp += amount;
    public void Healfull() => PublicHp = MaxHealth;
    public void Kill() => PublicHp = 0;
    public void Adjust(int value) => PublicHp -= value;
}