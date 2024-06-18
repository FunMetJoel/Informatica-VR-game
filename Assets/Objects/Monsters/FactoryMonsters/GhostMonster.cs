using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMonster : MonoBehaviour, IMonster
{
    public void Attack()
    {
        Debug.Log("GhostMonster Attack");
    }

    public void Move()
    {
        Debug.Log("GhostMonster Move");
    }

    public void Die()
    {
        Debug.Log("GhostMonster Die");
    }

    public float Health { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }
    public float AttackRange { get; set; }
}
