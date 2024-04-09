using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    public SliceObject so;
    public float lastDamageTime = 0f;
    public float damageCooldown = 0.55f;
    public int weaponDamage = 1;
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




    public IEnumerator DealDamage(GameObject enemy, bool vr)
    {
        if(enemy == null)
        {
            yield break;
        }
        if((wc == null || !wc.IsAttacking) && (!vr))
        {
            yield break;
        }
        if(enemy.tag == "Enemy")
        {
        if ((Time.time - lastDamageTime >= damageCooldown || !hitEnemies.Contains(enemy)) && wc.IsAttacking)
            {
                // lastDamageTime = Time.time;
                // hitEnemies.Add(enemy);
                // yield return new WaitUntil(() => !so.targetEmpty);                
                                
                int hp = enemy.GetComponent<Health>().PublicHp;
                Debug.Log(weaponDamage);
                Debug.Log(hp);
                // Debug.Log(so.target);
                // Debug.Log(so.hasHit);
                // Debug.Log(so);
                // if(so != null && so.target != null){
                // if(so.hasHit && hp <= weaponDamage){
                    // Debug.Log("slice!");
                    // so.slice(so.target);
                //  }
                //  }

                if(hp <= weaponDamage)
                {
                    enemy.tag = "Cut";
                }
                 
                Debug.Log(enemy.name);
                enemy.GetComponent<Animator>().SetTrigger("Hit");
                Debug.Log(enemy.GetComponent<Health>().PublicHp);
                enemy.GetComponent<Health>().Damage(weaponDamage);
                lastDamageTime = Time.time;
                hitEnemies.Add(enemy);
                so.target = null;
                //Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
            }
        }
    }

    

    private void OnTriggerExit(Collider other) {
        StartCoroutine(DealDamage(other.gameObject, false));   
    }

    private void OnTriggerStay(Collider other) {
        StartCoroutine(DealDamage(other.gameObject, false)); 
    }
    private void OnTriggerEnter(Collider other) {
        StartCoroutine(DealDamage(other.gameObject, true)); 
    }
}
