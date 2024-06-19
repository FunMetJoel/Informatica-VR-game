using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMonster : MonoBehaviour, IMonster
{
    public void Attack()
    {
        Debug.Log("SpiderMonster Attack");
    }

    public void Move()
    {
        Debug.Log("SpiderMonster Move");
    }

    public void Die()
    {
        Debug.Log("SpiderMonster Die");
    }

    public float Health { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRange { get; set; }
}
