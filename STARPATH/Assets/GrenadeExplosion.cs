using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    [SerializeField] private int damageType;    //0 for damage, 1 for stun
    [SerializeField] private bool damagePlayer;
    [SerializeField] private float stunTime, damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("grenade collision");
        if (other.tag == "Enemy")
        {
            MeleeEnemy enemy = other.GetComponent<MeleeEnemy>();

            if (enemy != null)
            {

                if(damageType == 0)
                {
                    enemy.health -= damage;
                    enemy.animator.SetBool("isHit", true);
                    if (enemy.health > 0)
                    {
                        enemy.TakeDamage();
                    }
                }
                else if(damageType == 1)
                {
                    enemy.Stun(stunTime);
                }
               

            }
        }
        else if (other.gameObject.tag == "BlasterEnemy" || other.gameObject.tag == "ShieldedBlasterEnemy")
        {
            BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
            if (blastEnemy != null)
            {
                if(damageType == 0)
                {
                    blastEnemy.health -= damage;

                    if (blastEnemy.health == 0)
                    {

                    }
                    else
                    {
                        blastEnemy.TakeDamage();
                    }
                }
                else if (damageType == 1)
                {
                    blastEnemy.Stun(stunTime);
                }
            }

        }
        else if (other.gameObject.tag == "Slime")
        {
            SlimeScript slime = other.gameObject.GetComponent<SlimeScript>();
            if (slime != null)
            {
                if(damageType == 0)
                {
                    slime.health -= damage;

                    if (slime.health == 0)
                    {

                    }
                    else
                    {
                        slime.TakeDamage();
                    }
                }
                else if (damageType == 1)
                {
                    //slime.Stun(stunTime);
                }
            }
        }
        else if (other.tag == "Turret")
        {
            Turret turret = other.GetComponent<Turret>();
            if (turret != null)
            {
                if(damageType == 0)
                {
                    turret.health -= damage;
                    turret.TakeDamage();
                }
                else if (damageType == 1)
                {
                    //enemy.Stun(stunTime);
                }
            }
        }

        else if (other.tag == "generator")
        {
            generatorscript gen = other.GetComponent<generatorscript>();
            if (gen != null && gen.health > 0 && damageType == 0)
            {
                gen.TakeDamage(damage);
                if (gen.health <= 0)
                {
                    gen.DamageOverflow();
                }
            
            }
        }
        else if (other.gameObject.tag == "Boss")
        {
            Boss boss = other.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                if(damageType == 0)
                {
                    

                    if (boss.health == 0)
                    {

                    }
                    else
                    {
                        boss.TakeDamage( damage);
                    }
                }
                
            }
        }

        //else if (other.tag == "DestructableObject){ damage that object }
    }
}
