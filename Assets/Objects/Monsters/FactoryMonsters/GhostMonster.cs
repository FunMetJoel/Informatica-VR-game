using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
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

    public int Health { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRange { get; set; }

    public void Setup()
    {
        GetComponent<Health>().MaxHealth = Health;
        GetComponent<walkAiTestEnemy>().Playerpos = FindObjectOfType<XROrigin>().gameObject.transform;

        attackCollider.size = new Vector3(AttackRange, 1, AttackRange);
    }

    [SerializeField]
    private BoxCollider attackCollider;
}
