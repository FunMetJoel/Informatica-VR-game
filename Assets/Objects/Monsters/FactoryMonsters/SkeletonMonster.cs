using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMonster : MonoBehaviour, IMonster
{
    public void Attack()
    {
        Debug.Log("SkeletonMonster Attack");
    }

    public void Move()
    {
        Debug.Log("SkeletonMonster Move");
    }

    public void Die()
    {
        Debug.Log("SkeletonMonster Die");
    }

    public void Setup()
    {
        GetComponent<Health>().MaxHealth = Health;
        attackCollider.size = new Vector3(AttackRange, 1, AttackRange);
    }

    [SerializeField]
    private BoxCollider attackCollider;

    public int Health { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRange { get; set; }
}
