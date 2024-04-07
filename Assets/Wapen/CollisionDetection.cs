using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    private float lastDamageTime = 0f;
    public float damageCooldown = 0.55f;
    private List<GameObject> hitEnemies = new List<GameObject>();    
    
    //public GameObject HitParticle;


        private void Update()
    {
        // Check if the cooldown period has expired, then clear the list of hit enemies
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            hitEnemies.Clear();
        }
    }




    private void DealDamage(GameObject enemy, bool vr)
    {
        if(enemy == null)
        {
            return;
        }
        if((wc == null || !wc.IsAttacking) && (!vr))
        {
            return;
        }
        if(enemy.tag == "Enemy")
        {
            if (Time.time - lastDamageTime >= damageCooldown || !hitEnemies.Contains(enemy))
            {
            Debug.Log(enemy.name);
            enemy.GetComponent<Animator>().SetTrigger("Hit");
            enemy.GetComponent<Health>().Damage(1);
            lastDamageTime = Time.time;
            hitEnemies.Add(enemy);
            //Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        DealDamage(other.gameObject, false);   
    }

    private void OnTriggerStay(Collider other) {
        DealDamage(other.gameObject, false);
    }
    private void OnTriggerEnter(Collider other) {
        DealDamage(other.gameObject, true);
    }
}

