using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SpiderMonster : MonoBehaviour, IMonster
{
    public void Attack()
    {
        Debug.Log("SpiderMonster Attack");
        animator.SetTrigger("Attack");
    }

    public void Move()
    {
        Debug.Log("SpiderMonster Move");
    }

    public void Die()
    {
        Debug.Log("SpiderMonster Die");
    }

    public void Setup()
    {
        GetComponent<Health>().MaxHealth = Health;

        GetComponent<walkAiTestEnemy>().Playerpos = FindObjectOfType<XROrigin>().gameObject.transform;
        
        // Set random version active
        int randomIndex = Random.Range(0, versions.Count);
        for (int i = 0; i < versions.Count; i++)
        {
            versions[i].SetActive(i == randomIndex);
            if (i == randomIndex)
            {
                animator = versions[i].GetComponent<Animator>();
            }
        }

        attackCollider.size = new Vector3(AttackRange, 1, AttackRange);
    }

    public int Health { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRange { get; set; }

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private List<GameObject> versions = new List<GameObject>();

    [SerializeField]
    private BoxCollider attackCollider;
}
