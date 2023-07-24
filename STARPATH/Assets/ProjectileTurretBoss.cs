using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTurretBoss : MonoBehaviour
{
    public float speed;
    public int damage;
    public PlayerController1 playerHealth;

    [SerializeField] private ParticleSystem impactEffect;
    private Transform player;
    //private Vector2 target;
    public Vector2 target;

    [SerializeField] private bool deflected = false;

    void Start()
    {
        deflected = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x + (Random.Range(-5f, 5f)), player.position.y + (Random.Range(-5f, 5f)));
        //randomizeTarget(10f, 10f);
    }

    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y){
            Instantiate(impactEffect, transform.position, transform.rotation);
            DestroyProjectile();
        }

        /*
        if(Mathf.Abs((transform.position.x - target.x)) <= 5f && Mathf.Abs(transform.position.y - target.y) <= 5f){
            Instantiate(impactEffect, transform.position, transform.rotation);
            DestroyProjectile();
        }*/        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.CompareTag("Player"))
        //{
        //    playerHealth.TakeDamage(damage);
        //    DestroyProjectile();
        //}

        if (other.tag == "Player")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            other.gameObject.GetComponent<PlayerController1>().TakeDamage(damage);
        }


        if (other.gameObject.tag == "Untagged")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (deflected == true)
        {
            

            if (other.gameObject.tag == "Enemy")
            {
                MeleeEnemy enemy = other.gameObject.GetComponent<MeleeEnemy>();
                if (enemy != null)
                {
                    if (AbilityManager.AbilityManagerInstance.getUnlockAbility(2, 0) && AbilityManager.AbilityManagerInstance.GetShootMode() == 2)
                    {
                        enemy.health -= damage;
                    }
                    else
                    {
                        enemy.health -= (damage - (.3f));
                    }

                    enemy.animator.SetBool("isHit", true);
                    if (enemy.health == 0)
                    {

                    }
                    else
                    {
                        enemy.TakeDamage();
                    }
                }
            }
            else if (other.gameObject.tag == "BlasterEnemy")
            {
                BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
                if (blastEnemy != null)
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
            }
            else if (other.gameObject.tag == "ShieldedBlasterEnemy")
            {
                BlasterEnemy blastEnemy = other.gameObject.GetComponent<BlasterEnemy>();
                if (blastEnemy != null)
                {
                    blastEnemy.health -= (damage*.5f);

                    if (blastEnemy.health == 0)
                    {

                    }
                    else
                    {
                        blastEnemy.TakeDamage();
                    }
                }
            }
            else if (other.gameObject.tag == "Slime")
            {
                SlimeScript slime = other.gameObject.GetComponent<SlimeScript>();
                if (slime != null)
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
            }
            else if (other.tag == "Turret")
            {
                Turret turret = other.GetComponent<Turret>();
                if (turret != null)
                {
                    turret.health -= damage;


                    turret.TakeDamage();
                    turret.Stun(30f);

                }
            }
            else if (other.tag == "generator")
            {
                generatorscript gen = other.GetComponent<generatorscript>();
                if (gen != null && gen.health > 0)
                {
                    gen.TakeDamage(damage);

                }
            }
            else if (other.gameObject.tag == "Boss")
            {
                Boss boss = other.gameObject.GetComponent<Boss>();
                if (boss != null)
                {
                    

                    if (boss.health == 0)
                    {

                    }
                    else
                    {
                        boss.TakeDamage(damage);
                    }
                }
            }
            DestroyProjectile();
        }
    }

    public void Deflect()
    {
        deflected = true;
        speed = -(speed * 3);

        target.x += (Random.Range(-1f, 1f));
        target.y += (Random.Range(-1f, 1f));
    }

    public void randomizeTarget(float xParam, float yParam)
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        //target = new Vector2(player.position.x, player.position.y);
        //target = new Vector2(player.position.x + Random.Range(-xParam, xParam), player.position.y + Random.Range(-yParam, yParam));

        target.x += (Random.Range(-xParam, xParam));
        target.y += (Random.Range(-yParam, yParam));
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}