using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public AudioClip SwordAttackSound;
    public bool IsAttacking = false;
    
    private bool Inrange = false;


    void Update() 
    {   
        if(CanAttack && Inrange)
        {
            SwordAttack();
        }
    }  

    public void SwordAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(SwordAttackSound);
        
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.55f);
        IsAttacking = false;

    }

    void OnTriggerEnter()
    {
        Debug.Log("Inrange");
        Inrange = true;
    }

    void OnTriggerExit()
    {
        Debug.Log("uitrange & thijs houdt van mannen");
        Inrange = false;
    }
}