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
    public bool isPlayerWeapon = false;

    //public GameObject HitParticle;


    private void Start()
    {
        so = FindObjectOfType<SliceObject>();
    }

    private void Update()
    {
        // Check if the cooldown period has expired, then clear the list of hit enemies
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            hitEnemies.Clear();
        }
    }




    public void DealDamage(GameObject enemy, bool vr)
    {
        if(enemy == null)
        {
            return;
        }
        if((wc == null || !wc.IsAttacking) && (!vr))
        {
            return;
        }
        if((enemy.tag == "Enemy" && isPlayerWeapon) || (enemy.tag == "Player" && !isPlayerWeapon))
        {
        if ((Time.time - lastDamageTime >= damageCooldown || !hitEnemies.Contains(enemy))) //&& wc.IsAttacking)
            {        
                                
                int hp = enemy.GetComponent<Health>().PublicHp;
                Debug.Log(weaponDamage);
                Debug.Log(hp);

                if(hp <= weaponDamage)
                {
                    enemy.tag = "Cut";
                }
                 
                Debug.Log(enemy.name);
                // enemy.GetComponent<Animator>().SetTrigger("Hit");
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
        DealDamage(other.gameObject, false);   
    }

    private void OnTriggerStay(Collider other) {
        DealDamage(other.gameObject, false); 
    }
    private void OnTriggerEnter(Collider other) {
        DealDamage(other.gameObject, true); 
    }
}
